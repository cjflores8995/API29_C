using CoreSwitch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business
{
    public class Fitbank
    {
        #region metodos generales

        public TBTHDETALLEPROCESO ExecutaTransaccionUci(TBTHDETALLEPROCESO obj, Int32 hilo)
        {
            AutFitbankXml.FITBANK objFit = new AutFitbankXml.FITBANK();
            string error = string.Empty;

            try
            {
                #region arma detail

                if (obj.transaccion != null)
                {
                    string[] comando = obj.transaccion.COMANDO.Split('.');
                    Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                    ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                    object reflecClassObject = constructor.Invoke(new object[] { });
                    MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                    obj = (TBTHDETALLEPROCESO)reflecMethod.Invoke(reflecClassObject, new object[] { obj, hilo });
                }

                #endregion arma detail

                if (obj.CERROR == "000" && !string.IsNullOrEmpty(obj.detailEntrada))
                {
                    try
                    {
                        #region balanceo

                        wsUCI.UCIWSBeanService wsUCI = new wsUCI.UCIWSBeanService();
                        wsUCI.Timeout = BthProcesos.glbFitbankTimeOut;

                        if (!BthProcesos.glbFitbankBalanceo)
                        {
                            wsUCI.Url = String.Format(BthProcesos.glbFitbankUrlS29, BthProcesos.glbFitbankIpNodo1);
                            obj.SERVIDOR = BthProcesos.glbFitbankIpNodo1;
                        }
                        else
                        {
                            if (obj.SECUENCIA.Value % 2 == 0)
                            {
                                wsUCI.Url = String.Format(BthProcesos.glbFitbankUrlS29, BthProcesos.glbFitbankIpNodo1);
                                obj.SERVIDOR = BthProcesos.glbFitbankIpNodo1;
                            }
                            else
                            {
                                wsUCI.Url = String.Format(BthProcesos.glbFitbankUrlS29, BthProcesos.glbFitbankIpNodo2);
                                obj.SERVIDOR = BthProcesos.glbFitbankIpNodo2;
                            }
                        }

                        #endregion balanceo

                        obj.CESTADO = "PROFIT";
                        obj.FACTUALIZACION = DateTime.Now;
                        obj.CERROR = null;
                        obj.DERROR = null;

                        if (new TBTHDETALLEPROCESO().Actualizar(obj))
                        {
                            Logging.EscribirLog(obj.detailEntrada, null, "FIT");

                            obj.timeCore = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                            obj.timeCore.Value.Reset();
                            obj.timeCore.Value.Start();

                            try
                            {
                                obj.detailSalida = wsUCI.processXML(obj.detailEntrada);
                            }
                            catch (Exception ex)
                            {
                                obj.CERROR = "907";
                                obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " - WS - " + ex.Message.ToString();
                                obj.detailSalida = string.Empty;
                            }

                            obj.timeCore.Value.Stop();
                            obj.TIEMPOCORE = Math.Round(obj.timeCore.Value.Elapsed.TotalSeconds, 2);

                            if (obj.CERROR != "907" && !string.IsNullOrEmpty(obj.detailSalida))
                            {
                                Logging.EscribirLog(obj.detailSalida, null, "FIT");

                                try { objFit = StringToFitBank(obj.detailSalida); }
                                catch { objFit = null; }

                                if (objFit != null)
                                {
                                    try
                                    {
                                        var response = (AutFitbankXml.FITBANKGRS)objFit.Items[2];
                                        obj.CERROR = response.cod.Trim() == "0" ? "000" : response.cod.Trim();
                                        obj.DERROR = response.MSGU;
                                        var grq = (AutFitbankXml.FITBANKGRQ)objFit.Items[0];
                                        obj.NUMEROMENSAJE = grq.MSG;

                                        if (obj.CERROR == "000" && (obj.CTIPOTRANSACCION == "NOTDEBPAR"))
                                        {
                                            try
                                            {
                                                var recuperado = (AutFitbankXml.FITBANKDET)objFit.Items[1];
                                                var rec = recuperado.CTL[0];
                                                obj.VALORMOVIMIENTO = Convert.ToDecimal(rec.CAM.Where(x => x.name.ToUpper() == "LMONTOCOBRADO").FirstOrDefault().VAL.Trim().Replace(".", ","));
                                            }
                                            catch (Exception ex)
                                            {
                                                obj.CERROR = "909";
                                                obj.DERROR = "ERROR OBTENIENDO VALOR DEBITADO: " + ex.Message.ToString().ToUpper();
                                                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.CERROR = "908";
                                        obj.DERROR = "ERROR EN DETAIL RESPUESTA FITBANK: " + ex.Message.ToString().ToUpper();
                                    }
                                }
                                else
                                {
                                    obj.CERROR = "901";
                                    obj.DERROR = "ERROR AL SERIALIZAR RESPUESTA FITBANK";
                                }
                            }
                        }
                        else
                        {
                            obj.CESTADO = "ERR";
                            obj.CERROR = "999";
                            obj.DERROR = "ERROR ACTUALIZANDO PREVIO ENVIO FITBANK: " + error;
                        }
                    }
                    catch (Exception ex)
                    {
                        obj.CERROR = "909";
                        obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                    }
                }
                else
                {
                    obj.CERROR = "901";
                    obj.DERROR = "ERROR AL ARMAR DETAIL DE TRANSACCION" + obj.DERROR;
                }
            }
            catch (Exception ex)
            {
                obj.CERROR = "909";
                obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return obj;
        }

        public TBTHPROCESO ExecutaTransaccionCargaLote(TBTHPROCESO obj)
        {
            AutFitbankXml.FITBANK objFit = new AutFitbankXml.FITBANK();
            DateTime? fechalote = null;
            Int32? numerolote = null;
            string error = string.Empty;

            try
            {
                obj = ParseCargaLotes(obj);

                #region arma detail

                //if (obj.transaccion != null)
                //{
                //    string[] comando = obj.transaccion.COMANDO.Split('.');
                //    Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                //    ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                //    object reflecClassObject = constructor.Invoke(new object[] { });
                //    MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                //    obj = (TBTHPROCESO)reflecMethod.Invoke(reflecClassObject, new object[] { obj });
                //}

                #endregion arma detail

                if (obj.CERROR == "000" && !string.IsNullOrEmpty(obj.detailEntrada))
                {
                    try
                    {
                        wsUCI.UCIWSBeanService wsUCI = new wsUCI.UCIWSBeanService();
                        wsUCI.Timeout = BthProcesos.glbFitbankTimeOut;
                        wsUCI.Url = String.Format(BthProcesos.glbFitbankUrlNormal, BthProcesos.glbFitbankIpLotes);

                        obj.CERROR = null;
                        obj.DERROR = null;

                        Logging.EscribirLog(obj.detailEntrada, null, "FIT");

                        try
                        {
                            obj.detailSalida = wsUCI.processXML(obj.detailEntrada);
                        }
                        catch (Exception ex)
                        {
                            obj.CERROR = "907";
                            obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " - WS - " + ex.Message.ToString();
                            obj.detailSalida = string.Empty;
                        }

                        if (obj.CERROR != "907" && !string.IsNullOrEmpty(obj.detailSalida))
                        {
                            Logging.EscribirLog(obj.detailSalida, null, "FIT");

                            try { objFit = StringToFitBank(obj.detailSalida); }
                            catch { objFit = null; }

                            if (objFit != null)
                            {
                                try
                                {
                                    var response = (AutFitbankXml.FITBANKGRS)objFit.Items[2];
                                    obj.CERROR = response.cod.Trim() == "0" ? "000" : response.cod.Trim();
                                    obj.DERROR = response.MSGU;
                                    var grq = (AutFitbankXml.FITBANKGRQ)objFit.Items[0];

                                    if (obj.CERROR == "000")
                                    {
                                        var recuperado = (AutFitbankXml.FITBANKDET)objFit.Items[1];
                                        var lote = recuperado.TBL.Where(x => x.name.ToUpper() == "TLOTEMENSAJES").FirstOrDefault();

                                        try { fechalote = Convert.ToDateTime(lote.REG[0].CAM.Where(x => x.name.ToUpper() == "FECHALOTE").FirstOrDefault().VAL.Trim()); }
                                        catch { fechalote = new DateTime(2999, 12, 31); };

                                        try { numerolote = Convert.ToInt32(lote.REG[0].CAM.Where(x => x.name.ToUpper() == "NUMEROLOTE").FirstOrDefault().VAL.Trim()); }
                                        catch { numerolote = 0; }

                                        obj.DATOSLOTE = fechalote.Value.ToString("dd/MM/yyyy") + ";" + numerolote.Value;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    obj.CERROR = "908";
                                    obj.DERROR = "ERROR EN DETAIL RESPUESTA FITBANK: " + ex.Message.ToString().ToUpper();
                                }
                            }
                            else
                            {
                                obj.CERROR = "901";
                                obj.DERROR = "ERROR AL SERIALIZAR RESPUESTA FITBANK";
                            }
                        }
                        else
                        {
                            obj.CERROR = "907";
                            obj.DERROR = "ERROR RESPUESTA FITBANK";
                        }
                    }
                    catch (Exception ex)
                    {
                        obj.CERROR = "909";
                        obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                    }
                }
                else
                {
                    obj.CERROR = "901";
                    obj.DERROR = "ERROR AL ARMAR DETAIL DE TRANSACCION" + obj.DERROR;
                }
            }
            catch (Exception ex)
            {
                obj.CERROR = "909";
                obj.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return obj;
        }

        public string GeneraMensaje(string proceso, string secuencia)
        {
            return String.Format(
                BthProcesos.glbFitbankTemNmgNor,
                Util.GetSecuencial(5),
                proceso.PadLeft(10, '0'),
                secuencia.PadLeft(10, '0'));
        }

        public string GeneraMensajeReverso(string proceso, string secuencia)
        {
            return String.Format(
                BthProcesos.glbFitbankTemNmgRev,
                Util.GetSecuencial(5),
                proceso.PadLeft(10, '0'),
                secuencia.PadLeft(10, '0'));
        }

        public AutFitbankXml.FITBANKGRQ ArmaGRQ(TBTHDETALLEPROCESO det)
        {
            AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();

            try
            {
                grq.ROL = BthProcesos.glbFitbankRol;

                try { grq.USR = BthProcesos.glbFitbankUsuario; }
                catch { grq.USR = BthProcesos.glbFitbankUsuario; }

                grq.SID = BthProcesos.glbFitbankSession;
                grq.IDM = BthProcesos.glbFitbankIdioma;
                grq.IPA = BthProcesos.glbFitbankIp;
                grq.TER = BthProcesos.glbFitbankTerminal;
                grq.SUB = det.transaccion.CSUBSISTEMA;
                grq.TRN = det.transaccion.CTRANSACCION;
                grq.VER = det.transaccion.VERSIONTRANSACCION;
                grq.TIP = det.transaccion.TIPO;
                grq.ARE = BthProcesos.glbFitbankArea;
                grq.TPP = "Join";
                grq.CIO = "2";

                try { grq.SUC = det.CSUCURSAL.Value.ToString(); }
                catch { grq.SUC = BthProcesos.glbFitbankSucursal; }

                try { grq.OFC = det.COFICINA.Value.ToString(); }
                catch { grq.OFC = BthProcesos.glbFitbankOficina; }

                if (det.CTIPOTRANSACCION != "REVERSO")
                {
                    grq.MSG = GeneraMensaje(det.CPROCESO.Value.ToString(), det.SECUENCIA.Value.ToString());
                }
                else
                {
                    grq.MSG = GeneraMensaje(det.CPROCESO.Value.ToString(), det.SECUENCIA.Value.ToString());
                }

                grq.REV = "0";
                grq.CAN = BthProcesos.glbFitbankCanal;
                grq.FCN = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                grq = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }

            return grq;
        }

        public AutFitbankXml.FITBANKGRQ ArmaGRQ(TBTHPROCESO det)
        {
            AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();

            try
            {
                grq.ROL = BthProcesos.glbFitbankRol;

                try { grq.USR = BthProcesos.glbFitbankUsuario; }
                catch { grq.USR = BthProcesos.glbFitbankUsuario; }

                grq.SID = BthProcesos.glbFitbankSession;
                grq.IDM = BthProcesos.glbFitbankIdioma;
                grq.IPA = BthProcesos.glbFitbankIp;
                grq.TER = BthProcesos.glbFitbankTerminal;
                grq.SUB = det.transaccion.CSUBSISTEMA;
                grq.TRN = det.transaccion.CTRANSACCION;
                grq.VER = det.transaccion.VERSIONTRANSACCION;
                grq.TIP = det.transaccion.TIPO;
                grq.ARE = BthProcesos.glbFitbankArea;
                grq.TPP = "Join";
                grq.CIO = "2";

                grq.SUC = BthProcesos.glbFitbankSucursal;

                grq.OFC = BthProcesos.glbFitbankOficina;

                grq.MSG = GeneraMensaje(det.CPROCESO.Value.ToString(), "0");

                grq.REV = "0";
                grq.CAN = BthProcesos.glbFitbankCanal;
                grq.FCN = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                grq = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }

            return grq;
        }

        public AutFitbankXml.FITBANKGRQ ArmaGRQReverso(TBTHDETALLEPROCESO det)
        {
            AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();

            try
            {
                grq.ROL = BthProcesos.glbFitbankRol;

                try { grq.USR = BthProcesos.glbFitbankUsuario; }
                catch { grq.USR = BthProcesos.glbFitbankUsuario; }

                grq.SID = BthProcesos.glbFitbankSession;
                grq.IDM = BthProcesos.glbFitbankIdioma;
                grq.IPA = BthProcesos.glbFitbankIp;
                grq.TER = BthProcesos.glbFitbankTerminal;
                grq.SUB = det.transaccion.CSUBSISTEMA;
                grq.TRN = det.transaccion.CTRANSACCION;
                grq.VER = det.transaccion.VERSIONTRANSACCION;
                grq.TIP = det.transaccion.TIPO;
                grq.ARE = BthProcesos.glbFitbankArea;
                grq.TPP = "Join";
                grq.CIO = "2";

                try { grq.SUC = det.CSUCURSAL.Value.ToString(); }
                catch { grq.SUC = BthProcesos.glbFitbankSucursal; }

                try { grq.OFC = det.COFICINA.Value.ToString(); }
                catch { grq.OFC = BthProcesos.glbFitbankOficina; }

                grq.MSG = GeneraMensajeReverso(det.CPROCESO.Value.ToString(), det.SECUENCIA.Value.ToString());
                grq.NMR = det.NUMEROMENSAJE_ORIGINAL;

                grq.REV = "1";
                grq.CAN = BthProcesos.glbFitbankCanal;
                grq.FCN = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                grq = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return grq;
        }

        public String ArmaDescripcion(TBTHDETALLEPROCESO registro)
        {
            string descripcion = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(registro.DESCRIPCIONMOVIMIENTO))
                    descripcion = "PROCESO BATCH " + registro.CPROCESO.Value.ToString() + " - " + registro.SECUENCIA.Value.ToString();
                else
                    descripcion = registro.DESCRIPCIONMOVIMIENTO;
            }
            catch
            {
                descripcion = string.Empty;
            }

            return descripcion;
        }

        public string DecimalToString(Decimal valor)
        {
            return valor.ToString("F2").Replace(',', '.');
        }

        public string ArmaNumeroDocumento(TBTHDETALLEPROCESO registro)
        {
            string numeroDocumento = string.Empty;
            try
            {
                numeroDocumento = registro.CPROCESO.Value.ToString().PadLeft(10, '0') + registro.SECUENCIA.Value.ToString().PadLeft(5, '0');
            }
            catch
            {
                numeroDocumento = string.Empty;
            }

            return numeroDocumento;
        }

        #endregion metodos generales

        #region metodos parce

        public TBTHDETALLEPROCESO ParseNotaCredito(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[1];

                #region FINANCIERO

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "FINANCIERO";
                det.TBL[0].blq = "0";
                det.TBL[0].financial = "true";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "FINANCIERO";
                det.TBL[0].npg = "1";
                det.TBL[0].ract = "0";
                det.TBL[0].readOnly = "true";

                #region EFECTIVO

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];
                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "1";
                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[12];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[0].name = "CODIGO";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = registro.RUBRO.Value.ToString();

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[1].name = "CUENTA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = registro.CUENTA;

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[2].name = "COMPANIA";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = "2";

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[3].name = "SUBCUENTA";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = "0";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[4].name = "MONEDACUENTA";
                det.TBL[0].REG[0].CAM[4].pk = "0";
                det.TBL[0].REG[0].CAM[4].VAL = "USD";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[5].name = "CODIGOCONTABLE";
                det.TBL[0].REG[0].CAM[5].pk = "0";

                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[6].name = "SUCURALDESTINO";
                det.TBL[0].REG[0].CAM[6].pk = "0";

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[7].name = "OFICINADESTINO";
                det.TBL[0].REG[0].CAM[7].pk = "0";

                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[8].name = "MONEDAORIGINAL";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = "USD";

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[9].name = "VALOR";
                det.TBL[0].REG[0].CAM[9].pk = "0";
                det.TBL[0].REG[0].CAM[9].VAL = DecimalToString(registro.VALOR.Value);

                det.TBL[0].REG[0].CAM[10] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[10].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[10].name = "DESCRIPCION";
                det.TBL[0].REG[0].CAM[10].pk = "0";
                det.TBL[0].REG[0].CAM[10].VAL = ArmaDescripcion(registro);

                det.TBL[0].REG[0].CAM[11] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[11].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[11].name = "FECHAVENCIMIENTO";
                det.TBL[0].REG[0].CAM[11].pk = "0";
                #endregion EFECTIVO

                #endregion FINANCIERO

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[6];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "DOCUMENTO";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = ArmaNumeroDocumento(registro);

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "CUENTALIBRETA";
                det.CTL[0].CAM[1].pk = "0";

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "DESCRIPCION";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "_LOTE_THREAD_NUMBER";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = hilo.ToString();

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "CMONEDA";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = "USD";

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "MONEDACUENTA";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = "USD";

                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHDETALLEPROCESO ParseNotaDebito(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();
            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ
                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[1];

                #region FINANCIERO

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "FINANCIERO";
                det.TBL[0].blq = "0";
                det.TBL[0].financial = "true";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "FINANCIERO";
                det.TBL[0].npg = "1";
                det.TBL[0].ract = "0";
                det.TBL[0].readOnly = "true";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];
                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "1";
                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[12];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[0].name = "CODIGO";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = registro.RUBRO.Value.ToString();

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[1].name = "CUENTA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = registro.CUENTA;

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[2].name = "COMPANIA";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = "2";

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[3].name = "SUBCUENTA";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = "0";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[4].name = "MONEDACUENTA";
                det.TBL[0].REG[0].CAM[4].pk = "0";
                det.TBL[0].REG[0].CAM[4].VAL = "USD";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[5].name = "CODIGOCONTABLE";
                det.TBL[0].REG[0].CAM[5].pk = "0";

                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[6].name = "SUCURALDESTINO";
                det.TBL[0].REG[0].CAM[6].pk = "0";

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[7].name = "OFICINADESTINO";
                det.TBL[0].REG[0].CAM[7].pk = "0";

                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[8].name = "MONEDAORIGINAL";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = "USD";

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[9].name = "VALOR";
                det.TBL[0].REG[0].CAM[9].pk = "0";
                det.TBL[0].REG[0].CAM[9].VAL = DecimalToString(registro.VALOR.Value);

                det.TBL[0].REG[0].CAM[10] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[10].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[10].name = "DESCRIPCION";
                det.TBL[0].REG[0].CAM[10].pk = "0";
                det.TBL[0].REG[0].CAM[10].VAL = ArmaDescripcion(registro);

                det.TBL[0].REG[0].CAM[11] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[11].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[11].name = "FECHAVENCIMIENTO";
                det.TBL[0].REG[0].CAM[11].pk = "0";

                #endregion FINANCIERO

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[5];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "DOCUMENTO";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = ArmaNumeroDocumento(registro);

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "DOCUMENTOFINAL";
                det.CTL[0].CAM[1].pk = "0";

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "CUENTADEBITO";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = registro.CUENTA;

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "POSTEO";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = "1";

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "DESCRIPCION";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = ArmaDescripcion(registro);

                #endregion CTL

                #endregion DET
                fit.Items = new object[] { grq, det };
                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return registro;
        }

        public TBTHDETALLEPROCESO ParseNotaDebitoParcial(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();
            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                #region CTL
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[12];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "IS_S29";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = "1";

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "CUENTADEBITO";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = registro.CUENTA;

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "MONTO";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = DecimalToString(registro.VALORPENDIENTE.Value);

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "LCSUBSISTEMA";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = transaccion.CSUBSISTEMA;

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "LCTRANSACCION";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = transaccion.CTRANSACCION;

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "LVERSION";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = transaccion.VERSIONTRANSACCION;

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "LRUBRO";
                det.CTL[0].CAM[6].pk = "0";
                det.CTL[0].CAM[6].VAL = registro.RUBRO.ToString();

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "LDESCRIPCION";
                det.CTL[0].CAM[7].pk = "0";
                det.CTL[0].CAM[7].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[8] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[8].name = "CUENTA";
                det.CTL[0].CAM[8].pk = "0";
                det.CTL[0].CAM[8].VAL = registro.CUENTA;

                det.CTL[0].CAM[9] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[9].name = "LDESBLOQUEOPARCIAL";
                det.CTL[0].CAM[9].pk = "0";
                det.CTL[0].CAM[9].VAL = "0";

                det.CTL[0].CAM[10] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[10].name = "PRESTAMO";
                det.CTL[0].CAM[10].pk = "0";
                det.CTL[0].CAM[10].VAL = "0";

                det.CTL[0].CAM[11] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[11].name = "LSBLOQUEOFONDOS";
                det.CTL[0].CAM[11].pk = "0";
                det.CTL[0].CAM[11].VAL = "0";

                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return registro;
        }

        public TBTHDETALLEPROCESO ParseNotaDebitoRecuperacion(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            try
            {
                #region GRQ
                
                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;
                
                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();

                #region CTL
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[12];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "IS_S29";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = "1";

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "CUENTADEBITO";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = registro.CUENTA;

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "MONTO";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = DecimalToString(registro.VALORPENDIENTE.Value);

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "LCSUBSISTEMA";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = registro.transaccion.CSUBSISTEMA;

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "LCTRANSACCION";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = registro.transaccion.CTRANSACCION;

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "LVERSION";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = registro.transaccion.VERSIONTRANSACCION;

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "LRUBRO";
                det.CTL[0].CAM[6].pk = "0";
                det.CTL[0].CAM[6].VAL = registro.RUBRO.ToString();

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "LDESCRIPCION";
                det.CTL[0].CAM[7].pk = "0";
                det.CTL[0].CAM[7].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[8] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[8].name = "CUENTA";
                det.CTL[0].CAM[8].pk = "0";
                det.CTL[0].CAM[8].VAL = registro.CUENTA;

                det.CTL[0].CAM[9] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[9].name = "LDESBLOQUEOPARCIAL";
                det.CTL[0].CAM[9].pk = "0";
                det.CTL[0].CAM[9].VAL = "1";

                det.CTL[0].CAM[10] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[10].name = "PRESTAMO";
                det.CTL[0].CAM[10].pk = "0";
                det.CTL[0].CAM[10].VAL = registro.REFERENCIA;

                //List<TCUENTABLOQUEOFONDOS> ltBlq = null;
                //TCUENTABLOQUEOFONDOS blq = null;
                //string error = string.Empty;
                //ltBlq = new TCUENTABLOQUEOFONDOS().ListarBloqueosRecuperacion(registro.CUENTA);
                //if (ltBlq != null && ltBlq.Count > 0)
                //{
                //    blq = ltBlq.First();
                //    det.CTL[0].CAM[10] = new AutFitbankXml.CAM();
                //    det.CTL[0].CAM[10].name = "PRESTAMO";
                //    det.CTL[0].CAM[10].pk = "0";
                //    det.CTL[0].CAM[10].VAL = blq.REFERENCIA;

                //    det.CTL[0].CAM[11] = new AutFitbankXml.CAM();
                //    det.CTL[0].CAM[11].name = "LSBLOQUEOFONDOS";
                //    det.CTL[0].CAM[11].pk = "0";
                //    det.CTL[0].CAM[11].VAL = blq.SBLOQUEOFONDOS.ToString();
                //}
                //else
                //{
                //    det.CTL[0].CAM[10] = new AutFitbankXml.CAM();
                //    det.CTL[0].CAM[10].name = "PRESTAMO";
                //    det.CTL[0].CAM[10].pk = "0";
                //    det.CTL[0].CAM[10].VAL = "0";

                //    det.CTL[0].CAM[11] = new AutFitbankXml.CAM();
                //    det.CTL[0].CAM[11].name = "LSBLOQUEOFONDOS";
                //    det.CTL[0].CAM[11].pk = "0";
                //    det.CTL[0].CAM[11].VAL = "0";
                //}
                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };
                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return registro;
        }

        public TBTHDETALLEPROCESO ParseBloqueo(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[1];

                #region FINANCIERO
                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "FINANCIERO";
                det.TBL[0].blq = "0";
                det.TBL[0].financial = "true";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "FINANCIERO";
                det.TBL[0].npg = "1";
                det.TBL[0].nrg = "1";
                det.TBL[0].ract = "0";
                det.TBL[0].readOnly = "true";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];

                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "0";
                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[13];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[0].name = "CODIGO";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = "9";

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[1].name = "CUENTA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = registro.CUENTA;

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[2].name = "COMPANIA";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = "2";

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[3].name = "SUBCUENTA";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = "0";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[4].name = "MONEDACUENTA";
                det.TBL[0].REG[0].CAM[4].pk = "0";
                det.TBL[0].REG[0].CAM[4].VAL = "USD";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[5].name = "CODIGOCONTABLE";
                det.TBL[0].REG[0].CAM[5].pk = "0";

                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[6].name = "SUCURALDESTINO";
                det.TBL[0].REG[0].CAM[6].pk = "0";

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[7].name = "OFICINADESTINO";
                det.TBL[0].REG[0].CAM[7].pk = "0";

                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[8].name = "MONEDAORIGINAL";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = "USD";

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[9].name = "VALOR";
                det.TBL[0].REG[0].CAM[9].pk = "0";
                det.TBL[0].REG[0].CAM[9].VAL = DecimalToString(registro.VALOR.Value);

                det.TBL[0].REG[0].CAM[10] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[10].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[10].name = "CONCEPTO";
                det.TBL[0].REG[0].CAM[10].pk = "0";
                det.TBL[0].REG[0].CAM[10].VAL = registro.CCONCEPTO;

                det.TBL[0].REG[0].CAM[11] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[11].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[11].name = "DESCRIPCION";
                det.TBL[0].REG[0].CAM[11].pk = "0";
                det.TBL[0].REG[0].CAM[11].VAL = ArmaDescripcion(registro);

                det.TBL[0].REG[0].CAM[12] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[12].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[12].name = "FECHAVENCIMIENTO";
                det.TBL[0].REG[0].CAM[12].pk = "0";

                #endregion FINANCIERO

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[24];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "IDENTIFICACIONR";
                det.CTL[0].CAM[0].pk = "0";

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "Subsistema";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = transaccion.CSUBSISTEMA;

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "Transaccion";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = transaccion.CTRANSACCION;

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "CUENTA";
                det.CTL[0].CAM[3].pk = "0";

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "CCONCEPTO";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = registro.CCONCEPTO;

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "VALOR_DESBLOQUEO";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = DecimalToString(registro.VALOR.Value);

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "OBSERVACIONES_DESBLOQUEO";
                det.CTL[0].CAM[6].pk = "0";
                det.CTL[0].CAM[6].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "REFERENCIA_DESBLOQUEO";
                det.CTL[0].CAM[7].pk = "0";
                det.CTL[0].CAM[7].VAL = registro.REFERENCIA;

                det.CTL[0].CAM[8] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[8].name = "FINANCIERO";
                det.CTL[0].CAM[8].pk = "0";
                det.CTL[0].CAM[8].VAL = "true";

                det.CTL[0].CAM[9] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[9].name = "FECHAVALOR";
                det.CTL[0].CAM[9].pk = "0";

                det.CTL[0].CAM[10] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[10].name = "MODO";
                det.CTL[0].CAM[10].pk = "0";
                det.CTL[0].CAM[10].VAL = "N";

                det.CTL[0].CAM[11] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[11].name = "DOCUMENTO";
                det.CTL[0].CAM[11].pk = "0";
                det.CTL[0].CAM[11].VAL = ArmaNumeroDocumento(registro);

                det.CTL[0].CAM[12] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[12].name = "DOCUMENTOFINAL";
                det.CTL[0].CAM[12].pk = "0";

                det.CTL[0].CAM[13] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[13].name = "CODIGOPERSONA";
                det.CTL[0].CAM[13].pk = "0";

                det.CTL[0].CAM[14] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[14].name = "TIPOPERSONA";
                det.CTL[0].CAM[14].pk = "0";

                det.CTL[0].CAM[15] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[15].name = "TIPOIDENTIFICACION";
                det.CTL[0].CAM[15].pk = "0";

                det.CTL[0].CAM[16] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[16].name = "CODIGODEPOSITO";
                det.CTL[0].CAM[16].pk = "0";

                det.CTL[0].CAM[17] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[17].name = "NUEVASUCURSAL";
                det.CTL[0].CAM[17].pk = "0";

                det.CTL[0].CAM[18] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[18].name = "NUEVAOFICINA";
                det.CTL[0].CAM[18].pk = "0";

                det.CTL[0].CAM[19] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[19].name = "DESCRIPCION";
                det.CTL[0].CAM[19].pk = "0";
                det.CTL[0].CAM[19].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[20] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[20].name = "MOTIVOCHEQUE";
                det.CTL[0].CAM[20].pk = "0";

                det.CTL[0].CAM[21] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[21].name = "NUEVOGRUPOPRODUCTO";
                det.CTL[0].CAM[21].pk = "0";

                det.CTL[0].CAM[22] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[22].name = "CCOMPANIA";
                det.CTL[0].CAM[22].pk = "0";
                det.CTL[0].CAM[22].VAL = "2";

                det.CTL[0].CAM[23] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[23].name = "REVISARFIRMA";
                det.CTL[0].CAM[23].pk = "0";
                det.CTL[0].CAM[23].VAL = "1";

                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHDETALLEPROCESO ParseDesbloqueo(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[1];
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                #region FINANCIERO

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "FINANCIERO";
                det.TBL[0].blq = "0";
                det.TBL[0].financial = "true";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "FINANCIERO";
                det.TBL[0].npg = "1";
                det.TBL[0].nrg = "1";
                det.TBL[0].ract = "0";
                det.TBL[0].readOnly = "true";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];
                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "0";

                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[13];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[0].name = "CODIGO";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = "9";

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[1].name = "CUENTA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = registro.CUENTA;

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[2].name = "COMPANIA";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = "2";

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[3].name = "SUBCUENTA";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = "0";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[4].name = "MONEDACUENTA";
                det.TBL[0].REG[0].CAM[4].pk = "0";
                det.TBL[0].REG[0].CAM[4].VAL = "USD";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[5].name = "CODIGOCONTABLE";
                det.TBL[0].REG[0].CAM[5].pk = "0";

                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[6].name = "SUCURALDESTINO";
                det.TBL[0].REG[0].CAM[6].pk = "0";

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[7].name = "OFICINADESTINO";
                det.TBL[0].REG[0].CAM[7].pk = "0";

                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[8].name = "MONEDAORIGINAL";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = "USD";

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[9].name = "VALOR";
                det.TBL[0].REG[0].CAM[9].pk = "0";
                det.TBL[0].REG[0].CAM[9].VAL = DecimalToString(registro.VALOR.Value);

                det.TBL[0].REG[0].CAM[10] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[10].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[10].name = "CONCEPTO";
                det.TBL[0].REG[0].CAM[10].pk = "0";
                det.TBL[0].REG[0].CAM[10].VAL = registro.CCONCEPTO;

                det.TBL[0].REG[0].CAM[11] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[11].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[11].name = "DESCRIPCION";
                det.TBL[0].REG[0].CAM[11].pk = "0";

                det.TBL[0].REG[0].CAM[12] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[12].alias = "FINANCIERO";
                det.TBL[0].REG[0].CAM[12].name = "FECHAVENCIMIENTO";
                det.TBL[0].REG[0].CAM[12].pk = "0";

                #endregion FINANCIERO

                #region CTL

                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[10];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "Subsistema";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = transaccion.CSUBSISTEMA;

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "Transaccion";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = transaccion.CTRANSACCION;

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "CUENTA";
                det.CTL[0].CAM[2].pk = "0";

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "VALORDESBLOQUEAR";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = DecimalToString(registro.VALOR.Value);

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "MOTIVOLEVANTAMIENTO";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "FINANCIERO";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = "true";

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "FECHAVALOR";
                det.CTL[0].CAM[6].pk = "0";

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "MODO";
                det.CTL[0].CAM[7].pk = "0";
                det.CTL[0].CAM[7].VAL = "N";

                det.CTL[0].CAM[8] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[8].name = "DESCRIPCION";
                det.CTL[0].CAM[8].pk = "0";
                det.CTL[0].CAM[8].VAL = ArmaDescripcion(registro);

                det.CTL[0].CAM[9] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[9].name = "SECUENCIA";
                det.CTL[0].CAM[9].pk = "0";
                det.CTL[0].CAM[9].VAL = registro.SECUENCIABLQ.Value.ToString();
                #endregion CTL
                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHDETALLEPROCESO ParseCambioEstadoCuenta(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            string[] parametros;

            try
            {
                parametros = registro.PARAMETROS.Split('|');

                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[1];
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                #region TCUENTA

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "tcuenta1";
                det.TBL[0].blq = "0";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "TCUENTA";
                det.TBL[0].npg = "1";
                det.TBL[0].nrg = "1";
                det.TBL[0].ract = "0";
                det.TBL[0].CRI = new AutFitbankXml.FITBANKDETTBLCRI[2];

                det.TBL[0].CRI[0] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[0].alias = "tcuenta1";
                det.TBL[0].CRI[0].cond = "LIKE";
                det.TBL[0].CRI[0].name = "CCUENTA";
                det.TBL[0].CRI[0].val = registro.CUENTA;

                det.TBL[0].CRI[1] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[1].alias = "tcuenta1";
                det.TBL[0].CRI[1].cond = "LIKE";
                det.TBL[0].CRI[1].name = "CCUENTA";
                det.TBL[0].CRI[1].ord = "1";
                det.TBL[0].CRI[1].tipo = "ORDER";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];

                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "0";

                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[4];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "tcuenta1";
                det.TBL[0].REG[0].CAM[0].name = "CESTATUSCUENTA";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = parametros[2];
                det.TBL[0].REG[0].CAM[0].OLDVAL = parametros[1];

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "tcuenta1";
                det.TBL[0].REG[0].CAM[1].name = "CSUBSISTEMA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = parametros[0];
                det.TBL[0].REG[0].CAM[1].OLDVAL = parametros[0];

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "tcuenta1";
                det.TBL[0].REG[0].CAM[2].name = "CCUENTA";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = registro.CUENTA;
                det.TBL[0].REG[0].CAM[2].OLDVAL = registro.CUENTA;

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "tcuenta1";
                det.TBL[0].REG[0].CAM[3].name = "VERSIONCONTROL";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = parametros[3];
                det.TBL[0].REG[0].CAM[3].OLDVAL = parametros[3];

                #endregion TCUENTA

                #region CTL

                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[5];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "__JOIN_QUIRK__";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = "true";

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "CCONDICION";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = parametros[2];

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "COMPANIA";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = "2";

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "CUENTA";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = registro.CUENTA;

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "ESTATUSCUENTA";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = parametros[1];

                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHDETALLEPROCESO ParseAutorizacionComprobanteContable(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();

                det.TBL = new AutFitbankXml.FITBANKDETTBL[2];

                #region TCOMPROBANTESCONTABLES

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "tcomprobantescontables1";
                det.TBL[0].blq = "0";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "TCOMPROBANTESCONTABLES";
                det.TBL[0].npg = "1";
                det.TBL[0].nrg = "1";
                det.TBL[0].ract = "0";

                det.TBL[0].CRI = new AutFitbankXml.FITBANKDETTBLCRI[2];

                det.TBL[0].CRI[0] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[0].alias = "tcomprobantescontables1";
                det.TBL[0].CRI[0].cond = "=";
                det.TBL[0].CRI[0].name = "NUMEROCOMPROBANTE";
                det.TBL[0].CRI[0].val = registro.PARAMETROS;

                det.TBL[0].CRI[1] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[1].alias = "tcomprobantescontables1";
                det.TBL[0].CRI[1].cond = "LIKE";
                det.TBL[0].CRI[1].name = "NUMEROCOMPROBANTE";
                det.TBL[0].CRI[1].ord = "1";
                det.TBL[0].CRI[1].tipo = "ORDER";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];

                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "0";

                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[10];

                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[0].name = "ESTATUSCOMPROBANTE";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = "A";
                det.TBL[0].REG[0].CAM[0].OLDVAL = "I";

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[1].name = "CCODIGOPLANTILLA";
                det.TBL[0].REG[0].CAM[1].pk = "0";

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[2].name = "FCONTABLE";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = registro.REFERENCIA;
                det.TBL[0].REG[0].CAM[2].OLDVAL = registro.REFERENCIA;


                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[3].name = "NUMEROCOMPROBANTEORIGINAL";
                det.TBL[0].REG[0].CAM[3].pk = "0";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[4].name = "CSUCURSAL";
                det.TBL[0].REG[0].CAM[4].pk = "0";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[5].name = "COFICINA";
                det.TBL[0].REG[0].CAM[5].pk = "0";

                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[6].name = "CUADRADO";
                det.TBL[0].REG[0].CAM[6].pk = "0";

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[7].name = "CUSUARIO_AUTORIZACION";
                det.TBL[0].REG[0].CAM[7].pk = "0";
                det.TBL[0].REG[0].CAM[7].VAL = registro.CUSUARIO;


                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[8].name = "NUMEROCOMPROBANTE";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = registro.PARAMETROS;
                det.TBL[0].REG[0].CAM[8].OLDVAL = registro.PARAMETROS;

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "tcomprobantescontables1";
                det.TBL[0].REG[0].CAM[9].name = "VERSIONCONTROL";
                det.TBL[0].REG[0].CAM[9].pk = "0";

                #endregion TCOMPROBANTESCONTABLES

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[6];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].alias = "";
                det.CTL[0].CAM[0].name = "F2Numero";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = registro.PARAMETROS;

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].alias = "";
                det.CTL[0].CAM[1].name = "ESTADO";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = "I";

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].alias = "";
                det.CTL[0].CAM[2].name = "CMONEDA";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = "USD";

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].alias = "";
                det.CTL[0].CAM[3].name = "ESTATUS";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = "A";

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].alias = "";
                det.CTL[0].CAM[4].name = "FVALOR";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = registro.REFERENCIA;

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].alias = "";
                det.CTL[0].CAM[5].name = "FECHACONTABLE";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = registro.REFERENCIA;

                #endregion CTL

                #endregion DET

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHDETALLEPROCESO ParseReverso(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQReverso(registro);
                registro.NUMEROMENSAJE = grq.MSG;

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.TBL = new AutFitbankXml.FITBANKDETTBL[2];
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                #region TUCITRANSACCIONESDIA

                det.TBL[0] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[0].alias = "tucit0";
                det.TBL[0].blq = "1";
                det.TBL[0].mpg = "0";
                det.TBL[0].name = "TUCITRANSACCIONESDIA";
                det.TBL[0].npg = "1";
                det.TBL[0].nrg = "10";
                det.TBL[0].ract = "0";
                det.TBL[0].distinct = "true";
                det.TBL[0].CRI = new AutFitbankXml.FITBANKDETTBLCRI[9];
                det.TBL[0].CRI[0] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[0].alias = "tucit0";
                det.TBL[0].CRI[0].cond = "=";
                det.TBL[0].CRI[0].name = "CUSUARIO";
                det.TBL[0].CRI[0].val = registro.CUSUARIO_ORIGINAL;

                det.TBL[0].CRI[1] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[1].alias = "tucit0";
                det.TBL[0].CRI[1].cond = "=";
                det.TBL[0].CRI[1].name = "FCONTABLE";
                det.TBL[0].CRI[1].val = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");

                det.TBL[0].CRI[2] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[2].alias = "tucit0";
                det.TBL[0].CRI[2].cond = "NOT IN ('1')";
                det.TBL[0].CRI[2].name = "REVERSADA_DESTINO";
                det.TBL[0].CRI[2].val = "";

                det.TBL[0].CRI[3] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[3].alias = "tucit0";
                det.TBL[0].CRI[3].cond = "NOT IN ('1')";
                det.TBL[0].CRI[3].name = "REVERSADA_COPIA";
                det.TBL[0].CRI[3].val = "";

                det.TBL[0].CRI[4] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[4].alias = "tucit0";
                det.TBL[0].CRI[4].cond = "IN ('1')";
                det.TBL[0].CRI[4].name = "AUTORIZAREVERSO";
                det.TBL[0].CRI[4].val = "";

                det.TBL[0].CRI[5] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[5].alias = "tucit0";
                det.TBL[0].CRI[5].cond = "=";
                det.TBL[0].CRI[5].name = "REVERSO";
                det.TBL[0].CRI[5].val = "0";
                det.TBL[0].CRI[5].tipo = "JOIN";

                det.TBL[0].CRI[6] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[6].alias = "tucit0";
                det.TBL[0].CRI[6].cond = "LIKE";
                det.TBL[0].CRI[6].name = "CSUBSISTEMA";
                det.TBL[0].CRI[6].val = "";

                det.TBL[0].CRI[7] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[7].alias = "tucit0";
                det.TBL[0].CRI[7].cond = "LIKE";
                det.TBL[0].CRI[7].name = "CTRANSACCION";
                det.TBL[0].CRI[7].val = "";

                det.TBL[0].CRI[8] = new AutFitbankXml.FITBANKDETTBLCRI();
                det.TBL[0].CRI[8].alias = "tucit0";
                det.TBL[0].CRI[8].cond = "LIKE";
                det.TBL[0].CRI[8].name = "CCUENTA";
                det.TBL[0].CRI[8].val = "";

                det.TBL[0].REG = new AutFitbankXml.FITBANKDETTBLREG[1];
                det.TBL[0].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[0].REG[0].numero = "0";
                det.TBL[0].REG[0].CAM = new AutFitbankXml.CAM[16];

                //OJO IMPORTANTE RETORNAR SECUENCIAL COMO SE LO MANDO EN LA TRANSACCION NORMAL AL UCI
                det.TBL[0].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[0].alias = "tucit0";
                det.TBL[0].REG[0].CAM[0].name = "NUMEROMENSAJE";
                det.TBL[0].REG[0].CAM[0].pk = "0";
                det.TBL[0].REG[0].CAM[0].VAL = registro.NUMEROMENSAJE_ORIGINAL;
                det.TBL[0].REG[0].CAM[0].OLDVAL = registro.NUMEROMENSAJE_ORIGINAL;

                det.TBL[0].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[1].alias = "tucit0";
                det.TBL[0].REG[0].CAM[1].name = "CSUBSISTEMA";
                det.TBL[0].REG[0].CAM[1].pk = "0";
                det.TBL[0].REG[0].CAM[1].VAL = registro.CSUBSISTEMA_ORIGINAL;
                det.TBL[0].REG[0].CAM[1].OLDVAL = registro.CSUBSISTEMA_ORIGINAL;

                det.TBL[0].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[2].alias = "tucit0";
                det.TBL[0].REG[0].CAM[2].name = "CTRANSACCION";
                det.TBL[0].REG[0].CAM[2].pk = "0";
                det.TBL[0].REG[0].CAM[2].VAL = registro.CTRANSACCION_ORIGINAL;
                det.TBL[0].REG[0].CAM[2].OLDVAL = registro.CTRANSACCION_ORIGINAL;

                det.TBL[0].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[3].alias = "tucit0";
                det.TBL[0].REG[0].CAM[3].name = "VERSIONTRANSACCION";
                det.TBL[0].REG[0].CAM[3].pk = "0";
                det.TBL[0].REG[0].CAM[3].VAL = "01";
                det.TBL[0].REG[0].CAM[3].OLDVAL = "01";

                det.TBL[0].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[4].alias = "tucit0";
                det.TBL[0].REG[0].CAM[4].name = "CCUENTA";
                det.TBL[0].REG[0].CAM[4].pk = "0";

                det.TBL[0].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[5].alias = "tucit0";
                det.TBL[0].REG[0].CAM[5].name = "NUMERODOCUMENTO";
                det.TBL[0].REG[0].CAM[5].pk = "0";
                det.TBL[0].REG[0].CAM[5].VAL = "1";
                det.TBL[0].REG[0].CAM[5].OLDVAL = "1";

                //OJO AQUI SE MANDA LA FECHA DE LA TRANSACCION ORIGINAL VER LA POSIBILIDAD DE OMITIR
                det.TBL[0].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[6].alias = "tucit0";
                det.TBL[0].REG[0].CAM[6].name = "FREAL";
                det.TBL[0].REG[0].CAM[6].pk = "0";
                det.TBL[0].REG[0].CAM[6].VAL = registro.FACTUALIZACION.Value.ToString("yyyy-MM-dd HH:mm:ss");
                det.TBL[0].REG[0].CAM[6].OLDVAL = registro.FACTUALIZACION.Value.ToString("yyyy-MM-dd HH:mm:ss");

                det.TBL[0].REG[0].CAM[7] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[7].alias = "tucit0";
                det.TBL[0].REG[0].CAM[7].name = "VALOR";
                det.TBL[0].REG[0].CAM[7].pk = "0";
                det.TBL[0].REG[0].CAM[7].VAL = DecimalToString(registro.VALOR.Value);
                det.TBL[0].REG[0].CAM[7].OLDVAL = DecimalToString(registro.VALOR.Value);

                det.TBL[0].REG[0].CAM[8] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[8].alias = "tucit0";
                det.TBL[0].REG[0].CAM[8].name = "CMONEDA";
                det.TBL[0].REG[0].CAM[8].pk = "0";
                det.TBL[0].REG[0].CAM[8].VAL = "USD";
                det.TBL[0].REG[0].CAM[8].OLDVAL = "USD";

                det.TBL[0].REG[0].CAM[9] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[9].alias = "tucit0";
                det.TBL[0].REG[0].CAM[9].name = "DETALLEREVERSO";
                det.TBL[0].REG[0].CAM[9].pk = "0";
                det.TBL[0].REG[0].CAM[9].VAL = "REVERSO SOLICITADO BATCH";
                det.TBL[0].REG[0].CAM[9].OLDVAL = "REVERSO SOLICITADO BATCH";

                det.TBL[0].REG[0].CAM[10] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[10].alias = "tucit0";
                det.TBL[0].REG[0].CAM[10].name = "REVERSO";
                det.TBL[0].REG[0].CAM[10].pk = "0";
                det.TBL[0].REG[0].CAM[10].VAL = "1";
                det.TBL[0].REG[0].CAM[10].OLDVAL = "0";

                det.TBL[0].REG[0].CAM[11] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[11].alias = "tucit0";
                det.TBL[0].REG[0].CAM[11].name = "CUSUARIO";
                det.TBL[0].REG[0].CAM[11].pk = "0";
                det.TBL[0].REG[0].CAM[11].VAL = registro.CUSUARIO_ORIGINAL;
                det.TBL[0].REG[0].CAM[11].OLDVAL = registro.CUSUARIO_ORIGINAL;

                det.TBL[0].REG[0].CAM[12] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[12].alias = "tucit0";
                det.TBL[0].REG[0].CAM[12].name = "FCONTABLE";
                det.TBL[0].REG[0].CAM[12].pk = "0";
                det.TBL[0].REG[0].CAM[12].VAL = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");
                det.TBL[0].REG[0].CAM[12].OLDVAL = BthProcesos.glbFitbankFechaContable.ToString("yyyy-MM-dd");

                det.TBL[0].REG[0].CAM[13] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[13].alias = "tucit0";
                det.TBL[0].REG[0].CAM[13].name = "REVERSADA_DESTINO";
                det.TBL[0].REG[0].CAM[13].pk = "0";
                det.TBL[0].REG[0].CAM[13].VAL = "0";
                det.TBL[0].REG[0].CAM[13].OLDVAL = "0";

                det.TBL[0].REG[0].CAM[14] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[14].alias = "tucit0";
                det.TBL[0].REG[0].CAM[14].name = "REVERSADA_COPIA";
                det.TBL[0].REG[0].CAM[14].pk = "0";
                det.TBL[0].REG[0].CAM[14].VAL = "0";
                det.TBL[0].REG[0].CAM[14].OLDVAL = "0";

                det.TBL[0].REG[0].CAM[15] = new AutFitbankXml.CAM();
                det.TBL[0].REG[0].CAM[15].alias = "tucit0";
                det.TBL[0].REG[0].CAM[15].name = "AUTORIZAREVERSO";
                det.TBL[0].REG[0].CAM[15].pk = "0";
                det.TBL[0].REG[0].CAM[15].VAL = "1";
                det.TBL[0].REG[0].CAM[15].OLDVAL = "1";

                #endregion TUCITRANSACCIONESDIA

                #region VCAJATRANSACCIONESDIA

                det.TBL[1] = new AutFitbankXml.FITBANKDETTBL();
                det.TBL[1].ract = "0";
                det.TBL[1].npg = "1";
                det.TBL[1].name = "VCAJATRANSACCIONESDIA";
                det.TBL[1].mpg = "0";
                det.TBL[1].distinct = "true";
                det.TBL[1].blq = "1";
                det.TBL[1].alias = "vcajatransaccionesdia1";
                det.TBL[1].readOnly = "true";
                det.TBL[1].REG = new AutFitbankXml.FITBANKDETTBLREG[1];
                det.TBL[1].REG[0] = new AutFitbankXml.FITBANKDETTBLREG();
                det.TBL[1].REG[0].numero = "0";
                det.TBL[1].REG[0].CAM = new AutFitbankXml.CAM[7];

                det.TBL[1].REG[0].CAM[0] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[0].name = "REVERSO";
                det.TBL[1].REG[0].CAM[0].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[0].pk = "0";
                det.TBL[1].REG[0].CAM[0].VAL = "1";

                det.TBL[1].REG[0].CAM[1] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[1].name = "CSUBSISTEMA_TRANSACCION";
                det.TBL[1].REG[0].CAM[1].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[1].pk = "0";
                det.TBL[1].REG[0].CAM[1].VAL = registro.CSUBSISTEMA_ORIGINAL;

                det.TBL[1].REG[0].CAM[2] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[2].name = "CTRANSACCION";
                det.TBL[1].REG[0].CAM[2].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[2].pk = "0";
                det.TBL[1].REG[0].CAM[2].VAL = registro.CTRANSACCION_ORIGINAL;

                det.TBL[1].REG[0].CAM[3] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[3].name = "VERSIONTRANSACCION";
                det.TBL[1].REG[0].CAM[3].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[3].pk = "0";
                det.TBL[1].REG[0].CAM[3].VAL = "01";

                det.TBL[1].REG[0].CAM[4] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[4].name = "CSUBSISTEMA_ORIGEN";
                det.TBL[1].REG[0].CAM[4].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[4].pk = "0";
                det.TBL[1].REG[0].CAM[4].VAL = registro.CSUBSISTEMA_ORIGINAL;

                det.TBL[1].REG[0].CAM[5] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[5].name = "CTRANSACCION_ORIGEN";
                det.TBL[1].REG[0].CAM[5].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[5].pk = "0";
                det.TBL[1].REG[0].CAM[5].VAL = registro.CTRANSACCION_ORIGINAL;

                det.TBL[1].REG[0].CAM[6] = new AutFitbankXml.CAM();
                det.TBL[1].REG[0].CAM[6].name = "VERSIONTRANSACCION_ORIGEN";
                det.TBL[1].REG[0].CAM[6].alias = "vcajatransaccionesdia1";
                det.TBL[1].REG[0].CAM[6].pk = "0";
                det.TBL[1].REG[0].CAM[6].VAL = "01";

                #endregion VCAJATRANSACCIONESDIA

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[9];

                //OJO IMPORTANTE RETORNAL SECUENCIAL COMO SE LO MANDO EN LA TRANSACCION NORMAL AL UCI
                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "MENSAJE_RVS";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = registro.NUMEROMENSAJE_ORIGINAL;

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "CCUENTA";
                det.CTL[0].CAM[1].pk = "0";

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "CCOMPANIA";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = "2";

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "FREAL";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "_REAL_CHANNEL";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = "W3";

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "_DATE_TIME";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = "2999-12-31 00:00:00.0";

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "_BPM_PENDING";
                det.CTL[0].CAM[6].pk = "0";
                det.CTL[0].CAM[6].VAL = "0";

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "ACCOUNTINGVALIDATOR";
                det.CTL[0].CAM[7].pk = "0";

                det.CTL[0].CAM[8] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[8].name = "__BPM_CALL_";
                det.CTL[0].CAM[8].pk = "0";

                #endregion CTL

                #endregion DET;

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        public TBTHPROCESO ParseCargaLotes(TBTHPROCESO obj)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            TBTHTIPOTRANSACCION transaccion = new TBTHTIPOTRANSACCION();
            var registro = (TBTHPROCESO)obj;

            try
            {
                #region GRQ

                AutFitbankXml.FITBANKGRQ grq = new AutFitbankXml.FITBANKGRQ();
                grq = ArmaGRQ(registro);

                #endregion GRQ

                #region DET

                AutFitbankXml.FITBANKDET det = new AutFitbankXml.FITBANKDET();
                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];

                #region CTL

                det.CTL = new AutFitbankXml.FITBANKDETTCTL[1];
                det.CTL[0] = new AutFitbankXml.FITBANKDETTCTL();
                det.CTL[0].CAM = new AutFitbankXml.CAM[8];

                det.CTL[0].CAM[0] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[0].name = "__JOIN_QUIRK__";
                det.CTL[0].CAM[0].pk = "0";
                det.CTL[0].CAM[0].VAL = "true";

                det.CTL[0].CAM[1] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[1].name = "FILE";
                det.CTL[0].CAM[1].pk = "0";
                det.CTL[0].CAM[1].VAL = registro.ARCHIVOLOTES;

                det.CTL[0].CAM[2] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[2].name = "SUB";
                det.CTL[0].CAM[2].pk = "0";
                det.CTL[0].CAM[2].VAL = "04";

                det.CTL[0].CAM[3] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[3].name = "TRAN";
                det.CTL[0].CAM[3].pk = "0";
                det.CTL[0].CAM[3].VAL = "7226";

                det.CTL[0].CAM[4] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[4].name = "VER";
                det.CTL[0].CAM[4].pk = "0";
                det.CTL[0].CAM[4].VAL = "01";

                det.CTL[0].CAM[5] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[5].name = "EST";
                det.CTL[0].CAM[5].pk = "0";
                det.CTL[0].CAM[5].VAL = "CREDIDEBI";

                det.CTL[0].CAM[6] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[6].name = "TIPOLOTE";
                det.CTL[0].CAM[6].pk = "0";
                det.CTL[0].CAM[6].VAL = "CRED";

                det.CTL[0].CAM[7] = new AutFitbankXml.CAM();
                det.CTL[0].CAM[7].name = "AUTOMATICO";
                det.CTL[0].CAM[7].pk = "0";
                det.CTL[0].CAM[7].VAL = "1";

                #endregion CTL

                #endregion DET;

                fit.Items = new object[] { grq, det };

                registro.detailEntrada = FitBankToString(fit);
                registro.CERROR = "000";
                registro.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                registro.detailEntrada = string.Empty;
                registro.CERROR = "999";
                registro.DERROR = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        #endregion metodos parce

        public string FitBankToString(AutFitbankXml.FITBANK obj)
        {
            string data = null;
            try
            {
                XmlSerializer op1 = new XmlSerializer(typeof(AutFitbankXml.FITBANK));
                System.IO.StringWriter ss1 = new System.IO.StringWriter();
                XmlSerializerNamespaces ns_app = new XmlSerializerNamespaces();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                op1.Serialize(ss1, obj, ns);
                var xml1 = ss1.ToString().Replace("xmlns=\"http://tempuri.org/\"", "");
                data = xml1.ToString();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return data;
        }

        public AutFitbankXml.FITBANK StringToFitBank(string xml)
        {
            AutFitbankXml.FITBANK fit = new AutFitbankXml.FITBANK();
            try
            {
                XmlSerializer s_dsl = new XmlSerializer(typeof(AutFitbankXml.FITBANK));
                fit = (AutFitbankXml.FITBANK)s_dsl.Deserialize(new System.IO.StringReader(xml));
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                fit = null;
            }
            return fit;
        }
    }
}

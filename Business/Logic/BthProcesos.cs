using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Crypto;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace Business
{
    public class BthProcesos
    {
        #region variables

        private static List<TBTHTIPOARCHIVO> glbLtTipoArchivo = null;
        public static List<TBTHTIPOTRANSACCION> glbLtTransaccion = null;

        private static string glbFtpParametros = string.Empty;
        private static string glbFtpLotesParametros = string.Empty;

        private static string glbPathLocal = string.Empty;
        private static string glbPathFtp = string.Empty;
        private static string glbPathFtpLotes = string.Empty;

        public static Int32 glbProcesosBdd = 0;

        public static bool glbLecturaActiva = false;
        public static Int32 glbLecturaTiempo = 0;

        public static bool glbTabuladoVistaActiva = false;
        public static Int32 glbTabuladoVistaTiempo = 0;

        public static bool glbTabuladoCreditoActiva = false;
        public static Int32 glbTabuladoCreditoTiempo = 0;

        public static bool glbTabuladoBloqueosActiva = false;
        public static Int32 glbTabuladoBloqueosTiempo = 0;

        public static bool glbTabuladoSpi3Activa = false;
        public static Int32 glbTabuladoSpi3Tiempo = 0;

        public static bool glbTabuladoLotesActiva = false;
        public static Int32 glbTabuladoLotesTiempo = 0;

        public static bool glbEjecutaNormalActiva = false;
        public static Int32 glbEjecutaNormalTiempo = 0;
        public static Int32 glbEjecutaNormalProcesos = 0;

        public static bool glbEjecutaBloqueosActiva = false;
        public static Int32 glbEjecutaBloqueosProcesos = 0;
        public static Int32 glbEjecutaBloqueosTiempo = 0;

        public static bool glbEjecutaRecuperacionActiva = false;
        public static Int32 glbEjecutaRecuperacionProcesos = 0;
        public static Int32 glbEjecutaRecuperacionTiempo = 0;

        public static bool glbEfectivizarChequesActiva = false;
        public static Int32 glbEfectivizarChequesTiempo = 0;
        public static Int32 glbEfectivizarChequesProcesos = 0;

        public static bool glbActivaBlqueosActiva = false;
        public static Int32 glbActivaBloqueosProcesos = 0;
        public static Int32 glbActivaBloqueosTiempo = 0;

        public static bool glbVerificaActiva = false;
        public static Int32 glbVerificaTiempo = 0;

        public static bool glbFinalizaActiva = false;
        public static Int32 glbFinalizaTiempo = 0;

        public static decimal glbComisionSpi = 0;
        public static string glbCuentaBce = string.Empty;

        public static string glbEmails = string.Empty;

        public static Int32 glbTimeoutWS = 0;
        public static Int32 glbTimeoutTrx = 0;

        #region fitbank

        public static bool glbFitbankBalanceo = false;
        public static string glbFitbankIp = string.Empty;
        public static string glbFitbankUrlS29 = string.Empty;
        public static string glbFitbankUrlNormal = string.Empty;
        public static string glbFitbankIpNodo1 = string.Empty;
        public static string glbFitbankIpNodo2 = string.Empty;
        public static string glbFitbankIpLotes = string.Empty;
        public static string glbFitbankIdioma = string.Empty;
        public static string glbFitbankUsuario = string.Empty;
        public static string glbFitbankTerminal = string.Empty;
        public static string glbFitbankRol = string.Empty;
        public static string glbFitbankArea = string.Empty;
        public static string glbFitbankSucursal = string.Empty;
        public static string glbFitbankOficina = string.Empty;
        public static string glbFitbankCanal = string.Empty;
        public static string glbFitbankSession = string.Empty;
        public static string glbFitbankTemNmgNor = string.Empty;
        public static string glbFitbankTemNmgRev = string.Empty;
        public static string glbFitbankQueryFContable = string.Empty;
        public static string glbFitbankQueryProceso = string.Empty;
        public static DateTime glbFitbankFechaContable = DateTime.Now;
        public static Int32 glbFitbankTimeOut = 0;

        #endregion fitbank

        #endregion variables

        #region carga parametros

        public static bool CargarParametros()
        {
            bool flag = true;

            try
            {
                glbProcesosBdd = Convert.ToInt32(ConfigurationManager.AppSettings["procesosBdd"].ToString());

                glbTimeoutTrx = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutTrx"].ToString());
                glbTimeoutWS = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutWS"].ToString());

                glbFtpParametros = ConfigurationManager.AppSettings["ftp"].ToString();
                glbFtpLotesParametros = ConfigurationManager.AppSettings["ftpLotes"].ToString();

                glbPathLocal = ConfigurationManager.AppSettings["pathLocal"].ToString();
                glbPathFtp = ConfigurationManager.AppSettings["pathFtp"].ToString();
                glbPathFtpLotes = ConfigurationManager.AppSettings["pathFtpLotes"].ToString();

                glbLecturaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["lecturaActiva"].ToString());
                glbLecturaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["lecturaTiempo"].ToString());

                glbTabuladoVistaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["tabuladoVistaActiva"].ToString());
                glbTabuladoVistaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tabuladoVistaTiempo"].ToString());

                glbTabuladoCreditoActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["tabuladoCreditoActiva"]);
                glbTabuladoCreditoTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tabuladoCreditoTiempo"]);

                glbTabuladoBloqueosActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["tabuladoBloqueosActiva"]);
                glbTabuladoBloqueosTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tabuladoBloqueosTiempo"]);

                glbTabuladoSpi3Activa = Convert.ToBoolean(ConfigurationManager.AppSettings["tabuladoSpi3Activa"]);
                glbTabuladoSpi3Tiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tabuladoSpi3Tiempo"]);

                glbTabuladoLotesActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["tabuladoLotesActiva"]);
                glbTabuladoLotesTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["tabuladoLotesTiempo"]);

                glbEjecutaNormalActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["ejecutaNormalActiva"].ToString());
                glbEjecutaNormalTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaNormalTiempo"].ToString());
                glbEjecutaNormalProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaNormalProcesos"].ToString());

                glbEjecutaBloqueosActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["ejecutaBloqueosActiva"]);
                glbEjecutaBloqueosTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaBloqueosTiempo"]);
                glbEjecutaBloqueosProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaBloqueosProcesos"]);

                glbEjecutaRecuperacionActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["ejecutaRecuperacionActiva"]);
                glbEjecutaRecuperacionTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaRecuperacionTiempo"]);
                glbEjecutaRecuperacionProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["ejecutaRecuperacionProcesos"]);

                glbEfectivizarChequesActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["efectivizaActiva"].ToString());
                glbEfectivizarChequesTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["efectivizaTiempo"].ToString());
                glbEfectivizarChequesProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["efectivizaProcesos"].ToString());

                glbActivaBlqueosActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["activaBloqueosActiva"]);
                glbActivaBloqueosTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["activaBloqueosTiempo"]);
                glbActivaBloqueosProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["activaBloqueosProcesos"]);

                glbVerificaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["verificaActiva"]);
                glbVerificaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["verificaTiempo"]);

                glbFinalizaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["finalizaActiva"].ToString());
                glbFinalizaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["finalizaTiempo"].ToString());

                glbFitbankTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["fitbankTimeOut"]);
                glbFitbankBalanceo = Convert.ToBoolean(ConfigurationManager.AppSettings["fitbankBalanceo"]);
                glbFitbankUrlS29 = ConfigurationManager.AppSettings["fitbankUrlS29"].ToString();
                glbFitbankUrlNormal = ConfigurationManager.AppSettings["fitbankUrlNormal"].ToString();
                glbFitbankIpNodo1 = ConfigurationManager.AppSettings["fitbankIpNodo1"].ToString();
                glbFitbankIpNodo2 = ConfigurationManager.AppSettings["fitbankIpNodo2"].ToString();
                glbFitbankIpLotes = ConfigurationManager.AppSettings["fitbankIpLotes"].ToString();
                glbFitbankIp = ConfigurationManager.AppSettings["fitbankIp"].ToString();
                glbFitbankArea = ConfigurationManager.AppSettings["fitbankArea"].ToString();
                glbFitbankCanal = ConfigurationManager.AppSettings["fitbankCanal"].ToString();
                glbFitbankIdioma = ConfigurationManager.AppSettings["fitbankIdioma"].ToString();
                glbFitbankSucursal = ConfigurationManager.AppSettings["fitbankSucursal"].ToString();
                glbFitbankOficina = ConfigurationManager.AppSettings["fitbankOficina"].ToString();
                glbFitbankRol = ConfigurationManager.AppSettings["fitbankRol"].ToString();
                glbFitbankTerminal = ConfigurationManager.AppSettings["fitbankTerminal"].ToString();
                glbFitbankTemNmgNor = ConfigurationManager.AppSettings["fitbankTemNmgNor"].ToString();
                glbFitbankTemNmgRev = ConfigurationManager.AppSettings["fitbankTemNmgRev"].ToString();
                glbFitbankUsuario = ConfigurationManager.AppSettings["fitbankUsuario"].ToString();
                glbFitbankSession = Util.GetSecuencial(10);

                glbCuentaBce = ConfigurationManager.AppSettings["cuentaBce"];

                glbEmails = ConfigurationManager.AppSettings["emails"].ToString();

                decimal comision = new BddAuxiliar().GetComisionSPI();
                decimal iva = new BddAuxiliar().GetIva();
                glbComisionSpi = Math.Round(comision + (comision * iva / 100), 2);

                glbLtTransaccion = new TBTHTIPOTRANSACCION().Listar();
                if (glbLtTransaccion == null || glbLtTransaccion.Count <= 0)
                {
                    flag = false;
                }

                glbLtTipoArchivo = new TBTHTIPOARCHIVO().Listar();
                if (glbLtTipoArchivo == null || glbLtTipoArchivo.Count <= 0)
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                Util.ImprimePantalla(ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        #endregion carga parametros

        #region lectura archivos

        public void ProcesoLecturaArchivos()
        {
            List<TBTHPROCESO> ltProcesos = null;
            TBTHTIPOARCHIVO tipoArchivo = null;
            string error = string.Empty;
            string pathFtp = string.Empty;
            string pathLocal = string.Empty;
            string archivo = string.Empty;
            bool flag = true;

            try
            {
                ltProcesos = new TBTHPROCESO().ListarPendientesLectura();
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    foreach (TBTHPROCESO obj in ltProcesos)
                    {
                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, obj.CPROCESO + " INICIA LECTURA");

                        pathFtp = string.Format(glbPathFtp, obj.FPROCESO.Value.ToString("yyyyMMdd"));
                        pathLocal = string.Format(glbPathLocal, obj.FPROCESO.Value.ToString("yyyyMMdd"));
                        archivo = obj.ARCHIVOORIGEN;
                        flag = true;

                        #region crea direcctorios

                        if (!Directory.Exists(pathLocal))
                        {
                            Directory.CreateDirectory(pathLocal);
                        }

                        #endregion crea direcctorios

                        #region extrae archivo

                        if (flag)
                        {
                            flag = Util.ExtraerFtp(glbFtpParametros, pathFtp + archivo, pathLocal + archivo, out error) && string.IsNullOrEmpty(error);
                        }

                        #endregion extrae archivo

                        #region lectura

                        if (flag)
                        {
                            tipoArchivo = glbLtTipoArchivo.Where(x => x.CTIPOARCHIVO == obj.CTIPOARCHIVO).First();
                            if (tipoArchivo != null)
                            {
                                try
                                {
                                    string[] comando = tipoArchivo.COMANDO.Split('.');
                                    Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                                    ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                                    object reflecClassObject = constructor.Invoke(new object[] { });
                                    MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                                    error = (string)reflecMethod.Invoke(reflecClassObject, new object[] { obj, pathLocal, archivo });
                                    if (string.IsNullOrEmpty(error))
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error = "ERROR LECTURA DE ARCHIVO: " + ex.Message.ToString();
                                }
                            }
                            else
                            {
                                error = "TIPO DE ARCHIVO NO ENCONTRADO";
                            }
                        }

                        #endregion lectura

                        #region actualiza proceso

                        if (string.IsNullOrEmpty(error))
                        {
                            obj.CODIGOAUTORIZA = CryptoUtil.Encrypt(Util.GetSecuencial(6), Util.semilla);
                            obj.FCODIGOAUTORIZA = DateTime.Now;
                            NotificarProceso(obj, out error);
                        }

                        if (string.IsNullOrEmpty(error))
                        {
                            obj.CESTADO = "PENAUT";
                        }
                        else
                        {
                            obj.CESTADO = "ERRPRO";
                            obj.ERROR = error;
                        }

                        obj.FMODIFICACION = DateTime.Now;

                        obj.Actualizar(obj);

                        #endregion actualiza proceso

                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, obj.CPROCESO + " FINALIZA LECTURA");
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public bool NotificarProceso(TBTHPROCESO proceso, out string error)
        {
            bool resp = true;
            VBTHPROCESO vproceso = null;
            Int32 registros = 0;
            Decimal total = 0;
            error = string.Empty;

            try
            {
                vproceso = new VBTHPROCESO().Listar(proceso.FPROCESO, proceso.CPROCESO);
                if (vproceso != null)
                {
                    switch (proceso.CTIPOPROCESO)
                    {
                        case "EFECHE":
                            resp = new TBTHDETALLECHEQUES().Totales(proceso.FPROCESO, proceso.CPROCESO, out registros, out total);
                            break;
                        default:
                            resp = new TBTHDETALLETABULADO().TotalesProceso(proceso.FPROCESO, proceso.CPROCESO, out registros, out total);
                            break;
                    }

                    if (resp)
                    {
                        wsS29.Iso8583 iso = new wsS29.Iso8583();
                        iso.ISO_000_Message_Type = "1200";
                        iso.ISO_002_PAN = "PROBTH001";
                        iso.ISO_003_ProcessingCode = "960000";
                        iso.ISO_007_TransDatetime = DateTime.Now;
                        iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                        iso.ISO_012_LocalDatetime = DateTime.Now;
                        iso.ISO_015_SettlementDatel = DateTime.Now;
                        iso.ISO_018_MerchantType = "0001";
                        iso.ISO_024_NetworkId = "555551";
                        csEstructuraNotificacion.notificacion objNotifica = new csEstructuraNotificacion.notificacion();
                        csEstructuraNotificacion.mensaje objMensaje = new csEstructuraNotificacion.mensaje();
                        objMensaje.to = glbEmails;
                        objMensaje.parametros =
                            vproceso.FPROCESO.Value.ToString("dd/MM/yyyy") + ";" +
                            vproceso.CPROCESO.Value.ToString() + ";" +
                            vproceso.TIPOPROCESO + ";" +
                            registros + ";" +
                            total.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                            vproceso.DESCRIPCION + ";" +
                            vproceso.CUSUARIO + " - " + vproceso.USUARIO + ";" +
                            CryptoUtil.Decrypt(proceso.CODIGOAUTORIZA, Util.semilla);
                        objNotifica.items = new object[] { objMensaje };
                        iso.ISO_114_ExtendedData = Util.ObjToString(objNotifica);
                        wsS29.uciMethods ws = new wsS29.uciMethods();
                        ws.Timeout = glbTimeoutWS;
                        iso = ws.ProcessingTransactionISO_WEB(iso);
                        if (iso.ISO_039_ResponseCode == "000")
                        {
                            resp = true;
                            error = string.Empty;
                        }
                        else
                        {
                            resp = false;
                            error = iso.ISO_039_ResponseCode + " - " + iso.ISO_039p_ResponseDetail;
                        }
                    }
                    else
                    {
                        error = "NO SE PUEDE OBTENER TOTALES DE PROCESO";
                    }
                }
                else
                {
                    error = "PROCESO NO ENCONTRADO";
                }
            }
            catch (Exception ex)
            {
                resp = false;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return resp;
        }

        public string LeeCheques(TBTHPROCESO pro, string path, string archivo)
        {
            #region variables

            StreamReader srArchivo = null;
            List<TBTHDETALLECHEQUES> ltDetalle = null;
            List<TBTHDETALLECHEQUES> ltAux = null;
            Task<TBTHDETALLECHEQUES>[] tareas = null;

            string linea = string.Empty;
            string error = string.Empty;
            string errorAux = string.Empty;
            string cuenta = string.Empty;

            Int32 numeroLinea = 0;
            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int hasta = 0;
            int indice = 0;
            Int32 registrosArchivo = 0;
            Int32 registrosBdd = 0;

            #endregion variables

            #region validacion archivo

            if (pro.ARCHIVOORIGEN.Split('.').ToArray()[1] != "txt")
            {
                error = "FORMATO DE ARCHIVO NO SOPORTADO";
            }

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            string[] campos = linea.Split('\t');
                            if (campos[5] == "20")
                            {
                                if (string.IsNullOrEmpty(campos[4]))
                                {
                                    errorAux += "CUENTA, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(campos[3]))
                                {
                                    errorAux += "BANCO, ";
                                }

                                if (string.IsNullOrEmpty(campos[2]))
                                {
                                    errorAux += "NUMERO CHEQUE, ";
                                }

                                if (string.IsNullOrEmpty(campos[4]))
                                {
                                    errorAux += "CUENTA CHEQUE, ";
                                }

                                if (string.IsNullOrEmpty(campos[6]))
                                {
                                    errorAux += "VALOR, ";
                                }

                                if (campos[6].IndexOf('.') == -1)
                                {
                                    errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            numeroLinea++;
                        }
                        else
                        {
                            error = "LINEA " + numeroLinea + ": VACIA";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR VALIDANDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion validacion archivo

            #region lectura

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    ltDetalle = new List<TBTHDETALLECHEQUES>();
                    Int32 secuencia = 0;
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        string[] campos = linea.Split('\t');
                        if (campos[5] == "20")
                        {
                            cuenta = campos[4];
                        }
                        else
                        {
                            try
                            {
                                TBTHDETALLECHEQUES reg = new TBTHDETALLECHEQUES();
                                reg.FPROCESO = pro.FPROCESO;
                                reg.CPROCESO = pro.CPROCESO;
                                reg.SECUENCIA = ++secuencia;
                                reg.CCUENTA = cuenta;
                                reg.MONTO = Convert.ToDecimal(campos[6].Replace(".", "")) / 100;
                                reg.BANCO = campos[3];
                                reg.NUMEROCHEQUE = campos[2];
                                reg.CUENTACHEQUE = campos[4];
                                try { reg.ACCION = campos[7].Trim(); }
                                catch { reg.ACCION = string.Empty; }
                                reg.CESTADO = "CARGAD";
                                reg.FCARGA = DateTime.Now;
                                ltDetalle.Add(reg);
                            }
                            catch (Exception ex)
                            {
                                Logging.EscribirLog("PROCESO " + pro.CPROCESO + " ERROR LEYENDO LINEA (" + numeroLinea + ") => " + linea, ex, "ERR");
                            }
                        }
                        numeroLinea++;
                    }

                    registrosArchivo = ltDetalle.Count;
                }
                catch (Exception ex)
                {
                    error = "ERROR LEYENDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                        srArchivo.Close();
                }
            }

            #endregion lectura

            #region guarda bdd

            if (string.IsNullOrEmpty(error) && ltDetalle.Count > 0)
            {
                numeroProcesos = glbProcesosBdd;
                numeroRegistros = ltDetalle.Count;
                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                for (int i = 1; i <= veces; i++)
                {
                    if (veces == i)
                    {
                        tareas = new Task<TBTHDETALLECHEQUES>[ultimo];
                        hasta = ultimo;
                    }
                    else
                    {
                        tareas = new Task<TBTHDETALLECHEQUES>[numeroProcesos];
                        hasta = numeroProcesos;
                    }

                    indice = 0;
                    ltAux = ltDetalle.GetRange(0, hasta);
                    foreach (TBTHDETALLECHEQUES detalle in ltAux)
                    {
                        tareas[indice] = new Task<TBTHDETALLECHEQUES>(() =>
                        {
                            return detalle.Insertar(detalle);
                        });
                        indice++;
                    }
                    foreach (TBTHDETALLECHEQUES detalle in ltAux)
                    {
                        ltDetalle.Remove(detalle);
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);
                }
            }

            #endregion guarda bdd

            #region validacion

            if (string.IsNullOrEmpty(error))
            {
                registrosBdd = new TBTHDETALLECHEQUES().ContarRegistrosProceso(pro);

                if (registrosBdd != registrosArchivo || registrosBdd == 0 || registrosArchivo == 0)
                {
                    error = "ERROS EN REGISTROS CARGADOS - ARCHIVO: " + registrosArchivo + ", BDD: " + registrosBdd;
                }
            }

            #endregion validacion

            return error;
        }

        public string LeeSPI2(TBTHPROCESO pro, string path, string archivo)
        {
            #region variables

            StreamReader srArchivo = null;

            List<TBTHCONCEPTOSBCE> ltConceptos = null;

            List<TBTHDETALLETABULADO> ltDetalle = null;
            List<TBTHDETALLETABULADO> ltAux = null;
            Task<TBTHDETALLETABULADO>[] tareas = null;

            string linea = string.Empty;
            string error = string.Empty;
            string errorAux = string.Empty;

            Int32 registrosArchivo = 0;
            Int32 numeroLinea = 0;
            Int32 registrosBdd = 0;

            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int hasta = 0;
            int indice = 0;

            Decimal totalValorArchivo = 0;
            Decimal totalBdd = 0;

            #endregion variables

            #region validacion archivo

            if (pro.ARCHIVOORIGEN.Split('.').ToArray()[1] != "txt")
            {
                error = "FORMATO DE ARCHIVO NO SOPORTADO";
            }

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            if (numeroLinea == 1)
                            {
                                string[] campos = linea.Split(',');

                                if (campos.Count() != 4)
                                {
                                    errorAux += "NUMERO CAMPOS CABECERA, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            else
                            {
                                string[] campos = linea.Split(',');

                                if (campos.Count() != 20)
                                {
                                    errorAux += "NUMERO CAMPOS, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            error = "LINEA " + numeroLinea + ": VACIA";
                            break;
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR VALIDANDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion validacion archivo

            #region lectura

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    ltConceptos = new TBTHCONCEPTOSBCE().Listar();
                    ltDetalle = new List<TBTHDETALLETABULADO>();
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;

                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        try
                        {
                            if (linea != "")
                            {
                                string[] campos = linea.Split(',');
                                if (numeroLinea > 1)
                                {
                                    TBTHDETALLETABULADO objDetalle = new TBTHDETALLETABULADO();
                                    objDetalle.FPROCESO = pro.FPROCESO;
                                    objDetalle.CPROCESO = pro.CPROCESO;
                                    objDetalle.SECUENCIA = numeroLinea - 1;
                                    objDetalle.IDENTIFICACION = campos[15];
                                    objDetalle.CUENTA = campos[11];
                                    objDetalle.VALOR = Convert.ToDecimal(campos[4].Replace(".", "")) / 100;
                                    try { objDetalle.RUBRO = ltConceptos.Where(x => x.CBCE == campos[5]).First().CFIT; }
                                    catch { objDetalle.RUBRO = 9001; }
                                    objDetalle.FCARGA = DateTime.Now;
                                    objDetalle.CESTADO = "CARGAD";
                                    objDetalle.ORDREFERENCIA = campos[1];
                                    objDetalle.ORDCUENTA = campos[7];
                                    objDetalle.ORDNOMBRE = campos[9].Replace("\'", "").Trim();
                                    objDetalle.ORDCUENTABCE = campos[19];
                                    objDetalle.RCPCUENTABCE = campos[6];
                                    objDetalle.LINEA = linea;
                                    ltDetalle.Add(objDetalle);
                                }
                                else
                                {
                                    registrosArchivo = Convert.ToInt32(campos[1]);
                                    totalValorArchivo = Convert.ToDecimal(campos[2].Replace('.', ','));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog("PROCESO " + pro.CPROCESO + " ERROR LEYENDO LINEA (" + numeroLinea + ") => " + linea, ex, "ERR");
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR LEYENDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                        srArchivo.Close();
                }
            }

            #endregion lectura

            #region guarda bdd

            if (string.IsNullOrEmpty(error) && ltDetalle.Count > 0)
            {
                numeroProcesos = glbProcesosBdd;
                numeroRegistros = ltDetalle.Count;
                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                for (int i = 1; i <= veces; i++)
                {
                    if (veces == i)
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[ultimo];
                        hasta = ultimo;
                    }
                    else
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[numeroProcesos];
                        hasta = numeroProcesos;
                    }

                    indice = 0;
                    ltAux = ltDetalle.GetRange(0, hasta);
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        tareas[indice] = new Task<TBTHDETALLETABULADO>(() =>
                        {
                            return detalle.Insertar(detalle);
                        });
                        indice++;
                    }
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        ltDetalle.Remove(detalle);
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);
                }
            }

            #endregion guarda bdd

            #region validacion

            if (string.IsNullOrEmpty(error))
            {
                if (new TBTHDETALLETABULADO().TotalesProceso(pro.FPROCESO, pro.CPROCESO, out registrosBdd, out totalBdd))
                {
                    if (registrosBdd != registrosArchivo && totalBdd != totalValorArchivo)
                    {
                        error = "ERROS EN REGISTROS CARGADOS - ARCHIVO: " + registrosArchivo + ", BDD: " + registrosBdd;
                    }
                }
            }

            #endregion validacion

            return error;
        }

        public string LeeTabuladoCuenta(TBTHPROCESO pro, string path, string archivo)
        {
            #region variables

            StreamReader srArchivo = null;

            List<TBTHDETALLETABULADO> ltDetalle = null;
            List<TBTHDETALLETABULADO> ltAux = null;
            Task<TBTHDETALLETABULADO>[] tareas = null;

            string linea = string.Empty;
            string error = string.Empty;
            string errorAux = string.Empty;

            Int32 registrosArchivo = 0;
            Int32 numeroLinea = 0;
            Int32 registrosBdd = 0;

            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int hasta = 0;
            int indice = 0;

            Decimal totalValorArchivo = 0;
            Decimal totalBdd = 0;

            #endregion variables

            #region validacion archivo

            if (pro.ARCHIVOORIGEN.Split('.').ToArray()[1] != "txt")
            {
                error = "FORMATO DE ARCHIVO NO SOPORTADO";
            }

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            if (numeroLinea == 1)
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "NUMERO REGISTROS, ";
                                }

                                if (string.IsNullOrEmpty(campos[1]))
                                {
                                    errorAux += "VALOR, ";
                                }

                                if (campos[1].IndexOf('.') == -1)
                                {
                                    errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            else
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "IDENTIFICACION, ";
                                }

                                if (string.IsNullOrEmpty(campos[2]))
                                {
                                    errorAux += "CUENTA, ";
                                }

                                try
                                {
                                    if (string.IsNullOrEmpty(campos[3]))
                                    {
                                        errorAux += "VALOR, ";
                                    }

                                    if (campos[3].IndexOf('.') == -1)
                                    {
                                        errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                    }
                                }
                                catch
                                {
                                    errorAux = "VALOR, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            error = "LINEA " + numeroLinea + ": VACIA";
                            break;
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR VALIDANDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion validacion archivo

            #region lectura

            if (string.IsNullOrEmpty(error))
            {
                try
                {

                    ltDetalle = new List<TBTHDETALLETABULADO>();
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;

                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        try
                        {
                            if (linea != "")
                            {
                                string[] campos = linea.Split('\t');
                                if (numeroLinea > 1)
                                {
                                    TBTHDETALLETABULADO objDetalle = new TBTHDETALLETABULADO();
                                    objDetalle.FPROCESO = pro.FPROCESO;
                                    objDetalle.CPROCESO = pro.CPROCESO;
                                    objDetalle.SECUENCIA = numeroLinea - 1;
                                    objDetalle.IDENTIFICACION = campos[0];
                                    objDetalle.CUENTA = campos[2];
                                    objDetalle.VALOR = Convert.ToDecimal(campos[3].Replace(".", "")) / 100;
                                    try { objDetalle.RUBRO = Convert.ToInt32(campos[4]); }
                                    catch { objDetalle.RUBRO = 9000; }
                                    objDetalle.FCARGA = DateTime.Now;
                                    objDetalle.CESTADO = "CARGAD";
                                    objDetalle.LINEA = linea;
                                    ltDetalle.Add(objDetalle);
                                }
                                else
                                {
                                    registrosArchivo = Convert.ToInt32(campos[0]);
                                    totalValorArchivo = Convert.ToDecimal(campos[1].Replace('.', ','));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog("PROCESO " + pro.CPROCESO + " ERROR LEYENDO LINEA (" + numeroLinea + ") => " + linea, ex, "ERR");
                        }
                        numeroLinea++;
                    }

                }
                catch (Exception ex)
                {
                    error = "ERROR LEYENDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion lectura

            #region guarda bdd

            if (string.IsNullOrEmpty(error) && ltDetalle.Count > 0)
            {
                numeroProcesos = glbProcesosBdd;
                numeroRegistros = ltDetalle.Count;
                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                for (int i = 1; i <= veces; i++)
                {
                    if (veces == i)
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[ultimo];
                        hasta = ultimo;
                    }
                    else
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[numeroProcesos];
                        hasta = numeroProcesos;
                    }

                    indice = 0;
                    ltAux = ltDetalle.GetRange(0, hasta);
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        tareas[indice] = new Task<TBTHDETALLETABULADO>(() =>
                        {
                            return detalle.Insertar(detalle);
                        });
                        indice++;
                    }
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        ltDetalle.Remove(detalle);
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);
                }
            }

            #endregion guarda bdd

            #region validacion

            if (string.IsNullOrEmpty(error))
            {
                if (new TBTHDETALLETABULADO().TotalesProceso(pro.FPROCESO, pro.CPROCESO, out registrosBdd, out totalBdd))
                {
                    if (registrosBdd != registrosArchivo && totalBdd != totalValorArchivo)
                    {
                        error = "ERROS EN REGISTROS CARGADOS - ARCHIVO: " + registrosArchivo + ", BDD: " + registrosBdd;
                    }
                }
            }

            #endregion validacion

            return error;
        }

        public string LeeTabuladoIdentificacion(TBTHPROCESO pro, string path, string archivo)
        {
            #region variables

            StreamReader srArchivo = null;

            List<TBTHDETALLETABULADO> ltDetalle = null;
            List<TBTHDETALLETABULADO> ltAux = null;
            Task<TBTHDETALLETABULADO>[] tareas = null;

            string linea = string.Empty;
            string error = string.Empty;
            string errorAux = string.Empty;

            Int32 registrosArchivo = 0;
            Int32 numeroLinea = 0;
            Int32 registrosBdd = 0;

            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int hasta = 0;
            int indice = 0;

            Decimal totalValorArchivo = 0;
            Decimal totalBdd = 0;

            #endregion variables

            #region validacion archivo

            if (pro.ARCHIVOORIGEN.Split('.').ToArray()[1] != "txt")
            {
                error = "FORMATO DE ARCHIVO NO SOPORTADO";
            }

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            if (numeroLinea == 1)
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "NUMERO REGISTROS, ";
                                }

                                if (string.IsNullOrEmpty(campos[1]))
                                {
                                    errorAux += "VALOR, ";
                                }

                                if (campos[1].IndexOf('.') == -1)
                                {
                                    errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            else
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "IDENTIFICACION, ";
                                }

                                if (string.IsNullOrEmpty(campos[2]))
                                {
                                    errorAux += "VALOR, ";
                                }

                                if (campos[2].IndexOf('.') == -1)
                                {
                                    errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            error = "LINEA " + numeroLinea + ": VACIA";
                            break;
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR VALIDANDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion validacion archivo

            #region lectura

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    ltDetalle = new List<TBTHDETALLETABULADO>();
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;

                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        try
                        {
                            if (linea != "")
                            {
                                string[] campos = linea.Split('\t');
                                if (numeroLinea > 1)
                                {
                                    TBTHDETALLETABULADO objDetalle = new TBTHDETALLETABULADO();
                                    objDetalle.FPROCESO = pro.FPROCESO;
                                    objDetalle.CPROCESO = pro.CPROCESO;
                                    objDetalle.SECUENCIA = numeroLinea - 1;
                                    objDetalle.IDENTIFICACION = campos[0];
                                    objDetalle.VALOR = Convert.ToDecimal(campos[2].Replace(".", "")) / 100;
                                    try { objDetalle.RUBRO = Convert.ToInt32(campos[3]); }
                                    catch { objDetalle.RUBRO = 9000; }
                                    objDetalle.FCARGA = DateTime.Now;
                                    objDetalle.CESTADO = "CARGAD";
                                    objDetalle.LINEA = linea;
                                    ltDetalle.Add(objDetalle);
                                }
                                else
                                {
                                    registrosArchivo = Convert.ToInt32(campos[0]);
                                    totalValorArchivo = Convert.ToDecimal(campos[1].Replace('.', ','));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog("PROCESO " + pro.CPROCESO + " ERROR LEYENDO LINEA (" + numeroLinea + ") => " + linea, ex, "ERR");
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR LEYENDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                        srArchivo.Close();
                }
            }

            #endregion lectura

            #region guarda bdd

            if (string.IsNullOrEmpty(error) && ltDetalle.Count > 0)
            {
                numeroProcesos = glbProcesosBdd;
                numeroRegistros = ltDetalle.Count;
                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                for (int i = 1; i <= veces; i++)
                {
                    if (veces == i)
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[ultimo];
                        hasta = ultimo;
                    }
                    else
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[numeroProcesos];
                        hasta = numeroProcesos;
                    }

                    indice = 0;
                    ltAux = ltDetalle.GetRange(0, hasta);
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        tareas[indice] = new Task<TBTHDETALLETABULADO>(() =>
                        {
                            return detalle.Insertar(detalle);
                        });
                        indice++;
                    }
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        ltDetalle.Remove(detalle);
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);
                }
            }

            #endregion guarda bdd

            #region validacion

            if (string.IsNullOrEmpty(error))
            {
                if (new TBTHDETALLETABULADO().TotalesProceso(pro.FPROCESO, pro.CPROCESO, out registrosBdd, out totalBdd))
                {
                    if (registrosBdd != registrosArchivo && totalBdd != totalValorArchivo)
                    {
                        error = "ERROS EN REGISTROS CARGADOS - ARCHIVO: " + registrosArchivo + ", BDD: " + registrosBdd;
                    }
                }
            }

            #endregion validacion

            return error;
        }

        public string LeeTabuladoCashPinchincha(TBTHPROCESO pro, string path, string archivo)
        {
            #region variables

            StreamReader srArchivo = null;

            List<TBTHDETALLETABULADO> ltDetalle = null;
            List<TBTHDETALLETABULADO> ltAux = null;
            Task<TBTHDETALLETABULADO>[] tareas = null;

            string linea = string.Empty;
            string error = string.Empty;
            string errorAux = string.Empty;

            Int32 registrosArchivo = 0;
            Int32 numeroLinea = 0;
            Int32 registrosBdd = 0;

            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int hasta = 0;
            int indice = 0;

            Decimal totalValorArchivo = 0;
            Decimal totalBdd = 0;

            #endregion variables

            #region validacion archivo

            if (pro.ARCHIVOORIGEN.Split('.').ToArray()[1] != "txt")
            {
                error = "FORMATO DE ARCHIVO NO SOPORTADO";
            }

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            if (numeroLinea == 1)
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "NUMERO REGISTROS, ";
                                }

                                if (string.IsNullOrEmpty(campos[1]))
                                {
                                    errorAux += "VALOR, ";
                                }

                                if (campos[1].IndexOf('.') == -1)
                                {
                                    errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                            else
                            {
                                string[] campos = linea.Split('\t');

                                if (string.IsNullOrEmpty(campos[0]))
                                {
                                    errorAux += "IDENTIFICACION, ";
                                }

                                if (string.IsNullOrEmpty(campos[2]))
                                {
                                    errorAux += "CREDITO, ";
                                }

                                try
                                {
                                    if (string.IsNullOrEmpty(campos[3]))
                                    {
                                        errorAux += "VALOR, ";
                                    }

                                    if (campos[3].IndexOf('.') == -1)
                                    {
                                        errorAux += "VALOR SIN SEPARADOR DECIMAL, ";
                                    }
                                }
                                catch
                                {
                                    errorAux = "VALOR, ";
                                }

                                if (!string.IsNullOrEmpty(errorAux))
                                {
                                    error = "LINEA " + numeroLinea + ": ERROR EN FORMATO => " + errorAux;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            error = "LINEA " + numeroLinea + ": VACIA";
                            break;
                        }
                        numeroLinea++;
                    }
                }
                catch (Exception ex)
                {
                    error = "ERROR VALIDANDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion validacion archivo

            #region lectura

            if (string.IsNullOrEmpty(error))
            {
                try
                {

                    ltDetalle = new List<TBTHDETALLETABULADO>();
                    srArchivo = new StreamReader(path + pro.ARCHIVOORIGEN, System.Text.Encoding.Default);
                    numeroLinea = 1;

                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        try
                        {
                            if (linea != "")
                            {
                                string[] campos = linea.Split('\t');
                                if (numeroLinea > 1)
                                {
                                    TBTHDETALLETABULADO objDetalle = new TBTHDETALLETABULADO();
                                    objDetalle.FPROCESO = pro.FPROCESO;
                                    objDetalle.CPROCESO = pro.CPROCESO;
                                    objDetalle.SECUENCIA = numeroLinea - 1;
                                    objDetalle.IDENTIFICACION = campos[0];
                                    objDetalle.CREDITO = campos[2];
                                    objDetalle.VALOR = Convert.ToDecimal(campos[3].Replace(".", "")) / 100;
                                    try { objDetalle.RUBRO = Convert.ToInt32(campos[4]); }
                                    catch { objDetalle.RUBRO = 9000; }
                                    objDetalle.FCARGA = DateTime.Now;
                                    objDetalle.CESTADO = "CARGAD";
                                    objDetalle.LINEA = linea;
                                    ltDetalle.Add(objDetalle);
                                }
                                else
                                {
                                    registrosArchivo = Convert.ToInt32(campos[0]);
                                    totalValorArchivo = Convert.ToDecimal(campos[1].Replace('.', ','));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog("PROCESO " + pro.CPROCESO + " ERROR LEYENDO LINEA (" + numeroLinea + ") => " + linea, ex, "ERR");
                        }
                        numeroLinea++;
                    }

                }
                catch (Exception ex)
                {
                    error = "ERROR LEYENDO ARCHIVO: " + ex.Message.ToString();
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
                finally
                {
                    if (srArchivo != null)
                    {
                        srArchivo.Close();
                    }
                }
            }

            #endregion lectura

            #region guarda bdd

            if (string.IsNullOrEmpty(error) && ltDetalle.Count > 0)
            {
                numeroProcesos = glbProcesosBdd;
                numeroRegistros = ltDetalle.Count;
                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                for (int i = 1; i <= veces; i++)
                {
                    if (veces == i)
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[ultimo];
                        hasta = ultimo;
                    }
                    else
                    {
                        tareas = new Task<TBTHDETALLETABULADO>[numeroProcesos];
                        hasta = numeroProcesos;
                    }

                    indice = 0;
                    ltAux = ltDetalle.GetRange(0, hasta);
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        tareas[indice] = new Task<TBTHDETALLETABULADO>(() =>
                        {
                            return detalle.Insertar(detalle);
                        });
                        indice++;
                    }
                    foreach (TBTHDETALLETABULADO detalle in ltAux)
                    {
                        ltDetalle.Remove(detalle);
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);
                }
            }

            #endregion guarda bdd

            #region validacion

            if (string.IsNullOrEmpty(error))
            {
                if (new TBTHDETALLETABULADO().TotalesProceso(pro.FPROCESO, pro.CPROCESO, out registrosBdd, out totalBdd))
                {
                    if (registrosBdd != registrosArchivo && totalBdd != totalValorArchivo)
                    {
                        error = "ERROS EN REGISTROS CARGADOS - ARCHIVO: " + registrosArchivo + ", BDD: " + registrosBdd;
                    }
                }
            }

            #endregion validacion

            return error;
        }

        #endregion lectura archivos

        #region tabulado vista

        public void ProcesoTabuladoVista()
        {
            #region variables
            List<VBTHPROCESO> ltProceso = null;
            List<TBTHDETALLETABULADO> ltTabulado = null;
            Task<TBTHDETALLETABULADO>[] tkTabulado = null;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            Int32 desde = 0;
            Int32 hasta = 0;
            Int32 indice = 0;
            #endregion variables

            try
            {
                ltProceso = new VBTHPROCESO().ListarXEstadoTipo("PENVAL", "SPITAB");
                if (ltProceso != null && ltProceso.Count > 0)
                {
                    foreach (VBTHPROCESO proceso in ltProceso)
                    {
                        new TBTHDETALLETABULADO().ActualizarPendientes(proceso.FPROCESO, proceso.CPROCESO, "CARGAD", "PENVIS");
                        numeroRegistros = new TBTHDETALLETABULADO().ContarPendientesPersonaCuenta(proceso.FPROCESO, proceso.CPROCESO, "PENVIS");
                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS: " + numeroRegistros);

                            #region segmentacionProcesos
                            numeroProcesos = glbProcesosBdd;
                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltTabulado = new TBTHDETALLETABULADO().ListarPendientesPersonaCuentaDesdeHasta(proceso.FPROCESO, proceso.CPROCESO, "PENVIS", desde, hasta);
                                if (ltTabulado != null && ltTabulado.Count > 0)
                                {
                                    tkTabulado = new Task<TBTHDETALLETABULADO>[ltTabulado.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLETABULADO detalle in ltTabulado)
                                    {
                                        detalle.vbthproceso = proceso;
                                        tkTabulado[indice] = new Task<TBTHDETALLETABULADO>(() =>
                                        {
                                            return TabuladoVista(detalle);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tkTabulado)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tkTabulado);
                                }
                            }
                            #endregion segmentacionProcesos

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " FIN PROCESO");
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " NO EXISTEN REGISTROS");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLETABULADO TabuladoVista(TBTHDETALLETABULADO registro)
        {
            List<VCUENTASPERSONA> ltCuenta = null;
            VCUENTASPERSONA objCuenta = null;

            try
            {
                #region validacion
                switch (registro.vbthproceso.CTIPOARCHIVO)
                {
                    case "0001":
                    case "0003":
                        objCuenta = new VCUENTASPERSONA().ListarCuentasVistaSPI(registro.CUENTA, registro.IDENTIFICACION);
                        if (objCuenta != null)
                        {
                            if (objCuenta.CESTATUSCUENTA == "002" && (objCuenta.CCONDICIONOPERATIVA == "NOR" || objCuenta.CCONDICIONOPERATIVA == "DEB"))
                            {
                                registro.CESTADO = "VALVIS";
                                registro.CPERSONA = objCuenta.CPERSONA;
                                registro.CSUBSISTEMA = objCuenta.CSUBSISTEMA;
                                registro.CODIGORECHAZO = "01";
                            }
                            else if (objCuenta.CESTATUSCUENTA == "005" || objCuenta.CESTATUSCUENTA == "004")
                            {
                                registro.CESTADO = "RECHAZ";
                                registro.CODIGORECHAZO = "02";
                                registro.ERROR = "CUENTA INACTIVA EST: " + objCuenta.CESTATUSCUENTA + "; CON: " + objCuenta.CCONDICIONOPERATIVA;
                            }
                            else if (objCuenta.CESTATUSCUENTA == "003")
                            {
                                registro.CESTADO = "RECHAZ";
                                registro.CODIGORECHAZO = "07";
                                registro.ERROR = "CUENTA INACTIVA EST: " + objCuenta.CESTATUSCUENTA + "; CON: " + objCuenta.CCONDICIONOPERATIVA;
                            }
                            else
                            {
                                registro.CESTADO = "RECHAZ";
                                registro.CODIGORECHAZO = "03";
                                registro.ERROR = "CUENTA EN ESTADO INVALIDO PARA PROCESO EST: " + objCuenta.CESTATUSCUENTA + "; CON: " + objCuenta.CCONDICIONOPERATIVA;
                            }
                        }
                        else
                        {
                            registro.CESTADO = "RECHAZ";
                            registro.CODIGORECHAZO = "03";
                            registro.ERROR = "CUENTA NO ENCONTRADA";
                        }
                        break;
                    case "0002":
                    case "0005":
                        ltCuenta = new VCUENTASPERSONA().ListarCuentaPersona(registro.IDENTIFICACION);
                        if (ltCuenta != null && ltCuenta.Count > 0)
                        {
                            objCuenta = ltCuenta.First();
                            registro.CUENTA = objCuenta.CCUENTA;
                            registro.CESTADO = "VALVIS";
                            registro.CPERSONA = objCuenta.CPERSONA;
                            registro.CSUBSISTEMA = objCuenta.CSUBSISTEMA;
                            registro.CODIGORECHAZO = "01";
                        }
                        else
                        {
                            registro.CESTADO = "RECHAZ";
                            registro.ERROR = "NO EXISTEN CUENTAS PARA IDENTIFICACION: " + registro.IDENTIFICACION;
                            registro.CUENTA = string.Empty;
                            registro.CPERSONA = null;
                            registro.CSUBSISTEMA = string.Empty;
                            registro.CODIGORECHAZO = "03";
                        }
                        break;
                    default:
                        break;
                }
                #endregion validacion

                registro.FACTUALIZACION = DateTime.Now;
                new TBTHDETALLETABULADO().ActualizarPersonaCuenta(registro, registro.vbthproceso.CTIPOARCHIVO);
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " PROCESO: " + registro.CPROCESO + "; CPERSONA: " + registro.CPERSONA + "; ", ex, "ERR");
            }

            return registro;
        }

        #endregion tabulado vista

        #region tabulado credito

        public void ProcesoTabuladoCredito()
        {
            #region varaibles

            List<VBTHPROCESO> ltProcesos = null;
            List<TBTHDETALLETABULADO> ltTabulado = null;
            Task<TBTHDETALLETABULADO>[] tkTabulado = null;
            string error = string.Empty;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            Int32 desde = 0;
            Int32 hasta = 0;
            Int32 indice = 0;

            #endregion varaibles

            try
            {
                ltProcesos = new VBTHPROCESO().ListarXEstadoTipo("PENVAL", "SPITAB");
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    foreach (VBTHPROCESO proceso in ltProcesos)
                    {
                        new TBTHDETALLETABULADO().ActualizarPendientes(proceso.FPROCESO, proceso.CPROCESO, "VALVIS", "PENCRE");
                        numeroRegistros = new TBTHDETALLETABULADO().ContarPendientesPersona(proceso.FPROCESO, proceso.CPROCESO, "PENCRE");
                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS: " + numeroRegistros);

                            #region segmentacionProcesos

                            numeroProcesos = glbProcesosBdd;
                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltTabulado = new TBTHDETALLETABULADO().ListarPendientesPersonaDesdeHasta(proceso.FPROCESO, proceso.CPROCESO, "PENCRE", desde, hasta);
                                if (ltTabulado != null && ltTabulado.Count > 0)
                                {
                                    tkTabulado = new Task<TBTHDETALLETABULADO>[ltTabulado.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLETABULADO detalle in ltTabulado)
                                    {
                                        detalle.vbthproceso = proceso;
                                        tkTabulado[indice] = new Task<TBTHDETALLETABULADO>(() =>
                                        {
                                            return TabuladoCredito(detalle);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tkTabulado)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tkTabulado);
                                }
                            }
                            #endregion segmentacionProcesos

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " FIN PROCESO");
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " NO EXISTEN REGISTROS");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLETABULADO TabuladoCredito(TBTHDETALLETABULADO registro)
        {
            List<VPRESTAMOSPERSONA> ltPrestamos = null;
            TBTHDETALLEPROCESO detProceso = null;

            try
            {
                ltPrestamos = new VPRESTAMOSPERSONA().ListarPrestamos(registro.CPERSONA);

                if (ltPrestamos != null && ltPrestamos.Count > 0)
                    registro.CESTADO = "VALCRE";
                else
                    registro.CESTADO = "TERMIN";

                #region inserta detalle proceso

                detProceso = new TBTHDETALLEPROCESO();
                detProceso.FPROCESO = registro.FPROCESO;
                detProceso.CPROCESO = registro.CPROCESO;
                detProceso.CTIPOTRANSACCION = "NOTCRE";
                detProceso.CUSUARIO = registro.vbthproceso.CUSUARIO;
                detProceso.CSUCURSAL = Convert.ToInt32(registro.vbthproceso.CSUCURSALREPARTO);
                detProceso.COFICINA = Convert.ToInt32(registro.vbthproceso.COFICINAREPARTO);
                detProceso.CPERSONA = registro.CPERSONA;
                detProceso.CESTADO = "VERMOV";
                detProceso.FCARGA = DateTime.Now;

                if (!detProceso.InsertarTabulado(detProceso))
                {
                    registro.CESTADO = "ERRREG";
                    registro.ERROR = "ERROR INSERTANDO EN TBTHDETALLEPROCESO";
                }

                #endregion inserta detalle proceso

                registro.FACTUALIZACION = DateTime.Now;
                new TBTHDETALLETABULADO().ActualizarPersona(registro);
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        #endregion tabulado credito

        #region tabulado bloqueos

        public void ProcesoTabuladoBloqueos()
        {
            #region varaibles
            List<VBTHPROCESO> ltProcesos = null;
            List<TBTHDETALLETABULADO> ltTabulado = null;
            Task<TBTHDETALLETABULADO>[] tkTabulado = null;
            string error = string.Empty;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            Int32 desde = 0;
            Int32 hasta = 0;
            Int32 indice = 0;
            #endregion varaibles
            try
            {
                ltProcesos = new VBTHPROCESO().ListarXEstadoTipo("PENVAL", "SPITAB");
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    foreach (VBTHPROCESO proceso in ltProcesos)
                    {
                        new TBTHDETALLETABULADO().ActualizarPendientes(proceso.FPROCESO, proceso.CPROCESO, "VALCRE", "PENBLQ");
                        numeroRegistros = new TBTHDETALLETABULADO().ContarPendientesPersonaCuenta(proceso.FPROCESO, proceso.CPROCESO, "PENBLQ");
                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS: " + numeroRegistros);

                            #region segmentacionProcesos

                            numeroProcesos = glbProcesosBdd;
                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltTabulado = new TBTHDETALLETABULADO().ListarPendientesPersonaCuentaDesdeHasta(proceso.FPROCESO, proceso.CPROCESO, "PENBLQ", desde, hasta);
                                if (ltTabulado != null && ltTabulado.Count > 0)
                                {
                                    tkTabulado = new Task<TBTHDETALLETABULADO>[ltTabulado.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLETABULADO detalle in ltTabulado)
                                    {
                                        detalle.vbthproceso = proceso;
                                        tkTabulado[indice] = new Task<TBTHDETALLETABULADO>(() =>
                                        {
                                            return TabuladoBloqueos(detalle, proceso.CUSUARIO);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tkTabulado)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tkTabulado);
                                }
                            }
                            #endregion segmentacionProcesos

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " FIN PROCESO");
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " NO EXISTEN REGISTROS");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLETABULADO TabuladoBloqueos(TBTHDETALLETABULADO objDetalle, string cusuario)
        {
            #region variables
            TBTHTEMP temp = null;
            List<TBTHDETALLETABULADO> ltDetalle = null;
            List<VPRESTAMOSPERSONA> ltCreditos = null;
            List<TCUENTABLOQUEOFONDOS> ltBloqueos = null;
            decimal valorPendiente = 0;
            decimal saldoBloqueado = 0;
            decimal valorBloqueo = 0;
            decimal saldoSpi = 0;
            string numeroCredito = string.Empty;
            int rubro = 0;
            #endregion variables
            try
            {
                if (objDetalle.vbthproceso.CTIPOARCHIVO != "0005")
                {
                    #region bloqueos spi

                    objDetalle.CESTADO = "TERMIN";
                    ltDetalle = new TBTHDETALLETABULADO().ListarPendientesPersonaCuenta(objDetalle.FPROCESO, objDetalle.CPROCESO, objDetalle.IDENTIFICACION, objDetalle.CUENTA, "PENBLQ");
                    if (ltDetalle != null && ltDetalle.Count > 0)
                    {
                        rubro = ltDetalle.FirstOrDefault().RUBRO.Value;
                        saldoSpi = ltDetalle.Sum(x => x.VALOR).Value;

                        ltCreditos = new VPRESTAMOSPERSONA().ListarPrestamos(objDetalle.CPERSONA);

                        if (ltCreditos != null && ltCreditos.Count > 0)
                        {
                            foreach (VPRESTAMOSPERSONA credito in ltCreditos)
                            {
                                valorPendiente = new BddAuxiliar().ValorPendienteCredito(credito.CCUENTA, credito.ORIGEN);
                                if (valorPendiente > 0)
                                {
                                    ltBloqueos = new TCUENTABLOQUEOFONDOS().ListarBloqueosCredito(credito.CCUENTA);

                                    if (ltBloqueos != null && ltBloqueos.Count > 0)
                                        saldoBloqueado = ltBloqueos.Sum(x => x.VALORBLOQUEO).Value - ltBloqueos.Sum(x => x.MONTOLIBERADO).Value;
                                    else
                                        saldoBloqueado = 0;

                                    valorBloqueo = valorPendiente - saldoBloqueado;

                                    if (valorBloqueo > 0)
                                    {
                                        if (saldoSpi <= valorBloqueo)
                                        {
                                            if (rubro != 9000)
                                                valorBloqueo = saldoSpi - (glbComisionSpi * ltDetalle.Count);
                                            else
                                                valorBloqueo = saldoSpi;
                                        }
                                        saldoSpi = saldoSpi - valorBloqueo;
                                        if (valorBloqueo > 0)
                                        {
                                            #region armaObjetoDetalle
                                            temp = new TBTHTEMP();
                                            temp.FPROCESO = objDetalle.FPROCESO;
                                            temp.CPROCESO = objDetalle.CPROCESO;
                                            temp.CTIPOTRANSACCION = "INGBLQ";
                                            temp.CUSUARIO = cusuario;
                                            temp.CSUCURSAL = 1;
                                            temp.COFICINA = 13;
                                            temp.CPERSONA = objDetalle.CPERSONA;
                                            temp.IDENTIFICACION = objDetalle.IDENTIFICACION;
                                            temp.CUENTA = objDetalle.CUENTA;
                                            temp.RUBRO = 1111;
                                            temp.VALOR = valorBloqueo;
                                            temp.VALORPENDIENTE = 0;
                                            temp.REFERENCIA = credito.CCUENTA;
                                            temp.CESTADO = "CARGAD";
                                            temp.PROCESA = "0";
                                            temp.FCARGA = DateTime.Now;
                                            if (!temp.Insertar(temp))
                                            {
                                                objDetalle.CESTADO = "ERRREG";
                                                objDetalle.ERROR = "ERROR INSERTANDO EN TBTHTEMP";
                                                break;
                                            }
                                            #endregion armaObjetoDetalle
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            objDetalle.CESTADO = "TERMIN";
                        }

                        objDetalle.FACTUALIZACION = DateTime.Now;
                        new TBTHDETALLETABULADO().ActualizarPersona(objDetalle);
                    }

                    #endregion bloqueos spi
                }
                else
                {
                    #region bloqueos cash

                    ltDetalle = new TBTHDETALLETABULADO().ListarPendientesPersonaCuenta(objDetalle.FPROCESO, objDetalle.CPROCESO, objDetalle.IDENTIFICACION, objDetalle.CUENTA, "PENBLQ");
                    if (ltDetalle != null && ltDetalle.Count > 0)
                    {
                        foreach (TBTHDETALLETABULADO detalle in ltDetalle)
                        {
                            VPRESTAMOSPERSONA credito = null;

                            detalle.CESTADO = "TERMIN";
                            numeroCredito = detalle.CREDITO;
                            rubro = detalle.RUBRO.Value;
                            saldoSpi = detalle.VALOR.Value;

                            try { credito = new VPRESTAMOSPERSONA().ListarPrestamos(numeroCredito).First(); }
                            catch { credito = null; }

                            if (credito != null)
                            {
                                valorPendiente = new BddAuxiliar().ValorPendienteCredito(credito.CCUENTA, credito.ORIGEN);
                                if (valorPendiente > 0)
                                {
                                    ltBloqueos = new TCUENTABLOQUEOFONDOS().ListarBloqueosCredito(credito.CCUENTA);

                                    if (ltBloqueos != null && ltBloqueos.Count > 0)
                                        saldoBloqueado = ltBloqueos.Sum(x => x.VALORBLOQUEO).Value - ltBloqueos.Sum(x => x.MONTOLIBERADO).Value;
                                    else
                                        saldoBloqueado = 0;

                                    valorBloqueo = valorPendiente - saldoBloqueado;

                                    if (valorBloqueo > 0)
                                    {
                                        if (saldoSpi <= valorBloqueo)
                                        {
                                            if (rubro != 9000)
                                                valorBloqueo = saldoSpi - (glbComisionSpi * ltDetalle.Count);
                                            else
                                                valorBloqueo = saldoSpi;
                                        }
                                        saldoSpi = saldoSpi - valorBloqueo;
                                        if (valorBloqueo > 0)
                                        {
                                            #region armaObjetoDetalle
                                            temp = new TBTHTEMP();
                                            temp.FPROCESO = detalle.FPROCESO;
                                            temp.CPROCESO = detalle.CPROCESO;
                                            temp.CTIPOTRANSACCION = "INGBLQ";
                                            temp.CUSUARIO = cusuario;
                                            temp.CSUCURSAL = 1;
                                            temp.COFICINA = 13;
                                            temp.CPERSONA = detalle.CPERSONA;
                                            temp.IDENTIFICACION = detalle.IDENTIFICACION;
                                            temp.CUENTA = detalle.CUENTA;
                                            temp.RUBRO = 1111;
                                            temp.VALOR = valorBloqueo;
                                            temp.VALORPENDIENTE = 0;
                                            temp.REFERENCIA = credito.CCUENTA;
                                            temp.CESTADO = "CARGAD";
                                            temp.PROCESA = "0";
                                            temp.FCARGA = DateTime.Now;
                                            if (!temp.Insertar(temp))
                                            {
                                                detalle.CESTADO = "ERRREG";
                                                detalle.ERROR = "ERROR INSERTANDO EN TBTHTEMP";
                                            }
                                            #endregion armaObjetoDetalle
                                        }
                                    }
                                }
                            }
                            else
                                detalle.CESTADO = "TERMIN";

                            detalle.FACTUALIZACION = DateTime.Now;
                            new TBTHDETALLETABULADO().Actualizar(detalle);
                        }
                    }

                    #endregion bloqueos cash
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + ", ", ex, "ERR");
            }

            return objDetalle;
        }

        #endregion tabulado bloqueos

        #region tabulado spi3

        public void ProcesoTabuladoSPI3()
        {
            #region variables

            List<TBTHPROCESO> ltProcesos = null;
            List<TBTHDETALLETABULADO> ltTabulado = null;
            Int32 registrosTotales = 0;
            Int32 registrosProcesados = 0;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            Int32 desde = 0;
            Int32 hasta = 0;
            decimal valorTotal = 0;
            string pathLocal = string.Empty;
            string pathFtp = string.Empty;
            string archivo = string.Empty;
            string ruta = string.Empty;
            string error = string.Empty;
            string linea = string.Empty;

            #endregion variables

            try
            {
                ltProcesos = new TBTHPROCESO().ListarXEstadoTipo("PENPRO", "SPITAB");
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProcesos)
                    {
                        if (proceso.CTIPOARCHIVO == "0001")
                        {
                            new TBTHDETALLETABULADO().TotalesProceso(proceso.FPROCESO, proceso.CPROCESO, out registrosTotales, out valorTotal);
                            registrosProcesados = new TBTHDETALLETABULADO().ContarPendientesValidarVista(proceso.FPROCESO, proceso.CPROCESO);

                            if (registrosTotales == registrosProcesados && string.IsNullOrEmpty(proceso.ARCHIVORESPUESTA))
                            {
                                pathFtp = string.Format(glbPathFtp, proceso.FPROCESO.Value.ToString("yyyyMMdd"));
                                pathLocal = string.Format(glbPathLocal, proceso.FPROCESO.Value.ToString("yyyyMMdd"));
                                archivo = string.Format("SPI3_{0}_{1}_{2}.txt", glbCuentaBce, proceso.FPROCESO.Value.ToString("dd-MM-yyyy"), proceso.CPROCESO);
                                StreamWriter file = null;
                                try
                                {
                                    if (!Directory.Exists(pathLocal))
                                    {
                                        Directory.CreateDirectory(pathLocal);
                                    }

                                    file = new StreamWriter(pathLocal + archivo);
                                    linea = DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "," + registrosProcesados + "," + proceso.CORTE + "," + glbCuentaBce;
                                    file.WriteLine(linea);

                                    #region segmentacionProcesos

                                    numeroRegistros = registrosTotales;
                                    numeroProcesos = glbProcesosBdd;
                                    Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);
                                    desde = 1;

                                    for (int i = 1; i <= veces; i++)
                                    {
                                        if (veces == i)
                                            hasta += ultimo;
                                        else
                                            hasta += numeroProcesos;

                                        ltTabulado = new TBTHDETALLETABULADO().ListarDesdeHasta(proceso.FPROCESO, proceso.CPROCESO, desde, hasta);
                                        if (ltTabulado != null && ltTabulado.Count > 0)
                                        {
                                            foreach (TBTHDETALLETABULADO detalle in ltTabulado)
                                            {
                                                string[] campos = detalle.LINEA.Split(',');
                                                linea =
                                                    campos[16] + "," +
                                                    campos[17] + "," +
                                                    campos[1] + "," +
                                                    DateTime.Today.ToString("dd/MM/yyyy 00:00:00") + "," +
                                                    Convert.ToInt16(detalle.CODIGORECHAZO) + "," +
                                                    campos[19];
                                                file.WriteLine(linea);
                                            }
                                        }
                                        desde += numeroProcesos;
                                    }

                                    #endregion segmentacionProcesos
                                }
                                catch (Exception ex)
                                {
                                    error = ex.Message.ToString().ToUpper();
                                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                                }
                                finally
                                {
                                    if (file != null)
                                    {
                                        file.Close();
                                    }
                                }

                                if (!Util.UploadFtp(glbFtpParametros, pathFtp, pathLocal, archivo, out error))
                                {
                                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ERROR CARGANDO ARCHIVO: " + error, null, "ERR");
                                }

                                proceso.ARCHIVORESPUESTA = archivo;
                                proceso.FMODIFICACION = DateTime.Now;
                                new TBTHPROCESO().Actualizar(proceso);
                            }
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        #endregion tabulado spi3

        #region archivo lotes

        public void ProcesoTabuladoLotes(out bool flag)
        {
            #region variables

            List<TBTHPROCESO> ltProcesos = null;
            List<TBTHDETALLETABULADO> ltTabulado = null;
            Int32 registrosTotales = 0;
            Int32 registrosProcesados = 0;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            Int32 desde = 0;
            Int32 hasta = 0;
            decimal valorTotal = 0;
            string pathLocal = string.Empty;
            string pathFtp = string.Empty;
            string archivo = string.Empty;
            string ruta = string.Empty;
            string error = string.Empty;
            string linea = string.Empty;
            StreamWriter file = null;
            flag = true;

            #endregion variables

            try
            {
                ltProcesos = new TBTHPROCESO().ListarXEstadoTipo("VALTER", "SPITAB");
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProcesos)
                    {
                        new TBTHDETALLETABULADO().TotalesProceso(proceso.FPROCESO, proceso.CPROCESO, out registrosTotales, out valorTotal);
                        registrosProcesados = new TBTHDETALLETABULADO().ContarPendientesValidarVista(proceso.FPROCESO, proceso.CPROCESO);

                        if (registrosTotales == registrosProcesados && string.IsNullOrEmpty(proceso.ARCHIVOLOTES) && string.IsNullOrEmpty(proceso.DATOSLOTE))
                        {
                            pathFtp = string.Format(glbPathFtp, proceso.FPROCESO.Value.ToString("yyyyMMdd"));
                            pathLocal = string.Format(glbPathLocal, proceso.FPROCESO.Value.ToString("yyyyMMdd"));
                            archivo = string.Format("LOTE{0}{1}_{2}.txt", proceso.FPROCESO.Value.ToString("yyyyMMdd"), proceso.CPROCESO, DateTime.Now.ToString("yyyyMMddHHmmss"));

                            try
                            {
                                if (!Directory.Exists(pathLocal))
                                    Directory.CreateDirectory(pathLocal);

                                file = new StreamWriter(pathLocal + archivo);

                                #region segmentacionProcesos

                                numeroRegistros = registrosTotales;
                                numeroProcesos = glbProcesosBdd;
                                Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);
                                desde = 1;

                                for (int i = 1; i <= veces; i++)
                                {
                                    if (veces == i)
                                        hasta += ultimo;
                                    else
                                        hasta += numeroProcesos;

                                    ltTabulado = new TBTHDETALLETABULADO().ListarDesdeHasta(proceso.FPROCESO, proceso.CPROCESO, desde, hasta);
                                    if (ltTabulado != null && ltTabulado.Count > 0)
                                    {
                                        foreach (TBTHDETALLETABULADO detalle in ltTabulado)
                                        {
                                            if (detalle.CODIGORECHAZO == "01")
                                            {
                                                linea =
                                                    detalle.CUENTA + "\t" +
                                                    detalle.VALOR.Value.ToString().Replace(",", ".") + "\t" +
                                                    detalle.RUBRO.Value.ToString() + "\t" +
                                                    glbFitbankSucursal + "\t" +
                                                    glbFitbankOficina;
                                                file.WriteLine(linea);
                                            }
                                        }
                                    }
                                    desde += numeroProcesos;
                                }

                                #endregion segmentacionProcesos
                            }
                            catch (Exception ex)
                            {
                                flag = false;
                                error = ex.Message.ToString().ToUpper();
                                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                            }
                            finally
                            {
                                if (file != null)
                                {
                                    file.Close();
                                }
                            }

                            if (!Util.UploadSftp(glbFtpLotesParametros, glbPathFtpLotes, pathLocal, archivo, out error))
                            {
                                flag = false;
                                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ERROR CARGANDO ARCHIVO: " + error, null, "ERR");
                            }

                            proceso.ARCHIVOLOTES = archivo;

                            proceso.transaccion = BthProcesos.glbLtTransaccion.Where(x => x.CTIPOTRANSACCION == "CARLOT").First();

                            Fitbank fit = new Fitbank();

                            TBTHPROCESO procesoAux = fit.ExecutaTransaccionCargaLote(proceso);

                            proceso.DATOSLOTE = procesoAux.DATOSLOTE;
                            proceso.FMODIFICACION = DateTime.Now;
                            if (procesoAux.CERROR != "000")
                            {
                                proceso.CESTADO = "ERRPRO";
                                proceso.ERROR = "ERROR CARAGANDO LOTE " + proceso.CERROR + " - " + proceso.DERROR;
                            }
                            else
                            {
                                proceso.CESTADO = "PENPRO";
                            }
                            flag = new TBTHPROCESO().Actualizar(proceso);
                        }

                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "FINALIZA PROCESO " + proceso.CPROCESO.Value);
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        #endregion archivo lotes

        #region proceso ejecuta

        public void ProcesoEjecuta(string criteriosProceso, string criteriosDetalle, int procesos)
        {
            List<TBTHPROCESO> ltProceso = null;
            List<TBTHDETALLEPROCESO> ltRegistros = null;
            Task<TBTHDETALLEPROCESO>[] tareas = null;
            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int desde = 0;
            int hasta = 0;
            int indice = 0;

            try
            {
                ltProceso = new TBTHPROCESO().ListarXCriterios(criteriosProceso);
                if (ltProceso != null && ltProceso.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProceso)
                    {
                        switch (proceso.CTIPOPROCESO)
                        {
                            case "SPITAB":
                                criteriosDetalle = " AND CTIPOTRANSACCION NOT IN ('NOTCRE') ";
                                break;
                            case "RCPSIF":
                                criteriosDetalle = string.Empty;
                                break;
                        }

                        new TBTHDETALLEPROCESO().ActualizarPendientes(proceso.CPROCESO.Value, criteriosDetalle);
                        numeroRegistros = new TBTHDETALLEPROCESO().ContarPendientes(proceso.CPROCESO.Value, criteriosDetalle);
                        numeroProcesos = procesos;

                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS A PROCESAR: " + numeroRegistros);

                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltRegistros = new TBTHDETALLEPROCESO().ListarPendientes(proceso.CPROCESO.Value, criteriosDetalle, desde, hasta);
                                if (ltRegistros != null && ltRegistros.Count > 0)
                                {
                                    tareas = new Task<TBTHDETALLEPROCESO>[ltRegistros.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLEPROCESO det in ltRegistros)
                                    {
                                        tareas[indice] = new Task<TBTHDETALLEPROCESO>(() =>
                                        {
                                            return RegistroEjecuta(det, det.SECUENCIA.Value);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tareas)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tareas);
                                }
                            }

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS PROCESADOS: " + numeroRegistros);
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLEPROCESO RegistroEjecuta(TBTHDETALLEPROCESO registro, Int32 hilo)
        {
            Fitbank fit = new Fitbank();

            try
            {
                registro.timeTotal = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                registro.timeTotal.Value.Reset();
                registro.timeTotal.Value.Start();

                registro.transaccion = BthProcesos.glbLtTransaccion.Where(x => x.CTIPOTRANSACCION == registro.CTIPOTRANSACCION).First();

                registro = fit.ExecutaTransaccionUci(registro, hilo);

                registro.CESTADO = ReturnEstado(registro.CERROR);
                registro.FACTUALIZACION = DateTime.Now;

                registro.timeTotal.Value.Stop();
                registro.TIEMPOTOTAL = Math.Round(registro.timeTotal.Value.Elapsed.TotalSeconds, 2);

                new TBTHDETALLEPROCESO().Actualizar(registro);

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, registro.CPROCESO.Value.ToString().PadRight(10, ' ') + " " + registro.SECUENCIA.Value.ToString().PadRight(10, ' ') + " " + registro.CTIPOTRANSACCION.PadRight(10, ' ') + " " + registro.CERROR.PadRight(10, ' '));
            }
            catch (Exception ex)
            {
                registro.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        #endregion proceso ejecuta

        #region proceso recuperacion

        public void ProcesoRecuperacion()
        {
            List<TBTHPROCESO> ltProceso = null;
            List<TBTHDETALLEPROCESO> ltRegistros = null;
            Task<TBTHDETALLEPROCESO>[] tareas = null;
            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int desde = 0;
            int hasta = 0;
            int indice = 0;

            try
            {
                ltProceso = new TBTHPROCESO().ListarXEstadoTipo("PENPRO", "RCPVAL");
                if (ltProceso != null && ltProceso.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProceso)
                    {
                        new TBTHDETALLEPROCESO().ActualizarPendientesRecuperacion(proceso.FPROCESO, proceso.CPROCESO);
                        numeroRegistros = new TBTHDETALLEPROCESO().ContarPendientesRecuperacion(proceso.FPROCESO, proceso.CPROCESO);
                        numeroProcesos = glbEjecutaRecuperacionProcesos;

                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS A PROCESAR: " + numeroRegistros);

                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltRegistros = new TBTHDETALLEPROCESO().ListarPendientesRecuperacion(proceso.FPROCESO, proceso.CPROCESO, desde, hasta);
                                if (ltRegistros != null && ltRegistros.Count > 0)
                                {
                                    tareas = new Task<TBTHDETALLEPROCESO>[ltRegistros.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLEPROCESO det in ltRegistros)
                                    {
                                        tareas[indice] = new Task<TBTHDETALLEPROCESO>(() =>
                                        {
                                            return RegistroRecuperacion(det);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tareas)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tareas);
                                }
                            }

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS PROCESADOS: " + numeroRegistros);
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLEPROCESO RegistroRecuperacion(TBTHDETALLEPROCESO registro)
        {
            Fitbank fit = new Fitbank();

            try
            {
                registro.timeTotal = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                registro.timeTotal.Value.Reset();
                registro.timeTotal.Value.Start();

                registro.transaccion = BthProcesos.glbLtTransaccion.Where(x => x.CTIPOTRANSACCION == registro.CTIPOTRANSACCION).First();

                registro = fit.ExecutaTransaccionUci(registro, 0);

                registro.CESTADO = "VERMOV";
                registro.FACTUALIZACION = DateTime.Now;

                registro.timeTotal.Value.Stop();
                registro.TIEMPOTOTAL = Math.Round(registro.timeTotal.Value.Elapsed.TotalSeconds, 2);

                new TBTHDETALLEPROCESO().Actualizar(registro);

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, registro.CPROCESO.Value.ToString().PadRight(10, ' ') + " " + registro.SECUENCIA.Value.ToString().PadRight(10, ' ') + " " + registro.CTIPOTRANSACCION.PadRight(10, ' ') + " " + registro.CERROR.PadRight(10, ' '));
            }
            catch (Exception ex)
            {
                registro.DERROR = MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return registro;
        }

        #endregion proceso recuperacion

        #region activa bloqueos

        public void ProcesoActivarBloqueos()
        {
            List<TBTHPROCESO> ltProceso = null;
            List<TBTHDETALLEPROCESO> ltRegistros = null;
            Task<TBTHDETALLEPROCESO>[] tareas = null;
            string error = string.Empty;
            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int desde = 0;
            int hasta = 0;
            int indice = 0;
            try
            {
                ltProceso = new TBTHPROCESO().ListarXEstadoTipo("PENPRO", "SPITAB");
                if (ltProceso != null && ltProceso.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProceso)
                    {
                        numeroRegistros = new TBTHDETALLEPROCESO().ContarActivaBloqueos(proceso.FPROCESO, proceso.CPROCESO);
                        numeroProcesos = BthProcesos.glbEjecutaBloqueosProcesos;

                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS A PROCESAR: " + numeroRegistros);

                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            for (int i = 1; i <= veces; i++)
                            {
                                desde = 1;
                                if (veces == i)
                                {
                                    hasta = ultimo;
                                }
                                else
                                {
                                    hasta = numeroProcesos;
                                }

                                ltRegistros = new TBTHDETALLEPROCESO().ListarActivaBloqueos(proceso.FPROCESO, proceso.CPROCESO, desde, hasta);
                                if (ltRegistros.Count > 0)
                                {
                                    tareas = new Task<TBTHDETALLEPROCESO>[ltRegistros.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLEPROCESO det in ltRegistros)
                                    {
                                        tareas[indice] = new Task<TBTHDETALLEPROCESO>(() =>
                                        {
                                            return ProcesaActivaBloqueos(det);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tareas)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tareas);
                                }
                            }

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS PROCESADOS: " + numeroRegistros);
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN PROCESOS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLEPROCESO ProcesaActivaBloqueos(TBTHDETALLEPROCESO obj)
        {
            List<TBTHDETALLEPROCESO> ltTrx = null;
            string error = string.Empty;
            string numeroMensaje = string.Empty;
            try
            {
                ltTrx = new TBTHDETALLEPROCESO().ListarTransaccionesPersona(obj);
                if (ltTrx != null && ltTrx.Count > 0)
                {
                    var ltTm = ltTrx.Where(x => x.CERROR == "907");
                    if (ltTm.Count() <= 0)
                    {
                        var lt1 = ltTrx.Where(x => x.CESTADO == "TERMIN");
                        if (ltTrx.Count() == lt1.Count())
                        {
                            var lt2 = ltTrx.Where(x => x.CERROR == "000");
                            if (ltTrx.Count() == lt2.Count())
                            {
                                obj.PROCESA = "1";
                                obj.CESTADO = "PENPRO";
                            }
                            else
                            {
                                var lt3 = ltTrx.Where(x => x.CERROR != "000");
                                obj.PROCESA = "0";
                                obj.CESTADO = "TERMIN";
                                obj.CERROR = lt3.First().CERROR;
                                obj.DERROR = lt3.First().DERROR;
                            }

                            obj.FACTUALIZACION = DateTime.Now;
                            obj.ActualizarActivaBloqueos(obj);
                        }
                    }
                }
                else
                {
                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + ", ERROR CONSULTANDO TRANSACCIONES:  " + error, null, "ERR");
                }

                //Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, obj.CPROCESO.Value.ToString().PadRight(10, ' ') + " " + obj.CPERSONA.Value.ToString().PadRight(10, ' ') + " " + obj.CESTADO.PadRight(10, ' '));
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return obj;
        }

        #endregion activa bloqueos

        #region efectivizacion cheques

        public void ProcesoEfectivizarCheques()
        {
            #region variables

            List<TBTHDETALLECHEQUES> ltRegistros = null;
            Task<TBTHDETALLECHEQUES>[] tareas = null;
            int numeroRegistros = 0;
            int numeroProcesos = 0;
            int veces = 0;
            int ultimo = 0;
            int desde = 0;
            int hasta = 0;
            int indice = 0;

            #endregion variables

            try
            {
                new TBTHDETALLECHEQUES().ActualizarEstado("PENPRO", "CARGAD");
                numeroRegistros = new TBTHDETALLECHEQUES().ContarPendientesProcesar();
                numeroProcesos = glbEfectivizarChequesProcesos;

                if (numeroProcesos > 0 && numeroRegistros > 0)
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS A PROCESAR: " + numeroRegistros);

                    Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);
                    for (int i = 1; i <= veces; i++)
                    {
                        desde = 1;
                        if (veces == i)
                        {
                            hasta = ultimo;
                        }
                        else
                        {
                            hasta = numeroProcesos;
                        }

                        ltRegistros = new TBTHDETALLECHEQUES().ListarPendientesProcesar(desde, hasta);
                        if (ltRegistros != null && ltRegistros.Count > 0)
                        {
                            indice = 0;
                            tareas = new Task<TBTHDETALLECHEQUES>[ltRegistros.Count];
                            foreach (TBTHDETALLECHEQUES det in ltRegistros)
                            {
                                tareas[indice] = new Task<TBTHDETALLECHEQUES>(() =>
                                {
                                    return EfectivizarCheque(det);
                                });
                                indice++;
                            }
                            foreach (Task t in tareas)
                            {
                                t.Start();
                            }
                            Task.WaitAll(tareas);
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "LISTADO REGISTROS A PROCESAR VACIO (#" + i + " de " + veces + ")");
                        }
                    }

                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "REGISTROS PROCESADOS: " + numeroRegistros);
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public TBTHDETALLECHEQUES EfectivizarCheque(TBTHDETALLECHEQUES obj)
        {
            TCUENTACHEQUESLOCALES cheque = null;
            wsS29.uciMethods ws = new wsS29.uciMethods();
            wsS29.Iso8583 iso = null;

            try
            {
                switch (obj.ACCION)
                {
                    case "":
                    case "CH DEV":
                        cheque = new TCUENTACHEQUESLOCALES().ConsultaCheque(obj.CUENTACHEQUE, Convert.ToInt32(obj.NUMEROCHEQUE), obj.CCUENTA);
                        if (cheque != null)
                        {
                            #region arma iso

                            iso = new wsS29.Iso8583();
                            iso.ISO_000_Message_Type = "1200";
                            if (string.IsNullOrEmpty(obj.ACCION.Trim()))
                            {
                                iso.ISO_003_ProcessingCode = "260010";
                            }
                            else
                            {
                                iso.ISO_003_ProcessingCode = "130010";
                            }
                            iso.ISO_004_AmountTransaction = cheque.VALORCHEQUE.Value;
                            iso.ISO_007_TransDatetime = DateTime.Now;
                            iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                            iso.ISO_012_LocalDatetime = DateTime.Now;
                            iso.ISO_015_SettlementDatel = DateTime.Today;
                            iso.ISO_018_MerchantType = "0001";
                            iso.ISO_024_NetworkId = "555551";
                            iso.ISO_102_AccountID_1 = cheque.CCUENTA;
                            iso.ISO_103_AccountID_2 = cheque.CUENTAGIRADA;
                            iso.ISO_120_ExtendedData = cheque.NUMEROCHEQUE.Value.ToString();
                            iso.ISO_121_ExtendedData = cheque.RUTATRANSITO;
                            iso.ISO_122_ExtendedData = cheque.PARTICION;
                            iso.ISO_123_ExtendedData = "20";
                            iso.ISO_124_ExtendedData = cheque.CPERSONA_COMPANIA.Value.ToString();

                            #endregion arma iso

                            try
                            {
                                iso = ws.ProcessingTransactionISO_WEB(iso);
                            }
                            catch (Exception ex)
                            {
                                obj.CESTADO = "TERMIN";
                                obj.CERROR = "907";
                                obj.DERROR = ex.Message.ToString();
                            }

                            if (iso.ISO_039_ResponseCode == "116" && iso.ISO_003_ProcessingCode == "130010")
                            {
                                iso.ISO_000_Message_Type = "1200";
                                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                                iso.ISO_034_PANExt = "30";
                                iso = ws.ProcessingTransactionISO_WEB(iso);
                                obj.PENDIENTECOMISION = "1";
                            }

                            obj.CSUCURSAL = cheque.CSUCURSAL;
                            obj.COFICINA = cheque.COFICINA;
                            obj.CESTADO = ReturnEstadoCheques(iso.ISO_039_ResponseCode);
                            obj.CERROR = iso.ISO_039_ResponseCode;
                            obj.DERROR = iso.ISO_039p_ResponseDetail;
                            obj.NUMEROMENSAJE = iso.ISO_044_AddRespData;
                        }
                        else
                        {
                            obj.CESTADO = "TERMIN";
                            obj.CERROR = "003";
                            obj.DERROR = "CHEQUE NO ENCONTRADO";
                        }
                        break;
                    default:
                        obj.CESTADO = "TERMIN";
                        obj.CERROR = "002";
                        obj.DERROR = obj.ACCION;
                        break;
                }

                obj.FMODIFICACION = DateTime.Now;
                obj.ActualizarProceso(obj);
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return obj;
        }

        private string ReturnEstadoCheques(string cerror)
        {
            string estado = string.Empty;
            switch (cerror)
            {
                case "000":
                    estado = "TERMIN";
                    break;
                case "907":
                case "CON001":
                case "CON003":
                case "BDD-00001":
                case "BDD-00060":
                case "BDD-01013":
                case "BDD-00028":
                case "BDD-17410":
                default:
                    estado = "TERMIN";
                    break;
            }
            return estado;
        }

        #endregion efectivizacion cheques

        #region proceso verifica

        public void ProcesoVerifica()
        {
            #region variables

            List<TBTHPROCESO> ltProceso = null;
            List<TBTHDETALLEPROCESO> ltRegistros = null;
            Task<TBTHDETALLEPROCESO>[] tareas = null;
            Int32 numeroRegistros = 0;
            Int32 numeroProcesos = 0;
            Int32 veces = 0;
            Int32 ultimo = 0;
            int desde = 0;
            int hasta = 0;
            int indice = 0;

            #endregion variables

            try
            {
                ltProceso = new TBTHPROCESO().ListarXEstado("PENPRO", false);
                if (ltProceso != null && ltProceso.Count > 0)
                {
                    foreach (TBTHPROCESO proceso in ltProceso)
                    {
                        numeroRegistros += new TBTHDETALLEPROCESO().ContarXEstado(proceso.FPROCESO, proceso.CPROCESO, "VERMOV");
                        numeroRegistros += new TBTHDETALLEPROCESO().ContarXEstado(proceso.FPROCESO, proceso.CPROCESO, "PROFIT");

                        if (numeroRegistros > 0)
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS: " + numeroRegistros);

                            #region segmentacionProcesos
                            numeroProcesos = BthProcesos.glbProcesosBdd;
                            Util.CalculaVeces(numeroProcesos, numeroRegistros, ref veces, ref ultimo);

                            desde = 1;
                            for (int i = 1; i <= veces; i++)
                            {
                                if (veces == i)
                                    hasta += ultimo;
                                else
                                    hasta += numeroProcesos;

                                ltRegistros = new TBTHDETALLEPROCESO().ListarPendientesVerificar(proceso.FPROCESO, proceso.CPROCESO, desde, hasta);
                                if (ltRegistros != null && ltRegistros.Count > 0)
                                {
                                    tareas = new Task<TBTHDETALLEPROCESO>[ltRegistros.Count];
                                    indice = 0;
                                    foreach (TBTHDETALLEPROCESO detalle in ltRegistros)
                                    {
                                        tareas[indice] = new Task<TBTHDETALLEPROCESO>(() =>
                                        {
                                            if (proceso.CTIPOPROCESO == "SPITAB" && detalle.CTIPOTRANSACCION != "INGBLQ")
                                                return VerificaLotes(proceso, detalle);
                                            else
                                                return VerificaNormal(proceso, detalle);
                                        });
                                        indice++;
                                    }
                                    foreach (Task t in tareas)
                                    {
                                        t.Start();
                                    }
                                    Task.WaitAll(tareas);
                                }

                                desde += numeroProcesos;
                            }
                            #endregion segmentacionProcesos

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " FIN PROCESO");
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " NO EXISTEN REGISTROS");
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private TBTHDETALLEPROCESO VerificaLotes(TBTHPROCESO proceso, TBTHDETALLEPROCESO obj)
        {
            string mensaje = string.Empty;
            string error = string.Empty;
            DateTime? fechalote = null;
            Int32? numerolote = null;
            bool actualizar = false;

            try
            {
                if (!string.IsNullOrEmpty(proceso.DATOSLOTE))
                {
                    string[] datoslote = proceso.DATOSLOTE.Split(';');
                    fechalote = Convert.ToDateTime(datoslote[0]);
                    numerolote = Convert.ToInt32(datoslote[1]);
                    mensaje = "%" + obj.CUENTA + "%" + obj.VALOR.Value.ToString().Replace(",", ".") + "%" + obj.RUBRO.Value.ToString() + "%" + glbFitbankSucursal + "%" + glbFitbankOficina + "%";
                    TLOTEMENSAJESDETALLE lote = new TLOTEMENSAJESDETALLE().ConsultaMensaje(fechalote, numerolote, mensaje);
                    if (lote != null)
                    {
                        if (!string.IsNullOrEmpty(lote.CODIGORESULTADO))
                        {
                            actualizar = true;
                            obj.CESTADO = "TERMIN";
                            obj.CERROR = lote.CODIGORESULTADO == "0" ? "000" : lote.CODIGORESULTADO;
                            obj.DERROR = lote.TEXTOERROR;
                            obj.NUMEROMENSAJE = lote.NUMEROMENSAJE;

                            if (lote.CODIGORESULTADO == "0")
                            {
                                TMOVIMIENTOS mov = new TMOVIMIENTOS().ListarMovimientosProcesoLotes(proceso.FCARGA, obj.CUENTA, obj.VALOR);
                                if (mov != null)
                                {
                                    actualizar = new TBTHDETALLECUADRE().InsertarCuadre(obj, mov.VALORMONEDACUENTA);
                                }
                                else
                                {
                                    actualizar = false;
                                }
                            }
                        }
                        else
                        {
                            actualizar = false;
                        }
                    }
                    else
                    {
                        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + "\t" + "MENSAJE NO ENCONTRADO: " + fechalote + ";" + numerolote + ";" + mensaje, null, "ERR");
                        actualizar = false;
                    }

                    if (actualizar)
                    {
                        obj.FACTUALIZACION = DateTime.Now;
                        new TBTHDETALLEPROCESO().Actualizar(obj);
                    }

                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, obj.CPROCESO.Value.ToString().PadRight(10, ' ') + " " + obj.SECUENCIA.ToString().PadRight(10, ' ') + " " + obj.CESTADO.PadRight(10, ' ') + " " + error);
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return obj;
        }

        private TBTHDETALLEPROCESO VerificaNormal(TBTHPROCESO proceso, TBTHDETALLEPROCESO obj)
        {
            string error = string.Empty;
            bool flag = false;
            Int32 timeoutTrx;

            try
            {
                if (obj.CESTADO == "VERMOV" && ValidaError(obj.CERROR, obj.CTIPOTRANSACCION))
                    timeoutTrx = 0;
                else
                    timeoutTrx = BthProcesos.glbTimeoutTrx;

                TimeSpan duracion = DateTime.Now - obj.FACTUALIZACION.Value;
                TimeSpan timeout = new TimeSpan(0, 0, 0, 0, timeoutTrx);
                if (duracion.TotalSeconds >= timeout.TotalSeconds)
                {
                    switch (obj.CTIPOTRANSACCION)
                    {
                        case "INGBLQ":
                        case "LEVBLQ":
                        case "AUTCOM":
                        case "CAMEST":
                        case "REVERSO":
                            flag = ValidacionExcepcion(ref obj);
                            break;
                        case "NOTDEBPAR":
                        case "NOTDEBREC":
                            flag = ValidacionRecuperacion(proceso, ref obj);
                            break;
                        case "NOTDEBMTJ":
                            flag = ValidacionMantenimientoTarjetas(ref obj);
                            break;
                        default:
                            flag = ValidacionNormal(ref obj);
                            break;
                    }
                }

                if (flag)
                {
                    obj.FACTUALIZACION = DateTime.Now;
                    new TBTHDETALLEPROCESO().Actualizar(obj);
                }

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, obj.CPROCESO.Value.ToString().PadRight(10, ' ') + " " + obj.SECUENCIA.ToString().PadRight(10, ' ') + " " + obj.CESTADO.PadRight(10, ' ') + " " + error);
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return obj;
        }

        private bool ValidacionNormal(ref TBTHDETALLEPROCESO obj)
        {
            List<TMOVIMIENTOS> ltMovimientos = null;
            string error = string.Empty;
            bool flag = false;

            try
            {
                ltMovimientos = new TMOVIMIENTOS().ListarMovimientosProceso(obj.FPROCESO, obj.CUENTA, obj.CPROCESO.Value.ToString(), obj.SECUENCIA.Value.ToString(), obj.VALOR);
                if (ltMovimientos == null || ltMovimientos.Count == 0)
                {
                    flag = true;
                    obj.CESTADO = "PENPRO";
                    obj.CERROR = string.Empty;
                    obj.DERROR = string.Empty;
                }
                else
                {
                    flag = true;
                    obj.CESTADO = "TERMIN";
                    obj.CERROR = "000";
                    obj.DERROR = "TRANSACCION REALIZADA CORRECTAMENTE";
                    obj.NUMEROMENSAJE = ltMovimientos.OrderBy(x => x.NUMEROMENSAJE).First().NUMEROMENSAJE;
                    new TBTHDETALLECUADRE().InsertarCuadre(obj, ltMovimientos.Sum(x => x.VALORMONEDACUENTA));
                }
            }
            catch (Exception ex)
            {
                flag = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return flag;
        }

        private bool ValidacionExcepcion(ref TBTHDETALLEPROCESO obj)
        {
            bool flag = true;
            obj.CESTADO = "TERMIN";
            return flag;
        }

        private bool ValidacionRecuperacion(TBTHPROCESO proceso, ref TBTHDETALLEPROCESO obj)
        {
            string error = string.Empty;
            bool flag = false;

            try
            {
                if (obj.CERROR == "000")
                {
                    switch (obj.CTIPOTRANSACCION)
                    {
                        case "NOTDEBPAR":
                            if (proceso.CTIPOPROCESO != "RCPSIF")
                            {
                                obj.VALORPENDIENTE = obj.VALORPENDIENTE - obj.VALORMOVIMIENTO;

                                if (obj.VALORPENDIENTE <= 0)
                                    obj.CESTADO = "TERMIN";
                                else
                                    obj.CESTADO = "PENPRO";

                                TBTHDETALLEDEBITO debito = new TBTHDETALLEDEBITO();
                                debito.FPROCESO = obj.FPROCESO;
                                debito.CPROCESO = obj.CPROCESO;
                                debito.SECUENCIA = obj.SECUENCIA;
                                debito.FDEBITO = DateTime.Now;
                                debito.VALOR = obj.VALORMOVIMIENTO;
                                debito.NUMEROMENSAJE = obj.NUMEROMENSAJE;
                                if (!debito.Insertar(debito))
                                {
                                    obj.CESTADO = "ERRREG";
                                    obj.CERROR = "909";
                                    obj.DERROR = "ERROR INSERTANDO DETALLE DEBITO, VALOR DEBITADO: " + obj.VALORMOVIMIENTO;
                                }

                                obj.VALORMOVIMIENTO = 0;
                            }
                            else
                            {
                                obj.CESTADO = "TERMIN";
                            }
                            break;
                        case "NOTDEBREC":
                            obj.CESTADO = "TERMIN";
                            obj.VALORPENDIENTE = 0;
                            break;
                    }
                }
                else
                {
                    if (ValidaError(obj.CERROR, obj.CTIPOTRANSACCION))
                        obj.CESTADO = "TERMIN";
                    else
                        obj.CESTADO = "PENPRO";
                }

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return flag;
        }

        private bool ValidacionMantenimientoTarjetas(ref TBTHDETALLEPROCESO obj)
        {
            List<TMOVIMIENTOS> ltMovimientos = null;
            string error = string.Empty;
            bool flag = false;

            //try
            //{
            //    ltMovimientos = new TMOVIMIENTOS().ListarMovimientosProceso(obj.FPROCESO, obj.CUENTA, obj.CPROCESO.Value.ToString(), obj.SECUENCIA.Value.ToString(), obj.VALOR, out error);
            //    if (error == "OK")
            //    {
            //        if (ltMovimientos.Count > 0)
            //        {
            //            flag = true;
            //            obj.CESTADO = "TER";
            //            obj.VALORPENDIENTE = 0;
            //            new TBTHPROCESOCUADRE().InsertarCuadre(obj, ltMovimientos.Sum(x => x.VALORMONEDACUENTA), out error);
            //        }
            //        else
            //        {
            //            flag = true;
            //            obj.CESTADO = "PEN";
            //        }
            //    }
            //    else
            //    {
            //        flag = false;
            //        BatchGlobal.ImprimePantalla("[VER] " + "ERROR CONSULTANDO MOVIMIENTOS PROCESO: " + obj.CPROCESO + "; SECUENCIA: " + obj.SECUENCIA);
            //        BatchGlobal.EscribirLog("ERROR CONSULTANDO MOVIMIENTOS PROCESO: " + obj.CPROCESO + "; SECUENCIA: " + obj.SECUENCIA + ": " + error, null, "ERR");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    flag = false;
            //    BatchGlobal.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            //}

            return flag;
        }

        #endregion proceso verifica

        #region finaliza procesos

        public void ProcesoFinalizaProcesos()
        {
            List<TBTHPROCESO> ltProcesos = null;
            Int32 registrosTotal = 0;
            Int32 registrosTerminados = 0;
            bool actualizar = false;

            try
            {
                ltProcesos = new TBTHPROCESO().ListarPendientesProceso();
                if (ltProcesos != null && ltProcesos.Count > 0)
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "INICIA");

                    foreach (TBTHPROCESO proceso in ltProcesos)
                    {
                        switch (proceso.CTIPOPROCESO)
                        {
                            case "EFECHE":
                                registrosTotal = new TBTHDETALLECHEQUES().ContarRegistrosProceso(proceso);
                                registrosTerminados = new TBTHDETALLECHEQUES().ContarRegistrosTerminados(proceso);
                                if (registrosTerminados == registrosTotal)
                                {
                                    actualizar = true;
                                    proceso.CESTADO = "TERMIN";
                                }
                                break;
                            case "SPITAB":
                                if (VerificaTotales(proceso))
                                {
                                    if (VerificaConfirmados(proceso))
                                    {
                                        if (InsertarBloqueos(proceso))
                                        {
                                            if (VerificaAfectaciones(proceso))
                                            {
                                                actualizar = true;
                                                proceso.CESTADO = "TERMIN";
                                            }
                                        }
                                    }
                                }
                                break;
                            case "RCPSIF":
                                if (VerificaEjecucion(proceso))
                                {
                                    if (ActualizaSifco(proceso))
                                    {
                                        actualizar = true;
                                        proceso.CESTADO = "TERMIN";
                                    }
                                }
                                break;
                            default:
                                if (VerificaEjecucion(proceso))
                                {
                                    actualizar = true;
                                    proceso.CESTADO = "TERMIN";
                                }
                                break;
                        }

                        if (actualizar)
                        {
                            proceso.FMODIFICACION = DateTime.Now;
                            proceso.Actualizar(proceso);
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " FINALIZADO");
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " PENDIENTE");
                        }
                    }

                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "FINALIZA");
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        private bool VerificaTotales(TBTHPROCESO proceso)
        {
            bool resp = true;
            Int32 registrosProcesados = 0;
            Int32 registrosTotales = 0;
            decimal valorTotal = 0;

            try
            {
                new TBTHDETALLETABULADO().TotalesProceso(proceso.FPROCESO, proceso.CPROCESO, out registrosTotales, out valorTotal);
                registrosProcesados = new TBTHDETALLETABULADO().ContarFinalizados(proceso.FPROCESO, proceso.CPROCESO);
                if (registrosProcesados == registrosTotales && registrosProcesados > 0 && registrosTotales > 0)
                {
                    if (proceso.CESTADO != "VALTER" && proceso.CESTADO != "ERRPRO" && proceso.CESTADO != "PENPRO")
                    {
                        proceso.CESTADO = "VALTER";
                        proceso.FMODIFICACION = DateTime.Now;
                        resp = proceso.Actualizar(proceso);
                    }
                    else
                    {
                        resp = true;
                    }
                }
                else
                {
                    resp = false;
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        private bool VerificaConfirmados(TBTHPROCESO proceso)
        {
            bool resp = true;
            Int32 registrosAcreditacion = 0;
            Int32 registrosConfirmados = 0;

            try
            {
                registrosConfirmados = new TBTHDETALLETABULADO().ContarConfirmados(proceso.FPROCESO, proceso.CPROCESO);
                registrosAcreditacion = new TBTHDETALLEPROCESO().ContarAcreditacion(proceso.FPROCESO, proceso.CPROCESO);
                if (registrosConfirmados != registrosAcreditacion)
                {
                    TBTHDETALLEPROCESO detBatch = new TBTHDETALLEPROCESO();
                    detBatch.FPROCESO = proceso.FPROCESO;
                    detBatch.CPROCESO = proceso.CPROCESO;
                    detBatch.CTIPOTRANSACCION = "NOTCRE";
                    detBatch.CUSUARIO = proceso.CUSUARIO;
                    detBatch.CSUCURSAL = 1;
                    detBatch.COFICINA = 13;
                    detBatch.CESTADO = "CARGAD";
                    detBatch.FCARGA = DateTime.Now;
                    resp = detBatch.InsertarTabuladoFaltantes(detBatch);
                }
                else
                {
                    resp = true;
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        private bool InsertarBloqueos(TBTHPROCESO proceso)
        {
            bool resp = true;

            try
            {
                Int32 secuencia = new TBTHDETALLETABULADO().SecuenciaProceso(proceso.FPROCESO, proceso.CPROCESO);
                if (secuencia > 0)
                {
                    if (new TBTHDETALLEPROCESO().InsertarTabuladoBloqueos(proceso.FPROCESO, proceso.CPROCESO, secuencia))
                    {
                        resp = true;
                        new TBTHTEMP().Borra(proceso.FPROCESO, proceso.CPROCESO);
                    }
                    else
                    {
                        resp = false;
                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " ERROR INSERTANDO BLOQUEOS");
                    }
                }
                else
                {
                    resp = false;
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " ERROR CONSULTANDO SECUENCIA");
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        private bool VerificaAfectaciones(TBTHPROCESO proceso)
        {
            bool resp = true;
            Int32 registrosProcesados = 0;
            Int32 registrosAcreditacion = 0;
            Int32 registrosTotales = 0;
            Int32 registrosConfirmados = 0;

            try
            {
                registrosTotales = new TBTHDETALLEPROCESO().ContarTotal(proceso.FPROCESO, proceso.CPROCESO);
                registrosAcreditacion = new TBTHDETALLEPROCESO().ContarTotal(proceso.FPROCESO, proceso.CPROCESO, "NOTCRE");
                registrosProcesados = new TBTHDETALLEPROCESO().ContarFinalizados(proceso.FPROCESO, proceso.CPROCESO);
                registrosConfirmados = new TBTHDETALLETABULADO().ContarConfirmados(proceso.FPROCESO, proceso.CPROCESO);

                if (registrosProcesados == registrosTotales && registrosAcreditacion == registrosConfirmados)
                {
                    resp = true;
                }
                else
                {
                    resp = false;
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        private bool VerificaEjecucion(TBTHPROCESO proceso)
        {
            bool resp = true;
            Int32 registrosTotales = 0;
            Int32 registrosProcesados = 0;

            try
            {
                registrosTotales = new TBTHDETALLEPROCESO().ContarTotal(proceso.FPROCESO, proceso.CPROCESO);
                registrosProcesados = new TBTHDETALLEPROCESO().ContarFinalizados(proceso.FPROCESO, proceso.CPROCESO);

                if (registrosProcesados == registrosTotales)
                {
                    resp = true;
                }
                else
                {
                    resp = false;
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, proceso.CPROCESO + " REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        private bool ActualizaSifco(TBTHPROCESO proceso)
        {
            bool resp = true;
            List<TBTHDETALLEPROCESO> ltRegistros = null;
            decimal valorCobrado = 0;
            DateTime fproceso = DateTime.Today;

            try
            {
                ltRegistros = new TBTHDETALLEPROCESO().ListarPendientes(proceso.CPROCESO.Value);
                if (ltRegistros != null)
                {
                    foreach (TBTHDETALLEPROCESO registro in ltRegistros)
                    {
                        try { fproceso = Convert.ToDateTime(registro.PARAMETROS.Trim()); }
                        catch { fproceso = DateTime.Today; }

                        try { valorCobrado = registro.VALORMOVIMIENTO.Value; }
                        catch { valorCobrado = 0; }

                        BddAuxiliar aux = new BddAuxiliar();
                        if (!aux.ActualizarCuotaSifco(fproceso, registro.CUENTA, registro.REFERENCIA, registro.DERROR, registro.CERROR, valorCobrado))
                        {
                            resp = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp = false;
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            return resp;
        }

        #endregion finaliza procesos

        private bool ValidaError(string cerror, string transaccion)
        {
            bool flag = false;

            switch (cerror)
            {
                case "000":
                case "DVI001":
                case "DVI011":
                case "DVI122":
                case "DVI123":
                case "DVI124":
                case "DVI216":
                    flag = true;
                    break;
                default:
                    flag = false;
                    break;
            }

            if (cerror == "DVI004" && transaccion == "NOTDEBREC")
                flag = true;

            return flag;
        }

        private string ReturnEstado(string cerror)
        {
            string estado = string.Empty;
            switch (cerror)
            {
                case "000":
                case "907":
                case "CON001":
                case "CON003":
                case "BDD-00001":
                case "BDD-00060":
                case "BDD-01013":
                case "BDD-00028":
                case "BDD-17410":
                    estado = "VERMOV";
                    break;
                default:
                    estado = "TERMIN";
                    break;
            }
            return estado;
        }
    }
}

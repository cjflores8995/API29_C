using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Business
{
    public class Facturacion
    {
        public static Int32 verificaTime = 0;

        public static Int32 publicaTime = 0;
        public static Int32 publicaProcess = 0;

        public static Int32 timeoutWS = 0;

        public static string ftpParametros = string.Empty;
        public static string ftpPathPdf = string.Empty;
        public static string ftpPathXml = string.Empty;
        public static string ftpPathProcom = string.Empty;

        public static string ftpLocal = string.Empty;

        public static string localPath = string.Empty;
        public static string criterios = string.Empty;

        public static bool notifica = false;

        public static int batchTime = 0;

        private delegate void dlLogging(string input, Exception ex, string type);

        public static bool CargarParametros()
        {
            bool flag = true;

            try
            {
                timeoutWS = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutWS"]);

                verificaTime = Convert.ToInt32(ConfigurationManager.AppSettings["verificaTime"]);

                publicaTime = Convert.ToInt32(ConfigurationManager.AppSettings["publicaTime"]);
                publicaProcess = Convert.ToInt32(ConfigurationManager.AppSettings["publicaProcess"]);

                ftpParametros = ConfigurationManager.AppSettings["ftpParametros"].ToString().Trim();

                ftpPathPdf = ConfigurationManager.AppSettings["ftpPathPdf"].ToString().Trim();
                ftpPathXml = ConfigurationManager.AppSettings["ftpPathXml"].ToString().Trim();
                ftpPathProcom = ConfigurationManager.AppSettings["ftpPathProcom"].ToString().Trim();

                ftpLocal = ConfigurationManager.AppSettings["ftpLocal"].ToString().Trim();

                batchTime = int.Parse(ConfigurationManager.AppSettings["batchTime"].ToString().Trim());

                localPath = ConfigurationManager.AppSettings["localPath"].ToString().Trim();
                notifica = Convert.ToBoolean(ConfigurationManager.AppSettings["notifica"].ToString().Trim());

                criterios = ConfigurationManager.AppSettings["criterios"].ToString().Trim();
            }
            catch (Exception ex)
            {
                flag = false;
                Util.ImprimePantalla(ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        public void ProcesoFacturacion()
        {
            List<VFECOMPROBANTES> ltRegistros = new List<VFECOMPROBANTES>();
            Task<VFECOMPROBANTES>[] tareas = null;
            string error = string.Empty;
            int desde = 0;
            int hasta = 0;
            int indice = 0;

            try
            {
                desde = 1;
                hasta = publicaProcess;

                ltRegistros = new VFECOMPROBANTES().ListarPendientes(desde, hasta, criterios);
                if (ltRegistros != null && ltRegistros.Count > 0)
                {
                    Util.ImprimePantalla("[FCT] REGISTROS A PROCESAR: " + ltRegistros.Count);

                    tareas = new Task<VFECOMPROBANTES>[ltRegistros.Count];
                    indice = 0;

                    foreach (VFECOMPROBANTES det in ltRegistros)
                    {
                        tareas[indice] = new Task<VFECOMPROBANTES>(() =>
                        {
                            return ProcesaComprobantes(det);
                            //return ProcesaComprobantesLocal(det);
                        });
                        indice++;
                    }
                    foreach (Task t in tareas)
                    {
                        t.Start();
                    }
                    Task.WaitAll(tareas);

                    Util.ImprimePantalla("[FCT] REGISTROS PROCESADOS: " + ltRegistros.Count);
                }
                else
                {
                    Util.ImprimePantalla("[FCT] NO EXISTEN REGISTROS PENDIENTES");
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla("[FCT] " + ex.Message.ToString());
            }
        }

        private VFECOMPROBANTES ProcesaComprobantes(VFECOMPROBANTES obj)
        {
            dlLogging logging = null;
            string pathFtp = string.Empty;
            string archivoFtp = string.Empty;

            string pathLocal = string.Empty;
            string archivoLocalPdf = string.Empty;
            string errorPdf = string.Empty;

            string archivoLocalXml = string.Empty;
            string errorXml = string.Empty;

            string errorNotifica = string.Empty;

            string nombre = string.Empty;
            string email = string.Empty;

            try
            {
                pathLocal = string.Format(localPath, obj.FEMISION.Value.ToString("yyyy"), obj.FEMISION.Value.ToString("MM"), obj.FEMISION.Value.ToString("dd"));
                if (!Directory.Exists(pathLocal))
                {
                    Directory.CreateDirectory(pathLocal);
                }

                archivoLocalPdf = obj.TIPODOCUMENTO + "_" + obj.NUMERODOCUMENTO + ".pdf";
                archivoLocalXml = obj.TIPODOCUMENTO + "_" + obj.NUMERODOCUMENTO + ".xml";

                TextInfo txtInfo = new CultureInfo("en-US", false).TextInfo;

                switch (obj.ORIGEN)
                {
                    case "FE":
                        #region extrae pdf

                        archivoFtp = obj.NUMEROAUTORIZACION + ".pdf";
                        pathFtp = ftpPathPdf + archivoFtp;
                        Util.ExtraerSftp(ftpParametros, pathFtp, (pathLocal + archivoLocalPdf), out errorPdf);

                        #endregion extrae pdf

                        #region extrae xml

                        archivoFtp = obj.TIPODOCUMENTO + obj.CLAVEACCESO + obj.VERIFICADOR;
                        pathFtp = ftpPathXml + archivoFtp;
                        Util.ExtraerSftp(ftpParametros, pathFtp, (pathLocal + archivoLocalXml), out errorXml);

                        #endregion extrae xml
                        break;
                    case "PR":
                        #region extrae pdf

                        archivoFtp = txtInfo.ToTitleCase(obj.TIPODOCUMENTO.ToLower()) + obj.CLAVEACCESO + ".PDF";
                        pathFtp = ftpPathProcom + archivoFtp;
                        Util.ExtraerSftp(ftpParametros, pathFtp, (pathLocal + archivoLocalPdf), out errorPdf);

                        #endregion extrae pdf

                        #region extrae xml

                        archivoFtp = txtInfo.ToTitleCase(obj.TIPODOCUMENTO.ToLower()) + obj.CLAVEACCESO + ".xml";
                        pathFtp = ftpPathProcom + archivoFtp;
                        Util.ExtraerSftp(ftpParametros, pathFtp, (pathLocal + archivoLocalXml), out errorXml);

                        #endregion extrae xml
                        break;
                    default:
                        break;
                }

                if (notifica && errorPdf == "OK" && errorXml == "OK")
                {
                    #region notificacion

                    VNOTIFICACIONPERSONADATOS datosNotifica = new VNOTIFICACIONPERSONADATOS().ListarPendientes(obj.IDENTIFICACION);
                    if (datosNotifica != null)
                    {
                        nombre = datosNotifica.NOMBRELEGAL;
                        email = datosNotifica.CORREO;
                    }
                    else
                    {
                        nombre = obj.NOMBRE;
                        email = obj.EMAIL;
                    }

                    email = "rmorales@29deoctubre.fin.ec";

                    if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(email))
                    {
                        wsS29.Iso8583 iso = new wsS29.Iso8583();
                        iso.ISO_000_Message_Type = "1200";
                        iso.ISO_002_PAN = "FTELCMP001";
                        iso.ISO_003_ProcessingCode = "960000";
                        iso.ISO_007_TransDatetime = DateTime.Now;
                        iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                        iso.ISO_012_LocalDatetime = DateTime.Now;
                        iso.ISO_015_SettlementDatel = DateTime.Now;
                        iso.ISO_018_MerchantType = "0004";
                        iso.ISO_024_NetworkId = "555551";

                        csEstructuraNotificacion.notificacion objNotifica = new csEstructuraNotificacion.notificacion();
                        csEstructuraNotificacion.mensaje objMensaje = new csEstructuraNotificacion.mensaje();
                        objMensaje.to = email;
                        objMensaje.parametros =
                            nombre + ";" +
                            obj.TIPODOCUMENTO + ";" +
                            obj.FEMISION.Value.ToString("dd/MM/yyyy") + ";" +
                            obj.NUMERODOCUMENTO;

                        csEstructuraNotificacion.archivo[] objAdjunto = new csEstructuraNotificacion.archivo[2];

                        if (errorXml == "OK")
                        {
                            objAdjunto[0] = new csEstructuraNotificacion.archivo();
                            objAdjunto[0].nombre = archivoLocalXml;
                            objAdjunto[0].contenido = Util.ArchivoToBase64String(pathLocal, archivoLocalXml);
                        }

                        if (errorPdf == "OK")
                        {
                            objAdjunto[1] = new csEstructuraNotificacion.archivo();
                            objAdjunto[1].nombre = archivoLocalPdf;
                            objAdjunto[1].contenido = Util.ArchivoToBase64String(pathLocal, archivoLocalPdf);
                        }

                        objNotifica.items = new object[] { objMensaje, objAdjunto };

                        iso.ISO_114_ExtendedData = Util.ObjToString(objNotifica);

                        try
                        {
                            wsS29.uciMethods ws = new wsS29.uciMethods();
                            ws.Timeout = timeoutWS;
                            iso = ws.ProcessingTransactionISO_WEB(iso);
                            if (iso.ISO_039_ResponseCode == "000")
                            {
                                errorNotifica = "OK";
                            }
                            else
                            {
                                errorNotifica = iso.ISO_039_ResponseCode + " - " + iso.ISO_039p_ResponseDetail;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorNotifica = ex.Message.ToString().ToUpper();
                        }
                    }
                    else
                    {
                        errorNotifica = "NO EXISTEN DATOS PARA NOTIFICACION";
                    }

                    #endregion notificacion
                }
                else
                {
                    errorNotifica = "OK";
                }

                #region guarda comprobante

                if (errorPdf == "OK" && errorXml == "OK" && errorNotifica == "OK")
                {
                    TBTHFACTURACION registro = new TBTHFACTURACION();
                    registro.CTIPODOCUMENTO = obj.CTIPODOCUMENTO;
                    registro.NUMERODOCUMENTO = obj.NUMERODOCUMENTO;
                    registro.FPROCESO = DateTime.Now;
                    registro.ERRORPDF = errorPdf;
                    registro.ERRORXML = errorXml;
                    registro.ERRORNOTIFICACION = errorNotifica;
                    if (!registro.Insertar(registro))
                    {
                        Logging.EscribirLog("INSERT COMPROBANTE: " + obj.CTIPODOCUMENTO + "; " + obj.NUMERODOCUMENTO, null, "ERR");
                    }

                    Util.ImprimePantalla("[FCT] " + obj.CTIPODOCUMENTO + " " + obj.NUMERODOCUMENTO + " => OK");
                }
                else
                {
                    Logging.EscribirLog("COMPROBANTE: " + obj.CTIPODOCUMENTO + "; " + obj.NUMERODOCUMENTO + "\n PDF: " + errorPdf + "\n XML: " + errorXml + "\n NTF: " + errorNotifica, null, "ERR");
                    Util.ImprimePantalla("[FCT] " + obj.CTIPODOCUMENTO + " " + obj.NUMERODOCUMENTO + " => ERROR");
                }

                #endregion guarda comprobante
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla("[FCT] " + obj.CTIPODOCUMENTO + " " + obj.NUMERODOCUMENTO + " => ERROR GENERAL");
                logging = new dlLogging(Logging.EscribirLog);
                logging.BeginInvoke(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR", null, null);
            }

            return obj;
        }

        private VFECOMPROBANTES ProcesaComprobantesLocal(VFECOMPROBANTES obj)
        {
            #region variables

            dlLogging logging = null;
            string pathFtp = string.Empty;
            string archivoFtp = string.Empty;

            string pathLocalPdf = string.Empty;
            string archivoLocalPdf = string.Empty;
            string errorPdf = string.Empty;

            string pathLocalXml = string.Empty;
            string archivoLocalXml = string.Empty;
            string errorXml = string.Empty;

            string errorNotifica = string.Empty;

            string nombre = string.Empty;
            string email = string.Empty;

            #endregion variables

            try
            {
                #region extrae pdf

                archivoLocalPdf = obj.TIPODOCUMENTO + "_" + obj.NUMERODOCUMENTO + ".pdf";
                pathLocalPdf = string.Format(localPath, obj.FEMISION.Value.ToString("yyyy"), obj.FEMISION.Value.ToString("MM"), obj.FEMISION.Value.ToString("dd"));
                if (!Directory.Exists(pathLocalPdf))
                {
                    Directory.CreateDirectory(pathLocalPdf);
                }

                archivoFtp = obj.NUMEROAUTORIZACION + ".pdf";
                pathFtp = ftpLocal + @"pdf\" + archivoFtp;

                try
                {
                    File.Move(pathFtp, (pathLocalPdf + archivoLocalPdf));
                    errorPdf = "OK";
                }
                catch (Exception ex)
                {
                    errorPdf = ex.Message.ToString();
                }

                #endregion extrae pdf

                #region extrae xml

                archivoLocalXml = obj.TIPODOCUMENTO + "_" + obj.NUMERODOCUMENTO + ".xml";
                pathLocalXml = string.Format(localPath, obj.FEMISION.Value.ToString("yyyy"), obj.FEMISION.Value.ToString("MM"), obj.FEMISION.Value.ToString("dd"));
                if (!Directory.Exists(pathLocalXml))
                {
                    Directory.CreateDirectory(pathLocalXml);
                }

                archivoFtp = obj.TIPODOCUMENTO + obj.CLAVEACCESO + obj.VERIFICADOR + ".xml";
                pathFtp = ftpLocal + @"xml\" + archivoFtp;

                try
                {
                    File.Move(pathFtp, (pathLocalXml + archivoLocalXml));
                    errorXml = "OK";
                }
                catch (Exception ex)
                {
                    errorXml = ex.Message.ToString();
                }

                #endregion extrae xml

                errorNotifica = "OK";

                #region guarda comprobante

                TBTHFACTURACION registro = new TBTHFACTURACION();
                registro.CTIPODOCUMENTO = obj.CTIPODOCUMENTO;
                registro.NUMERODOCUMENTO = obj.NUMERODOCUMENTO;
                registro.FPROCESO = DateTime.Now;
                registro.ERRORPDF = errorPdf;
                registro.ERRORXML = errorXml;
                registro.ERRORNOTIFICACION = errorNotifica;
                if (!registro.Insertar(registro))
                {
                    Logging.EscribirLog("INSERT COMPROBANTE: " + obj.CTIPODOCUMENTO + "; " + obj.NUMERODOCUMENTO, null, "ERR");
                }

                Util.ImprimePantalla("[FCT] " + obj.CTIPODOCUMENTO + " " + obj.NUMERODOCUMENTO + " => OK");

                #endregion guarda comprobante
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla("[FCT] " + obj.CTIPODOCUMENTO + " " + obj.NUMERODOCUMENTO + " => ERROR GENERAL");
                logging = new dlLogging(Logging.EscribirLog);
                logging.BeginInvoke(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR", null, null);
            }

            return obj;
        }
    }
}

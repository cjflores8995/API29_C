using Ionic.Zip;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tamir.SharpSsh;
using btch = Business.Logic.BthPos;

namespace Business
{

    public class BthPos
    {
        #region variables globales

        public static string glbParametrosFtp = string.Empty;
        public static string glbPathCargadosFtp = string.Empty;
        public static string glbPathCargados = string.Empty;
        public static string glbPathOrigen = string.Empty;
        public static string glbPathResultado = string.Empty;

        //public static bool glbForzarCierre = false;
        //public static Int32 glbDiasCierre = 0;
        public static Int32 glbTimeoutWS = 0;

        public static bool glbExtraccionActiva = false;
        public static Int32 glbExtraccionTiempo = 0;

        public static bool glbLecturaActiva = false;
        public static Int32 glbLecturaTiempo = 0;

        public static bool glbCompensaActiva = false;
        public static Int32 glbCompensaTiempo = 0;

        public static bool glbAutorizaActiva = false;
        public static Int32 glbAutorizaTiempo = 0;

        public static Decimal glbPorcentajeIva = 0;

        public static string glbCopiaMail = string.Empty;

        #endregion variables globales

        #region carga parametros globales

        public BthPos() { }

        public static bool CargaParametros(out string error)
        {
            bool response = true;
            error = "OK";

            try
            {
                glbExtraccionActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["extraccionActiva"]);
                glbExtraccionTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["extraccionTiempo"]);

                glbLecturaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["lecturaActiva"]);
                glbLecturaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["lecturaTiempo"]);

                glbCompensaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["compensaActiva"]);
                glbCompensaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["compensaTiempo"]);

                glbAutorizaActiva = Convert.ToBoolean(ConfigurationManager.AppSettings["autorizaActiva"]);
                glbAutorizaTiempo = Convert.ToInt32(ConfigurationManager.AppSettings["autorizaTiempo"]);

                glbParametrosFtp = ConfigurationManager.AppSettings["ftp"];

                glbPathCargadosFtp = ConfigurationManager.AppSettings["pathCargadosFtp"];
                glbPathCargados = ConfigurationManager.AppSettings["pathCargados"];
                glbPathOrigen = ConfigurationManager.AppSettings["pathOrigen"];
                glbPathResultado = ConfigurationManager.AppSettings["pathResultado"];

                //glbForzarCierre = Convert.ToBoolean(ConfigurationManager.AppSettings["forzarCierre"]);
                //glbDiasCierre = Convert.ToInt32(ConfigurationManager.AppSettings["diasCierre"]);
                glbTimeoutWS = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutWS"]);
                glbCopiaMail = ConfigurationManager.AppSettings["copiaMail"];

                glbPorcentajeIva = new BddAuxiliar().GetIva();
                if (glbPorcentajeIva <= 0)
                {
                    response = false;
                    error = "";
                }
            }
            catch (Exception ex)
            {
                response = false;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return response;
        }

        #endregion carga parametros globales

        #region extraccion archivos

        public void Extraccion()
        {
            List<string> paths = null;
            List<string> directorios = null;
            List<string> archivos = null;
            string subcarpeta = string.Empty;
            string error = string.Empty;
            bool flag = true;

            try
            {
                btch.ExtractFiles extractFiles_ = new btch.ExtractFiles();

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "INICIANDO EXTRACCIÓN DE ARCHIVOS...");

                #region descarga ftp

                directorios = Util.ListarArchivosFtp(glbParametrosFtp, glbPathCargadosFtp, out error);

                if (directorios != null && directorios.Count > 0)
                {
                    foreach (string dir in directorios)
                    {
                        archivos = Util.ListarArchivosFtp(glbParametrosFtp, glbPathCargadosFtp + dir, out error);
                        if (archivos != null && archivos.Count > 0)
                        {
                            foreach (string arc in archivos)
                            {
                                string pathArchivoFtp = glbPathCargadosFtp + dir + "/" + arc;
                                string pathArchivoLocal = glbPathCargados + dir + @"\" + arc;
                                if (Util.ExtraerFtp(glbParametrosFtp, pathArchivoFtp, pathArchivoLocal, out error))
                                {
                                    Util.BorrarFtp(glbParametrosFtp, pathArchivoFtp, out error);
                                }
                            }
                        }
                    }
                }

                #endregion descarga ftp

                #region descompresion

                paths = new List<string>(Directory.EnumerateDirectories(glbPathCargados));

                foreach (var path in paths)
                {
                    subcarpeta = path.Substring(path.LastIndexOf("\\") + 1);
                    switch (subcarpeta)
                    {
                        case "datafast":
                            extractFiles_.ExtractDatafastFiles(path, flag, error, glbPathOrigen, subcarpeta);
                            break;

                        case "favorita":
                            extractFiles_.ExtractLafavoritaFiles(path, flag, error, glbPathOrigen, subcarpeta);
                            break;

                        case "farcomed":
                            extractFiles_.ExtractFarcomedFiles(path, flag, error, glbPathOrigen, subcarpeta);
                            break;

                        case "econofarm":
                            extractFiles_.ExtractEconofarmFiles(path, flag, error, glbPathOrigen, subcarpeta);
                            break;

                        default:
                            break;
                    }
                }

                #endregion descompresion

                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "EXTRACCIÓN DE ARCHIVOS FINALIZADA.");
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        #endregion extraccion archivos

        #region lectura

        public void Lectura()
        {
            List<VPOSCOMPENSACABECERA> ltCabecera = null;
            List<TPOSCOMPENSADETALLE> ltDetalle = new List<TPOSCOMPENSADETALLE>();
            VPOSCONVENIO objConvenio = new VPOSCONVENIO();
            string rutaLocal = string.Empty;
            string archivo = string.Empty;
            string error = string.Empty;
            bool lectura = true;
            bool existeArchivo = true;

            try
            {
                btch.ReadFiles readFiles_ = new btch.ReadFiles();

                ltCabecera = new VPOSCOMPENSACABECERA().ListarPendientesLectura();
                if (ltCabecera != null && ltCabecera.Count > 0)
                {
                    foreach (VPOSCOMPENSACABECERA objCabecera in ltCabecera)
                    {
                        objConvenio = new VPOSCONVENIO().ListarXConvenio(objCabecera.CCONVENIO.Value);
                        objCabecera.CESTADO = "LEC";
                        rutaLocal = string.Format(glbPathOrigen, objCabecera.FPROCESO.Value.ToString("yyyyMMdd"), objConvenio.ARCHIVOORIGEN);
                        if (!Directory.Exists(rutaLocal))
                        {
                            Directory.CreateDirectory(rutaLocal);
                        }

                        switch (objConvenio.ARCHIVOORIGEN)
                        {
                            #region datafast
                            case "datafast":
                                string[] parametros = ConfigurationManager.AppSettings["datafast"].ToString().Split(';');
                                archivo = string.Format(parametros[7], objCabecera.FPROCESO.Value.ToString("MMddyyyy"));
                                if (File.Exists(rutaLocal + archivo))
                                {
                                    existeArchivo = true;
                                    ltDetalle = readFiles_.LeerDatafast(objCabecera.FPROCESO.Value, objConvenio, rutaLocal, archivo, out error);

                                    lectura = (ltDetalle != null && ltDetalle.Count > 0 && error == "OK") ? true : false;
                                }
                                else
                                {
                                    existeArchivo = false;
                                    lectura = false;
                                }
                                break;
                            #endregion datafast
                            #region favorita
                            case "favorita":
                                DirectoryInfo filesFavorita = new DirectoryInfo(rutaLocal);
                                if (filesFavorita.EnumerateFiles().Count() > 0)
                                {
                                    existeArchivo = true;
                                    ltDetalle = readFiles_.LeerFavorita(objCabecera.FPROCESO.Value, objConvenio, rutaLocal, out error);

                                    lectura = (ltDetalle.Count > 0 && error == "OK") ? true : false;
                                }
                                else
                                {
                                    existeArchivo = false;
                                    lectura = false;
                                }
                                break;
                            #endregion favorita
                            #region farcomed
                            case "farcomed":
                            case "econofarm":
                                DirectoryInfo filesFarcomed = new DirectoryInfo(rutaLocal);
                                if (filesFarcomed.EnumerateFiles().Count() > 0)
                                {
                                    existeArchivo = true;
                                    ltDetalle = readFiles_.LeerFarcomed(objCabecera.FPROCESO.Value, objConvenio, rutaLocal, out error);

                                    lectura = (ltDetalle.Count > 0 && error == "OK") ? true : false;
                                }
                                else
                                {
                                    existeArchivo = false;
                                    lectura = false;
                                }
                                break;
                            #endregion favorita
                            #region default
                            default:
                                lectura = false;
                                existeArchivo = false;
                                break;
                                #endregion default
                        }

                        if (existeArchivo)
                        {
                            if (lectura)
                            {
                                foreach (TPOSCOMPENSADETALLE objDetalle in ltDetalle)
                                {
                                    if (!new TPOSCOMPENSADETALLE().Insertar(objDetalle))
                                    {
                                        Logging.EscribirLog("ERROR INSERTANDO REGISTRO " + objDetalle.FPROCESO.Value.ToString("yyyy/MM/dd") + ";" + objDetalle.CCONVENIO.Value + ";" + objDetalle.NUMEROTRANSACCION, null, "ERR");
                                        lectura = false;
                                        break;
                                    }

                                    if (!lectura)
                                    {
                                        objCabecera.CESTADO = "ERR";
                                        objCabecera.ERROR = "ERROR INSERTANDO REGISTROS BDD";
                                    }
                                }
                            }
                            else
                            {
                                lectura = true;
                                objCabecera.CESTADO = "FIN";
                                objCabecera.ERROR = "NO EXISTEN REGISTROS PARA COMPENSAR";
                            }

                            if (lectura)
                            {
                                if (objConvenio.COMPENSA == "1")
                                {
                                    TPOSCOMPENSACABECERA cabNueva = null;
                                    cabNueva = new TPOSCOMPENSACABECERA().Listar(objCabecera.FPROCESO.Value.AddDays(1), objCabecera.CCONVENIO);
                                    if (cabNueva == null)
                                    {
                                        cabNueva = new TPOSCOMPENSACABECERA();
                                        cabNueva.FPROCESO = objCabecera.FPROCESO.Value.AddDays(1);
                                        cabNueva.CCONVENIO = objCabecera.CCONVENIO;
                                        cabNueva.CESTADO = "PEN";
                                        new TPOSCOMPENSACABECERA().Insertar(cabNueva);
                                    }
                                }
                            }

                            new TPOSCOMPENSACABECERA().Actualizar(objCabecera);

                            if (!string.IsNullOrEmpty(error))
                            {
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + "OK");
                            }
                            else
                            {
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + "ERROR");
                            }
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + "NO EXISTE ARCHIVO ORIGEN");
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
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        #endregion lectura

        #region compensacion

        public void Compensar()
        {
            List<VPOSCOMPENSACABECERA> ltObjCabecera = null;
            VPOSCONVENIO objConvenio = new VPOSCONVENIO();
            Int32 registros = 0;
            string rutaLocal = string.Empty;
            bool flag = true;
            string error = "OK";
            try
            {
                ltObjCabecera = new VPOSCOMPENSACABECERA().ListarPendientesCompensar();
                if (ltObjCabecera != null && ltObjCabecera.Count > 0)
                {
                    foreach (VPOSCOMPENSACABECERA objCabecera in ltObjCabecera)
                    {
                        objConvenio = new VPOSCONVENIO().ListarXConvenio(objCabecera.CCONVENIO.Value);

                        if (objConvenio != null)
                        {
                            flag = Compensacion(objConvenio, objCabecera, out error, out registros);

                            if (flag && error == "OK")
                            {
                                objCabecera.CESTADO = "PRO";
                            }
                            else
                            {
                                objCabecera.CESTADO = "ERR";
                                objCabecera.ERROR = "ERROR COMPENSANDO REGISTROS";
                            }

                            new TPOSCOMPENSACABECERA().Actualizar(objCabecera);

                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + (error != "OK" ? error : "COMPENSACION REALIZADA CORRECTAMENTE"));
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + "NO SE PUEDE RECUPERAR CONVENIO");
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
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "PROCESO COMPENSAR: " + ex.Message.ToString());

            }
        }

        public bool Compensacion(VPOSCONVENIO objConvenio, VPOSCOMPENSACABECERA objCabecera, out string error, out Int32 registros)
        {
            List<TPOSCOMPENSADETALLE> ltCompensar = null;
            ISO8583_HISTORICO objTransaccion = null;
            Int32 registrosCompensados = 0;
            Int32 registrosRechazados = 0;
            bool flag = true;
            registros = 0;
            error = "OK";
            try
            {
                ltCompensar = new TPOSCOMPENSADETALLE().ListarPendientesCompensarXConvenio(objCabecera.FPROCESO, objCabecera.CCONVENIO);
                registros = ltCompensar.Count;
                if (ltCompensar.Count > 0)
                {
                    foreach (TPOSCOMPENSADETALLE objDetalle in ltCompensar)
                    {
                        try
                        {
                            string[] comando = objConvenio.COMANDOTRANSACCION.Split('.');
                            Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                            ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                            object reflecClassObject = constructor.Invoke(new object[] { });
                            MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                            objTransaccion = (ISO8583_HISTORICO)reflecMethod.Invoke(reflecClassObject, new object[] { objDetalle.TARJETA, objDetalle.FTRANSACCION, objDetalle.MID, objDetalle.NUMEROTRANSACCION, objDetalle.NUMEROAPROBACION });
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message.ToString().ToUpper();
                            objTransaccion = null;
                            Logging.EscribirLog("ERROR BUSCANDO TRANSACCION ORIGINAL", ex, "ERR");
                        }

                        if (objTransaccion != null && error == "OK")
                        {
                            if (objTransaccion.WISO_039_RESPONSECODE == "000")
                            {
                                if (objTransaccion.WISO_FECHACOMPENSACION == null || objDetalle.CESTADO == "REC")
                                {
                                    if (objTransaccion.WISO_004_AMOUNTTRANSACTION == objDetalle.VALORTRANSACCION)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(objConvenio.COMANDOCALCULO))
                                            {
                                                string[] comando = objConvenio.COMANDOCALCULO.Split('.');
                                                Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                                                ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                                                object reflecClassObject = constructor.Invoke(new object[] { });
                                                MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                                                TPOSCOMPENSADETALLE aux = null;
                                                aux = (TPOSCOMPENSADETALLE)reflecMethod.Invoke(reflecClassObject, new object[] { objDetalle, objConvenio });

                                                if (aux.CERROR == "000")
                                                {

                                                    objDetalle.VALORCOMISION = aux.VALORCOMISION;
                                                    objDetalle.VALORIVACOMISION = aux.VALORIVACOMISION;
                                                    objDetalle.VALORRETENCIONFUENTE = aux.VALORRETENCIONFUENTE;
                                                    objDetalle.VALORRETENCIONIVA = aux.VALORRETENCIONIVA;
                                                    objDetalle.VALORLIQUIDADO = aux.VALORLIQUIDADO;

                                                    objDetalle.FTRANSACCION = objTransaccion.WISO_007_TRANSDATETIME;
                                                    objDetalle.MID = objTransaccion.WISO_041_CARDACCEPTORID;
                                                    objDetalle.NUMEROMENSAJE = objTransaccion.WISO_044_ADDRESPDATA;
                                                    objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);
                                                    objDetalle.FCOMPENSACION = DateTime.Now;
                                                    objDetalle.CCUENTA = objTransaccion.WISO_102_ACCOUNTID_1;
                                                    objDetalle.CESTADO = "CMP";
                                                    objDetalle.CERROR = "000";
                                                    objDetalle.DERROR = "TRANSACCION COMPENSADA CORRECTAMENTE";
                                                    registrosCompensados++;

                                                    if (!new ISO8583_HISTORICO().ActualizarCompensacion(objTransaccion))
                                                    {
                                                        objDetalle.CESTADO = "ERR";
                                                        objDetalle.DERROR = "ERROR ACTUALIZANDO TRANSCCION ORIGINAL";
                                                    }
                                                }
                                                else
                                                {
                                                    objDetalle.CESTADO = "ERR";
                                                    objDetalle.DERROR = "ERROR EN CALCULO DE VALORES " + aux.DERROR;
                                                }
                                            }
                                            else
                                            {
                                                objDetalle.CESTADO = "ERR";
                                                objDetalle.DERROR = "TIPO CALCULO NO DEFINIDO (" + objConvenio.COMANDOCALCULO + ")";
                                                objDetalle.VALORCOMISION = 0;
                                                objDetalle.VALORIVACOMISION = 0;
                                                objDetalle.VALORRETENCIONFUENTE = 0;
                                                objDetalle.VALORRETENCIONIVA = 0;
                                                objDetalle.VALORLIQUIDADO = 0;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            objDetalle.CESTADO = "ERR";
                                            objDetalle.DERROR = "ERROR CALCULANDO VALORES " + ex.Message.ToUpper();
                                            objDetalle.VALORCOMISION = 0;
                                            objDetalle.VALORIVACOMISION = 0;
                                            objDetalle.VALORRETENCIONFUENTE = 0;
                                            objDetalle.VALORRETENCIONIVA = 0;
                                            objDetalle.VALORLIQUIDADO = 0;
                                            Logging.EscribirLog(null, ex, "ERR");
                                        }
                                    }
                                    else
                                    {
                                        objDetalle.CCUENTA = objTransaccion.WISO_102_ACCOUNTID_1;
                                        objDetalle.CESTADO = "RCH";
                                        objDetalle.CERROR = "901";
                                        objDetalle.DERROR = "VALOR NO CORRESPOENDE";
                                        registrosRechazados++;
                                    }
                                }
                                else
                                {
                                    objDetalle.CCUENTA = objTransaccion.WISO_102_ACCOUNTID_1;
                                    objDetalle.CESTADO = "RCH";
                                    objDetalle.CERROR = "903";
                                    objDetalle.DERROR = "TRANSACCION ORIGINAL YA COMPENSADA";
                                    registrosRechazados++;
                                }
                            }
                            else
                            {
                                objDetalle.CCUENTA = objTransaccion.WISO_102_ACCOUNTID_1;
                                objDetalle.CESTADO = "RCH";
                                objDetalle.CERROR = "902";
                                objDetalle.DERROR = "TRANSACCION ORIGINAL CON ERROR";
                                registrosRechazados++;
                            }
                        }
                        else
                        {
                            if (error == "OK")
                            {
                                try { objDetalle.CCUENTA = new TARJETACUENTA().ListarTarjetaCuenta(objDetalle.TARJETA).CUENTA; }
                                catch { objDetalle.CCUENTA = string.Empty; }
                                objDetalle.CESTADO = "RCH";
                                objDetalle.CERROR = "900";
                                objDetalle.DERROR = "TRANSACCION ORIGEN NO ENCONTRADA";
                                registrosRechazados++;
                            }
                        }
                        new TPOSCOMPENSADETALLE().Actualizar(objDetalle);
                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy").PadRight(20) + objCabecera.CCONVENIO.Value.ToString().PadRight(20) + objDetalle.SECUENCIA.Value.ToString().PadRight(10) + (objDetalle.CERROR == "000" ? "OK" : "ERROR"));
                    }
                    if (ltCompensar.Count == (registrosRechazados + registrosCompensados))
                    {
                        error = "OK";
                    }
                    else
                    {
                        error = "DIFERENCIA EN REGISTROS PROCESADOS TOTAL: " + ltCompensar.Count + "; COMPENSADOS: " + registrosCompensados + "; RECHAZADOS: " + registrosRechazados + ";";
                    }
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA COMPENSAR";
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString();
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        public TPOSCOMPENSADETALLE CalculoRetencion(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoRetencion(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoSinRetencion(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoSinRetencion(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoRetencionSinIva(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoRetencionSinIva(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoFavorita(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoFavorita(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoTia(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoTia(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoFarcomed(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoFarcomed(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoTablita(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoTablita(objDetalle, objConvenio);
        }

        public TPOSCOMPENSADETALLE CalculoEtatex(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            btch.RetentionMethods retencion = new btch.RetentionMethods();
            return retencion.CalculoEtatex(objDetalle, objConvenio);
        }

        #endregion compensacion

        #region autorizacion

        public void Autorizar()
        {
            VPOSCOMPENSACABECERA objCabeceraActualizar = null;
            VPOSCONVENIO objConvenio = null;
            List<VPOSCOMPENSACABECERA> ltPendientes = null;
            List<VPOSCOMPENSACABECERA> ltAutorizar = null;
            List<TPOSCOMPENSADETALLE> ltDetalleCompensar = null;
            bool flag = true;
            string pathResultado = string.Empty;
            string archivo = string.Empty;
            string error = "OK";
            string estado = string.Empty;

            try
            {
                ltPendientes = new VPOSCOMPENSACABECERA().ListarPendientesAutorizar();
                if (ltPendientes != null && ltPendientes.Count > 0)
                {
                    foreach (VPOSCOMPENSACABECERA cab in ltPendientes)
                    {
                        error = "OK";
                        archivo = string.Empty;

                        pathResultado = string.Format(glbPathResultado, cab.FAUTORIZACION.Value.ToString("yyyyMMdd"));
                        if (!Directory.Exists(pathResultado))
                        {
                            Directory.CreateDirectory(pathResultado);
                        }

                        objConvenio = new VPOSCONVENIO().ListarXConvenio(cab.CCONVENIO.Value);

                        ltAutorizar = new VPOSCOMPENSACABECERA().ListarAutorizadosConvenio(cab.CCONVENIO, cab.FAUTORIZACION);
                        if (ltAutorizar != null && ltAutorizar.Count > 0)
                        {
                            ltDetalleCompensar = new List<TPOSCOMPENSADETALLE>();
                            foreach (VPOSCOMPENSACABECERA objCabecera in ltAutorizar)
                            {
                                ltDetalleCompensar.AddRange(new TPOSCOMPENSADETALLE().ListarCompensadosXConvenio(objCabecera.FPROCESO, objCabecera.CCONVENIO));
                            }

                            if (ltDetalleCompensar.Count > 0)
                            {
                                flag = Pagos(objConvenio, cab.FAUTORIZACION.Value, ltAutorizar, out error);

                                if (flag && error == "OK")
                                {
                                    if (objConvenio.NOTIFICA == "1")
                                    {
                                        #region arma archivos
                                        string[] comandoArchivo = objConvenio.COMANDOARCHIVO.Split(';');
                                        foreach (string comandoEjecutar in comandoArchivo)
                                        {
                                            try
                                            {
                                                string[] comando = comandoEjecutar.Split('.');
                                                Type tipo = Type.GetType(comando[0] + "." + comando[1] + ", " + comando[0]);
                                                ConstructorInfo constructor = tipo.GetConstructor(Type.EmptyTypes);
                                                object reflecClassObject = constructor.Invoke(new object[] { });
                                                MethodInfo reflecMethod = tipo.GetMethod(comando[2]);
                                                string[] respuesta = (string[])reflecMethod.Invoke(reflecClassObject, new object[] { objConvenio, ltDetalleCompensar, pathResultado, cab.FAUTORIZACION });
                                                if (respuesta[0] == "OK")
                                                    archivo += respuesta[1] + ";";
                                                else
                                                {
                                                    error = respuesta[0];
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                error = ex.Message.ToString().ToUpper();
                                                Logging.EscribirLog("ERROR EJECUTANDO COMANDO ARCHIVO CCONVENIO: " + objConvenio.CCONVENIO, ex, "ERR");
                                                break;
                                            }
                                        }
                                        #endregion arma archivos

                                        #region notificacion
                                        if (error == "OK")
                                        {
                                            if (NotificarArchivos(objConvenio, ltDetalleCompensar, pathResultado, archivo, out error) && error == "OK")
                                            {
                                                estado = "FIN";
                                            }
                                            else
                                            {
                                                estado = "ERR";
                                            }
                                        }
                                        else
                                        {
                                            estado = "ERR";
                                        }
                                        #endregion notificacion
                                    }
                                    else
                                    {
                                        estado = "FIN";
                                    }
                                }
                                else
                                {
                                    estado = "ERR";
                                }

                                if (flag)
                                {
                                    objCabeceraActualizar = ltAutorizar.First();
                                    objCabeceraActualizar.CESTADO = estado;
                                    objCabeceraActualizar.FAUTORIZACION = cab.FAUTORIZACION;
                                    if (error != "OK")
                                    {
                                        objCabeceraActualizar.ERROR = error;
                                    }
                                    new TPOSCOMPENSACABECERA().ActualizarAutorizar(objCabeceraActualizar);
                                }
                            }
                            else
                            {
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO.Value.ToString().PadRight(20) + " NO EXISTEN REGISTROS POR AUTORIZAR " + (error != "OK" ? " - " + error : ""));
                            }
                        }
                        else
                        {
                            Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN REGISTROS PARA AUTORIZAR CONVENIO (" + cab.CCONVENIO + ") " + (error != "OK" ? " - " + error : ""));
                        }
                    }
                }
                else
                {
                    Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "NO EXISTEN CONVENIOS PARA AUTORIZAR " + (error != "OK" ? " - " + error : ""));
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
            }
        }

        #region archivos respuesta

        btch.StructureFiles structureFiles_ = new btch.StructureFiles();

        public string[] EstructuraNormal(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraNormal(convenio, ltCompensados, ruta, fautorizacion); 
        }

        public string[] EstructuraFarmaEnlace(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraFarmaEnlace(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraNormalExcel(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraNormalExcel(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraEtatex(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraEtatex(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraTia(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraTia(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraSantaMaria(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraSantaMaria(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraSantaMariaExcel(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraSantaMariaExcel(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraFavorita(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraFavoritaNueva(convenio, ltCompensados, ruta, fautorizacion);
        }

        public string[] EstructuraFacomed(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            return structureFiles_.EstructuraFacomedNuevoFormato(convenio, ltCompensados, ruta, fautorizacion);
        }
   

        #endregion archivos respuesta

        public bool NotificarArchivos(VPOSCONVENIO objConvenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, string archivo, out string error)
        {
            bool resp = true;
            error = "OK";
            try
            {
                wsS29.Iso8583 iso = new wsS29.Iso8583();
                iso.ISO_000_Message_Type = "1200";
                iso.ISO_002_PAN = "POSCOMP001";
                iso.ISO_003_ProcessingCode = "960000";
                iso.ISO_007_TransDatetime = DateTime.Now;
                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                iso.ISO_012_LocalDatetime = DateTime.Now;
                iso.ISO_015_SettlementDatel = DateTime.Now;
                iso.ISO_018_MerchantType = "0001";
                iso.ISO_024_NetworkId = "555551";
                csEstructuraNotificacion.notificacion objNotifica = new csEstructuraNotificacion.notificacion();
                csEstructuraNotificacion.mensaje objMensaje = new csEstructuraNotificacion.mensaje();
                objMensaje.to = objConvenio.NOTIFICAPARAMETROS;
                objMensaje.cc = glbCopiaMail;
                objMensaje.parametros =
                    objConvenio.NOMBRE + ";" +
                    ltCompensados.OrderBy(x => x.FPROCESO).First().FPROCESO.Value.ToString("dd/MM/yyyy") + ";" +
                    ltCompensados.OrderBy(x => x.FPROCESO).Last().FPROCESO.Value.ToString("dd/MM/yyyy") + ";" +
                    ltCompensados.Count + ";" +
                    ltCompensados.Sum(x => x.VALORTRANSACCION).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                    ltCompensados.Sum(x => x.VALORCOMISION).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                    ltCompensados.Sum(x => x.VALORIVACOMISION).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                    ltCompensados.Sum(x => x.VALORRETENCIONIVA).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                    ltCompensados.Sum(x => x.VALORRETENCIONFUENTE).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC")) + ";" +
                    ltCompensados.Sum(x => x.VALORLIQUIDADO).Value.ToString("C2", CultureInfo.CreateSpecificCulture("es-EC"));
                switch (objConvenio.NOTIFICATIPO)
                {
                    case "MAIL":
                        string[] archivos = archivo.Split(';');
                        archivos = archivos.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        csEstructuraNotificacion.archivo[] objAdjunto = new csEstructuraNotificacion.archivo[archivos.Count()];
                        for (int i = 0; i < archivos.Count(); i++)
                        {
                            if (!string.IsNullOrEmpty(archivos[i]))
                            {
                                objAdjunto[i] = new csEstructuraNotificacion.archivo();
                                objAdjunto[i].nombre = archivos[i];
                                objAdjunto[i].contenido = Util.ArchivoToBase64String(ruta, archivos[i]);
                            }
                        }
                        objNotifica.items = new object[] { objMensaje, objAdjunto };
                        break;
                }
                iso.ISO_114_ExtendedData = Util.ObjToString(objNotifica);
                wsS29.uciMethods ws = new wsS29.uciMethods();
                ws.Timeout = glbTimeoutWS;
                iso = ws.ProcessingTransactionISO_WEB(iso);
                if (iso.ISO_039_ResponseCode == "000")
                {
                    resp = true;
                    error = "OK";
                }
                else
                {
                    resp = false;
                    error = iso.ISO_039_ResponseCode + " - " + iso.ISO_039p_ResponseDetail;
                }
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO.Value.ToString().PadRight(20) + " NOTIFICACION ARCHIVOS: " + error);
            }
            catch (Exception ex)
            {
                resp = false;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return resp;
        }

        public bool Pagos(VPOSCONVENIO objConvenio, DateTime fautorizacion, List<VPOSCOMPENSACABECERA> ltObjCabecera, out string error)
        {
            decimal valorLiquidado = 0;
            string referencia = string.Empty;
            bool flag = true;
            error = "OK";

            try
            {
                var cab = ltObjCabecera.FirstOrDefault();
                switch (objConvenio.TIPOLIQUIDACION)
                {
                    case "TOT":
                        #region transferencia
                        if (flag && error == "OK")
                        {
                            valorLiquidado = ltObjCabecera.Sum(x => x.TOTALTRANSACCION).Value;
                            if (valorLiquidado > 0 && string.IsNullOrEmpty(cab.TRANSFERENCIA))
                            {
                                flag = Transferencia(objConvenio, valorLiquidado, out referencia, out error);
                                if (flag && error == "OK")
                                {
                                    new TPOSCOMPENSACABECERA().ActualizarReferenciaTransferencia(objConvenio.CCONVENIO, fautorizacion, referencia);
                                }
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO.Value.ToString().PadRight(20) + " TRANSFERENCIA:" + error);
                            }
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                        #endregion transferencia
                        break;
                    case "LIQ":
                        #region transferencia
                        if (flag && error == "OK")
                        {
                            valorLiquidado = ltObjCabecera.Sum(x => x.TOTALLIQUIDADO).Value;
                            if (valorLiquidado > 0 && string.IsNullOrEmpty(cab.TRANSFERENCIA))
                            {
                                flag = Transferencia(objConvenio, valorLiquidado, out referencia, out error);
                                if (flag && error == "OK")
                                {
                                    new TPOSCOMPENSACABECERA().ActualizarReferenciaTransferencia(objConvenio.CCONVENIO, fautorizacion, referencia);
                                }
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO.Value.ToString().PadRight(20) + " TRANSFERENCIA:" + error);
                            }
                            else
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                        #endregion transferencia

                        #region debito comision
                        if (flag && error == "OK")
                        {
                            valorLiquidado = ltObjCabecera.Sum(x => x.TOTALCOMISION).Value + ltObjCabecera.Sum(x => x.TOTALIVACOMISION).Value - ltObjCabecera.Sum(x => x.TOTALRETENCIONFTE).Value - ltObjCabecera.Sum(x => x.TOTALRETENCIONIVA).Value;
                            if (valorLiquidado > 0 && string.IsNullOrEmpty(cab.COMISION))
                            {
                                flag = NotaDebito(objConvenio, valorLiquidado, "73", "COMISION COMPENSACION", out referencia, out error);
                                if (flag && error == "OK")
                                {
                                    new TPOSCOMPENSACABECERA().ActualizarReferenciaComision(objConvenio.CCONVENIO, fautorizacion, referencia);
                                }
                                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO.Value.ToString().PadRight(20) + " DEBITO:" + error);
                            }
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                        #endregion debito comision
                        break;
                    default:
                        flag = false;
                        error = "TIPO LIQUIDACION NO DEFINIDO";
                        Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, objConvenio.CCONVENIO + " TIPO LIQUIDACION NO DEFINIDO");
                        break;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            if (error.Contains("BDD-00001"))
            {
                flag = false;
            }

            return flag;
        }

        public bool Transferencia(VPOSCONVENIO objConvenio, decimal valor, out string referencia, out string error)
        {
            bool response = false;
            switch (objConvenio.TIPOPAGO)
            {
                case "EXT":
                    response = TransferenciaSpi(objConvenio, valor, out referencia, out error);
                    break;
                case "INT":
                    response = TransferenciaInterna(objConvenio, valor, out referencia, out error);
                    break;
                default:
                    response = false;
                    referencia = string.Empty;
                    error = "TIPO PAGO NO DEFINIDO";
                    break;
            }
            return response;
        }

        public bool TransferenciaSpi(VPOSCONVENIO objConvenio, decimal valor, out string referencia, out string error)
        {
            bool flag = true;
            error = "OK";
            referencia = string.Empty;
            try
            {

                wsS29.Iso8583 iso = new wsS29.Iso8583();
                iso.ISO_000_Message_Type = "1200";
                iso.ISO_002_PAN = objConvenio.IDENTIFICACION;
                iso.ISO_003_ProcessingCode = "111020";
                iso.ISO_004_AmountTransaction = valor;
                iso.ISO_007_TransDatetime = DateTime.Now;
                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                iso.ISO_012_LocalDatetime = DateTime.Now;
                iso.ISO_015_SettlementDatel = DateTime.Now;
                iso.ISO_018_MerchantType = "0001";
                iso.ISO_023_CardSeq = objConvenio.CUENTACREDITOTIPO;
                iso.ISO_024_NetworkId = "555551";
                iso.ISO_034_PANExt = objConvenio.NOMBRE.ToUpper();
                iso.ISO_041_CardAcceptorID = "1";
                iso.ISO_042_Card_Acc_ID_Code = "13";
                iso.ISO_090_OriginalData = objConvenio.CUENTACREDITOBANCO.Value.ToString();
                iso.ISO_102_AccountID_1 = objConvenio.CUENTADEBITO;
                iso.ISO_103_AccountID_2 = objConvenio.CUENTACREDITO;
                iso.ISO_104_TranDescription = "SPI PAGOS COMPENSACION";
                iso.ISO_121_ExtendedData = objConvenio.CCONVENIO.Value.ToString();
                iso.ISO_122_ExtendedData = objConvenio.NOMBRE.ToUpper();
                wsS29.uciMethods ws = new wsS29.uciMethods();
                ws.Timeout = glbTimeoutWS;
                iso = ws.ProcessingTransactionISO_WEB(iso);
                if (iso.ISO_039_ResponseCode == "000")
                {
                    flag = true;
                    error = "OK";
                    referencia = iso.ISO_011_SysAuditNumber + ";" + iso.ISO_124_ExtendedData;
                }
                else
                {
                    flag = false;
                    error = "TRANSFERENCIA - " + iso.ISO_039_ResponseCode + " - " + iso.ISO_039p_ResponseDetail;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = "TRANSFERENCIA - " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        public bool TransferenciaInterna(VPOSCONVENIO objConvenio, decimal valor, out string referencia, out string error)
        {
            bool flag = true;
            error = "OK";
            referencia = string.Empty;
            try
            {

                wsS29.Iso8583 iso = new wsS29.Iso8583();
                iso.ISO_000_Message_Type = "1200";
                iso.ISO_002_PAN = objConvenio.IDENTIFICACION;
                iso.ISO_003_ProcessingCode = "401010";
                iso.ISO_004_AmountTransaction = valor;
                iso.ISO_007_TransDatetime = DateTime.Now;
                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                iso.ISO_012_LocalDatetime = DateTime.Now;
                iso.ISO_015_SettlementDatel = DateTime.Now;
                iso.ISO_018_MerchantType = "0001";
                iso.ISO_024_NetworkId = "555551";
                iso.ISO_037_RetrievalReferenceNumber = iso.ISO_011_SysAuditNumber;
                iso.ISO_102_AccountID_1 = objConvenio.CUENTADEBITO;
                iso.ISO_103_AccountID_2 = objConvenio.CUENTACREDITO;
                iso.ISO_104_TranDescription = "SPI PAGOS COMPENSACION";
                wsS29.uciMethods ws = new wsS29.uciMethods();
                ws.Timeout = glbTimeoutWS;
                iso = ws.ProcessingTransactionISO_WEB(iso);
                if (iso.ISO_039_ResponseCode == "000")
                {
                    flag = true;
                    error = "OK";
                    referencia = iso.ISO_037_RetrievalReferenceNumber;
                }
                else
                {
                    flag = false;
                    error = "TRANSFERENCIA - " + iso.ISO_039_ResponseCode + " - " + iso.ISO_039p_ResponseDetail;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = "TRANSFERENCIA - " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        public bool NotaDebito(VPOSCONVENIO objConvenio, decimal valor, string rubro, string descripcion, out string referencia, out string error)
        {
            bool flag = true;
            error = "OK";
            referencia = string.Empty;
            try
            {
                wsS29.Iso8583 iso = new wsS29.Iso8583();
                iso.ISO_000_Message_Type = "1200";
                iso.ISO_002_PAN = objConvenio.IDENTIFICACION;
                iso.ISO_003_ProcessingCode = "011000";
                iso.ISO_004_AmountTransaction = valor;
                iso.ISO_007_TransDatetime = DateTime.Now;
                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                iso.ISO_012_LocalDatetime = DateTime.Now;
                iso.ISO_015_SettlementDatel = DateTime.Now;
                iso.ISO_018_MerchantType = "0001";
                iso.ISO_024_NetworkId = "555551";
                iso.ISO_034_PANExt = rubro;
                iso.ISO_037_RetrievalReferenceNumber = iso.ISO_011_SysAuditNumber;
                iso.ISO_041_CardAcceptorID = "00000000";
                iso.ISO_102_AccountID_1 = objConvenio.CUENTADEBITO;
                iso.ISO_104_TranDescription = descripcion;
                wsS29.uciMethods ws = new wsS29.uciMethods();
                ws.Timeout = glbTimeoutWS;
                iso = ws.ProcessingTransactionISO_WEB(iso);
                if (iso.ISO_039_ResponseCode == "000")
                {
                    flag = true;
                    error = "OK";
                    referencia = iso.ISO_011_SysAuditNumber;
                }
                else
                {
                    flag = false;
                    error = "DEBITO - " + iso.ISO_039_ResponseCode;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error = "DEBITO - " + ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return flag;
        }

        #endregion autorizacion
    }
}

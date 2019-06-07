using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Business
{
    public class WebEstructuras
    {
        public static StreamWriter file = null;

        /*public CanalRespuesta GeneraConciliacion(DateTime finicio, DateTime ffin, string path, out string archivoZip)
        {
            #region Variables

            CanalRespuesta resp = new CanalRespuesta();
            string archivoDetalle = string.Empty;
            string archivoCabecera = string.Empty;
            string archivoValidacion = string.Empty;*/



        /*Int32 totalClientes = 0;
        Int32 totalProductos = 0;*/



        /*string error = string.Empty;
        archivoZip = string.Empty;

        //Variables de conciliación
        string codigoEntidad = ConfigurationManager.AppSettings["uafCodigoInstitucion"];
        DateTime fechaHoraGeneracion = DateTime.Now;
        double valorTotal = 0;
        int numeroTransacciones = 0;
        string cabecera = string.Empty;


        #endregion Variables

        try
        {




            /*UAFDetalle(finicio, path, out error, out archivoDetalle, out totalClientes, out totalProductos);
            if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(archivoDetalle))
            {
                UAFValidacion(finicio, path, archivoDetalle, out archivoValidacion);
                UAFCabecera(finicio, path, archivoDetalle, totalClientes, totalProductos, out error, out archivoCabecera);
                if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(archivoCabecera))
                {
                    archivoZip = "UAF_" + finicio.ToString("yyyyMMdd") + ".zip";
                    ZipFile zip = new ZipFile();
                    zip.AddFile(path + archivoDetalle, "");
                    zip.AddFile(path + archivoCabecera, "");
                    if (!string.IsNullOrEmpty(archivoValidacion))
                        zip.AddFile(path + archivoValidacion, "");
                    zip.Save(path + archivoZip);

                    resp.CError = "000";
                    resp.DError = "ESTRUCTURA GENERADA CORRECTAMENTE";
                }
            }*/





        /*
            string cabecera = String.Format("{0}_{1}_{2}", "1", "2", "3");


            System.IO.File.WriteAllLines(path + "IN_1122_" + ffin.ToString("yyyyMMdd") + ".dat", cabecera);


            resp.CError = "000";
            resp.DError = "ESTRUCTURA GENERADA CORRECTAMENTE";

        }
        catch (Exception ex)
        {
            resp.CError = "999";
            resp.DError = "ERROR EN SISTEMA: " + Util.ReturnExceptionString(ex);
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
        return resp;
    }*/


        public CanalRespuesta GeneraUAF(DateTime fcorte, string path, out string archivoZip)
        {
            #region Variables

            CanalRespuesta resp = new CanalRespuesta();
            string archivoDetalle = string.Empty;
            string archivoCabecera = string.Empty;
            string archivoValidacion = string.Empty;
            Int32 totalClientes = 0;
            Int32 totalProductos = 0;
            string error = string.Empty;
            archivoZip = string.Empty;

            #endregion Variables

            try
            {
                UAFDetalle(fcorte, path, out error, out archivoDetalle, out totalClientes, out totalProductos);
                if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(archivoDetalle))
                {
                    UAFValidacion(fcorte, path, archivoDetalle, out archivoValidacion);
                    UAFCabecera(fcorte, path, archivoDetalle, totalClientes, totalProductos, out error, out archivoCabecera);
                    if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(archivoCabecera))
                    {
                        archivoZip = "UAF_" + fcorte.ToString("yyyyMMdd") + ".zip";
                        ZipFile zip = new ZipFile();
                        zip.AddFile(path + archivoDetalle, "");
                        zip.AddFile(path + archivoCabecera, "");
                        if (!string.IsNullOrEmpty(archivoValidacion))
                            zip.AddFile(path + archivoValidacion, "");
                        zip.Save(path + archivoZip);

                        resp.CError = "000";
                        resp.DError = "ESTRUCTURA GENERADA CORRECTAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.CError = "999";
                resp.DError = "ERROR EN SISTEMA: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return resp;
        }

        public void UAFDetalle(DateTime fcorte, string path, out string error, out string archivo, out Int32 totalClientes, out Int32 totalProductos)
        {
            ESTRUCTURA uaf = null;
            StreamWriter file = null;
            archivo = string.Empty;
            error = string.Empty;
            totalClientes = 0;
            totalProductos = 0;

            try
            {
                #region clientes
                List<UAFCLIENTE> ltClientes = new UAFCLIENTE().Listar(fcorte, 0, 0);
                if (ltClientes != null && ltClientes.Count > 0)
                {
                    uaf = new ESTRUCTURA();
                    uaf.CLI = new CLIENTE[ltClientes.Count];
                    int x = 0;
                    foreach (UAFCLIENTE cliente in ltClientes)
                    {
                        uaf.CLI[x] = new CLIENTE();
                        uaf.CLI[x].TID = cliente.TID;
                        uaf.CLI[x].IDE = cliente.IDE;
                        uaf.CLI[x].NRS = cliente.NRS;
                        uaf.CLI[x].NAC = cliente.NAC;
                        uaf.CLI[x].DIR = cliente.DIR;
                        uaf.CLI[x].CCC = cliente.CCC;
                        uaf.CLI[x].AEC = cliente.AEC;
                        uaf.CLI[x].IMT = cliente.IMT.ToString();
                        uaf.CLI[x].CDR = cliente.CDR;
                        uaf.CLI[x].FCT = cliente.FCT.Value.ToString("yyyyMMdd");

                        #region productos
                        List<UAFPRODUCTO> ltProductos = new UAFPRODUCTO().Listar(fcorte, cliente.TID, cliente.IDE);
                        if (ltProductos != null && ltProductos.Count > 0)
                        {
                            uaf.CLI[x].PRO = new PRODUCTO[ltProductos.Count];
                            int y = 0;
                            foreach (UAFPRODUCTO producto in ltProductos)
                            {
                                uaf.CLI[x].PRO[y] = new PRODUCTO();
                                uaf.CLI[x].PRO[y].TCO = producto.TCO;
                                uaf.CLI[x].PRO[y].NCO = producto.NCO;
                                uaf.CLI[x].PRO[y].CAP = producto.CAP;
                                uaf.CLI[x].PRO[y].FAC = producto.FAC.Value.ToString("yyyyMMdd");

                                #region transacciones
                                List<UAFTRANSACCION> ltTransacciones = new UAFTRANSACCION().Listar(fcorte, cliente.TID, cliente.IDE, producto.NCO);
                                if (ltProductos != null && ltProductos.Count > 0)
                                {
                                    uaf.CLI[x].PRO[y].TRX = new TRANSACCION[ltTransacciones.Count];
                                    int z = 0;
                                    foreach (UAFTRANSACCION trx in ltTransacciones)
                                    {
                                        uaf.CLI[x].PRO[y].TRX[z] = new TRANSACCION();
                                        uaf.CLI[x].PRO[y].TRX[z].FTR = trx.FTR.Value.ToString("yyyyMMdd");
                                        uaf.CLI[x].PRO[y].TRX[z].NTR = trx.NTR;
                                        uaf.CLI[x].PRO[y].TRX[z].VDE = trx.VDE.Value.ToString();
                                        uaf.CLI[x].PRO[y].TRX[z].VCR = trx.VCR.Value.ToString();
                                        uaf.CLI[x].PRO[y].TRX[z].VEF = trx.VEF.Value.ToString();
                                        uaf.CLI[x].PRO[y].TRX[z].VCH = trx.VCH.Value.ToString();
                                        uaf.CLI[x].PRO[y].TRX[z].VVT = trx.VVT.Value.ToString();
                                        uaf.CLI[x].PRO[y].TRX[z].MND = trx.MND;
                                        uaf.CLI[x].PRO[y].TRX[z].TTR = trx.TTR;
                                        uaf.CLI[x].PRO[y].TRX[z].NOB = trx.NOB;
                                        uaf.CLI[x].PRO[y].TRX[z].CSW = trx.CSW;
                                        uaf.CLI[x].PRO[y].TRX[z].ISD = trx.ISD.Value.ToString();

                                        uaf.CLI[x].PRO[y].TRX[z].BNC = new BANCO[1];
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0] = new BANCO();
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0].IDO = trx.IDO;
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0].COB = trx.COB;
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0].PDO = trx.PDO;
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0].CAT = trx.CAT;
                                        uaf.CLI[x].PRO[y].TRX[z].BNC[0].CCT = trx.CCT;
                                        z++;
                                    }
                                }
                                #endregion transacciones

                                y++;
                                totalProductos++;
                            }
                        }
                        #endregion productos

                        x++;
                        totalClientes++;
                    }
                }
                #endregion clientes

                if (uaf != null)
                {
                    try
                    {
                        archivo =
                            "DETALLE" +
                            ConfigurationManager.AppSettings["uafCodigoInstitucion"] +
                            fcorte.ToString("yyyyMMdd") +
                            ".xml";

                        file = new StreamWriter(path + archivo);
                        file.Write(UAFDetalleToString(uaf));
                    }
                    catch (Exception ex)
                    {
                        archivo = "";
                        error = Util.ReturnExceptionString(ex);
                    }
                    finally
                    {
                        if (file != null)
                            file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                error = Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public void UAFCabecera(DateTime fcorte, string path, string archivoDetalle, Int32 totalClientes, Int32 totalProductos, out string error, out string archivo)
        {
            SEGURIDAD seg = null;
            CABECERA cab = null;
            StreamWriter file = null;
            error = string.Empty;
            archivo = string.Empty;

            try
            {
                UAFTRANSACCION transaccion = new UAFTRANSACCION().Totales(fcorte);
                if (transaccion != null)
                {
                    seg = new SEGURIDAD();
                    seg.CLAVE = new FileInfo(path + archivoDetalle).LastWriteTime.ToString("dd-MM-yyyy HH:mm:ss");

                    cab = new CABECERA();
                    cab.CDR = ConfigurationManager.AppSettings["uafCodigoInstitucion"];
                    cab.PDR = fcorte.ToString("yyyyMMdd");
                    cab.FRE = DateTime.Now.ToString("yyyyMMdd");
                    cab.USR = ConfigurationManager.AppSettings["uafUsuarioOficial"];
                    cab.CLI = totalClientes.ToString();
                    cab.PRO = totalProductos.ToString();
                    cab.TRA = transaccion.TRX.Value.ToString();
                    cab.TRB = transaccion.TRX.Value.ToString();
                    cab.TDE = transaccion.TDE.Value.ToString();
                    cab.TCR = transaccion.TCR.Value.ToString();
                    cab.TEF = transaccion.TEF.Value.ToString();
                    cab.TCH = transaccion.TCH.Value.ToString();
                    cab.TVT = transaccion.TVT.Value.ToString();

                    seg.Items = new object[] { cab };

                    try
                    {
                        archivo =
                            "CABECERA" +
                            ConfigurationManager.AppSettings["uafCodigoInstitucion"] +
                            fcorte.ToString("yyyyMMdd") +
                            ".xml";

                        file = new StreamWriter(path + archivo);
                        file.Write(UAFCabeceraToString(seg));
                    }
                    catch (Exception ex)
                    {
                        archivo = "";
                        error = Util.ReturnExceptionString(ex);
                    }
                    finally
                    {
                        if (file != null)
                            file.Close();
                    }
                }
                else
                {
                    error = "NO SE PUEDE RECUPERAR TOTALES: " + error;
                }
            }
            catch (Exception ex)
            {
                error = Util.ReturnExceptionString(ex);
            }
        }

        public void UAFValidacion(DateTime fcorte, string path, string archivoEstructura, out string archivo)
        {
            archivo = string.Empty;

            try
            {
                archivo = "VALIDA" + ConfigurationManager.AppSettings["uafCodigoInstitucion"] + fcorte.ToString("yyyyMMdd") + ".txt";

                file = new StreamWriter(path + archivo);

                XmlReaderSettings booksSettings = new XmlReaderSettings();
                booksSettings.Schemas.Add(string.Empty, ConfigurationManager.AppSettings["pathArchivos"] + "estructuras/UAF/" + "UAFDetalle.xsd");
                booksSettings.ValidationType = ValidationType.Schema;
                booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);
                XmlReader books = XmlReader.Create(path + archivoEstructura, booksSettings);
                while (books.Read()) { }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }

        public static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            string linea = string.Empty;

            if (e.Severity == XmlSeverityType.Warning)
            {
                linea = "WARNING: " + e.Message;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                linea = "ERROR: " + e.Message;
            }

            if (file != null)
            {
                file.Write(linea);
            }
        }

        public CanalRespuesta ConvierteEstructura(string tipoEstructura, DateTime fechaCorte, string ruta, string archivo, out string rutaZip, out string archivoZip)
        {
            #region Variables
            CanalRespuesta resp = new CanalRespuesta();
            string ruc = ConfigurationManager.AppSettings["coop29Ruc"];
            string linea = string.Empty;
            string codigoHash = string.Empty;
            string estructuraXml = string.Empty;
            string rutaXml = string.Empty;
            string rutaHash = string.Empty;
            string nombreZip = string.Empty;
            string error = "OK";
            Int64 numeroRegistros = 0;
            Int64 numeroLinea = 0;
            DateTime fechaEstructura = DateTime.Now;
            DataTable dt = new DataTable();
            StreamReader srArchivo;
            StringBuilder archivoXmlEstructura = new StringBuilder();
            ZipFile fileZip = new ZipFile();
            resp.CError = "000";
            resp.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
            rutaZip = string.Empty;
            archivoZip = string.Empty;
            #endregion Variables
            try
            {
                #region ProcesaEstructura
                switch (tipoEstructura)
                {
                    #region B11-B13
                    case "B11":
                    case "B13":
                        break;
                    #endregion B11-B13
                    #region C01
                    case "C01":
                        error = "OK";
                        #region armaTabla
                        if (error == "OK")
                        {
                            try
                            {
                                dt.Clear();
                                dt.Columns.Add("tipoIdentificacionSujeto");
                                dt.Columns.Add("identificacionSujeto");
                                dt.Columns.Add("numeroOperacion");
                                dt.Columns.Add("valorOperacion");
                                dt.Columns.Add("tasaInteresNominal");
                                dt.Columns.Add("tasaEfectivaAnual");
                                dt.Columns.Add("fechaConcesion");
                                dt.Columns.Add("fechaVencimiento");
                                dt.Columns.Add("lineaCredito");
                                dt.Columns.Add("periodicidadPago");
                                dt.Columns.Add("frecuenciaRevision");
                                dt.Columns.Add("oficinaConcesion");
                                dt.Columns.Add("garante");
                                dt.Columns.Add("tipoCredito");
                                dt.Columns.Add("claseCredito");
                                dt.Columns.Add("estadoOperacion");
                                dt.Columns.Add("numeroOperacionAnterior");
                                dt.Columns.Add("origenOperacion");
                                dt.Columns.Add("tipoOperacion");
                                dt.Columns.Add("causalVinculacion");
                                dt.Columns.Add("destinoFinanciero");
                                dt.Columns.Add("actividadEconomica");
                                dt.Columns.Add("geograficoProvincia");
                                dt.Columns.Add("geograficoCanton");
                                dt.Columns.Add("geograficoParroquia");
                                dt.Columns.Add("totalIngresosSujeto");
                                dt.Columns.Add("totalEgresosSujeto");
                                dt.Columns.Add("patrimonioSujeto");
                                dt.Columns.Add("actividadSujeto");
                                dt.Columns.Add("nivelEstudios");
                                dt.Columns.Add("tipoVivienda");
                                dt.Columns.Add("nivelEstudioEsperado");
                                dt.Columns.Add("numParticipantesCredito");
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion armaTabla
                        #region lecturaArchivo
                        if (error == "OK")
                        {
                            try
                            {
                                srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
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
                                                DataRow row = dt.NewRow();

                                                try { row["tipoIdentificacionSujeto"] = CampoTexto(campos[0].Trim()); }
                                                catch { row["tipoIdentificacionSujeto"] = "X"; }

                                                try { row["identificacionSujeto"] = CampoTexto(campos[1].Trim()); }
                                                catch { row["identificacionSujeto"] = "XXXXXXXXXX"; }

                                                try { row["numeroOperacion"] = CampoTexto(campos[2].Trim()); }
                                                catch { row["numeroOperacion"] = "XXXXXXXXXX"; }

                                                try { row["valorOperacion"] = CampoValor(campos[3].Trim()); }
                                                catch { row["valorOperacion"] = "0"; }

                                                try { row["tasaInteresNominal"] = CampoValor(campos[4].Trim()); }
                                                catch { row["tasaInteresNominal"] = "0"; }

                                                try { row["tasaEfectivaAnual"] = CampoValor(campos[5].Trim()); }
                                                catch { row["tasaEfectivaAnual"] = "0"; }

                                                try { row["fechaConcesion"] = CampoFecha(campos[6].Trim()); }
                                                catch { row["fechaConcesion"] = "00/00/0000"; }

                                                try { row["fechaVencimiento"] = CampoFecha(campos[7].Trim()); }
                                                catch { row["fechaVencimiento"] = "00/00/0000"; }

                                                try { row["lineaCredito"] = CampoTexto(campos[8].Trim()); }
                                                catch { row["lineaCredito"] = "X"; }

                                                try { row["periodicidadPago"] = CampoTexto(campos[9].Trim()); }
                                                catch { row["periodicidadPago"] = "XX"; }

                                                try { row["frecuenciaRevision"] = CampoTexto(campos[10].Trim()); }
                                                catch { row["frecuenciaRevision"] = "X"; }

                                                try { row["oficinaConcesion"] = CampoTexto(campos[11].Trim()); }
                                                catch { row["oficinaConcesion"] = "XXXXX"; }

                                                try { row["garante"] = CampoTexto(campos[12].Trim()); }
                                                catch { row["garante"] = "XX"; }

                                                try { row["tipoCredito"] = CampoTexto(campos[13].Trim()); }
                                                catch { row["tipoCredito"] = "XX"; }

                                                try { row["claseCredito"] = CampoTexto(campos[14].Trim()); }
                                                catch { row["claseCredito"] = "X"; }

                                                try { row["estadoOperacion"] = CampoTexto(campos[15].Trim()); }
                                                catch { row["estadoOperacion"] = "X"; }

                                                try { row["numeroOperacionAnterior"] = CampoTexto(campos[16].Trim()); }
                                                catch { row["numeroOperacionAnterior"] = "XXXXXXXXXX"; }

                                                try { row["origenOperacion"] = CampoTexto(campos[17].Trim()); }
                                                catch { row["origenOperacion"] = "X"; }

                                                try { row["tipoOperacion"] = CampoTexto(campos[18].Trim()); }
                                                catch { row["tipoOperacion"] = "XXX"; }

                                                try { row["causalVinculacion"] = CampoTexto(campos[19].Trim()); }
                                                catch { row["causalVinculacion"] = "XXX"; }

                                                try { row["destinoFinanciero"] = CampoTexto(campos[20].Trim()); }
                                                catch { row["destinoFinanciero"] = "XX"; }

                                                try { row["actividadEconomica"] = CampoTexto(campos[21].Trim()); }
                                                catch { row["actividadEconomica"] = "XXXXX"; }

                                                try { row["geograficoProvincia"] = CampoTexto(campos[22].Trim()); }
                                                catch { row["geograficoProvincia"] = "XX"; }

                                                try { row["geograficoCanton"] = CampoTexto(campos[23].Trim()); }
                                                catch { row["geograficoCanton"] = "XX"; }

                                                try { row["geograficoParroquia"] = CampoTexto(campos[24].Trim()); }
                                                catch { row["geograficoParroquia"] = "XX"; }

                                                try { row["totalIngresosSujeto"] = CampoValor(campos[25].Trim()); }
                                                catch { row["totalIngresosSujeto"] = "0"; }

                                                try { row["totalEgresosSujeto"] = CampoValor(campos[26].Trim()); }
                                                catch { row["totalEgresosSujeto"] = "0"; }

                                                try { row["patrimonioSujeto"] = CampoValor(campos[27].Trim()); }
                                                catch { row["patrimonioSujeto"] = "0"; }

                                                try { row["actividadSujeto"] = CampoTexto(campos[28].Trim()); }
                                                catch { row["actividadSujeto"] = "XXX"; }

                                                try { row["nivelEstudios"] = CampoTexto(campos[29].Trim()); }
                                                catch { row["nivelEstudios"] = "X"; }

                                                try { row["tipoVivienda"] = CampoTexto(campos[30].Trim()); }
                                                catch { row["tipoVivienda"] = "X"; }

                                                try { row["nivelEstudioEsperado"] = CampoTexto(campos[31].Trim()); }
                                                catch { row["nivelEstudioEsperado"] = "X"; }

                                                try { row["numParticipantesCredito"] = CampoTexto(campos[32].Trim()); }
                                                catch { row["numParticipantesCredito"] = "X"; }

                                                dt.Rows.Add(row);
                                            }
                                            else
                                            {
                                                try { ruc = ConfigurationManager.AppSettings["coop29Ruc"].Trim(); }
                                                catch { ruc = "1790567699001"; }

                                                try { numeroRegistros = Convert.ToInt64(campos[3].Trim()); }
                                                catch { numeroRegistros = 0; }

                                                try { fechaEstructura = DateTime.Parse(campos[2].Trim()); }
                                                catch { fechaEstructura = DateTime.Now; }
                                            }
                                        }
                                    }
                                    catch { }
                                    numeroLinea++;
                                }
                                srArchivo.Close();
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion lecturaArchivo
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            operacion ope = new operacion();
                            ope.estructura = tipoEstructura;
                            ope.rucEntidad = ruc;
                            ope.fechaCorte = fechaEstructura.ToString("dd/MM/yyyy");
                            ope.numRegistro = numeroRegistros.ToString();
                            ope.elemento = new elementoOperacion[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            ope.elemento = new elementoOperacion[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                ope.elemento[x] = new elementoOperacion();
                                if (dr[x].ItemArray[0].ToString() != "") ope.elemento[x].tipoIdentificacionSujeto = dr[x].ItemArray[0].ToString();
                                if (dr[x].ItemArray[1].ToString() != "") ope.elemento[x].identificacionSujeto = dr[x].ItemArray[1].ToString();
                                if (dr[x].ItemArray[2].ToString() != "") ope.elemento[x].numeroOperacion = dr[x].ItemArray[2].ToString();
                                if (dr[x].ItemArray[3].ToString() != "") ope.elemento[x].valorOperacion = dr[x].ItemArray[3].ToString();
                                if (dr[x].ItemArray[4].ToString() != "") ope.elemento[x].tasaInteresNominal = dr[x].ItemArray[4].ToString();
                                if (dr[x].ItemArray[5].ToString() != "") ope.elemento[x].tasaEfectivaAnual = dr[x].ItemArray[5].ToString();
                                if (dr[x].ItemArray[6].ToString() != "") ope.elemento[x].fechaConcesion = dr[x].ItemArray[6].ToString();
                                if (dr[x].ItemArray[7].ToString() != "") ope.elemento[x].fechaVencimiento = dr[x].ItemArray[7].ToString();
                                if (dr[x].ItemArray[8].ToString() != "") ope.elemento[x].lineaCredito = dr[x].ItemArray[8].ToString();
                                if (dr[x].ItemArray[9].ToString() != "") ope.elemento[x].periodicidadPago = dr[x].ItemArray[9].ToString();
                                if (dr[x].ItemArray[10].ToString() != "") ope.elemento[x].frecuenciaRevision = dr[x].ItemArray[10].ToString();
                                if (dr[x].ItemArray[11].ToString() != "") ope.elemento[x].oficinaConcesion = dr[x].ItemArray[11].ToString();
                                if (dr[x].ItemArray[12].ToString() != "") ope.elemento[x].garante = dr[x].ItemArray[12].ToString();
                                if (dr[x].ItemArray[13].ToString() != "") ope.elemento[x].tipoCredito = dr[x].ItemArray[13].ToString();
                                if (dr[x].ItemArray[14].ToString() != "") ope.elemento[x].claseCredito = dr[x].ItemArray[14].ToString();
                                if (dr[x].ItemArray[15].ToString() != "") ope.elemento[x].estadoOperacion = dr[x].ItemArray[15].ToString();
                                if (dr[x].ItemArray[16].ToString() != "") ope.elemento[x].numeroOperacionAnterior = dr[x].ItemArray[16].ToString();
                                if (dr[x].ItemArray[17].ToString() != "") ope.elemento[x].origenOperacion = dr[x].ItemArray[17].ToString();
                                if (dr[x].ItemArray[18].ToString() != "") ope.elemento[x].tipoOperacion = dr[x].ItemArray[18].ToString();
                                if (dr[x].ItemArray[19].ToString() != "") ope.elemento[x].causalVinculacion = dr[x].ItemArray[19].ToString();
                                if (dr[x].ItemArray[20].ToString() != "") ope.elemento[x].destinoFinanciero = dr[x].ItemArray[20].ToString();
                                if (dr[x].ItemArray[21].ToString() != "") ope.elemento[x].actividadEconomica = dr[x].ItemArray[21].ToString();
                                if (dr[x].ItemArray[22].ToString() != "") ope.elemento[x].geograficoProvincia = dr[x].ItemArray[22].ToString();
                                if (dr[x].ItemArray[23].ToString() != "") ope.elemento[x].geograficoCanton = dr[x].ItemArray[23].ToString();
                                if (dr[x].ItemArray[24].ToString() != "") ope.elemento[x].geograficoParroquia = dr[x].ItemArray[24].ToString();
                                if (dr[x].ItemArray[25].ToString() != "") ope.elemento[x].totalIngresosSujeto = dr[x].ItemArray[25].ToString();
                                if (dr[x].ItemArray[26].ToString() != "") ope.elemento[x].totalEgresosSujeto = dr[x].ItemArray[26].ToString();
                                if (dr[x].ItemArray[27].ToString() != "") ope.elemento[x].patrimonioSujeto = dr[x].ItemArray[27].ToString();
                                if (dr[x].ItemArray[28].ToString() != "") ope.elemento[x].actividadSujeto = dr[x].ItemArray[28].ToString();
                                if (dr[x].ItemArray[29].ToString() != "") ope.elemento[x].nivelEstudios = dr[x].ItemArray[29].ToString();
                                if (dr[x].ItemArray[30].ToString() != "") ope.elemento[x].tipoVivienda = dr[x].ItemArray[30].ToString();
                                if (dr[x].ItemArray[31].ToString() != "") ope.elemento[x].nivelEstudioEsperado = dr[x].ItemArray[31].ToString();
                                if (dr[x].ItemArray[32].ToString() != "") ope.elemento[x].numParticipantesCredito = dr[x].ItemArray[32].ToString();
                            }
                            estructuraXml = C01ToString(ope).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA";
                        }
                        #endregion armaEstructura
                        break;
                    #endregion C01
                    #region C02
                    case "C02":
                        error = "OK";
                        #region armaTabla
                        if (error == "OK")
                        {
                            try
                            {
                                dt.Clear();
                                dt.Columns.Add("tipoIdentificacionSujeto");
                                dt.Columns.Add("identificacionSujeto");
                                dt.Columns.Add("numeroOperacion");
                                dt.Columns.Add("diasMorosidad");
                                dt.Columns.Add("calificacion");
                                dt.Columns.Add("calificacionHomologada");
                                dt.Columns.Add("tasaInteresEfectiva");
                                dt.Columns.Add("tipoAmortizacion");
                                dt.Columns.Add("valorVencer1_30");
                                dt.Columns.Add("valorVencer31_90");
                                dt.Columns.Add("valorVencer91_180");
                                dt.Columns.Add("valorVencer181_360");
                                dt.Columns.Add("valorVencerMas360");
                                dt.Columns.Add("valorNoDevInteres1_30");
                                dt.Columns.Add("valorNoDevInteres31_90");
                                dt.Columns.Add("valorNoDevInteres91_180");
                                dt.Columns.Add("valorNoDevInteres181_360");
                                dt.Columns.Add("valorNoDevInteresMas360");
                                dt.Columns.Add("valorVencido1_30");
                                dt.Columns.Add("valorVencido31_90");
                                dt.Columns.Add("valorVencido91_180");
                                dt.Columns.Add("valorVencido181_360");
                                dt.Columns.Add("valorVencidoMas360");
                                dt.Columns.Add("valorVencido181_270");
                                dt.Columns.Add("valorVencidoMas270");
                                dt.Columns.Add("valorVencido91_270");
                                dt.Columns.Add("valorVencido271_360");
                                dt.Columns.Add("valorVencido361_720");
                                dt.Columns.Add("valorVencidoMas720");
                                dt.Columns.Add("saldoTotal");
                                dt.Columns.Add("formaCancelacion");
                                dt.Columns.Add("gestionRecuperCarteraVencida");
                                dt.Columns.Add("gastosJudicRecuperCarteraVencida");
                                dt.Columns.Add("interesOrdinario");
                                dt.Columns.Add("interesMora");
                                dt.Columns.Add("valorDemandaJudicial");
                                dt.Columns.Add("carteraCastidada");
                                dt.Columns.Add("fechaCastigo");
                                dt.Columns.Add("provisionEspecifica");
                                dt.Columns.Add("provisionRequeridaReducida");
                                dt.Columns.Add("provisionConstituida");
                                dt.Columns.Add("tipoOperacion");
                                dt.Columns.Add("objetoFideicomiso");
                                dt.Columns.Add("prima");
                                dt.Columns.Add("cuotaCredito");
                                dt.Columns.Add("fechaUltimaCuotaCompPagada");
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion armaTabla
                        #region lecturaArchivo
                        if (error == "OK")
                        {
                            try
                            {
                                srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
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
                                                DataRow row = dt.NewRow();

                                                try { row["tipoIdentificacionSujeto"] = CampoTexto(campos[0].Trim()); }
                                                catch { row["tipoIdentificacionSujeto"] = "X"; }

                                                try { row["identificacionSujeto"] = CampoTexto(campos[1].Trim()); }
                                                catch { row["identificacionSujeto"] = "XXXXXXXXXX"; }

                                                try { row["numeroOperacion"] = CampoTexto(campos[2].Trim()); }
                                                catch { row["numeroOperacion"] = "XXXXXXXXXX"; }

                                                try { row["diasMorosidad"] = campos[3].Trim(); }
                                                catch { row["diasMorosidad"] = "0"; }

                                                try { row["calificacion"] = CampoTexto(campos[4].Trim()); }
                                                catch { row["calificacion"] = "XX"; }

                                                try { row["calificacionHomologada"] = CampoTexto(campos[5].Trim()); }
                                                catch { row["calificacionHomologada"] = "XX"; }

                                                try { row["tasaInteresEfectiva"] = CampoValor(campos[6].Trim()); }
                                                catch { row["tasaInteresEfectiva"] = "0"; }

                                                try { row["tipoAmortizacion"] = CampoTexto(campos[7].Trim()); }
                                                catch { row["tipoAmortizacion"] = "XX"; }

                                                try { row["valorVencer1_30"] = CampoValor(campos[8].Trim()); }
                                                catch { row["valorVencer1_30"] = "0"; }

                                                try { row["valorVencer31_90"] = CampoValor(campos[9].Trim()); }
                                                catch { row["valorVencer31_90"] = "0"; }

                                                try { row["valorVencer91_180"] = CampoValor(campos[10].Trim()); }
                                                catch { row["valorVencer91_180"] = "0"; }

                                                try { row["valorVencer181_360"] = CampoValor(campos[11].Trim()); }
                                                catch { row["valorVencer181_360"] = "0"; }

                                                try { row["valorVencerMas360"] = CampoValor(campos[12].Trim()); }
                                                catch { row["valorVencerMas360"] = "0"; }

                                                try { row["valorNoDevInteres1_30"] = CampoValor(campos[13].Trim()); }
                                                catch { row["valorNoDevInteres1_30"] = "0"; }

                                                try { row["valorNoDevInteres31_90"] = CampoValor(campos[14].Trim()); }
                                                catch { row["valorNoDevInteres31_90"] = "0"; }

                                                try { row["valorNoDevInteres91_180"] = CampoValor(campos[15].Trim()); }
                                                catch { row["valorNoDevInteres91_180"] = "0"; }

                                                try { row["valorNoDevInteres181_360"] = CampoValor(campos[16].Trim()); }
                                                catch { row["valorNoDevInteres181_360"] = "0"; }

                                                try { row["valorNoDevInteresMas360"] = CampoValor(campos[17].Trim()); }
                                                catch { row["valorNoDevInteresMas360"] = "0"; }

                                                try { row["valorVencido1_30"] = CampoValor(campos[18].Trim()); }
                                                catch { row["valorVencido1_30"] = "0"; }

                                                try { row["valorVencido31_90"] = CampoValor(campos[19].Trim()); }
                                                catch { row["valorVencido31_90"] = "0"; }

                                                try { row["valorVencido91_180"] = CampoValor(campos[20].Trim()); }
                                                catch { row["valorVencido91_180"] = "0"; }

                                                try { row["valorVencido181_360"] = CampoValor(campos[21].Trim()); }
                                                catch { row["valorVencido181_360"] = "0"; }

                                                try { row["valorVencidoMas360"] = CampoValor(campos[22].Trim()); }
                                                catch { row["valorVencidoMas360"] = "0"; }

                                                try { row["valorVencido181_270"] = CampoValor(campos[23].Trim()); }
                                                catch { row["valorVencido181_270"] = "0"; }

                                                try { row["valorVencidoMas270"] = CampoValor(campos[24].Trim()); }
                                                catch { row["valorVencidoMas270"] = "0"; }

                                                try { row["valorVencido91_270"] = CampoValor(campos[25].Trim()); }
                                                catch { row["valorVencido91_270"] = "0"; }

                                                try { row["valorVencido271_360"] = CampoValor(campos[26].Trim()); }
                                                catch { row["valorVencido271_360"] = "0"; }

                                                try { row["valorVencido361_720"] = CampoValor(campos[27].Trim()); }
                                                catch { row["valorVencido361_720"] = "0"; }

                                                try { row["valorVencidoMas720"] = CampoValor(campos[28].Trim()); }
                                                catch { row["valorVencidoMas720"] = "0"; }

                                                try { row["saldoTotal"] = CampoValor(campos[29].Trim()); }
                                                catch { row["saldoTotal"] = "0"; }

                                                try { row["formaCancelacion"] = CampoTexto(campos[30].Trim()); }
                                                catch { row["formaCancelacion"] = "X"; }

                                                try { row["gestionRecuperCarteraVencida"] = CampoValor(campos[31].Trim()); }
                                                catch { row["gestionRecuperCarteraVencida"] = "0"; }

                                                try { row["gastosJudicRecuperCarteraVencida"] = CampoValor(campos[32].Trim()); }
                                                catch { row["gastosJudicRecuperCarteraVencida"] = "0"; }

                                                try { row["interesOrdinario"] = CampoValor(campos[33].Trim()); }
                                                catch { row["interesOrdinario"] = "0"; }

                                                try { row["interesMora"] = CampoValor(campos[34].Trim()); }
                                                catch { row["interesMora"] = "0"; }

                                                try { row["valorDemandaJudicial"] = CampoValor(campos[35].Trim()); }
                                                catch { row["valorDemandaJudicial"] = "0"; }

                                                try { row["carteraCastidada"] = CampoValor(campos[36].Trim()); }
                                                catch { row["carteraCastidada"] = "0"; }

                                                try { row["fechaCastigo"] = CampoFecha(campos[37].Trim()); }
                                                catch { row["fechaCastigo"] = "00/00/0000"; }

                                                try { row["provisionEspecifica"] = CampoValor(campos[38].Trim()); }
                                                catch { row["provisionEspecifica"] = "0"; }

                                                try { row["provisionRequeridaReducida"] = CampoValor(campos[39].Trim()); }
                                                catch { row["provisionRequeridaReducida"] = "0"; }

                                                try { row["provisionConstituida"] = CampoValor(campos[40].Trim()); }
                                                catch { row["provisionConstituida"] = "0"; }

                                                try { row["tipoOperacion"] = CampoTexto(campos[41].Trim()); }
                                                catch { row["tipoOperacion"] = "XXX"; }

                                                try { row["objetoFideicomiso"] = campos[42].Trim(); }
                                                catch { row["objetoFideicomiso"] = "0"; }

                                                try { row["prima"] = CampoValor(campos[43].Trim()); }
                                                catch { row["prima"] = "0"; }

                                                try { row["cuotaCredito"] = CampoValor(campos[44].Trim()); }
                                                catch { row["cuotaCredito"] = "0"; }

                                                try { row["fechaUltimaCuotaCompPagada"] = CampoFecha(campos[45].Trim()); }
                                                catch { row["fechaUltimaCuotaCompPagada"] = "00/00/0000"; }

                                                dt.Rows.Add(row);
                                            }
                                            else
                                            {
                                                try { ruc = ConfigurationManager.AppSettings["coop29Ruc"].Trim(); }
                                                catch { ruc = "1790567699001"; }

                                                try { numeroRegistros = Convert.ToInt64(campos[3].Trim()); }
                                                catch { numeroRegistros = 0; }

                                                try { fechaEstructura = DateTime.Parse(campos[2].Trim()); }
                                                catch { fechaEstructura = DateTime.Now; }
                                            }
                                        }
                                    }
                                    catch { }
                                    numeroLinea++;
                                }
                                srArchivo.Close();
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion lecturaArchivo
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            saldo sl = new saldo();
                            sl.estructura = tipoEstructura;
                            sl.rucEntidad = ruc;
                            sl.fechaCorte = fechaEstructura.ToString("dd/MM/yyyy");
                            sl.numRegistro = numeroRegistros.ToString();
                            sl.elemento = new elementoSaldo[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            sl.elemento = new elementoSaldo[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                sl.elemento[x] = new elementoSaldo();
                                if (dr[x].ItemArray[0].ToString() != "") { sl.elemento[x].tipoIdentificacionSujeto = dr[x].ItemArray[0].ToString(); }
                                if (dr[x].ItemArray[1].ToString() != "") { sl.elemento[x].identificacionSujeto = dr[x].ItemArray[1].ToString(); }
                                if (dr[x].ItemArray[2].ToString() != "") { sl.elemento[x].numeroOperacion = dr[x].ItemArray[2].ToString(); }
                                if (dr[x].ItemArray[3].ToString() != "") { sl.elemento[x].diasMorosidad = dr[x].ItemArray[3].ToString(); }
                                if (dr[x].ItemArray[4].ToString() != "") { sl.elemento[x].calificacion = dr[x].ItemArray[4].ToString(); }
                                if (dr[x].ItemArray[5].ToString() != "") { sl.elemento[x].calificacionHomologada = dr[x].ItemArray[5].ToString(); }
                                if (dr[x].ItemArray[6].ToString() != "") { sl.elemento[x].tasaInteresEfectiva = dr[x].ItemArray[6].ToString(); }
                                if (dr[x].ItemArray[7].ToString() != "") { sl.elemento[x].tipoAmortizacion = dr[x].ItemArray[7].ToString(); }
                                if (dr[x].ItemArray[8].ToString() != "") { sl.elemento[x].valorVencer1_30 = dr[x].ItemArray[8].ToString(); }
                                if (dr[x].ItemArray[9].ToString() != "") { sl.elemento[x].valorVencer31_90 = dr[x].ItemArray[9].ToString(); }
                                if (dr[x].ItemArray[10].ToString() != "") { sl.elemento[x].valorVencer91_180 = dr[x].ItemArray[10].ToString(); }
                                if (dr[x].ItemArray[11].ToString() != "") { sl.elemento[x].valorVencer181_360 = dr[x].ItemArray[11].ToString(); }
                                if (dr[x].ItemArray[12].ToString() != "") { sl.elemento[x].valorVencerMas360 = dr[x].ItemArray[12].ToString(); }
                                if (dr[x].ItemArray[13].ToString() != "") { sl.elemento[x].valorNoDevInteres1_30 = dr[x].ItemArray[13].ToString(); }
                                if (dr[x].ItemArray[14].ToString() != "") { sl.elemento[x].valorNoDevInteres31_90 = dr[x].ItemArray[14].ToString(); }
                                if (dr[x].ItemArray[15].ToString() != "") { sl.elemento[x].valorNoDevInteres91_180 = dr[x].ItemArray[15].ToString(); }
                                if (dr[x].ItemArray[16].ToString() != "") { sl.elemento[x].valorNoDevInteres181_360 = dr[x].ItemArray[16].ToString(); }
                                if (dr[x].ItemArray[17].ToString() != "") { sl.elemento[x].valorNoDevInteresMas360 = dr[x].ItemArray[17].ToString(); }
                                if (dr[x].ItemArray[18].ToString() != "") { sl.elemento[x].valorVencido1_30 = dr[x].ItemArray[18].ToString(); }
                                if (dr[x].ItemArray[19].ToString() != "") { sl.elemento[x].valorVencido31_90 = dr[x].ItemArray[19].ToString(); }
                                if (dr[x].ItemArray[20].ToString() != "") { sl.elemento[x].valorVencido91_180 = dr[x].ItemArray[20].ToString(); }
                                if (dr[x].ItemArray[21].ToString() != "") { sl.elemento[x].valorVencido181_360 = dr[x].ItemArray[21].ToString(); }
                                if (dr[x].ItemArray[22].ToString() != "") { sl.elemento[x].valorVencidoMas360 = dr[x].ItemArray[22].ToString(); }
                                if (dr[x].ItemArray[23].ToString() != "") { sl.elemento[x].valorVencido181_270 = dr[x].ItemArray[23].ToString(); }
                                if (dr[x].ItemArray[24].ToString() != "") { sl.elemento[x].valorVencidoMas270 = dr[x].ItemArray[24].ToString(); }
                                if (dr[x].ItemArray[25].ToString() != "") { sl.elemento[x].valorVencido91_270 = dr[x].ItemArray[25].ToString(); }
                                if (dr[x].ItemArray[26].ToString() != "") { sl.elemento[x].valorVencido271_360 = dr[x].ItemArray[26].ToString(); }
                                if (dr[x].ItemArray[27].ToString() != "") { sl.elemento[x].valorVencido361_720 = dr[x].ItemArray[27].ToString(); }
                                if (dr[x].ItemArray[28].ToString() != "") { sl.elemento[x].valorVencidoMas720 = dr[x].ItemArray[28].ToString(); }
                                if (dr[x].ItemArray[29].ToString() != "") { sl.elemento[x].saldoTotal = dr[x].ItemArray[29].ToString(); }
                                if (dr[x].ItemArray[30].ToString() != "") { sl.elemento[x].formaCancelacion = dr[x].ItemArray[30].ToString(); }
                                if (dr[x].ItemArray[31].ToString() != "") { sl.elemento[x].gestionRecuperCarteraVencida = dr[x].ItemArray[31].ToString(); }
                                if (dr[x].ItemArray[32].ToString() != "") { sl.elemento[x].gastosJudicRecuperCarteraVencida = dr[x].ItemArray[32].ToString(); }
                                if (dr[x].ItemArray[33].ToString() != "") { sl.elemento[x].interesOrdinario = dr[x].ItemArray[33].ToString(); }
                                if (dr[x].ItemArray[34].ToString() != "") { sl.elemento[x].interesMora = dr[x].ItemArray[34].ToString(); }
                                if (dr[x].ItemArray[35].ToString() != "") { sl.elemento[x].valorDemandaJudicial = dr[x].ItemArray[35].ToString(); }
                                if (dr[x].ItemArray[36].ToString() != "") { sl.elemento[x].carteraCastidada = dr[x].ItemArray[36].ToString(); }
                                if (dr[x].ItemArray[37].ToString() != "") { sl.elemento[x].fechaCastigo = dr[x].ItemArray[37].ToString(); }
                                if (dr[x].ItemArray[38].ToString() != "") { sl.elemento[x].provisionEspecifica = dr[x].ItemArray[38].ToString(); }
                                if (dr[x].ItemArray[39].ToString() != "") { sl.elemento[x].provisionRequeridaReducida = dr[x].ItemArray[39].ToString(); }
                                if (dr[x].ItemArray[40].ToString() != "") { sl.elemento[x].provisionConstituida = dr[x].ItemArray[40].ToString(); }
                                if (dr[x].ItemArray[41].ToString() != "") { sl.elemento[x].tipoOperacion = dr[x].ItemArray[41].ToString(); }
                                if (dr[x].ItemArray[42].ToString() != "") { sl.elemento[x].objetoFideicomiso = dr[x].ItemArray[42].ToString(); }
                                if (dr[x].ItemArray[43].ToString() != "") { sl.elemento[x].prima = dr[x].ItemArray[43].ToString(); }
                                if (dr[x].ItemArray[44].ToString() != "") { sl.elemento[x].cuotaCredito = dr[x].ItemArray[44].ToString(); }
                                //if (dr[x].ItemArray[45].ToString() != "") { sl.elemento[x].fechaUltimaCuotaCompPagada = dr[x].ItemArray[45].ToString(); }
                                sl.elemento[x].fechaUltimaCuotaCompPagada = dr[x].ItemArray[45].ToString();
                            }
                            estructuraXml = C02ToString(sl).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA";
                        }
                        #endregion armaEstructura
                        break;
                    #endregion C02
                    #region C03
                    case "C03":
                        error = "OK";
                        #region armaTabla
                        if (error == "OK")
                        {
                            try
                            {
                                dt.Clear();
                                dt.Columns.Add("tipoIdentificacionSujeto");
                                dt.Columns.Add("identificacionSujeto");
                                dt.Columns.Add("numeroOperacion");
                                dt.Columns.Add("tipoGaranteCodeudor");
                                dt.Columns.Add("tipoIdentGaranteCodeudor");
                                dt.Columns.Add("identGaranteCodeudor");
                                dt.Columns.Add("causaElimGaranteCodeudor");
                                dt.Columns.Add("fechElimGaranteCodeudor");
                                dt.Columns.Add("numeroGarantia");
                                dt.Columns.Add("tipoGarantia");
                                dt.Columns.Add("ubicacionGarantiaProvincia");
                                dt.Columns.Add("ubicacionGarantiaCanton");
                                dt.Columns.Add("ubicacionGarantiaParroquia");
                                dt.Columns.Add("valorAvaluo");
                                dt.Columns.Add("valorAvaluoComercial");
                                dt.Columns.Add("valorAvaluoCatastral");
                                dt.Columns.Add("fechaAvaluo");
                                dt.Columns.Add("numeroRegistroGarantia");
                                dt.Columns.Add("fechaContabGarantia");
                                dt.Columns.Add("porcentajeCubreGarantia");
                                dt.Columns.Add("estadoRegistro");
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion armaTabla
                        #region lecturaArchivo
                        if (error == "OK")
                        {
                            try
                            {
                                srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
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
                                                DataRow row = dt.NewRow();

                                                try { row["tipoIdentificacionSujeto"] = CampoTexto(campos[0].Trim()); }
                                                catch { row["tipoIdentificacionSujeto"] = "X"; }

                                                try { row["identificacionSujeto"] = CampoTexto(campos[1].Trim()); }
                                                catch { row["identificacionSujeto"] = "XXXXXXXXXX"; }

                                                try { row["numeroOperacion"] = CampoTexto(campos[2].Trim()); }
                                                catch { row["numeroOperacion"] = "XXXXXXXXXX"; }

                                                try { row["tipoGaranteCodeudor"] = CampoTexto(campos[3].Trim()); }
                                                catch { row["tipoGaranteCodeudor"] = "X"; }

                                                try { row["tipoIdentGaranteCodeudor"] = CampoTexto(campos[4].Trim()); }
                                                catch { row["tipoIdentGaranteCodeudor"] = "X"; }

                                                try { row["identGaranteCodeudor"] = CampoTexto(campos[5].Trim()); }
                                                catch { row["identGaranteCodeudor"] = "XXXXXXXXXX"; }

                                                try { row["causaElimGaranteCodeudor"] = CampoTexto(campos[6].Trim()); }
                                                catch { row["causaElimGaranteCodeudor"] = "X"; }

                                                try { row["fechElimGaranteCodeudor"] = CampoFecha(campos[7].Trim()); }
                                                catch { row["fechElimGaranteCodeudor"] = "00/00/0000"; }

                                                try { row["numeroGarantia"] = CampoTexto(campos[8].Trim()); }
                                                catch { row["numeroGarantia"] = "XXXXXXXXXX"; }

                                                try { row["tipoGarantia"] = CampoTexto(campos[9].Trim()); }
                                                catch { row["tipoGarantia"] = "XXX"; }

                                                try { row["ubicacionGarantiaProvincia"] = CampoTexto(campos[10].Trim()); }
                                                catch { row["ubicacionGarantiaProvincia"] = "XX"; }

                                                try { row["ubicacionGarantiaCanton"] = CampoTexto(campos[11].Trim()); }
                                                catch { row["ubicacionGarantiaCanton"] = "XX"; }

                                                try { row["ubicacionGarantiaParroquia"] = CampoTexto(campos[12].Trim()); }
                                                catch { row["ubicacionGarantiaParroquia"] = "XX"; }

                                                try { row["valorAvaluo"] = CampoValor(campos[13].Trim()); }
                                                catch { row["valorAvaluo"] = "0"; }

                                                try { row["valorAvaluoComercial"] = CampoValor(campos[14].Trim()); }
                                                catch { row["valorAvaluoComercial"] = "0"; }

                                                try { row["valorAvaluoCatastral"] = CampoValor(campos[15].Trim()); }
                                                catch { row["valorAvaluoCatastral"] = "0"; }

                                                try { row["fechaAvaluo"] = CampoFecha(campos[16].Trim()); }
                                                catch { row["fechaAvaluo"] = "00/00/0000"; }

                                                try { row["numeroRegistroGarantia"] = CampoTexto(campos[17].Trim()); }
                                                catch { row["numeroRegistroGarantia"] = "X"; }

                                                try { row["fechaContabGarantia"] = CampoFecha(campos[18].Trim()); }
                                                catch { row["fechaContabGarantia"] = "00/00/0000"; }

                                                try { row["porcentajeCubreGarantia"] = CampoValor(campos[19].Trim()); }
                                                catch { row["porcentajeCubreGarantia"] = "0"; }

                                                try { row["estadoRegistro"] = CampoTexto(campos[20].Trim()); }
                                                catch { row["estadoRegistro"] = "X"; }

                                                dt.Rows.Add(row);
                                            }
                                            else
                                            {
                                                try { ruc = ConfigurationManager.AppSettings["coop29Ruc"].Trim(); }
                                                catch { ruc = "1790567699001"; }

                                                try { numeroRegistros = Convert.ToInt64(campos[3].Trim()); }
                                                catch { numeroRegistros = 0; }

                                                try { fechaEstructura = DateTime.Parse(campos[2].Trim()); }
                                                catch { fechaEstructura = DateTime.Now; }
                                            }
                                        }
                                    }
                                    catch { }
                                    numeroLinea++;
                                }
                                srArchivo.Close();
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion lecturaArchivo
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            garantia gar = new garantia();
                            gar.estructura = tipoEstructura;
                            gar.rucEntidad = ruc;
                            gar.fechaCorte = fechaEstructura.ToString("dd/MM/yyyy");
                            gar.numRegistro = numeroRegistros.ToString();
                            gar.elemento = new elementoGarantia[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            gar.elemento = new elementoGarantia[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                gar.elemento[x] = new elementoGarantia();

                                if (dr[x].ItemArray[0].ToString() != "") { gar.elemento[x].tipoIdentificacionSujeto = dr[x].ItemArray[0].ToString(); }
                                if (dr[x].ItemArray[1].ToString() != "") { gar.elemento[x].identificacionSujeto = dr[x].ItemArray[1].ToString(); }
                                if (dr[x].ItemArray[2].ToString() != "") { gar.elemento[x].numeroOperacion = dr[x].ItemArray[2].ToString(); }
                                if (dr[x].ItemArray[3].ToString() != "") { gar.elemento[x].tipoGaranteCodeudor = dr[x].ItemArray[3].ToString(); }
                                if (dr[x].ItemArray[4].ToString() != "") { gar.elemento[x].tipoIdentGaranteCodeudor = dr[x].ItemArray[4].ToString(); }
                                if (dr[x].ItemArray[5].ToString() != "") { gar.elemento[x].identGaranteCodeudor = dr[x].ItemArray[5].ToString(); }
                                if (dr[x].ItemArray[6].ToString() != "") { gar.elemento[x].causaElimGaranteCodeudor = dr[x].ItemArray[6].ToString(); }
                                if (dr[x].ItemArray[7].ToString() != "") { gar.elemento[x].fechElimGaranteCodeudor = dr[x].ItemArray[7].ToString(); }
                                if (dr[x].ItemArray[8].ToString() != "") { gar.elemento[x].numeroGarantia = dr[x].ItemArray[8].ToString(); }
                                if (dr[x].ItemArray[9].ToString() != "") { gar.elemento[x].tipoGarantia = dr[x].ItemArray[9].ToString(); }
                                if (dr[x].ItemArray[10].ToString() != "") { gar.elemento[x].ubicacionGarantiaProvincia = dr[x].ItemArray[10].ToString(); }
                                if (dr[x].ItemArray[11].ToString() != "") { gar.elemento[x].ubicacionGarantiaCanton = dr[x].ItemArray[11].ToString(); }
                                if (dr[x].ItemArray[12].ToString() != "") { gar.elemento[x].ubicacionGarantiaParroquia = dr[x].ItemArray[12].ToString(); }
                                if (dr[x].ItemArray[13].ToString() != "") { gar.elemento[x].valorAvaluo = dr[x].ItemArray[13].ToString(); }
                                if (dr[x].ItemArray[14].ToString() != "") { gar.elemento[x].valorAvaluoComercial = dr[x].ItemArray[14].ToString(); }
                                if (dr[x].ItemArray[15].ToString() != "") { gar.elemento[x].valorAvaluoCatastral = dr[x].ItemArray[15].ToString(); }
                                if (dr[x].ItemArray[16].ToString() != "") { gar.elemento[x].fechaAvaluo = dr[x].ItemArray[16].ToString(); }
                                if (dr[x].ItemArray[17].ToString() != "") { gar.elemento[x].numeroRegistroGarantia = dr[x].ItemArray[17].ToString(); }
                                if (dr[x].ItemArray[18].ToString() != "") { gar.elemento[x].fechaContabGarantia = dr[x].ItemArray[18].ToString(); }
                                if (dr[x].ItemArray[19].ToString() != "") { gar.elemento[x].porcentajeCubreGarantia = dr[x].ItemArray[19].ToString(); }
                                if (dr[x].ItemArray[20].ToString() != "") { gar.elemento[x].estadoRegistro = dr[x].ItemArray[20].ToString(); }

                            }
                            estructuraXml = C03ToString(gar).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA";
                        }
                        #endregion armaEstructura
                        break;
                    #endregion C03
                    #region C04
                    case "C04":
                        error = "OK";
                        #region armaTabla
                        if (error == "OK")
                        {
                            try
                            {
                                dt.Clear();
                                dt.Columns.Add("tipoIdentificacionSujeto");
                                dt.Columns.Add("identificacionSujeto");
                                dt.Columns.Add("numeroOperacion");
                                dt.Columns.Add("codigoBien");
                                dt.Columns.Add("tipoBien");
                                dt.Columns.Add("nombreEmisor");
                                dt.Columns.Add("fechaEmision");
                                dt.Columns.Add("fechaVencimiento");
                                dt.Columns.Add("valorNominal");
                                dt.Columns.Add("fechaContabilizacion");
                                dt.Columns.Add("valorEnLibros");
                                dt.Columns.Add("valorUltimoAvaluo");
                                dt.Columns.Add("valorProvisionConst");
                                dt.Columns.Add("fechaRealizacion");
                                dt.Columns.Add("valorRealizacion");
                                dt.Columns.Add("estadoRegistro");
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion armaTabla
                        #region lecturaArchivo
                        if (error == "OK")
                        {
                            try
                            {
                                srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
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
                                                DataRow row = dt.NewRow();

                                                try { row["tipoIdentificacionSujeto"] = CampoTexto(campos[0].Trim()); }
                                                catch { row["tipoIdentificacionSujeto"] = "X"; }

                                                try { row["identificacionSujeto"] = CampoTexto(campos[1].Trim()); }
                                                catch { row["identificacionSujeto"] = "XXXXXXXXXX"; }

                                                try { row["numeroOperacion"] = CampoTexto(campos[2].Trim()); }
                                                catch { row["numeroOperacion"] = "XXXXXXXXXX"; }

                                                try { row["codigoBien"] = CampoTexto(campos[3].Trim()); }
                                                catch { row["codigoBien"] = "XXXXXXXXXX"; }

                                                try { row["tipoBien"] = CampoTexto(campos[4].Trim()); }
                                                catch { row["tipoBien"] = "XXX"; }

                                                try { row["nombreEmisor"] = CampoTexto(campos[5].Trim()); }
                                                catch { row["nombreEmisor"] = "XXXXXXXXXX"; }

                                                try { row["fechaEmision"] = CampoFecha(campos[6].Trim()); }
                                                catch { row["fechaEmision"] = "00/00/0000"; }

                                                try { row["fechaVencimiento"] = CampoFecha(campos[7].Trim()); }
                                                catch { row["fechaVencimiento"] = "00/00/0000"; }

                                                try { row["valorNominal"] = CampoValor(campos[8].Trim()); }
                                                catch { row["valorNominal"] = "0"; }

                                                try { row["fechaContabilizacion"] = CampoFecha(campos[9].Trim()); }
                                                catch { row["fechaContabilizacion"] = "00/00/0000"; }

                                                try { row["valorEnLibros"] = CampoValor(campos[10].Trim()); }
                                                catch { row["valorEnLibros"] = "0"; }

                                                try { row["valorUltimoAvaluo"] = CampoValor(campos[11].Trim()); }
                                                catch { row["valorUltimoAvaluo"] = "0"; }

                                                try { row["valorProvisionConst"] = CampoValor(campos[12].Trim()); }
                                                catch { row["valorProvisionConst"] = "0"; }

                                                try { row["fechaRealizacion"] = CampoFecha(campos[13].Trim()); }
                                                catch { row["fechaRealizacion"] = "00/00/0000"; }

                                                try { row["valorRealizacion"] = CampoValor(campos[14].Trim()); }
                                                catch { row["valorRealizacion"] = "0"; }

                                                try { row["estadoRegistro"] = CampoTexto(campos[15].Trim()); }
                                                catch { row["estadoRegistro"] = "X"; }

                                                dt.Rows.Add(row);
                                            }
                                            else
                                            {
                                                try { ruc = ConfigurationManager.AppSettings["coop29Ruc"].Trim(); }
                                                catch { ruc = "1790567699001"; }

                                                try { numeroRegistros = Convert.ToInt64(campos[3].Trim()); }
                                                catch { numeroRegistros = 0; }

                                                try { fechaEstructura = DateTime.Parse(campos[2].Trim()); }
                                                catch { fechaEstructura = DateTime.Now; }
                                            }
                                        }
                                    }
                                    catch { }
                                    numeroLinea++;
                                }
                                srArchivo.Close();
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion lecturaArchivo
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            bien bin = new bien();
                            bin.estructura = tipoEstructura;
                            bin.rucEntidad = ruc;
                            bin.fechaCorte = fechaEstructura.ToString("dd/MM/yyyy");
                            bin.numRegistro = numeroRegistros.ToString();
                            bin.elemento = new elementoBien[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            bin.elemento = new elementoBien[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                bin.elemento[x] = new elementoBien();
                                if (dr[x].ItemArray[0].ToString() != "") { bin.elemento[x].tipoIdentificacionSujeto = dr[x].ItemArray[0].ToString(); }
                                if (dr[x].ItemArray[1].ToString() != "") { bin.elemento[x].identificacionSujeto = dr[x].ItemArray[1].ToString(); }
                                if (dr[x].ItemArray[2].ToString() != "") { bin.elemento[x].numeroOperacion = dr[x].ItemArray[2].ToString(); }
                                if (dr[x].ItemArray[3].ToString() != "") { bin.elemento[x].codigoBien = dr[x].ItemArray[3].ToString(); }
                                if (dr[x].ItemArray[4].ToString() != "") { bin.elemento[x].tipoBien = dr[x].ItemArray[4].ToString(); }
                                if (dr[x].ItemArray[5].ToString() != "") { bin.elemento[x].nombreEmisor = dr[x].ItemArray[5].ToString(); }
                                if (dr[x].ItemArray[6].ToString() != "") { bin.elemento[x].fechaEmision = dr[x].ItemArray[6].ToString(); }
                                if (dr[x].ItemArray[7].ToString() != "") { bin.elemento[x].fechaVencimiento = dr[x].ItemArray[7].ToString(); }
                                if (dr[x].ItemArray[8].ToString() != "") { bin.elemento[x].valorNominal = dr[x].ItemArray[8].ToString(); }
                                if (dr[x].ItemArray[9].ToString() != "") { bin.elemento[x].fechaContabilizacion = dr[x].ItemArray[9].ToString(); }
                                if (dr[x].ItemArray[10].ToString() != "") { bin.elemento[x].valorEnLibros = dr[x].ItemArray[10].ToString(); }
                                if (dr[x].ItemArray[11].ToString() != "") { bin.elemento[x].valorUltimoAvaluo = dr[x].ItemArray[11].ToString(); }
                                if (dr[x].ItemArray[12].ToString() != "") { bin.elemento[x].valorProvisionConst = dr[x].ItemArray[12].ToString(); }
                                if (dr[x].ItemArray[13].ToString() != "") { bin.elemento[x].fechaRealizacion = dr[x].ItemArray[13].ToString(); }
                                if (dr[x].ItemArray[14].ToString() != "") { bin.elemento[x].valorRealizacion = dr[x].ItemArray[14].ToString(); }
                                if (dr[x].ItemArray[15].ToString() != "") { bin.elemento[x].estadoRegistro = dr[x].ItemArray[15].ToString(); }
                            }
                            estructuraXml = C04ToString(bin).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA";
                        }
                        #endregion armaEstructura
                        break;
                    #endregion C04
                    #region I01
                    case "I01":
                        error = "OK";
                        #region extraeData
                        dt = new BddAuxiliar().DataEstructurasI01(fechaCorte);
                        #endregion extraeData
                        #region armaEstructura
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            portafolio obj = new portafolio();
                            obj.estructura = tipoEstructura;
                            obj.rucEntidad = ruc;
                            obj.fechaCorte = fechaCorte.ToString("dd/MM/yyyy");
                            obj.numRegistro = (dt.Rows.Count + 1).ToString();
                            obj.elemento = new elementoPortafolio[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            obj.elemento = new elementoPortafolio[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                obj.elemento[x] = new elementoPortafolio();
                                if (dr[x].ItemArray[1].ToString() != "") { obj.elemento[x].numeroIdentificacionDeposito = CampoTexto(dr[x].ItemArray[1].ToString()); }
                                if (dr[x].ItemArray[2].ToString() != "") { obj.elemento[x].tipoIdentificacionEmisorDepositario = CampoTexto(dr[x].ItemArray[2].ToString()); }
                                if (dr[x].ItemArray[3].ToString() != "") { obj.elemento[x].identificacionEmisorDepositario = CampoTexto(dr[x].ItemArray[3].ToString()); }
                                if (dr[x].ItemArray[4].ToString() != "") { obj.elemento[x].fechaEmision = CampoFecha(dr[x].ItemArray[4].ToString()); }
                                if (dr[x].ItemArray[5].ToString() != "") { obj.elemento[x].fechaCompra = CampoFecha(dr[x].ItemArray[5].ToString()); }
                                if (dr[x].ItemArray[6].ToString() != "") { obj.elemento[x].tipoInstrumento = CampoTexto(dr[x].ItemArray[6].ToString()); }
                                if (dr[x].ItemArray[7].ToString() != "") { obj.elemento[x].paisEmisionDepositario = CampoTexto(dr[x].ItemArray[7].ToString()); }
                                if (dr[x].ItemArray[8].ToString() != "") { obj.elemento[x].valorNominal = CampoValor(dr[x].ItemArray[8].ToString()); }
                                if (dr[x].ItemArray[9].ToString() != "") { obj.elemento[x].valorCompra = CampoValor(dr[x].ItemArray[9].ToString()); }
                                if (dr[x].ItemArray[10].ToString() != "") { obj.elemento[x].periodicidadPagoCupon = CampoTexto(dr[x].ItemArray[10].ToString()); }
                                if (dr[x].ItemArray[11].ToString() != "") { obj.elemento[x].clasificacionEmisorDepositario = CampoTexto(dr[x].ItemArray[11].ToString()); }
                                if (dr[x].ItemArray[12].ToString() != "") { obj.elemento[x].tipoEmisorDepositario = CampoTexto(dr[x].ItemArray[12].ToString()); }
                            }
                            estructuraXml = I01ToString(obj).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = error == "OK" ? "NO EXISTEN REGISTROS PARA LA FECHA CORTE SELECCIONADA" : error;
                        }
                        #endregion armaEstructura
                        break;
                    #endregion I01
                    #region I02
                    case "I02":
                        error = "OK";
                        #region extraeData
                        dt = new BddAuxiliar().DataEstructurasI02(fechaCorte);
                        #endregion extraeData
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            saldos obj = new saldos();
                            obj.estructura = tipoEstructura;
                            obj.rucEntidad = ruc;
                            obj.fechaCorte = fechaCorte.ToString("dd/MM/yyyy");
                            obj.numRegistro = (dt.Rows.Count + 1).ToString();
                            obj.elemento = new elementoSaldos[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            obj.elemento = new elementoSaldos[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                obj.elemento[x] = new elementoSaldos();
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[1].ToString())) { obj.elemento[x].numeroIdentificacionDeposito = CampoTexto(dr[x].ItemArray[1].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[2].ToString())) { obj.elemento[x].tipoIdentificacionEmisorDepositario = CampoTexto(dr[x].ItemArray[2].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[3].ToString())) { obj.elemento[x].identificacionEmisorDepositario = CampoTexto(dr[x].ItemArray[3].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[4].ToString())) { obj.elemento[x].fechaEmision = CampoFecha(dr[x].ItemArray[4].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[5].ToString())) { obj.elemento[x].fechaCompra = CampoFecha(dr[x].ItemArray[5].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[6].ToString())) { obj.elemento[x].fechaVencimiento = CampoFecha(dr[x].ItemArray[6].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[7].ToString())) { obj.elemento[x].calificacionRiesgo = CampoTexto(dr[x].ItemArray[7].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[8].ToString())) { obj.elemento[x].calificadoraRiesgo = CampoTexto(dr[x].ItemArray[8].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[9].ToString())) { obj.elemento[x].fechaUltimaCalificacion = CampoFecha(dr[x].ItemArray[9].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[10].ToString())) { obj.elemento[x].cuentaContable = CampoTexto(dr[x].ItemArray[10].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[11].ToString())) { obj.elemento[x].valorLibros = CampoValor(dr[x].ItemArray[11].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[12].ToString())) { obj.elemento[x].estadoTitulo = CampoTexto(dr[x].ItemArray[12].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[13].ToString())) { obj.elemento[x].tasaInteresNominal = CampoValor(dr[x].ItemArray[13].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[14].ToString())) { obj.elemento[x].montoInteresGenerado = CampoValor(dr[x].ItemArray[14].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[15].ToString())) { obj.elemento[x].calificacionRiesgoNormativa = CampoTexto(dr[x].ItemArray[15].ToString()); }
                                if (!string.IsNullOrEmpty(dr[x].ItemArray[16].ToString())) { obj.elemento[x].provisionConstituida = CampoTexto(dr[x].ItemArray[16].ToString()); }
                            }
                            estructuraXml = I02ToString(obj).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = error == "OK" ? "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA" : error;
                        }
                        #endregion armaEstructura
                        break;
                    #endregion I02
                    #region F01
                    case "F01":
                        error = "OK";
                        #region armaTabla
                        if (error == "OK")
                        {
                            try
                            {
                                dt.Clear();
                                dt.Columns.Add("tipoServicio");
                                dt.Columns.Add("codigoServicio");
                                dt.Columns.Add("tipoCanal");
                                dt.Columns.Add("valorTarifa");
                                dt.Columns.Add("numeroTransacciones");
                                dt.Columns.Add("ingresoTotal");
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion armaTabla
                        #region lecturaArchivo
                        if (error == "OK")
                        {
                            try
                            {
                                srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
                                numeroLinea = 1;
                                while ((linea = srArchivo.ReadLine()) != null)
                                {
                                    try
                                    {
                                        if (linea != "")
                                        {
                                            string[] campos = linea.Split('\t');
                                            DataRow row = dt.NewRow();

                                            try { row["tipoServicio"] = CampoTexto(campos[0].Trim()); }
                                            catch { row["tipoServicio"] = "XXX"; }

                                            try { row["codigoServicio"] = CampoTexto(campos[1].Trim()); }
                                            catch { row["codigoServicio"] = "XXXXXX"; }

                                            try { row["tipoCanal"] = CampoTexto(campos[2].Trim()); }
                                            catch { row["tipoCanal"] = "XXX"; }

                                            try { row["valorTarifa"] = CampoValor(campos[3].Trim()); }
                                            catch { row["valorTarifa"] = "0"; }

                                            try { row["numeroTransacciones"] = CampoTexto(campos[4].Trim()); }
                                            catch { row["numeroTransacciones"] = "0"; }

                                            try { row["ingresoTotal"] = CampoValor(campos[5].Trim()); }
                                            catch { row["ingresoTotal"] = "0"; }

                                            dt.Rows.Add(row);
                                        }
                                    }
                                    catch { }
                                    numeroLinea++;
                                }

                                srArchivo.Close();

                                try { ruc = ConfigurationManager.AppSettings["coop29Ruc"].Trim(); }
                                catch { ruc = "1790567699001"; }

                                try { numeroRegistros = numeroLinea; }
                                catch { numeroRegistros = 0; }

                                try { fechaEstructura = fechaCorte; }
                                catch { fechaEstructura = DateTime.Now; }
                            }
                            catch (Exception ex)
                            {
                                error = Util.ReturnExceptionString(ex);
                            }
                        }
                        #endregion lecturaArchivo
                        #region armaEstructura
                        if (dt.Rows.Count > 0)
                        {
                            financiero fin = new financiero();
                            fin.estructura = tipoEstructura;
                            fin.rucEntidad = ruc;
                            fin.fechaCorte = fechaEstructura.ToString("dd/MM/yyyy");
                            fin.numRegistro = numeroRegistros.ToString();
                            fin.elemento = new elementoFinanciero[dt.Rows.Count];
                            DataRow[] dr = null;
                            dr = dt.Select();
                            fin.elemento = new elementoFinanciero[dr.Length];
                            for (int x = 0; x < dr.Length; x++)
                            {
                                fin.elemento[x] = new elementoFinanciero();
                                if (dr[x].ItemArray[0].ToString() != "") { fin.elemento[x].tipoServicio = dr[x].ItemArray[0].ToString(); }
                                if (dr[x].ItemArray[1].ToString() != "") { fin.elemento[x].codigoServicio = dr[x].ItemArray[1].ToString(); }
                                if (dr[x].ItemArray[2].ToString() != "") { fin.elemento[x].tipoCanal = dr[x].ItemArray[2].ToString(); }
                                if (dr[x].ItemArray[3].ToString() != "") { fin.elemento[x].valorTarifa = dr[x].ItemArray[3].ToString(); }
                                if (dr[x].ItemArray[4].ToString() != "") { fin.elemento[x].numeroTransacciones = dr[x].ItemArray[4].ToString(); }
                                if (dr[x].ItemArray[5].ToString() != "") { fin.elemento[x].ingresoTotal = dr[x].ItemArray[5].ToString(); }
                            }
                            estructuraXml = F01ToString(fin).Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace(" xmlns=\"\"", "");
                        }
                        else
                        {
                            error = "NO EXISTEN REGISTROS PARA LE FECHA CORTE SELECCIONADA";
                        }
                        #endregion armaEstructura
                        break;
                        #endregion F01
                }
                #endregion ProcesaEstructura
                #region GuardaEstructura
                if (error == "OK")
                {
                    try
                    {
                        archivoXmlEstructura.Append(estructuraXml);
                        rutaXml =
                            ruta +
                            tipoEstructura + "_" +
                            ruc + "_" +
                            fechaCorte.ToString("dd") + "-" +
                            fechaCorte.ToString("MM") + "-" +
                            fechaCorte.ToString("yyyy") + ".xml";
                        StreamWriter fileXml = new StreamWriter(rutaXml);
                        fileXml.Write(estructuraXml);
                        fileXml.Close();
                    }
                    catch (Exception ex)
                    {
                        error = Util.ReturnExceptionString(ex);
                    }
                }
                #endregion GuardaEstructura;
                #region GeneraHash
                if (error == "OK")
                {
                    try
                    {
                        FileStream fs = new FileStream(rutaXml, FileMode.Open);
                        codigoHash = fs.GetMd5Hash();
                        rutaHash =
                            ruta +
                            tipoEstructura + "_" +
                            ruc + "_" +
                            fechaCorte.ToString("dd") + "-" +
                            fechaCorte.ToString("MM") + "-" +
                            fechaCorte.ToString("yyyy") + "-hash.txt";
                        StreamWriter fileHash = new StreamWriter(rutaHash);
                        fileHash.Write(codigoHash);
                        fileHash.Close();
                    }
                    catch (Exception ex)
                    {
                        error = Util.ReturnExceptionString(ex);
                    }
                }
                #endregion GeneraHash
                #region Zip
                if (error == "OK")
                {
                    try
                    {
                        nombreZip =
                            tipoEstructura + "_" +
                            ruc + "_" +
                            fechaCorte.ToString("dd") + "-" +
                            fechaCorte.ToString("MM") + "-" +
                            fechaCorte.ToString("yyyy");
                        archivoZip = nombreZip + ".zip";
                        rutaZip = ruta + archivoZip;
                        fileZip = new ZipFile(ruta + nombreZip);
                        fileZip.AddFile(rutaXml, "");
                        fileZip.AddFile(rutaHash, "");
                        fileZip.Save(rutaZip);
                    }
                    catch (Exception ex)
                    {
                        error = Util.ReturnExceptionString(ex);
                    }
                }
                #endregion Zip
                #region armaError
                if (error != "OK")
                {
                    resp.CError = "999";
                    resp.DError = error;
                }
                #endregion armaError
            }
            catch (Exception ex)
            {
                resp.CError = "999";
                resp.DError = "ERROR EN SISTEMA: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return resp;
        }

        public string CampoTexto(string texto)
        {
            return texto.
                Replace("Á", "A").
                Replace("É", "E").
                Replace("Í", "I").
                Replace("Ó", "O").
                Replace("Ú", "U").
                Replace("Ñ", "N").
                Replace("\"", "");
        }

        public string CampoValor(string texto)
        {
            string resp = string.Empty;
            try
            {
                if (texto != "")
                {
                    decimal valor = Convert.ToDecimal(texto.Replace(".", ","));
                    resp = valor.ToString("N2").Replace(".", "").Replace(",", ".");
                }
                else
                {
                    resp = "";
                }
            }
            catch
            {
                resp = "";
            }
            return resp;
        }

        public string CampoFecha(string texto)
        {
            string resp = string.Empty;
            DateTime fecha = DateTime.Now;
            try
            {
                fecha = Convert.ToDateTime(texto.Trim());
                resp = fecha.ToString("dd/MM/yyyy");
            }
            catch
            {
                resp = "";
            }
            return resp;
        }

        #region ClasesEstructuras

        #region C01

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/operaciones", IsNullable = false)]
        public partial class operacion
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;
            private elementoOperacion[] elementoField;

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoOperacion[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            public string xsiSchemaLocation = "http://www.seps.gob.ec/operaciones operaciones/operacionesconcedidas.xsd";
        }

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoOperacion
        {
            private string tipoIdentificacionSujetoField;
            private string identificacionSujetoField;
            private string numeroOperacionField;
            private string valorOperacionField;
            private string tasaInteresNominalField;
            private string tasaEfectivaAnualField;
            private string fechaConcesionField;
            private string fechaVencimientoField;
            private string lineaCreditoField;
            private string periodicidadPagoField;
            private string frecuenciaRevisionField;
            private string oficinaConcesionField;
            private string garanteField;
            private string tipoCreditoField;
            private string claseCreditoField;
            private string estadoOperacionField;
            private string numeroOperacionAnteriorField;
            private string origenOperacionField;
            private string tipoOperacionField;
            private string causalVinculacionField;
            private string destinoFinancieroField;
            private string actividadEconomicaField;
            private string geograficoProvinciaField;
            private string geograficoCantonField;
            private string geograficoParroquiaField;
            private string totalIngresosSujetoField;
            private string totalEgresosSujetoField;
            private string patrimonioSujetoField;
            private string actividadSujetoField;
            private string nivelEstudiosField;
            private string tipoViviendaField;
            private string nivelEstudioEsperadoField;
            private string numParticipantesCreditoField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionSujeto { get { return this.tipoIdentificacionSujetoField; } set { this.tipoIdentificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionSujeto { get { return this.identificacionSujetoField; } set { this.identificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroOperacion { get { return this.numeroOperacionField; } set { this.numeroOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorOperacion { get { return this.valorOperacionField; } set { this.valorOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tasaInteresNominal { get { return this.tasaInteresNominalField; } set { this.tasaInteresNominalField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tasaEfectivaAnual { get { return this.tasaEfectivaAnualField; } set { this.tasaEfectivaAnualField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaConcesion { get { return this.fechaConcesionField; } set { this.fechaConcesionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaVencimiento { get { return this.fechaVencimientoField; } set { this.fechaVencimientoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string lineaCredito { get { return this.lineaCreditoField; } set { this.lineaCreditoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string periodicidadPago { get { return this.periodicidadPagoField; } set { this.periodicidadPagoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string frecuenciaRevision { get { return this.frecuenciaRevisionField; } set { this.frecuenciaRevisionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string oficinaConcesion { get { return this.oficinaConcesionField; } set { this.oficinaConcesionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string garante { get { return this.garanteField; } set { this.garanteField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoCredito { get { return this.tipoCreditoField; } set { this.tipoCreditoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string claseCredito { get { return this.claseCreditoField; } set { this.claseCreditoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estadoOperacion { get { return this.estadoOperacionField; } set { this.estadoOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroOperacionAnterior { get { return this.numeroOperacionAnteriorField; } set { this.numeroOperacionAnteriorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string origenOperacion { get { return this.origenOperacionField; } set { this.origenOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoOperacion { get { return this.tipoOperacionField; } set { this.tipoOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string causalVinculacion { get { return this.causalVinculacionField; } set { this.causalVinculacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string destinoFinanciero { get { return this.destinoFinancieroField; } set { this.destinoFinancieroField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string actividadEconomica { get { return this.actividadEconomicaField; } set { this.actividadEconomicaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string geograficoProvincia { get { return this.geograficoProvinciaField; } set { this.geograficoProvinciaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string geograficoCanton { get { return this.geograficoCantonField; } set { this.geograficoCantonField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string geograficoParroquia { get { return this.geograficoParroquiaField; } set { this.geograficoParroquiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string totalIngresosSujeto { get { return this.totalIngresosSujetoField; } set { this.totalIngresosSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string totalEgresosSujeto { get { return this.totalEgresosSujetoField; } set { this.totalEgresosSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string patrimonioSujeto { get { return this.patrimonioSujetoField; } set { this.patrimonioSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string actividadSujeto { get { return this.actividadSujetoField; } set { this.actividadSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nivelEstudios { get { return this.nivelEstudiosField; } set { this.nivelEstudiosField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoVivienda { get { return this.tipoViviendaField; } set { this.tipoViviendaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nivelEstudioEsperado { get { return this.nivelEstudioEsperadoField; } set { this.nivelEstudioEsperadoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numParticipantesCredito { get { return this.numParticipantesCreditoField; } set { this.numParticipantesCreditoField = value; } }
        }

        #endregion C01

        #region C02

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/saldosoperacionales", IsNullable = false)]
        public partial class saldo
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;

            private elementoSaldo[] elementoField;

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoSaldo[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            public string xsiSchemaLocation = "http://www.seps.gob.ec/saldosoperacionales saldosoperacionales/saldosoperacionales.xsd";
        }

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoSaldo
        {
            private string tipoIdentificacionSujetoField;
            private string identificacionSujetoField;
            private string numeroOperacionField;
            private string diasMorosidadField;
            private string calificacionField;
            private string calificacionHomologadaField;
            private string tasaInteresEfectivaField;
            private string tipoAmortizacionField;
            private string valorVencer1_30Field;
            private string valorVencer31_90Field;
            private string valorVencer91_180Field;
            private string valorVencer181_360Field;
            private string valorVencerMas360Field;
            private string valorNoDevInteres1_30Field;
            private string valorNoDevInteres31_90Field;
            private string valorNoDevInteres91_180Field;
            private string valorNoDevInteres181_360Field;
            private string valorNoDevInteresMas360Field;
            private string valorVencido1_30Field;
            private string valorVencido31_90Field;
            private string valorVencido91_180Field;
            private string valorVencido181_360Field;
            private string valorVencidoMas360Field;
            private string valorVencido181_270Field;
            private string valorVencidoMas270Field;
            private string valorVencido91_270Field;
            private string valorVencido271_360Field;
            private string valorVencido361_720Field;
            private string valorVencidoMas720Field;
            private string saldoTotalField;
            private string formaCancelacionField;
            private string gestionRecuperCarteraVencidaField;
            private string gastosJudicRecuperCarteraVencidaField;
            private string interesOrdinarioField;
            private string interesMoraField;
            private string valorDemandaJudicialField;
            private string carteraCastidadaField;
            private string fechaCastigoField;
            private string provisionEspecificaField;
            private string provisionRequeridaReducidaField;
            private string provisionConstituidaField;
            private string tipoOperacionField;
            private string objetoFideicomisoField;
            private string primaField;
            private string cuotaCreditoField;
            private string fechaUltimaCuotaCompPagadaField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionSujeto { get { return this.tipoIdentificacionSujetoField; } set { this.tipoIdentificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionSujeto { get { return this.identificacionSujetoField; } set { this.identificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroOperacion { get { return this.numeroOperacionField; } set { this.numeroOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string diasMorosidad { get { return this.diasMorosidadField; } set { this.diasMorosidadField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string calificacion { get { return this.calificacionField; } set { this.calificacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string calificacionHomologada { get { return this.calificacionHomologadaField; } set { this.calificacionHomologadaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tasaInteresEfectiva { get { return this.tasaInteresEfectivaField; } set { this.tasaInteresEfectivaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoAmortizacion { get { return this.tipoAmortizacionField; } set { this.tipoAmortizacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencer1_30 { get { return this.valorVencer1_30Field; } set { this.valorVencer1_30Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencer31_90 { get { return this.valorVencer31_90Field; } set { this.valorVencer31_90Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencer91_180 { get { return this.valorVencer91_180Field; } set { this.valorVencer91_180Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencer181_360 { get { return this.valorVencer181_360Field; } set { this.valorVencer181_360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencerMas360 { get { return this.valorVencerMas360Field; } set { this.valorVencerMas360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNoDevInteres1_30 { get { return this.valorNoDevInteres1_30Field; } set { this.valorNoDevInteres1_30Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNoDevInteres31_90 { get { return this.valorNoDevInteres31_90Field; } set { this.valorNoDevInteres31_90Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNoDevInteres91_180 { get { return this.valorNoDevInteres91_180Field; } set { this.valorNoDevInteres91_180Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNoDevInteres181_360 { get { return this.valorNoDevInteres181_360Field; } set { this.valorNoDevInteres181_360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNoDevInteresMas360 { get { return this.valorNoDevInteresMas360Field; } set { this.valorNoDevInteresMas360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido1_30 { get { return this.valorVencido1_30Field; } set { this.valorVencido1_30Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido31_90 { get { return this.valorVencido31_90Field; } set { this.valorVencido31_90Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido91_180 { get { return this.valorVencido91_180Field; } set { this.valorVencido91_180Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido181_360 { get { return this.valorVencido181_360Field; } set { this.valorVencido181_360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencidoMas360 { get { return this.valorVencidoMas360Field; } set { this.valorVencidoMas360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido181_270 { get { return this.valorVencido181_270Field; } set { this.valorVencido181_270Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencidoMas270 { get { return this.valorVencidoMas270Field; } set { this.valorVencidoMas270Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido91_270 { get { return this.valorVencido91_270Field; } set { this.valorVencido91_270Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido271_360 { get { return this.valorVencido271_360Field; } set { this.valorVencido271_360Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencido361_720 { get { return this.valorVencido361_720Field; } set { this.valorVencido361_720Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorVencidoMas720 { get { return this.valorVencidoMas720Field; } set { this.valorVencidoMas720Field = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string saldoTotal { get { return this.saldoTotalField; } set { this.saldoTotalField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string formaCancelacion { get { return this.formaCancelacionField; } set { this.formaCancelacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string gestionRecuperCarteraVencida { get { return this.gestionRecuperCarteraVencidaField; } set { this.gestionRecuperCarteraVencidaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string gastosJudicRecuperCarteraVencida { get { return this.gastosJudicRecuperCarteraVencidaField; } set { this.gastosJudicRecuperCarteraVencidaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string interesOrdinario { get { return this.interesOrdinarioField; } set { this.interesOrdinarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string interesMora { get { return this.interesMoraField; } set { this.interesMoraField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorDemandaJudicial { get { return this.valorDemandaJudicialField; } set { this.valorDemandaJudicialField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string carteraCastidada { get { return this.carteraCastidadaField; } set { this.carteraCastidadaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCastigo { get { return this.fechaCastigoField; } set { this.fechaCastigoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string provisionEspecifica { get { return this.provisionEspecificaField; } set { this.provisionEspecificaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string provisionRequeridaReducida { get { return this.provisionRequeridaReducidaField; } set { this.provisionRequeridaReducidaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string provisionConstituida { get { return this.provisionConstituidaField; } set { this.provisionConstituidaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoOperacion { get { return this.tipoOperacionField; } set { this.tipoOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string objetoFideicomiso { get { return this.objetoFideicomisoField; } set { this.objetoFideicomisoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string prima { get { return this.primaField; } set { this.primaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string cuotaCredito { get { return this.cuotaCreditoField; } set { this.cuotaCreditoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaUltimaCuotaCompPagada { get { return this.fechaUltimaCuotaCompPagadaField; } set { this.fechaUltimaCuotaCompPagadaField = value; } }
        }

        #endregion C02

        #region C03

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/garantiasreales", IsNullable = false)]
        public partial class garantia
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;

            private elementoGarantia[] elementoField;

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoGarantia[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            public string xsiSchemaLocation = "http://www.seps.gob.ec/garantiasreales garantiasreales/garantiasreales.xsd";
        }

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoGarantia
        {
            private string tipoIdentificacionSujetoField;
            private string identificacionSujetoField;
            private string numeroOperacionField;
            private string tipoGaranteCodeudorField;
            private string tipoIdentGaranteCodeudorField;
            private string identGaranteCodeudorField;
            private string causaElimGaranteCodeudorField;
            private string fechElimGaranteCodeudorField;
            private string numeroGarantiaField;
            private string tipoGarantiaField;
            private string ubicacionGarantiaProvinciaField;
            private string ubicacionGarantiaCantonField;
            private string ubicacionGarantiaParroquiaField;
            private string valorAvaluoField;
            private string valorAvaluoComercialField;
            private string valorAvaluoCatastralField;
            private string fechaAvaluoField;
            private string numeroRegistroGarantiaField;
            private string fechaContabGarantiaField;
            private string porcentajeCubreGarantiaField;
            private string estadoRegistroField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionSujeto { get { return this.tipoIdentificacionSujetoField; } set { this.tipoIdentificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionSujeto { get { return this.identificacionSujetoField; } set { this.identificacionSujetoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroOperacion { get { return this.numeroOperacionField; } set { this.numeroOperacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoGaranteCodeudor { get { return this.tipoGaranteCodeudorField; } set { this.tipoGaranteCodeudorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentGaranteCodeudor { get { return this.tipoIdentGaranteCodeudorField; } set { this.tipoIdentGaranteCodeudorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identGaranteCodeudor { get { return this.identGaranteCodeudorField; } set { this.identGaranteCodeudorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string causaElimGaranteCodeudor { get { return this.causaElimGaranteCodeudorField; } set { this.causaElimGaranteCodeudorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechElimGaranteCodeudor { get { return this.fechElimGaranteCodeudorField; } set { this.fechElimGaranteCodeudorField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroGarantia { get { return this.numeroGarantiaField; } set { this.numeroGarantiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoGarantia { get { return this.tipoGarantiaField; } set { this.tipoGarantiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ubicacionGarantiaProvincia { get { return this.ubicacionGarantiaProvinciaField; } set { this.ubicacionGarantiaProvinciaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ubicacionGarantiaCanton { get { return this.ubicacionGarantiaCantonField; } set { this.ubicacionGarantiaCantonField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ubicacionGarantiaParroquia { get { return this.ubicacionGarantiaParroquiaField; } set { this.ubicacionGarantiaParroquiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorAvaluo { get { return this.valorAvaluoField; } set { this.valorAvaluoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorAvaluoComercial { get { return this.valorAvaluoComercialField; } set { this.valorAvaluoComercialField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorAvaluoCatastral { get { return this.valorAvaluoCatastralField; } set { this.valorAvaluoCatastralField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaAvaluo { get { return this.fechaAvaluoField; } set { this.fechaAvaluoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroRegistroGarantia { get { return this.numeroRegistroGarantiaField; } set { this.numeroRegistroGarantiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaContabGarantia { get { return this.fechaContabGarantiaField; } set { this.fechaContabGarantiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string porcentajeCubreGarantia { get { return this.porcentajeCubreGarantiaField; } set { this.porcentajeCubreGarantiaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estadoRegistro { get { return this.estadoRegistroField; } set { this.estadoRegistroField = value; } }
        }

        #endregion C03

        #region C04

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/bienes", IsNullable = false)]
        public partial class bien
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;

            private elementoBien[] elementoField;

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoBien[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            public string xsiSchemaLocation = "http://www.seps.gob.ec/bienes bienes/bienes.xsd";
        }

        /// <comentarios/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoBien
        {
            private string tipoIdentificacionSujetoField;
            private string identificacionSujetoField;
            private string numeroOperacionField;
            private string codigoBienField;
            private string tipoBienField;
            private string nombreEmisorField;
            private string fechaEmisionField;
            private string fechaVencimientoField;
            private string valorNominalField;
            private string fechaContabilizacionField;
            private string valorEnLibrosField;
            private string valorUltimoAvaluoField;
            private string valorProvisionConstField;
            private string fechaRealizacionField;
            private string valorRealizacionField;
            private string estadoRegistroField;

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionSujeto
            {
                get
                {
                    return this.tipoIdentificacionSujetoField;
                }
                set
                {
                    this.tipoIdentificacionSujetoField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionSujeto
            {
                get
                {
                    return this.identificacionSujetoField;
                }
                set
                {
                    this.identificacionSujetoField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroOperacion
            {
                get
                {
                    return this.numeroOperacionField;
                }
                set
                {
                    this.numeroOperacionField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string codigoBien
            {
                get
                {
                    return this.codigoBienField;
                }
                set
                {
                    this.codigoBienField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoBien
            {
                get
                {
                    return this.tipoBienField;
                }
                set
                {
                    this.tipoBienField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nombreEmisor
            {
                get
                {
                    return this.nombreEmisorField;
                }
                set
                {
                    this.nombreEmisorField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaEmision
            {
                get
                {
                    return this.fechaEmisionField;
                }
                set
                {
                    this.fechaEmisionField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaVencimiento
            {
                get
                {
                    return this.fechaVencimientoField;
                }
                set
                {
                    this.fechaVencimientoField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNominal
            {
                get
                {
                    return this.valorNominalField;
                }
                set
                {
                    this.valorNominalField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaContabilizacion
            {
                get
                {
                    return this.fechaContabilizacionField;
                }
                set
                {
                    this.fechaContabilizacionField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorEnLibros
            {
                get
                {
                    return this.valorEnLibrosField;
                }
                set
                {
                    this.valorEnLibrosField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorUltimoAvaluo
            {
                get
                {
                    return this.valorUltimoAvaluoField;
                }
                set
                {
                    this.valorUltimoAvaluoField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorProvisionConst
            {
                get
                {
                    return this.valorProvisionConstField;
                }
                set
                {
                    this.valorProvisionConstField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaRealizacion
            {
                get
                {
                    return this.fechaRealizacionField;
                }
                set
                {
                    this.fechaRealizacionField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorRealizacion
            {
                get
                {
                    return this.valorRealizacionField;
                }
                set
                {
                    this.valorRealizacionField = value;
                }
            }

            /// <comentarios/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estadoRegistro
            {
                get
                {
                    return this.estadoRegistroField;
                }
                set
                {
                    this.estadoRegistroField = value;
                }
            }
        }

        #endregion C04

        #region I01
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/portafolios", IsNullable = false)]
        public partial class portafolio
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;
            private elementoPortafolio[] elementoField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            //[System.Xml.Serialization.XmlElementAttribute("estructura", Order = 1)]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            //[System.Xml.Serialization.XmlElementAttribute("rucEntidad", Order = 2)]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            //[System.Xml.Serialization.XmlElementAttribute("fechaCorte", Order = 3)]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            //[System.Xml.Serialization.XmlElementAttribute("numRegistro", Order = 4)]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            //[System.Xml.Serialization.XmlElementAttribute("elemento", Order = 5)]
            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoPortafolio[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            //[System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            //public string xsiSchemaLocation = "http://www.seps.gob.ec/operaciones operaciones/operacionesconcedidas.xsd";
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoPortafolio
        {
            private string numeroIdentificacionDepositoField;
            private string tipoIdentificacionEmisorDepositarioField;
            private string identificacionEmisorDepositarioField;
            private string fechaEmisionField;
            private string fechaCompraField;
            private string tipoInstrumentoField;
            private string paisEmisionDepositarioField;
            private string valorNominalField;
            private string valorCompraField;
            private string periodicidadPagoCuponField;
            private string clasificacionEmisorDepositarioField;
            private string tipoEmisorDepositarioField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroIdentificacionDeposito { get { return this.numeroIdentificacionDepositoField; } set { this.numeroIdentificacionDepositoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionEmisorDepositario { get { return this.tipoIdentificacionEmisorDepositarioField; } set { this.tipoIdentificacionEmisorDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionEmisorDepositario { get { return this.identificacionEmisorDepositarioField; } set { this.identificacionEmisorDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaEmision { get { return this.fechaEmisionField; } set { this.fechaEmisionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCompra { get { return this.fechaCompraField; } set { this.fechaCompraField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoInstrumento { get { return this.tipoInstrumentoField; } set { this.tipoInstrumentoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string paisEmisionDepositario { get { return this.paisEmisionDepositarioField; } set { this.paisEmisionDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorNominal { get { return this.valorNominalField; } set { this.valorNominalField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorCompra { get { return this.valorCompraField; } set { this.valorCompraField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string periodicidadPagoCupon { get { return this.periodicidadPagoCuponField; } set { this.periodicidadPagoCuponField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string clasificacionEmisorDepositario { get { return this.clasificacionEmisorDepositarioField; } set { this.clasificacionEmisorDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoEmisorDepositario { get { return this.tipoEmisorDepositarioField; } set { this.tipoEmisorDepositarioField = value; } }
        }
        #endregion I01

        #region I02
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/saldos", IsNullable = false)]
        public partial class saldos
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;
            private elementoSaldos[] elementoField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoSaldos[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            //[System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            //public string xsiSchemaLocation = "http://www.seps.gob.ec/operaciones operaciones/operacionesconcedidas.xsd";
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoSaldos
        {
            private string numeroIdentificacionDepositoField;
            private string tipoIdentificacionEmisorDepositarioField;
            private string identificacionEmisorDepositarioField;
            private string fechaEmisionField;
            private string fechaCompraField;
            private string fechaVencimientoField;
            private string calificacionRiesgoField;
            private string calificadoraRiesgoField;
            private string fechaUltimaCalificacionField;
            private string cuentaContableField;
            private string valorLibrosField;
            private string estadoTituloField;
            private string tasaInteresNominalField;
            private string montoInteresGeneradoField;
            private string calificacionRiesgoNormativaField;
            private string provisionConstituidaField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroIdentificacionDeposito { get { return this.numeroIdentificacionDepositoField; } set { this.numeroIdentificacionDepositoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoIdentificacionEmisorDepositario { get { return this.tipoIdentificacionEmisorDepositarioField; } set { this.tipoIdentificacionEmisorDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string identificacionEmisorDepositario { get { return this.identificacionEmisorDepositarioField; } set { this.identificacionEmisorDepositarioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaEmision { get { return this.fechaEmisionField; } set { this.fechaEmisionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCompra { get { return this.fechaCompraField; } set { this.fechaCompraField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaVencimiento { get { return this.fechaVencimientoField; } set { this.fechaVencimientoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string calificacionRiesgo { get { return this.calificacionRiesgoField; } set { this.calificacionRiesgoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string calificadoraRiesgo { get { return this.calificadoraRiesgoField; } set { this.calificadoraRiesgoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaUltimaCalificacion { get { return this.fechaUltimaCalificacionField; } set { this.fechaUltimaCalificacionField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string cuentaContable { get { return this.cuentaContableField; } set { this.cuentaContableField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorLibros { get { return this.valorLibrosField; } set { this.valorLibrosField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estadoTitulo { get { return this.estadoTituloField; } set { this.estadoTituloField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tasaInteresNominal { get { return this.tasaInteresNominalField; } set { this.tasaInteresNominalField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string montoInteresGenerado { get { return this.montoInteresGeneradoField; } set { this.montoInteresGeneradoField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string calificacionRiesgoNormativa { get { return this.calificacionRiesgoNormativaField; } set { this.calificacionRiesgoNormativaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string provisionConstituida { get { return this.provisionConstituidaField; } set { this.provisionConstituidaField = value; } }
        }
        #endregion I02

        #region F01

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = false)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.seps.gob.ec/serviciosfinancieros", IsNullable = false)]
        public partial class financiero
        {
            private string estructuraField;
            private string rucEntidadField;
            private string fechaCorteField;
            private string numRegistroField;
            private elementoFinanciero[] elementoField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string estructura
            {
                get
                {
                    return this.estructuraField;
                }
                set
                {
                    this.estructuraField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rucEntidad
            {
                get
                {
                    return this.rucEntidadField;
                }
                set
                {
                    this.rucEntidadField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string fechaCorte
            {
                get
                {
                    return this.fechaCorteField;
                }
                set
                {
                    this.fechaCorteField = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numRegistro
            {
                get
                {
                    return this.numRegistroField;
                }
                set
                {
                    this.numRegistroField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute("elemento")]
            public elementoFinanciero[] elemento
            {
                get
                {
                    return this.elementoField;
                }
                set
                {
                    this.elementoField = value;
                }
            }

            //[System.Xml.Serialization.XmlAttributeAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
            //public string xsiSchemaLocation = "http://www.seps.gob.ec/operaciones operaciones/operacionesconcedidas.xsd";
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class elementoFinanciero
        {
            private string tipoServicioField;
            private string codigoServicioField;
            private string tipoCanalField;
            private string valorTarifaField;
            private string numeroTransaccionesField;
            private string ingresoTotalField;

            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoServicio { get { return this.tipoServicioField; } set { this.tipoServicioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string codigoServicio { get { return this.codigoServicioField; } set { this.codigoServicioField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string tipoCanal { get { return this.tipoCanalField; } set { this.tipoCanalField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string valorTarifa { get { return this.valorTarifaField; } set { this.valorTarifaField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string numeroTransacciones { get { return this.numeroTransaccionesField; } set { this.numeroTransaccionesField = value; } }
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ingresoTotal { get { return this.ingresoTotalField; } set { this.ingresoTotalField = value; } }
        }

        #endregion F01

        #region UAF

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class SEGURIDAD
        {
            private string CLAVEField;
            private object[] itemsField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CLAVE
            {
                get
                {
                    return this.CLAVEField;
                }
                set
                {
                    this.CLAVEField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute("CABECERA", typeof(CABECERA), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class CLAVE
        {
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class CABECERA
        {
            private string CDRField;
            private string PDRField;
            private string FREField;
            private string USRField;
            private string CLIField;
            private string PROField;
            private string TRAField;
            private string TRBField;
            private string TDEField;
            private string TCRField;
            private string TEFField;
            private string TCHField;
            private string TVTField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CDR
            {
                get
                {
                    return this.CDRField;
                }
                set
                {
                    this.CDRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string PDR
            {
                get
                {
                    return this.PDRField;
                }
                set
                {
                    this.PDRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string FRE
            {
                get
                {
                    return this.FREField;
                }
                set
                {
                    this.FREField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string USR
            {
                get
                {
                    return this.USRField;
                }
                set
                {
                    this.USRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CLI
            {
                get
                {
                    return this.CLIField;
                }
                set
                {
                    this.CLIField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string PRO
            {
                get
                {
                    return this.PROField;
                }
                set
                {
                    this.PROField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TRA
            {
                get
                {
                    return this.TRAField;
                }
                set
                {
                    this.TRAField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TRB
            {
                get
                {
                    return this.TRBField;
                }
                set
                {
                    this.TRBField = value;
                }
            }
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TDE
            {
                get
                {
                    return this.TDEField;
                }
                set
                {
                    this.TDEField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TCR
            {
                get
                {
                    return this.TCRField;
                }
                set
                {
                    this.TCRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TEF
            {
                get
                {
                    return this.TEFField;
                }
                set
                {
                    this.TEFField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TCH
            {
                get
                {
                    return this.TCHField;
                }
                set
                {
                    this.TCHField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TVT
            {
                get
                {
                    return this.TVTField;
                }
                set
                {
                    this.TVTField = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class ESTRUCTURA
        {
            private CLIENTE[] CLIField;

            [System.Xml.Serialization.XmlElementAttribute("CLIENTE", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public CLIENTE[] CLI
            {
                get
                {
                    return this.CLIField;
                }
                set
                {
                    this.CLIField = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class CLIENTE
        {
            private string TIDField;
            private string IDEField;
            private string NRSField;
            private string NACField;
            private string DIRField;
            private string CCCField;
            private string AECField;
            private string IMTField;
            private string CDRField;
            private string FCTField;
            private PRODUCTO[] PROField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TID
            {
                get
                {
                    return this.TIDField;
                }
                set
                {
                    this.TIDField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string IDE
            {
                get
                {
                    return this.IDEField;
                }
                set
                {
                    this.IDEField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string NRS
            {
                get
                {
                    return this.NRSField;
                }
                set
                {
                    this.NRSField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string NAC
            {
                get
                {
                    return this.NACField;
                }
                set
                {
                    this.NACField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string DIR
            {
                get
                {
                    return this.DIRField;
                }
                set
                {
                    this.DIRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CCC
            {
                get
                {
                    return this.CCCField;
                }
                set
                {
                    this.CCCField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string AEC
            {
                get
                {
                    return this.AECField;
                }
                set
                {
                    this.AECField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string IMT
            {
                get
                {
                    return this.IMTField;
                }
                set
                {
                    this.IMTField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CDR
            {
                get
                {
                    return this.CDRField;
                }
                set
                {
                    this.CDRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string FCT
            {
                get
                {
                    return this.FCTField;
                }
                set
                {
                    this.FCTField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute("PRODUCTO", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public PRODUCTO[] PRO
            {
                get
                {
                    return this.PROField;
                }
                set
                {
                    this.PROField = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class PRODUCTO
        {
            private string TCOField;
            private string NCOField;
            private string CAPField;
            private string FACField;
            private TRANSACCION[] TRXField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TCO
            {
                get
                {
                    return this.TCOField;
                }
                set
                {
                    this.TCOField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string NCO
            {
                get
                {
                    return this.NCOField;
                }
                set
                {
                    this.NCOField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CAP
            {
                get
                {
                    return this.CAPField;
                }
                set
                {
                    this.CAPField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string FAC
            {
                get
                {
                    return this.FACField;
                }
                set
                {
                    this.FACField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute("TRANSACCION", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public TRANSACCION[] TRX
            {
                get
                {
                    return this.TRXField;
                }
                set
                {
                    this.TRXField = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class TRANSACCION
        {
            private string FTRField;
            private string NTRField;
            private string VDEField;
            private string VCRField;
            private string VEFField;
            private string VCHField;
            private string VVTField;
            private string MNDField;
            private string TTRField;
            private string NOBField;
            //private string PDOField;
            //private string IDOField;
            //private string COBField;
            //private string CATField;
            //private string CCTField;
            private string CSWField;
            private string ISDField;
            private BANCO[] BNCField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string FTR
            {
                get
                {
                    return this.FTRField;
                }
                set
                {
                    this.FTRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string NTR
            {
                get
                {
                    return this.NTRField;
                }
                set
                {
                    this.NTRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string VDE
            {
                get
                {
                    return this.VDEField;
                }
                set
                {
                    this.VDEField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string VCR
            {
                get
                {
                    return this.VCRField;
                }
                set
                {
                    this.VCRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string VEF
            {
                get
                {
                    return this.VEFField;
                }
                set
                {
                    this.VEFField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string VCH
            {
                get
                {
                    return this.VCHField;
                }
                set
                {
                    this.VCHField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string VVT
            {
                get
                {
                    return this.VVTField;
                }
                set
                {
                    this.VVTField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string MND
            {
                get
                {
                    return this.MNDField;
                }
                set
                {
                    this.MNDField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string TTR
            {
                get
                {
                    return this.TTRField;
                }
                set
                {
                    this.TTRField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string NOB
            {
                get
                {
                    return this.NOBField;
                }
                set
                {
                    this.NOBField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CSW
            {
                get
                {
                    return this.CSWField;
                }
                set
                {
                    this.CSWField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string ISD
            {
                get
                {
                    return this.ISDField;
                }
                set
                {
                    this.ISDField = value;
                }
            }


            [System.Xml.Serialization.XmlElementAttribute("BANCO", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public BANCO[] BNC
            {
                get
                {
                    return this.BNCField;
                }
                set
                {
                    this.BNCField = value;
                }
            }
        }
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class BANCO
        {
            private string IDOField;
            private string COBField;
            private string PDOField;
            private string CATField;
            private string CCTField;

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string IDO
            {
                get
                {
                    return this.IDOField;
                }
                set
                {
                    this.IDOField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string COB
            {
                get
                {
                    return this.COBField;
                }
                set
                {
                    this.COBField = value;
                }
            }
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string PDO
            {
                get
                {
                    return this.PDOField;
                }
                set
                {
                    this.PDOField = value;
                }
            }
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CAT
            {
                get
                {
                    return this.CATField;
                }
                set
                {
                    this.CATField = value;
                }
            }

            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string CCT
            {
                get
                {
                    return this.CCTField;
                }
                set
                {
                    this.CCTField = value;
                }
            }
        }

        #endregion UAF

        #endregion ClasesEstructuras

        #region Serializacion

        public static string C01ToString(operacion obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(operacion));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string C02ToString(saldo obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(saldo));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string C03ToString(garantia obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(garantia));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string C04ToString(bien obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(bien));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string I01ToString(portafolio obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(portafolio));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string I02ToString(saldos obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(saldos));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string F01ToString(financiero obj)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(financiero));
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, obj);
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string UAFCabeceraToString(SEGURIDAD obj)
        {
            string data = null;
            try
            {
                XmlSerializer op1 = new XmlSerializer(typeof(SEGURIDAD));
                StringWriterUtf8 ss1 = new StringWriterUtf8();
                XmlSerializerNamespaces ns_app = new XmlSerializerNamespaces();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                op1.Serialize(ss1, obj, ns);
                var xml1 = ss1.ToString().Replace("xmlns=\"http://tempuri.org/\"", "");
                data = xml1.ToString();
            }
            catch
            {
                return string.Empty;
            }

            return data;
        }

        public static string UAFDetalleToString(ESTRUCTURA obj)
        {
            string data = null;
            try
            {
                XmlSerializer op1 = new XmlSerializer(typeof(ESTRUCTURA));
                StringWriterUtf8 ss1 = new StringWriterUtf8();
                XmlSerializerNamespaces ns_app = new XmlSerializerNamespaces();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                op1.Serialize(ss1, obj, ns);
                var xml1 = ss1.ToString().Replace("xmlns=\"http://tempuri.org/\"", "");
                data = xml1.ToString();
            }
            catch (Exception) { /*LOG*/ }

            return data;
        }

        public class StringWriterUtf8 : StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }

        #endregion Serializacion
    }

    public static class FileStreamExtensions
    {
        public static String GetMd5Hash(this FileStream fs)
        {
            return getHash(fs, new MD5CryptoServiceProvider());
        }

        public static String GetSha1Hash(this FileStream fs)
        {
            return getHash(fs, new SHA1Managed());
        }

        public static String GetSha256Hash(this FileStream fs)
        {
            return getHash(fs, new SHA256Managed());
        }

        private static String getHash(FileStream fs, HashAlgorithm hash)
        {
            Int64 currentPos = fs.Position;
            try
            {
                fs.Seek(0, SeekOrigin.Begin);
                StringBuilder sb = new StringBuilder();
                foreach (Byte b in hash.ComputeHash(fs))
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
            finally
            {
                fs.Seek(currentPos, SeekOrigin.Begin);
            }
        }
    }
}

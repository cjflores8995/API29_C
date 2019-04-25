using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WebPos
    {
        public List<VPOSCONVENIO> CargaConvenios()
        {
            return new VPOSCONVENIO().ListarTodos();
        }

        public List<TPOSESTADO> CargaEstado()
        {
            return new TPOSESTADO().Listar();
        }

        public List<VPOSCOMPENSACABECERA> BuscaProcesosResumen(DateTime? fechaDesde, DateTime? fechaHasta, Int32? cconvenio, string estado)
        {
            return new VPOSCOMPENSACABECERA().Listar(fechaDesde, fechaHasta, cconvenio, estado);
        }

        public VPOSCOMPENSACABECERA TraeCabeceraResumen(DateTime? fproceso, Int32? cconvenio)
        {
            return new VPOSCOMPENSACABECERA().Listar(fproceso, cconvenio);
        }

        public CanalRespuesta AutorizarCompensacion(DateTime fechaProceso, Int32 convenio, string usuario)
        {
            CanalRespuesta response = new CanalRespuesta();
            TPOSCOMPENSACABECERA objCab = new TPOSCOMPENSACABECERA();

            try
            {
                objCab.FPROCESO = fechaProceso;
                objCab.CCONVENIO = convenio;
                objCab.FAUTORIZACION = DateTime.Now;
                objCab.CUSUARIOAUTORIZACION = usuario;
                if (objCab.Autorizar(objCab))
                {
                    response.CError = "000";
                    response.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
                }
                else
                {
                    response.CError = "999";
                    response.DError = "ERROR AUTORIZANDO COMPENSACION";
                }
            }
            catch (Exception ex)
            {
                response.CError = "999";
                response.DError = Util.ReturnExceptionString(ex);
            }
            return response;
        }

        public TPOSCOMPENSACABECERA TraeCabecera(DateTime fecha, Int32 convenio)
        {
            return new TPOSCOMPENSACABECERA().Listar(fecha, convenio);
        }

        public List<TPOSCOMPENSADETALLE> TraeDetalleProceso(DateTime fecha, Int32 convenio)
        {
            return new TPOSCOMPENSADETALLE().ListarDetalleProceso(fecha, convenio);
        }

        public CanalRespuesta GeneraReporteResumen(DateTime? fechaDesde, DateTime? fechaHasta, Int32? convenio, string estado, out string ruta, out string archivo)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            List<VPOSCOMPENSACABECERA> ltObj = null;
            List<VPOSCONVENIO> ltConvenio = null;
            List<TPOSESTADO> ltEstado = null;
            //string error = string.Empty;
            ruta = string.Empty;
            archivo = string.Empty;
            Int32 linea = 0;
            try
            {
                ltConvenio = new VPOSCONVENIO().ListarTodos();
                ltEstado = new TPOSESTADO().Listar();
                ltObj = new VPOSCOMPENSACABECERA().Listar(fechaDesde, fechaHasta, convenio, estado);
                if (ltObj != null && ltObj.Count > 0)
                {
                    SLDocument sl = new SLDocument();

                    SLStyle styleFechaLarga = sl.CreateStyle();
                    styleFechaLarga = sl.CreateStyle();
                    styleFechaLarga.FormatCode = "dd/mm/yyyy hh:mm:ss";

                    SLStyle styleFechaCorta = sl.CreateStyle();
                    styleFechaCorta = sl.CreateStyle();
                    styleFechaCorta.FormatCode = "dd/mm/yyyy";

                    SLStyle styleNumeros = sl.CreateStyle();
                    styleNumeros = sl.CreateStyle();
                    styleNumeros.FormatCode = "###0.00";

                    sl.SetCellValue(1, 1, "RESUMEN COMPENSACION");
                    sl.MergeWorksheetCells("A1", "L1");

                    sl.SetCellValue(3, 1, "FECHA PROCESO");
                    sl.SetCellValue(3, 2, "CCONVENIO");
                    sl.SetCellValue(3, 3, "NOMBRE");
                    sl.SetCellValue(3, 4, "RECHAZOS");
                    sl.SetCellValue(3, 5, "COMPENSADOS");
                    sl.SetCellValue(3, 6, "TOTAL TRANSACCION");
                    sl.SetCellValue(3, 7, "TOTAL COMISION");
                    sl.SetCellValue(3, 8, "TOTAL RETENCION FTE");
                    sl.SetCellValue(3, 9, "TOTAL RETENCION IVA");
                    sl.SetCellValue(3, 10, "TOTAL LIQUIDADO");
                    sl.SetCellValue(3, 11, "FECHA AUTORIZACION");
                    sl.SetCellValue(3, 12, "USUARIO AUTORIZACION");
                    sl.SetCellValue(3, 13, "CESTADO");
                    sl.SetCellValue(3, 14, "ESTADO");

                    linea = 4;

                    foreach (VPOSCOMPENSACABECERA obj in ltObj)
                    {
                        try
                        {
                            sl.SetCellValue(linea, 1, obj.FPROCESO.Value);
                            sl.SetCellStyle(linea, 1, styleFechaCorta);
                        }
                        catch { sl.SetCellValue(linea, 1, ""); }

                        try { sl.SetCellValue(linea, 2, obj.CCONVENIO.Value); }
                        catch { sl.SetCellValue(linea, 2, 0); }

                        try { sl.SetCellValue(linea, 3, ltConvenio.Where(x => x.CCONVENIO == obj.CCONVENIO).First().NOMBRE); }
                        catch { sl.SetCellValue(linea, 3, ""); }

                        try { sl.SetCellValue(linea, 4, obj.RECHAZADOS.Value); }
                        catch { sl.SetCellValue(linea, 4, 0); }

                        try { sl.SetCellValue(linea, 5, obj.COMPENSADOS.Value); }
                        catch { sl.SetCellValue(linea, 5, 0); }

                        try { sl.SetCellValue(linea, 6, obj.TOTALTRANSACCION.Value); }
                        catch { sl.SetCellValue(linea, 6, 0); }
                        sl.SetCellStyle(linea, 6, styleNumeros);

                        try { sl.SetCellValue(linea, 7, obj.TOTALCOMISION.Value); }
                        catch { sl.SetCellValue(linea, 7, 0); }
                        sl.SetCellStyle(linea, 7, styleNumeros);

                        try { sl.SetCellValue(linea, 8, obj.TOTALRETENCIONFTE.Value); }
                        catch { sl.SetCellValue(linea, 8, 0); }
                        sl.SetCellStyle(linea, 8, styleNumeros);

                        try { sl.SetCellValue(linea, 9, obj.TOTALRETENCIONIVA.Value); }
                        catch { sl.SetCellValue(linea, 9, 0); }
                        sl.SetCellStyle(linea, 9, styleNumeros);

                        try { sl.SetCellValue(linea, 10, obj.TOTALLIQUIDADO.Value); }
                        catch { sl.SetCellValue(linea, 10, 0); }
                        sl.SetCellStyle(linea, 10, styleNumeros);

                        try
                        {
                            sl.SetCellValue(linea, 11, obj.FAUTORIZACION.Value);
                            sl.SetCellStyle(linea, 11, styleFechaLarga);
                        }
                        catch { sl.SetCellValue(linea, 11, ""); }

                        sl.SetCellValue(linea, 12, obj.CUSUARIOAUTORIZACION);
                        sl.SetCellValue(linea, 13, obj.CESTADO);

                        try { sl.SetCellValue(linea, 14, ltEstado.Where(x => x.CESTADO == obj.CESTADO).First().DESCRIPCION); }
                        catch { sl.SetCellValue(linea, 14, ""); }

                        linea++;
                    }

                    try { sl.SetCellValue(linea, 4, ltObj.Sum(x => x.RECHAZADOS).Value); }
                    catch { sl.SetCellValue(linea, 4, 0); }
                    sl.SetCellStyle(linea, 4, styleNumeros);

                    try { sl.SetCellValue(linea, 5, ltObj.Sum(x => x.COMPENSADOS).Value); }
                    catch { sl.SetCellValue(linea, 5, 0); }
                    sl.SetCellStyle(linea, 5, styleNumeros);

                    try { sl.SetCellValue(linea, 6, ltObj.Sum(x => x.TOTALTRANSACCION).Value); }
                    catch { sl.SetCellValue(linea, 6, 0); }
                    sl.SetCellStyle(linea, 6, styleNumeros);

                    try { sl.SetCellValue(linea, 7, ltObj.Sum(x => x.TOTALCOMISION).Value); }
                    catch { sl.SetCellValue(linea, 7, 0); }
                    sl.SetCellStyle(linea, 7, styleNumeros);

                    try { sl.SetCellValue(linea, 8, ltObj.Sum(x => x.TOTALRETENCIONFTE).Value); }
                    catch { sl.SetCellValue(linea, 8, 0); }
                    sl.SetCellStyle(linea, 8, styleNumeros);

                    try { sl.SetCellValue(linea, 9, ltObj.Sum(x => x.TOTALRETENCIONIVA).Value); }
                    catch { sl.SetCellValue(linea, 9, 0); }
                    sl.SetCellStyle(linea, 9, styleNumeros);

                    try { sl.SetCellValue(linea, 10, ltObj.Sum(x => x.TOTALLIQUIDADO).Value); }
                    catch { sl.SetCellValue(linea, 10, 0); }
                    sl.SetCellStyle(linea, 10, styleNumeros);

                    sl.Filter("A3", "L3");
                    sl.AutoFitColumn("A3", "L3");

                    archivo = "RESUMEN_COMPENSACION_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    ruta = ConfigurationManager.AppSettings["pathArchivos"].ToString() + ConfigurationManager.AppSettings["pathArchivosBatch"].ToString() + archivo;
                    sl.SaveAs(ruta + archivo);
                    respuesta.CError = "000";
                    respuesta.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
                }
                else
                {
                    respuesta.CError = "998";
                    respuesta.DError = "NO EXISTEN REGISTROS PARA LOS CRITERIOS SELECCIONADOS";
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                respuesta.CError = "999";
                respuesta.DError = "ERROR GENERANDO REPORTE: " + ex.Message.ToString().ToUpper();
            }
            return respuesta;
        }

        public CanalRespuesta GeneraReporteDetalle(DateTime? fecha, Int32? convenio, out string ruta, out string archivo)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            VPOSCOMPENSACABECERA objCabecera = new VPOSCOMPENSACABECERA();
            List<TPOSCOMPENSADETALLE> ltDetalle = new List<TPOSCOMPENSADETALLE>();
            List<VPOSCONVENIO> ltConvenio = new List<VPOSCONVENIO>();
            List<TPOSESTADO> ltEstado = new List<TPOSESTADO>();
            ruta = string.Empty;
            archivo = string.Empty;
            Int32 linea = 0;
            try
            {
                ltConvenio = new VPOSCONVENIO().ListarTodos();
                ltEstado = new TPOSESTADO().Listar();
                objCabecera = new VPOSCOMPENSACABECERA().Listar(fecha, convenio);
                if (objCabecera != null)
                {
                    ltDetalle = new TPOSCOMPENSADETALLE().ListarDetalleProceso(fecha, convenio);
                    if (ltDetalle != null && ltDetalle.Count > 0)
                    {
                        SLDocument sl = new SLDocument();

                        SLStyle styleFechaLarga = sl.CreateStyle();
                        styleFechaLarga = sl.CreateStyle();
                        styleFechaLarga.FormatCode = "dd/mm/yyyy hh:mm:ss";

                        SLStyle styleFechaCorta = sl.CreateStyle();
                        styleFechaCorta = sl.CreateStyle();
                        styleFechaCorta.FormatCode = "dd/mm/yyyy";

                        SLStyle styleNumeros = sl.CreateStyle();
                        styleNumeros = sl.CreateStyle();
                        styleNumeros.FormatCode = "###0.00";

                        sl.SetCellValue(1, 1, "RESUMEN DETALLE PROCESO");
                        sl.MergeWorksheetCells("A1", "N1");

                        sl.SetCellValue(3, 1, "FECHA PROCESO: " + Convert.ToDateTime(objCabecera.FPROCESO).ToString("yyyy/MM/dd"));
                        sl.MergeWorksheetCells("A3", "C3");

                        sl.SetCellValue(3, 4, "CONVENIO: " + objCabecera.CCONVENIO + " - " + ltConvenio.Where(x => x.CCONVENIO == objCabecera.CCONVENIO).First().NOMBRE);
                        sl.MergeWorksheetCells("D3", "G3");

                        sl.SetCellValue(3, 8, "ESTADO: " + objCabecera.CESTADO + " - " + ltEstado.Where(x => x.CESTADO == objCabecera.CESTADO).First().DESCRIPCION);
                        sl.MergeWorksheetCells("H3", "J3");

                        sl.SetCellValue(4, 1, "DEBITO: " + objCabecera.COMISION);
                        sl.MergeWorksheetCells("A4", "C4");

                        sl.SetCellValue(4, 4, "TRANSFERENCIA: " + objCabecera.TRANSFERENCIA);
                        sl.MergeWorksheetCells("D4", "F4");

                        sl.SetCellValue(4, 7, "COMPENSADOS: " + objCabecera.COMPENSADOS);
                        sl.MergeWorksheetCells("G4", "H4");

                        sl.SetCellValue(4, 9, "RECHAZADOS: " + objCabecera.RECHAZADOS);
                        sl.MergeWorksheetCells("I4", "J4");

                        sl.SetCellValue(5, 1, "TOTAL TRANSACCIONES: " + objCabecera.TOTALTRANSACCION.Value.ToString("F2"));
                        sl.MergeWorksheetCells("A5", "C5");

                        sl.SetCellValue(5, 4, "TOTAL LIQUIDADO: " + objCabecera.TOTALLIQUIDADO.Value.ToString("F2"));
                        sl.MergeWorksheetCells("D5", "F5");

                        sl.SetCellValue(5, 7, "TOTAL COMISION: " + objCabecera.TOTALCOMISION.Value.ToString("F2"));
                        sl.MergeWorksheetCells("G5", "I5");

                        sl.SetCellValue(7, 1, "FECHA TRANSACCION");
                        sl.SetCellValue(7, 2, "TARJETA");
                        sl.SetCellValue(7, 3, "CUENTA");
                        sl.SetCellValue(7, 4, "FECHA COMPENSACION");
                        sl.SetCellValue(7, 5, "LOTE");
                        sl.SetCellValue(7, 6, "NUMERO TRANSACCION");
                        sl.SetCellValue(7, 7, "NUMERO APROBACION");
                        sl.SetCellValue(7, 8, "VALOR TRANSACCION");
                        sl.SetCellValue(7, 9, "VALOR LIQUIDADO");
                        sl.SetCellValue(7, 10, "VALOR COMISION");
                        sl.SetCellValue(7, 11, "ESTADO");
                        sl.SetCellValue(7, 12, "DESCRIPCION ESTADO");
                        sl.SetCellValue(7, 13, "ERROR");
                        sl.SetCellValue(7, 14, "DESCRIPCION ERROR");

                        linea = 8;

                        foreach (TPOSCOMPENSADETALLE obj in ltDetalle)
                        {
                            try
                            {
                                sl.SetCellValue(linea, 1, obj.FTRANSACCION.Value);
                                sl.SetCellStyle(linea, 1, styleFechaLarga);
                            }
                            catch { sl.SetCellValue(linea, 1, ""); }

                            sl.SetCellValue(linea, 2, obj.TARJETA);
                            sl.SetCellValue(linea, 3, obj.CCUENTA);

                            try
                            {
                                sl.SetCellValue(linea, 4, obj.FCOMPENSACION.Value);
                                sl.SetCellStyle(linea, 4, styleFechaLarga);
                            }
                            catch { sl.SetCellValue(linea, 4, ""); }

                            sl.SetCellValue(linea, 5, obj.LOTE);
                            sl.SetCellValue(linea, 6, obj.NUMEROTRANSACCION);
                            sl.SetCellValue(linea, 7, obj.NUMEROAPROBACION);

                            try { sl.SetCellValue(linea, 8, obj.VALORTRANSACCION.Value); }
                            catch { sl.SetCellValue(linea, 8, 0); }
                            sl.SetCellStyle(linea, 8, styleNumeros);

                            try { sl.SetCellValue(linea, 9, obj.VALORLIQUIDADO.Value); }
                            catch { sl.SetCellValue(linea, 9, 0); }
                            sl.SetCellStyle(linea, 9, styleNumeros);

                            try { sl.SetCellValue(linea, 10, obj.VALORCOMISION.Value); }
                            catch { sl.SetCellValue(linea, 10, 0); }
                            sl.SetCellStyle(linea, 10, styleNumeros);

                            sl.SetCellValue(linea, 11, obj.CESTADO);

                            try { sl.SetCellValue(linea, 12, ltEstado.Where(x => x.CESTADO == obj.CESTADO).First().DESCRIPCION); }
                            catch { sl.SetCellValue(linea, 12, ""); }

                            sl.SetCellValue(linea, 13, obj.CERROR);
                            sl.SetCellValue(linea, 14, obj.DERROR);

                            linea++;
                        }

                        try { sl.SetCellValue(linea, 8, ltDetalle.Sum(x => x.VALORTRANSACCION).Value); }
                        catch { sl.SetCellValue(linea, 8, 0); }
                        sl.SetCellStyle(linea, 8, styleNumeros);

                        try { sl.SetCellValue(linea, 9, ltDetalle.Sum(x => x.VALORLIQUIDADO).Value); }
                        catch { sl.SetCellValue(linea, 9, 0); }
                        sl.SetCellStyle(linea, 9, styleNumeros);

                        try { sl.SetCellValue(linea, 10, ltDetalle.Sum(x => x.VALORCOMISION).Value); }
                        catch { sl.SetCellValue(linea, 10, 0); }
                        sl.SetCellStyle(linea, 10, styleNumeros);

                        sl.Filter("A7", "N7");
                        sl.AutoFitColumn("A7", "N7");

                        archivo = "DETALLE_COMPENSACION_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        ruta = ConfigurationManager.AppSettings["pathArchivos"].ToString() + ConfigurationManager.AppSettings["pathArchivosBatch"].ToString() + archivo;
                        sl.SaveAs(ruta + archivo);
                        respuesta.CError = "000";
                        respuesta.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
                    }
                    else
                    {
                        respuesta.CError = "998";
                        respuesta.DError = "NO EXISTEN REGISTROS PARA LOS CRITERIOS SELECCIONADOS";
                    }
                }
                else
                {
                    respuesta.CError = "999";
                    respuesta.DError = "ERROR RECUPERANDO CABECERA";
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                respuesta.CError = "999";
                respuesta.DError = "ERROR GENERANDO REPORTE: " + ex.Message.ToString().ToUpper();
            }
            return respuesta;
        }

        public CanalRespuesta CancelarArchivo(DateTime? fecha, Int32? convenio)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            int registros = 0;
            try
            {
                TPOSCONVENIO objcon = new TPOSCONVENIO().ListarXConvenio(convenio.Value);
                if (objcon != null)
                {
                    if (new TPOSCOMPENSACABECERA().ActualizarFinalizado(fecha, convenio, "NO EXISTE ARCHIVO ORIGEN") && objcon.COMPENSA == "1")
                    {
                        TPOSCOMPENSACABECERA cabNueva = null;
                        cabNueva = new TPOSCOMPENSACABECERA().Listar(fecha.Value.AddDays(1), objcon.CCONVENIO);
                        if (cabNueva == null)
                        {
                            cabNueva = new TPOSCOMPENSACABECERA();
                            cabNueva.FPROCESO = fecha.Value.AddDays(1);
                            cabNueva.CCONVENIO = objcon.CCONVENIO;
                            cabNueva.CESTADO = "PEN";
                            if (new TPOSCOMPENSACABECERA().Insertar(cabNueva))
                            {
                                respuesta.CError = "000";
                                respuesta.DError = "PROCESO CANCELADO CORRECTAMENTE";
                            }
                            else
                            {
                                respuesta.CError = "999";
                                respuesta.DError = "ERROR CANCELANDO PROCESO";
                            }
                        }
                    }
                }
                else
                {
                    respuesta.CError = "999";
                    respuesta.DError = "ERROR OBTENIENDO DATOS CONVENIO";
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                respuesta.CError = "999";
                respuesta.DError = "ERROR EN PROCESO: " + ex.Message.ToString().ToUpper();
            }
            return respuesta;
        }

        public CanalRespuesta GeneraReporteTablita(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            return respuesta;
        }
    }
}

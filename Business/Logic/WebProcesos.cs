using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WebProcesos
    {
        public void CargaSifco(string fecha, string proceso, out string error, out int regok, out int reger)
        {
            new BddAuxiliar().CargaSifco(fecha, proceso, out error, out regok, out reger);
        }

        public void CargaDesbloqueos(string fecha, string usuario, out string proceso, out string error, out int regok, out int reger)
        {
            new BddAuxiliar().CargaDesbloqueos(fecha, usuario, out proceso, out error, out regok, out reger);
            if (error == "OK")
            {
                TBTHPROCESO proc = new TBTHPROCESO().Listar(DateTime.Today, Convert.ToInt32(proceso));
                proc.CESTADO = "PENPRO";
                if (!proc.Actualizar(proc))
                {
                    error = "ACTUALIZANDO PROCESO";
                }
            }
        }

        public void CargaCobrosSifco(string fecha, string usuario, out string proceso, out string error, out int regok, out int reger)
        {
            new BddAuxiliar().CargaCobrosSifco(fecha, usuario, out proceso, out error, out regok, out reger);
            if (error == "OK")
            {
                TBTHPROCESO proc = new TBTHPROCESO().Listar(DateTime.Today, Convert.ToInt32(proceso));
                proc.CESTADO = "PENPRO";
                if (!proc.Actualizar(proc))
                {
                    error = "ACTUALIZANDO PROCESO";
                }
            }
        }

        public void ConvivenciaFaltante(string fcontable, string fproceso, out string error)
        {
            new BddAuxiliar().ConvivenciaFaltante(fcontable, fproceso, out error);
        }

        public void ProcesaConvivenciaSifco(string fecha, string usuario, out string proceso, out string error, out int regok, out int reger)
        {
            error = "OK";
            proceso = string.Empty;
            regok = 0;
            reger = 0;

            try
            {
                if (error == "OK")
                {
                    new BddAuxiliar().ConvivenciaSifcoCargaComprobantes(fecha, out error);
                }

                if (error == "OK")
                {
                    new BddAuxiliar().ConvivenciaSifcoPasaComprobantes(fecha, usuario, out proceso, out error, out regok, out reger);
                }
            }
            catch (Exception ex)
            {
                error = "ERROR GENERANDO REPORTE: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public List<VBTHPROCESORESUMEN> BuscaProcesosResumen(DateTime? fdesde, DateTime? fhasta, Int32? cproceso)
        {
            return new VBTHPROCESORESUMEN().ListarResumen(fdesde, fhasta, cproceso);
        }

        public VBTHPROCESORESUMEN BuscaProcesoResumen(DateTime? fproceso, Int32? cproceso)
        {
            return new VBTHPROCESORESUMEN().ListarResumen(fproceso, cproceso);
        }

        public VBTHPROCESO ConsultaProceso(DateTime? fproceso, Int32? cproceso)
        {
            return new VBTHPROCESO().Listar(fproceso, cproceso);
        }

        public List<VBTHPROCESOERRORES> ResumenErrores(DateTime fProceso, Int32 cproceso)
        {
            return new VBTHPROCESOERRORES().Listar(fProceso, cproceso);
        }

        public List<VBTHTABULADORESUMEN> ResumenTabulado(DateTime fecha, Int32 proceso)
        {
            return new VBTHTABULADORESUMEN().Listar(fecha, proceso);
        }

        public List<VBTHCHEQUESERRORES> ErroresCheques(DateTime fecha, Int32 proceso)
        {
            return new VBTHCHEQUESERRORES().Listar(fecha, proceso);
        }

        public List<TBTHTIPOARCHIVO> CargaTipoArchivo()
        {
            return new TBTHTIPOARCHIVO().Listar();
        }

        public List<TBTHREPARTO> CargaRepartos()
        {
            return new TBTHREPARTO().Listar();
        }

        public TBTHREPARTO CargaRepartos(string creparto)
        {
            return new TBTHREPARTO().ListarXCodigo(creparto);
        }

        public List<TBTHTIPOPROCESO> CargaTipoProcesos()
        {
            return new TBTHTIPOPROCESO().Listar();
        }

        public CanalRespuesta InsertaCabecera(ref TBTHPROCESO obj)
        {
            CanalRespuesta response = new CanalRespuesta();
            try
            {
                obj.CPROCESO = new BddAuxiliar().GetSQBatchProceso();
                if (obj.CPROCESO > 0)
                {
                    if (new TBTHPROCESO().Insertar(obj))
                    {
                        response.CError = "000";
                        response.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
                    }
                    else
                    {
                        response.CError = "999";
                        response.DError = "ERROR INSERTANDO PROCESO";
                    }
                }
                else
                {
                    response.CError = "999";
                    response.DError = "ERROR GENERANDO SECUENCIA DE PROCESO";
                }
            }
            catch (Exception ex)
            {
                response.CError = "999";
                response.DError = Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return response;
        }

        public List<VBTHPROCESO> ConsultaProcesos(DateTime? fdesde, DateTime? fhasta, Int32? cproceso)
        {
            return new VBTHPROCESO().Listar(fdesde, fhasta, cproceso);
        }

        public List<VBTHPROCESO> ConsultarPendientesAutorizar(Int32? cproceso, string estado)
        {
            return new VBTHPROCESO().ListarPendientesAutorizar(cproceso, estado);
        }

        public bool ConsultaTotalesDetalleTabulado(DateTime? fproceso, Int32? cproceso, out Int32 registros, out Decimal total)
        {
            return new TBTHDETALLETABULADO().TotalesProceso(fproceso, cproceso, out registros, out total);
        }

        public bool ConsultaTotalesCheques(DateTime? fproceso, Int32? cproceso, out Int32 registros, out Decimal total)
        {
            return new TBTHDETALLECHEQUES().Totales(fproceso, cproceso, out registros, out total);
        }

        public bool AutorizarProceso(VBTHPROCESO obj)
        {
            return new TBTHPROCESO().Autorizar(obj);
        }

        public Int32 TotalRegistrosDetalleProceso(DateTime? fproceso, Int32? cproceso)
        {
            return new TBTHDETALLEPROCESO().ContarTotal(fproceso, cproceso);
        }

        //public List<VBTHTABULADOCABECERA> ConsultaResumenCabeceraFechasProceso(DateTime? fdesde, DateTime? fhasta, Int32? proceso, out string error)
        //{
        //    return new VBTHTABULADOCABECERA().ListarResumenDesdeHasta(fdesde, fhasta, proceso, out error);
        //}



        //public VBTHTABULADOCABECERA ConsultaResumenCabecera(DateTime fecha, Int32 proceso, out string error)
        //{
        //    return new VBTHTABULADOCABECERA().Listar(fecha, proceso, out error);
        //}
    }
}

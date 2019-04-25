using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.BthPos
{
    class RetentionMethods : Business.BthPos
    {
        public new TPOSCOMPENSADETALLE CalculoRetencionSinIva(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORIVACOMISION = objDetalle.VALORCOMISION * (glbPorcentajeIva / 100);
                objDetalle.VALORIVACOMISION = Math.Round(objDetalle.VALORIVACOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = objDetalle.VALORCOMISION * 0.02m;
                objDetalle.VALORRETENCIONFUENTE = Math.Round(objDetalle.VALORRETENCIONFUENTE.Value, 2);

                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORRETENCIONIVA = Math.Round(objDetalle.VALORRETENCIONIVA.Value, 2);

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION + objDetalle.VALORRETENCIONFUENTE + objDetalle.VALORRETENCIONIVA - objDetalle.VALORCOMISION - objDetalle.VALORIVACOMISION;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoRetencion(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORIVACOMISION = objDetalle.VALORCOMISION * (glbPorcentajeIva / 100);
                objDetalle.VALORIVACOMISION = Math.Round(objDetalle.VALORIVACOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = objDetalle.VALORCOMISION * 0.02m;
                objDetalle.VALORRETENCIONFUENTE = Math.Round(objDetalle.VALORRETENCIONFUENTE.Value, 2);

                objDetalle.VALORRETENCIONIVA = objDetalle.VALORIVACOMISION * 0.20m;
                objDetalle.VALORRETENCIONIVA = Math.Round(objDetalle.VALORRETENCIONIVA.Value, 2);

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION + objDetalle.VALORRETENCIONFUENTE + objDetalle.VALORRETENCIONIVA - objDetalle.VALORCOMISION - objDetalle.VALORIVACOMISION;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoSinRetencion(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {


            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORIVACOMISION = objDetalle.VALORCOMISION * (glbPorcentajeIva / 100);
                objDetalle.VALORIVACOMISION = Math.Round(objDetalle.VALORIVACOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONFUENTE = Math.Round(objDetalle.VALORRETENCIONFUENTE.Value, 2);

                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORRETENCIONIVA = Math.Round(objDetalle.VALORRETENCIONIVA.Value, 2);

                //objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - (objDetalle.VALORCOMISION + objDetalle.VALORRETENCIONFUENTE + objDetalle.VALORRETENCIONIVA);

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION - objDetalle.VALORIVACOMISION;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoFavorita(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = objDetalle.VALORTRANSACCION * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);
                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORLIQUIDADO = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoTia(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTARIFA0 + objDetalle.VALORTARIFAIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);
                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORLIQUIDADO = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoFarcomed(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = objDetalle.VALORTRANSACCION * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = objDetalle.VALORCOMISION * 0.02m;
                objDetalle.VALORRETENCIONFUENTE = Math.Round(objDetalle.VALORRETENCIONFUENTE.Value, 2);

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONIVA = 0;

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoTablita(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * 0.02m;
                objDetalle.VALORRETENCIONFUENTE = Math.Round(objDetalle.VALORRETENCIONFUENTE.Value, 2);

                objDetalle.VALORRETENCIONIVA = objDetalle.VALORIVA * 0.20m;
                objDetalle.VALORRETENCIONIVA = Math.Round(objDetalle.VALORRETENCIONIVA.Value, 2);

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION - objDetalle.VALORRETENCIONFUENTE - objDetalle.VALORRETENCIONIVA;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.VALORIVACOMISION = 0;

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }

        public new TPOSCOMPENSADETALLE CalculoEtatex(TPOSCOMPENSADETALLE objDetalle, VPOSCONVENIO objConvenio)
        {
            try
            {
                objDetalle.VALORCOMISION = (objDetalle.VALORTRANSACCION - objDetalle.VALORIVA) * (objConvenio.COMISION / 100);
                objDetalle.VALORCOMISION = Math.Round(objDetalle.VALORCOMISION.Value, 2);

                objDetalle.VALORIVACOMISION = objDetalle.VALORCOMISION * (glbPorcentajeIva / 100);
                objDetalle.VALORIVACOMISION = Math.Round(objDetalle.VALORIVACOMISION.Value, 2);

                objDetalle.VALORRETENCIONFUENTE = 0;

                objDetalle.VALORRETENCIONIVA = 0;

                objDetalle.VALORLIQUIDADO = objDetalle.VALORTRANSACCION - objDetalle.VALORCOMISION - objDetalle.VALORIVACOMISION;
                objDetalle.VALORLIQUIDADO = Math.Round(objDetalle.VALORLIQUIDADO.Value, 2);

                objDetalle.CERROR = "000";
                objDetalle.DERROR = "OK";
            }
            catch (Exception ex)
            {
                objDetalle.CERROR = "999";
                objDetalle.DERROR = ex.Message.ToUpper();
                objDetalle.VALORCOMISION = 0;
                objDetalle.VALORIVACOMISION = 0;
                objDetalle.VALORRETENCIONFUENTE = 0;
                objDetalle.VALORRETENCIONIVA = 0;
                objDetalle.VALORLIQUIDADO = 0;
            }
            return objDetalle;
        }


    }
}

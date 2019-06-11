using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace Business
{
    public class WebEstructurasConciliacion : IWebEstructurasConciliacion
    {
        private CanalRespuesta respuesta = new CanalRespuesta();
        private LoadWebMessages msj = new LoadWebMessages();
        


        public CanalRespuesta GeneraEstructuraConciliacion()
        {
            try
            {
                Thread.Sleep(5000);
                respuesta.CError = msj.VAR_MSJ_OK;
                respuesta.DError = msj.VAR_MSJ_OK_DET;

            } catch(Exception ex)
            {
                respuesta.CError = msj.VAR_MSJ_ERROR;
                respuesta.DError = string.Format($"{msj.VAR_MSJ_ERROR_DET}: {ex.Message.ToUpper().ToString()}");
            }

            return respuesta;
        }
    }
}

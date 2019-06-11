using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Business
{
    public class WebEstructurasConciliacion : IWebEstructurasConciliacion
    {
        private CanalRespuesta respuesta = new CanalRespuesta();
        private LoadPropertiesConfig prop = new LoadPropertiesConfig();
        


        public CanalRespuesta GeneraEstructuraConciliacion(DateTime FECHA_INICIO, DateTime FECHA_FIN)
        {
            string NOMBRE_ESTRUCTURA = string.Empty;
            string FECHA = string.Empty;
            string PATH_ARCHIVO = string.Empty;
            CanalRespuesta resp = new CanalRespuesta();
            string borrar = string.Empty;
            List<VCONCILIACIONFACILITO> listadoElementos = new List<VCONCILIACIONFACILITO>();
            string VAR_FECHA_INICIO = string.Empty;
            string VAR_FECHA_FIN = string.Empty;
            int contador = 0;
            double VALOR_TOTAL = 0;
            string CABECERA_VAR = string.Empty;


            try
            {
                Logging.EscribirLog("Inicio de la generacion de la estructura", null, "GEN");

                VAR_FECHA_INICIO = FECHA_INICIO.ToString("dd/MM/yyyy");
                VAR_FECHA_FIN = FECHA_FIN.ToString("dd/MM/yyyy");

                FECHA = Util.ConvertToYearMonthDay(FECHA_FIN);
                NOMBRE_ESTRUCTURA = string.Format($"{prop.VAR_INPUT}_{prop.VAR_CODIGO_ENTIDAD}_{FECHA}.{prop.VAR_FORMATO_CONCILIACION}");
                PATH_ARCHIVO = prop.VAR_PATH_CONCILIACION + NOMBRE_ESTRUCTURA;

                resp = CrearArchivo(PATH_ARCHIVO);

                listadoElementos = new VCONCILIACIONFACILITO().ListarElementos(VAR_FECHA_INICIO, VAR_FECHA_FIN);

                if (listadoElementos != null && listadoElementos.Count > 0)
                {
                    if (resp.CError == prop.VAR_MSJ_OK)
                    {
                        using (StreamWriter file = new StreamWriter(PATH_ARCHIVO))
                        {
                            foreach (VCONCILIACIONFACILITO e in listadoElementos)
                            {
                                contador++;
                                VALOR_TOTAL = VALOR_TOTAL + e.VALOR;
                            }

                            CABECERA_VAR = string.Format($"{prop.VAR_CODIGO_ENTIDAD},{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")},{Convert.ToDecimal(VALOR_TOTAL)},{contador}");

                            file.WriteLine(CABECERA_VAR);

                            foreach (VCONCILIACIONFACILITO e in listadoElementos)
                            {
                                string linea = GenerarLineaRegistro(e);

                                file.WriteLine(linea);
                            }

                            file.Close();
                        }
                    }
                }
               
                respuesta.CError = prop.VAR_MSJ_OK;
                respuesta.DError = prop.VAR_MSJ_OK_DET;
                
            }
            catch (Exception ex)
            {
                respuesta.CError = prop.VAR_MSJ_ERROR;
                respuesta.DError = string.Format($"{prop.VAR_MSJ_ERROR_DET}: {ex.Message.ToUpper().ToString()}");
            }

            return respuesta;
        }

        //en caso de que existan registros llenos genera la linea que debera ser insertada
        private string GenerarLineaRegistro(VCONCILIACIONFACILITO e)
        {
            string resp = string.Empty;
            resp = string.Format($"{e.REFERENCIA},{e.NUMEROMOVIMIENTO},{e.NUMEROCUENTAORIGEN},{e.NUMEROCUENTADESTINO},{e.CODIGODECLIENTE},{e.ESTADO},{e.TIPO},{e.SUBTIPO},{e.FECHAHORATRANSACCION},{e.VALOR},{e.COMISIONTOTAL}");
            return resp;
        }

        //crea el archivo de texto
        private CanalRespuesta CrearArchivo(string PATH_ARCHIVO)
        {
            CanalRespuesta r = new CanalRespuesta();

            try
            {
                if (File.Exists(PATH_ARCHIVO))
                {
                    File.Delete(PATH_ARCHIVO);
                    File.Create(PATH_ARCHIVO).Close();
                }
                else
                {
                    File.Create(PATH_ARCHIVO).Close();
                }

                r.CError = prop.VAR_MSJ_OK;
                r.DError = prop.VAR_MSJ_OK_DET;


            }
            catch (Exception ex)
            {
                r.CError = "991";
                r.DError = string.Format($"{prop.VAR_MSJ_ERROR_DET} {ex.Message.ToUpper().ToString()}");
            }

            return r;
        }
    }
}

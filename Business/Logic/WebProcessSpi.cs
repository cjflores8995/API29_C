using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ExcelDataReader;

namespace Business
{
    public class WebProcessSpi
    {
        public CanalRespuesta ReadDataSpi(string path, DateTime fechacorte, int numeroCorte)
        {
            CanalRespuesta resp = new CanalRespuesta();
            List<TSPI4DETALLES> list = new List<TSPI4DETALLES>();
            TSPI4DETALLES spi = new TSPI4DETALLES();
            DateTime _FECHAARCHIVO;
            int _NUMEROCORTE;

            try
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    string s = string.Empty;
                    int i = 0;
                    string[] registro = null;
                    resp.CError = "000";
                    resp.DError = "TRANSACCIÓN REALIZADA CORRECTAMENTE";

                    //elimino los cortes existentes en caso de que se encuentren insertados
                    spi.EliminarRegistrosSpi(fechacorte, numeroCorte);

                    while ((s = reader.ReadLine()) != null)
                    {
                        _FECHAARCHIVO = DateTime.Now;
                        _NUMEROCORTE = 0;

                        registro = s.Split(',');
                        i++;

                        //si es mayor a 1 no toma en cuenta la cabecera
                        if (i > 1)
                        {
                            DateTime FECHAPROCESO = fechacorte;
                            int NUMEROCORTE = numeroCorte;
                            DateTime FECHAVALIDACIONBCE = Convert.ToDateTime(registro[0]);
                            Int64 SGIROTRANSFERENCIAAUTORIZADO = Convert.ToInt64(registro[1]);
                            int SECUENCIALUNICOBCE = int.Parse(registro[3]);
                            DateTime FECHACOMPENSACIONCE = Convert.ToDateTime(registro[4]);
                            int ESTATUSOPI = int.Parse(registro[5]);
                            string DETALLE = s;

                            //creo el objeto
                            TSPI4DETALLES obj = new TSPI4DETALLES
                            {
                                FECHAPROCESO = FECHAPROCESO,
                                NUMEROCORTE = NUMEROCORTE,
                                FECHAVALIDACIONBCE = FECHAVALIDACIONBCE,
                                SGIROTRANSFERENCIAAUTORIZADO = SGIROTRANSFERENCIAAUTORIZADO,
                                SECUENCIALUNICOBCE = SECUENCIALUNICOBCE,
                                FECHACOMPENSACIONCE = FECHACOMPENSACIONCE,
                                ESTATUSOPI = ESTATUSOPI,
                                DETALLE = DETALLE
                            };

                            new TSPI4DETALLES().Insertar(obj);

                        }
                        else
                        {
                            _FECHAARCHIVO = Convert.ToDateTime(registro[0]);
                            _NUMEROCORTE = int.Parse(registro[3]);

                            if (fechacorte != _FECHAARCHIVO || numeroCorte != _NUMEROCORTE)
                            {
                                resp.CError = "999";
                                resp.DError = "LOS VALORES INGRESADOS POR EL USUARIO NO COINCIDEN CON LOS VALORES DEL ARCHIVO";
                                break;
                            }
                        }
                    }

                    reader.Close();
                }
            } catch(Exception ex)
            {
                resp.CError = "997";
                resp.DError = ex.Message.ToUpper();
            }
            
            return resp;
        }
    }
}



//abro y empiezo a leer el excel
/*using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
{
    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
    {
        DataSet result = excelReader.AsDataSet();

        DataTable table = result.Tables[0];

        foreach(DataRow row in table.Rows)
        {
            string celda1 = row[0].ToString();
            string celda2 = row[1].ToString();
            string celda3 = row[2].ToString();
            string celda4 = row[3].ToString();
            string celda5 = row[4].ToString();
            string celda6 = row[5].ToString();
            string celda7 = row[6].ToString();
            string celda8 = row[7].ToString();
            string celda9 = row[8].ToString();
            string celda10 = row[9].ToString();
            string celda11 = row[10].ToString();
            string celda12 = row[11].ToString();
            string celda13 = row[12].ToString();

            TSPI4DETALLES obj = new TSPI4DETALLES()
            {
                USUARIO = "2841",
                FECHACORTE = fechacorte,
                CORTE = numeroCorte,
                DETALLE = row.ToString(),
                CAMPO1 = celda1,
                CAMPO2 = celda2,
                CAMPO3 = celda3,
                CAMPO4 = celda4,
                CAMPO5 = celda5,
                CAMPO6 = celda6,
                CAMPO7 = celda7,
                CAMPO8 = celda8,
                CAMPO9 = celda9,
                CAMPO10 = celda10,
                CAMPO11 = celda11,
                CAMPO12 = celda12,
                CAMPO13 = celda13
            };

            list.Add(obj);
            obj = null;
        }

    }
}*/

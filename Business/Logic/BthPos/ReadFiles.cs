using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.BthPos
{
    class ReadFiles
    {


        public ReadFiles()
        {

        }

        /// <summary>
        /// Lee los archivos de datafast y los inserta en TPOSCOMPENSADETALLE
        /// </summary>
        public List<TPOSCOMPENSADETALLE> LeerDatafast(DateTime fproceso, VPOSCONVENIO objConvenio, string ruta, string archivo, out string error)
        {
            List<TPOSCOMPENSADETALLE> ltCompensacionDetalle = new List<TPOSCOMPENSADETALLE>();
            StreamReader srArchivo;
            List<TSWTERMINALESPOS> ltTerminales = null;
            TSWTERMINALESPOS eterminal = null;
            Int32 secuencia = 0;
            string tipo = string.Empty;
            string linea = string.Empty;
            string convenioComercio = string.Empty;
            string comercio = string.Empty;
            string lote = string.Empty;
            string terminal = string.Empty;
            Int32 numeroLinea = 0;
            error = "OK";
            try
            {
                secuencia = new TPOSCOMPENSADETALLE().ContarRegistrosProceso(fproceso, objConvenio.CCONVENIO);
                ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(objConvenio.CCONVENIO);
                if (ltTerminales != null && ltTerminales.Count > 0 && error == "OK")
                {
                    srArchivo = new StreamReader(ruta + archivo, System.Text.Encoding.Default);
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        numeroLinea++;
                        tipo = linea.Substring(0, 1);
                        switch (tipo)
                        {
                            case "1":
                                #region Cabecera
                                comercio = linea.Substring(1, 12);
                                lote = linea.Substring(19, 7);
                                terminal = linea.Substring(26, 8);
                                try
                                {
                                    eterminal = ltTerminales.Where(x => x.MID.PadRight(12, '0') == comercio).First();
                                    comercio = eterminal.MID;
                                    convenioComercio = eterminal.CCONVENIO.ToString();
                                }
                                catch
                                {
                                    convenioComercio = "0";
                                }
                                break;
                            #endregion Cabecera
                            case "2":
                                #region Detalle
                                if (objConvenio.CCONVENIO.Value == Convert.ToInt32(convenioComercio))
                                {
                                    #region Campos
                                    string tarjeta = linea.Substring(1, 19).Trim();
                                    string procesingCode = linea.Substring(20, 6).Trim();
                                    string ftransaccion = linea.Substring(26, 6).Trim();
                                    string htransaccion = linea.Substring(32, 6).Trim();
                                    string numerotransaccion = linea.Substring(38, 6).Trim().PadLeft(6, '0');
                                    string codigoaprobacion = linea.Substring(44, 6).Trim().PadLeft(6, '0');
                                    string montotransaccion = linea.Substring(50, 13).Trim();
                                    string autorizacionsourcecode = linea.Substring(63, 1).Trim();
                                    string tipocredito = linea.Substring(64, 2).Trim();
                                    string numerocuotas = linea.Substring(66, 2).Trim();
                                    string tipolectura = linea.Substring(68, 3).Trim();
                                    string tipomoneda = linea.Substring(71, 3).Trim();
                                    string iva = linea.Substring(74, 13).Trim();
                                    string servicio = linea.Substring(87, 13).Trim();
                                    string propina = linea.Substring(100, 13).Trim();
                                    string interes = linea.Substring(113, 13).Trim();
                                    string montofijo = linea.Substring(126, 13).Trim();
                                    string codigopromocion = linea.Substring(139, 2).Trim();
                                    string puntospromocion = linea.Substring(141, 3).Trim();
                                    string ice = linea.Substring(144, 13).Trim();
                                    string otrosimpuestos = linea.Substring(157, 13).Trim();
                                    string val1 = linea.Substring(170, 13).Trim();
                                    string cashover = linea.Substring(183, 13).Trim();
                                    string tarifa0 = linea.Substring(196, 13).Trim();
                                    string tarifa12 = linea.Substring(209, 13).Trim();
                                    string cestado = "CAR";
                                    #endregion Campos
                                    #region CreacionObjeto
                                    ltCompensacionDetalle.Add(new TPOSCOMPENSADETALLE
                                    {
                                        FPROCESO = fproceso,
                                        SECUENCIA = ++secuencia,
                                        ARCHIVO = archivo,
                                        CCONVENIO = Convert.ToInt32(convenioComercio),
                                        MID = comercio,
                                        TERMINAL = terminal,
                                        TARJETA = tarjeta,
                                        FTRANSACCION = new DateTime(
                                            Convert.ToInt16(DateTime.Now.Year.ToString().Substring(0, 2) + ftransaccion.Substring(0, 2)),
                                            Convert.ToInt16(ftransaccion.Substring(2, 2)),
                                            Convert.ToInt16(ftransaccion.Substring(4, 2)),
                                            Convert.ToInt16(htransaccion.Substring(0, 2)),
                                            Convert.ToInt16(htransaccion.Substring(2, 2)),
                                            Convert.ToInt16(htransaccion.Substring(4, 2))),
                                        FCARGA = DateTime.Now,
                                        LOTE = lote,
                                        NUMEROTRANSACCION = numerotransaccion,
                                        NUMEROAPROBACION = codigoaprobacion,
                                        VALORTRANSACCION = Util.StringToDecimal(montotransaccion),
                                        VALORIVA = Util.StringToDecimal(iva),
                                        VALORTARIFA0 = Util.StringToDecimal(tarifa0),
                                        VALORTARIFAIVA = Util.StringToDecimal(tarifa12),
                                        VALORCOMISION = 0,
                                        VALORRETENCIONFUENTE = 0,
                                        VALORRETENCIONIVA = 0,
                                        VALORLIQUIDADO = 0,
                                        CESTADO = cestado,
                                        LINEA = linea
                                    });
                                    #endregion CreacionObjeto
                                }
                                break;
                            #endregion Detalle
                            case "3":
                                break;
                            case "9":
                                break;
                            default:
                                break;
                        }
                    }
                    srArchivo.Close();
                }
                else
                {
                    ltCompensacionDetalle = null;
                    error = "NO EXISTEN TERMINALES PARA CONVENIO [" + objConvenio.CCONVENIO + "]";
                }
            }
            catch (Exception ex)
            {
                ltCompensacionDetalle = null;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return ltCompensacionDetalle;
        }

        /// <summary>
        /// Lee los archivos de la Favorita y los inserta en TPOSCOMPENSADETALLE
        /// </summary>
        public List<TPOSCOMPENSADETALLE> LeerFavorita(DateTime fproceso, VPOSCONVENIO objConvenio, string ruta, out string error)
        {
            List<TPOSCOMPENSADETALLE> ltCompensacionDetalle = new List<TPOSCOMPENSADETALLE>();
            StreamReader srArchivo;
            Int32 secuencia = 0;
            string tipo = string.Empty;
            string linea = string.Empty;
            string convenioComercio = string.Empty;
            string comercio = string.Empty;
            string lote = string.Empty;
            string terminal = string.Empty;
            Int32 numeroLinea = 0;
            string nombreArchivo = string.Empty;
            string tarjetaHash = string.Empty;
            string tarjeta = string.Empty;
            string ftransaccion = string.Empty;
            string numerotransaccion = string.Empty;
            string numeroaprobacion = string.Empty;
            string montotransaccion = string.Empty;
            string cestado = string.Empty;
            string tarjetaUltimosDigitos = string.Empty;

            error = "OK";
            try
            {
                secuencia = new TPOSCOMPENSADETALLE().ContarRegistrosProceso(fproceso, objConvenio.CCONVENIO);
                DirectoryInfo filesFavorita = new DirectoryInfo(ruta);
                foreach (var file in filesFavorita.EnumerateFiles())
                {
                    nombreArchivo = file.ToString();

                    srArchivo = new StreamReader(file.FullName, System.Text.Encoding.Default);

                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        numeroLinea++;
                        tipo = linea.Substring(0, 1);
                        switch (tipo)
                        {
                            #region detalle
                            case "1":
                                lote = linea.Substring(9, 10).Trim();
                                break;
                            case "2":
                                #region Campos

                                if (nombreArchivo.Contains(".108"))
                                {
                                    tarjetaHash = linea.Substring(23, 64).Trim();
                                    tarjetaUltimosDigitos = linea.Substring(188, 4).Trim();

                                    numeroaprobacion = linea.Substring(87, 10).Trim().Substring(4, 6);
                                    string preTarjeta = new ISO8583_HISTORICO().obtenerTarjetaFavorita(fproceso, numeroaprobacion, tarjetaUltimosDigitos);
                                    tarjeta = Util.validarTarjetaHash(preTarjeta, tarjetaHash);

                                    ftransaccion = linea.Substring(11, 8).Trim();
                                    numerotransaccion = linea.Substring(1, 10).Trim().Substring(4, 6);

                                    montotransaccion = linea.Substring(97, 15).Trim();
                                    string iva = linea.Substring(112, 15).Trim();
                                    string  iva12 = linea.Substring(157, 8).Trim();
                                    string  iva0 = linea.Substring(165, 8).Trim();
                                    cestado = "CAR";

                                    #region CreacionObjeto
                                    ltCompensacionDetalle.Add(new TPOSCOMPENSADETALLE
                                    {
                                        FPROCESO = fproceso,
                                        CCONVENIO = objConvenio.CCONVENIO,
                                        SECUENCIA = ++secuencia,
                                        ARCHIVO = file.Name,
                                        LOTE = lote,
                                        TARJETA = tarjeta,
                                        FTRANSACCION = new DateTime(
                                            Convert.ToInt16(ftransaccion.Substring(0, 4)),
                                            Convert.ToInt16(ftransaccion.Substring(4, 2)),
                                            Convert.ToInt16(ftransaccion.Substring(6, 2))),
                                        FCARGA = DateTime.Now,
                                        NUMEROTRANSACCION = numerotransaccion,
                                        NUMEROAPROBACION = numeroaprobacion,
                                        VALORTRANSACCION = Util.StringToDecimal(montotransaccion),
                                        VALORIVA = Util.StringToDecimal(iva),
                                        VALORTARIFA0 = Util.StringToDecimal(iva0),
                                        VALORTARIFAIVA = Util.StringToDecimal(iva12),
                                        VALORCOMISION = 0,
                                        VALORRETENCIONFUENTE = 0,
                                        VALORRETENCIONIVA = 0,
                                        VALORLIQUIDADO = 0,
                                        CESTADO = cestado,
                                        LINEA = linea
                                    });
                                    #endregion CreacionObjeto

                                }
                                else
                                {
                                    tarjeta = linea.Substring(23, 19).Trim().Substring(3, 16);
                                    ftransaccion = linea.Substring(11, 8).Trim();
                                    numerotransaccion = linea.Substring(1, 10).Trim().Substring(4, 6);
                                    numeroaprobacion = linea.Substring(42, 10).Trim().Substring(4, 6);
                                    montotransaccion = linea.Substring(52, 15).Trim();
                                    string iva = linea.Substring(67, 15).Trim();
                                    string iva0 = "0".PadLeft(15, '0');
                                    string iva12 = "0".PadLeft(15, '0');
                                    cestado = "CAR";

                                    #region CreacionObjeto
                                    ltCompensacionDetalle.Add(new TPOSCOMPENSADETALLE
                                    {
                                        FPROCESO = fproceso,
                                        CCONVENIO = objConvenio.CCONVENIO,
                                        SECUENCIA = ++secuencia,
                                        ARCHIVO = file.Name,
                                        LOTE = lote,
                                        TARJETA = tarjeta,
                                        FTRANSACCION = new DateTime(
                                            Convert.ToInt16(ftransaccion.Substring(0, 4)),
                                            Convert.ToInt16(ftransaccion.Substring(4, 2)),
                                            Convert.ToInt16(ftransaccion.Substring(6, 2))),
                                        FCARGA = DateTime.Now,
                                        NUMEROTRANSACCION = numerotransaccion,
                                        NUMEROAPROBACION = numeroaprobacion,
                                        VALORTRANSACCION = Util.StringToDecimal(montotransaccion),
                                        VALORIVA = Util.StringToDecimal(iva),
                                        VALORTARIFA0 = Util.StringToDecimal(iva0),
                                        VALORTARIFAIVA = Util.StringToDecimal(iva12),
                                        VALORCOMISION = 0,
                                        VALORRETENCIONFUENTE = 0,
                                        VALORRETENCIONIVA = 0,
                                        VALORLIQUIDADO = 0,
                                        CESTADO = cestado,
                                        LINEA = linea
                                    });
                                    #endregion CreacionObjeto


                                }

                                string anio = ftransaccion.Substring(0, 4);
                                string mes = ftransaccion.Substring(4, 2);
                                string dia = ftransaccion.Substring(6, 2);



                                #endregion Campos

                                

                                break;
                                #endregion detalle
                        }
                    }
                    
                    srArchivo.Close();
                }
            }
            catch (Exception ex)
            {
                ltCompensacionDetalle = null;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return ltCompensacionDetalle;
        }

        /// <summary>
        /// Lee los archivos de Econofarm y Farcomed y los inserta en TPOSCOMPENSADETALLE
        /// </summary>
        public List<TPOSCOMPENSADETALLE> LeerFarcomed(DateTime fproceso, VPOSCONVENIO objConvenio, string ruta, out string error)
        {
            List<TPOSCOMPENSADETALLE> ltCompensacionDetalle = new List<TPOSCOMPENSADETALLE>();
            StreamReader srArchivo;
            Int32 secuencia = 0;
            string tipo = string.Empty;
            string linea = string.Empty;
            string convenioComercio = string.Empty;
            string comercio = string.Empty;
            string lote = string.Empty;
            string terminal = string.Empty;
            Int32 numeroLinea = 0;
            error = "OK";
            try
            {
                secuencia = new TPOSCOMPENSADETALLE().ContarRegistrosProceso(fproceso, objConvenio.CCONVENIO);
                DirectoryInfo filesFybeca = new DirectoryInfo(ruta);
                foreach (var file in filesFybeca.EnumerateFiles())
                {
                    srArchivo = new StreamReader(file.FullName, System.Text.Encoding.Default);
                    while ((linea = srArchivo.ReadLine()) != null)
                    {
                        numeroLinea++;
                        tipo = linea.Substring(0, 1);
                        switch (tipo)
                        {
                            #region detalle
                            case "1":
                                lote = linea.Substring(9, 10).Trim().Substring(3, 7);
                                break;
                            case "2":
                                #region Campos
                                string tarjeta = linea.Substring(63, 24).Trim().Substring(8, 16);
                                string ftransaccion = linea.Substring(11, 8).Trim();
                                string numerotransaccion = linea.Substring(1, 10).Trim().Substring(2, 8);
                                string numeroaprobacion = linea.Substring(87, 10).Trim().Substring(4, 6);
                                string montotransaccion = linea.Substring(97, 15).Trim();
                                string iva = linea.Substring(112, 15).Trim();
                                string tarifaiva = linea.Substring(213, 14).Trim();
                                string tarifa0 = linea.Substring(227, 14).Trim();
                                string cestado = "CAR";
                                #endregion Campos
                                #region CreacionObjeto
                                ltCompensacionDetalle.Add(new TPOSCOMPENSADETALLE
                                {
                                    FPROCESO = fproceso,
                                    CCONVENIO = objConvenio.CCONVENIO,
                                    SECUENCIA = ++secuencia,
                                    ARCHIVO = file.Name,
                                    LOTE = lote,
                                    TARJETA = tarjeta,
                                    FTRANSACCION = new DateTime(
                                        Convert.ToInt16(ftransaccion.Substring(0, 4)),
                                        Convert.ToInt16(ftransaccion.Substring(4, 2)),
                                        Convert.ToInt16(ftransaccion.Substring(6, 2))),
                                    FCARGA = DateTime.Now,
                                    NUMEROTRANSACCION = numerotransaccion,
                                    NUMEROAPROBACION = numeroaprobacion,
                                    VALORTRANSACCION = Util.StringToDecimal(montotransaccion),
                                    VALORIVA = Util.StringToDecimal(iva),
                                    VALORTARIFA0 = 0,
                                    VALORTARIFAIVA = 0,
                                    VALORCOMISION = 0,
                                    VALORRETENCIONFUENTE = 0,
                                    VALORRETENCIONIVA = 0,
                                    VALORLIQUIDADO = 0,
                                    CESTADO = cestado,
                                    LINEA = linea
                                });
                                #endregion CreacionObjeto
                                break;
                                #endregion detalle
                        }
                    }
                    srArchivo.Close();
                }
            }
            catch (Exception ex)
            {
                ltCompensacionDetalle = null;
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return ltCompensacionDetalle;
        }
    }
}

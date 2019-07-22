using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.BthPos
{
    class StructureFiles
    {
        #region Variables
        private string campo_01;
        private string campo_02;
        private string campo_03;
        private string campo_04;
        private string campo_05;
        private string campo_06;
        private string campo_07;
        private string campo_08;
        private string campo_09;
        private string campo_10;
        private string campo_11;
        private string campo_12;
        private string campo_13;
        private string campo_14;
        private string campo_15;
        private string campo_16;
        private string campo_17;
        private string campo_18;
        private string campo_19;
        private string campo_20;
        private string campo_21;
        private string campo_22;
        private string campo_23;
        private string campo_24;
        private string campo_25;
        private string campo_26;
        private string campo_27;
        private string campo_28;
        private string campo_29;
        #endregion Variables

        #region Constructor
        public StructureFiles() {
            this.campo_01 = string.Empty;
            this.campo_02 = string.Empty;
            this.campo_03 = string.Empty;
            this.campo_04 = string.Empty;
            this.campo_05 = string.Empty;
            this.campo_06 = string.Empty;
            this.campo_07 = string.Empty;
            this.campo_08 = string.Empty;
            this.campo_09 = string.Empty;
            this.campo_10 = string.Empty;
            this.campo_11 = string.Empty;
            this.campo_12 = string.Empty;
            this.campo_13 = string.Empty;
            this.campo_14 = string.Empty;
            this.campo_15 = string.Empty;
            this.campo_16 = string.Empty;
            this.campo_17 = string.Empty;
            this.campo_18 = string.Empty;
            this.campo_19 = string.Empty;
            this.campo_20 = string.Empty;
            this.campo_21 = string.Empty;
            this.campo_22 = string.Empty;
            this.campo_23 = string.Empty;
            this.campo_24 = string.Empty;
            this.campo_25 = string.Empty;
            this.campo_26 = string.Empty;
            this.campo_27 = string.Empty;
            this.campo_28 = string.Empty;
            this.campo_29 = string.Empty;
        }
        #endregion Constructor

        #region Estructura Normal
        public string[] EstructuraNormal(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string nombreArchivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumador = 0;

            StructureFiles estructuranormal = new StructureFiles();

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    nombreArchivo = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, nombreArchivo) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("yyyyMMdd") + "29" + "COAC 29 DE OCTUBRE".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        estructuranormal.campo_01 = "2";
                        estructuranormal.campo_02 = "USD";
                        estructuranormal.campo_03 = detalle.LOTE.Trim().PadLeft(14, '0');
                        estructuranormal.campo_04 = detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0');
                        estructuranormal.campo_05 = ("0").PadLeft(14, '0');
                        estructuranormal.campo_06 = ("0").PadLeft(14, '0');
                        estructuranormal.campo_07 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_08 = "0101";
                        estructuranormal.campo_09 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadRight(14, ' ') ?? ("0").PadRight(14, ' ');
                        estructuranormal.campo_10 = "00";
                        estructuranormal.campo_11 = detalle.MID.PadRight(10, '0');
                        estructuranormal.campo_12 = Util.OfuscaTarjeta(detalle.TARJETA).PadRight(19, ' ');
                        estructuranormal.campo_13 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_14 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_15 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_16 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_17 = Util.DecimalToString(detalle.VALORRETENCIONIVA.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        estructuranormal.campo_18 = "01";
                        estructuranormal.campo_19 = "00";
                        estructuranormal.campo_20 = Util.DecimalToString(0).PadLeft(15, '0');

                        linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", estructuranormal.campo_01, estructuranormal.campo_02, estructuranormal.campo_03, estructuranormal.campo_04, estructuranormal.campo_05, estructuranormal.campo_06, estructuranormal.campo_07, estructuranormal.campo_08, estructuranormal.campo_09, estructuranormal.campo_10, estructuranormal.campo_11, estructuranormal.campo_12, estructuranormal.campo_13, estructuranormal.campo_14, estructuranormal.campo_15, estructuranormal.campo_16, estructuranormal.campo_17, estructuranormal.campo_18, estructuranormal.campo_19, estructuranormal.campo_20);

                        file.WriteLine(linea);
                        contador++;
                        sumador += detalle.VALORLIQUIDADO.Value;
                    }

                    linea = "3" + contador.ToString().PadLeft(5, '0') + ("0").PadLeft(5, '0') + ("0").PadLeft(18, '0') +
                        contador.ToString().PadLeft(5, '0') + Util.DecimalToString(sumador).PadLeft(18, '0');
                    file.WriteLine(linea);
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Normal

        #region Estructura FarmaEnlace
        public string[] EstructuraFarmaEnlace(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string nombreArchivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;

            decimal total_vale = 0;
            decimal sumatoria_retencion_iva = 0;
            decimal sumatoria_retencion_fte = 0;
            decimal sum_valor_liquidado = 0;
            decimal sum_valor_comision = 0;
            decimal sum_total_pagado = 0;

            StructureFiles farmaenlace = new StructureFiles();


            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    nombreArchivo = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, nombreArchivo) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + fautorizacion.ToString("yyyyMMdd") + ("X").PadLeft(15, 'X');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        farmaenlace.campo_01 = "2"; //tipo de registro(1)
                        farmaenlace.campo_02 = detalle.NUMEROTRANSACCION.Trim().PadLeft(8, '0'); //numero de vale(8)
                        farmaenlace.campo_03 = detalle.TARJETA ?? ("0").PadLeft(16, '0'); //numero de tarjeta(16)
                        farmaenlace.campo_04 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd") ?? null; //fecha del vale(8)
                        farmaenlace.campo_05 = "1"; //tipo de credito(1)
                        farmaenlace.campo_06 = "00"; //cuotas trasladadas (2)
                        farmaenlace.campo_07 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //total del vale(15)
                        farmaenlace.campo_08 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //comision(15)
                        farmaenlace.campo_09 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //valor neto(15)
                        farmaenlace.campo_10 = Util.DecimalToString(detalle.VALORRETENCIONIVA.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //retencion iva(15)
                        farmaenlace.campo_11 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //retencion en la fuente(15)
                        farmaenlace.campo_12 = "00"; //numero de cuotas(2)
                        farmaenlace.campo_13 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //total total(15)
                        farmaenlace.campo_14 = "00"; //causal rechazo(2)
                        farmaenlace.campo_15 = "00"; //causal rechazo, efectivamente se repite(2)
                        farmaenlace.campo_16 = "01"; //fipo de valie ->1 firme(2)
                        farmaenlace.campo_17 = detalle.NUMEROAPROBACION.Trim().PadLeft(7, '0') ?? ("0").PadLeft(7, '0'); //numero de recap
                        farmaenlace.campo_18 = detalle.LOTE.Trim().PadLeft(7, '0') ?? ("0").PadLeft(7, '0');
                        farmaenlace.campo_19 = "X".PadLeft(8, 'X'); //numero de comprobante de retencion(8)
                        farmaenlace.campo_20 = ("X").PadLeft(20, 'X'); //numero de comprobante de pago(20)
                        farmaenlace.campo_21 = ("X").PadLeft(10, 'X'); //numero de autorizacion(10)
                        farmaenlace.campo_22 = ("X").PadLeft(2, 'X'); //banco (2)
                        farmaenlace.campo_23 = ("X").PadLeft(22, 'X'); //numero de cuenta bancaria(22)
                        farmaenlace.campo_24 = ("20").PadLeft(8, 'X'); //nombre socio(texto) longitud indefinida
                        farmaenlace.campo_25 = ("X").PadLeft(7, 'X'); //numero de boleta procesa(7)
                        farmaenlace.campo_26 = ("X").PadLeft(8, 'X'); //liquidacion fecha de pago(8)
                        farmaenlace.campo_27 = ("X").PadLeft(16, 'X'); //base imponible comision(16)
                        farmaenlace.campo_28 = ("X").PadLeft(16, 'X'); //iva causado de comision(16)
                        farmaenlace.campo_29 = ("X").PadLeft(20, 'X'); //numero de factura iva comision(20)

                        linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}", farmaenlace.campo_01, farmaenlace.campo_02, farmaenlace.campo_03, farmaenlace.campo_04, farmaenlace.campo_05, farmaenlace.campo_06, farmaenlace.campo_07, farmaenlace.campo_08, farmaenlace.campo_09, farmaenlace.campo_10, farmaenlace.campo_11, farmaenlace.campo_12, farmaenlace.campo_13, farmaenlace.campo_14, farmaenlace.campo_15, farmaenlace.campo_16, farmaenlace.campo_17, farmaenlace.campo_18, farmaenlace.campo_19, farmaenlace.campo_20, farmaenlace.campo_21, farmaenlace.campo_22, farmaenlace.campo_23, farmaenlace.campo_24, farmaenlace.campo_25, farmaenlace.campo_26, farmaenlace.campo_27, farmaenlace.campo_28);

                        file.WriteLine(linea);
                        contador++;
                        //sumador += detalle.VALORLIQUIDADO.Value;

                        //Sumatoria de totales
                        total_vale += detalle.VALORTRANSACCION.Value;
                        sumatoria_retencion_iva += detalle.VALORRETENCIONIVA.Value;
                        sum_valor_liquidado += detalle.VALORLIQUIDADO.Value;
                        sum_valor_comision += detalle.VALORCOMISION.Value;
                        sumatoria_retencion_fte += detalle.VALORRETENCIONFUENTE.Value;
                        sum_total_pagado += detalle.VALORLIQUIDADO.Value - detalle.VALORRETENCIONIVA.Value - detalle.VALORRETENCIONFUENTE.Value;
                    }

                    linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        "3",
                        contador.ToString().PadLeft(6, '0'),
                        Util.DecimalToString(total_vale).PadLeft(16, '0'),
                        Util.DecimalToString(sumatoria_retencion_iva).PadLeft(16, '0'),
                        Util.DecimalToString(sum_valor_liquidado).PadLeft(16, '0'),
                        Util.DecimalToString(sum_valor_comision).PadLeft(16, '0'),
                        Util.DecimalToString(sumatoria_retencion_fte).PadLeft(16, '0'),
                        Util.DecimalToString(sum_total_pagado).PadLeft(16, '0'));

                    file.WriteLine(linea);
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura FarmaEnlace

        #region Estructura Normal Excel
        public string[] EstructuraNormalExcel(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            List<TSWTERMINALESPOS> ltTerminales = null;
            string[] respuesta = new string[2];
            string error = "OK";
            string archivoNombre = string.Empty;
            string archivo = string.Empty;
            Int32 numeroLinea = 0;

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(ltCompensados.FirstOrDefault().CCONVENIO);

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

                    sl.SetCellValue(1, 1, "RESUMEN COMPENSACION COAC 29 DE OCTUBRE");
                    sl.MergeWorksheetCells("A1", "O1");

                    sl.SetCellValue(2, 1, "FECHA AUTORIZACION: ");
                    sl.SetCellValue(2, 2, fautorizacion.ToString("dd/MM/yyyy"));
                    sl.SetCellStyle(2, 2, styleFechaCorta);

                    sl.SetCellValue(4, 01, "FECHA TRANSACCION");
                    sl.SetCellValue(4, 02, "TARJETA");
                    sl.SetCellValue(4, 03, "MID");
                    sl.SetCellValue(4, 04, "TERMINAL");
                    sl.SetCellValue(4, 05, "NUMERO TRANSACCION");
                    sl.SetCellValue(4, 06, "NUMERO APROBACION");
                    sl.SetCellValue(4, 07, "VALOR LIQUIDADO");
                    sl.SetCellValue(4, 08, "VALOR TRANSACCION");
                    sl.SetCellValue(4, 09, "VALOR IVA");
                    sl.SetCellValue(4, 10, "VALOR TARIFA IVA 0");
                    sl.SetCellValue(4, 11, "VALOR TARIFA IVA %");
                    sl.SetCellValue(4, 12, "VALOR COMISION");
                    sl.SetCellValue(4, 13, "VALOR IVA COMISION");
                    sl.SetCellValue(4, 14, "VALOR RETENCION FUENTE");
                    sl.SetCellValue(4, 15, "VALORRETENCIONIVA");

                    numeroLinea = 5;

                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        try
                        {
                            sl.SetCellValue(numeroLinea, 1, detalle.FTRANSACCION.Value.ToString("dd/MM/yyyy"));
                        }
                        catch { sl.SetCellValue(numeroLinea, 1, string.Empty); }
                        sl.SetCellStyle(numeroLinea, 1, styleFechaCorta);

                        sl.SetCellValue(numeroLinea, 2, Util.OfuscaTarjeta(detalle.TARJETA));

                        sl.SetCellValue(numeroLinea, 3, detalle.MID);

                        sl.SetCellValue(numeroLinea, 4, detalle.TERMINAL);

                        sl.SetCellValue(numeroLinea, 5, detalle.NUMEROTRANSACCION.Trim());

                        sl.SetCellValue(numeroLinea, 6, detalle.NUMEROAPROBACION.Trim());

                        try { sl.SetCellValue(numeroLinea, 7, detalle.VALORLIQUIDADO.Value); }
                        catch { sl.SetCellValue(numeroLinea, 7, 0); }
                        sl.SetCellStyle(numeroLinea, 7, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 8, detalle.VALORTRANSACCION.Value); }
                        catch { sl.SetCellValue(numeroLinea, 8, 0); }
                        sl.SetCellStyle(numeroLinea, 8, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 9, detalle.VALORIVA.Value); }
                        catch { sl.SetCellValue(numeroLinea, 9, 0); }
                        sl.SetCellStyle(numeroLinea, 9, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 10, detalle.VALORTARIFA0.Value); }
                        catch { sl.SetCellValue(numeroLinea, 10, 0); }
                        sl.SetCellStyle(numeroLinea, 10, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 11, detalle.VALORTARIFAIVA.Value); }
                        catch { sl.SetCellValue(numeroLinea, 11, 0); }
                        sl.SetCellStyle(numeroLinea, 11, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 12, detalle.VALORCOMISION.Value); }
                        catch { sl.SetCellValue(numeroLinea, 12, 0); }
                        sl.SetCellStyle(numeroLinea, 12, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 13, detalle.VALORIVACOMISION.Value); }
                        catch { sl.SetCellValue(numeroLinea, 13, 0); }
                        sl.SetCellStyle(numeroLinea, 13, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 14, detalle.VALORRETENCIONFUENTE.Value); }
                        catch { sl.SetCellValue(numeroLinea, 14, 0); }
                        sl.SetCellStyle(numeroLinea, 14, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 15, detalle.VALORRETENCIONIVA.Value); }
                        catch { sl.SetCellValue(numeroLinea, 15, 0); }
                        sl.SetCellStyle(numeroLinea, 15, styleNumeros);

                        numeroLinea++;
                    }

                    sl.SetCellValue(numeroLinea, 07, "=SUM(G5:G" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 08, "=SUM(H5:H" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 09, "=SUM(I5:I" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 10, "=SUM(J5:J" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 11, "=SUM(K5:K" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 12, "=SUM(L5:L" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 13, "=SUM(M5:M" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 14, "=SUM(N5:N" + (numeroLinea - 1) + ")");
                    sl.SetCellValue(numeroLinea, 15, "=SUM(O5:O" + (numeroLinea - 1) + ")");

                    sl.Filter("A4", "O4");
                    sl.AutoFitColumn("A4", "O4");

                    archivoNombre = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, archivoNombre) + ".xlsx";
                    sl.SaveAs(ruta + archivo);
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Normal Excel

        #region Estructura Etatex
        public string[] EstructuraEtatex(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivoNombre = string.Empty;
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal valotTarifa0 = 0;
            decimal valorTarifaiva = 0;
            decimal subtotal = 0;
            decimal sumador = 0;
            string campo_01 = string.Empty;
            string campo_02 = string.Empty;
            string campo_03 = string.Empty;
            string campo_04 = string.Empty;
            string campo_05 = string.Empty;
            string campo_06 = string.Empty;
            string campo_07 = string.Empty;
            string campo_08 = string.Empty;
            string campo_09 = string.Empty;
            string campo_10 = string.Empty;
            string campo_11 = string.Empty;
            string campo_12 = string.Empty;
            string campo_13 = string.Empty;
            string campo_14 = string.Empty;
            string campo_15 = string.Empty;
            string campo_16 = string.Empty;
            string campo_17 = string.Empty;
            string campo_18 = string.Empty;
            string campo_19 = string.Empty;
            string campo_20 = string.Empty;

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    archivoNombre = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, archivoNombre) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("yyyyMMdd") + "29" + "COAC 29 DE OCTUBRE".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        campo_01 = "2";
                        campo_02 = "USD";
                        campo_03 = detalle.LOTE.Trim().PadLeft(14, '0');
                        campo_04 = detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0');
                        campo_05 = ("0").PadLeft(14, '0');
                        campo_06 = ("0").PadLeft(14, '0');

                        try { campo_07 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0'); }
                        catch { campo_07 = ("0").PadLeft(15, '0'); }

                        campo_08 = "0101";

                        try { campo_09 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(14, '0'); }
                        catch { campo_09 = ("0").PadLeft(14, '0'); }

                        campo_10 = "00";
                        campo_11 = detalle.MID.PadLeft(10, '0');
                        campo_12 = Util.OfuscaTarjeta(detalle.TARJETA).PadLeft(19, '0');

                        try { valotTarifa0 = detalle.VALORTARIFA0.Value; }
                        catch { valotTarifa0 = 0; }

                        try { valorTarifaiva = detalle.VALORTARIFAIVA.Value; }
                        catch { valorTarifaiva = 0; }

                        subtotal = valotTarifa0 + valorTarifaiva;

                        campo_13 = Util.DecimalToString(subtotal).PadLeft(15, '0');

                        try { campo_14 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0'); }
                        catch { campo_14 = ("0").PadLeft(15, '0'); }

                        try { campo_15 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0'); }
                        catch { campo_15 = ("0").PadLeft(15, '0'); }

                        try { campo_16 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0'); }
                        catch { campo_16 = ("0").PadLeft(15, '0'); }

                        try { campo_17 = Util.DecimalToString(detalle.VALORRETENCIONIVA.Value).PadLeft(15, '0'); }
                        catch { campo_17 = ("0").PadLeft(15, '0'); }

                        campo_18 = "01";
                        campo_19 = "00";

                        try { campo_20 = Util.DecimalToString(detalle.VALORIVACOMISION.Value).PadLeft(15, '0'); }
                        catch { campo_20 = ("0").PadLeft(15, '0'); }

                        linea = campo_01 + campo_02 + campo_03 + campo_04 + campo_05 +
                            campo_06 + campo_07 + campo_08 + campo_09 + campo_10 +
                            campo_11 + campo_12 + campo_13 + campo_14 + campo_15 +
                            campo_16 + campo_17 + campo_18 + campo_19 + campo_20;

                        file.WriteLine(linea);
                        contador++;
                        sumador += detalle.VALORLIQUIDADO.Value;
                    }

                    linea = "3" + contador.ToString().PadLeft(5, '0') + ("0").PadLeft(5, '0') + ("0").PadLeft(18, '0') +
                        contador.ToString().PadLeft(5, '0') + Util.DecimalToString(sumador).PadLeft(18, '0');
                    file.WriteLine(linea);
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Etatex

        #region Estructura Tia
        public string[] EstructuraTia(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumador = 0;
            string campo_01 = string.Empty;
            string campo_02 = string.Empty;
            string campo_03 = string.Empty;
            string campo_04 = string.Empty;
            string campo_05 = string.Empty;
            string campo_06 = string.Empty;
            string campo_07 = string.Empty;
            string campo_08 = string.Empty;
            string campo_09 = string.Empty;
            string campo_10 = string.Empty;
            string campo_11 = string.Empty;
            string campo_12 = string.Empty;
            string campo_13 = string.Empty;
            string campo_14 = string.Empty;
            string campo_15 = string.Empty;
            string campo_16 = string.Empty;
            string campo_17 = string.Empty;
            string campo_18 = string.Empty;
            string campo_19 = string.Empty;
            string campo_20 = string.Empty;

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    archivo = string.Format(convenio.ARCHIVORETRONO, fautorizacion.ToString("yyyyMMdd")) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("ddMMyyyy") + "29" + "29CARD".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        campo_01 = "2";
                        campo_02 = "USD";
                        campo_03 = "0000000";
                        if (detalle.TERMINAL.Trim().Length <= 6)
                            campo_04 = detalle.TERMINAL.Trim().PadLeft(6, '0');
                        else
                            campo_04 = detalle.TERMINAL.Trim().Substring(0, 7);
                        campo_05 = detalle.NUMEROAPROBACION.Trim().PadLeft(14, '0');
                        campo_06 = detalle.LOTE.Trim().PadLeft(14, '0');
                        campo_07 = detalle.NUMEROAPROBACION.Trim().PadLeft(14, '0');
                        if (detalle.VALORLIQUIDADO != null)
                            campo_08 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0');
                        else
                            campo_08 = ("0").PadLeft(15, '0');
                        campo_09 = "0101";
                        if (detalle.FTRANSACCION != null)
                            campo_10 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(14, '0');
                        else
                            campo_10 = ("0").PadLeft(14, ' ');
                        campo_11 = "00";
                        campo_12 = detalle.MID.Trim().PadLeft(10, '0');
                        campo_13 = detalle.TARJETA.Trim().PadLeft(19, '0');
                        try { campo_14 = Util.DecimalToString(Math.Round(detalle.VALORTARIFA0.Value + detalle.VALORTARIFAIVA.Value, 2)).PadLeft(15, '0'); }
                        catch { campo_14 = ("0").PadLeft(15, '0'); }
                        try { campo_15 = Util.DecimalToString(Math.Round(((detalle.VALORTARIFA0.Value + detalle.VALORTARIFAIVA.Value) * (convenio.COMISION.Value / 100)), 2)).PadLeft(15, '0'); }
                        catch { campo_15 = ("0").PadLeft(15, '0'); }
                        campo_16 = "000000000000000";
                        if (detalle.VALORIVA != null)
                            campo_17 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0');
                        else
                            campo_17 = ("0").PadLeft(15, '0');
                        campo_18 = "000000000000000";
                        campo_19 = "01";
                        campo_20 = "01";
                        linea =
                            campo_01 + campo_02 + campo_03 + campo_04 + campo_05 +
                            campo_06 + campo_07 + campo_08 + campo_09 + campo_10 +
                            campo_11 + campo_12 + campo_13 + campo_14 + campo_15 +
                            campo_16 + campo_17 + campo_18 + campo_19 + campo_20;
                        file.WriteLine(linea);
                        contador++;
                        sumador += detalle.VALORLIQUIDADO.Value;
                    }
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Tia

        #region Estructura Santa Maria
        public string[] EstructuraSantaMaria(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            List<TSWTERMINALESPOS> ltTerminales = null;
            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal subtotal = 0;
            decimal sumador = 0;
            string campo_01 = string.Empty;
            string campo_02 = string.Empty;
            string campo_03 = string.Empty;
            string campo_04 = string.Empty;
            string campo_05 = string.Empty;
            string campo_06 = string.Empty;
            string campo_07 = string.Empty;
            string campo_08 = string.Empty;
            string campo_09 = string.Empty;
            string campo_10 = string.Empty;
            string campo_11 = string.Empty;
            string campo_12 = string.Empty;
            string campo_13 = string.Empty;
            string campo_14 = string.Empty;
            string campo_15 = string.Empty;
            string campo_16 = string.Empty;
            string campo_17 = string.Empty;
            string campo_18 = string.Empty;
            string campo_19 = string.Empty;
            string campo_20 = string.Empty;

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(ltCompensados.FirstOrDefault().CCONVENIO);
                    archivo = string.Format(convenio.ARCHIVORETRONO, fautorizacion.ToString("yyyyMMdd")) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("yyyyMMdd") + "29" + "COAC29DEOCTUBRE".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        campo_01 = "2";
                        campo_02 = "USD";
                        try
                        {
                            campo_03 =
                                (ltTerminales.Where(x => x.MID == detalle.MID).First().CODIGOALTERNO.PadLeft(3, '0') +
                                detalle.FTRANSACCION.Value.DayOfYear.ToString().PadLeft(3, '0')).PadLeft(14, '0');
                        }
                        catch
                        { campo_03 = ("0").PadLeft(14, '0'); }

                        campo_04 = detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0');
                        campo_05 = ("0").PadLeft(14, '0');
                        campo_06 = ("0").PadLeft(14, '0');
                        if (detalle.VALORLIQUIDADO != null)
                            campo_07 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0');
                        else
                            campo_07 = ("0").PadLeft(15, '0');
                        campo_08 = "0101";
                        if (detalle.FTRANSACCION != null)
                            campo_09 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(14, '0');
                        else
                            campo_09 = ("0").PadRight(14, ' ');
                        campo_10 = "00";
                        campo_11 = detalle.MID.PadRight(10, '0');
                        campo_12 = Util.OfuscaTarjeta(detalle.TARJETA, 6, 4).PadLeft(19, '0');
                        if (detalle.VALORTARIFA0 != null && detalle.VALORTARIFAIVA != null)
                        {
                            subtotal = detalle.VALORTARIFA0.Value + detalle.VALORTARIFAIVA.Value;
                            campo_13 = Util.DecimalToString(subtotal).PadLeft(15, '0');
                        }
                        else
                            campo_13 = ("0").PadLeft(15, '0');
                        if (detalle.VALORCOMISION != null)
                            campo_14 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0');
                        else
                            campo_14 = ("0").PadLeft(15, '0');
                        if (detalle.VALORRETENCIONFUENTE != null)
                            campo_15 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0');
                        else
                            campo_15 = ("0").PadLeft(15, '0');
                        if (detalle.VALORIVA != null)
                            campo_16 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0');
                        else
                            campo_16 = ("0").PadLeft(15, '0');
                        if (detalle.VALORRETENCIONIVA != null)
                            campo_17 = Util.DecimalToString(detalle.VALORRETENCIONIVA.Value).PadLeft(15, '0');
                        else
                            campo_17 = ("0").PadLeft(15, '0');

                        campo_18 = "01";

                        campo_19 = "00";

                        linea =
                            campo_01 + campo_02 + campo_03 + campo_04 + campo_05 +
                            campo_06 + campo_07 + campo_08 + campo_09 + campo_10 +
                            campo_11 + campo_12 + campo_13 + campo_14 + campo_15 +
                            campo_16 + campo_17 + campo_18 + campo_19 + campo_20;

                        file.WriteLine(linea);

                        contador++;
                        sumador += detalle.VALORLIQUIDADO.Value;
                    }

                    linea =
                        "3" + contador.ToString().PadLeft(5, '0') + ("0").PadLeft(5, '0') + ("0").PadLeft(18, '0') +
                        contador.ToString().PadLeft(5, '0') + Util.DecimalToString(sumador).PadLeft(18, '0');

                    file.WriteLine(linea);

                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Santa Maria

        #region Estructura Santa Maria Excel
        public string[] EstructuraSantaMariaExcel(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            List<TSWTERMINALESPOS> ltTerminales = null;
            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal subtotal = 0;
            Int32 numeroLinea = 0;
            decimal sumador = 0;

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(ltCompensados.FirstOrDefault().CCONVENIO);

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

                    sl.SetCellValue(1, 1, "1");
                    sl.SetCellValue(1, 2, "VTC");
                    sl.SetCellValue(1, 3, "UIO");
                    sl.SetCellValue(1, 4, fautorizacion.ToString("yyyyMMdd"));
                    sl.SetCellValue(1, 5, "29");
                    sl.SetCellValue(1, 6, "COAC 29 DE OCTUBRE");

                    numeroLinea = 2;

                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        sl.SetCellValue(numeroLinea, 1, "2");

                        sl.SetCellValue(numeroLinea, 2, "USD");

                        try
                        {
                            sl.SetCellValue(numeroLinea, 3,
                                (ltTerminales.Where(x => x.MID == detalle.MID).First().CODIGOALTERNO.PadLeft(3, '0') +
                                detalle.FTRANSACCION.Value.DayOfYear.ToString().PadLeft(3, '0')).PadLeft(14, '0'));
                        }
                        catch
                        { sl.SetCellValue(numeroLinea, 3, ("0").PadLeft(14, '0')); }

                        sl.SetCellValue(numeroLinea, 4, detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0'));

                        sl.SetCellValue(numeroLinea, 5, ("0").PadLeft(14, '0'));

                        sl.SetCellValue(numeroLinea, 6, ("0").PadLeft(14, '0'));

                        try { sl.SetCellValue(numeroLinea, 7, detalle.VALORLIQUIDADO.Value); }
                        catch { sl.SetCellValue(numeroLinea, 7, 0); }
                        sl.SetCellStyle(numeroLinea, 7, styleNumeros);

                        sl.SetCellValue(numeroLinea, 8, "0101");

                        try
                        {
                            sl.SetCellValue(numeroLinea, 9, detalle.FTRANSACCION.Value.ToString("yyyyMMdd"));
                        }
                        catch { sl.SetCellValue(numeroLinea, 9, string.Empty); }

                        sl.SetCellValue(numeroLinea, 10, "00");

                        sl.SetCellValue(numeroLinea, 11, detalle.MID);

                        sl.SetCellValue(numeroLinea, 12, Util.OfuscaTarjeta(detalle.TARJETA, 6, 4));


                        try
                        {
                            subtotal = detalle.VALORTARIFA0.Value + detalle.VALORTARIFAIVA.Value;
                            sl.SetCellValue(numeroLinea, 13, subtotal);
                        }
                        catch { sl.SetCellValue(numeroLinea, 13, 0); }
                        sl.SetCellStyle(numeroLinea, 13, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 14, detalle.VALORCOMISION.Value); }
                        catch { sl.SetCellValue(numeroLinea, 14, 0); }
                        sl.SetCellStyle(numeroLinea, 14, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 15, detalle.VALORRETENCIONFUENTE.Value); }
                        catch { sl.SetCellValue(numeroLinea, 15, 0); }
                        sl.SetCellStyle(numeroLinea, 15, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 16, detalle.VALORIVA.Value); }
                        catch { sl.SetCellValue(numeroLinea, 16, 0); }
                        sl.SetCellStyle(numeroLinea, 16, styleNumeros);

                        try { sl.SetCellValue(numeroLinea, 17, detalle.VALORRETENCIONIVA.Value); }
                        catch { sl.SetCellValue(numeroLinea, 17, 0); }
                        sl.SetCellStyle(numeroLinea, 17, styleNumeros);

                        sl.SetCellValue(numeroLinea, 18, "01");

                        sl.SetCellValue(numeroLinea, 19, "00");

                        numeroLinea++;
                        contador++;
                        sumador += detalle.VALORLIQUIDADO.Value;
                    }

                    sl.SetCellValue(numeroLinea, 1, "3");

                    sl.SetCellValue(numeroLinea, 2, contador);

                    sl.SetCellValue(numeroLinea, 3, 0);
                    sl.SetCellStyle(numeroLinea, 3, styleNumeros);

                    sl.SetCellValue(numeroLinea, 4, 0);
                    sl.SetCellStyle(numeroLinea, 4, styleNumeros);

                    sl.SetCellValue(numeroLinea, 5, contador);

                    sl.SetCellValue(numeroLinea, 6, sumador);
                    sl.SetCellStyle(numeroLinea, 6, styleNumeros);

                    archivo = string.Format(convenio.ARCHIVORETRONO, fautorizacion.ToString("yyyyMMdd")) + ".xlsx";
                    sl.SaveAs(ruta + archivo);
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Santa Maria Excel

        #region Estructura La Favorita Nueva
        public string[] EstructuraFavoritaNueva(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            List<TSWTERMINALESPOS> ltTerminales = null;
            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumador = 0;
            string tarjeta_hash = string.Empty;

            StructureFiles lafavorita = new StructureFiles();

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(ltCompensados.FirstOrDefault().CCONVENIO);
                    archivo = string.Format(convenio.ARCHIVORETRONO, fautorizacion.ToString("MMdd")) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("ddMMyyyy") + "29" + "29CARD".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        lafavorita.campo_01 = "2"; //ok
                        lafavorita.campo_02 = "USD"; //ok
                        lafavorita.campo_03 = detalle.LOTE.Trim().PadLeft(14, '0'); //ok
                        lafavorita.campo_04 = detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0'); //ok
                        lafavorita.campo_05 = ("0").PadLeft(14, '0'); //ok
                        lafavorita.campo_06 = ("0").PadLeft(14, '0'); //ok
                        lafavorita.campo_07 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_08 = "0101"; //ok
                        lafavorita.campo_09 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(14, '0') ?? ("0").PadRight(14, ' '); //ok
                        lafavorita.campo_10 = "00"; //ok
                        lafavorita.campo_11 = ("1" + detalle.MID.Substring(6, 3)).PadLeft(10, '0'); //ok
                        lafavorita.campo_12 = Util.HashSHA256(detalle.TARJETA);
                        lafavorita.campo_13 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_14 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_15 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_16 = Util.DecimalToString(detalle.VALORTARIFA0.Value + detalle.VALORTARIFAIVA.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_17 = Util.DecimalToString(detalle.VALORRETENCIONIVA.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0'); //ok
                        lafavorita.campo_18 = "01"; //ok
                        lafavorita.campo_19 = "01"; //ok
                        lafavorita.campo_20 = Util.OfuscaTarjeta(detalle.TARJETA, 6, 4).PadLeft(19, '0');
                        lafavorita.campo_21 = Util.DecimalToString(detalle.VALORIVACOMISION.Value).PadLeft(15, '0') ?? ("0").PadLeft(15, '0');
                        lafavorita.campo_22 = detalle.NUMEROAPROBACION.Trim().PadLeft(10, '0');

                        linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}", lafavorita.campo_01, lafavorita.campo_02, lafavorita.campo_03, lafavorita.campo_04, lafavorita.campo_05, lafavorita.campo_06, lafavorita.campo_07, lafavorita.campo_08, lafavorita.campo_09, lafavorita.campo_10, lafavorita.campo_11, tarjeta_hash, lafavorita.campo_12, lafavorita.campo_13, lafavorita.campo_14, lafavorita.campo_15, lafavorita.campo_16, lafavorita.campo_17, lafavorita.campo_18, lafavorita.campo_19, lafavorita.campo_20, lafavorita.campo_21, lafavorita.campo_22);
                        linea = linea + lafavorita.campo_22;

                        file.WriteLine(linea);
                        contador++;
                        sumador += detalle.VALORTRANSACCION.Value;
                    }
                    linea = String.Format("{0}{1}{2}{3}{4}{5}",
                        "3", //1 ok
                        (contador).ToString().PadLeft(5, '0'), //2 ok
                        "0".PadLeft(5, '0'), //3 total de registros en sucres ok
                        "0".PadLeft(18, '0'), //4 valor de los pagos en sucres
                        (contador).ToString().PadLeft(5, '0'), //5
                        Util.DecimalToString(sumador).PadLeft(18, '0')); //6

                    file.WriteLine(linea);
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura La Favorita Nueva

        #region Estructura La Favorita
        public string[] EstructuraFavorita(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            List<TSWTERMINALESPOS> ltTerminales = null;
            string[] respuesta = new string[2];
            string error = "OK";
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumador = 0;

            StructureFiles lafavorita = new StructureFiles();

            #endregion variables
            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    ltTerminales = new TSWTERMINALESPOS().ListarTerminalesXConvenio(ltCompensados.FirstOrDefault().CCONVENIO);
                    archivo = string.Format(convenio.ARCHIVORETRONO, fautorizacion.ToString("MMdd")) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);
                    linea = "1" + "VTC" + "UIO" + fautorizacion.ToString("ddMMyyyy") + "29" + "29CARD".PadRight(30, ' ');
                    file.WriteLine(linea);
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        lafavorita.campo_01 = "2";
                        lafavorita.campo_02 = "USD";
                        lafavorita.campo_03 = detalle.LOTE.Trim().PadLeft(14, '0');
                        lafavorita.campo_04 = detalle.NUMEROTRANSACCION.Trim().PadLeft(14, '0');
                        lafavorita.campo_05 = ("0").PadLeft(14, '0');
                        lafavorita.campo_06 = ("0").PadLeft(14, '0');
                        lafavorita.campo_07 = (detalle.VALORTRANSACCION != null) ? Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0') : ("0").PadLeft(15, '0');
                        lafavorita.campo_08 = "0101";
                        lafavorita.campo_09 = (detalle.FTRANSACCION != null) ? detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(14, '0') : ("0").PadRight(14, ' ');
                        lafavorita.campo_10 = "00";
                        lafavorita.campo_11 = ("1" + detalle.MID.Substring(6, 3)).PadLeft(10, '0');
                        lafavorita.campo_12 = detalle.TARJETA.PadLeft(19, '0');
                        lafavorita.campo_13 = lafavorita.campo_07;
                        lafavorita.campo_14 = ("0").PadLeft(15, '0');
                        lafavorita.campo_15 = ("0").PadLeft(15, '0');
                        lafavorita.campo_16 = ("0").PadLeft(15, '0');
                        lafavorita.campo_17 = ("0").PadLeft(15, '0');
                        lafavorita.campo_18 = "01";
                        lafavorita.campo_19 = "01";
                        lafavorita.campo_20 = ("0").PadLeft(15, '0');


                        linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", lafavorita.campo_01, lafavorita.campo_02, lafavorita.campo_03, lafavorita.campo_04, lafavorita.campo_05, lafavorita.campo_06, lafavorita.campo_07, lafavorita.campo_08, lafavorita.campo_09, lafavorita.campo_10, lafavorita.campo_11, lafavorita.campo_12, lafavorita.campo_13, lafavorita.campo_14, lafavorita.campo_15, lafavorita.campo_16, lafavorita.campo_17, lafavorita.campo_18, lafavorita.campo_19, lafavorita.campo_20);

                        file.WriteLine(linea);
                        contador++;
                        sumador += detalle.VALORTRANSACCION.Value;
                    }
                    linea = String.Format("{0}{1}{2}{3}{4}",
                        "3",
                        (contador + 2).ToString().PadLeft(5, '0'),
                        ("0").PadLeft(5, '0') + ("0").PadLeft(18, '0'),
                        contador.ToString().PadLeft(5, '0'),
                        Util.DecimalToString(sumador).PadLeft(18, '0'));
                        
                    file.WriteLine(linea);
                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura La favorita

        #region Estructura Farcomed
        public string[] EstructuraFacomed(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivoNombre = string.Empty;
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumadorTotal = 0;
            decimal sumadorIva = 0;
            decimal sumadorPagar = 0;
            decimal sumadorComision = 0;
            decimal sumadorRetencion = 0;
            decimal sumadorCalculo = 0;
            string campo_01 = string.Empty;
            string campo_02 = string.Empty;
            string campo_03 = string.Empty;
            string campo_04 = string.Empty;
            string campo_05 = string.Empty;
            string campo_06 = string.Empty;
            string campo_07 = string.Empty;
            string campo_08 = string.Empty;
            string campo_09 = string.Empty;
            string campo_10 = string.Empty;
            string campo_11 = string.Empty;
            string campo_12 = string.Empty;
            string campo_13 = string.Empty;
            string campo_14 = string.Empty;
            string campo_15 = string.Empty;
            string campo_16 = string.Empty;
            string campo_17 = string.Empty;
            string campo_18 = string.Empty;
            string campo_19 = string.Empty;
            string campo_20 = string.Empty;
            string campo_21 = string.Empty;
            string campo_22 = string.Empty;
            string campo_23 = string.Empty;

            #endregion variables

            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    archivoNombre = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, archivoNombre) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);

                    linea =
                        "1" +
                        fautorizacion.ToString("yyyyMMdd") +
                        "0000000" +
                        "01" +
                        "COAC29DB";

                    file.WriteLine(linea);

                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        campo_01 = "2";

                        campo_02 = detalle.NUMEROTRANSACCION.PadLeft(10, '0');

                        campo_03 = ("0").PadLeft(64, '0');

                        if (detalle.FTRANSACCION != null)
                            campo_04 = detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(8, '0');
                        else
                            campo_04 = ("0").PadLeft(8, '0');

                        campo_05 = "1";

                        campo_06 = "0";

                        if (detalle.VALORTRANSACCION != null)
                            campo_07 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0');
                        else
                            campo_07 = ("0").PadLeft(15, '0');

                        if (detalle.VALORCOMISION != null)
                            campo_08 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0');
                        else
                            campo_08 = ("0").PadLeft(15, '0');

                        if (detalle.VALORLIQUIDADO != null)
                            campo_09 = Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0');
                        else
                            campo_09 = ("0").PadLeft(15, '0');

                        if (detalle.VALORIVA != null)
                            campo_10 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0');
                        else
                            campo_10 = ("0").PadLeft(15, '0');

                        if (detalle.VALORRETENCIONFUENTE != null)
                            campo_11 = Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0');
                        else
                            campo_11 = ("0").PadLeft(15, '0');

                        campo_12 = "00";

                        if (detalle.VALORTRANSACCION != null)
                            campo_12 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0');
                        else
                            campo_12 = ("0").PadLeft(15, '0');

                        if (detalle.VALORTRANSACCION != null)
                            campo_13 = Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0');
                        else
                            campo_13 = ("0").PadLeft(15, '0');

                        campo_14 = "00";

                        campo_15 = "00";

                        campo_16 = "01";

                        campo_17 = detalle.LOTE.Trim().PadLeft(10, '0');

                        campo_18 = ("0").PadLeft(15, '0');

                        campo_19 = detalle.NUMEROAPROBACION.Trim().PadLeft(10, '0');

                        campo_20 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(16, '0');

                        campo_21 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(16, '0');

                        campo_22 = ("0").PadLeft(20, '0');

                        campo_23 = Util.OfuscaTarjeta(detalle.TARJETA.Trim()).PadLeft(16, '0');

                        linea =
                            campo_01 + campo_02 + campo_03 + campo_04 + campo_05 +
                            campo_06 + campo_07 + campo_08 + campo_09 + campo_10 +
                            campo_11 + campo_12 + campo_13 + campo_14 + campo_15 +
                            campo_16 + campo_17 + campo_18 + campo_19 + campo_20 +
                            campo_21 + campo_22 + campo_23;

                        file.WriteLine(linea);

                        contador++;
                        sumadorTotal += detalle.VALORTRANSACCION.Value;
                        sumadorIva += detalle.VALORIVA.Value;
                        sumadorComision += detalle.VALORCOMISION.Value;
                        sumadorPagar += detalle.VALORLIQUIDADO.Value;
                        sumadorRetencion += detalle.VALORRETENCIONFUENTE.Value;
                        sumadorCalculo += (detalle.VALORLIQUIDADO.Value + detalle.VALORRETENCIONFUENTE.Value);
                    }

                    linea =
                        "3" +
                        contador.ToString().PadLeft(6, '0') +
                        Util.DecimalToString(sumadorTotal).PadLeft(16, '0') +
                        Util.DecimalToString(sumadorIva).PadLeft(16, '0') +
                        Util.DecimalToString(sumadorPagar).PadLeft(16, '0') +
                        Util.DecimalToString(sumadorComision).PadLeft(16, '0') +
                        Util.DecimalToString(sumadorRetencion).PadLeft(16, '0') +
                        Util.DecimalToString(sumadorCalculo).PadLeft(16, '0');

                    file.WriteLine(linea);

                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Farcomed

        #region Estructura Farcomed Nuevo Formato
        public string[] EstructuraFacomedNuevoFormato(VPOSCONVENIO convenio, List<TPOSCOMPENSADETALLE> ltCompensados, string ruta, DateTime fautorizacion)
        {
            #region variables

            string[] respuesta = new string[2];
            string error = "OK";
            string archivoNombre = string.Empty;
            string archivo = string.Empty;
            string linea = string.Empty;
            int contador = 0;
            decimal sumadorTotal = 0;
            decimal sumadorIva = 0;
            decimal sumadorPagar = 0;
            decimal sumadorComision = 0;
            decimal sumadorRetencion = 0;
            decimal sumadorCalculo = 0;
            string campo_01 = string.Empty;
            string campo_02 = string.Empty;
            string campo_03 = string.Empty;
            string campo_04 = string.Empty;
            string campo_05 = string.Empty;
            string campo_06 = string.Empty;
            string campo_07 = string.Empty;
            string campo_08 = string.Empty;
            string campo_09 = string.Empty;
            string campo_10 = string.Empty;
            string campo_11 = string.Empty;
            string campo_12 = string.Empty;
            string campo_13 = string.Empty;
            string campo_14 = string.Empty;
            string campo_15 = string.Empty;
            string campo_16 = string.Empty;
            string campo_17 = string.Empty;
            string campo_18 = string.Empty;
            string campo_19 = string.Empty;
            string campo_20 = string.Empty;
            string campo_21 = string.Empty;
            string campo_22 = string.Empty;
            string campo_23 = string.Empty;

            #endregion variables

            try
            {
                if (ltCompensados.Count > 0 && error == "OK")
                {
                    archivoNombre = fautorizacion.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    archivo = string.Format(convenio.ARCHIVORETRONO, archivoNombre) + ".txt";
                    StreamWriter file = new StreamWriter(ruta + archivo);

                    //Cabecera
                    linea = String.Format("{0}{1}{2}{3}{4}",
                        "1",
                        fautorizacion.ToString("yyyyMMdd"),
                        "000000",
                        "01",
                        "0000000000000000000000000000000000000000000000000000COAC29DB");

                    file.WriteLine(linea);

                    //Detalles
                    foreach (TPOSCOMPENSADETALLE detalle in ltCompensados)
                    {
                        campo_01 = "2";

                        campo_02 = detalle.NUMEROTRANSACCION.PadLeft(10, '0');

                        campo_03 = ("0").PadLeft(64, '0');

                        campo_04 = (detalle.FTRANSACCION != null) ? detalle.FTRANSACCION.Value.ToString("yyyyMMdd").PadLeft(8, '0') : ("0").PadLeft(8, '0');

                        campo_05 = "1";

                        campo_06 = "00";

                        campo_07 =(detalle.VALORTRANSACCION != null)? Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0'): ("0").PadLeft(15, '0');

                        campo_08 = (detalle.VALORCOMISION != null) ? Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(15, '0') : ("0").PadLeft(15, '0');

                        campo_09 = (detalle.VALORLIQUIDADO != null)? Util.DecimalToString(detalle.VALORLIQUIDADO.Value).PadLeft(15, '0'): ("0").PadLeft(15, '0');

                        campo_10 = (detalle.VALORIVA != null) ? Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(15, '0') : ("0").PadLeft(15, '0');

                        campo_11 = (detalle.VALORRETENCIONFUENTE != null)? Util.DecimalToString(detalle.VALORRETENCIONFUENTE.Value).PadLeft(15, '0'): ("0").PadLeft(15, '0');

                        campo_12 = "00";

                        campo_13 = (detalle.VALORTRANSACCION != null)? Util.DecimalToString(detalle.VALORTRANSACCION.Value).PadLeft(15, '0'): ("0").PadLeft(15, '0'); 
                        
                        campo_14 = "00";

                        campo_15 = "00";

                        campo_16 = "01";

                        campo_17 = detalle.LOTE.Trim().PadLeft(10, '0');

                        campo_18 = ("0").PadLeft(15, '0');

                        campo_19 = detalle.NUMEROAPROBACION.Trim().PadLeft(10, '0');

                        campo_20 = Util.DecimalToString(detalle.VALORCOMISION.Value).PadLeft(16, '0');

                        campo_21 = Util.DecimalToString(detalle.VALORIVA.Value).PadLeft(16, '0');

                        campo_22 = ("0").PadLeft(20, '0');

                        campo_23 = Util.OfuscaTarjeta(detalle.TARJETA.Trim()).PadLeft(19, '0');

                        linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}", campo_01, campo_02, campo_03, campo_04, campo_05, campo_06, campo_07, campo_08, campo_09, campo_10, campo_11, campo_12, campo_13, campo_14, campo_15, campo_16, campo_17, campo_18, campo_19, campo_20, campo_21, campo_22, campo_23);

                        file.WriteLine(linea);

                        contador++;
                        sumadorTotal += detalle.VALORTRANSACCION.Value;
                        sumadorIva += detalle.VALORIVA.Value;
                        sumadorComision += detalle.VALORCOMISION.Value;
                        sumadorPagar += detalle.VALORLIQUIDADO.Value;
                        sumadorRetencion += detalle.VALORRETENCIONFUENTE.Value;
                        sumadorCalculo += (detalle.VALORLIQUIDADO.Value + detalle.VALORRETENCIONFUENTE.Value);
                    }
                    //Sumatoria de totales

                    linea = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        "3",
                        contador.ToString().PadLeft(6, '0'),
                        Util.DecimalToString(sumadorTotal).PadLeft(16, '0'),
                        Util.DecimalToString(sumadorIva).PadLeft(16, '0'),
                        Util.DecimalToString(sumadorPagar).PadLeft(16, '0'),
                        Util.DecimalToString(sumadorComision).PadLeft(16, '0'),
                        Util.DecimalToString(sumadorRetencion).PadLeft(16, '0'),
                        Util.DecimalToString(sumadorCalculo).PadLeft(16, '0'));
                        
                    file.WriteLine(linea);

                    file.Close();
                }
                else
                {
                    error = "NO EXISTEN REGISTROS PARA GENERAR ARCHIVOS";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                archivo = string.Empty;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }

            respuesta[0] = error;
            respuesta[1] = archivo;

            return respuesta;
        }
        #endregion Estructura Farcomed Nuevo Formato
    }
}

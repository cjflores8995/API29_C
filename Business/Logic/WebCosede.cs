using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WebCosede
    {
        public CanalRespuesta ConsultaBeneficiario(string identificacion, out TCOSPAGOS beneficiario)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            beneficiario = null;
            try
            {
                wsS29.uciMethods ws = new wsS29.uciMethods();
                wsS29.Iso8583 iso = new wsS29.Iso8583();

                iso.ISO_000_Message_Type = "1200";
                iso.ISO_002_PAN = identificacion;
                iso.ISO_003_ProcessingCode = "310000";
                iso.ISO_007_TransDatetime = DateTime.Now;
                iso.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                iso.ISO_012_LocalDatetime = DateTime.Now;
                iso.ISO_015_SettlementDatel = DateTime.Now;
                iso.ISO_018_MerchantType = "0007";
                iso.ISO_024_NetworkId = "555551";
                iso = ws.ProcessingTransactionISO_WEB(iso);

                respuesta.CError = iso.ISO_039_ResponseCode;
                respuesta.DError = Util.ReturnExceptionString(iso.ISO_039p_ResponseDetail);

                if (iso.ISO_039_ResponseCode == "000")
                {
                    string[] datosBeneficiario = iso.ISO_120_ExtendedData.Split('|');
                    beneficiario = new TCOSPAGOS();
                    beneficiario.INSTITUCION = datosBeneficiario[1];
                    beneficiario.TIPOIDENTIFICACION = datosBeneficiario[2];
                    beneficiario.IDENTIFICACION = datosBeneficiario[3];
                    beneficiario.NOMBRE = datosBeneficiario[4];
                    beneficiario.MONTO = Convert.ToDecimal(datosBeneficiario[5].Replace(",", "").Replace(".", ","));
                    beneficiario.DIRECCION = datosBeneficiario[6];
                    beneficiario.TELEFONO1 = datosBeneficiario[7];
                    beneficiario.CORREO = datosBeneficiario[8];
                    beneficiario.IDCOSEDE = datosBeneficiario[9];
                }
            }
            catch (Exception ex)
            {
                respuesta.CError = "999";
                respuesta.DError = "ERROR CONSULTANDO BENEFICIARIO: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return respuesta;
        }

        public CanalRespuesta PagoBeneficiario(TCOSPAGOS beneficiario, TSISTERMINAL terminal, TSISUSUARIO usuario)
        {
            CanalRespuesta respuesta = new CanalRespuesta();

            try
            {
                wsS29.uciMethods ws = new wsS29.uciMethods();
                wsS29.Iso8583 isoCosede = new wsS29.Iso8583();

                isoCosede.ISO_000_Message_Type = "1200";
                isoCosede.ISO_002_PAN = beneficiario.IDENTIFICACION;
                isoCosede.ISO_003_ProcessingCode = "010000";
                isoCosede.ISO_007_TransDatetime = DateTime.Now;
                isoCosede.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                isoCosede.ISO_012_LocalDatetime = DateTime.Now;
                isoCosede.ISO_015_SettlementDatel = DateTime.Now;
                isoCosede.ISO_018_MerchantType = "0007";
                isoCosede.ISO_024_NetworkId = "555551";

                isoCosede.ISO_120_ExtendedData =
                    beneficiario.IDENTIFICACION + "|" +
                    beneficiario.MONTO.Value.ToString("N2").Replace(',', '.') + "|" +
                    beneficiario.IDCOSEDE + "|" +
                    beneficiario.DIRECCION + "|" +
                    beneficiario.TELEFONO1 + "|" +
                    beneficiario.TELEFONO2 + "|" +
                    beneficiario.CORREO;

                var oficina = Web.ltOficinas.Where(x => x.COFICINA == terminal.COFICINA).LastOrDefault();
                isoCosede.ISO_121_ExtendedData =
                    oficina.CSUCURSAL.Value + oficina.COFICINA.Value.ToString().PadRight(2, '0') + "_" + oficina.OFICINA + "_" + oficina.CIUDAD + "|" +
                    isoCosede.ISO_011_SysAuditNumber + "|" +
                    oficina.CSUCURSAL.Value + oficina.COFICINA.Value.ToString().PadRight(2, '0') + "|" +
                    usuario.ALIAS + "/" + usuario.CUSUARIO + "|" +
                    "Ordenes de Pago";

                isoCosede = ws.ProcessingTransactionISO_WEB(isoCosede);

                respuesta.CError = isoCosede.ISO_039_ResponseCode;
                respuesta.DError = Util.ReturnExceptionString(isoCosede.ISO_039p_ResponseDetail);

                if (isoCosede.ISO_039_ResponseCode == "000")
                {
                    string[] datos = isoCosede.ISO_120_ExtendedData.Split('|');
                    beneficiario.FPAGOREAL = DateTime.Now;
                    beneficiario.CUSUARIOPAGO = usuario.CUSUARIO;
                    beneficiario.CSUCURSAL = terminal.CSUCURSAL;
                    beneficiario.COFICINA = terminal.COFICINA;
                    beneficiario.COMPROBANTE = isoCosede.ISO_011_SysAuditNumber;
                    beneficiario.CTERMINAL = terminal.CTERMINAL;
                    beneficiario.REFERENCIA = datos.ToArray()[6].ToString();
                    beneficiario.FORDENPAGO = Convert.ToDateTime(datos.ToArray()[14].ToString());
                    beneficiario.FPAGOCOSEDE = Convert.ToDateTime(datos.ToArray()[7].ToString());

                    if (!beneficiario.Insertar(beneficiario))
                    {
                        respuesta.CError = "999";
                        respuesta.DError = "ERROR GUARDANDO REGISTRO PAGO";
                        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ERROR GUARDANDO REGISTRO PAGO: " + beneficiario.COMPROBANTE + " TRAMA: " + isoCosede.ISO_120_ExtendedData, null, "ERR");
                    }

                    //if (respuesta.CError == "000")
                    //{
                    //    wsS29.Iso8583 isoFitbank = new wsS29.Iso8583();
                    //    isoFitbank.ISO_000_Message_Type = "1200";
                    //    isoFitbank.ISO_002_PAN = beneficiario.IDENTIFICACION;
                    //    isoFitbank.ISO_003_ProcessingCode = "520001";
                    //    isoFitbank.ISO_004_AmountTransaction = beneficiario.MONTO.Value;
                    //    isoFitbank.ISO_007_TransDatetime = DateTime.Now;
                    //    isoFitbank.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                    //    isoFitbank.ISO_012_LocalDatetime = DateTime.Now;
                    //    isoFitbank.ISO_015_SettlementDatel = DateTime.Now;
                    //    isoFitbank.ISO_018_MerchantType = "0007";
                    //    isoFitbank.ISO_024_NetworkId = "555551";
                    //    isoFitbank.ISO_034_PANExt = "77";
                    //    isoFitbank.ISO_041_CardAcceptorID = terminal.CSUCURSAL.ToString();
                    //    isoFitbank.ISO_042_Card_Acc_ID_Code = terminal.COFICINA.ToString();
                    //    isoFitbank.ISO_043_CardAcceptorLoc = usuario.CUSUARIO;
                    //    isoFitbank.ISO_049_TranCurrCode = 840;
                    //    isoFitbank.ISO_104_TranDescription = "PAGO COSEDE " + beneficiario.COMPROBANTE;

                    //    isoFitbank = ws.ProcessingTransactionISO_WEB(isoFitbank);

                    //    if (isoFitbank.ISO_039_ResponseCode != "000")
                    //    {
                    //        respuesta.CError = "001";
                    //        respuesta.DError = "ERROR REGISTRANDO MOVIMIENTO EN FITBANK " + isoFitbank.ISO_039p_ResponseDetail;
                    //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ERROR REGISTRANDO PAGO EN FITBANK: \n COMPROBANTE: " + beneficiario.COMPROBANTE + "\n ERROR: " + isoFitbank.ISO_039_ResponseCode + "-" + isoFitbank.ISO_039p_ResponseDetail, null, "ERR");
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                respuesta.CError = "999";
                respuesta.DError = "ERROR EN PAGO A BENEFICIARIO: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return respuesta;
        }

        public CanalRespuesta GeneraComprobante(string idcosede, string identificacion, out string ruta, out string archivo)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            TCOSPAGOS beneficiario = null;
            string error = string.Empty;
            string monto = string.Empty;
            archivo = string.Empty;
            ruta = string.Empty;

            try
            {
                beneficiario = new TCOSPAGOS().ListarPagoUnitario(idcosede, identificacion);
                if (beneficiario != null)
                {
                    archivo = beneficiario.IDENTIFICACION + "_" + beneficiario.FPAGOREAL.Value.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    ruta = ConfigurationManager.AppSettings["pathArchivos"].ToString() + ConfigurationManager.AppSettings["pathArchivosCosede"].ToString();
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    #region construyePDF

                    try { monto = beneficiario.MONTO.Value.ToString("F2"); }
                    catch { monto = Convert.ToDecimal("0").ToString("F2"); }

                    var doc = new iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(ruta + archivo, FileMode.Create));

                    writer.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfBoolean.PDFTRUE);
                    doc.Open();

                    iTextSharp.text.Font _FontTitle = new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                    iTextSharp.text.Font _FontText = new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    iTextSharp.text.Font _FontSub = new iTextSharp.text.Font(
                        iTextSharp.text.Font.FontFamily.COURIER, 8, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);

                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(ConfigurationManager.AppSettings["pathLogosCosede"].ToString());
                    imagen.BorderWidth = 0;
                    imagen.Alignment = Element.ALIGN_CENTER;
                    float percentage = 0.0f;
                    percentage = 400 / imagen.Width;
                    imagen.ScalePercent(percentage * 100);
                    doc.Add(imagen);

                    doc.Add(Chunk.NEWLINE);
                    doc.Add(Chunk.NEWLINE);

                    Paragraph titulo = new Paragraph(
                        new Phrase("CORPORACION DEL SEGURO DE DEPOSITOS, FONDO DE LIQUIDEZ Y FONDO DE SEGUROS PRIVADOS (COSEDE)", _FontTitle));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    titulo.Font.Size = 10;
                    doc.Add(titulo);

                    doc.Add(Chunk.NEWLINE);

                    Paragraph numeroRecibo = new Paragraph(
                        new Phrase("N° RECIBO DE PAGO: " + beneficiario.COMPROBANTE.PadLeft(10, '0'), _FontSub));
                    numeroRecibo.Alignment = Element.ALIGN_RIGHT;
                    numeroRecibo.Font.Size = 8;
                    doc.Add(numeroRecibo);

                    doc.Add(Chunk.NEWLINE);

                    PdfContentByte content = new PdfContentByte(writer);

                    PdfPTable table1 = new PdfPTable(2);
                    table1.TotalWidth = content.PdfDocument.PageSize.Width - 100;
                    table1.LockedWidth = true;
                    float[] table1Widths = new float[] { 50f, 300f };
                    table1.SetWidths(table1Widths);
                    table1.HorizontalAlignment = 0;
                    table1.SpacingBefore = 0f;
                    table1.SpacingAfter = 0f;

                    PdfPCell table1Cell = new PdfPCell();

                    table1Cell = new PdfPCell(new Phrase("RUC:", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("1768150270001", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("RAZON SOCIAL:", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("CORPORACION DEL SEGURO DE DEPOSITOS, FONDO DE LIQUIDEZ Y FONDO DE SEGURO PRIVADOS (COSEDE)", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("DIRECCION:", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("PLATAFORMA FINANCIERA GUBERNAMENTAL, EXTREMO NORTE, CALLES M. AYORA Y ALFONSO PEREIRA", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("TELEFONO:", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    table1Cell = new PdfPCell(new Phrase("593-2-396 0340", _FontText));
                    table1Cell.BorderWidth = 0;
                    table1Cell.BorderWidthBottom = 0;
                    table1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(table1Cell);

                    doc.Add(table1);

                    doc.Add(Chunk.NEWLINE);

                    Paragraph datBenef = new Paragraph(
                        new Phrase("DATOS DEL BENEFICIARIO", _FontSub));
                    datBenef.Alignment = Element.ALIGN_CENTER;
                    datBenef.Font.Size = 8;
                    doc.Add(datBenef);

                    doc.Add(Chunk.NEWLINE);

                    PdfPTable table2 = new PdfPTable(2);
                    table2.TotalWidth = content.PdfDocument.PageSize.Width;
                    table2.LockedWidth = true;
                    float[] table2Widths = new float[] { 100f, 250f };
                    table2.SetWidths(table2Widths);
                    table2.HorizontalAlignment = 0;
                    table2.SpacingBefore = 0f;
                    table2.SpacingAfter = 0f;

                    PdfPCell table2Cell = new PdfPCell();

                    table2Cell = new PdfPCell(new Phrase("FECHA PAGO:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.FPAGOCOSEDE.Value.ToString("dd/MM/yyyy"), _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("FECHA ORDEN PAGO:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.FORDENPAGO.Value.ToString("dd/MM/yyyy"), _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("RUC/CC/CI/ PASAPORTE CLIENTE:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.IDENTIFICACION, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("NOMBRE CLIENTE:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.NOMBRE, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("DIRECCION CLIENTE:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.DIRECCION, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("TELEFONO 1:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.TELEFONO1, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("TELEFONO 2:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.TELEFONO2, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("CORREO ELECTRONICO:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.CORREO, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("REFERENCIA:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.REFERENCIA, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    //----------------------
                    table2Cell = new PdfPCell(new Phrase("CAJERO:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase(beneficiario.CUSUARIONOMBRELEGAL, _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    //----------------------

                    //----------------------
                    table2Cell = new PdfPCell(new Phrase("SERVICIO:", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    table2Cell = new PdfPCell(new Phrase("PAGO DEL SEGURO", _FontText));
                    table2Cell.BorderWidth = 0;
                    table2Cell.BorderWidthBottom = 0;
                    table2Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table2.AddCell(table2Cell);

                    //----------------------


                    doc.Add(table2);

                    doc.Add(Chunk.NEWLINE);

                    PdfPTable table = new PdfPTable(2);
                    table.TotalWidth = content.PdfDocument.PageSize.Width - 100;
                    table.LockedWidth = true;
                    float[] widths = new float[] { 330, 50f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = 0;
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;

                    PdfPCell cell = new PdfPCell();

                    cell = new PdfPCell(new Phrase("DESCRIPCION DEL PAGO", _FontText));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0.75f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("VALOR", _FontText));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0.75f;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("VALOR QUE CANCELAMOS DE ACUERDO A LAS INSTRUCCIONES RECIBIDAS", _FontText));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(monto, _FontText));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("VALOR TOTAL EN DOLARES DE LOS ESTADOS UNIDOS DE AMERICA", _FontText));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(monto, _FontSub));
                    cell.BorderWidth = 0;
                    cell.BorderWidthBottom = 0;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    doc.Add(table);

                    doc.Add(Chunk.NEWLINE);

                    PdfPTable table3 = new PdfPTable(4);
                    table3.TotalWidth = content.PdfDocument.PageSize.Width;
                    table3.LockedWidth = true;
                    float[] table3Widths = new float[] { 60f, 80f, 60f, 150f };
                    table3.SetWidths(table3Widths);
                    table3.HorizontalAlignment = 0;
                    table3.SpacingBefore = 0f;
                    table3.SpacingAfter = 0f;

                    PdfPCell table3Cell = new PdfPCell();

                    table3Cell = new PdfPCell(new Phrase("FORMA DE PAGO:", _FontText));
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase("EFECTIVO", _FontText));
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase("NO. CAJA:", _FontText));
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase(beneficiario.CSUCURSAL.Value + beneficiario.COFICINA.Value.ToString().PadRight(2, '0'), _FontText));
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase("CAJERO:", _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase(beneficiario.CUSUARIOPAGO, _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    var oficina = Web.ltOficinas.Where(x => x.COFICINA == beneficiario.COFICINA).LastOrDefault();
                    table3Cell = new PdfPCell(new Phrase("AGENCIA:", _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase(oficina.CSUCURSAL.Value + oficina.COFICINA.Value.ToString().PadRight(2, '0') + "_" + oficina.OFICINA + "_" + oficina.CIUDAD, _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase("SERVICIO:", _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    table3Cell = new PdfPCell(new Phrase("PAGO DEL SEGURO", _FontText));
                    table3Cell.Colspan = 0;
                    table3Cell.BorderWidth = 0;
                    table3Cell.BorderWidthBottom = 0;
                    table3Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.AddCell(table3Cell);

                    doc.Add(table3);

                    doc.Add(Chunk.NEWLINE);

                    Paragraph firma = new Paragraph();

                    firma = new Paragraph(new Phrase(
                        "Con mi firma declaro que he recibido de la COSEDE, la suma de US$ " + monto + " " +
                        "por concepto del pago del seguro de los depósitos mantenidos en la " + beneficiario.INSTITUCION + " " +
                        "en Liquidación; en este sentido, renuncio expresamente a reclamar valor alguno por dicho " +
                        "concepto, por cuanto producto del presente pago, opera de pleno derecho la subrogación en los " +
                        "derechos de cobro a favor de la COSEDE de conformidad con lo establecido en el artículo 331 " +
                        "del Código Monetario y Financiero.\n\r" +
                        "Declaro bajo juramento que la informacion que he proporcionada es real y legítima y que " +
                        "corresponde con los documentos de identidad que he presentado para el cobro, autorizado a la " +
                        "COSEDE darle uso considere conveniente para la consecución de sus fines.\n \r" +
                        "Este comprobante de pago constituye título de crédito suficiente para ejecutar la subrogación " +
                        "de derechos a favor de la COSEDE por el valor recibido", _FontText));
                    firma.Alignment = Element.ALIGN_JUSTIFIED;
                    firma.Font.Size = 8;
                    doc.Add(firma);

                    doc.Add(Chunk.NEWLINE);
                    doc.Add(Chunk.NEWLINE);
                    doc.Add(Chunk.NEWLINE);

                    firma = new Paragraph(
                        new Phrase("__________________________________________", _FontText));
                    firma.Alignment = Element.ALIGN_LEFT;
                    firma.Font.Size = 8;
                    doc.Add(firma);
                    firma = new Paragraph(
                       new Phrase("FIRMA BENEFICIARIO", _FontText));
                    firma.Alignment = Element.ALIGN_LEFT;
                    firma.Font.Size = 8;
                    doc.Add(firma);

                    doc.Add(Chunk.NEWLINE);

                    firma = new Paragraph(
                        new Phrase("Número documento de identificación ______________________________", _FontText));
                    firma.Alignment = Element.ALIGN_LEFT;
                    firma.Font.Size = 8;
                    doc.Add(firma);

                    doc.Add(Chunk.NEWLINE);

                    firma = new Paragraph(new Phrase(
                        "NOTA: Para todos los efectos legales y reglamentos, y en especial para recuperar lo " +
                        "pagado mediante la jurisdicción coactiva de ser necesario, el presente documento es un " +
                        "título de crédito por contener una obligación pura, líquida y determinada de conformidad " +
                        "con lo dispuesto en los articulos 994 y 948 de Código de Procedemiento Civil en cordancia " +
                        "con lo dispuesto en la Disposición Transitoria Segundo del Codigo Orgánico General Prcesos." +
                        " \n\r" +
                        "Cualquier valor superior al máximo de la cobertura del seguro de depósito deberá ser " +
                        "requerido al Liquidador de " + beneficiario.INSTITUCION + "EN LIQUIDACION", _FontText));
                    firma.Alignment = Element.ALIGN_JUSTIFIED;
                    firma.Font.Size = 8;
                    doc.Add(firma);

                    doc.Close();
                    writer.Close();


                    #endregion construyePDF

                    respuesta.CError = "000";
                    respuesta.DError = "RECIBO GENERADO CORRECTAMENTE";
                }
                else
                {
                    respuesta.CError = "999";
                    respuesta.DError = "ERROR OBTENIENDO DATOS PARA RECIBO" + (error != "OK" ? (": " + error) : string.Empty);
                }
            }
            catch (Exception ex)
            {
                respuesta.CError = "999";
                respuesta.DError = "ERROR GENERANDO RECIBO: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return respuesta;
        }

        public List<TCOSPAGOS> ListarPagosUsuario(string identificacion)
        {
            return new TCOSPAGOS().ListarPagosUsuario(identificacion);
        }
    }
}

using Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class formularios_0024 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralControl lcControl = new LiteralControl();
        string sesionActiva = string.Empty;
        try
        {
            if (!IsPostBack)
            {
                try { sesionActiva = Session["sesionActiva"].ToString(); }
                catch { sesionActiva = "N"; }
                if (sesionActiva == "S")
                {
                    if (Request.Params["trx"] != null)
                    {
                        try
                        {
                            string trx = Util.DecryptKey(Request.Params["trx"]);
                            try
                            {
                                var transaccion = ((List<VSISUSUARIOMENU>)Session["sesionMenu"]).Where(x => x.CTRANSACCION == trx).FirstOrDefault();
                                lcControl.Text = transaccion.TRANSACCION;
                            }
                            catch
                            {
                                lcControl.Text = string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                        }
                        txtTitulo.Text = lcControl.Text;
                        phTitulo.Controls.Add(lcControl);
                        IniciaFormulario();
                    }
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("../ingreso.aspx", true);
                }
            }
            else
            {
                lcControl.Text = txtTitulo.Text;
                phTitulo.Controls.Add(lcControl);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void IniciaFormulario()
    {
        try
        {
            CargaTiposComprobantes();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargaTiposComprobantes()
    {
        ClientScriptManager cs = Page.ClientScript;
        string error = string.Empty;
        try
        {
            ddlTipoComprobante.Items.Clear();
            ddlTipoComprobante.DataBind();
            ddlTipoComprobante.Items.Insert(0, new ListItem("", ""));
            ddlTipoComprobante.Items.Insert(1, new ListItem("FACTURA", "01"));
            ddlTipoComprobante.Items.Insert(2, new ListItem("NOTA CREDITO", "04"));
            ddlTipoComprobante.Items.Insert(2, new ListItem("RETENCION", "07"));
        }
        catch (Exception ex)
        {
            LimpiaGrid();
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    private void CargarGrid()
    {
        ClientScriptManager cs = Page.ClientScript;
        LiteralControl lcControl = new LiteralControl();
        List<VFECOMPROBANTES> lista = new List<VFECOMPROBANTES>();

        try
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                lista = new WebFacturacion().ListarComprobantes(txtIdentificacion.Text, ddlTipoComprobante.SelectedValue, txtComprobante.Text, txtFechaDesde.Text, txtFechaHasta.Text);
                if (lista != null && lista.Count > 0)
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            item.linkPdf = "PDF;" + item.TIPODOCUMENTO + ";" + item.NUMERODOCUMENTO + ";" + item.FEMISION.Value.ToString("dd/MM/yyyy");
                            item.linkXml = "XML;" + item.TIPODOCUMENTO + ";" + item.NUMERODOCUMENTO + ";" + item.FEMISION.Value.ToString("dd/MM/yyyy");
                        }
                        gridView.DataSource = lista;
                        gridView.DataBind();
                    }
                    else
                    {
                        LimpiaGrid();
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PROCESOS PARA LOS CRITERIOS INGRESADOS", "IN"), true);
                    }
                }
                else
                {
                    LimpiaGrid();
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR CONSULTANDO PROCESOS", "ER"), true);
                }
            }
            else
            {
                LimpiaGrid();
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ES NECESARIO AL MENOS INGRESAR EL NUMERO DE IDENTIFICACION", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            LimpiaGrid();
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    private void LimpiaGrid()
    {
        gridView.DataSource = null;
        gridView.DataBind();
        panelTabla.Update();
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridView.PageIndex = e.NewPageIndex;
        CargarGrid();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        txtComprobante.Text = string.Empty;
        txtFechaDesde.Text = string.Empty;
        txtFechaHasta.Text = string.Empty;
        CargaTiposComprobantes();
        LimpiaGrid();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }

    public void DescargaComprobante(object sender, EventArgs e)
    {
        string tmp = string.Empty;
        string pathTmp = string.Empty;
        string pathArchivo = string.Empty;
        string archivo = string.Empty;
        DateTime fecha = DateTime.Now;
        string[] parametros = null;

        try
        {
            LinkButton btn = (LinkButton)sender;

            if (!string.IsNullOrEmpty(btn.CommandArgument))
            {
                parametros = btn.CommandArgument.Split(';');
                fecha = Convert.ToDateTime(parametros[3]);
                tmp = ConfigurationManager.AppSettings["pathTmp"];
                pathTmp = Server.MapPath(tmp);
                pathArchivo = string.Format(ConfigurationManager.AppSettings["pathArchivosFacturacion"], fecha.ToString("yyyy"), fecha.ToString("MM"), fecha.ToString("dd"));

                switch (parametros[0])
                {
                    case "PDF":
                        archivo = parametros[1] + "_" + parametros[2] + ".pdf";
                        if (File.Exists(pathArchivo + archivo))
                        {
                            File.Copy(pathArchivo + archivo, pathTmp + archivo, true);
                            Response.Clear();
                            Response.Write("<script>window.open('" + (tmp + archivo) + "','_blank')</script>");
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                        }
                        break;
                    case "XML":
                        archivo = parametros[1] + "_" + parametros[2] + ".xml";
                        if (File.Exists(pathArchivo + archivo))
                        {
                            string strFullPath = (@pathArchivo + archivo);

                            FileInfo file = new FileInfo(@pathArchivo + archivo);

                            string strContents = null;
                            System.IO.StreamReader objReader = default(System.IO.StreamReader);
                            objReader = new System.IO.StreamReader(strFullPath);
                            strContents = objReader.ReadToEnd();
                            objReader.Close();

                            string attachment = ("attachment; filename=" + file.Name);
                            Response.ClearContent();
                            Response.ContentType = "application/xml";
                            Response.AddHeader("content-disposition", attachment);
                            Response.Write(strContents);
                            Response.End();
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PARAMETROS PARA DESCARGA", "ER"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR GENERAL BUSCANDO DOCUMENTO: " + ex.Message.ToString().ToUpper(), "ER"), true);
        }


        //string rutaTemporal = ConfigurationManager.AppSettings["pathTmp"] + "comprobante_1801379167_20170525_20170525115721.pdf";

        //Response.Write("<script>window.open('" + rutaTemporal + "','_blank')</script>");



        //lnkDescargar.HRef = "http://localhost/canalelectronico/ingreso.aspx";

        //ClientScript.RegisterStartupScript(
        //    this.GetType(), "newWindow", "<script>AbrirLnk('lnkDescargar');</script>");


        //FileInfo file = new FileInfo(@"D:\temp\RETENCION001-013-000025081.xml");

        //Response.Clear();
        //Response.ContentType = "text/xml";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //Response.TransmitFile(file.FullName);
        //Response.End();


        //LinkButton btn = (LinkButton)sender;
        //using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        //{
        //    

        //    //string url = "http://localhost/canalelectronico/tmp/comprobante_1801379167_20170525_20170525115721.pdf";

        //    //ClientScript.RegisterStartupScript(
        //    //    this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));

        //    //<script type=text/javascript>window.close();</script>
        //}
    }

}
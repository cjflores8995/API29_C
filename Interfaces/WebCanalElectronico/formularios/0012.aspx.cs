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

public partial class formularios_0012 : System.Web.UI.Page
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
            txtFechaCorte.Enabled = true;
            ddlEstructura.Enabled = true;

            txtFechaCorte.Text = string.Empty;
            btnProcesar.Disabled = false;
            btnProcesar.Visible = true;
            lnkDescargar.Visible = false;

            ddlEstructura.Items.Clear();
            ddlEstructura.Items.Add("");
            ddlEstructura.Items.Add("I01");
            ddlEstructura.Items.Add("I02");
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        WebEstructuras est = new WebEstructuras();
        CanalRespuesta respuesta = new CanalRespuesta();
        string ruta, archivo, rutaArchivo, rutaZip, nombreZip, rutaTemporal, rutaArchivoTmp;
        ruta = archivo = rutaArchivo = rutaZip = nombreZip = rutaTemporal = rutaArchivoTmp = string.Empty;
        try
        {
            if (txtFechaCorte.Text != "")
            {
                if (ddlEstructura.SelectedItem.Text != "")
                {
                    ruta = ConfigurationManager.AppSettings["pathArchivos"].Trim() + string.Format(ConfigurationManager.AppSettings["pathArchivosEstructuras"].Trim(), ddlEstructura.SelectedItem.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);
                    respuesta = est.ConvierteEstructura(ddlEstructura.SelectedItem.Text, Convert.ToDateTime(txtFechaCorte.Text), ruta, archivo, out rutaZip, out nombreZip);
                    if (respuesta.CError == "000")
                    {
                        rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                        rutaArchivo = rutaZip;
                        rutaArchivoTmp = Server.MapPath(rutaTemporal) + nombreZip;
                        if (!Directory.Exists(Server.MapPath(rutaTemporal)))
                            Directory.CreateDirectory(Server.MapPath(rutaTemporal));
                        File.Copy(rutaArchivo, rutaArchivoTmp, true);
                        txtFechaCorte.Enabled = false;
                        ddlEstructura.Enabled = false;
                        btnProcesar.Disabled = true;
                        btnProcesar.Visible = false;
                        lnkDescargar.Visible = true;
                        lnkDescargar.HRef = rutaTemporal + nombreZip;
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ESTRUCTURA GENERADA CORRECTAMENTE", "OK"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", respuesta.DError, "ER"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "SELECCIONE EL TIPO DE ESTRUCTURA", "WR"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "INGRESE UNA FECHA CORTE VALIDA", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }
}
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

public partial class formularios_0003 : System.Web.UI.Page
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
            txtArchivo.Enabled = true;

            btnProcesar.Disabled = false;
            btnProcesar.Visible = true;
            lnkDescargar.Visible = false;

            txtFechaCorte.Text = string.Empty;

            ddlEstructura.Items.Clear();
            ddlEstructura.Items.Add("");
            ddlEstructura.Items.Add("C01");
            ddlEstructura.Items.Add("C02");
            ddlEstructura.Items.Add("C03");
            ddlEstructura.Items.Add("C04");
            ddlEstructura.Items.Add("F01");
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
                    if (txtArchivo.HasFile)
                    {
                        ruta = ConfigurationManager.AppSettings["pathArchivos"].Trim() + string.Format(ConfigurationManager.AppSettings["pathArchivosEstructuras"].Trim(), ddlEstructura.SelectedItem.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        archivo = txtArchivo.PostedFile.FileName;
                        rutaArchivo = ruta + archivo;
                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);
                        txtArchivo.PostedFile.SaveAs(rutaArchivo);
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
                            txtArchivo.Enabled = false;
                            btnProcesar.Disabled = true;
                            btnProcesar.Visible = false;
                            lnkDescargar.Visible = true;
                            lnkDescargar.HRef = rutaTemporal + nombreZip;
                            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "ESTRUCTURA GENERADA CORRECTAMENTE", "OK"));
                        }
                        else
                        {
                            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", respuesta.DError, "ER"));
                        }
                    }
                    else
                    {
                        cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "DEBE SELECCIONAR EL ARCHIVO A CARGAR", "WR"));
                    }
                }
                else
                {
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "DEBE SELECCIONAR EL TIPO DE ESTRUCTURA QUE DESEA PROCESAR", "WR"));
                }
            }
            else
            {
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "DEBE INGRESAR LA FECHA DE CORTE", "WR"));
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "WR"));
        }
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }
}
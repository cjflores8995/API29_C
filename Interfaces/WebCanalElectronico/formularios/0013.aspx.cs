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

public partial class formularios_0013 : System.Web.UI.Page
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
        LimpiarFormulario();
    }

    protected void LimpiarFormulario()
    {
        txtArchivo.Enabled = true;
        ddlTipo.Enabled = true;
        btnCargar.Disabled = false;
        CargaTipos();
    }

    private void CargaTipos()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebProcesos tab = new WebProcesos();
        string error = string.Empty;
        try
        {
            ddlTipo.Items.Clear();
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("", ""));
            ddlTipo.SelectedValue = "";

            ddlTipo.Items.Add(new ListItem("DATAFAST", "datafast"));
            ddlTipo.Items.Add(new ListItem("CORPORACION FAVORITA", "favorita"));
            ddlTipo.Items.Add(new ListItem("FYBECA", "farcomed"));
            ddlTipo.Items.Add(new ListItem("SANA SANA", "econofarm"));
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    protected bool ValidaCampos()
    {
        if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
            return false;
        if (!txtArchivo.HasFile)
            return false;
        return true;
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        string ruta = string.Empty;
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;
        try
        {
            if (ValidaCampos())
            {
                ruta = string.Format(ConfigurationManager.AppSettings["pathArchivosPos"].Trim(), ddlTipo.SelectedValue);
                archivo = txtArchivo.PostedFile.FileName;
                rutaArchivo = ruta + archivo;
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                txtArchivo.PostedFile.SaveAs(rutaArchivo);
                ddlTipo.Enabled = false;
                txtArchivo.Enabled = false;
                btnCargar.Disabled = true;
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "ARCHIVO CARGADO CORRECTAMENTE", "OK"));
            }
            else
            {
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "COMPLETE LOS CAMPOS OBLIGATORIOS", "WR"));
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
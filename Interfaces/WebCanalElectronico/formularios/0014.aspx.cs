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

public partial class formularios_0014 : System.Web.UI.Page
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
            ddlTipoProceso.Items.Clear();
            ddlArchivo.Items.Clear();
            ddlCorte.Items.Clear();
            CargaTipoProceso();
            ddlTipoProceso.Enabled = true;
            ddlArchivo.Enabled = false;
            txtCReparto.Enabled = false;
            txtDescripcion.Enabled = true;
            txtArchivo.Visible = true;
            txtArchivo.Enabled = true;
            lblArchivoCargado.Text = string.Empty;
            txtfcontable.Text = string.Empty;
            txtNumeroProceso.Text = string.Empty;
            txtCReparto.Text = string.Empty;
            txtReparto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ddlCorte.Enabled = false;
            btnProcesar.Disabled = false;
            btnProcesar.Visible = true;
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        TBTHPROCESO obj = new TBTHPROCESO();
        WebProcesos proc = new WebProcesos();
        CanalRespuesta response = new CanalRespuesta();
        string ruta = string.Empty;
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;

        try
        {
            if (ValidaCampos())
            {
                #region carga archivo

                ruta = ConfigurationManager.AppSettings["pathArchivos"].Trim() + string.Format(ConfigurationManager.AppSettings["pathArchivosProcesos"].Trim(), DateTime.Today.ToString("yyyyMMdd"));
                archivo = txtArchivo.PostedFile.FileName;
                rutaArchivo = ruta + archivo;
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                txtArchivo.PostedFile.SaveAs(rutaArchivo);

                #endregion carga archivo

                #region armaObjetoCabecera

                obj.FPROCESO = DateTime.Today;
                obj.CTIPOPROCESO = ddlTipoProceso.SelectedValue;
                obj.CESTADO = "CARGAD";
                obj.CUSUARIO = ((TSISUSUARIO)Session["sesionUsuario"]).CUSUARIO;
                obj.FCARGA = DateTime.Now;
                obj.DESCRIPCION = txtDescripcion.Text.ToUpper();
                obj.CTIPOARCHIVO = ddlArchivo.SelectedValue;
                if (obj.CTIPOARCHIVO == "0001")
                    obj.CORTE = Convert.ToInt32(ddlCorte.SelectedValue);
                obj.CREPARTO = txtCReparto.Text;
                obj.ARCHIVOORIGEN = archivo;
                response = proc.InsertaCabecera(ref obj);

                #endregion armaObjetoCabecera

                if (response.CError == "000")
                {
                    txtfcontable.Text = obj.FPROCESO.Value.ToShortDateString();
                    txtNumeroProceso.Text = obj.CPROCESO.Value.ToString();

                    ddlTipoProceso.Enabled = false;
                    ddlArchivo.Enabled = false;
                    ddlCorte.Enabled = false;
                    txtCReparto.Enabled = false;
                    txtDescripcion.Enabled = false;
                    txtArchivo.Visible = false;
                    lblArchivoCargado.Text = archivo;
                    txtArchivo.Enabled = false;
                    btnProcesar.Disabled = true;
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "PROCESO CARGADO CORRECTAMENTE", "OK"));
                }
                else
                {
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", response.DError, "ER"));
                }
            }
            else
            {
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "COMPLETE LOS CAMPOS OBLIGATORIOS", "WR"));
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }

    private void CargaTipoArchivo()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebProcesos tab = new WebProcesos();

        try
        {
            List<TBTHTIPOARCHIVO> ltObj = new List<TBTHTIPOARCHIVO>();
            ltObj = tab.CargaTipoArchivo();
            if (ltObj != null && ltObj.Count > 0)
            {
                ddlArchivo.Items.Clear();
                ddlArchivo.DataBind();
                ddlArchivo.Items.Insert(0, new ListItem("", ""));
                ddlArchivo.SelectedValue = "";

                foreach (TBTHTIPOARCHIVO obj in ltObj)
                {
                    if (obj.ACTIVO == "1" && obj.WEB == "1")
                        ddlArchivo.Items.Add(new ListItem(obj.DESCRIPCION, obj.CTIPOARCHIVO));
                }
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    private void CargaTipoProceso()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebProcesos tab = new WebProcesos();

        try
        {
            List<TBTHTIPOPROCESO> ltObj = new List<TBTHTIPOPROCESO>();
            ltObj = tab.CargaTipoProcesos();
            if (ltObj != null && ltObj.Count > 0)
            {
                ddlTipoProceso.Items.Clear();
                ddlTipoProceso.DataBind();
                ddlTipoProceso.Items.Insert(0, new ListItem("", ""));
                ddlTipoProceso.SelectedValue = "";

                foreach (TBTHTIPOPROCESO obj in ltObj)
                {
                    if (obj.ACTIVO == "1" && obj.WEB == "1")
                        ddlTipoProceso.Items.Add(new ListItem(obj.DESCRIPCION, obj.CTIPOPROCESO));
                }
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    private void CargaCortes()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebProcesos tab = new WebProcesos();
        string error = string.Empty;
        try
        {
            ddlCorte.Items.Clear();
            ddlCorte.DataBind();
            ddlCorte.Items.Insert(0, new ListItem("", ""));
            ddlCorte.SelectedValue = "";

            ddlCorte.Items.Add(new ListItem("PRIMER CORTE", "1"));
            ddlCorte.Items.Add(new ListItem("SEGUNDO CORTE", "2"));
            ddlCorte.Items.Add(new ListItem("TERCER CORTE", "3"));
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    protected bool ValidaCampos()
    {
        if (string.IsNullOrEmpty(ddlTipoProceso.SelectedValue))
            return false;

        if (string.IsNullOrEmpty(ddlArchivo.SelectedValue))
            return false;

        if (ddlTipoProceso.SelectedValue != "EFECHE")
        {
            if (ddlArchivo.SelectedValue == "0001")
                if (string.IsNullOrEmpty(ddlCorte.SelectedValue))
                    return false;

            if (string.IsNullOrEmpty(txtCReparto.Text))
                return false;
        }

        if (string.IsNullOrEmpty(txtDescripcion.Text))
            return false;

        if (!txtArchivo.HasFile)
            return false;

        return true;
    }

    protected void ddlTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaTipoArchivo();
        if (ddlTipoProceso.SelectedValue == "EFECHE")
        {
            ddlArchivo.SelectedValue = "0004";
            ddlArchivo.Enabled = false;
        }
        else
        {
            txtCReparto.Enabled = true;
            ddlArchivo.Enabled = true;
        }
    }

    protected void ddlArchivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlArchivo.SelectedValue == "0001")
        {
            ddlCorte.Enabled = true;
            CargaCortes();
        }
        else
        {
            ddlCorte.ClearSelection();
            ddlCorte.Enabled = false;
        }
    }

    protected void txtCReparto_Disposed(object sender, EventArgs e)
    {
        txtReparto.Text = TraeDescripcionReparto(txtCReparto.Text);
    }

    protected void txtCReparto_TextChanged(object sender, EventArgs e)
    {
        txtReparto.Text = TraeDescripcionReparto(txtCReparto.Text);
    }

    private string TraeDescripcionReparto(string reparto)
    {
        TBTHREPARTO obj = null;
        string error = string.Empty;
        string descripcion = string.Empty;
        try
        {
            obj = new WebProcesos().CargaRepartos(reparto);
            if (obj != null)
            {
                descripcion = obj.DESCRIPCION;
            }
            else
            {
                descripcion = "NO EXISTE CONVENIO";
            }
        }
        catch
        {
            descripcion = "ERROR AL BUSCAR REPARTO";
        }
        return descripcion;
    }


}
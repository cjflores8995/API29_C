using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0025 : System.Web.UI.Page
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
            CargarGrid();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtNumeroProceso.Text = "";
        txtUsuario.Text = "";
        LimpiaGrid();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }

    protected void gvProcesos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcesos.PageIndex = e.NewPageIndex;
        CargarGrid();
    }

    private void CargarGrid()
    {
        ClientScriptManager cs = Page.ClientScript;
        LiteralControl lcControl = new LiteralControl();
        WebProcesos tab = new WebProcesos();
        List<VBTHPROCESO> ltObj = null;
        string error = string.Empty;
        Int32? cproceso = null;
        string cusuario = string.Empty;

        try
        {
            if (string.IsNullOrEmpty(txtNumeroProceso.Text))
            {
                cproceso = null;
            }
            else
            {
                cproceso = Convert.ToInt32(txtNumeroProceso.Text.Trim());
            }

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                cusuario = string.Empty;
            }
            else
            {
                cusuario = txtUsuario.Text.Trim();
            }

            ltObj = tab.ConsultarPendientesAutorizar(cproceso, cusuario);

            if (ltObj != null && ltObj.Count > 0)
            {
                foreach (var item in ltObj)
                {
                    item.link1 = "0026.aspx?trx=" + Util.EncryptKey("0026") + "&values=" + Util.EncryptKey(item.FPROCESO + "|" + item.CPROCESO);
                }
                gvProcesos.DataSource = ltObj;
                gvProcesos.DataBind();
            }
            else
            {
                LimpiaGrid();
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PROCESOS PARA LOS CRITERIOS INGRESADOS", "IN"), true);
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
        gvProcesos.DataSource = null;
        gvProcesos.DataBind();
        panelTabla.Update();
    }
}
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0007 : System.Web.UI.Page
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
        txtFechaDesde.Text = "";
        txtFechaHasta.Text = "";
        txtNumeroProceso.Text = "";
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
        WebProcesos nbatch = new WebProcesos();
        List<VBTHPROCESORESUMEN> lista = new List<VBTHPROCESORESUMEN>();
        string error;
        error = string.Empty;
        try
        {
            if (txtFechaDesde.Text == "" && txtFechaHasta.Text == "" && txtNumeroProceso.Text == "")
                lista = nbatch.BuscaProcesosResumen(DateTime.Today, DateTime.Today, null);
            else if (txtFechaDesde.Text != "" && txtFechaHasta.Text != "" && txtNumeroProceso.Text == "")
                lista = nbatch.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), null);
            else if (txtFechaDesde.Text == "" && txtFechaHasta.Text == "" && txtNumeroProceso.Text != "")
                lista = nbatch.BuscaProcesosResumen(null, null, Convert.ToInt32(txtNumeroProceso.Text));
            else if (txtFechaDesde.Text != "" && txtFechaHasta.Text != "" && txtNumeroProceso.Text != "")
                lista = nbatch.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), Convert.ToInt32(txtNumeroProceso.Text));

            if (error == "OK")
            {
                if (lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        item.Link = "0008.aspx?trx=" + Util.EncryptKey("010008") + "&values=" + Util.EncryptKey(item.FPROCESO.Value.ToString("dd/MM/yyyy") + "|" + item.CPROCESO);
                    }
                    gvProcesos.DataSource = lista;
                    gvProcesos.DataBind();
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
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR CONSULTANDO PROCESOS: " + error, "ER"), true);
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
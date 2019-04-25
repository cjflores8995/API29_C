using Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0011 : System.Web.UI.Page
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
            CargaConvenios();
            CargarGrid();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }
    private void CargaConvenios()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebPos pos = new WebPos();

        try
        {
            List<VPOSCONVENIO> ltConvenios = null;
            ltConvenios = pos.CargaConvenios();
            if (ltConvenios != null && ltConvenios.Count > 0)
            {
                ddlConvenio.Items.Clear();
                ddlConvenio.DataBind();
                ddlConvenio.Items.Insert(0, new ListItem("", ""));
                ddlConvenio.SelectedValue = "";

                foreach (VPOSCONVENIO convenio in ltConvenios)
                {
                    ListItem ltItem = new ListItem(convenio.NOMBRE, convenio.CCONVENIO.Value.ToString());
                    ddlConvenio.Items.Add(ltItem);
                }
            }
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
        WebPos npos = new WebPos();
        List<VPOSCOMPENSACABECERA> lista = null;

        try
        {
            if (ddlConvenio.SelectedValue != "")
            {
                lista = npos.BuscaProcesosResumen(null, null, Convert.ToInt32(ddlConvenio.SelectedValue), "PRO");

                if (lista != null && lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        item.link = "000010.aspx?trx=" + Util.EncryptKey("000010") + "&values=" + Util.EncryptKey(item.FPROCESO + "|" + item.CCONVENIO);
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
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ES NECESARIO SELECCIONAR UN CONVENIO", "IN"), true);
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
    protected void gvProcesos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcesos.PageIndex = e.NewPageIndex;
        CargarGrid();
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        CargaConvenios();
        LimpiaGrid();
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }
    protected void btnAutorizar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        WebPos pos = new WebPos();

        try
        {
            TSISUSUARIO objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
            foreach (GridViewRow row in gvProcesos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkAutorizar = (row.Cells[0].FindControl("chkAutorizar") as CheckBox);
                    if (chkAutorizar.Checked)
                    {
                        pos.AutorizarCompensacion(Convert.ToDateTime(row.Cells[0].Text), Convert.ToInt32(row.Cells[1].Text), objUsuario.CUSUARIO);
                    }
                }
            }
            LimpiaGrid();
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "PROCESO FINALIZADO CORRECTAMENTE", "OK"), true);
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
}
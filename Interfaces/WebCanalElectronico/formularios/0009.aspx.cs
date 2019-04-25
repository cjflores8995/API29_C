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

public partial class formularios_0009 : System.Web.UI.Page
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
            CargaEstado();
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
            List<VPOSCONVENIO> ltConvenios = new List<VPOSCONVENIO>();
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

    private void CargaEstado()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebPos pos = new WebPos();

        try
        {
            List<TPOSESTADO> ltObj = new List<TPOSESTADO>();
            ltObj = pos.CargaEstado();
            if (ltObj != null && ltObj.Count > 0)
            {
                ddlEstado.Items.Clear();
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem("", ""));
                ddlEstado.SelectedValue = "";

                foreach (TPOSESTADO obj in ltObj)
                {
                    if (obj.CESTADO != "CMP" && obj.CESTADO != "RCH")
                    {
                        ListItem ltItem = new ListItem(obj.DESCRIPCION, obj.CESTADO);
                        ddlEstado.Items.Add(ltItem);
                    }
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
        List<VPOSCOMPENSACABECERA> lista = new List<VPOSCOMPENSACABECERA>();
        string error;
        error = string.Empty;
        try
        {
            if (string.IsNullOrEmpty(txtFechaDesde.Text) && string.IsNullOrEmpty(txtFechaHasta.Text) && string.IsNullOrEmpty(ddlConvenio.SelectedValue) && string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(DateTime.Now, DateTime.Now, null, null);
            else if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text) && !string.IsNullOrEmpty(ddlConvenio.SelectedValue) && !string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), Convert.ToInt32(ddlConvenio.SelectedValue), ddlEstado.SelectedValue);
            else if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text) && string.IsNullOrEmpty(ddlConvenio.SelectedValue) && string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), null, null);
            else if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text) && !string.IsNullOrEmpty(ddlConvenio.SelectedValue) && string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), Convert.ToInt32(ddlConvenio.SelectedValue), null);
            else if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text) && string.IsNullOrEmpty(ddlConvenio.SelectedValue) && !string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), Convert.ToInt32(ddlConvenio.SelectedValue), null);
            else if (string.IsNullOrEmpty(txtFechaDesde.Text) && string.IsNullOrEmpty(txtFechaHasta.Text) && string.IsNullOrEmpty(ddlConvenio.SelectedValue) && !string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(null, null, null, ddlEstado.SelectedValue);
            else if (string.IsNullOrEmpty(txtFechaDesde.Text) && string.IsNullOrEmpty(txtFechaHasta.Text) && !string.IsNullOrEmpty(ddlConvenio.SelectedValue) && string.IsNullOrEmpty(ddlEstado.SelectedValue))
                lista = npos.BuscaProcesosResumen(null, null, Convert.ToInt32(ddlConvenio.SelectedValue), null);


            if (lista != null && lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    item.link = "0010.aspx?trx=" + Util.EncryptKey("020010") + "&values=" + Util.EncryptKey(item.FPROCESO + "|" + item.CCONVENIO);
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
        txtFechaDesde.Text = string.Empty;
        txtFechaHasta.Text = string.Empty;
        CargaConvenios();
        CargaEstado();
        LimpiaGrid();
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }
    protected void btnReporte_Click(object sender, EventArgs e)
    {
        CanalRespuesta CanalRespuesta = new CanalRespuesta();
        WebPos pos = new WebPos();
        ClientScriptManager cs = Page.ClientScript;
        string ruta, archivo, rutaArchivo, rutaArchivoTmp, rutaTemporal;
        ruta = archivo = rutaArchivo = rutaArchivoTmp = rutaTemporal = string.Empty;
        try
        {
            if (txtFechaDesde.Text != "" && txtFechaHasta.Text != "")
            {
                CanalRespuesta = pos.GeneraReporteResumen(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text), Convert.ToInt32(ddlConvenio.SelectedValue), ddlEstado.SelectedValue, out ruta, out archivo);
                if (CanalRespuesta.CError == "000")
                {
                    rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                    rutaArchivo = ruta + archivo;
                    rutaArchivoTmp = Server.MapPath(rutaTemporal) + archivo;
                    File.Copy(rutaArchivo, rutaArchivoTmp, true);
                    btnReporte.Disabled = true;
                    btnReporte.Visible = false;
                    lnkReporte.Visible = true;
                    lnkReporte.HRef = rutaTemporal + archivo;
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "REPORTE GENERADO CORRECTAMENTE", "OK"), true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", CanalRespuesta.DError, "IN"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "AL MENOS DEBE SELECCIONAR UN RANGO DE FECHAS PARA GENERAR EL REPORTE", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
}
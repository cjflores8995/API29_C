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

public partial class formularios_0016 : System.Web.UI.Page
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
        WebProcesos tab = new WebProcesos();
        VBTHPROCESO proceso = null;
        string error = string.Empty;
        Int32 registros = 0;
        Decimal total = 0;

        try
        {
            lnkReporte.Visible = false;
            if (Request.Params["values"] != null)
            {
                string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
                proceso = tab.ConsultaProceso(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
                if (proceso != null)
                {
                    try { txtFechaProceso.Text = proceso.FPROCESO.Value.ToString("dd/MM/yyyy"); }
                    catch { txtFechaProceso.Text = string.Empty; }
                    try { txtNumeroProceso.Text = proceso.CPROCESO.Value.ToString(); }
                    catch { txtNumeroProceso.Text = string.Empty; }
                    txtTipoProceso.Text = proceso.CTIPOPROCESO + " - " + proceso.TIPOPROCESO;
                    txtEstado.Text = proceso.CESTADO + " - " + proceso.ESTADO;
                    switch (proceso.CORTE)
                    {
                        case 1:
                            txtCorte.Text = "PRIMER CORTE";
                            break;
                        case 2:
                            txtCorte.Text = "SEGUNDO CORTE";
                            break;
                        case 3:
                            txtCorte.Text = "TERCER CORTE";
                            break;
                        default:
                            txtCorte.Text = string.Empty;
                            break;
                    }
                    txtDescripcion.Text = proceso.DESCRIPCION;
                    txtFormatoArchivo.Text = proceso.CTIPOARCHIVO + " - " + proceso.TIPOARCHIVO;
                    txtReparto.Text = proceso.CREPARTO + " - " + proceso.REPARTO;
                    txtUsuario.Text = proceso.CUSUARIO + " - " + proceso.USUARIO;
                    try { txtFCarga.Text = proceso.FCARGA.Value.ToString("dd/MM/yyyy HH:mm:ss"); }
                    catch { txtFCarga.Text = string.Empty; }
                    txtLote.Text = proceso.DATOSLOTE;
                    txtError.Text = proceso.ERROR;

                    if (tab.ConsultaTotalesDetalleTabulado(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()), out registros, out total)) ;
                    {
                        try { txtRegistros.Text = registros.ToString(); }
                        catch { txtRegistros.Text = string.Empty; }

                        try { txtTotal.Text = total.ToString("N2"); }
                        catch { txtRegistros.Text = string.Empty; }
                    }

                    try { txtRegistrosProcesar.Text = tab.TotalRegistrosDetalleProceso(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString())).ToString(); }
                    catch { txtRegistrosProcesar.Text = string.Empty; }

                    if (!string.IsNullOrEmpty(proceso.ARCHIVORESPUESTA))
                    {
                        txtSpi3.Text = proceso.ARCHIVORESPUESTA;
                        btnSpi3.Visible = true;
                        btnSpi3.Disabled = false;
                    }
                    CargarGridTabulado();
                    CargarGridErrores();
                }
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("0015.aspx?trx=" + Util.EncryptKey("0015"), false);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        //CanalRespuesta CanalRespuesta = new CanalRespuesta();
        //WebProcesos tab = new WebProcesos();
        //ClientScriptManager cs = Page.ClientScript;
        //string ruta, archivo, rutaArchivo, rutaArchivoTmp, rutaTemporal;
        //ruta = archivo = rutaArchivo = rutaArchivoTmp = rutaTemporal = string.Empty;
        //try
        //{
        //    string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
        //    CanalRespuesta = tab.GeneraReporteDetalle(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()), out ruta, out archivo);
        //    if (CanalRespuesta.CError == "000")
        //    {
        //        rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
        //        rutaArchivo = ruta + archivo;
        //        rutaArchivoTmp = Server.MapPath(rutaTemporal) + archivo;
        //        File.Copy(rutaArchivo, rutaArchivoTmp, true);
        //        lnkReporte.Visible = true;
        //        lnkReporte.HRef = rutaTemporal + archivo;
        //        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "REPORTE GENERADO CORRECTAMENTE", "OK"), true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", CanalRespuesta.DError, "IN"), true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        //    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        //}
    }

    protected void gridTabulado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridTabulado.PageIndex = e.NewPageIndex;
        CargarGridTabulado();
    }

    protected void gridErrores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridErrores.PageIndex = e.NewPageIndex;
        CargarGridErrores();
    }

    private void CargarGridTabulado()
    {
        WebProcesos tab = new WebProcesos();
        List<VBTHTABULADORESUMEN> ltDetalle = null;

        try
        {
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            ltDetalle = tab.ResumenTabulado(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
            if (ltDetalle != null && ltDetalle.Count > 0)
            {
                gridTabulado.DataSource = ltDetalle;
                gridTabulado.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargarGridErrores()
    {
        WebProcesos tab = new WebProcesos();
        List<VBTHPROCESOERRORES> ltDetalle = null;

        try
        {
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            ltDetalle = tab.ResumenErrores(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
            if (ltDetalle != null && ltDetalle.Count > 0)
            {
                gridErrores.DataSource = ltDetalle;
                gridErrores.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnSpi3_Click(object sender, EventArgs e)
    {
        string rutaTemporal = string.Empty;
        string rutaArchivo = string.Empty;
        string rutaArchivoTmp = string.Empty;
        try
        {
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
            rutaArchivo = ConfigurationManager.AppSettings["pathArchivos"].Trim() + string.Format(ConfigurationManager.AppSettings["pathArchivosProcesos"].Trim(), Convert.ToDateTime(values[0].ToString()).ToString("yyyyMMdd")) + txtSpi3.Text;
            if (!Directory.Exists(Server.MapPath(rutaTemporal)))
                Directory.CreateDirectory(Server.MapPath(rutaTemporal));
            rutaArchivoTmp = Server.MapPath(rutaTemporal) + txtSpi3.Text;
            File.Copy(rutaArchivo, rutaArchivoTmp, true);
            btnSpi3.Disabled = true;
            btnSpi3.Visible = false;
            lnkReporte.Visible = true;
            lnkReporte.HRef = rutaTemporal + txtSpi3.Text;
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "REPORTE GENERADO CORRECTAMENTE", "OK"), true);
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
}
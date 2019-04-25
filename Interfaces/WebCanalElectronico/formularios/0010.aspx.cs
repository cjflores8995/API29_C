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

public partial class formularios_0010 : System.Web.UI.Page
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
        WebPos pos = new WebPos();
        VPOSCOMPENSACABECERA objCabecera = null;
        string error = string.Empty;
        try
        {
            if (Request.Params["values"] != null)
            {
                string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
                objCabecera = pos.TraeCabeceraResumen(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
                if (objCabecera != null)
                {
                    try { txtfcontable.Text = objCabecera.FPROCESO.Value.ToString("dd/MM/yyyy"); }
                    catch { txtfcontable.Text = "--"; }

                    try
                    {
                        txtCConvenio.Text = objCabecera.CCONVENIO.Value.ToString();
                        txtConvenio.Text = pos.CargaConvenios().Where(x => x.CCONVENIO == objCabecera.CCONVENIO).First().NOMBRE;
                    }
                    catch
                    {
                        txtCConvenio.Text = string.Empty;
                        txtConvenio.Text = string.Empty;
                    }

                    try
                    {
                        txtCEstado.Text = objCabecera.CESTADO;
                        txtEstado.Text = pos.CargaEstado().Where(x => x.CESTADO == objCabecera.CESTADO).First().DESCRIPCION;
                    }
                    catch
                    {
                        txtCEstado.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                    }

                    try { txtCuenta.Text = pos.CargaConvenios().Where(x => x.CCONVENIO == objCabecera.CCONVENIO).First().CUENTADEBITO; }
                    catch { txtCuenta.Text = "--"; }

                    txtDebito.Text = objCabecera.COMISION;
                    txtTransferencia.Text = objCabecera.TRANSFERENCIA;
                    txtError.Text = objCabecera.ERROR;
                    try { txtCompensados.Text = objCabecera.COMPENSADOS.Value.ToString(); }
                    catch { txtCompensados.Text = (0).ToString(); }
                    try { txtRechazados.Text = objCabecera.RECHAZADOS.Value.ToString(); }
                    catch { txtRechazados.Text = (0).ToString(); }
                    try { txtTransacciones.Text = objCabecera.TOTALTRANSACCION.Value.ToString("F2"); }
                    catch { txtTransacciones.Text = (0).ToString("F2"); }
                    try { txtLiquidado.Text = objCabecera.TOTALLIQUIDADO.Value.ToString("F2"); }
                    catch { txtLiquidado.Text = (0).ToString("F2"); }
                    try { txtComision.Text = objCabecera.TOTALCOMISION.Value.ToString("F2"); }
                    catch { txtComision.Text = (0).ToString("F2"); }

                    if (objCabecera.CESTADO != "PEN")
                        btnCancelarArchivo.Disabled = true;

                    CargarGridDetalle();
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
        Response.Redirect("0009.aspx?trx=" + Util.EncryptKey("0009"), false);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
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
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            CanalRespuesta = pos.GeneraReporteDetalle(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()), out ruta, out archivo);
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
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetalle.PageIndex = e.NewPageIndex;
        CargarGridDetalle();
    }

    private void CargarGridDetalle()
    {
        List<TPOSCOMPENSADETALLE> ltDetalle = new List<TPOSCOMPENSADETALLE>();
        List<TPOSESTADO> ltEstado = new List<TPOSESTADO>();
        WebPos pos = new WebPos();
        string error;
        error = string.Empty;
        try
        {
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            ltDetalle = pos.TraeDetalleProceso(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
            if (ltDetalle != null && ltDetalle.Count > 0 && error == "OK")
            {
                ltEstado = pos.CargaEstado();
                foreach (TPOSCOMPENSADETALLE obj in ltDetalle)
                {
                    obj.Estado = ltEstado.Where(x => x.CESTADO == obj.CESTADO).First().DESCRIPCION;
                }
                gvDetalle.DataSource = ltDetalle;
                gvDetalle.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnCancelarArchivo_ServerClick(object sender, EventArgs e)
    {
        CanalRespuesta CanalRespuesta = new CanalRespuesta();
        WebPos pos = new WebPos();
        ClientScriptManager cs = Page.ClientScript;
        try
        {
            CanalRespuesta = pos.CancelarArchivo(Convert.ToDateTime(txtfcontable.Text), Convert.ToInt32(txtCConvenio.Text));
            if (CanalRespuesta.CError == "000")
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "REPORTE GENERADO CORRECTAMENTE", "OK"), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", CanalRespuesta.DError, "IN"), true);
            }
            IniciaFormulario();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
}
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

public partial class formularios_0008 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 900;
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
        //List<VBTHPROCESOERRORES> ltErrores = new List<VBTHPROCESOERRORES>();
        WebProcesos nbatch = new WebProcesos();
        VBTHPROCESORESUMEN obj = null;
        string error = string.Empty;
        try
        {
            if (Request.Params["values"] != null)
            {
                string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
                obj = nbatch.BuscaProcesoResumen(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
                if (obj != null)
                {
                    txtfcontable.Text = obj.FPROCESO.Value.ToString("dd/MM/yyyy");
                    txtNumeroProceso.Text = obj.CPROCESO.ToString();
                    txtDescripcion.Text = obj.DESCRIPCION;
                    txtEstado.Text = obj.ESTADO;
                    txtTotal.Text = obj.TOTAL.ToString();
                    txtPendientes.Text = obj.PENDIENTES.ToString();
                    txtCorrectos.Text = obj.CORRECTOS.ToString();
                    txtErrores.Text = obj.ERRORES.ToString();
                    txtTotalCreditos.Text = obj.TOTALCREDITOS.Value.ToString("N2");
                    txtTotalDebitos.Text = obj.TOTALDEBITOS.Value.ToString("N2");
                    CargarGridErrores();
                }
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargarGridErrores()
    {
        List<VBTHPROCESOERRORES> ltErrores = null;
        WebProcesos nbatch = new WebProcesos();
        string error;
        error = string.Empty;
        try
        {
            ltErrores = nbatch.ResumenErrores(Convert.ToDateTime(txtfcontable.Text), Convert.ToInt32(txtNumeroProceso.Text));
            if (error == "OK")
            {
                if (ltErrores.Count > 0)
                {
                    gvErrores.DataSource = ltErrores;
                    gvErrores.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void gvErrores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvErrores.PageIndex = e.NewPageIndex;
        CargarGridErrores();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("0007.aspx?trx=" + Util.EncryptKey("0007"), false);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        //CanalRespuesta CanalRespuesta = new CanalRespuesta();
        //WebProcesos nbatch = new WebProcesos();
        //ClientScriptManager cs = Page.ClientScript;
        //string ruta, archivo, rutaArchivo, rutaArchivoTmp, rutaTemporal;
        //ruta = archivo = rutaArchivo = rutaArchivoTmp = rutaTemporal = string.Empty;
        //try
        //{
        //    CanalRespuesta = nbatch.GeneraReporteCuadre(Convert.ToDateTime(txtfcontable.Text), Convert.ToInt32(txtNumeroProceso.Text), out ruta, out archivo);
        //    if (CanalRespuesta.CError == "000")
        //    {
        //        rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
        //        rutaArchivo = ruta + archivo;
        //        rutaArchivoTmp = Server.MapPath(rutaTemporal) + archivo;
        //        File.Copy(rutaArchivo, rutaArchivoTmp, true);
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
}
using Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0029 : System.Web.UI.Page
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
            txtFechaInicio.Enabled = true;
            txtFechaInicio.Text = string.Empty;

            txtFechaFin.Enabled = true;
            txtFechaFin.Text = string.Empty;

            btnProcesar.Disabled = false;
            btnProcesar.Visible = true;
            lnkDescargar.Visible = false;
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        #region variables
        ClientScriptManager cs = Page.ClientScript;
        WebEstructuras est = new WebEstructuras();
        CanalRespuesta respuesta = new CanalRespuesta();
        string path = string.Empty;
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;
        string nombreZip = string.Empty;
        string rutaTemporal = string.Empty;
        string rutaArchivoTmp = string.Empty;
        string tiempo = string.Empty;
        ThreadLocal<Stopwatch> duracion;
        TimeSpan totalDuracion;
        #endregion variables

        try
        {
            if (txtFechaInicio.Text != "" && txtFechaFin.Text != "")
            {
                //ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "HOLA", "WR"), true);

                duracion = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                duracion.Value.Reset();
                duracion.Value.Start();

                path = ConfigurationManager.AppSettings["pathConciliacion"].Trim().ToString();// + string.Format(ConfigurationManager.AppSettings["pathArchivosEstructuras"].Trim(), "UAF", DateTime.Now.ToString("yyyyMMddHHmmss"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //Thread.Sleep(5000);
                //respuesta = est.GeneraConciliacion(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), path, out nombreZip);

                duracion.Value.Stop();
                totalDuracion = duracion.Value.Elapsed;
                tiempo = totalDuracion.ToString(@"hh\:mm\:ss");

                if (respuesta.CError == "000")
                {
                    /*rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                    rutaArchivo = path + nombreZip;
                    rutaArchivoTmp = Server.MapPath(rutaTemporal) + nombreZip;
                    if (!Directory.Exists(Server.MapPath(rutaTemporal)))
                        Directory.CreateDirectory(Server.MapPath(rutaTemporal));
                    File.Copy(rutaArchivo, rutaArchivoTmp, true);
                    txtFechaInicio.Enabled = false;
                    btnProcesar.Disabled = true;
                    btnProcesar.Visible = false;
                    lnkDescargar.Visible = true;
                    lnkDescargar.HRef = rutaTemporal + nombreZip;
                    */
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", respuesta.DError+"\\nTIEMPO: " + tiempo, "OK"), true);
                
            }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", respuesta.DError + "\\nTIEMPO: " + tiempo, "ER"), true);
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "INGRESE UNA FECHA CORTE VALIDA", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString("AQUI: "+ex), "ER"), true);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }
}
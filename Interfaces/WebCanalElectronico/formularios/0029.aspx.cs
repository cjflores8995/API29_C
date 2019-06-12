using Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0029 : System.Web.UI.Page
{
    LoadPropertiesConfig p = new LoadPropertiesConfig();

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
                duracion = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                duracion.Value.Reset();
                duracion.Value.Start();

                path = ConfigurationManager.AppSettings["pathConciliacion"].Trim().ToString();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                DateTime FECHA_INICIO_VAR = Convert.ToDateTime(txtFechaInicio.Text.ToString());
                DateTime FECHA_FIN_VAR = Convert.ToDateTime(txtFechaFin.Text.ToString());
                
                respuesta = new WebEstructurasConciliacion().GeneraEstructuraConciliacion(FECHA_INICIO_VAR, FECHA_FIN_VAR);

                duracion.Value.Stop();
                totalDuracion = duracion.Value.Elapsed;
                tiempo = totalDuracion.ToString(@"hh\:mm\:ss");

                if (respuesta.CError == "000")
                {
                    string fecha = string.Format("{2}{1}{0}", txtFechaInicio.Text.ToString().Substring(0, 2), txtFechaInicio.Text.ToString().Substring(3, 2),txtFechaInicio.Text.ToString().Substring(6, 4));  // DateTime.ParseExact(txtFechaFin.Text, "yyyyMMdd", CultureInfo.InvariantCulture);

                    archivo = string.Format("{0}_{1}_{2}.{3}",p.VAR_INPUT, p.VAR_CODIGO_ENTIDAD, fecha, p.VAR_FORMATO_CONCILIACION);

                    if(File.Exists(path + archivo))
                    {
                        rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                        rutaArchivo = path+archivo;
                        rutaArchivoTmp = Server.MapPath(rutaTemporal) + archivo;
                        if (!Directory.Exists(Server.MapPath(rutaTemporal)))
                            Directory.CreateDirectory(Server.MapPath(rutaTemporal));
                        File.Copy(rutaArchivo, rutaArchivoTmp, true);
                        txtFechaInicio.Enabled = false;
                        txtFechaFin.Enabled = false;
                        btnProcesar.Disabled = true;
                        btnProcesar.Visible = true;

                        CanalRespuesta c = new WebEstructurasConciliacion().DownloadFile(path, archivo);

                        respuesta.CError = c.CError;
                        respuesta.DError = c.DError;


                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", respuesta.DError + "\\nTIEMPO: " + tiempo, "OK"), true);

                    } else
                    {
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", respuesta.DError +"\\nTIEMPO: " + tiempo, "ERROR"), true);

                    }
                    


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
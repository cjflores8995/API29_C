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

public partial class formularios_0022 : System.Web.UI.Page
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
            LimpiarFormulario();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargarGrid()
    {
        ClientScriptManager cs = Page.ClientScript;
        LiteralControl lcControl = new LiteralControl();
        List<TCOSPAGOS> lista = new List<TCOSPAGOS>();


        string error;
        error = string.Empty;
        try
        {
            //lista = new WebCosede().ListarPagosUsuario(DateTime.Now, ((TSISUSUARIO)Session["sesionUsuario"]).CUSUARIO, txtIdentificacion.Text);
            lista = new WebCosede().ListarPagosUsuario(txtIdentificacion.Text);
            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    gridView.DataSource = lista;
                    gridView.DataBind();
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
        gridView.DataSource = null;
        gridView.DataBind();
        panelTabla.Update();
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridView.PageIndex = e.NewPageIndex;
        //CargarGrid();
    }

    private void LimpiarFormulario()
    {
        LimpiaGrid();
        txtIdentificacion.Text = string.Empty;
        //CargarGrid();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        LimpiaGrid();
    }

    public void DescargaComprobante(object sender, EventArgs e)
    {
        CanalRespuesta resp = null;
        string[] parametros = null;
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;
        string rutaTemporal = string.Empty;
        string rutaTemporalCompleta = string.Empty;

        try
        {
            LinkButton btn = (LinkButton)sender;

            if (!string.IsNullOrEmpty(btn.CommandArgument))
            {
                parametros = btn.CommandArgument.Split(';');
                resp = new WebCosede().GeneraComprobante(parametros[1], parametros[0], out rutaArchivo, out archivo);

                if (resp.CError == "000")
                {
                    if (!string.IsNullOrEmpty(rutaArchivo) && !string.IsNullOrEmpty(archivo))
                    {
                        rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                        rutaTemporalCompleta = Server.MapPath(rutaTemporal);
                        if (!Directory.Exists(rutaTemporalCompleta))
                            Directory.CreateDirectory(rutaTemporalCompleta);
                        File.Copy(rutaArchivo + archivo, rutaTemporalCompleta + archivo, true);

                        Response.Clear();
                        Response.Write("<script>window.open('" + (rutaTemporal + archivo) + "','_blank')</script>");


                        
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(resp.DError), "ER"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PARAMETROS PARA DESCARGA", "ER"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR GENERAL BUSCANDO DOCUMENTO: " + ex.Message.ToString().ToUpper(), "ER"), true);
        }
    }
}
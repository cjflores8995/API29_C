using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0004 : System.Web.UI.Page
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
                            Logging.EscribirLog(
                                MethodBase.GetCurrentMethod().DeclaringType + "::" +
                                MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                        }
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
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(
                MethodBase.GetCurrentMethod().DeclaringType + "::" +
                MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }
    protected void IniciaFormulario()
    {
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta",
            Util.MostarAlertaFormularios("", "PROCESO FINALIZADO CORRECTAMENTE", "OK"), true);
    }
}
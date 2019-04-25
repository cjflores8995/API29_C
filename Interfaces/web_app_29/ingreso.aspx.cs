using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ingreso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sesionActiva = string.Empty;

            try { sesionActiva = Session["sesionActiva"].ToString(); }
            catch { sesionActiva = "N"; }

            if (sesionActiva != "S")
            {
                Session.Add("sesionId", Session.SessionID);
            }
            else
            {
                Session.Clear();
                Session.Abandon();
            }
        }
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        WebUsuario nusu = new WebUsuario();

        TSISUSUARIO TSISUSUARIO = null;
        TSISTERMINAL TSISTERMINAL = null;
        List<VSISUSUARIOMENU> eMenu = null;
        List<VSISUSUARIOTRANSACCIONES> eTransacciones = null;
        CanalRespuesta CanalRespuesta = new CanalRespuesta();
        ClientScriptManager cs = Page.ClientScript;

        try
        {
            if (txtUsuario.Text != "" && txtClave.Text != "")
            {
                string sesionId = string.Empty;

                try { sesionId = Session["sesionId"].ToString(); }
                catch { Session.Add("sesionId", Session.SessionID); sesionId = Session["sesionId"].ToString(); }

                CanalRespuesta = nusu.ValidaUsuario(txtUsuario.Text, txtClave.Text, sesionId, buscaIp(),
                    out TSISUSUARIO, out TSISTERMINAL, out eTransacciones, out eMenu);

                if (CanalRespuesta.CError == "000")
                {
                    Session.Add("sesionActiva", "S");
                    Session.Add("sesionUsuario", TSISUSUARIO);
                    Session.Add("sesionTerminal", TSISTERMINAL);
                    Session.Add("sesionTransacciones", eTransacciones);
                    Session.Add("sesionMenu", eMenu);

                    Response.Redirect("~/frm/0000.aspx", false);
                }
                else if (CanalRespuesta.CError == "001")
                {
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "Cambio de Clave", "WR"));
                }
                else
                {
                    txtUsuario.Text = "";
                    txtClave.Text = "";
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", CanalRespuesta.DError, "ER"));
                }
            }
            else
            {
                txtUsuario.Text = "";
                txtClave.Text = "";
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "Ingrese Usuario y Clave", "WR"));
            }
        }
        catch (Exception ex)
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }

    protected string buscaIp()
    {
        string clientIP;

        clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (clientIP == null)
        {
            clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        if (clientIP == "::1")
        {
            clientIP = "127.0.0.1";
        }

        return clientIP;
    }
}
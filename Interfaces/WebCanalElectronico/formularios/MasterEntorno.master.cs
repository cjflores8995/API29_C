using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;

public partial class MasterEntorno : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralControl lcMenu = new LiteralControl();
        string sesionActiva = string.Empty;
        string menu = string.Empty;
        if (!IsPostBack)
        {
            try { sesionActiva = Session["sesionActiva"].ToString(); }
            catch { sesionActiva = "N"; }
            if (sesionActiva == "S")
            {
                List<VSISUSUARIOMENU> objMenu = new List<VSISUSUARIOMENU>();
                TSISTERMINAL objTerminal = new TSISTERMINAL();
                TSISUSUARIO objUsuario = new TSISUSUARIO();

                try
                {
                    objTerminal = (TSISTERMINAL)Session["sesionTerminal"];
                    objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
                    objMenu = (List<VSISUSUARIOMENU>)Session["sesionMenu"];
                    if (objTerminal != null)
                    {
                        txtTerminal.Text = objTerminal.IPADRESS;
                        txtAgencia.Text = Web.TraeDescripcionAgencia(objTerminal.COFICINA);
                    }
                    if (objUsuario != null)
                    {
                        txtUsuario.Text = objUsuario.ALIAS;
                        txtRol.Text = Web.TraeDescripcionRol(objUsuario.CROL);
                    }
                    if (objMenu != null)
                    {
                        menu += "<ul>";
                        var n1Data = objMenu.GroupBy(x => new { x.CMENU }).Select(y => new VSISUSUARIOMENU() { CMENU = y.Key.CMENU });
                        foreach (var item1 in n1Data)
                        {
                            var n1Obj = objMenu.Where(x => x.CMENU == item1.CMENU).FirstOrDefault();
                            menu += "<li class=\"nivel1\">";
                            menu += "<a href=\"#\" class=\"nivel1\">" + n1Obj.MENU + "</a>";
                            var n2Data = objMenu.Where(x => x.CMENU == item1.CMENU).GroupBy(x => new { x.CMENU, x.CMODULO }).
                                Select(y => new VSISUSUARIOMENU() { CMENU = y.Key.CMENU, CMODULO = y.Key.CMODULO });
                            menu += "<ul class=\"nivel2\">";
                            foreach (var item2 in n2Data)
                            {
                                var n2Obj = objMenu.Where(x => x.CMENU == n1Obj.CMENU && x.CMODULO == item2.CMODULO).FirstOrDefault();
                                menu += "<li><a href=\"#\">" + n2Obj.MODULO + "</a>";
                                menu += "<ul class=\"nivel3\">";
                                var n3Data = objMenu.Where(x => x.CMENU == item1.CMENU && x.CMODULO == item2.CMODULO).
                                    GroupBy(x => new { x.CMENU, x.CMODULO, x.CTRANSACCION }).
                                    Select(y => new VSISUSUARIOMENU() { CMENU = y.Key.CMENU, CMODULO = y.Key.CMODULO, CTRANSACCION = y.Key.CTRANSACCION });
                                int z = 1;
                                foreach (var item3 in n3Data)
                                {
                                    var n3Obj = objMenu.Where(x => x.CMENU == n1Obj.CMENU && x.CMODULO == n2Obj.CMODULO && x.CTRANSACCION == item3.CTRANSACCION).FirstOrDefault();
                                    if (n3Obj.INTERNA == "0")
                                    {
                                        menu +=
                                            "<li><a " + (z == 1 ? "class=\"primera\"" : "") +
                                            " href=\"" + n3Obj.CTRANSACCION + ".aspx?trx=" + Util.EncryptKey(n3Obj.CTRANSACCION) + "\">" +
                                            n3Obj.TRANSACCION + "</a></li>";
                                    }
                                }
                                menu += "</ul>";
                                menu += "</li>";
                            }
                            menu += "</ul>";
                            menu += "</li>";
                        }
                        menu += "</ul>";
                        Session.Add("menu", menu);
                        lcMenu.Text = menu;
                        phMenu.Controls.Add(lcMenu);
                    }
                }
                catch (Exception ex)
                {
                    Logging.EscribirLog(
                        MethodBase.GetCurrentMethod().DeclaringType + "::" +
                        MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                }
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/ingreso.aspx", true);
            }
        }
        else
        {
            try { sesionActiva = Session["sesionActiva"].ToString(); }
            catch { sesionActiva = "N"; }
            if (sesionActiva == "S")
            {
                lcMenu.Text = Session["menu"].ToString();
                phMenu.Controls.Add(lcMenu);
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/ingreso.aspx", true);
            }
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        WebUsuario nUsuario = new WebUsuario();
        TSISUSUARIO objUsuario = new TSISUSUARIO();
        try
        {
            objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
            nUsuario.CaducaSesion(objUsuario.CUSUARIO);
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
        finally
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/ingreso.aspx", true);
            cs.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript>window.close();</script>");
        }
    }
}

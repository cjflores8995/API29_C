<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        Business.Web.IniciaSistema();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error sin procesar
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse una nueva sesión
    }

    void Session_End(object sender, EventArgs e)
    {
        Business.WebUsuario nUsuario = new Business.WebUsuario();
        Business.TSISUSUARIO objUsuario = new Business.TSISUSUARIO();
        try
        {
            objUsuario = (Business.TSISUSUARIO)Session["sesionUsuario"];
            if (objUsuario != null)
            {
                nUsuario.CaducaSesion(objUsuario.CUSUARIO);
            }
        }
        catch (Exception ex)
        {
            Business.Logging.EscribirLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
        finally
        {
            Session.Clear();
            Session.Abandon();
            //HttpContext.Current.Response.Redirect("~/ingreso.aspx");
            //Response.Redirect("~/ingreso.aspx");
        }
    }
</script>

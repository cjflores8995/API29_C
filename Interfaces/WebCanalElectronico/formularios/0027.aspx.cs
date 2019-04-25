﻿using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0027 : System.Web.UI.Page
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

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        WebProcesos batch = new WebProcesos();
        string error;
        string proceso;
        int registrosCorrectos = 0;
        int registrosError = 0;

        try
        {
            if (txtFechaProceso.Text != "")
            {
                if (Util.ValidaFechas(txtFechaProceso.Text))
                {
                    btnProcesar.Disabled = true;
                    TSISUSUARIO objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
                    batch.CargaCobrosSifco(txtFechaProceso.Text, objUsuario.CUSUARIO, out proceso, out error, out registrosCorrectos, out registrosError);
                    if (error == "OK")
                    {
                        txtFechaProceso.Enabled = false;
                        txtNumeroProceso.Text = proceso;
                        txtError.Text = error;
                        txtCorrectos.Text = registrosCorrectos.ToString();
                        txtErrores.Text = registrosError.ToString();
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "PROCESO FINALIZADO CORRECTAMENTE", "OK"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", error, "ER"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "FECHA INCORRECTA", "ER"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "VERIFIQUE QUE TODOS LOS CAMPOS \\nTENGAN INFORMACION", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void LimpiarFormulario()
    {
        txtFechaProceso.Enabled = true;
        btnProcesar.Disabled = false;
        txtFechaProceso.Text = string.Empty;
        txtNumeroProceso.Text = string.Empty;
        txtError.Text = string.Empty;
        txtCorrectos.Text = string.Empty;
        txtErrores.Text = string.Empty;
        panelformulario.Update();
        panelTabla.Update();
    }
}
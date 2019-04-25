using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0026 : System.Web.UI.Page
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
        WebProcesos web = new WebProcesos();
        VBTHPROCESO proceso = new VBTHPROCESO();
        Int32 registros = 0;
        Decimal total = 0;

        try
        {
            if (Request.Params["values"] != null)
            {
                string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
                proceso = web.ConsultaProceso(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
                if (proceso != null)
                {
                    txtFechaProceso.Text = proceso.FPROCESO.Value.ToString("dd/MM/yyyy");
                    txtNumeroProceso.Text = proceso.CPROCESO.ToString();
                    txtTipoProceso.Text = proceso.TIPOPROCESO;
                    txtEstado.Text = proceso.ESTADO;
                    txtDescripcion.Text = proceso.DESCRIPCION;
                    txtUsuario.Text = proceso.USUARIO;

                    switch (proceso.CTIPOPROCESO)
                    {
                        case "SPITAB":
                            web.ConsultaTotalesDetalleTabulado(proceso.FPROCESO, proceso.CPROCESO, out registros, out total);
                            break;
                        case "EFECHE":
                            web.ConsultaTotalesCheques(proceso.FPROCESO, proceso.CPROCESO, out registros, out total);
                            break;
                        default:
                            registros = 0;
                            total = 0;
                            break;
                    }

                    txtRegistros.Text = registros.ToString();
                    txtTotal.Text = total.ToString("N2");
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
        Response.Redirect("0025.aspx?trx=" + Util.EncryptKey("0025"), false);
    }

    protected void btnAutorizar_ServerClick(object sender, EventArgs e)
    {
        WebProcesos web = new WebProcesos();
        VBTHPROCESO proceso = new VBTHPROCESO();

        try
        {
            TSISUSUARIO objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
            if (!string.IsNullOrEmpty(txtOtp.Text))
            {
                proceso = web.ConsultaProceso(Convert.ToDateTime(txtFechaProceso.Text), Convert.ToInt32(txtNumeroProceso.Text));
                if (proceso != null)
                {
                    if (Util.Encriptar(txtOtp.Text, Util.semilla) == proceso.CODIGOAUTORIZA)
                    {
                        if (proceso.CTIPOPROCESO == "SPITAB")
                            proceso.CESTADO = "PENVAL";
                        else
                            proceso.CESTADO = "PENPRO";
                        proceso.FAUTORIZA = DateTime.Now;
                        proceso.CUSUARIOAUTORIZA = objUsuario.CUSUARIO;
                        if (web.AutorizarProceso(proceso))
                        {
                            txtOtp.Enabled = false;
                            btnAutorizar.Disabled = true;
                            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "PROCESO AUTORIZADO", "OK"), true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR AUTORIZANDO PROCESO", "ER"), true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "CODIGO AUTORIZACION INGRESADO INCORRECTO", "WR"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO SE PUEDE RECUPERAR PROCESO", "ER"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "VERIFIQUE QUE LOS CAMPOS SE ENCUENTREN LLENOS", "IN"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }
}
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

public partial class formularios_0021 : System.Web.UI.Page
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
            txtIdentificacion.Text = string.Empty;
            txtTipoIdentificacion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtInstitucion.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtIdCosede.Text = string.Empty;
            txtCTipoIdentificacion.Text = string.Empty;

            txtIdentificacion.Enabled = true;
            txtTipoIdentificacion.Enabled = false;
            txtNombre.Enabled = false;
            txtInstitucion.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono1.Enabled = false;
            txtTelefono2.Enabled = false;
            txtCorreo.Enabled = false;
            txtMonto.Enabled = false;
            txtIdCosede.Enabled = false;
            txtCTipoIdentificacion.Enabled = false;

            btnBuscar.Disabled = false;
            btnPagar.Disabled = true;
            btnRecibo.Disabled = true;
            btnRecibo.Visible = true;
            lnkRecibo.Visible = false;
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }

    protected void btnBuscar_ServerClick(object sender, EventArgs e)
    {
        WebCosede cosede = new WebCosede();
        CanalRespuesta resp = null;
        TCOSPAGOS beneficiario = null;

        try
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                beneficiario = new TCOSPAGOS();
                resp = cosede.ConsultaBeneficiario(txtIdentificacion.Text, out beneficiario);

                if (resp.CError == "000" && beneficiario != null)
                {
                    txtIdCosede.Text = beneficiario.IDCOSEDE;
                    txtIdentificacion.Enabled = false;
                    txtIdentificacion.Text = beneficiario.IDENTIFICACION;
                    txtCTipoIdentificacion.Text = beneficiario.TIPOIDENTIFICACION;
                    switch (beneficiario.TIPOIDENTIFICACION)
                    {
                        case "C":
                            txtTipoIdentificacion.Text = "CEDULA";
                            break;
                        case "R":
                            txtTipoIdentificacion.Text = "RUC";
                            break;
                        case "P":
                            txtTipoIdentificacion.Text = "PASAPORTE";
                            break;
                        default:
                            break;
                    }
                    txtNombre.Text = beneficiario.NOMBRE;
                    txtInstitucion.Text = beneficiario.INSTITUCION;
                    txtDireccion.Text = beneficiario.DIRECCION;
                    txtTelefono1.Text = beneficiario.TELEFONO1;
                    txtCorreo.Text = beneficiario.CORREO;
                    try
                    {
                        txtMonto.Text = beneficiario.MONTO.Value.ToString("F2");
                    }
                    catch
                    {
                        txtMonto.Text = string.Empty;
                    }

                    txtIdentificacion.Enabled = false;
                    txtTipoIdentificacion.Enabled = false;
                    txtNombre.Enabled = false;
                    txtInstitucion.Enabled = false;
                    txtDireccion.Enabled = true;
                    txtTelefono1.Enabled = true;
                    txtTelefono2.Enabled = true;
                    txtCorreo.Enabled = true;
                    txtMonto.Enabled = false;
                    txtIdCosede.Enabled = false;
                    txtCTipoIdentificacion.Enabled = false;

                    btnBuscar.Disabled = true;
                    btnPagar.Disabled = false;
                    lnkRecibo.Disabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", resp.DError, "ER"), true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "INGRESE EL NUMERO DE IDENTIFICACION QUE DESEA CONSULTAR", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    protected void btnPagar_ServerClick(object sender, EventArgs e)
    {
        WebCosede cosede = new WebCosede();
        CanalRespuesta resp = null;
        TSISTERMINAL terminal = null;
        TSISUSUARIO usuario = null;
        TCOSPAGOS beneficiario = null;
        string tipoAlerta = string.Empty;

        try
        {
            if (!string.IsNullOrEmpty(txtIdCosede.Text) && !string.IsNullOrEmpty(txtDireccion.Text) && !string.IsNullOrEmpty(txtTelefono1.Text))
            {
                terminal = (TSISTERMINAL)Session["sesionTerminal"];
                usuario = (TSISUSUARIO)Session["sesionUsuario"];
                beneficiario = new TCOSPAGOS();
                beneficiario.INSTITUCION = txtInstitucion.Text;
                beneficiario.TIPOIDENTIFICACION = txtCTipoIdentificacion.Text;
                beneficiario.IDENTIFICACION = txtIdentificacion.Text;
                beneficiario.NOMBRE = txtNombre.Text;
                beneficiario.MONTO = Convert.ToDecimal(txtMonto.Text);
                beneficiario.DIRECCION = txtDireccion.Text.ToUpper();
                beneficiario.TELEFONO1 = txtTelefono1.Text;
                beneficiario.TELEFONO2 = txtTelefono2.Text;
                beneficiario.CORREO = txtCorreo.Text;
                beneficiario.IDCOSEDE = txtIdCosede.Text;

                resp = cosede.PagoBeneficiario(beneficiario, terminal, usuario);

                txtIdentificacion.Enabled = false;
                txtTipoIdentificacion.Enabled = false;
                txtNombre.Enabled = false;
                txtInstitucion.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelefono1.Enabled = false;
                txtTelefono2.Enabled = false;
                txtCorreo.Enabled = false;
                txtMonto.Enabled = false;
                txtIdCosede.Enabled = false;
                txtCTipoIdentificacion.Enabled = false;

                btnBuscar.Disabled = true;
                btnPagar.Disabled = true;

                switch (resp.CError)
                {
                    case "000":
                        tipoAlerta = "OK";
                        btnRecibo.Disabled = false;
                        break;
                    case "001":
                        tipoAlerta = "WR";
                        btnRecibo.Disabled = false;
                        break;
                    default:
                        tipoAlerta = "ER";
                        btnRecibo.Disabled = true;
                        break;
                }

                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(resp.DError), tipoAlerta), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "VERIFIQUE QUE TODOS LOS CAMPOS OBLIGATORIOS ESTEN COMPLETOS", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    protected void btnRecibo_ServerClick(object sender, EventArgs e)
    {
        WebCosede cosede = new WebCosede();
        CanalRespuesta resp = null;
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;
        string rutaTemporal = string.Empty;
        string rutaTemporalCompleta = string.Empty;

        try
        {
            resp = cosede.GeneraComprobante(txtIdCosede.Text, txtIdentificacion.Text, out rutaArchivo, out archivo);
            if (resp.CError == "000")
            {
                if (!string.IsNullOrEmpty(rutaArchivo) && !string.IsNullOrEmpty(archivo))
                {
                    rutaTemporal = ConfigurationManager.AppSettings["pathTmp"];
                    rutaTemporalCompleta = Server.MapPath(rutaTemporal);
                    if (!Directory.Exists(rutaTemporalCompleta))
                        Directory.CreateDirectory(rutaTemporalCompleta);
                    File.Copy(rutaArchivo + archivo, rutaTemporalCompleta + archivo, true);
                    btnRecibo.Disabled = true;
                    btnRecibo.Visible = false;
                    lnkRecibo.Visible = true;
                    lnkRecibo.HRef = rutaTemporal + archivo;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(resp.DError), "ER"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }
}
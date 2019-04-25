using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_0017 : System.Web.UI.Page
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
        WebProcesos tab = new WebProcesos();
        VBTHPROCESO proceso = null;
        string error = string.Empty;
        Int32 registros = 0;
        Decimal total = 0;

        try
        {
            if (Request.Params["values"] != null)
            {
                string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
                proceso = tab.ConsultaProceso(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
                if (proceso != null)
                {
                    try { txtFechaProceso.Text = proceso.FPROCESO.Value.ToString("dd/MM/yyyy"); }
                    catch { txtFechaProceso.Text = string.Empty; }
                    try { txtNumeroProceso.Text = proceso.CPROCESO.Value.ToString(); }
                    catch { txtNumeroProceso.Text = string.Empty; }
                    txtTipoProceso.Text = proceso.CTIPOPROCESO + " - " + proceso.TIPOPROCESO;
                    txtEstado.Text = proceso.CESTADO + " - " + proceso.ESTADO;
                    switch (proceso.CORTE)
                    {
                        case 1:
                            txtCorte.Text = "PRIMER CORTE";
                            break;
                        case 2:
                            txtCorte.Text = "SEGUNDO CORTE";
                            break;
                        case 3:
                            txtCorte.Text = "TERCER CORTE";
                            break;
                        default:
                            txtCorte.Text = string.Empty;
                            break;
                    }
                    txtDescripcion.Text = proceso.DESCRIPCION;
                    txtFormatoArchivo.Text = proceso.CTIPOARCHIVO + " - " + proceso.TIPOARCHIVO;
                    txtReparto.Text = proceso.CREPARTO + " - " + proceso.REPARTO;
                    txtUsuario.Text = proceso.CUSUARIO + " - " + proceso.USUARIO;
                    try { txtFCarga.Text = proceso.FCARGA.Value.ToString("dd/MM/yyyy HH:mm:ss"); }
                    catch { txtFCarga.Text = string.Empty; }
                    txtError.Text = proceso.ERROR;

                    if (tab.ConsultaTotalesCheques(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()), out registros, out total)) ;
                    {
                        try { txtRegistros.Text = registros.ToString(); }
                        catch { txtRegistros.Text = string.Empty; }

                        try { txtTotal.Text = total.ToString("N2"); }
                        catch { txtRegistros.Text = string.Empty; }
                    }

                    CargarGridErroresCheques();
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
        Response.Redirect("0015.aspx?trx=" + Util.EncryptKey("0015"), false);
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }

    protected void gridErrores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridErrores.PageIndex = e.NewPageIndex;
        CargarGridErroresCheques();
    }

    private void CargarGridErroresCheques()
    {
        WebProcesos tab = new WebProcesos();
        List<VBTHCHEQUESERRORES> ltDetalle = null;

        try
        {
            string[] values = Util.DecryptKey(Request.Params["values"]).Split('|');
            ltDetalle = tab.ErroresCheques(Convert.ToDateTime(values[0].ToString()), Convert.ToInt32(values[1].ToString()));
            if (ltDetalle != null && ltDetalle.Count > 0)
            {
                gridErrores.DataSource = ltDetalle;
                gridErrores.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }
}
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

public partial class formularios_0030 : System.Web.UI.Page
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
        LimpiarFormulario();
    }

    protected void LimpiarFormulario()
    {
        txtArchivo.Enabled = true;
        txtFechaCorte.Enabled = true;
        btnCargar.Disabled = false;
        CargaTipos();
    }

    private void CargaTipos()
    {
        ClientScriptManager cs = Page.ClientScript;
        WebProcesos tab = new WebProcesos();
        string error = string.Empty;
        try
        {
            /*ddlTipo.Items.Clear();
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("", ""));
            ddlTipo.SelectedValue = "";

            ddlTipo.Items.Add(new ListItem("DATAFAST", "datafast"));
            ddlTipo.Items.Add(new ListItem("CORPORACION FAVORITA", "favorita"));
            ddlTipo.Items.Add(new ListItem("FYBECA", "farcomed"));
            ddlTipo.Items.Add(new ListItem("SANA SANA", "econofarm"));*/

        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        }
    }


    protected void btnCargar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
        string ruta = ConfigurationManager.AppSettings["pathArchivosSpi"].Trim();
        string archivo = string.Empty;
        string rutaArchivo = string.Empty;
        CanalRespuesta existeDirectorio = new CanalRespuesta();
        try
        {
            existeDirectorio = Util.ValidateDirectory(ruta);
            archivo = txtFechaCorte.Text.Substring(6,4)+ txtFechaCorte.Text.Substring(3, 2)+ txtFechaCorte.Text.Substring(0, 2)+ "\\";
            rutaArchivo = ruta + archivo;
            string nombreArchivo = txtArchivo.PostedFile.FileName;
            string nuevoNombre = archivo.Substring(0, archivo.Length - 1) + txtNumeroCorte.Text+".txt";


            //elimino el archivo en caso de que exista
            if (File.Exists(@rutaArchivo + nuevoNombre))
                File.Delete(@rutaArchivo + nuevoNombre);

            //valido que exista el directorio del SPI
            if (existeDirectorio.CError == "000")
            {
                if (!Directory.Exists(rutaArchivo))
                    Directory.CreateDirectory(rutaArchivo);

                txtArchivo.PostedFile.SaveAs(rutaArchivo + nombreArchivo);

                File.Move(@rutaArchivo + nombreArchivo, @rutaArchivo + nuevoNombre);

                txtArchivo.Enabled = false;
                btnCargar.Disabled = true;

                //obtengo el usuario
                //if((TSISUSUARIO)Session["sesionUsuario"] != null)
                //{
                //    TSISUSUARIO objUsuario = (TSISUSUARIO)Session["sesionUsuario"];
                //}
                //else {
                //    TSISUSUARIO objUsuario = "0000";
                //}

                //mover este codigo al .dll Business

                CanalRespuesta resultado = new WebProcessSpi().ReadDataSpi(@rutaArchivo + nuevoNombre, Convert.ToDateTime(txtFechaCorte.Text), int.Parse(txtNumeroCorte.Text));

                if(resultado.CError == "000")
                {
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", resultado.DError, "OK"));
                } else
                {
                    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", resultado.DError, "ER"));
                }


            } else
            {
                cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", existeDirectorio.DError, "ER"));
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "WR"));
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        IniciaFormulario();
    }
}
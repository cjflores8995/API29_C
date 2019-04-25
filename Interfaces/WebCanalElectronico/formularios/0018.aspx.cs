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

public partial class formularios_0018 : System.Web.UI.Page
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
        txtCodigo.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtNombre.Enabled = true;
        txtArchivo.Enabled = true;
        lblArchivoCargado.Text = string.Empty;
        lblArchivoCargado.Visible = false;
        LimpiaGrid();
        CargarGrid();
    }

    protected bool ValidaCampos()
    {
        if (string.IsNullOrEmpty(txtNombre.Text))
            return false;
        if (!txtArchivo.HasFile)
            return false;
        return true;
    }

    private void CargarGrid()
    {
        //ClientScriptManager cs = Page.ClientScript;
        //List<TMAILLISTA> ltObj = null;
        //string error;
        //error = string.Empty;
        //try
        //{
        //    ltObj = new CanalMailing().CargarListas(out error);

        //    if (error == "OK")
        //    {
        //        if (ltObj.Count > 0)
        //        {
        //            gvDetalle.DataSource = ltObj;
        //            gvDetalle.DataBind();
        //        }
        //        else
        //        {
        //            LimpiaGrid();
        //        }
        //    }
        //    else
        //    {
        //        LimpiaGrid();
        //        cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "ERROR CONSULTANDO REGISTROS: " + error, "ER"));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LimpiaGrid();
        //    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        //    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "ER"));
        //}
    }

    private void LimpiaGrid()
    {
        gvDetalle.SelectedIndex = -1;
        gvDetalle.DataSource = null;
        gvDetalle.DataBind();
    }

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        //ClientScriptManager cs = Page.ClientScript;
        //TMAILLISTA obj = null;
        //CanalMailing mail = new CanalMailing();
        //CanalRespuesta respuesta = new CanalRespuesta();
        //string ruta, archivo, rutaArchivo, rutaZip, nombreZip, rutaTemporal, rutaArchivoTmp;
        //ruta = archivo = rutaArchivo = rutaZip = nombreZip = rutaTemporal = rutaArchivoTmp = string.Empty;
        //try
        //{
        //    if (ValidaCampos())
        //    {
        //        obj = new TMAILLISTA();
        //        obj.DESCRIPCION = txtNombre.Text.ToUpper();
        //        obj.CARGA = "0";
        //        obj.FMODIFICACION = DateTime.Now;
        //        obj.CUSUARIOMODIFICACION = ((TSISUSUARIO)Session["sesionUsuario"]).CUSUARIO;

        //        if (string.IsNullOrEmpty(txtCodigo.Text))
        //        {
        //            respuesta = mail.CrearLista(ref obj);
        //        }
        //        else
        //        {
        //            obj.CLISTA = Convert.ToInt32(txtCodigo.Text);
        //            respuesta = mail.ActualizarLista(ref obj);
        //        }

        //        if (respuesta.CError == "000")
        //        {
        //            txtNombre.Enabled = false;
        //            txtArchivo.Enabled = false;
        //            lblArchivoCargado.Text = txtArchivo.PostedFile.FileName;
        //            lblArchivoCargado.Visible = true;

        //            ruta = ConfigurationManager.AppSettings["pathArchivosMailingListas"].Trim();
        //            if (!Directory.Exists(ruta))
        //                Directory.CreateDirectory(ruta);
        //            rutaArchivo = ruta + obj.CLISTA + ".txt";
        //            txtArchivo.PostedFile.SaveAs(rutaArchivo);
        //            LimpiaGrid();
        //            CargarGrid();

        //            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "LISTA CARGADA CORRECTAMENTE", "OK"));
        //        }
        //        else
        //        {
        //            cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", respuesta.DError, "ER"));
        //        }
        //    }
        //    else
        //    {
        //        cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", "DEBE INGRESAR LOS CAMPOS MARCADOS CON (*)", "WR"));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        //    cs.RegisterStartupScript(this.GetType(), "PopupScript", Util.MostarAlerta("", Util.ReturnExceptionString(ex), "WR"));
        //}
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetalle.PageIndex = e.NewPageIndex;
        CargarGrid();
    }

    protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvDetalle.SelectedRow;
        txtCodigo.Text = row.Cells[0].Text;
        txtNombre.Text = row.Cells[1].Text;
    }

    protected void gvDetalle_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = gvDetalle.Rows[e.NewSelectedIndex];
        txtCodigo.Text = row.Cells[0].Text;
        txtNombre.Text = row.Cells[1].Text;
    }
}
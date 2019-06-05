using Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class formularios_0024 : System.Web.UI.Page
{
    private string ftpParametrosVar = string.Empty;
    private string ftpPathPdfVar = string.Empty;
    private string ftpPathXmlVar = string.Empty;
    private string ftpLocalVar = string.Empty; 
    private string ftpPathProcom = string.Empty; 

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
            CargaTiposComprobantes();
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargaTiposComprobantes()
    {
        ClientScriptManager cs = Page.ClientScript;
        string error = string.Empty;
        try
        {
            ddlTipoComprobante.Items.Clear();
            ddlTipoComprobante.DataBind();
            ddlTipoComprobante.Items.Insert(0, new ListItem("", ""));
            ddlTipoComprobante.Items.Insert(1, new ListItem("FACTURA", "01"));
            ddlTipoComprobante.Items.Insert(2, new ListItem("NOTA DE CREDITO", "04"));
            ddlTipoComprobante.Items.Insert(2, new ListItem("RETENCION", "07"));
            ddlTipoComprobante.Items.Insert(2, new ListItem("NOTA DE DEBITO", "05"));
        }
        catch (Exception ex)
        {
            LimpiaGrid();
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    //Carga las propiedades de facturacion Electronica
    public void CargaPropiedadesFacturacionElectronica()
    {
        try
        {
            ftpParametrosVar = System.Configuration.ConfigurationManager.AppSettings["ftpParametros"].ToString();
            ftpPathPdfVar = System.Configuration.ConfigurationManager.AppSettings["ftpPathPdf"].ToString();
            ftpPathXmlVar = System.Configuration.ConfigurationManager.AppSettings["ftpPathXml"].ToString();
            ftpLocalVar = System.Configuration.ConfigurationManager.AppSettings["ftpLocal"].ToString();
            ftpPathProcom = System.Configuration.ConfigurationManager.AppSettings["ftpPathProcom"].ToString();

        } catch(Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
        }
    }

    private void CargarGrid()
    {
        ClientScriptManager cs = Page.ClientScript;
        LiteralControl lcControl = new LiteralControl();
        List<VFECOMPROBANTES> lista = new List<VFECOMPROBANTES>();
        List<VFACTURACIONELECTRONICA> documentos = new List<VFACTURACIONELECTRONICA>();

        try
        {
            //si el campo identificacion no esta vacio
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                //lista = new WebFacturacion().ListarComprobantes(txtIdentificacion.Text, ddlTipoComprobante.SelectedValue, txtComprobante.Text, txtFechaDesde.Text, txtFechaHasta.Text);

                //traigo el select de la vista con la informacion de fitbank
                documentos = new VFACTURACIONELECTRONICA().ListarDocumentos(txtIdentificacion.Text, ddlTipoComprobante.SelectedValue, txtComprobante.Text, txtFechaDesde.Text, txtFechaHasta.Text);

                if (documentos != null && documentos.Count > 0)
                {
                    if (documentos.Count > 0)
                    {
                        foreach (var item in documentos)
                        {
                            item.linkPdf = "PDF;" + item.DOCUMENTOFACTURACION + ";" + item.NUMERODOCUMENTO + ";" + item.FECHAEMISION.Value.ToString("dd/MM/yyyy")+";"+item.NUMEROAUTORIZACION+";"+item.CTIPODOCUMENTOFACTURACION;
                            item.linkXml = "XML;" + item.DOCUMENTOFACTURACION + ";" + item.NUMERODOCUMENTO + ";" + item.FECHAEMISION.Value.ToString("dd/MM/yyyy") + ";" + item.NUMEROAUTORIZACION + ";" + item.CTIPODOCUMENTOFACTURACION;
                        }
                        gridView.DataSource = documentos;
                        gridView.DataBind();
                    }
                    else
                    {
                        LimpiaGrid();
                        ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PROCESOS PARA LOS CRITERIOS INGRESADOS", "IN"), true);
                    }
                }
                else
                {
                    LimpiaGrid();
                    ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR CONSULTANDO PROCESOS", "ER"), true);
                }
            }
            else
            {
                LimpiaGrid();
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ES NECESARIO AL MENOS INGRESAR EL NUMERO DE IDENTIFICACION", "WR"), true);
            }
        }
        catch (Exception ex)
        {
            LimpiaGrid();
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", Util.ReturnExceptionString(ex), "ER"), true);
        }
    }

    private void LimpiaGrid()
    {
        gridView.DataSource = null;
        gridView.DataBind();
        panelTabla.Update();
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridView.PageIndex = e.NewPageIndex;
        CargarGrid();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        txtComprobante.Text = string.Empty;
        txtFechaDesde.Text = string.Empty;
        txtFechaHasta.Text = string.Empty;
        CargaTiposComprobantes();
        LimpiaGrid();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrid();
    }

    public void DescargaComprobante(object sender, EventArgs e)
    {
        CargaPropiedadesFacturacionElectronica();

        string error = "No se ha encontrado el documento solicitado";
        string tmp = string.Empty;
        string pathTmp = string.Empty;
        string pathArchivo = string.Empty;
        string archivo = string.Empty;
        DateTime fecha = DateTime.Now;
        string[] parametros = null;
        string nombreDocumento = string.Empty;

        try
        {
            LinkButton btn = (LinkButton)sender;

            if (!string.IsNullOrEmpty(btn.CommandArgument))
            {
                parametros = btn.CommandArgument.Split(';');
                fecha = Convert.ToDateTime(parametros[3]);
                tmp = ConfigurationManager.AppSettings["pathTmp"];
                pathArchivo = ConfigurationManager.AppSettings["pathArchivos"];
                pathTmp = Server.MapPath(tmp);
                bool PC = false;
                

                
                switch (parametros[0])
                {
                    case "PDF":

                        //nombre para facturacion electronica
                        string ftpPathPdfVar1 = ftpPathPdfVar + parametros[4] + ".pdf";
                        nombreDocumento = parametros[4] + ".pdf";

                        //nombre para proveedores y compras
                        string ftpPathPdfVarPC = ftpPathPdfVar + "Retencion" + parametros[4] + ".PDF";

                        bool respVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVar1, pathTmp, out error);

                        if(respVar == false)
                        {
                            Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVarPC, pathTmp, out error);
                            nombreDocumento = "Retencion" + parametros[4] + ".PDF";
                        }

                        
                        archivo = nombreDocumento;
                        if (File.Exists(pathTmp + archivo))
                        {
                            Response.Clear();
                            Response.Write("<script>window.open('" + (tmp + archivo) + "','_blank')</script>");
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                        }
                        break;
                    case "XML":

                        string ftpPathXmlVar1 = ftpPathXmlVar + "RETENCION" + parametros[4];
                        nombreDocumento = "RETENCION" + parametros[4];// + ".xml"; 

                        bool respXMLVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathXmlVar1, pathTmp, out error);

                        if(!respXMLVar)
                        {
                            string ftpPathXmlVar2 = ftpPathXmlVar + "FACTURA" + parametros[4];// + ".xml";
                            nombreDocumento = "FACTURA" + parametros[4];// + ".xml";
                            respXMLVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathXmlVar2, pathTmp, out error);
                        }

                        if (!respXMLVar)
                        {
                            string ftpPathXmlVar2 = ftpPathProcom + "Retencion" + parametros[4] + ".xml";
                            nombreDocumento = "Retencion" + parametros[4] + ".xml";
                            respXMLVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathXmlVar2, pathTmp, out error);
                            PC = true;
                        }

                        if(respXMLVar)
                        {
                            archivo = nombreDocumento;
                            if (File.Exists(pathTmp + archivo))
                            {
                                string strFullPath = (pathTmp + archivo);

                                FileInfo file = new FileInfo(pathTmp + archivo);

                                string strContents = null;
                                System.IO.StreamReader objReader = default(System.IO.StreamReader);
                                objReader = new System.IO.StreamReader(strFullPath);
                                strContents = objReader.ReadToEnd();
                                objReader.Close();



                                string attachment = (PC==true) ?("attachment; filename=" + nombreDocumento): ("attachment; filename=" + nombreDocumento + ".xml");
                                //string attachment = "attachment; filename=" + nombreDocumento;

                                Response.ClearContent();
                                Response.ContentType = "application/xml";
                                Response.AddHeader("content-disposition", attachment);
                                Response.Write(strContents);
                                Response.End();
                            }
                            else
                            {
                                Response.Clear();
                                Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                            }
                        }


                        ////nombre para proveedores y compras
                        //string ftpPathPdfVarPC = ftpPathPdfVar + "Retencion" + parametros[4] + ".PDF";

                        //bool respVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVar1, pathTmp, out error);

                        //if (respVar == false)
                        //{
                        //    Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVarPC, pathTmp, out error);
                        //    nombreDocumento = "Retencion" + parametros[4] + ".PDF";
                        //}



                        //fin del proceso de copia del xml


                        
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "NO EXISTEN PARAMETROS PARA DESCARGA", "ER"), true);
            }
        }
        catch (Exception ex)
        {
            Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            ScriptManager.RegisterStartupScript(this.panelformulario, GetType(), "alerta", Util.MostarAlertaFormularios("", "ERROR GENERAL BUSCANDO DOCUMENTO: " + ex.Message.ToString().ToUpper(), "ER"), true);
        }


        //string rutaTemporal = ConfigurationManager.AppSettings["pathTmp"] + "comprobante_1801379167_20170525_20170525115721.pdf";

        //Response.Write("<script>window.open('" + rutaTemporal + "','_blank')</script>");



        //lnkDescargar.HRef = "http://localhost/canalelectronico/ingreso.aspx";

        //ClientScript.RegisterStartupScript(
        //    this.GetType(), "newWindow", "<script>AbrirLnk('lnkDescargar');</script>");


        //FileInfo file = new FileInfo(@"D:\temp\RETENCION001-013-000025081.xml");

        //Response.Clear();
        //Response.ContentType = "text/xml";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //Response.TransmitFile(file.FullName);
        //Response.End();


        //LinkButton btn = (LinkButton)sender;
        //using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        //{
        //    

        //    //string url = "http://localhost/canalelectronico/tmp/comprobante_1801379167_20170525_20170525115721.pdf";

        //    //ClientScript.RegisterStartupScript(
        //    //    this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));

        //    //<script type=text/javascript>window.close();</script>
        //}
    }


    /*public bool GeneraDocumentoCliente(string FORMATO_DOCUMENTO, string NUMERO_AUTORIZACION, string TIPO_DOCUMENTO, string PATH_TMP)
    {
        
        bool respuestaVar = false;
        string nombreVarchivoRuta = string.Empty;
        string nombreArchivoVar = string.Empty;

        switch(FORMATO_DOCUMENTO)
        {
            case "PDF":

                switch(TIPO_DOCUMENTO)
                {
                    case "01": //factura
                        nombreVarchivoRuta = string.Format("{0}{1}.pdf", ftpPathPdfVar, NUMERO_AUTORIZACION);
                        nombreArchivoVar = string.Format("{0}.pdf", NUMERO_AUTORIZACION);
                        respuestaVar = Util.ExtraerSftp(ftpParametrosVar, nombreVarchivoRuta, PATH_TMP, out error);
                        break;
                    case "07": //retencion
                        nombreVarchivoRuta = string.Format("{0}{1}.pdf", ftpPathPdfVar, NUMERO_AUTORIZACION);
                        nombreArchivoVar = string.Format("{0}.pdf", NUMERO_AUTORIZACION);
                        respuestaVar = Util.ExtraerSftp(ftpParametrosVar, nombreVarchivoRuta, PATH_TMP, out error);

                        if(!respuestaVar)
                        {
                            nombreVarchivoRuta = string.Format("{0}Retencion{1}.PDF", ftpPathPdfVar, NUMERO_AUTORIZACION);
                            nombreArchivoVar = string.Format("Retencion{0}.PDF", NUMERO_AUTORIZACION);
                            Util.ExtraerSftp(ftpParametrosVar, nombreVarchivoRuta, PATH_TMP, out error);
                            if (nombreVarchivoRuta != null)
                            {
                                Response.Clear();
                                Response.Write("<script>window.open('" + (PATH_TMP + nombreArchivoVar) + "','_blank')</script>");
                            }
                        }
                        break;
                    case "04": //nota de credito
                        break;
                    case "05": //nota de debito
                        break;
                    default:
                        Response.Clear();
                        Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                        break;
                }
                    break;
            case "XML":
                break;
            default:
                Response.Clear();
                Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                break;
        }

        

        /*
        switch (FORMATO_DOCUMENTO)
        {
            case "PDF":

                //nombre para facturacion electronica
                string ftpPathPdfVar1 = ftpPathPdfVar + parametros[4] + ".pdf";
                nombreDocumento = parametros[4] + ".pdf";

                //nombre para proveedores y compras
                string ftpPathPdfVarPC = ftpPathPdfVar + "Retencion" + parametros[4] + ".PDF";

                bool respVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVar1, pathTmp, out error);

                if (respVar == false)
                {
                    Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVarPC, pathTmp, out error);
                    nombreDocumento = "Retencion" + parametros[4] + ".PDF";
                }


                archivo = nombreDocumento;
                if (File.Exists(pathTmp + archivo))
                {
                    Response.Clear();
                    Response.Write("<script>window.open('" + (tmp + archivo) + "','_blank')</script>");
                }
                else
                {
                    Response.Clear();
                    Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                }
                break;
            case "XML":
                //inicio del proceso de copia del xml
                //nombre para facturacion electronica
                string ftpPathXmlVar1 = ftpPathXmlVar + parametros[4] + ".xml";
                nombreDocumento = parametros[4] + ".xml";

                //nombre para proveedores y compras
                string ftpPathPdfVarPC = ftpPathPdfVar + "Retencion" + parametros[4] + ".PDF";

                bool respVar = Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVar1, pathTmp, out error);

                if (respVar == false)
                {
                    Util.ExtraerSftp(ftpParametrosVar, ftpPathPdfVarPC, pathTmp, out error);
                    nombreDocumento = "Retencion" + parametros[4] + ".PDF";
                }



                //fin del proceso de copia del xml


                archivo = parametros[1] + "_" + parametros[2] + ".xml";
                if (File.Exists(pathArchivo + archivo))
                {
                    string strFullPath = (@pathArchivo + archivo);

                    FileInfo file = new FileInfo(@pathArchivo + archivo);

                    string strContents = null;
                    System.IO.StreamReader objReader = default(System.IO.StreamReader);
                    objReader = new System.IO.StreamReader(strFullPath);
                    strContents = objReader.ReadToEnd();
                    objReader.Close();

                    string attachment = ("attachment; filename=" + file.Name);
                    Response.ClearContent();
                    Response.ContentType = "application/xml";
                    Response.AddHeader("content-disposition", attachment);
                    Response.Write(strContents);
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("<script>alert('DOCUMENTO NO ENCONTRADO')</script>");
                }
                break;
            default:
                break;
        }

        */
        


}
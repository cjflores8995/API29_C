<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0021.aspx.cs" Inherits="formularios_0021" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
<script>
        function valida(e) {
            tecla = (document.all) ? e.keyCode : e.which;

            //Tecla de retroceso para borrar, siempre la permite
            if (tecla == 8) {
                return true;
            }

            // Patron de entrada, en este caso solo acepta numeros
            patron = /[0-9]/;
            tecla_final = String.fromCharCode(tecla);
            return patron.test(tecla_final);
        }
</script>
<script type='text/javascript' src='/js/funciones.js'></script>

<script type="text/javascript">
function validar(e) {
tecla = (document.all) ? e.keyCode : e.which;
if (tecla==8) return true;
patron =/[A-Za-z0-9\s]/;
    //patron =/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.([a-zA-Z]{2,4})+$/;
te = String.fromCharCode(tecla);
return patron.test(te);
    }


   

function validarCorreo(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla==8) return true;
    patron =/[a-zA-Z0-9_.-@]+/;
    //patron =/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.([a-zA-Z]{2,4})+$/;
    te = String.fromCharCode(tecla);
    return patron.test(te);

}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div class="formulario">
            <asp:UpdatePanel ID="panelformulario" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table class="form-col1">
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblIdentificacion" runat="server">Identificación: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="texto texto-2 texto-large" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblTipoIdentificacion" runat="server">Tipo Identificación: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="texto texto-2 texto-large" Width="350px" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblNombre" runat="server">Nombre: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="texto texto-2 texto-large" Width="350px" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblInstitucion" runat="server">Institución: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtInstitucion" runat="server" CssClass="texto texto-2 texto-large" Width="350px" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblDireccion" runat="server">Dirección: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="texto texto-2 texto-large" Width="350px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblTelefono1" runat="server">Teléfono: (*)</asp:Label>
								
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtTelefono1" runat="server" CssClass="texto texto-2 texto-large" Width="350px"  onkeypress="return valida(event)" MaxLength="10"></asp:TextBox>
								
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblTelefono2" runat="server">Celular:</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtTelefono2" runat="server" CssClass="texto texto-2 texto-large" Width="350px"  onkeypress="return valida(event)" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblCorreo" runat="server">Correo:</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="texto texto-2 texto-large" Width="350px" onkeypress="return validarCorreo(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblMonto" runat="server">Monto: (*)</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtMonto" runat="server" CssClass="texto texto-2 texto-large" Width="350px" Enabled="false" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tfoot>
                            <tr>
                                <td colspan="4">
                                    <button id="btnBuscar" runat="server" type="button" class="btn btn-2 icon-search" title="Buscar" onserverclick="btnBuscar_ServerClick"></button>
                                    <button id="btnPagar" runat="server" type="button" class="btn btn-2 icon-money" title="Realizar Pago" onserverclick="btnPagar_ServerClick"></button>
                                    <button id="btnRecibo" runat="server" type="button" class="btn btn-2 icon-file-text2" title="Generar Recibo" onserverclick="btnRecibo_ServerClick"></button>
                                    <a id="lnkRecibo" runat="server" href="#" target="_blank" visible="false" class="lnk lnk-1 icon-print" title="Comprobante Pago"></a>
                                    <button id="btnLimpiar" runat="server" type="button" class="btn btn-2 icon-magic" title="Limpiar" onserverclick="btnLimpiar_Click"></button>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="progressFormulario" runat="server">
                <ProgressTemplate>
                    <div id="load">
                        <asp:Image ID="Image1" ImageUrl="~/img/load.gif" runat="server" />
                        <br />
                        Procesando, espere por favor...!!!
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false" Enabled="false" ReadOnly="true"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtIdCosede" Visible="false" Enabled="false" ReadOnly="true"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCTipoIdentificacion" Visible="false" Enabled="false" ReadOnly="true"></asp:TextBox>
    </div>
</asp:Content>


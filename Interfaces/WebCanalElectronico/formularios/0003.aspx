<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0003.aspx.cs" Inherits="formularios_0003" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div class="formulario">
            <table class="form-col1">
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblFechaCorte" runat="server">Fecha Corte</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtFechaCorte" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblTipoEstructura" runat="server">Estructura</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:DropDownList ID="ddlEstructura" runat="server" AutoPostBack="false" CssClass="combo combo-large">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblArchivo" runat="server">Archivo</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:FileUpload ID="txtArchivo" runat="server"></asp:FileUpload>
                    </td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <button id="btnProcesar" runat="server" type="button" class="btn btn-2 icon-cogs" title="Procesar" onserverclick="btnProcesar_Click"></button>
                            <a id="lnkDescargar" runat="server" href="#" target="_blank" visible="false" class="lnk lnk-1 icon-download" title="Descargar"></a>
                            <button id="btnLimpiar" runat="server" type="button" class="btn btn-2 icon-magic" title="Limpiar" onserverclick="btnLimpiar_Click"></button>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false"></asp:TextBox>
    </div>
</asp:Content>


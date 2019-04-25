<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0014.aspx.cs" Inherits="formularios_0014" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div class="formulario">
            <table class="form-col2">
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblFechaProceso" runat="server">Fecha Proceso</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtfcontable" runat="server" CssClass="texto texto-2 texto-large" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="label-form">
                        <asp:Label ID="lblNumeroProceso" runat="server">Numero Proceso</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtNumeroProceso" runat="server" CssClass="texto texto-2 texto-large" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="Label1" runat="server">Tipo Proceso *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:DropDownList ID="ddlTipoProceso" runat="server" AutoPostBack="true" CssClass="combo combo-large" OnSelectedIndexChanged="ddlTipoProceso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="label-form">
                        <asp:Label ID="lblTipoArchivo" runat="server">Formato Archivo *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:DropDownList ID="ddlArchivo" runat="server" AutoPostBack="true" CssClass="combo combo-large" OnSelectedIndexChanged="ddlArchivo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblCorte" runat="server">Corte *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:DropDownList ID="ddlCorte" runat="server" AutoPostBack="false" CssClass="combo combo-large" Enabled="false">
                        </asp:DropDownList>
                    </td>
                    <td class="label-form">
                        <asp:Label ID="lblReparto" runat="server">Reparto *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtCReparto" runat="server" CssClass="texto texto-2 texto-small" AutoPostBack="True" OnDisposed="txtCReparto_Disposed" OnTextChanged="txtCReparto_TextChanged"></asp:TextBox>
                        <asp:TextBox ID="txtReparto" runat="server" CssClass="texto texto-2 texto-medium" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblDescripcion" runat="server">Descripción *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="texto texto-2 texto-large"></asp:TextBox>
                    </td>
                    <td class="label-form">
                        <asp:Label ID="lblArchivo" runat="server">Archivo *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:FileUpload ID="txtArchivo" runat="server"></asp:FileUpload><br />
                        <asp:Label ID="lblArchivoCargado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <button id="btnProcesar" runat="server" type="button" class="btn btn-2 icon-cogs" title="Procesar" onserverclick="btnProcesar_Click"></button>
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


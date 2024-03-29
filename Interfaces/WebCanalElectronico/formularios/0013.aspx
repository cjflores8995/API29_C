﻿<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0013.aspx.cs" Inherits="formularios_0013" %>

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
                        <asp:Label ID="lblTipo" runat="server">Tipo Archivo *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="false" CssClass="combo combo-large">
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
                            <button id="btnCargar" runat="server" type="button" class="btn btn-2 icon-upload" title="Procesar" onserverclick="btnCargar_Click"></button>
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


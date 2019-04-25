<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0029.aspx.cs" Inherits="formularios_0029" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
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
                                <asp:Label ID="lblFechaInicio" runat="server">Fecha Inicio</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox required="true" ID="txtFechaInicio" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);" placeholder="dd/MM/YYYY"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-form">
                                <asp:Label ID="lblFechaFin" runat="server">Fecha Fin</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox required="true" ID="txtFechaFin" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);" placeholder="dd/MM/YYYY"></asp:TextBox>
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
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false"></asp:TextBox>
    </div>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0004.aspx.cs" Inherits="formularios_0004" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div>
            <asp:UpdatePanel ID="panelformulario" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="botonProcesar" OnClick="btnProcesar_Click" />
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
</asp:Content>


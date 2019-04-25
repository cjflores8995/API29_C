<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0018.aspx.cs" Inherits="formularios_0018" %>

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
                        <asp:Label ID="lblCodigo" runat="server">Código</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="texto texto-2 texto-large" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblNombre" runat="server">Nombre *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="texto texto-2 texto-large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-form">
                        <asp:Label ID="lblArchivo" runat="server">Archivo *</asp:Label>
                    </td>
                    <td class="field-form">
                        <asp:FileUpload ID="txtArchivo" runat="server"></asp:FileUpload>
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
        <div>
            <asp:GridView ID="gvDetalle" runat="server"
                AutoGenerateColumns="False"
                CssClass="tbl tbl-2"
                AllowPaging="True"
                OnPageIndexChanging="gvDetalle_PageIndexChanging"
                OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged"
                OnSelectedIndexChanging="gvDetalle_SelectedIndexChanging"
                PageSize="5">
                <PagerSettings FirstPageText="&lt;&lt;" />
                <RowStyle CssClass="filaNormal" />
                <PagerStyle CssClass="paginacion" />
                <AlternatingRowStyle CssClass="filaAlterna" />
                <SelectedRowStyle CssClass="filaSeleccionada" />
                <Columns>
                    <asp:BoundField DataField="CLISTA" HeaderText="Código">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:CommandField SelectImageUrl="~/img/iconos/seleccion.png" SelectText="" ShowSelectButton="True" ButtonType="Image" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false"></asp:TextBox>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0008.aspx.cs" Inherits="formularios_0008" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="900"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div class="formulario">
            <div>
                <asp:UpdatePanel ID="panelformulario" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <div>
                            <table class="=form-col3">
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblFechaProceso" runat="server">Fecha Proceso: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtfcontable" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblNumeroProceso" runat="server">Número Proceso: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtNumeroProceso" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblEstado" runat="server">Estado: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblDescripcion" runat="server">Descripción: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblTotalCreditos" runat="server">Total Créditos: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTotalCreditos" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblTotalDebitos" runat="server">Total Debitos: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTotalDebitos" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblTotal" runat="server">Total Registros: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblPendientes" runat="server">Reg. Pendientes: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtPendientes" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblCorrectos" runat="server">Reg. Correctos: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCorrectos" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblErrores" runat="server">Reg. Errores: </asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtErrores" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tfoot>
                                    <tr>
                                        <td colspan="6">
                                            <button id="btnVolver" runat="server" type="button" class="btn btn-2 icon-arrow-left" title="Regresar" onserverclick="btnVolver_Click"></button>
                                            <button id="btnActualizar" runat="server" type="button" class="btn btn-2 icon-refresh" title="Actualizar" onserverclick="btnActualizar_Click"></button>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="gvErrores" runat="server" AutoGenerateColumns="False" CssClass="tbl tbl-1" AllowPaging="True" OnPageIndexChanging="gvErrores_PageIndexChanging" PageSize="5">
                                <PagerSettings FirstPageText="&lt;&lt;" />
                                <RowStyle CssClass="filaNormal" />
                                <PagerStyle CssClass="paginacion" />
                                <AlternatingRowStyle CssClass="filaAlterna" />
                                <Columns>
                                    <asp:BoundField DataField="CError" HeaderText="Código Error">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Error" HeaderText="Descripción">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Valor" DataFormatString="{0:C}" HeaderText="Valor">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
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
    </div>
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false"></asp:TextBox>
    </div>
</asp:Content>


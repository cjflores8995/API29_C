<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0010.aspx.cs" Inherits="formularios_0010" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
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
                                        <asp:Label ID="lblFechaProceso" runat="server">Fecha Proceso</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtfcontable" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblConvenio" runat="server">Código Convenio</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCConvenio" runat="server" CssClass="texto texto-2 texto-small" Enabled="false" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtConvenio" runat="server" CssClass="texto texto-2 texto-medium" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblEstado" runat="server">Estado</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCEstado" runat="server" CssClass="texto texto-2 texto-small" Enabled="false" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="texto texto-2 texto-medium" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblCuenta" runat="server">Cuenta</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCuenta" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblDebito" runat="server">Referencia Debito</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtDebito" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblTransferencia" runat="server">Referencia Transferencia</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTransferencia" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblCompensados" runat="server">Compensados</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCompensados" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblRechazados" runat="server">Rechazados</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtRechazados" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblError" runat="server">Detalle Error</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtError" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblTransacciones" runat="server">Total Transacciones</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTransacciones" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblLiquidado" runat="server">Total Liquidado</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtLiquidado" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblComision" runat="server">Total Comisión</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtComision" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tfoot>
                                    <tr>
                                        <td colspan="6">
                                            <button id="btnVolver" runat="server" type="button" class="btn btn-2 icon-arrow-left" title="Regresar" onserverclick="btnVolver_Click"></button>
                                            <button id="btnCancelarArchivo" runat="server" type="button" class="btn btn-2 icon-ban" title="Cancelar por archivo inexistente" onserverclick="btnCancelarArchivo_ServerClick"></button>
                                            <button id="btnActualizar" runat="server" type="button" class="btn btn-2 icon-refresh" title="Actualizar" onserverclick="btnActualizar_Click"></button>
                                            <button id="btnReporte" runat="server" type="button" class="btn btn-2 icon-file" title="Reporte" onserverclick="btnReporte_Click"></button>
                                            <a id="lnkReporte" runat="server" href="#" target="_blank" visible="false" class="lnk lnk-1 icon-download" title="Descargar"></a>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" CssClass="tbl tbl-1" AllowPaging="True" OnPageIndexChanging="gvDetalle_PageIndexChanging" PageSize="5">
                                <PagerSettings FirstPageText="&lt;&lt;" />
                                <RowStyle CssClass="filaNormal" />
                                <PagerStyle CssClass="paginacion" />
                                <AlternatingRowStyle CssClass="filaAlterna" />
                                <Columns>
                                    <asp:BoundField DataField="FTRANSACCION" HeaderText="Fecha Transacción">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TARJETA" HeaderText="Tarjeta">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CCUENTA" HeaderText="Cuenta">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMEROTRANSACCION" HeaderText="# Trans.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMEROAPROBACION" HeaderText="# Autori.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VALORTRANSACCION" HeaderText="V. Trans.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VALORCOMISION" HeaderText="V. Comi.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VALORLIQUIDADO" HeaderText="V. Liqui.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Estado" HeaderText="Estado">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
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


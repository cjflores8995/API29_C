<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0011.aspx.cs" Inherits="formularios_0011" %>

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
                        <table class="form-col1">
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblConvenio" runat="server">Convenio: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:DropDownList ID="ddlConvenio" runat="server" AutoPostBack="false" CssClass="combo combo-large">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tfoot>
                                <tr>
                                    <td colspan="4">
                                        <button id="btnBuscar" runat="server" type="button" class="btn btn-2 icon-search" title="Buscar" onserverclick="btnBuscar_Click"></button>
                                        <button id="btnLimpiar" runat="server" type="button" class="btn btn-2 icon-magic" title="Buscar" onserverclick="btnLimpiar_Click"></button>
                                        <button id="btnAutorizar" runat="server" type="button" class="btn btn-2 icon-check" title="Buscar" onserverclick="btnAutorizar_Click"></button>
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
            <div>
                <asp:UpdatePanel ID="panelTabla" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 100%; float: left;">
                            <asp:GridView ID="gvProcesos" runat="server" AutoGenerateColumns="False" CssClass="tbl tbl-1" AllowPaging="True" OnPageIndexChanging="gvProcesos_PageIndexChanging" PageSize="5">
                                <PagerSettings FirstPageText="&lt;&lt;" />
                                <RowStyle CssClass="filaNormal" />
                                <PagerStyle CssClass="paginacion" />
                                <AlternatingRowStyle CssClass="filaAlterna" />
                                <Columns>
                                    <asp:BoundField DataField="FPROCESO" HeaderText="Fecha">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CConvenio" HeaderText="Convenio">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Compensados" HeaderText="Compensados">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rechazados" HeaderText="Rechazados">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalTransaccion" HeaderText="T. Transacción" DataFormatString="{0:C}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalLiquidado" HeaderText="T. Liquidado" DataFormatString="{0:C}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalComision" HeaderText="T. Comisión" DataFormatString="{0:C}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container.DataItem, "Link", "{0}") %>' class="lnk lnk-2  icon-table22"></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Autorizar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAutorizar" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="ServerClick" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false"></asp:TextBox>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0007.aspx.cs" Inherits="formularios_0007" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

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
                        <table class="form-col3">
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblFechaDesde" runat="server">Fecha Desde: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);"></asp:TextBox>
                                </td>
                                <td class="label-form">
                                    <asp:Label ID="lblFechaHasta" runat="server">Fecha Hasta: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);"></asp:TextBox>
                                </td>
                                <td class="label-form">
                                    <asp:Label ID="lblNumeroProceso" runat="server">Número Proceso: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtNumeroProceso" runat="server" CssClass="texto texto-2 texto-large" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <button id="btnBuscar" runat="server" type="button" class="btn btn-2 icon-search" title="Buscar" onserverclick="btnBuscar_Click"></button>
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
                                    <asp:BoundField DataField="FPROCESO" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPROCESO" HeaderText="Proceso">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FINALIZADOS" HeaderText="FINALIZADOS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PENDIENTES" HeaderText="PENDIENTES">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CORRECTOS" HeaderText="CORRECTOS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ERRORES" HeaderText="ERRORES">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container.DataItem, "link", "{0}") %>' class="lnk lnk-2 icon-table22"></a>
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

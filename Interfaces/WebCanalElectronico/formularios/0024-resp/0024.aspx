<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0024.aspx.cs" Inherits="formularios_0024" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:ScriptManager ID="managerFormulario" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout="9000000"></asp:ScriptManager>
    <div id="form-title">
        <asp:PlaceHolder ID="phTitulo" runat="server"></asp:PlaceHolder>
    </div>
    <div id="form-body">
        <div class="formulario">
            <div>
                <asp:UpdatePanel ID="panelformulario" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <table class="form-col2">
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblIdentificacion" runat="server">Identificación: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="texto texto-2 texto-large"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblTipoComprobante" runat="server">Tipo comprobante: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" AutoPostBack="false" CssClass="combo combo-large">
                                    </asp:DropDownList>
                                </td>
                                <td class="label-form">
                                    <asp:Label ID="lblComprobante" runat="server">Número comprobante: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtComprobante" runat="server" CssClass="texto texto-2 texto-large" ToolTip="000-000-000000000"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblFechaDesde" runat="server">Fecha desde: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);"></asp:TextBox>
                                </td>
                                <td class="label-form">
                                    <asp:Label ID="lblFechaHasta" runat="server">Fecha hasta: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="texto texto-2 texto-large" onKeyUp="this.value=formateafecha(this.value);"></asp:TextBox>
                                </td>
                            </tr>
                            <tfoot>
                                <tr>
                                    <td colspan="4">
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
                            <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" CssClass="tbl tbl-1" AllowPaging="True" OnPageIndexChanging="GridView_PageIndexChanging" PageSize="5">
                                <PagerSettings FirstPageText="&lt;&lt;" />
                                <RowStyle CssClass="filaNormal" />
                                <PagerStyle CssClass="paginacion" />
                                <AlternatingRowStyle CssClass="filaAlterna" />
                                <Columns>
                                    <asp:BoundField DataField="TIPODOCUMENTO" HeaderText="Tipo Comprobante">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMERODOCUMENTO" HeaderText="Número Comprobante">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOCUMENTOSUSTENTO" HeaderText="Documento Sustento">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FEMISION" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Emisión">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMEROAUTORIZACION" HeaderText="Número Autorización">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="PDF">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="upnPdf" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkPdf" runat="server" OnClick="DescargaComprobante" CommandArgument='<%# Eval("linkPdf") %>' CssClass="lnk lnk-2  icon-file-pdf-o"></asp:LinkButton>&nbsp;&nbsp;
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkPdf" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="XML">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="upnXml" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkXml" runat="server" OnClick="DescargaComprobante" CommandArgument='<%# Eval("linkXml") %>' CssClass="lnk lnk-2 icon-file-code-o"></asp:LinkButton>&nbsp;&nbsp;
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkXml" />
                                                </Triggers>
                                            </asp:UpdatePanel>
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
        <a id="lnkDescargar" runat="server" href="http://localhost/canalelectronico/ingreso.aspx" target="_blank" visible="false" class="lnk lnk-1 icon-download" title="Descargar"></a>
    </div>
</asp:Content>


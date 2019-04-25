<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0022.aspx.cs" Inherits="formularios_0022" %>

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
                                <asp:Label ID="lblIdentificacion" runat="server">Identificación:</asp:Label>
                            </td>
                            <td class="field-form">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="texto texto-2 texto-large" Width="350px"></asp:TextBox>
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
                                <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INSTITUCION" HeaderText="Institución">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MONTO" HeaderText="Monto" DataFormatString="{0:C}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COMPROBANTE" HeaderText="Comprobante">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Reimprimir">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upnAux1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnk" runat="server" OnClick="DescargaComprobante" CommandArgument='<%# Eval("lnk") %>' CssClass="lnk lnk-2 icon-file-text2"></asp:LinkButton>&nbsp;&nbsp;
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnk" />
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
    <div id="popup" style="display: none;"></div>
    <div class="popup-overlay"></div>
    <div id="oculto">
        <asp:TextBox runat="server" ID="txtTitulo" Visible="false" Enabled="false" ReadOnly="true"></asp:TextBox>
    </div>
</asp:Content>


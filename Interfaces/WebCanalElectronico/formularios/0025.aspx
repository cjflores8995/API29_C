<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0025.aspx.cs" Inherits="formularios_0025" %>

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
                        <table class="form-col4">
                            <tr>
                                <td class="label-form">
                                    <asp:Label ID="lblNumeroProceso" runat="server">Número Proceso: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtNumeroProceso" runat="server" CssClass="texto texto-2 texto-large" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                                <td class="label-form">
                                    <asp:Label ID="lblCodigoUsuario" runat="server">Usuario: </asp:Label>
                                </td>
                                <td class="field-form">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="texto texto-2 texto-large" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <button id="btnBuscar" runat="server" type="button" class="btn btn-2 icon-search" title="Buscar" onserverclick="btnBuscar_Click"></button>
                                        <button id="btnLimpiar" runat="server" type="button" class="btn btn-2 icon-magic" title="Buscar" onserverclick="btnLimpiar_Click"></button>
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
                            <asp:GridView ID="gvProcesos" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="tbl tbl-1"
                                AllowPaging="True"
                                OnPageIndexChanging="gvProcesos_PageIndexChanging"
                                PageSize="5">
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
                                    <asp:BoundField DataField="TIPOPROCESO" HeaderText="Tipo Proceso">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CUSUARIO" HeaderText="Usuario">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Autorizar">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container.DataItem, "link1", "{0}") %>' class="lnk lnk-2  icon-table22"></a>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/formularios/MasterEntorno.master" AutoEventWireup="true" CodeFile="0017.aspx.cs" Inherits="formularios_0017" %>

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
                                        <asp:TextBox ID="txtFechaProceso" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblNumeroProceso" runat="server">Número Proceso</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtNumeroProceso" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="Label1" runat="server">Tipo Proceso</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTipoProceso" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblEstado" runat="server">Estado</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblCorte" runat="server">Corte</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtCorte" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblDescripcion" runat="server">Descripción</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblTipo" runat="server">Formato Archivo</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtFormatoArchivo" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblReparto" runat="server">Reparto</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtReparto" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblUsuario" runat="server">Usuario</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblFCarga" runat="server">Fecha Carga</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtFCarga" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblError" runat="server">Error</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtError" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-form">
                                        <asp:Label ID="lblRegistros" runat="server"># Registros</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtRegistros" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="label-form">
                                        <asp:Label ID="lblTotal" runat="server">Total</asp:Label>
                                    </td>
                                    <td class="field-form">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="texto texto-2 texto-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tfoot>
                                    <tr>
                                        <td colspan="8">
                                            <button id="btnVolver" runat="server" type="button" class="btn btn-2 icon-arrow-left" title="Regresar" onserverclick="btnVolver_Click"></button>
                                            <button id="btnActualizar" runat="server" type="button" class="btn btn-2 icon-refresh" title="Actualizar" onserverclick="btnActualizar_Click"></button>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="gridErrores" runat="server" AutoGenerateColumns="False" CssClass="tbl tbl-1" AllowPaging="True" OnPageIndexChanging="gridErrores_PageIndexChanging" PageSize="5">
                                <PagerSettings FirstPageText="&lt;&lt;" />
                                <RowStyle CssClass="filaNormal" />
                                <PagerStyle CssClass="paginacion" />
                                <AlternatingRowStyle CssClass="filaAlterna" />
                                <Columns>
                                    <asp:BoundField DataField="CERROR" HeaderText="Código">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DERROR" HeaderText="Descripción">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REGISTROS" HeaderText="Registros">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VALOR" DataFormatString="{0:C}" HeaderText="Total">
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
        <asp:TextBox runat="server" ID="txtSpi3" Visible="false"></asp:TextBox>
    </div>
</asp:Content>

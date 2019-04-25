<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLogin.master" AutoEventWireup="true" CodeFile="ingreso.aspx.cs" Inherits="ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <p>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="texto texto-1" Width="250px" MaxLength="20"
                placeholder="Usuario" AutoCompleteType="None"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="txtClave" runat="server" CssClass="texto texto-1" Width="250px" MaxLength="20"
                TextMode="Password" placeholder="Clave" AutoCompleteType="None"></asp:TextBox>
        </p>
        <p>
            <button id="btnIngresar" class="btn btn-1 icon-user" runat="server" onserverclick="btnIngresar_Click">Ingresar</button>
        </p>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contrasena.aspx.cs" Inherits="UI.Contrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Contacto/Styles/StyleContacto.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container2">
        <h1>Ingrese su mail</h1>
        <div>
            <asp:TextBox ID="txtMail" class="input" TextMode="Email" placeholder="Ingrese su mail" runat="server" />
            <asp:Button ID="btnRecuperar" class="btnEnviar" Text="Recuperar contraseña" OnClick="btnRecuperar_Click" runat="server" />
        </div>
    </div>
</asp:Content>

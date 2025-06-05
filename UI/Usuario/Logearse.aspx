<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Logearse.aspx.cs" Inherits="UI.Logearse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Contacto/Styles/StyleContacto.css" rel="stylesheet" />
    <script src="/Usuario/Js/JsLogearse.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container2 needs-validation" novalidate="false">
            <h1>Inicie sesión</h1>
            <div>
                <asp:TextBox ID="txtMail" class="input" TextMode="Email" ClientIDMode="Static"  placeholder="Mail" runat="server"/>
                <small id="mailErrorMsj" class="text-danger"></small>
                <small id="mailCorrectoMsj" class="valid-feedback"></small>
                <asp:TextBox ID="txtPass" class="input" TextMode="Password" ClientIDMode="Static" placeholder="Contraseña" runat="server" />
                <small id="contrasenaCorrectaMsj" class="valid-feedback"></small>
                <small id="contrasenaErrorMsj" class="text-danger"></small>
                <asp:Button ID="BtnEnviar" OnClientClick="return validarCampos()" class="BtnEnviar" Text="Iniciar sesión" OnClick="BtnEnviar_Click" runat="server" />
            </div>
            <div>
                <span>Si olvidó su contraseña </span>
                <a href="/Usuario/Contrasena.aspx">Haga click aquí</a>
                <p></p>
            </div>
            <div>
                <span>Si todavía no tiene una cuenta </span>
                <a href="/Registro.aspx">Registrese aquí</a>
            </div>
        </div>
    </main>
</asp:Content>

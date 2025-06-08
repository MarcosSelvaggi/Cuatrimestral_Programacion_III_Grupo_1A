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
                <asp:TextBox ID="txtMail" class="input" TextMode="Email" ClientIDMode="Static" placeholder="Mail" runat="server" />
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
        <div class="modal fade" id="contraseñaIncorrectaModal" tabindex="-1" aria-labelledby="contraseñaIncorrectaModal" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-dialog" style="min-width: 400px; width:90%">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h1 class="modal-title fs-5" id="contraseñaIncorrectaModalH1" runat="server">❌ Ocurrió un error</h1>
                        </div>
                        <div class="modal-body">
                            <p id="pDatosErroneosModal" runat="server">Mail o contraseña incorrectos</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-dark ms-auto" data-bs-dismiss="modal">Entendido</button>
                            <div class="col-4"></div> <!--Horrible, pero funciona-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>

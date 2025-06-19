<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="UI.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://emoji-css.afeld.me/emoji.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-2">
            </div>
            <div class="col-6">
                <div class="row" id="datosUsuario" runat="server">
                    <div class="col-md-4">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-4">
                        <label for="inputPassword4" class="form-label">Documento</label>
                        <asp:TextBox ID="txtDocumento" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputEmail4" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputEmail4" class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputCity" class="form-label">Provincia</label>
                        <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputCity" class="form-label">Localidad</label>
                        <asp:TextBox ID="txtLocalidad" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputAddress" class="form-label">Dirección</label>
                        <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label for="inputZip" class="form-label">Código postal</label>
                        <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                </div>
                <asp:Button ID="BtnModificarDatos" OnClick="BtnModificarDatos_Click" Text="Modificar datos" CssClass="btn btn-outline-dark " runat="server" Visible="false" />
            </div>
            <div class="col-3" id="menuOpciones">
                <ul class="list-group list-group-flush">
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/Carrito.aspx">Carrito</a>
                        <i class="em em-shopping_trolley" aria-role="presentation" aria-label="SHOPPING TROLLEY"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/Favoritos.aspx">Favoritos</a>
                        <i class="em em-star" aria-role="presentation" aria-label="WHITE MEDIUM STAR"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Productos.aspx">Revisar productos</a>
                        <i class="em em-video_game" aria-role="presentation" aria-label="VIDEO GAME"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Perfil.aspx?datos=1">Modificar datos</a>
                        <i class="em em-page_with_curl" aria-role="presentation" aria-label="PAGE WITH CURL"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/cambiarcontrasena.aspx">Cambiar contraseña</a>
                        <i class="em em-unlock" aria-role="presentation" aria-label="OPEN LOCK"></i>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="modal fade" id="datosCargadosIncorrectamenteModal" tabindex="-1" aria-labelledby="datosCargadosIncorrectamenteModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog" style="min-width: 400px; width: 90%">
                <div class="modal-content">
                    <div class="modal-header bg-danger text-white">
                        <h1 class="modal-title fs-5" id="datosCargadosIncorrectamenteH1" runat="server">❌ Ocurrió un error</h1>
                    </div>
                    <div class="modal-body">
                        <p id="pDatosErroneosModal" runat="server">No se puede modificar el perfil con datos vacíos</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-dark ms-auto" data-bs-dismiss="modal">Entendido</button>
                        <div class="col-4"></div>
                        <!--Horrible, pero funciona-->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="datosCambiadosCorrectamenteModal" tabindex="-1" aria-labelledby="datosCambiadosCorrectamenteModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog" style="min-width: 400px; width: 90%">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h1 class="modal-title fs-5" runat="server">Datos actualizados</h1>
                    </div>
                    <div class="modal-body">
                        <p>Se ha actualizado tu información personal</p>
                        <p>En unos segundos serás redirigido a tu perfil.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="/Perfil.aspx" class="btn btn-dark ms-auto">Ir ahora</a>
                        <div class="col-4"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="UI.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://emoji-css.afeld.me/emoji.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-2"></div>
            <div class="col-6" id="datosUsuario">
                <div class="row">
                    <div class="col-md-5">
                        <label class="form-label">Nombre</label>
                        <input type="text" class="form-control" name="nombre" value="<%=UsuarioLogeado.Nombre %>" disabled />
                    </div>
                    <div class="col-md-5">
                        <label class="form-label">Apellido</label>
                        <input type="text" class="form-control" name="apellido" value="<%=UsuarioLogeado.Apellido %>" disabled />
                    </div>
                    <div class="col-md-2">
                        <label for="inputPassword4" class="form-label">Documento</label>
                        <input type="text" class="form-control" name="documento" value="<%=UsuarioLogeado.Documento %>" disabled>
                    </div>
                    <div class="col-md-6">
                        <label for="inputEmail4" class="form-label">Email</label>
                        <input type="text" class="form-control" name="mail" value="<%=UsuarioLogeado.Email %>" disabled />
                    </div>
                    <div class="col-md-6">
                        <label for="inputEmail4" class="form-label">Teléfono</label>
                        <input type="text" class="form-control" name="telefono" value="<%=UsuarioLogeado.Telefono %>" disabled />
                    </div>
                    <div class="col-md-6">
                        <label for="inputAddress" class="form-label">Dirección</label>
                        <input type="text" class="form-control" name="direccion" value="<%=UsuarioLogeado.Direccion %>" disabled>
                    </div>
                    <div class="col-md-6">
                        <label for="inputCity" class="form-label">Provincia</label>
                        <input type="text" class="form-control" name="provincia" value="<%=UsuarioLogeado.Provincia%>" disabled>
                    </div>
                    <div class="col-md-6">
                        <label for="inputCity" class="form-label">Localidad</label>
                        <input type="text" class="form-control" name="localidad" value="<%=UsuarioLogeado.Localidad %>" disabled>
                    </div>
                    <div class="col-md-6">
                        <label for="inputZip" class="form-label">Código postal</label>
                        <input type="text" class="form-control" name="codPostal" value="<%=UsuarioLogeado.CodigoPostal%>" disabled>
                    </div>
                </div>
            </div>
            <div class="col-3" id="menuOpciones">
                <ul class="list-group list-group-flush">
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/Carrito.aspx">Carrito</a>
                        <i class="em em-shopping_trolley" aria-role="presentation" aria-label="SHOPPING TROLLEY"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/Favoritos.aspx">Favoritos</a>
                        <i class="em em-star" aria-role="presentation" aria-label="WHITE MEDIUM STAR"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Usuario/Cambiascontrasena.apx">Cambiar contraseña</a> 
                        <i class="em em-unlock" aria-role="presentation" aria-label="OPEN LOCK"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="/Productos.aspx">Revisar productos</a>
                        <i class="em em-video_game" aria-role="presentation" aria-label="VIDEO GAME"></i>
                    </li>
                    <li class="list-group-item list-group-item-action"><a href="#">Cambiar datos</a>
                        <i class="em em-page_with_curl" aria-role="presentation" aria-label="PAGE WITH CURL"></i>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="UI.Contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Contacto/Styles/StyleContacto.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="container2">
                    <h1>Deje su consulta y nos contactaremos via mail</h1>
                    <div>
                        <asp:TextBox ID="txtNombre" class="input" placeholder="Ingrese su nombre" runat="server" />
                        <asp:TextBox ID="txtMail" class="input" TextMode="Email" placeholder="Ingrese su mail" runat="server" />
                        <asp:TextBox ID="txtConsulta" class="textarea" TextMode="MultiLine" placeholder="Ingrese su consulta" Style="max-height: 170px; min-height: 170px" runat="server" />
                        <asp:Button class="btnEnviar" ID="btnEnviarConsulta" Text="Enviar" OnClick="btnEnviarConsulta_Click" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col">
                <h2 style="font-weight: 400">También puede encontrarnos en</h2>
                <img src="/Contacto/Imagenes/imgUbicacion.jpg" style="max-width: 400px; height: auto" alt="Ubicación de la tienda" />
                <h3>Simón de Iriondo 1164, Victoria</h3>
                <h3>Lunes a viernes de 8:00 a 19:00</h3>
                <h3>Nuestro número es: +54 1111 1111</h3>
            </div>
        </div>
    </div>

    <div class="modal fade" id="contactoExitosoModal" tabindex="-1" aria-labelledby="contactoExitosoModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog" style="min-width: 400px; width: 90%">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h1 class="modal-title fs-5" runat="server">Se ha registrado su consulta</h1>
                    </div>
                    <div class="modal-body">
                        <p>Se ha registrado exitosamente su consulta.</p>
                        <p>Se envió una copia de la consulta via mail.</p>
                        <p>Nos contactaremos via mail con usted en la brevedad.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="/Inicio.aspx" class="btn btn-dark ms-auto">Ir al inicio</a>
                        <div class="col-4"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

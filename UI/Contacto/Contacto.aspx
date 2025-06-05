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
                        <asp:TextBox class="input" placeholder="Ingrese su nombre" runat="server" />
                        <asp:TextBox class="input" TextMode="Email" placeholder="Ingrese su mail" runat="server" />
                        <asp:TextBox class="textarea" TextMode="MultiLine" placeholder="Ingrese su consulta" Style="max-height: 170px; min-height: 170px" runat="server" />
                        <asp:Button class="btnEnviar" Text="Enviar" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col">
                <h1>También puede encontrarnos en</h1>
                <img src="https://media.discordapp.net/attachments/768321868162662511/1380197429420490764/image.png?ex=68430086&is=6841af06&hm=c5087e2e3ecbf35419cb49734c05318b226139006572bc1833c9e4e23ac299b6&=&format=webp&quality=lossless" style="max-width:400px; height:auto" alt="Ubicación de la tienda" />
                <h3>Av.Rivera Indarte y Av.Perito Moreno</h3>
                <h3>Lunes a viernes de 8:00 a 19:00</h3>
                <h3>Nuestro número es: +54 1111 1111</h3>
            </div>
        </div>
    </div>

</asp:Content>

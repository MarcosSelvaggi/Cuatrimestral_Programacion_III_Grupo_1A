<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="UI.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container d-flex justify-content-center align-items-start pt-4">
        <div class="card shadow p-4 bg-dark text-white border-light" style="max-width: 700px; width: 100%;">
            <h3 class="text-center mb-4"><i class="bi bi-caret-right-fill"></i> Registro <i class="bi bi-caret-left-fill"></i></h3>
            <div class="row g-3">

                <div class="col-md-6">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Apellido</label>
                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <div class="input-group">
                        <span class="input-group-text bg-secondary text-white">@</span>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Teléfono</label>
                    <div class="input-group">
                        <span class="input-group-text bg-secondary text-white">+54</span>
                        <asp:TextBox ID="txtTeléfono" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Número de Documento</label>
                    <asp:TextBox ID="txtDocumento" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDirección" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-6">
                </div>

                <div class="col-md-4">
                    <label class="form-label">Provincia</label>
                    <asp:DropDownList ID="ddlProvincia" CssClass="form-select" runat="server">
                        <asp:ListItem Text="Opciones" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Buenos Aires" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Cordoba" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Localidad</label>
                    <asp:DropDownList ID="ddlLocalidad" CssClass="form-select" runat="server">
                        <asp:ListItem Text="Opciones" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Catabarumba" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Narnia" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Código Postal</label>
                    <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-12 text-center mt-4">
                    <asp:Button ID="btnRegistro" runat="server" Text="Registrarse" CssClass="btn btn-light px-5" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
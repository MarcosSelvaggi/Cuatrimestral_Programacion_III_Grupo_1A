<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="UI.Usuario.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2><i class="bi bi-archive-fill"></i> Mis Pedidos</h2>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Pedidos</th>
                    <th>Fecha</th>
                    <th>Estado</th>
                    <th>Total</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>2025-05-01</td>
                    <td><span class="badge bg-success">Entregado</span></td>
                    <td>$145.000</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-secondary">Reordenar</button>
                    </td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>2025-05-20</td>
                    <td><span class="badge bg-warning text-dark">En proceso</span></td>
                    <td>$20.000</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-secondary">Reordenar</button>
                    </td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>2025-05-20</td>
                    <td><span class="badge bg-warning text-dark">En proceso</span></td>
                    <td>$90.000</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-secondary">Reordenar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

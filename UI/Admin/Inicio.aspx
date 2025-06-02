<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UI.Admin.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="mt-4">Últimas Ventas</h1>
    <div class="card mb-4">
        <div class="card-header">
            <i class="fas fa-list me-1"></i>
            Ventas recientes
        </div>
        <div class="card-body">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Cliente</th>
                        <th>Producto</th>
                        <th>Fecha</th>
                        <th>Monto</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Ana García</td>
                        <td>Mochila Gamer</td>
                        <td>2025-05-30</td>
                        <td>$1200.00</td>
                    </tr>
                    <tr>
                        <td>Juan Pérez</td>
                        <td>Teclado Mecánico RGB</td>
                        <td>2025-05-29</td>
                        <td>$4500.00</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
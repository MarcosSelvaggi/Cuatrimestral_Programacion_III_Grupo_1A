<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionPedidos.aspx.cs" Inherits="UI.Admin.GestionPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2><i class="bi bi-receipt-cutoff"></i> Gestión de Pedidos</h2>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>N° Pedido</th>
                    <th>Cliente</th>
                    <th>Fecha</th>
                    <th>Estado</th>
                    <th>Total</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>5001</td>
                    <td>Juan Pérez</td>
                    <td>2025-05-01</td>
                    <td><span class="badge bg-success">Entregado</span></td>
                    <td>$145.000</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-warning">Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
                <tr>
                    <td>5002</td>
                    <td>Ana García</td>
                    <td>2025-05-20</td>
                    <td><span class="badge bg-warning text-dark">En proceso</span></td>
                    <td>$90.000</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-warning">Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
                <tr>
                    <td>5003</td>
                    <td>Carlos Núñez</td>
                    <td>2025-06-01</td>
                    <td><span class="badge bg-danger">Cancelado</span></td>
                    <td>$0</td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver detalle</button>
                        <button class="btn btn-sm btn-warning" disabled>Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

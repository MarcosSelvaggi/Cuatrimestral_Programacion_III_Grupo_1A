<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="UI.Admin.GestionProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2><i class="bi bi-box-seam-fill"></i>Gestión de Productos</h2>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Categoría</th>
                    <th>Precio</th>
                    <th>Stock</th>
                    <th>Activo</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Monitor 24"</td>
                    <td>Electrónica</td>
                    <td>$120.000</td>
                    <td>15</td>
                    <td><span class="badge bg-success">Sí</span></td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver</button>
                        <button class="btn btn-sm btn-warning">Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
                <tr>
                    <td>Teclado Mecánico</td>
                    <td>Accesorios</td>
                    <td>$35.000</td>
                    <td>50</td>
                    <td><span class="badge bg-success">Sí</span></td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver</button>
                        <button class="btn btn-sm btn-warning">Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
                <tr>
                    <td>Silla Gamer</td>
                    <td>Muebles</td>
                    <td>$200.000</td>
                    <td>5</td>
                    <td><span class="badge bg-danger">No</span></td>
                    <td>
                        <button class="btn btn-sm btn-info">Ver</button>
                        <button class="btn btn-sm btn-warning">Editar</button>
                        <button class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

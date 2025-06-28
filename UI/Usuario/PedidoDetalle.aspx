<%@ Page Title="Detalle de Pedido" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="PedidoDetalle.aspx.cs" Inherits="UI.Usuario.PedidoDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3><i class="bi bi-backpack4"></i> Detalle Pedido</h3>

    <div class="card mb-3">
        <div class="card-header bg-black text-white">
            Datos del Cliente
        </div>
        <div class="card-body">
            <p><strong>Cliente:</strong> <%= UsuarioPedido.Nombre %> <%= UsuarioPedido.Apellido %></p>
            <p><strong>Documento:</strong> <%= UsuarioPedido.Documento %></p>
            <p><strong>Dirección:</strong> <%= UsuarioPedido.Direccion %></p>
            <p><strong>Localidad:</strong> <%= UsuarioPedido.Localidad %></p>
            <p><strong>Provincia:</strong> <%= UsuarioPedido.Provincia %></p>
            <p><strong>Código Postal:</strong> <%= UsuarioPedido.CodigoPostal %></p>
            <p><strong>Teléfono:</strong> <%= UsuarioPedido.Telefono %></p>
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header bg-black text-white">
            Datos del Pedido
        </div>
        <div class="card-body">
            <p><strong>N° Pedido:</strong> <%= PedidoSeleccionado.IdPedido %></p>
            <p><strong>Fecha:</strong> <%= PedidoSeleccionado.FechaPedido.ToString("dd/MM/yyyy HH:mm") %></p>
            <p><strong>Estado del Pedido:</strong> <%= PedidoSeleccionado.EstadoPedido.Descripcion %></p>
            <p><strong>Estado de Envío:</strong> <%= PedidoSeleccionado.EstadoEnvio.Descripcion %></p>
            <p><strong>Total:</strong> $<%= PedidoSeleccionado.PrecioTotal.ToString("N2") %></p>
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header bg-black text-white">
            Detalle del Pago
        </div>
        <div class="card-body">
            <p><strong>Método de Pago:</strong> <%= PedidoSeleccionado.DetallePago.Metodo %></p>
            <p><strong>Fecha de Pago:</strong> <%= PedidoSeleccionado.DetallePago.Fecha.ToString("dd/MM/yyyy") %></p>
            <p><strong>Estado del Pago:</strong> <%= PedidoSeleccionado.DetallePago.Estado %></p>
            <p><strong>Detalles:</strong> <%= PedidoSeleccionado.DetallePago.Descripcion %></p>
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header bg-black text-white">
            Productos
        </div>
        <div class="card-body p-0">
            <table class="table mb-0">
                <thead class="table-light">
                    <tr>
                        <th>Producto</th>
                        <th>Cantidad</th>
                        <th>Precio Unitario</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var detalle in PedidoSeleccionado.ListaDetalles)
                        { %>
                    <tr>
                        <td><%= detalle.Producto.Nombre %></td>
                        <td><%= detalle.Cantidad %></td>
                        <td>$<%= detalle.PrecioUnitario.ToString("N2") %></td>
                        <td>$<%= detalle.Subtotal.ToString("N2") %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
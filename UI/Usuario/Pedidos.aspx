<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="UI.Usuario.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3><i class="bi bi-clock-history"></i> Mis Pedidos</h3>
    <asp:Repeater ID="rptPedidos" runat="server">
        <HeaderTemplate>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Pedido</th>
                        <th>Fecha</th>
                        <th>Estados</th>
                        <th>Total</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("IdPedido") %></td>
                <td><%# Eval("FechaPedido") %></td>
                <td>
                    <span class="badge btn btn-info">
                        <%# Eval("EstadoPedido.Descripcion") %>
                    </span>
                    <span class="badge btn btn-info">
                        <%# Eval("EstadoPago.Descripcion") %>
                    </span>
                </td>
                <td>$<%# Eval("PrecioTotal", "{0:N2}") %></td>
                <td>
                    <a href='<%# "PedidoDetalle.aspx?id=" + Eval("IdPedido") %>' class="btn btn-sm btn-info">Ver detalle</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>

<%@ Page Title="Admin - Pedidos" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionPedidos.aspx.cs" Inherits="UI.Admin.GestionPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2><i class="bi bi-receipt-cutoff"></i>Gestión de Pedidos</h2>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>N° Pedido</th>
                    <th>Cliente</th>
                    <th>Fecha</th>
                    <th>Estado Pedido</th>
                    <th>Estado Envío</th>
                    <th>Total</th>
                    <th style="text-align: right;">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPedidos" runat="server" OnItemCommand="rptPedidos_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IdPedido") %></td>
                            <td><%# Eval("Cliente") %></td>
                            <td><%# Eval("FechaPedido", "{0:dd-MM-yyyy HH:mm}") %></td>
                            <td><%# Eval("EstadoPedido.Descripcion") %></td>
                            <td><%# Eval("EstadoEnvio.Descripcion") %></td>
                            <td>$<%# Eval("PrecioTotal", "{0:N2}") %></td>
                            <td style="text-align: right;">
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-primary me-1"
                                    CommandName="Ver" CommandArgument='<%# Eval("IdPedido") %>' UseSubmitBehavior="false">
                <i class="fas fa-eye"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-success me-1"
                                    CommandName="Editar" CommandArgument='<%# Eval("IdPedido") %>' UseSubmitBehavior="false">
                <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("IdPedido") %>' UseSubmitBehavior="false">
                <i class="fas fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <div class="modal fade" id="modalDetallePedido" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalle Pedido</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <h6>Detalle de Pedido</h6>
                        <asp:Label ID="lblDetallePedido" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="mb-3">
                        <h6>Detalle de Pago</h6>
                        <asp:Label ID="lblDetallePago" runat="server" Text=""></asp:Label>
                    </div>
                    <table class="table table-sm">
                        <thead>
                            <tr>
                                <th>Producto</th>
                                <th>Cantidad</th>
                                <th>Precio Unitario</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDetallePedido" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Producto.Nombre") %></td>
                                        <td><%# Eval("Cantidad") %></td>
                                        <td>$<%# Eval("PrecioUnitario", "{0:N2}") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEditarEstado" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar Estado de Pedido</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblIdPedidoEditar" runat="server" CssClass="d-none"></asp:Label>
                    <div class="mb-3">
                        <label>Estado de Pedido:</label>
                        <asp:DropDownList ID="ddlEstadoPedido" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoPedido_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Estado de Envío:</label>
                        <asp:DropDownList ID="ddlEstadoEnvio" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Método de Pago:</label>
                        <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select" Enabled="false"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Estado de Pago:</label>
                        <asp:DropDownList ID="ddlEstadoPago" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarEstado" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarEstado_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminarPedido" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmar Eliminación</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblDescripcionEliminarPedido" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblIdEliminarPedido" runat="server" CssClass="d-none"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarEliminarPedido" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminarPedido_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

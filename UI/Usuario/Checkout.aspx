<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="UI.Usuario.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h1><i class="bi bi-bag-check-fill"></i>Checkout</h1>

        <div class="row">
            <div class="col-md-6">
                <h4><i class="bi bi-rocket-takeoff-fill"></i>Datos de Envío</h4>
                <div id="fs">
                    <div class="mb-3">
                        <label class="form-label">Documento</label>
                        <input type="text" class="form-control" id="iptDocu" value="<%=UsuarioLogeado.Documento%>" disabled />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Nombre Completo</label>
                        <input type="text" class="form-control" id="iptNombre" value="<%=UsuarioLogeado.Nombre + " " + UsuarioLogeado.Apellido%>" disabled />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Direccion</label>
                        <input type="text" class="form-control" id="iptDireccion" value="<%= UsuarioLogeado.Direccion + ", " + UsuarioLogeado.Localidad + ", " + UsuarioLogeado.Provincia%>" disabled />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Codigo Postal</label>
                        <input type="text" class="form-control" id="iptCodigoPostal" value="<%=UsuarioLogeado.CodigoPostal%>" disabled />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Teléfono</label>
                        <input type="tel" class="form-control" id="iptTelefono" value="<%=UsuarioLogeado.Telefono%>" disabled />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <h4><i class="bi bi-check-circle-fill"></i>Resumen del Pedido</h4>
                <ul class="list-group mb-3">
                    <% 
                    decimal total = 0;
                    foreach (var detalle in listaDetalles)
                    {
                        total += detalle.Subtotal;
                    %>
                    <li class="list-group-item d-flex justify-content-between">
                        <div>
                            <h6 class="my-0"><%= detalle.Producto.Nombre %></h6>
                            <small class="text-muted">Cantidad: <%= detalle.Cantidad %></small>
                        </div>
                        <span class="text-muted">$<%= detalle.Subtotal.ToString("N2") %></span>
                    </li>
                    <% } %>
                    <li class="list-group-item d-flex justify-content-between">
                        <span>Total (ARS)</span>
                        <strong>$<%= total.ToString("N2") %></strong>
                    </li>
                </ul>
                <div class="mb-3">
                    <label class="form-label">Método de Pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" CssClass="btn btn-dark w-100" OnClick="btnConfirmarCompra_Click" />
            </div>
        </div>
    </div>

    <div class="modal fade" id="compraExitosa" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">Compra realizada</h5>
                </div>
                <div class="modal-body">
                    ¡Compra realizada con total éxito! Recibirás un email con la factura.
                </div>
                <div class="modal-footer">
                    <a href="/Inicio.aspx" class="btn btn-dark">Seguir comprando</a>
                </div>
            </div>
        </div>
    </div>

    <script>
        function confirmarCompra() {
            var modal = new bootstrap.Modal(document.getElementById('compraExitosa'));
            modal.show();
        }
    </script>
</asp:Content>

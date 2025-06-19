<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="UI.Usuario.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <h1><i class="bi bi-cart-check-fill"></i> Mi Carrito</h1>
    </div>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-3 mt-3">
        <% foreach (Dominio.Detalle detalle in listaDetalles)
            {
                Dominio.Producto producto = detalle.Producto;
                List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes
                    .Where(img => img.IdProducto == producto.Id).ToList();
                string carouselId = "carousel" + detalle.Id;
        %>
        <div class="col">
            <div class="card h-100">
                <div id="<%= carouselId %>" class="carousel slide" data‑bs‑ride="carousel">
                    <div class="carousel-inner">
                        <% if (imagenesDelProducto.Count == 0)
                            { %>
                        <div class="carousel-item active">
                            <img src="https://th.bing.com/th/id/OIP.mSzrXbopNaal5jPsMxNHHwHaHa?cb=iwc1&rs=1&pid=ImgDetMain"
                                class="d-block w-100" style="height:300px;object-fit:contain;"
                                alt="Sin imagen" />
                        </div>
                        <% }
                            else
                            {
                                for (int i = 0; i < imagenesDelProducto.Count; i++)
                                { %>
                        <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                            <img src="<%= imagenesDelProducto[i].UrlProducto %>"
                                class="d-block w-100" style="height:300px;object-fit:contain;"
                                alt="Imagen producto" />
                        </div>
                        <%   }
                            } %>
                    </div>
                </div>

                <div class="card-body text-center">
                    <h5 class="card-title"><%= producto.Nombre %></h5>
                    <p class="card-text"> Precio unitario: <%= detalle.PrecioUnitario.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                    <p class="card-text">Cantidad: <%= detalle.Cantidad %></p>
                    <p class="card-text fw-bold"> Subtotal: <%= detalle.Subtotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                </div>
                <div class="card-footer text-center">
                    <a href="Carrito.aspx?quitar=<%= detalle.IdProducto %>" class="btn btn-danger btn-sm"
                        onclick="abrirModalQuitarProducto(<%= producto.Id %>, '<%= producto.Nombre.Replace("'", "\\'") %>'); return false;">Quitar
                    </a>
                </div>
            </div>
        </div>
        <%
            } %>
    </div>

    <% if (listaDetalles != null && listaDetalles.Count > 0)
        { %>
    <div class="d-flex justify-content-end mt-4">
        <h4>
            <i class="bi bi-cash-stack"></i>Total a pagar: 
            <%= total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-AR")) %>
        </h4>
    </div>
    <div class="d-flex justify-content-end">
        <button class="btn btn-dark btn-lg">Continuar con el Pago</button>
    </div>
    <% } %>

    <div class="modal fade" id="quitarProductoModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Quitar del Carrito</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="quitarProductoModalBody"></div>
                <div class="modal-footer">
                    <a href="#" id="confirmarQuitarProductoBtn" class="btn btn-light">Quitar</a>
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModalQuitarProducto(idProducto, nombreProducto) {
            document.getElementById('quitarProductoModalBody').textContent = "¿Estás seguro que querés quitar '" + nombreProducto + "' de tu carrito?";
            document.getElementById('confirmarQuitarProductoBtn').href = "Carrito.aspx?quitar=" + idProducto;

            var modal = new bootstrap.Modal(document.getElementById('quitarProductoModal'));
            modal.show();
        }
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="UI.Usuario.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1><i class="bi bi-star"></i> Favoritos</h1>
    </div>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-3 mt-3">
        <% foreach (Dominio.Favorito favorito in listaFavoritos)
            {
                Dominio.Producto producto = favorito.Producto;
                List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes.Where(img => img.IdProducto == producto.Id).ToList();
                string carouselId = "carousel" + producto.Id;
        %>
        <div class="col">
            <div class="card h-100">
                <div id="<%= carouselId %>" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <% if (imagenesDelProducto.Count == 0)
                            { %>
                        <div class="carousel-item active">
                            <img src="https://th.bing.com/th/id/OIP.mSzrXbopNaal5jPsMxNHHwHaHa?cb=iwc1&rs=1&pid=ImgDetMain"
                                class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Producto sin imagen" />
                        </div>
                        <% }
                            else
                            {
                                for (int i = 0; i < imagenesDelProducto.Count; i++)
                                { %>
                        <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                            <img src="<%= imagenesDelProducto[i].UrlProducto %>"
                                class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Imagen del producto" />
                        </div>
                        <% }
                            } %>
                    </div>
                </div>

                <div class="card-body text-center">
                    <h5 class="card-title"><%= producto.Nombre %></h5>
                    <p class="card-text"> <%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                </div>
                <div class="card-footer text-center">
                    <a href="#" class="btn btn-danger btn-sm"
                        onclick="abrirModalQuitarFavorito(<%= producto.Id %>, '<%= producto.Nombre.Replace("'", "\\'") %>'); return false;">Quitar
                    </a>

                    <a href="AgregarAlCarrito.aspx?id=<%= producto.Id %>" class="btn btn-dark btn-sm">Agregar al carrito</a>
                </div>

            </div>
        </div>
        <% } %>
    </div>

    <div class="modal fade" id="quitarFavoritoModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Quitar de Favoritos</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="quitarFavoritoModalBody"></div>
                <div class="modal-footer">
                    <a href="#" id="confirmarQuitarFavoritoBtn" class="btn btn-light">Quitar</a>
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModalQuitarFavorito(idProducto, nombreProducto) {
            document.getElementById('quitarFavoritoModalBody').textContent = "¿Estás seguro que querés quitar '" + nombreProducto + "' de tus favoritos?";
            document.getElementById('confirmarQuitarFavoritoBtn').href = "Favoritos.aspx?quitar=" + idProducto;

            var modal = new bootstrap.Modal(document.getElementById('quitarFavoritoModal'));
            modal.show();
        }
    </script>

</asp:Content>

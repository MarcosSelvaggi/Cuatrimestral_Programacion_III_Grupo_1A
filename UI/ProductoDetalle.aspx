<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoDetalle.aspx.cs" Inherits="UI.ProductoDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="py-5">
        <div class="container px-4 px-lg-5 my-5">
            <div class="row gx-4 gx-lg-5 align-items-center">
                <div class="col-md-6">
                    <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
                        <% List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes.Where(img => img.IdProducto == producto.Id).ToList();
                            string carouselId = "carousel" + producto.Id; %>
                        <div id="<%= carouselId %>" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <%if (imagenesDelProducto.Count == 0)
                                    {  %>
                                <div class="carousel-item active">
                                    <img src="https://th.bing.com/th/id/OIP.mSzrXbopNaal5jPsMxNHHwHaHa?cb=iwc1&rs=1&pid=ImgDetMain" class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Producto sin imagen">
                                </div>
                                <%}
                                else
                                { %>
                                <% for (int i = 0; i < imagenesDelProducto.Count; i++)
                                    { %>
                                <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                                    <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Imagen del producto" id="imgProducto">
                                </div>
                                <% }
                                }%>
                            </div>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#<%= carouselId %>" data-bs-slide="prev">
                            <i class="bi bi-arrow-left-circle-fill fs-2 text-dark"></i>
                            <span class="visually-hidden">Anterior</span>
                        </button>

                        <button class="carousel-control-next" type="button" data-bs-target="#<%= carouselId %>" data-bs-slide="next">
                            <i class="bi bi-arrow-right-circle-fill fs-2 text-dark"></i>
                            <span class="visually-hidden">Siguiente</span>
                        </button>
                    </div>
                </div>

                <div class="col-md-6">
                    <h4 class="display-8 fw-bolder">
                        <asp:Label ID="lblCategoria" runat="server" /></h4>
                    <h1 class="display-5 fw-bolder">
                        <asp:Label ID="lblNombre" runat="server"/></h1>
                    <p class="lead">
                        <asp:Label ID="lblMarca" runat="server" />
                    </p>
                    <div class="fs-5 mb-5">
                        <span>
                            <asp:Label ID="lblPrecio" runat="server"/></span>
                    </div>
                    <div class="d-flex gap-2">
                        <% if (Session["usuario"] != null)
                            { %>
                                <button type="button" class="btn btn-outline-dark flex-shrink-0" data-bs-toggle="modal" data-bs-target="#favoritoModal">
                                    <i class="bi bi-bookmark-heart"></i>
                                    <%= favoritoRepetido ? " Quitar de Favoritos" : " Añadir a Favoritos" %>
                                </button>
                        <% } %>

                        <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control text-center me-3"
                            MaxLength="3" Style="max-width: 3rem" />
                        <button class="btn btn-outline-dark flex-shrink-0" type="button">
                            <i class="bi bi-cart-plus"></i>Agregar al Carrito
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="modal fade" id="favoritoModal" tabindex="-1" aria-labelledby="favoritoModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="favoritoModalLabel">
                        <%= favoritoRepetido ? "Quitar de Favoritos" : "Agregar a Favoritos" %>
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <%= favoritoRepetido ? "Producto ya existente en tu lista de favoritos ¿Estás seguro que querés quitar este producto de tus favoritos?" : "¿Querés agregar este producto a tus favoritos?" %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                    <% if (favoritoRepetido)
                        { %>
                    <a href="ProductoDetalle.aspx?id=<%= producto.Id %>&fav=quitar" class="btn btn-danger">Quitar</a>
                    <% }
                    else
                    { %>
                    <a href="ProductoDetalle.aspx?id=<%= producto.Id %>&fav=agregar" class="btn btn-success">Agregar</a>
                    <% } %>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

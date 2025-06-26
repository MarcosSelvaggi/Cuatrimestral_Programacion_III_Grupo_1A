<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="UI.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Inicio/Js/JsProductos.js"></script>
    <!-- Ocultamos el overflow horizontal -->
    <style>
        body {
            overflow-x: hidden;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <div class="row">
        <!-- Banner izquierdo -->
        <aside class="col-2" style="padding-left: 50px">
            <a href="/Productos.aspx?Marca=Nvidia">
                <img src="/Inicio/Imagenes/Banners/banner-vertical-small-0.jpg" class="img-fluid" style="width: 227px; height: auto" alt="Banner lateral" />
            </a>
            <p></p>
            <a href="/Productos.aspx?Marca=MSI">
                <img src="/Inicio/Imagenes/Banners/banner-vertical-large-2.jpg" class="img-fluid" style="width: 227px; height: 1008px" alt="Banner lateral" />
            </a>
        </aside>
        <!-- Fin banner izquierdo -->
        <div class="container text-center col-8" id="DivSeparador">
            <div class="row align-items-start">
                <div class="col-3 gap-0 column-gap-3" id="MarcasCategorias" style="grid-template-columns: 1fr 1fr;">
                    <!-- Búsqueda de productos -->
                    <asp:Panel runat="server" DefaultButton="RealizarBusquedaProducto">
                        <div class="d-flex p-2" role="search">
                            <asp:TextBox CssClass="form-control me-2" ID="txtBusqueda" placeholder="RTX-8090" runat="server" />
                            <asp:Button ID="RealizarBusquedaProducto" OnClick="RealizarBusquedaProducto_Click" Text="Buscar" CssClass="btn btn-outline-success" runat="server" />
                        </div>
                    </asp:Panel>
                    <!-- Rango de precios -->
                    <div class="container p-2 col-12" id="txtRangoPrecios">
                        <div class="row mb-2">
                            <div class="col-3">
                                <label for="txtPrecioMinimo" class="col-form-label">Desde:</label>
                            </div>
                            <div class="col-9">
                                <asp:TextBox runat="server" ClientIDMode="Static" onKeyPress="return soloNumeros(event)" ID="txtPrecioMinimo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-3">
                                <label for="txtPrecioMaximo" class="col-form-label">Hasta:</label>
                            </div>
                            <div class="col-9">
                                <asp:TextBox runat="server" ClientIDMode="Static" onKeyPress="return soloNumeros(event)" ID="txtPrecioMaximo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="text-center mt-3">
                            <asp:Button Text="Aplicar rango de precios" CssClass="btn btn-outline-primary" ID="btnRangoPrecios" OnClick="btnRangoPrecios_Click" runat="server" />
                        </div>
                    </div>
                    <!-- Listado por categorías -->
                    <div class="btn-group p-2 col-12">
                        <button type="button" class="btn btn-dark dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                            Categorias
                        </button>
                        <ul class="dropdown-menu">
                            <% foreach (Dominio.Categoria categoria in listaCategorias)
                                { %>
                            <li><a class="dropdown-item" href="/Productos.aspx?categoria=<%:categoria.Descripcion %>"><%: categoria.Descripcion %></a></li>
                            <%  } %>
                        </ul>
                    </div>
                    <!-- Listado por marcas -->
                    <div class="btn-group p-2 col-12">
                        <button type="button" class="btn btn-dark dropdown-toggle " data-bs-toggle="dropdown" aria-expanded="false">
                            Marcas
                        </button>
                        <ul class="dropdown-menu">
                            <% foreach (Dominio.Marca marca in listaMarcas)
                                { %>
                            <li><a class="dropdown-item" href="/Productos.aspx?marca=<%:marca.Descripcion %>"><%: marca.Descripcion %></a></li>
                            <% } %>
                        </ul>
                    </div>
                </div>
                <div class="col-8" id="Productos">
                    <div class="row row-cols-md-3">
                        <%foreach (Dominio.Producto producto in listaProductos)
                            {
                        %>
                        <div class="col-4" id="grillaArticulos">
                            <a href="/ProductoDetalle.aspx?id=<%: producto.Id %>" style="text-decoration: none">
                                <div class="card" style="height: 214px; width: 264px">
                                    <% List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes.Where(img => img.IdProducto == producto.Id).ToList();
                                        string carouselCategoriaId = "carousel" + producto.Id; %>
                                    <div id="<%= carouselCategoriaId %>" class="carousel slide" data-bs-ride="carousel">
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
                                                <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 100px; width: auto; object-fit: contain;" alt="Imagen del producto">
                                            </div>
                                            <% }
                                                }%>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <h5 class="card-title"><%: producto.Nombre %></h5>
                                        <p class="card-text"><%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                                    </div>
                                </div>
                            </a>
                            <br />
                        </div>
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
        <!-- Banner dercho -->
        <aside class="col-2" style="padding-left: 30px">
            <a href="/Productos.aspx?Marca=Nvidia">
                <img src="/Inicio/Imagenes/Banners/banner-vertical-large-1.jpg" class="img-fluid" style="width: 227px; height: 1008px" alt="Banner lateral" />
            </a>
            <!-- Separado de banners-->
            <p></p>
            <a href="/Productos.aspx?Marca=Nvidia">
                <img src="/Inicio/Imagenes/Banners/banner-vertical-small-1.jpg" class="img-fluid" style="width: 227px; height: auto" alt="Banner lateral" />
            </a>
        </aside>
        <!-- Fin banner derecho -->
    </div>
</asp:Content>

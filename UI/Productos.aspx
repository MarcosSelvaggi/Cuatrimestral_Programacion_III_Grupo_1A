<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="UI.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/JsProductos.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <div class="container text-center" id="DivSeparador">
        <div class="row align-items-start">
            <div class="col-3 gap-0 column-gap-3" id="MarcasCategorias" style="grid-template-columns: 1fr 1fr;" >
                <div class="d-flex p-2" role="search">
                    <asp:TextBox Cssclass="form-control me-2" ID="txtBusqueda" placeholder="RTX-8090" runat="server" /> 
                    <asp:Button ID="RealizarBusquedaProducto" OnClick="RealizarBusquedaProducto_Click" Text="Buscar" CssClass="btn btn-outline-success" runat="server" />
                </div>
                <div class="btn-group p-2 col-12">
                    <button type="button" class="btn btn-dark dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                        Categorias
                    </button>
                    <ul class="dropdown-menu" >
                        <% foreach (Dominio.Categoria categoria in listaCategorias)
                            { %>
                        <li><a class="dropdown-item" href="/Productos.aspx?categoria=<%:categoria.Id %>"><%: categoria.Descripcion %></a></li>
                        <%  } %>
                    </ul>
                </div>
                <div class="btn-group p-2 col-12" >
                    <button type="button" class="btn btn-dark dropdown-toggle " data-bs-toggle="dropdown" aria-expanded="false">
                        Marcas
                    </button>
                    <ul class="dropdown-menu" >
                        <% foreach (Dominio.Marca marca in listaMarcas)
                            { %>
                        <li><a class="dropdown-item" href="/Productos.aspx?marca=<%:marca.Id %>"><%: marca.Descripcion %></a></li>
                        <% } %>
                    </ul>
                </div>
            </div>
            <div class="col-9" id="Productos">
                <% if (CategoriaSeleccionada != -1)
                    {  %>
                <div class="row row-cols-md-3" style="font-family: Calibri;">
                    <%foreach (Dominio.Producto producto in listaProductos)
                        {
                            if (producto.Categoria.Id == CategoriaSeleccionada)
                            {
                    %>
                    <div class="col-4" id="grillaArticulosPorCategoria">
                        <div class="card">
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
                                        <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 100px; object-fit: contain;" alt="Imagen del producto">
                                    </div>
                                    <% }
                                        }%>
                                </div>
                            </div>
                            <div class="card-body">
                                <a href="/ProductoDetalle.aspx?id=<%: producto.Id %>">
                                    <h5 class="card-title" style="font-palette: dark"><%: producto.Nombre %></h5>
                                </a>
                                <p class="card-text"><%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                            </div>
                        </div>
                        <br />
                    </div>
                    <% }
                        } %>
                </div>
                <%}
                    else if (MarcaSeleccionada != -1)
                    { %>
                <div class="row row-cols-md-3" style="font-family: Calibri;">
                    <%foreach (Dominio.Producto producto in listaProductos)
                        {
                            if (producto.Marca.Id == MarcaSeleccionada)
                            {
                    %>
                    <div class="col-4" id="grillaArticulosPorMarcas">
                        <div class="card">
                            <% List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes.Where(img => img.IdProducto == producto.Id).ToList();
                                string carouselMarcaId = "carousel" + producto.Id; %>
                            <div id="<%= carouselMarcaId %>" class="carousel slide" data-bs-ride="carousel">
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
                                        <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 100px; object-fit: contain;" alt="Imagen del producto">
                                    </div>
                                    <% }
                                        }%>
                                </div>
                            </div>
                            <div class="card-body">
                                <a href="/ProductoDetalle.aspx?id=<%: producto.Id %>">
                                    <h5 class="card-title" style="font-palette: dark"><%: producto.Nombre %></h5>
                                </a>
                                <p class="card-text"><%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                            </div>
                        </div>
                        <br />
                    </div>
                    <% }
                        } %>
                </div>
                <% }
                    else
                    {  %>
                <div class="row row-cols-md-3" style="font-family: Calibri;">
                    <%foreach (Dominio.Producto producto in listaProductosBuscados)
                        {%>
                    <div class="col-4" id="grillaArticulosBuscados">
                        <div class="card">
                            <% List<Dominio.ImagenesProducto> imagenesDelProducto = listaImagenes.Where(img => img.IdProducto == producto.Id).ToList();
                                string carouselProductosBuscadosId = "carousel" + producto.Id; %>
                            <div id="<%= carouselProductosBuscadosId %>" class="carousel slide" data-bs-ride="carousel">
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
                                        <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 100px; object-fit: contain;" alt="Imagen del producto">
                                    </div>
                                    <% }
                                        }%>
                                </div>
                            </div>
                            <div class="card-body">
                                <a href="/ProductoDetalle.aspx?id=<%: producto.Id %>">
                                    <h5 class="card-title" style="font-palette: dark"><%: producto.Nombre %></h5>
                                </a>
                                <p class="card-text"><%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                            </div>
                        </div>
                        <br />
                    </div>
                    <% }
                        }%>
                </div>
            </div>
        </div>
    </div>




</asp:Content>

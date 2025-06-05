<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UI.Inicio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/EstiloImagenes.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <header>
        <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
            <div class="container-fluid">
                <div class="collapse navbar-collapse d-flex justify-content-center" id="navbarSupportedContent">
                    <ul class="navbar-nav mb-2 mb-lg-0">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="/Productos.aspx">Productos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="/Logearse.aspx">Iniciar Sesión</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="/Contacto.aspx">Contacto</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div id="logos-marcas" class="row white-background">
        <div class="container d-flex justify-content-center align-items-center" style="height: 3.75rem; background-color: white">
            <div class="logo-marca">
                <a href="/productos">
                    <img src="https://logowik.com/content/uploads/images/amd7686.jpg" alt="AMD" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos">
                    <img src="https://iot.org.ar/wp-content/uploads/2020/12/logo-intel.jpg" alt="Intel" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos">
                    <img src="https://www.nvidia.com/content/dam/en-zz/Solutions/about-nvidia/logo-and-brand/nvidia-og-image-white-bg-1200x630.jpg" alt="Nvidia" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos">
                    <img src="https://bsmedia.business-standard.com/_media/bs/img/article/2022-12/12/full/1670815387-4729.jpg?im=FeatureCrop,size=(826,465)" alt="Asus" />
                </a>
            </div>
        </div>
    </div>

    <h1 style="font-family: Calibri;">Disfruta de nuestras categorias</h1>
    <div id="separadorPorCategorias">
        <%foreach (Dominio.Categoria categoria in listaCategorias)
            {
                int cantidadArticulosDibujados = 0; //Varaible que se encarga de manejar la cantidad de productos que se dibujan 
        %>
        <div id="nombreCategoria">
            <h1><%= categoria.Descripcion %>
               <a href="/Productos.aspx" class="btn btn-dark">Ver Más</a>
            </h1>
        </div>
        <div class="row row-cols-md-3" style="font-family: Calibri;">
            <%foreach (Dominio.Producto producto in listaProductos)
                {
                    if (producto.Categoria.Id == categoria.Id && cantidadArticulosDibujados < 3)
                    {
                        cantidadArticulosDibujados++; //Varaible que se encarga de manejar la cantidad de productos que se dibujan 
            %>
            <div class="col-4" id="grillaArticulos">
                <div class="card">
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
                                <img src="<%= imagenesDelProducto[i].UrlProducto %>" class="d-block w-100" style="height: 100px; object-fit: contain;" alt="Imagen del producto" id="imgProducto">
                            </div>
                            <% }
                                }%>
                        </div>
                    </div>
                    <div class="card-body">
                        <a href="/ProductoDetalle.aspx?id=<%: producto.Id %>">
                            <h5 class="card-title" style="font-palette: dark"><%: producto.Nombre %></h5>
                        </a>
                        <p class="card-text">$ <%: Math.Round(producto.Precio, 2, MidpointRounding.AwayFromZero) %></p>
                    </div>
                </div>
                <br />
            </div>
            <% }
                } %>
        </div>
        <% } %>
    </div>

</asp:Content>

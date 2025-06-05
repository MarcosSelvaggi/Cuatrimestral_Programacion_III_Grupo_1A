<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UI.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Inicio/StylesInicio/EstiloImagenes.css" rel="stylesheet" />
    <link href="Inicio/StylesInicio/styles.css" rel="stylesheet" />
    <script src="Inicio/ScriptsInicio/scripts.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <header id="headerInicio" class="bg-dark py-5">
        <div class="container px-4 px-lg-5 my-5">
            <div class="text-center text-white">
                <h1 class="display-4 fw-bolder rgbLetras">UNITE AL MUNDO GAMER</h1>
            </div>
        </div>
    </header>


    <div id="logos-marcas" class="row white-background">
        <div class="container d-flex justify-content-center align-items-center" style="height: 3.75rem; background-color: white">
            <div class="logo-marca">
                <a href="/productos.aspx">
                    <img src="https://logowik.com/content/uploads/images/amd7686.jpg" alt="AMD" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos.aspx">
                    <img src="https://iot.org.ar/wp-content/uploads/2020/12/logo-intel.jpg" alt="Intel" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos.aspx">
                    <img src="https://www.nvidia.com/content/dam/en-zz/Solutions/about-nvidia/logo-and-brand/nvidia-og-image-white-bg-1200x630.jpg" alt="Nvidia" />
                </a>
            </div>
            <div class="logo-marca">
                <a href="/productos.aspx">
                    <img src="https://bsmedia.business-standard.com/_media/bs/img/article/2022-12/12/full/1670815387-4729.jpg?im=FeatureCrop,size=(826,465)" alt="Asus" />
                </a>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="py-5">
                <div class="container " style="background-color: white">
                    <%foreach (Dominio.Categoria categoria in listaCategorias)
                        {
                            int cantidadArticulosDibujados = 0; //Varaible que se encarga de manejar la cantidad de productos que se dibujan 
                    %>
                    <div id="nombreCategoria">
                        <h1 style="font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif"><%= categoria.Descripcion %>
                            <a href="/Productos.aspx?categoria=<%=categoria.Id%>" class="btn btn-dark">Ver más</a>
                        </h1>
                    </div>
                    <br />
                    <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                        <%foreach (Dominio.Producto producto in listaProductos)
                            {
                                if (producto.Categoria.Id == categoria.Id && cantidadArticulosDibujados < 4)
                                {
                                    cantidadArticulosDibujados++; //Varaible que se encarga de manejar la cantidad de productos que se dibujan 
                        %>
                        <div class="col mb-5">
                            <div class="card h-100">
                                <!-- Product image-->
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
                                <!-- Product details-->
                                <div class="card-body p-4">
                                    <div class="text-center">
                                        <!-- Product name-->
                                        <h5 class="fw-bolder"><%= producto.Nombre%></h5>
                                        <!-- Product price-->
                                        <%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %>
                                    </div>
                                </div>
                                <!-- Product actions-->
                                <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
                                    <div class="text-center">
                                        <a class="btn btn-outline-dark mt-auto" href="/ProductoInfo">Ver más información</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%}
                            }%>
                    </div>
                    <% }  %>
                </div>
            </div>
        </div>
    </div>

    <!--FOOTER-->
    <footer class="py-5 bg-dark">
        <div class="container">
            <p class="m-0 text-center text-white">Grupo 1A &copy; Proyecto Final eCommerce</p>
        </div>
    </footer>
</asp:Content>

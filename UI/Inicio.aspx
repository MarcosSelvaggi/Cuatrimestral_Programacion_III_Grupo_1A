<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UI.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Inicio/Estilos/EstiloImagenes.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Header -->
    <div id="carouselExampleAutoplaying" class="carousel slide carousel-fade" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Banner 1"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="2" aria-label="Slide 3"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="3" aria-label="Slide 4"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="4" aria-label="Slide 5"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="5" aria-label="Slide 6"></button>
            <button type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide-to="6" aria-label="Slide 7"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/168.png" alt="Primer banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/203.jpg" alt="Segundo banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/204.jpg" alt="Tercero banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/209.png" alt="Cuarto banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/217.jpg" alt="Quinto banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/218.jpg" alt="Sexto banner">
                </a>
            </div>
            <div class="carousel-item">
                <a href="/Productos.aspx?Marcas=Gigabyte">
                    <img class="d-block w-100" src="/Inicio/Imagenes/Banners/219.jpg" alt="Septimo banner">
                </a>
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
        </button>
    </div>
    <!-- Fin Header -->
    <!-- Main -->
    <div class="row">
        <div class="py-5">
            <div class="row" style="background-color: white">
                <!-- Banner izquierdo -->
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
                <!-- Fin banner izquierdo -->
                <div class="container col-8">
                    <!-- Inicio de sección de categorias -->
                    <div class="col-12">
                        <span class="tituloCategorias">EXPLORÁ NUESTRAS CATEGORIAS</span>
                        <p></p>
                        <div class="row">
                            <div class="col-4">
                                <a href="/Productos.aspx">
                                    <div class="categoriaPrincipal">
                                        <div class="sombraCategoria"></div>
                                        <div class="divTextoCategoria">
                                            <p class="textoCategoria">PROCESADORES</p>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="col-8 container">
                                <div class="contenedorCategorias row g-4">
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Periféricos" class="col-12">
                                            <div class="Perifericos">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Periféricos</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Mothers">
                                            <div class="Mothers">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Mothers</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=GPU">
                                            <div class="Gpu">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Placas de vídeo</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=RAMs">
                                            <div class="RAMS">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Memorias RAM</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Almacenamiento">
                                            <div class="Almacenamiento">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Almacenamiento</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Fuentes">
                                            <div class="Fuentes">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Fuentes</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Monitores">
                                            <div class="Monitores">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Monitores</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?Categoria=Notebooks">
                                            <div class="Notebooks">
                                                <div class="sombraCategoria"></div>
                                                <div class="divTextoCategoria">
                                                    <p class="textoCategoria">Notebooks</p>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Fin de sección de categorias -->

                    <!-- Inicio de sección de marcas -->
                    <div class="col-12">
                        <span class="tituloCategorias">EXPLORÁ LAS DISTINTAS MARCAS</span>
                        <p></p>
                        <div class="row">
                            <div class="col-8 container">
                                <div class="contenedorCategorias row g-4">
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=Steelseries" class="col-12">
                                            <div class="Steelseries"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=MSI">
                                            <div class="MSI"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=Logitech">
                                            <div class="Logitech"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=Hyper-X">
                                            <div class="Hyper-X"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=Asus">
                                            <div class="Asus"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=Razer">
                                            <div class="Razer"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=MSI">
                                            <div class="MSI"></div>
                                        </a>
                                    </div>
                                    <div class="categoriaSecundaria col-3">
                                        <a href="/Productos.aspx?marca=AMD">
                                            <div class="AMD"></div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4">
                                <a href="/Productos.aspx?marca=Nvidia">
                                    <div class="marcaPrincipal"></div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <!-- Fin de sección de marcas -->
                    <!-- Inicio productos destacados -->
                    <p></p>
                    <div class="row col-12">

                        <div class="d-grid col-4" id="productosDescatados" style="background-image: linear-gradient( rgb(61, 60, 60), black); height: 300px; width: 360px; border-radius: 15px;">
                            <div class="productosDestacados">
                                <span class="productoDestacadoTitulo">PRODUCTOS</span>
                                <span class="destacadoTitulo">DESTACADOS</span>
                            </div>
                            <a href="/Productos.aspx" class="w-75 btn btn-danger btn-lg m-auto px-3">VER MÁS</a>
                        </div>

                        <div class="container col-8">
                            <div class="row row-cols-4">
                                <div class="col-4 Producto">
                                    <a href="/ProductoDetalle.aspx?id=<%:listaProductosAux[0].Id %>" style="text-decoration: none">
                                        <div class="card" style="height: 300px; width: 264px">
                                            <div class="card-body">
                                                <img src="<%:listaImagesAux[0].UrlProducto %>" class="d-block w-100" style="height: 180px; width: auto; object-fit: contain;" alt="Imagen del producto" />
                                                <h5 class="card-title"><%: listaProductosAux[0].Nombre %></h5>
                                                <p class="card-text"><%= listaProductosAux[0].Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-4 Producto">
                                    <a href="/ProductoDetalle.aspx?id=<%:listaProductosAux[1].Id %>" style="text-decoration: none">
                                        <div class="card" style="height: 300px; width: 264px">
                                            <div class="card-body">
                                                <img src="<%:listaImagesAux[1].UrlProducto %>" class="d-block w-100" style="height: 180px; width: auto; object-fit: contain;" alt="Imagen del producto" />
                                                <h5 class="card-title"><%: listaProductosAux[1].Nombre %></h5>
                                                <p class="card-text"><%= listaProductosAux[1].Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-4 Producto">
                                    <a href="/ProductoDetalle.aspx?id=<%:listaProductosAux[2].Id %>" style="text-decoration: none">
                                        <div class="card" style="height: 300px; width: 264px">
                                            <div class="card-body">
                                                <img src="<%:listaImagesAux[2].UrlProducto %>" class="d-block w-100" style="height: 180px; width: auto; object-fit: contain;" alt="Imagen del producto" />
                                                <h5 class="card-title"><%: listaProductosAux[2].Nombre %></h5>
                                                <p class="card-text"><%= listaProductosAux[2].Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Fin productos destacados -->
                </div>
                <!-- Banner derecho -->
                <aside class="col-2" style="padding-left: 50px">
                    <a href="/Productos.aspx?Marca=Nvidia">
                        <img src="/Inicio/Imagenes/Banners/banner-vertical-small-0.jpg" class="img-fluid" style="width: 227px; height: auto" alt="Banner lateral" />
                    </a>
                    <p></p>
                    <a href="/Productos.aspx?Marca=MSI">
                        <img src="/Inicio/Imagenes/Banners/banner-vertical-large-2.jpg" class="img-fluid" style="width: 227px; height: 1008px" alt="Banner lateral" />
                    </a>
                </aside>
                <!-- Fin banner derecho -->
            </div>
        </div>

        <!--Footer-->
        <footer class="py-5 bg-dark">
            <div class="container">
                <p class="m-0 text-center text-white">Grupo 1A &copy; Proyecto Final eCommerce <a href="/Contacto/Contacto.aspx">Pongase en contacto con nosotros</a></p>
            </div>
        </footer>
</asp:Content>

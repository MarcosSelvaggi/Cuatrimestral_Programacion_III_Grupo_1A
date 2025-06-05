<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoDetalle.aspx.cs" Inherits="UI.ProductoDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="py-5">
        <div class="container px-4 px-lg-5 my-5">
            <div class="row gx-4 gx-lg-5 align-items-center">
                <div class="col-md-6">
                    <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <img src="https://th.bing.com/th/id/OIP.Ne2cdbsYHSrbxjiYGRjBPwHaHa?rs=1&pid=ImgDetMain" class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Imagen1" />
                            </div>
                            <div class="carousel-item">
                                <img src="https://images-na.ssl-images-amazon.com/images/I/71HPtyhEz8L._AC_SL1500_.jpg" class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Imagen2" />
                            </div>
                            <div class="carousel-item">
                                <img src="https://th.bing.com/th/id/OIP.Z13QCdzPBnfbySIRRCEG2wHaHa?rs=1&pid=ImgDetMain" class="d-block w-100" style="height: 300px; object-fit: contain;" alt="Imagen3" />
                            </div>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                            <i class="bi bi-arrow-left-circle-fill fs-2 text-dark"></i>
                            <span class="visually-hidden">Anterior</span>
                        </button>

                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                            <i class="bi bi-arrow-right-circle-fill fs-2 text-dark"></i>
                            <span class="visually-hidden">Siguiente</span>
                        </button>
                    </div>
                </div>

                <div class="col-md-6">
                    <h1 class="display-5 fw-bolder">
                        <asp:Label ID="lblNombre" runat="server" Text="Mouse Mega Gamer bb" /></h1>
                    <div class="fs-5 mb-5">
                        <span>
                            <asp:Label ID="lblPrecio" runat="server" Text="$40.000" /></span>
                    </div>
                    <p class="lead">
                        <asp:Label ID="lblDescription" runat="server" Text="Super comodo y facil de usar hasta para gente manca." />
                    </p>
                    <div class="d-flex">
                        <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control text-center me-3"
                            MaxLength="3" Style="max-width: 3rem" />
                        <button class="btn btn-outline-dark flex-shrink-0" type="button">
                            <i class="bi bi-cart-plus"></i>Agregar al carrito
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

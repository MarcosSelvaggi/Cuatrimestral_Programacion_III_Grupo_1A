<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="UI.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/JsProductos.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="container text-center" id="DivSeparador">
        <div class="row">
            <div class="col" id="Categorias" >
                <div class="btn-group-vertical" role="group" aria-label="Vertical button group">
                    <div class="btn-group-vertical" role="group" aria-label="Basic radio toggle button group" id="btnRadioButtonCategoria" runat="server">
                        <% 
                            foreach (Dominio.Categoria categoria in listaCategorias)
                            {
                                if (categoria.Id == categoriaSeleccionada)
                                { %>
                        <input type="radio" class="btn-check" name="btnradio" id="btnradio<%= categoria.Id %>" autocomplete="off" checked onchange="__doPostBack(">
                        <a class="btn btn-dark" href="/Productos.aspx?categoria=<%:categoria.Id %>"><%: categoria.Descripcion %></a>
                        <% 
                            }
                            else
                            { %>
                        <input type="radio" class="btn-check" name="btnradio" id="btnradio<%: categoria.Id %>" autocomplete="off">
                        <a class="btn btn-dark" href="/Productos.aspx?categoria=<%:categoria.Id %>"><%: categoria.Descripcion %></a>
                       
                        <%  }
                            } %>
                    </div>
                </div>
            </div>
            <div class="col" id="Productos">
                <div class="row row-cols-md-3" style="font-family: Calibri;">
                    <%foreach (Dominio.Producto producto in listaProductos)
                        {
                            if (producto.Categoria.Id == categoriaSeleccionada)
                            { 
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
                                <p class="card-text"> <%= producto.Precio.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("es-ar")) %></p>
                            </div>
                        </div>
                        <br />
                    </div>
                    <% }
                        } %>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

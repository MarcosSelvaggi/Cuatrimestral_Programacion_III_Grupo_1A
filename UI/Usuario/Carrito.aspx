<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="UI.Usuario.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2><i class="bi bi-cart-check-fill"></i> Mi Carrito</h2>

        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-3 mt-3">

            <div class="col">
                <div class="card h-100">
                    <img src="https://th.bing.com/th/id/OIP.UxHQMTi7GOuKGJul1sQJTgHaEh?rs=1&pid=ImgDetMain" class="card-img-top" alt="Producto">
                    <div class="card-body text-center">
                        <h5 class="card-title">PC Gamer</h5>
                        <p class="card-text">$40.000</p>
                    </div>
                    <div class="card-footer text-center d-flex justify-content-around align-items-center">
                        <button class="btn btn-danger btn-sm">Quitar</button>
                        <input type="number" min="1" value="1" class="form-control form-control-sm" style="width: 70px;" />
                        <span class="fw-bold">$50.000</span>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card h-100">
                    <img src="https://th.bing.com/th/id/OIP.jnXWSN21fYDghAUSXXiB5gHaEv?rs=1&pid=ImgDetMain" class="card-img-top" alt="Producto">
                    <div class="card-body text-center">
                        <h5 class="card-title">AMD Radeon RX 570 RS Black Edition</h5>
                        <p class="card-text">$40.000</p>
                    </div>
                    <div class="card-footer text-center d-flex justify-content-around align-items-center">
                        <button class="btn btn-danger btn-sm">Quitar</button>
                        <input type="number" min="1" value="2" class="form-control form-control-sm" style="width: 70px;" />
                        <span class="fw-bold">$50.000</span>
                    </div>
                </div>
            </div>

        </div>

        <div class="d-flex justify-content-end mt-4">
            <h4><i class="bi bi-cash-stack"></i> Total a pagar: $90.000 ARS</h4>
        </div>

        <div class="d-flex justify-content-end">
            <button class="btn btn-dark btn-lg">Continuar con el Pago</button>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/UsuarioMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="UI.Usuario.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h1><i class="bi bi-bag-check-fill"></i> Checkout</h1>

        <div class="row">
            <div class="col-md-6">
                <h4><i class="bi bi-rocket-takeoff-fill"></i> Datos de Envío</h4>
                <div id="formShipping">
                    <div class="mb-3">
                        <label for="inputName" class="form-label">Nombre Completo</label>
                        <input type="text" class="form-control" id="inputNombre" placeholder="Leonel Messi" required />
                    </div>
                    <div class="mb-3">
                        <label for="inputAddress" class="form-label">Dirección</label>
                        <input type="text" class="form-control" id="inputDirección" placeholder="Los palmares 256" required />
                    </div>
                    <div class="mb-3">
                        <label for="inputCity" class="form-label">Localidad</label>
                        <input type="text" class="form-control" id="inputLocalidad" placeholder="Chacabuco" required />
                    </div>
                    <div class="mb-3">
                        <label for="inputCity" class="form-label">Ciudad</label>
                        <input type="text" class="form-control" id="inputCiudad" placeholder="Lima" required />
                    </div>
                    <div class="mb-3">
                        <label for="inputPostalCode" class="form-label">Código Postal</label>
                        <input type="text" class="form-control" id="inputCodigoPostal" placeholder="102910" required />
                    </div>
                    <div class="mb-3">
                        <label for="inputPhone" class="form-label">Teléfono</label>
                        <input type="tel" class="form-control" id="inputTelefono" placeholder="(+52) 145 39-2312" required />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <h4><i class="bi bi-check-circle-fill"></i> Resumen del Pedido</h4>
                <ul class="list-group mb-3">
                    <li class="list-group-item d-flex justify-content-between">
                        <div>
                            <h6 class="my-0">PC Gamer</h6>
                            <small class="text-muted">Cantidad: 2</small>
                        </div>
                        <span class="text-muted">$40.000</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                        <div>
                            <h6 class="my-0">AMD Radeon RX 570 RS Black Edition</h6>
                            <small class="text-muted">Cantidad: 1</small>
                        </div>
                        <span class="text-muted">$50.000</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                        <span>Total (ARS)</span>
                        <strong>$130.000</strong>
                    </li>
                </ul>

                <button type="submit" class="btn btn-dark w-100">Confirmar Compra</button>
            </div>
        </div>
    </div>
</asp:Content>

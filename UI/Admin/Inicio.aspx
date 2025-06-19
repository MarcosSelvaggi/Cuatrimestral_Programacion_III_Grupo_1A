<%@ Page Title="Admin - Inicio" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UI.Admin.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="mt-4">
        <i class="fa-solid fa-rocket me-2"></i>
        Últimas Ventas
    </h1>
    <div class="card mb-4">
        <div class="card-header">
            <i class="fa-solid fa-list me-1"></i>
            Ventas Entregadas
        </div>
        <div class="card-body">
            <asp:Repeater ID="rptVentasEntregadas" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Cliente</th>
                                <th>Fecha</th>
                                <th>Monto</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Cliente") %></td>
                        <td><%# Eval("FechaPedido", "{0:dd-MM-yyyy HH:mm}") %></td>
                        <td><%# String.Format("{0:C}", Eval("PrecioTotal")) %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionMarcas.aspx.cs" Inherits="UI.Admin.GestionMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <h2>Gestión de Marcas</h2>
        <div class="mb-3">
            <button type="button" class="btn btn-outline-primary">
                <i class="fas fa-plus me-2"></i>Agregar Marca
            </button>
        </div>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th class="d-none">ID</th>
                    <th>Descripción</th>
                    <th style="width: 220px; text-align: right;">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptMarcas" runat="server">
                    <itemtemplate>
                        <tr>
                            <td class="d-none"><%# Eval("Id") %></td>
                            <td><%# Eval("Descripcion") %></td>
                            <td style="text-align: right;">
                                <button type="button" class="btn btn-outline-primary me-1" title="Ver">
                                    <i class="fas fa-eye"></i>
                                </button>
                                <button type="button" class="btn btn-outline-success me-1" title="Editar">
                                    <i class="fas fa-pen"></i>
                                </button>
                                <button type="button" class="btn btn-outline-danger" title="Eliminar">
                                    <i class="fas fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </itemtemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

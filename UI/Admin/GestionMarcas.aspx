<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionMarcas.aspx.cs" Inherits="UI.Admin.GestionMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <h2>Gestión de Marcas</h2>
        <div class="mb-3">
            <asp:Button ID="btnAbrirAgregar" runat="server" Text="Agregar Marca" CssClass="btn btn-outline-primary" OnClick="btnAbrirAgregar_Click" />
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
                <asp:Repeater ID="rptMarcas" runat="server" OnItemCommand="rptMarcas_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td class="d-none"><%# Eval("Id") %></td>
                            <td><%# Eval("Descripcion") %></td>
                            <td style="text-align: right;">
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-primary me-1" CommandName="Ver" CommandArgument='<%# Eval("Id") %>' UseSubmitBehavior="false">
                                    <i class="fas fa-eye"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-success me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' UseSubmitBehavior="false">
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' UseSubmitBehavior="false">
                                    <i class="fas fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <div class="modal fade" id="modalMarca" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="lblModalTitulo" runat="server">Detalle Marca</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblIdMarca" runat="server" CssClass="d-none"></asp:Label>
                    <div class="mb-3">
                        <label>Descripción:</label>
                        <asp:Label ID="lblDescripcion" runat="server" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Activo:</label>
                        <asp:Label ID="lblActivo" runat="server" CssClass="form-control"></asp:Label>
                        <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select" Visible="false">
                            <asp:ListItem Text="Sí" Value="true"></asp:ListItem>
                            <asp:ListItem Text="No" Value="false"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" Visible="false" OnClick="btnGuardar_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmar Eliminación</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblDescripcionEliminar" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblIdEliminar" runat="server" CssClass="d-none"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalAgregar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar Marca</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtNuevaDescripcion">Descripción:</label>
                        <asp:TextBox ID="txtNuevaDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Activo:</label>
                        <asp:DropDownList ID="ddlNuevoActivo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Sí" Value="true"></asp:ListItem>
                            <asp:ListItem Text="No" Value="false"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

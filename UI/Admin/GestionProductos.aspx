<%@ Page Title="Admin - Productos" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="UI.Admin.GestionProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <h2><i class="bi bi-box-seam-fill"></i>Gestión de Productos</h2>
        <div class="mb-3">
            <asp:Button ID="btnAbrirAgregar" runat="server" Text="Agregar Producto" CssClass="btn btn-outline-primary" OnClick="btnAbrirAgregar_Click" />
        </div>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th class="d-none">ID</th>
                    <th>Nombre</th>
                    <th>Precio</th>
                    <th>Stock</th>
                    <th>Categoria</th>
                    <th>Marca</th>
                    <th>Activo</th>
                    <th style="width: 220px; text-align: right;">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptProductos" runat="server" OnItemCommand="rptProductos_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td class="d-none"><%# Eval("Id") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# String.Format("{0:C}", Eval("Precio")) %></td>
                            <td><%# Eval("Stock") %></td>
                            <td><%# Eval("Categoria.Descripcion") %></td>
                            <td><%# Eval("Marca.Descripcion") %></td>
                            <td><%# Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No" %></td>
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

    <div class="modal fade" id="modalProducto" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="lblModalTitulo" runat="server">Detalle Producto</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblIdProducto" runat="server" CssClass="d-none"></asp:Label>
                    <div class="mb-3">
                        <label>Nombre:</label>
                        <asp:Label ID="lblNombre" runat="server" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Precio:</label>
                        <asp:Label ID="lblPrecio" runat="server" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Stock:</label>
                        <asp:Label ID="lblStock" runat="server" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Categoria:</label>
                        <asp:Label ID="lblCategoria" runat="server" CssClass="form-control"></asp:Label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Marca:</label>
                        <asp:Label ID="lblMarca" runat="server" CssClass="form-control"></asp:Label>
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="mb-3" id="divAgregarImagenEdicion" runat="server" visible="false">
                        <label>Agregar URL de Imagen:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNuevaImagenEdicion" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnAgregarImagenTemporalEdicion" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarImagenTemporalEdicion_Click" />
                        </div>
                    </div>
                    <asp:Repeater ID="rptImagenesTemporalesEdicion" OnItemCommand="rptImagenesTemporalesEdicion_ItemCommand" runat="server">
                        <ItemTemplate>
                            <div class="d-flex align-items-center mb-2">
                                <img src='<%# Container.DataItem %>' class="img-thumbnail me-2" style="width: 100px; height: 100px; object-fit: cover;" />
                                <asp:LinkButton ID="btnEliminarImagenEdicion" runat="server" CommandName="EliminarTemporalEdicion" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-danger btn-sm">Eliminar</asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="mb-3">
                        <label>Imágenes:</label>
                        <asp:Repeater ID="rptImagenes" OnItemCommand="rptImagenes_ItemCommand" runat="server">
                            <ItemTemplate>
                                <div class="d-flex align-items-center mb-2">
                                    <img src='<%# Eval("UrlProducto") %>' class="img-thumbnail me-2" style="width: 100px; height: 100px; object-fit: cover;" />
                                    <asp:LinkButton ID="btnEliminarImagen" runat="server" CommandName="EliminarImagen" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger btn-sm" Visible='<%# ((UI.Admin.GestionProductos)Page).EsModoEdicion %>'>Eliminar</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
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
                    <h5 class="modal-title">Agregar Producto</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtNuevoNombre">Nombre:</label>
                        <asp:TextBox ID="txtNuevoNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtNuevoPrecio">Precio:</label>
                        <asp:TextBox ID="txtNuevoPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtNuevoStock">Stock:</label>
                        <asp:TextBox ID="txtNuevoStock" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label>Categoria:</label>
                        <asp:DropDownList ID="ddlNuevaCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Marca:</label>
                        <asp:DropDownList ID="ddlNuevaMarca" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label>Agregar URL de Imagen:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNuevaImagen" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnAgregarImagenTemporal" runat="server" Text="Agregar" CssClass="btn btn-primary" Visible='<%# ((UI.Admin.GestionProductos)Page).EsModoEdicion %>' OnClick="btnAgregarImagenTemporal_Click" />
                        </div>
                    </div>

                    <asp:Repeater ID="rptImagenesTemporales" OnItemCommand="rptImagenesTemporales_ItemCommand" runat="server">
                        <ItemTemplate>
                            <div class="d-flex align-items-center mb-2">
                                <img src='<%# Container.DataItem %>' class="img-thumbnail me-2" style="width: 100px; height: 100px; object-fit: cover;" />
                                <asp:LinkButton ID="btnEliminarImagen" runat="server" CommandName="EliminarImagen" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-danger btn-sm">Eliminar
                                <i class="fas fa-trash"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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

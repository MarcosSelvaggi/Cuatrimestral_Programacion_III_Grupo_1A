<%@ Page Title="Admin - Clientes" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="UI.Admin.GestionClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2><i class="bi bi-people-fill"></i>Gestión de Clientes</h2>

        <%--<div class="mb-3">
            <asp:Button ID="btnAbrirAgregar" runat="server" Text="Agregar Cliente" CssClass="btn btn-outline-primary" OnClick="btnAbrirAgregar_Click" />
        </div>--%>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Email</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Activo</th>
                    <th style="width: 220px; text-align: right;">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptClientes" runat="server" OnItemCommand="rptClientes_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Apellido") %></td>
                            <td><%# Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No" %></td>
                            <td style="text-align: right;">
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-primary me-1" CommandName="Ver" CommandArgument='<%# Eval("Id") %>'>
                                    <i class="fas fa-eye"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-success me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>'>
                                    <i class="fas fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'>
                                    <i class="fas fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <div class="modal fade" id="modalCliente" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="lblModalTitulo" runat="server">Detalle Cliente</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblIdCliente" runat="server" CssClass="d-none"></asp:Label>

                    <div class="row mb-3">
                        <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-danger d-none" />
                        <div class="col">
                            <label>Nombre:</label>
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Visible="false" MaxLength="30" onkeyup="validarLongitudYCaracteresEspeciales(this, 30, 'nombreEditarMsj')"></asp:TextBox>
                            <span id="nombreEditarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Apellido:</label>
                            <asp:Label ID="lblApellido" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Visible="false" MaxLength="20" onkeyup="validarLongitudYCaracteresEspeciales(this, 20, 'apellidoEditarMsj')"></asp:TextBox>
                            <span id="apellidoEditarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label>Email:</label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control d-none" MaxLength="40" onkeyup="validarEmail(this, 'emailEditarMsj')"></asp:TextBox>
                        <span id="emailEditarMsj" class="form-text text-muted"></span>
                    </div>

                    <div class="row mb-3">
                        <div class="col">
                            <label>Provincia:</label>
                            <asp:Label ID="lblProvincia" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" Visible="false" MaxLength="30"
                                onkeyup="validarLongitudYCaracteresEspeciales(this, 30,'provinciaEditarMsj')"></asp:TextBox>
                            <span id="provinciaEditarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Localidad:</label>
                            <asp:Label ID="lblLocalidad" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" Visible="false" MaxLength="30" onkeyup="validarLongitud(this, 30, 'localidadEditarMsj')"></asp:TextBox>
                            <span id="localidadEditarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col">
                            <label>Código Postal:</label>
                            <asp:Label ID="lblCodigoPostal" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" Visible="false" MaxLength="10"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 10, 'cpEditarMsj')"></asp:TextBox>
                            <span id="cpEditarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Dirección:</label>
                            <asp:Label ID="lblDireccion" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Visible="false" MaxLength="50"
                                onkeyup="validarLongitud(this, 50, 'direccionEditarMsj')"></asp:TextBox>
                            <span id="direccionEditarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col">
                            <label>Teléfono:</label>
                            <asp:Label ID="lblTelefono" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Visible="false" MaxLength="20"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 20, 'telefonoEditarMsj')"></asp:TextBox>
                            <span id="telefonoEditarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Documento:</label>
                            <asp:Label ID="lblDocumento" runat="server" CssClass="form-control"></asp:Label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" Visible="false" MaxLength="20"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 20, 'documentoEditarMsj')"></asp:TextBox>
                            <span id="documentoEditarMsj" class="form-text text-muted"></span>
                        </div>
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
                    <asp:Label ID="lblDescripcionEliminar" runat="server"></asp:Label>
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
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar Cliente</h5>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col">
                            <label>Nombre:</label>
                            <asp:TextBox ID="txtNuevoNombre" runat="server" CssClass="form-control" MaxLength="30" onkeyup="validarLongitud(this, 50, 'nombreAgregarMsj')"></asp:TextBox>
                            <span id="nombreAgregarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Apellido:</label>
                            <asp:TextBox ID="txtNuevoApellido" runat="server" CssClass="form-control" MaxLength="50" onkeyup="validarLongitud(this, 30, 'apellidoAgregarMsj')"></asp:TextBox>
                            <span id="apellidoAgregarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label>Email:</label>
                        <asp:TextBox ID="txtNuevoEmail" runat="server" CssClass="form-control" MaxLength="100" onkeyup="validarEmail(this, 'emailAgregarMsj')"></asp:TextBox>
                        <span id="emailAgregarMsj" class="form-text text-muted"></span>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <label>Provincia:</label>
                            <asp:TextBox ID="txtNuevaProvincia" runat="server" CssClass="form-control" MaxLength="50"
                                onkeyup="validarLongitud(this, 50, 'provinciaAgregarMsj')"></asp:TextBox>
                            <span id="provinciaAgregarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Localidad:</label>
                            <asp:TextBox ID="txtNuevaLocalidad" runat="server" CssClass="form-control" MaxLength="50" onkeyup="validarLongitud(this, 50, 'localidadAgregarMsj')"></asp:TextBox>
                            <span id="localidadAgregarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <label>Código Postal:</label>
                            <asp:TextBox ID="txtNuevoCodigoPostal" runat="server" CssClass="form-control" MaxLength="10"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 10, 'cpAgregarMsj')"></asp:TextBox>
                            <span id="cpAgregarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Dirección:</label>
                            <asp:TextBox ID="txtNuevaDireccion" runat="server" CssClass="form-control" MaxLength="100"
                                onkeyup="validarLongitud(this, 100, 'direccionAgregarMsj')"></asp:TextBox>
                            <span id="direccionAgregarMsj" class="form-text text-muted"></span>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <label>Teléfono:</label>
                            <asp:TextBox ID="txtNuevoTelefono" runat="server" CssClass="form-control" MaxLength="20"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 20, 'telefonoAgregarMsj')"></asp:TextBox>
                            <span id="telefonoAgregarMsj" class="form-text text-muted"></span>
                        </div>
                        <div class="col">
                            <label>Documento:</label>
                            <asp:TextBox ID="txtNuevoDocumento" runat="server" CssClass="form-control" MaxLength="20"
                                onkeypress="return soloNumeros(event)"
                                onkeyup="validarLongitud(this, 20, 'documentoAgregarMsj')"></asp:TextBox>
                            <span id="documentoAgregarMsj" class="form-text text-muted"></span>
                        </div>
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

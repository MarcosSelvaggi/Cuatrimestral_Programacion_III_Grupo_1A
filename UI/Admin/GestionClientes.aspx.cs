using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionClientes : System.Web.UI.Page
    {
        private UsuarioManager managerCliente = new UsuarioManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();
            }
        }

        protected void rptClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCliente = Convert.ToInt32(e.CommandArgument);
            var cliente = managerCliente.obtenerPorID(idCliente);

            if (cliente != null)
            {
                if (e.CommandName == "Ver")
                {
                    lblIdCliente.Text = cliente.Id.ToString();
                    lblModalTitulo.InnerText = "Detalle Cliente";

                    lblNombre.Text = cliente.Nombre;
                    lblApellido.Text = cliente.Apellido;
                    lblEmail.Text = cliente.Email;
                    lblDocumento.Text = cliente.Documento;
                    lblProvincia.Text = cliente.Provincia;
                    lblLocalidad.Text = cliente.Localidad;
                    lblCodigoPostal.Text = cliente.CodigoPostal;
                    lblDireccion.Text = cliente.Direccion;
                    lblTelefono.Text = cliente.Telefono;
                    lblActivo.Text = cliente.Activo ? "Sí" : "No";

                    // Muestro solo labels
                    alternarCamposEdicion(false);

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalCliente')); modal.show();", true);
                }
                else if (e.CommandName == "Editar")
                {
                    lblIdCliente.Text = cliente.Id.ToString();
                    lblModalTitulo.InnerText = "Editar Cliente";

                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtEmail.Text = cliente.Email;
                    txtDocumento.Text = cliente.Documento;
                    txtProvincia.Text = cliente.Provincia;
                    txtLocalidad.Text = cliente.Localidad;
                    txtCodigoPostal.Text = cliente.CodigoPostal;
                    txtDireccion.Text = cliente.Direccion;
                    txtTelefono.Text = cliente.Telefono;
                    ddlActivo.SelectedValue = cliente.Activo.ToString().ToLower();

                    // Muestro los campos de edición
                    alternarCamposEdicion(true);

                    lblMensajeError.Text = "";
                    lblMensajeError.CssClass = "alert alert-danger d-none";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalCliente')); modal.show();", true);
                }
                else if (e.CommandName == "Eliminar")
                {
                    lblIdEliminar.Text = cliente.Id.ToString();
                    lblDescripcionEliminar.Text = $"¿Estás seguro que querés eliminar al cliente: <strong>{cliente.Nombre} {cliente.Apellido}</strong>?";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEliminarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
                }
            }
        }

        //protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
        //}

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNuevoNombre.Text.Trim();
            string apellido = txtNuevoApellido.Text.Trim();
            string email = txtNuevoEmail.Text.Trim();
            string documento = txtNuevoDocumento.Text.Trim();
            string provincia = txtNuevaProvincia.Text.Trim();
            string localidad = txtNuevaLocalidad.Text.Trim();
            string codigoPostal = txtNuevoCodigoPostal.Text.Trim();
            string direccion = txtNuevaDireccion.Text.Trim();
            string telefono = txtNuevoTelefono.Text.Trim();
            bool activo = Convert.ToBoolean(ddlNuevoActivo.SelectedValue);


            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(email))
            {
                Usuarios nuevo = new Usuarios
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Provincia = provincia,
                    Localidad = localidad,
                    Documento = documento,
                    CodigoPostal = codigoPostal,
                    Direccion = direccion,
                    Telefono = telefono,
                    Activo = activo
                };

                bool valido = validarCliente(nuevo);
                if (!valido)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", $"var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
                    return;
                }

                managerCliente.agregar(nuevo);
                limpiarCamposAgregar();
                cargarClientes();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblIdCliente.Text);

            Usuarios clienteEditado = new Usuarios
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Documento = txtDocumento.Text.Trim(),
                Provincia = txtProvincia.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                CodigoPostal = txtCodigoPostal.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Activo = Convert.ToBoolean(ddlActivo.SelectedValue)
            };

            bool valido = validarCliente(clienteEditado);
            if (!valido)
            {
                lblMensajeError.Text = "Por favor, verifique los campos ingresados.";
                lblMensajeError.CssClass = "alert alert-danger";
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", $"var modal = new bootstrap.Modal(document.getElementById('modalCliente')); modal.show();", true);
                return;
            }

            managerCliente.modificar(clienteEditado);
            cargarClientes();
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idCliente = Convert.ToInt32(lblIdEliminar.Text);
            managerCliente.eliminar(idCliente);
            cargarClientes();
        }

        private void cargarClientes()
        {
            var lista = managerCliente.listar();
            rptClientes.DataSource = lista;
            rptClientes.DataBind();
            lblMensajeError.Text = "";
            lblMensajeError.CssClass = "alert alert-danger d-none";
        }

        private void limpiarCamposAgregar()
        {
            txtNuevoNombre.Text = "";
            txtNuevoApellido.Text = "";
            txtNuevoEmail.Text = "";
            txtNuevoDocumento.Text = "";
            txtNuevaProvincia.Text = "";
            txtNuevaLocalidad.Text = "";
            txtNuevoCodigoPostal.Text = "";
            txtNuevaDireccion.Text = "";
            txtNuevoTelefono.Text = "";
            ddlNuevoActivo.SelectedValue = "true";
            lblMensajeError.Text = "";
            lblMensajeError.CssClass = "alert alert-danger d-none";
        }

        private void alternarCamposEdicion(bool editar)
        {
            lblNombre.Visible = !editar;
            txtNombre.Visible = editar;

            lblDocumento.Visible = !editar;
            txtDocumento.Visible = editar;

            lblApellido.Visible = !editar;
            txtApellido.Visible = editar;

            lblEmail.Visible = !editar;
            txtEmail.CssClass = editar ? "form-control" : "form-control d-none";

            lblProvincia.Visible = !editar;
            txtProvincia.Visible = editar;

            lblLocalidad.Visible = !editar;
            txtLocalidad.Visible = editar;

            lblCodigoPostal.Visible = !editar;
            txtCodigoPostal.Visible = editar;

            lblDireccion.Visible = !editar;
            txtDireccion.Visible = editar;

            lblTelefono.Visible = !editar;
            txtTelefono.Visible = editar;

            lblActivo.Visible = !editar;
            ddlActivo.Visible = editar;

            btnGuardar.Visible = editar;
        }

        private bool validarCliente(Usuarios cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) ||
                cliente.Nombre.Length > 30 ||
                !Regex.IsMatch(cliente.Nombre, @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ\s]+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Apellido) ||
                cliente.Apellido.Length > 20 ||
                !Regex.IsMatch(cliente.Apellido, @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ\s]+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Email) ||
                cliente.Email.Length > 100 ||
                !Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Documento) ||
                cliente.Documento.Length > 20 ||
                !Regex.IsMatch(cliente.Documento, @"^\d+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Provincia) || cliente.Provincia.Length > 50 || !Regex.IsMatch(cliente.Provincia, @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ\s]+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Localidad) || cliente.Localidad.Length > 50)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.CodigoPostal) ||
                cliente.CodigoPostal.Length > 10 ||
                !Regex.IsMatch(cliente.CodigoPostal, @"^\d+$"))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Direccion) ||
                cliente.Direccion.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Telefono) ||
                cliente.Telefono.Length > 20 ||
                !Regex.IsMatch(cliente.Telefono, @"^\d+$"))
                return false;

            return true;
        }

    }
}
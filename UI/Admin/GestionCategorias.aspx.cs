using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionCategorias : System.Web.UI.Page
    {
        private CategoriaManager managerCategoria = new CategoriaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Usuario/Logearse.aspx", false);
                return;
            }

            Usuarios usuarioLogeado = (Usuarios)Session["Usuario"];

            if (usuarioLogeado.Rol.Id != 1)
            {
                Response.Redirect("/Perfil.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                cargarCategorias();
            }
        }
        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCategoria = Convert.ToInt32(e.CommandArgument);
            var categoria = managerCategoria.obtenerCategoriaPorId(idCategoria);

            if (categoria != null)
            {
                if (e.CommandName == "Ver")
                {
                    lblIdCategoria.Text = categoria.Id.ToString();
                    lblModalTitulo.InnerText = "Detalle Categoría";
                    lblDescripcion.Text = categoria.Descripcion;
                    lblActivo.Text = categoria.Activo ? "Sí" : "No";
                    ddlActivo.SelectedValue = categoria.Activo.ToString().ToLower();
                    lblActivo.Visible = true;
                    ddlActivo.Visible = false;
                    lblDescripcion.Visible = true;
                    txtDescripcion.Visible = false;
                    btnGuardar.Visible = false;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalCategoria')); modal.show();", true);
                }
                else if (e.CommandName == "Editar")
                {
                    lblIdCategoria.Text = categoria.Id.ToString();
                    lblModalTitulo.InnerText = "Editar Categoría";
                    txtDescripcion.Text = categoria.Descripcion;
                    lblActivo.Text = categoria.Activo ? "Sí" : "No";
                    ddlActivo.SelectedValue = categoria.Activo.ToString().ToLower();
                    lblActivo.Visible = false;
                    ddlActivo.Visible = true;
                    lblDescripcion.Visible = false;
                    txtDescripcion.Visible = true;
                    btnGuardar.Visible = true;

                    lblMensajeError.Text = "";
                    lblMensajeError.CssClass = "alert alert-danger d-none";
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalCategoria')); modal.show();", true);
                }
                else if (e.CommandName == "Eliminar")
                {
                    lblIdEliminar.Text = categoria.Id.ToString();
                    lblDescripcionEliminar.Text = $"¿Estás seguro que querés eliminar la categoría: <strong>{categoria.Descripcion}</strong>?";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEliminarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
                }
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(lblIdCategoria.Text);
            string nuevaDescripcion = txtDescripcion.Text;
            bool activo = Convert.ToBoolean(ddlActivo.SelectedValue);

            if (string.IsNullOrWhiteSpace(nuevaDescripcion) || nuevaDescripcion.Length > 20 || (managerCategoria.existeDescripcion(nuevaDescripcion)))
            {
                lblMensajeError.Text = "Por favor, verifique los campos ingresados.";
                lblMensajeError.CssClass = "alert alert-danger";
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalCategoria')); modal.show();", true);
                return;
            }

            Categoria aux = new Categoria { Descripcion = nuevaDescripcion, Id = idCategoria, Activo = activo };

            managerCategoria.modificar(aux);

            cargarCategorias();
        }
        protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = txtNuevaDescripcion.Text.Trim();
            bool activo = Convert.ToBoolean(ddlNuevoActivo.SelectedValue);

            if (string.IsNullOrWhiteSpace(descripcion) || descripcion.Length > 20 || (managerCategoria.existeDescripcion(descripcion)))
            {
                limpiarCamposAgregar();
                lblMensajeErrorAgregar.Text = "Por favor, verifique los campos ingresados.";
                lblMensajeErrorAgregar.CssClass = "alert alert-danger";
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
                return;
            }

            Categoria aux = new Categoria { Descripcion = descripcion, Activo = activo };
            managerCategoria.agregar(aux);
            limpiarCamposAgregar();
            cargarCategorias();
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(lblIdEliminar.Text);
            managerCategoria.eliminar(idCategoria);

            cargarCategorias();
        }
        private void cargarCategorias()
        {
            var lista = new CategoriaManager().listar();
            rptCategorias.DataSource = lista;
            rptCategorias.DataBind();
            lblMensajeError.Text = "";
            lblMensajeError.CssClass = "alert alert-danger d-none";
            lblMensajeErrorAgregar.Text = "";
            lblMensajeErrorAgregar.CssClass = "alert alert-danger d-none";
        }
        private void limpiarCamposAgregar()
        {
            txtNuevaDescripcion.Text = "";
            ddlNuevoActivo.SelectedValue = "true";
            lblMensajeError.Text = "";
            lblMensajeError.CssClass = "alert alert-danger d-none";
            lblMensajeErrorAgregar.Text = "";
            lblMensajeErrorAgregar.CssClass = "alert alert-danger d-none";
        }
    }
}
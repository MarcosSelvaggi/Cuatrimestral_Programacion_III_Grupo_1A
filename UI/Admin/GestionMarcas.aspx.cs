using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionMarcas : System.Web.UI.Page
    {
        private MarcaManager managerMarca = new MarcaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarMarcas();
            }
        }

        protected void rptMarcas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idMarca = Convert.ToInt32(e.CommandArgument);
            var marca = managerMarca.obtenerMarcaPorId(idMarca);

            if (marca != null)
            {
                if (e.CommandName == "Ver")
                {
                    lblIdMarca.Text = marca.Id.ToString();
                    lblModalTitulo.InnerText = "Detalle Marca";
                    lblDescripcion.Text = marca.Descripcion;
                    lblActivo.Text = marca.Activo ? "Sí" : "No";
                    ddlActivo.SelectedValue = marca.Activo.ToString().ToLower();
                    lblActivo.Visible = true;
                    ddlActivo.Visible = false;
                    lblDescripcion.Visible = true;
                    txtDescripcion.Visible = false;
                    btnGuardar.Visible = false;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalMarca')); modal.show();", true);
                }
                else if (e.CommandName == "Editar")
                {
                    lblIdMarca.Text = marca.Id.ToString();
                    lblModalTitulo.InnerText = "Editar Marca";
                    txtDescripcion.Text = marca.Descripcion;
                    lblActivo.Text = marca.Activo ? "Sí" : "No";
                    ddlActivo.SelectedValue = marca.Activo.ToString().ToLower();
                    lblActivo.Visible = false;
                    ddlActivo.Visible = true;
                    lblDescripcion.Visible = false;
                    txtDescripcion.Visible = true;
                    btnGuardar.Visible = true;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalMarca')); modal.show();", true);
                }
                else if (e.CommandName == "Eliminar")
                {
                    lblIdEliminar.Text = marca.Id.ToString();
                    lblDescripcionEliminar.Text = $"¿Estás seguro que querés eliminar la marca: <strong>{marca.Descripcion}</strong>?";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEliminarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
                }
            }
        }
        protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMarca = Convert.ToInt32(lblIdMarca.Text);
            string nuevaDescripcion = txtDescripcion.Text;
            bool activo = Convert.ToBoolean(ddlActivo.SelectedValue);

            Marca aux = new Marca { Id = idMarca, Descripcion = nuevaDescripcion, Activo = activo };

            managerMarca.modificar(aux);
            cargarMarcas();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = txtNuevaDescripcion.Text.Trim();
            bool activo = Convert.ToBoolean(ddlNuevoActivo.SelectedValue);
            if (!string.IsNullOrEmpty(descripcion))
            {
                Marca aux = new Marca { Descripcion = descripcion, Activo = activo };
                managerMarca.agregar(aux);
                limpiarCamposAgregar();
                cargarMarcas();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
            }
        }
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idMarca = Convert.ToInt32(lblIdEliminar.Text);
            managerMarca.eliminar(idMarca);
            cargarMarcas();
        }
        private void cargarMarcas()
        {
            var lista = managerMarca.listar();
            rptMarcas.DataSource = lista;
            rptMarcas.DataBind();
        }
        private void limpiarCamposAgregar()
        {
            txtNuevaDescripcion.Text = "";
            ddlNuevoActivo.SelectedValue = "true";
        }
    }
}

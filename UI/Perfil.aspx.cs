using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI
{
    public partial class Perfil : UI.ClaseMaster.BasePage
    {
        public Usuarios UsuarioLogeado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Inicio.aspx", false);
            }
            else if (Request.QueryString["datos"] != null)
            {
                if (Int32.TryParse(Request.QueryString["datos"], out int resultado))
                {
                    if (resultado == 1)
                    {
                        HabilitarModificacionDatos();
                    }
                    else
                    {
                        Response.Redirect("/Perfil.aspx", false);
                    }
                }

            }

            UsuarioLogeado = new Usuarios();
            UsuarioLogeado = (Usuarios)Session["Usuario"];

            if (!IsPostBack)
            {
                txtNombre.Text = UsuarioLogeado.Nombre;
                txtApellido.Text = UsuarioLogeado.Apellido;
                txtDocumento.Text = UsuarioLogeado.Documento;

                txtEmail.Text = UsuarioLogeado.Email;
                txtTelefono.Text = UsuarioLogeado.Telefono;

                txtProvincia.Text = UsuarioLogeado.Provincia;
                txtLocalidad.Text = UsuarioLogeado.Localidad;

                txtDireccion.Text = UsuarioLogeado.Direccion;
                txtCodigoPostal.Text = UsuarioLogeado.CodigoPostal;
            }


        }

        protected void BtnModificarDatos_Click(object sender, EventArgs e)
        {

            UsuarioLogeado.Nombre = txtNombre.Text;
            UsuarioLogeado.Apellido = txtApellido.Text;
            UsuarioLogeado.Documento = txtDocumento.Text;

            UsuarioLogeado.Email = txtEmail.Text;
            UsuarioLogeado.Telefono = txtTelefono.Text;

            UsuarioLogeado.Provincia = txtProvincia.Text;
            UsuarioLogeado.Localidad = txtLocalidad.Text;

            UsuarioLogeado.Direccion = txtDireccion.Text;
            UsuarioLogeado.CodigoPostal = txtCodigoPostal.Text;


            if (DatosCargadosCorrectamente())
            {
                UsuarioManager usuarioManager = new UsuarioManager();
                usuarioManager.modificar(UsuarioLogeado);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCambiadosCorrectamenteModal",
                "var modal = new bootstrap.Modal(document.getElementById('datosCambiadosCorrectamenteModal')); modal.show();" +
                "setTimeout(function() { window.location.href = '/Perfil.aspx'; }, 5000);", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCargadosIncorrectamenteModal",
                "var modal = new bootstrap.Modal(document.getElementById('datosCargadosIncorrectamenteModal')); modal.show();", true);
                return;
            }
        }

        private void HabilitarModificacionDatos()
        {
            BtnModificarDatos.Visible = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDocumento.Enabled = true;

            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;

            txtProvincia.Enabled = true;
            txtLocalidad.Enabled = true;

            txtDireccion.Enabled = true;
            txtCodigoPostal.Enabled = true;
        }

        private bool DatosCargadosCorrectamente()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtProvincia.Text) || string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(ListarProvincias));
            }
        }

        public async Task ListarProvincias()
        {
            var url = "https://apis.datos.gob.ar/georef/api/provincias";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    var respuestaString = await respuesta.Content.ReadAsStringAsync();
                    var resultado = JsonSerializer.Deserialize<ListaDeProvincias>(respuestaString,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    var provincias = resultado?.Provincias.OrderBy(p => p.Nombre).ToList();

                    ddlProvincia.DataSource = provincias;

                    ddlProvincia.DataTextField = "Nombre";
                    ddlProvincia.DataValueField = "Nombre";
                    ddlProvincia.DataBind();


                    ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione una Provincia --", ""));
                }
            }
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedIndex != 0)
            {
                RegisterAsyncTask(new PageAsyncTask(ListarLocalidades));
                ddlLocalidad.Enabled = true;
                btnRegistro.Enabled = true;
            }
            else
            {
                ddlLocalidad.Enabled = false;
                btnRegistro.Enabled = false;
            }
        }

        public async Task ListarLocalidades()
        {
            var url = "https://apis.datos.gob.ar/georef/api/localidades?provincia=" + ddlProvincia.SelectedValue + "&campos=id,nombre&max=500";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    var respuestaString = await respuesta.Content.ReadAsStringAsync();
                    var resultado = JsonSerializer.Deserialize<ListaDeLocalidades>(respuestaString,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    var Localidades = resultado?.Localidades.OrderBy(p => p.Nombre).ToList();

                    ddlLocalidad.DataSource = Localidades;
                    ddlLocalidad.DataTextField = "Nombre";
                    ddlLocalidad.DataValueField = "Nombre";
                    ddlLocalidad.DataBind();
                }
            }
        }
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            if (DatosCorrectos())
            {
                Usuarios NuevoUsuario = new Usuarios()
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Constraseña = txtContraseña.Text,
                    Telefono = txtTeléfono.Text,
                    Documento = txtDocumento.Text,
                    Provincia = ddlProvincia.SelectedValue,
                    Localidad = ddlLocalidad.SelectedValue,
                    Direccion = txtDirección.Text,
                    CodigoPostal = txtCodigoPostal.Text
                };
                UsuarioManager usuarioManager = new UsuarioManager();

                if (usuarioManager.MailYaRegistrado(txtEmail.Text))
                {
                    //Mail registrado
                    problemaAlRegistrarseModalH1.InnerText = "❌ Mail registrado";
                    pDatosErroneosModal.InnerText = "El mail ya se encuentra registrado.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "problemaAlRegistrarseModal",
                   "var modal = new bootstrap.Modal(document.getElementById('problemaAlRegistrarseModal')); modal.show();", true);
                    return;
                }
                else if (usuarioManager.DocumentoYaRegistrado(txtDocumento.Text))
                {
                    //Documento registrado
                    problemaAlRegistrarseModalH1.InnerText = "❌ Documento registrado";
                    pDatosErroneosModal.InnerText = "El documento ya se encuentra registrado.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "problemaAlRegistrarseModal",
                    "var modal = new bootstrap.Modal(document.getElementById('problemaAlRegistrarseModal')); modal.show();", true);
                    return;
                }

                int resultado = usuarioManager.agregar(NuevoUsuario);
                if (resultado != 0)
                {
                    NuevoUsuario.Id = resultado;
                    Session.Add("Usuario", NuevoUsuario);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "usuarioRegistradoModal",
                    "var modal = new bootstrap.Modal(document.getElementById('usuarioRegistradoModal')); modal.show();" +
                    "setTimeout(function() { window.location.href = '/Perfil.aspx'; }, 5000);", true);
                    return;
                }
                else
                {
                    //No pudo registrar al usuario 
                    problemaAlRegistrarseModalH1.InnerText = "❌ Error inesperado";
                    pDatosErroneosModal.InnerText = "Hubo un error al registrar el usuario, intente más tarde.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "problemaAlRegistrarseModal",
                   "var modal = new bootstrap.Modal(document.getElementById('problemaAlRegistrarseModal')); modal.show();", true);
                    return;
                }

            }
            else
            {
                //Campos incompletos
                problemaAlRegistrarseModalH1.InnerText = "❌ Campos incompletos";
                pDatosErroneosModal.InnerText = "Debe completar todos los campos para poder registrarse.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "problemaAlRegistrarseModal",
                   "var modal = new bootstrap.Modal(document.getElementById('problemaAlRegistrarseModal')); modal.show();", true);
                return;
            }
        }

        private bool DatosCorrectos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtTeléfono.Text) || string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtDirección.Text) || string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
                return false;
            return true;
        }
    }
}
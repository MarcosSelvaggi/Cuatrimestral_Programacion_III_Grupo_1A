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
    public partial class Perfil : UI.ClaseMaster.BasePage
    {
        public Usuarios UsuarioLogeado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si no hay usuario en la sesión devuelve al inicio 
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Inicio.aspx", false);
            }

            if (Request.QueryString["datos"] != null) //Variable pasada por URL que sirve para habilitar la modificación de los datos
            {
                if (Int32.TryParse(Request.QueryString["datos"], out int resultado))
                {
                    if (resultado == 1)
                    {
                        if (!IsPostBack) //Si no es Postback habilita la los textbox para modificar la información
                        {
                            HabilitarModificacionDatos();
                            RegisterAsyncTask(new PageAsyncTask(ListarProvincias)); //Llamada de la API para cargar las provincias y sus localidades
                        }
                    }
                    else
                    {
                        Response.Redirect("/Perfil.aspx", false);
                        return;
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
            if (DatosCargadosCorrectamente())
            {
                UsuarioLogeado.Nombre = txtNombre.Text;
                UsuarioLogeado.Apellido = txtApellido.Text;
                UsuarioLogeado.Documento = txtDocumento.Text;

                UsuarioLogeado.Email = txtEmail.Text;
                UsuarioLogeado.Telefono = txtTelefono.Text;

                UsuarioLogeado.Provincia = ddlProvincia.SelectedValue;
                UsuarioLogeado.Localidad = ddlLocalidad.SelectedValue;

                UsuarioLogeado.Direccion = txtDireccion.Text;
                UsuarioLogeado.CodigoPostal = txtCodigoPostal.Text;

                UsuarioManager usuarioManager = new UsuarioManager();

                if (usuarioManager.MailYaRegistrado(txtEmail.Text))
                {
                    //Mail registrado
                    datosCargadosIncorrectamenteH1.InnerText = "❌ Mail registrado";
                    pDatosErroneosModal.InnerText = "El mail ya se encuentra registrado.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCargadosIncorrectamenteModal",
                   "var modal = new bootstrap.Modal(document.getElementById('datosCargadosIncorrectamenteModal')); modal.show();", true);
                    return;
                }
                else if (usuarioManager.DocumentoYaRegistrado(txtDocumento.Text))
                {
                    //Documento registrado
                    datosCargadosIncorrectamenteH1.InnerText = "❌ Documento registrado";
                    pDatosErroneosModal.InnerText = "El documento ya se encuentra registrado.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCargadosIncorrectamenteModal",
                    "var modal = new bootstrap.Modal(document.getElementById('datosCargadosIncorrectamenteModal')); modal.show();", true);
                    return;
                }

                usuarioManager.modificar(UsuarioLogeado);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCambiadosCorrectamenteModal",
                "var modal = new bootstrap.Modal(document.getElementById('datosCambiadosCorrectamenteModal')); modal.show();" +
                "setTimeout(function() { window.location.href = '/Perfil.aspx'; }, 5000);", true);
                return;
            }
            else
            {
                datosCargadosIncorrectamenteH1.InnerHtml = "❌ Ocurrió un error";
                pDatosErroneosModal.InnerHtml = "No se puede modificar el perfil con datos vacíos"; 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "datosCargadosIncorrectamenteModal",
                "var modal = new bootstrap.Modal(document.getElementById('datosCargadosIncorrectamenteModal')); modal.show();", true);
                return;
            }
        }

        private void HabilitarModificacionDatos()
        {
            CambiarDatos.Visible = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDocumento.Enabled = true;

            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;

            divTxtProvincia.Visible = false;
            divTxtLocalidad.Visible = false;

            divDdlProvincia.Visible = true;
            divDdlLocalidad.Visible = true;

            txtDireccion.Enabled = true;
            txtCodigoPostal.Enabled = true;
        }

        //Si hay algún dato vacio o con espacios devuelve que no están cargados correctamente 
        private bool DatosCargadosCorrectamente()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Hace el get de la API y carga las provincias en el Drop Down List 
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

                    //Asigna el resultado del JsonSerializer y lo ordena por nombre 
                    var provincias = resultado?.Provincias.OrderBy(p => p.Nombre).ToList();

                    ddlProvincia.DataSource = provincias;

                    ddlProvincia.DataTextField = "Nombre";
                    ddlProvincia.DataValueField = "Nombre";
                    ddlProvincia.DataBind();

                    ddlProvincia.SelectedValue = UsuarioLogeado.Provincia;
                    RegisterAsyncTask(new PageAsyncTask(ListarLocalidades));

                }
            }
        }

        //Si cambia la selección de la provincia, hace el llamado al método que carga las localidades de esa provincia
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(ListarLocalidades));
        }

        //Hace el llamado del a API para obtener las localidades de la provincia usando cómo parámetro el SelectedValue de la misma
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

                    //Si la provincia seleccioanda es la misma que la que está en Session y NO es PostBack, le asigna la localidad que tiene en Session
                    if (ddlProvincia.SelectedValue == UsuarioLogeado.Provincia && !IsPostBack)
                    {
                        ddlLocalidad.SelectedValue = UsuarioLogeado.Localidad;
                    }

                }
            }
        }
    }
}
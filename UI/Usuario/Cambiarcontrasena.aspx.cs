using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Services;
using static System.Collections.Specialized.BitVector32;

namespace UI.Usuario
{
    public partial class Cambiarcontrasena : System.Web.UI.Page
    {

        private Usuarios usuarioModificado {  get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Inicio.aspx", false);
                return;
            }
            else
            {
                usuarioModificado = (Usuarios)Session["Usuario"];
            }

            if (Session["codigoRecuperacion"] != null)
            {
                divIngresarCodigo.Visible = true;
                divIngresarContraseña.Visible = false;
            }
            else
            {
                divIngresarContraseña.Visible = true;
                divIngresarCodigo.Visible = false;
            }
        }
        //Genera el número aleatorio que será enviado via mail
        private int numRandom()
        {
            Random random = new Random();
            int aux = random.Next(100000, 999999);
            return aux;
        }

        //Envia el código al mail del usuario
        protected void btnEnviarCodigoMail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtContraseñaNueva.Text))
            {
                string codigo = numRandom().ToString();
                Session.Add("codigoRecuperacion", codigo);

                EmailService emailService = new EmailService();
                emailService.correoCambioContrasena(usuarioModificado.Email, codigo);
                emailService.enviarEmail();

                Session.Add("Contraseña", txtContraseñaNueva.Text);

                Response.Redirect("/Usuario/Cambiarcontrasena.aspx", false);
                return;
            }
            else
            {
                SmallCotraseñaNueva.InnerHtml = "Debes ingresar una contraseña";
            }
        }

        //Cambia la contraseña del usuario y redirecciona al perfil del mismo
        protected void btnCodigoMail_Click(object sender, EventArgs e)
        {
            if (txtCodigoMail.Text != Session["codigoRecuperacion"].ToString())
            {
                smallCodigoIncorrecto.InnerHtml = "Código erroneo";
                return;
            }
            else
            {
                UsuarioManager usuarioManager = new UsuarioManager();
                usuarioModificado = usuarioManager.modificarContraseña(usuarioModificado, (string)Session["Contraseña"]);

                EliminarDatosSession();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "contrasenaCambiadaModal",
                  "var modal = new bootstrap.Modal(document.getElementById('contrasenaCambiadaModal')); modal.show();" +
                  "setTimeout(function() { window.location.href = '/Perfil.aspx'; }, 5000);", true);
                return;
            }
        }

        //Boton de volver, borra todos los objetos en la sesión para que tenga que empezar devuelta 
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            EliminarDatosSession();
            Response.Redirect("/Perfil.aspx", false);
            return;
        }

        protected void EliminarDatosSession()
        {
            Session.Remove("Contraseña");
            Session.Remove("codigoRecuperacion");
        }
    }
}
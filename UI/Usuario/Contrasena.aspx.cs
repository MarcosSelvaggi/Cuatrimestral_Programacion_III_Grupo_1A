using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Services;
using System.Security.Cryptography;
using Dominio;
using System.Threading.Tasks;

namespace UI
{
    public partial class Contrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["codigoRecuperacion"] != null)
            {
                divIngresarCodigo.Visible = true;
                divIngresarMail.Visible = false;
                divIngresarContraseña.Visible = false;
            }
            else if (Session["ModificarContraseña"] != null)
            {
                divIngresarContraseña.Visible = true;
                divIngresarMail.Visible = false;
                divIngresarCodigo.Visible = false;
            }
            else
            {
                divIngresarContraseña.Visible = false;
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
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            Usuarios usuarioModificado = usuarioManager.buscarMail(txtMail.Text);

            if (usuarioModificado.Id != -1)
            {
                mailNoEncontrado.InnerHtml = "";
                string codigo = numRandom().ToString();
                //Codigo hardcodeado para pruebas, se puede borrar 
                //string codigo = "222"; 
                Session.Add("codigoRecuperacion", codigo);

                EmailService emailService = new EmailService();
                emailService.correoContrasenaOlvidada(txtMail.Text, codigo);
                emailService.enviarEmail();

                Session.Add("UsuarioAModificar", usuarioModificado);

                Response.Redirect("/Usuario/Contrasena.aspx", false);
            }
            else
            {
                mailNoEncontrado.InnerHtml = "Mail no encontrado";
            }
        }

        //Revisa que el código ingresado por el usuario esté correcto o al menos no esté vacio
        protected void btnCodigoMail_Click(object sender, EventArgs e)
        {
            if (txtCodigoMail.Text.Length == 0)
            {
                smallCodigoIncorrecto.InnerHtml = "Error, debes ingresar el código que fue enviado a tu mail";
            }
            else if (txtCodigoMail.Text != Session["codigoRecuperacion"].ToString())
            {
                smallCodigoIncorrecto.InnerHtml = "Código erroneo";
                return;
            }
            else
            {
                Session.Add("ModificarContraseña", (Usuarios)Session["UsuarioAModificar"]);
                //Borra los objetos en session para que no interfieran con el PageLoad
                Session.Remove("codigoRecuperacion");
                Session.Remove("UsuarioAModificar");

                Response.Redirect("Contrasena.aspx", false);
            }
        }

        //Cambia la contraseña del usuario y redirecciona al perfil del mismo
        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            Usuarios usuarioModificado = (Usuarios)Session["ModificarContraseña"];

            UsuarioManager usuarioManager = new UsuarioManager();
            usuarioModificado = usuarioManager.modificarContraseña(usuarioModificado, txtContraseñaNueva.Text);

            Session.Remove("ModificarContraseña");
            Session.Add("Usuario", usuarioModificado);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "contrasenaCambiadaModal",
              "var modal = new bootstrap.Modal(document.getElementById('contrasenaCambiadaModal')); modal.show();" + 
              "setTimeout(function() { window.location.href = '/Perfil.aspx'; }, 5000);", true);

        }

        //Boton de volver, borra todos los objetos en la sesión para que tenga que empezar devuelta 
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Session.Remove("codigoRecuperacion");
            Session.Remove("UsuarioAModificar");
            Session.Remove("ModificarContraseña");
            Response.Redirect("/Usuario/Logearse.aspx", false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace UI
{
    public partial class Logearse : System.Web.UI.Page
    {
        public string Mail { get; set; } = "mail@mail.com";
        public string Pass { get; set; } = "pass";
        private Usuarios usuarioLogeado; 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (Mail == txtMail.Text && Pass == txtPass.Text)
            {
                //Usuario hardcodeado para ser usado luego 
                usuarioLogeado = new Usuarios
                {
                    Id = 1,
                    Nombre = "Marcos",
                    Apellido = "Selvaggi",
                    Email = Mail,
                    Constraseña = Pass,
                    Estado = true,
                    Documento = 12112312,
                    Provincia = "Buenos Aires",
                    Localidad = "Virreyes",
                    CodigoPostal = "1633",
                    Direccion = "Calle falsa 123",
                    Telefono = "11111111"
                };
                usuarioLogeado.Rol.Id = 1;

                Session.Add("Usuario", usuarioLogeado); 
                
                Response.Redirect("/Perfil.aspx", false);
            }
            else
            {
            }
        }
    }
}
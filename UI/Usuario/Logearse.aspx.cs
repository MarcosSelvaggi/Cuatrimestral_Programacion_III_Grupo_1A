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
    public partial class Logearse : System.Web.UI.Page
    {
        private Usuarios usuarioLogeado;
        private UsuarioManager usuarioManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioManager = new UsuarioManager();
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            usuarioLogeado = usuarioManager.logearse(txtMail.Text, txtPass.Text);

            if (usuarioLogeado.Id != -1)
            {   
                Session.Add("Usuario", usuarioLogeado); 
                
                Response.Redirect("/Perfil.aspx", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "contraseñaIncorrectaModal",
                "var modal = new bootstrap.Modal(document.getElementById('contraseñaIncorrectaModal')); modal.show();", true);
                return;
            }
        }
    }
}
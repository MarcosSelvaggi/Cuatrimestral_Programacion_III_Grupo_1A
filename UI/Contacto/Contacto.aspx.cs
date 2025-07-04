using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using static System.Net.Mime.MediaTypeNames;

namespace UI
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarConsulta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMail.Text) || !string.IsNullOrWhiteSpace(txtConsulta.Text) || !string.IsNullOrWhiteSpace(txtNombre.Text)){
                EmailService emailService = new EmailService();
                emailService.correoConsulta(txtMail.Text, txtNombre.Text, txtConsulta.Text);
                emailService.enviarEmail();
                emailService.correoConsultaInterno(txtNombre.Text, txtConsulta.Text); 
                emailService.enviarEmail();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "contactoExitosoModal",
                    "var modal = new bootstrap.Modal(document.getElementById('contactoExitosoModal')); modal.show();", true);

            }
        }
    }
}
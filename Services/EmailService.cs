using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Services
{
    public class EmailService
    {
        private MailMessage email;
        private readonly SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            string user = ConfigurationManager.AppSettings["EmailUsername"];
            string pass = ConfigurationManager.AppSettings["EmailPassword"];
            server.Credentials = new NetworkCredential(user, pass);
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void correoContrasenaOlvidada(string emailDestino, string codigoParaIngresar)
        {
            email = new MailMessage();
            email.From = new MailAddress("correoprogramaciontp@gmail.com", "Tienda Electronics Store");
            email.To.Add(emailDestino);
            email.Subject = "Código de recuperación";
            email.IsBodyHtml = true;
            email.Body = "<img src='https://i.ibb.co/F4xqv50Q/logo.png' alt='Logo' style='width:150px;' /><br/>" +
                "<h2>Código para recuperar la contraseña</h2>" +
                "<p>Tu código para cambiar la contraseña es el siguiente " + codigoParaIngresar + "</p>" +
                "<p>Si tu no has sido quien solicito el código recomendamos cambiar la contraseña</p>";
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

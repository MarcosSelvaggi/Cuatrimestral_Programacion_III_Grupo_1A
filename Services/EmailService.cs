using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

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
            email.Body = "<img src='https://i.ibb.co/7qTWCSz/logo.png' alt='Logo' style='width:150px;' /><br/>" +
                "<h2>Código para recuperar la contraseña</h2>" +
                "<p>Tu código para cambiar la contraseña es el siguiente " + codigoParaIngresar + "</p>" +
                "<p>Si tu no has sido quien solicito el código recomendamos cambiar la contraseña</p>";
        }

        //Es lo mismo que el de arriba, cambia ligeramente el mensaje
        public void correoCambioContrasena(string emailDestino, string codigoCambioContrasena)
        {
            email = new MailMessage();
            email.From = new MailAddress("correoprogramaciontp@gmail.com", "Tienda Electronics Store");
            email.To.Add(emailDestino);
            email.Subject = "Código de recuperación";
            email.IsBodyHtml = true;
            email.Body = "<img src='https://i.ibb.co/7qTWCSz/logo.png' alt='Logo' style='width:150px;' /><br/>" +
                "<h2>Código para cambiar la contraseña</h2>" +
                "<p>Tu código para cambiar la contraseña es el siguiente " + codigoCambioContrasena + "</p>";
        }

        public void correoConsulta(string emailDestino, string nombreConsulta, string descripciónConsulta)
        {
            email = new MailMessage();
            email.From = new MailAddress("correoprogramaciontp@gmail.com", "Tienda Electronics Store");
            email.To.Add(emailDestino);
            email.Subject = "Copia de consulta";
            email.IsBodyHtml = true;
            email.Body = "<img src='https://i.ibb.co/7qTWCSz/logo.png' alt='Logo' style='width:150px;' /><br/>"+
                         "<H1>Buenas " + nombreConsulta + "</H1>" +
                         "<H2>A continuación se deja la constancia de su consulta, en breve nos contactaremos con usted</H2>" +
                         "<P></P><P></P><P></P>" +
                         "<H3>" + descripciónConsulta + "</H3>";
        }   

        public void correoConsultaInterno(string nombreConsulta, string descripciónConsulta)
        {
            email = new MailMessage();
            email.From = new MailAddress("correoprogramaciontp@gmail.com", "Tienda Electronics Store");
            email.To.Add("correoprogramaciontp@gmail.com");
            email.Subject = "Copia de consulta";
            email.IsBodyHtml = true;
            email.Body = "<img src='https://i.ibb.co/7qTWCSz/logo.png' alt='Logo' style='width:150px;' /><br/>" +
                         "<H1>Buenas " + nombreConsulta + "</H1>" +
                         "<H2>A continuación se deja la constancia de su consulta, en breve nos contactaremos con usted</H2>" +
                         "<P></P><P></P><P></P>" +
                         "<H3>" + descripciónConsulta + "</H3>";
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

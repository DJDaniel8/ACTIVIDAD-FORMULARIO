using ACTIVIDAD_FORMULARIO.Models;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using MailKit.Security;
using MimeKit;

namespace ACTIVIDAD_FORMULARIO.Services
{
    public class EmailSender
    {
        private IConfiguration configuration;

        string email = "UMGDesarrolloWeb@gmail.com";
        private  string pwd;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
            pwd = this.configuration.GetSection("SMTP").GetSection("ClaveSecreta").Value;
        }

        public void SendEmail(Alumno alumno)
        {
            var fromAddress = new MailAddress(email);
            var toAddress = new MailAddress(alumno.Correo);

            string subject = $"{alumno.Nombre}, Gracias Hemos recibido tus datos";
            string body = $"Nombre: {alumno.Nombre}\n";
            body += $"Carnet: {alumno.Carnet}\n";
            body += $"Fecha Nacimiento: {alumno.FechaNacimiento.ToShortDateString()}\n";
            body += $"Correo: {alumno.Correo}";
            var Credenciales = new NetworkCredential(email, pwd);

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(email, pwd);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;
            QRGenerator.Generar(alumno.Carnet, bodyBuilder, body);


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Remitente", pwd));
            message.To.Add(new MailboxAddress("Destinatario", alumno.Correo));
            message.Subject = subject;

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (smtp)
                {


                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
                Console.WriteLine("Correo enviado exitosamente.");
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

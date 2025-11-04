using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Interfaces;
using Services.Facade;

namespace Services__ArqBase_.Facade
{
    /// <summary>
    /// Clase base abstracta (Plantilla) para servicios de envío de correo electrónico.
    /// Define la lógica común de SMTP y debe ser heredada por una implementación
    /// concreta (ej: <see cref="GmailService"/>) que provea las credenciales y el host.
    /// </summary>
    internal abstract class MailService
    {
        private SmtpClient SmtpClient;
        /// <summary>
        /// La dirección de correo electrónico del remitente.
        /// </summary>
        protected string senderMail { get; set; }

        /// <summary>
        /// La contraseña de la cuenta del remitente (o una contraseña de aplicación).
        /// </summary>
        protected string password { get; set; }

        /// <summary>
        /// El host del servidor SMTP (ej: "smtp.gmail.com").
        /// </summary>
        protected string host { get; set; }

        /// <summary>
        /// El puerto del servidor SMTP (ej: 587).
        /// </summary>
        protected int port { get; set; }

        /// <summary>
        /// Especifica si se debe usar SSL/TLS para la conexión.
        /// </summary>
        protected bool ssl { get; set; }

        private ILogger logger;


        /// <summary>
        /// Inicializa la instancia de <see cref="SmtpClient"/> con las credenciales
        /// y configuración de host/puerto/ssl provistas por la clase hija.
        /// También inicializa el logger.
        /// </summary>
        protected void initializeSmtpClient()
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new NetworkCredential(senderMail, password);
            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = ssl;
            logger = LoggerService.GetLogger();
        }


        /// <summary>
        /// Construye y envía un correo electrónico a uno o más destinatarios.
        /// </summary>
        /// <param name="subject">El asunto (subject) del correo.</param>
        /// <param name="body">El cuerpo (body) del correo (como texto plano).</param>
        /// <param name="recipientMail">Una lista de strings con las direcciones de email de los destinatarios.</param>
        /// <remarks>
        /// Si ocurre un error durante el envío, la excepción será capturada,
        /// logueada (usando <see cref="ILogger"/>), y el método finalizará
        /// limpiamente (Dispose). No propaga la excepción.
        /// </remarks>
        public void sendMail(string subject, string body, List<string> recipientMail)
        {
            var mailMessage = new MailMessage();

            try
            {
                mailMessage.From = new MailAddress(senderMail);
                foreach (string mail in recipientMail)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient.Send(mailMessage);
                    
                
            }
            catch (Exception ex) 
            {
                logger.Error($"error enviando email a {String.Join(",", recipientMail)}, error: {ex.Message}");
            }
            finally
            {
                mailMessage.Dispose();
                SmtpClient.Dispose();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Facade;
using Services__ArqBase_.Facade;

namespace Services__ArqBase_.Bll
{

    /// <summary>
    /// Implementación concreta de <see cref="MailService"/> configurada
    /// específicamente para enviar correos a través del servidor SMTP de Gmail.
    /// </summary>
    /// <remarks>
    /// Las credenciales (SenderEmail, ContraseñaEmail) se leen desde el App.config.
    /// Es fundamental que la cuenta de Gmail utilizada tenga habilitado el acceso
    /// de "aplicaciones menos seguras" o utilice una "contraseña de aplicación".
    /// </remarks>
    internal class GmailService : MailService
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="GmailService"/>.
        /// Configura las propiedades (Sender, Password, Host, Port, SSL)
        /// con los valores específicos para Gmail y llama al inicializador del cliente SMTP.
        /// </summary>
        public GmailService()
        {
            senderMail = ConfigurationManager.AppSettings["SenderEmail"].ToString();
            string encryptedPassword = ConfigurationManager.AppSettings["ContraseñaEmail"].ToString();
            password = CryptographyService.Decrypt(encryptedPassword);
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }


    }
}

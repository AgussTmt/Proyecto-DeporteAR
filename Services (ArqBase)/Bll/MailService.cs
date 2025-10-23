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
    internal abstract class MailService
    {
        private SmtpClient SmtpClient;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        private ILogger logger;

        protected void initializeSmtpClient()
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new NetworkCredential(senderMail, password);
            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = ssl;
            logger = LoggerService.GetLogger();
        }


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

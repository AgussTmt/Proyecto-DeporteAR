using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services__ArqBase_.Facade;

namespace Services__ArqBase_.Bll
{
    internal class GmailService : MailService
    {
        public GmailService()
        {
            senderMail = ConfigurationManager.AppSettings["SenderEmail"].ToString();
            password = ConfigurationManager.AppSettings["ContraseñaEmail"].ToString();
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }


        public string recoverPassword(string userRequesting)
        {

        }
    }
}

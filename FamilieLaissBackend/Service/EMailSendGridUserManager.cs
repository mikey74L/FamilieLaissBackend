using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Service
{
    public class EMailSendGridUserManager: IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            //Eine neue Send-Grid Message erzeugen
            var myMessage = new SendGridMessage();

            //Sender-Adresse festlegen
            myMessage.SetFrom(ConfigurationManager.AppSettings["MailSenderAdress"], ConfigurationManager.AppSettings["MailSenderName"]);

            //Empfänger festlegen
            myMessage.AddTo(message.Destination);

            //Den Betreff festlegen
            myMessage.Subject = message.Subject;

            //Den Text der Mail als HTML-Content setzen
            myMessage.AddContent(MimeType.Html, message.Body);

            //Send-Grid-Credentials zur Anmeldung festlegen
            string APIKey = ConfigurationManager.AppSettings["SendGridAPIKey"];

            //Einen Web-Transport für Send-Grid erzeugen
            SendGridClient client = new SendGridClient(APIKey);

            //Die eMail über Send-Grid versenden
            await client.SendEmailAsync(myMessage);
        }
    }
}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Service
{
    public class EMailMailtrapUserManager : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //Einen neuen SMTP-Client initialisieren für den Zugriff auf mailtrap
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.mailtrap.io",
                Port = 2525,
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailtrapUser"], ConfigurationManager.AppSettings["MailtrapPwd"]),
                EnableSsl = true,
            };

            //Sender-Adresse festlegen
            MailAddress fromAdress = new MailAddress(ConfigurationManager.AppSettings["MailSenderAdress"], ConfigurationManager.AppSettings["MailSenderName"]);

            //Empfänger-Adresse festlegen
            MailAddress toAdress = new MailAddress(message.Destination);

            //Neue Mail erzeugen
            MailMessage mail = new MailMessage(fromAdress, toAdress)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
            };
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html));

            //Versenden der Mail Async
            return client.SendMailAsync(mail);
        }
    }
}
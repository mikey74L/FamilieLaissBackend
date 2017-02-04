using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Service
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (FamilieLaissBackend.Properties.Settings.Default.MailServer != "{MailServer}" &&
                FamilieLaissBackend.Properties.Settings.Default.MailUser != "{MailUser}" &&
                FamilieLaissBackend.Properties.Settings.Default.MailPassword != "{MailPassword}" &&
                FamilieLaissBackend.Properties.Settings.Default.MailPort != "{MailPort}" &&
                FamilieLaissBackend.Properties.Settings.Default.MailSender != "{MailSender}" &&
                FamilieLaissBackend.Properties.Settings.Default.MailSenderAdress != "{MailSenderAdress}")
            {
                //Neue Mail-Message erzeugen
                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();

                //Empfänger festlegen
                mailMsg.To.Add(new MailAddress(message.Destination, ""));

                //Sender festlegen
                mailMsg.From = new MailAddress(FamilieLaissBackend.Properties.Settings.Default.MailSenderAdress, FamilieLaissBackend.Properties.Settings.Default.MailSender);

                //Betreff und Body festlegen
                mailMsg.Subject = message.Subject;
                string html = message.Body;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                //Den SMTP-Client initialisieren
                SmtpClient smtpClient = new SmtpClient(FamilieLaissBackend.Properties.Settings.Default.MailServer, Convert.ToInt32(FamilieLaissBackend.Properties.Settings.Default.MailPort));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(FamilieLaissBackend.Properties.Settings.Default.MailUser, FamilieLaissBackend.Properties.Settings.Default.MailPassword);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = FamilieLaissBackend.Properties.Settings.Default.MailSSL;

                //Versenden der Mail
                return Task.Factory.StartNew(() => smtpClient.SendAsync(mailMsg, "token"));
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }
}
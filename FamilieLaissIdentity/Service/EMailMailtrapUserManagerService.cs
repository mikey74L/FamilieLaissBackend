using FamilieLaissIdentity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilieLaissIdentity.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Microsoft.Extensions.Options;

namespace FamilieLaissIdentity.Service
{
    public class EMailMailtrapUserManagerService : IMailSender
    {
        #region Private Members
        private AppSettings AppSettings;
        #endregion

        #region C'tor
        public EMailMailtrapUserManagerService (IOptions<AppSettings> appSettings)
        {
            //Übernehmen der App-Konfig
            AppSettings = appSettings.Value;
        }
        #endregion

        #region Public Methods
        public async Task SendEmailAsync(SendMailModel mailInfo)
        {
            //Einen neuen SMTP-Client initialisieren für den Zugriff auf mailtrap
            SmtpClient client = new SmtpClient();

            //Mit dem SMTP-Server verbinden
            await client.ConnectAsync(AppSettings.MailtrapAdress, Convert.ToInt32(AppSettings.MailtrapPort), false);

            //Da wir kein Token für eine OAUTH Autentifizierung haben entfernen wir dieses Protokoll
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            //Autentifizierung beim Server
            await client.AuthenticateAsync(AppSettings.MailtrapUsername, AppSettings.MailtrapPassword);

            //Erstellen der Mime-Message
            var message = new MimeMessage();

            //Sender-Adresse festlegen
            message.From.Add(new MailboxAddress(AppSettings.MailSenderName, AppSettings.MailSenderAdress));

            //Empfänger-Adresse festlegen
            message.To.Add(new MailboxAddress(mailInfo.ReceiverName, mailInfo.ReceiverAdress));

            //Das Subject zuweisen
            message.Subject = mailInfo.Subject;

            //Den Body zuweisen
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = mailInfo.Body
            };

            //Versenden der Mail Async
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        #endregion
    }
}

using FamilieLaissIdentity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilieLaissIdentity.Models;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace FamilieLaissIdentity.Service
{
    public class EMailSendGridUserManagerService : IMailSender
    {
        #region Private Members
        private AppSettings AppSettings;
        #endregion

        #region C'tor
        public EMailSendGridUserManagerService(IOptions<AppSettings> appSettings)
        {
            //Übernehmen der App-Konfig
            AppSettings = appSettings.Value;
        }
        #endregion

        #region Public Methods
        public async Task SendEmailAsync(SendMailModel mailInfo)
        {
            //Eine neue Send-Grid Message erzeugen
            var myMessage = new SendGridMessage();

            //Sender-Adresse festlegen
            myMessage.SetFrom(AppSettings.MailSenderAdress, AppSettings.MailSenderName);

            //Empfänger festlegen
            myMessage.AddTo(mailInfo.ReceiverAdress, mailInfo.ReceiverName);

            //Den Betreff festlegen
            myMessage.Subject = mailInfo.Subject;

            //Den Text der Mail als HTML-Content setzen
            myMessage.AddContent(MimeType.Html, mailInfo.Body);

            //Send-Grid-Credentials zur Anmeldung festlegen
            string APIKey = AppSettings.SendGridAPIKey;

            //Einen Web-Transport für Send-Grid erzeugen
            SendGridClient client = new SendGridClient(APIKey);

            //Die eMail über Send-Grid versenden
            await client.SendEmailAsync(myMessage);
        }
        #endregion
    }
}

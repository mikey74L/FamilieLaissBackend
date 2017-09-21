using FamilieLaissIdentity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilieLaissIdentity.Models;
using MailKit.Net.Smtp;

namespace FamilieLaissIdentity.Service
{
    public class EMailMailtrapUserManagerService : IMailSender
    {
        public async Task SendEmailAsync(SendMailModel user)
        {
            //Einen neuen SMTP-Client initialisieren für den Zugriff auf mailtrap
            SmtpClient client = new SmtpClient();

            //Mit dem SMTP-Server verbinden
            await client.ConnectAsync("", 12, false);
        }
    }
}

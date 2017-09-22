using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models
{
    public class AppSettings
    {
        #region Mailtrap
        public string MailtrapAdress { get; set; }
        public string MailtrapPort { get; set; }
        public string MailtrapUsername { get; set; }
        public string MailtrapPassword { get; set; }
        #endregion

        #region SendGrid
        public string SendGridAPIKey { get; set; }
        #endregion

        #region Mail General
        public string MailSenderName { get; set; }
        public string MailSenderAdress { get; set; }
        public string MailType { get; set; }
        #endregion
    }
}

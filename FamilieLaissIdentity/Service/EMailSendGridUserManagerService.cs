using FamilieLaissIdentity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilieLaissIdentity.Models;

namespace FamilieLaissIdentity.Service
{
    public class EMailSendGridUserManagerService : IMailSender
    {
        public Task SendEmailAsync(SendMailModel mailData)
        {
            throw new NotImplementedException();
        }
    }
}

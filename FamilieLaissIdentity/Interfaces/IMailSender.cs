using FamilieLaissIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Interfaces
{
    public interface IMailSender
    {
        Task SendEmailAsync(SendMailModel mailData);
    }
}

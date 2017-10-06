using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models;
using FamilieLaissIdentity.Service.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Service
{
    public class MailGeneratorService : IMailGenerator
    {
        public Task<SendMailModel> GenerateRegisterMail(FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL)
        {
            //Deklaration
            SendMailModel ReturnValue = new SendMailModel();

            //Befüllen der Properties für Model
            ReturnValue.IsBodyHtml = true;
            ReturnValue.ReceiverAdress = user.Email;
            ReturnValue.ReceiverName = user.FirstName + " " + user.FamilyName;
            ReturnValue.Subject = MailGenerator_Resources.Subject_Register;

            //Generieren des HTML-Bodies

            //Model zurückliefern
            return null;
        }
    }
}

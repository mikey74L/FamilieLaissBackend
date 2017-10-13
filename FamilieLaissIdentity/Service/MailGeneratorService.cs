using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models;
using Microsoft.Extensions.Localization;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Service
{
    public class MailGeneratorService : IMailGenerator
    {
        #region Private Members
        private readonly IRazorLightEngine razorLightEngine;
        private readonly IStringLocalizer<MailGeneratorService> _Localizer;
        #endregion

        #region C'tor
        public MailGeneratorService(IRazorLightEngine engineRazorLight, IStringLocalizer<MailGeneratorService> localizer)
        {
            //Razor Light Engine aus dem DI-Container übernehmen
            razorLightEngine = engineRazorLight;

            //Localizer aus dem DI-Container übernehmen
            _Localizer = localizer;
        }
        #endregion

        public SendMailModel GenerateRegisterMail(FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL)
        {
            //Deklaration
            SendMailModel ReturnValue = new SendMailModel();
            GenerateMailRegisterUserModel GenerateModel = new GenerateMailRegisterUserModel(user);

            //Befüllen der Properties für Model
            ReturnValue.IsBodyHtml = true;
            ReturnValue.ReceiverAdress = user.Email;
            ReturnValue.ReceiverName = user.FirstName + " " + user.FamilyName;
            ReturnValue.Subject = _Localizer["Subject_Register"];

            //Generieren des HTML-Bodies
            ReturnValue.Body = razorLightEngine.Parse("Register.cshtml", GenerateModel);

            //Model zurückliefern
            return ReturnValue;
        }
    }
}

using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models;
using FamilieLaissIdentity.ViewHelper;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Service
{
    public class MailGeneratorService : IMailGenerator
    {
        #region Private Members
        private readonly IViewRenderer _ViewRenderer;
        private readonly IStringLocalizer<MailGeneratorService> _Localizer;
        private readonly IStringLocalizer<CountrySelectList> _LocalizerCountry;
        private readonly IStringLocalizer<SecurityQuestionList> _LocalizerQuestion;
        #endregion

        #region C'tor
        public MailGeneratorService(IViewRenderer viewRenderer, IStringLocalizer<MailGeneratorService> localizer, IStringLocalizer<CountrySelectList> localizerCountry,
            IStringLocalizer<SecurityQuestionList> localizerQuestion)
        {
            //Razor Light Engine aus dem DI-Container übernehmen
            _ViewRenderer = viewRenderer;

            //Localizer aus dem DI-Container übernehmen
            _Localizer = localizer;
            _LocalizerCountry = localizerCountry;
            _LocalizerQuestion = localizerQuestion;
        }
        #endregion

        public async Task<SendMailModel> GenerateRegisterMail(string urlVerification, FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL)
        {
            //Deklaration
            SendMailModel ReturnValue = new SendMailModel();
            CountrySelectList CountryList = new CountrySelectList(_LocalizerCountry);
            SecurityQuestionList QuestionList = new SecurityQuestionList(_LocalizerQuestion);
            GenerateMailRegisterUserModel GenerateModel = new GenerateMailRegisterUserModel(urlVerification, tokenMailConfirm, user, CountryList, QuestionList);

            //Befüllen der Properties für Return-Value
            ReturnValue.IsBodyHtml = true;
            ReturnValue.ReceiverAdress = user.Email;
            ReturnValue.ReceiverName = user.FirstName + " " + user.FamilyName;
            ReturnValue.Subject = _Localizer["Subject_Register"];

            //Generieren des HTML-Bodies
            ReturnValue.Body = await _ViewRenderer.RenderToStringAsync<GenerateMailRegisterUserModel>("~/Views/MailGenerator/Register.cshtml", GenerateModel);

            //Model zurückliefern
            return ReturnValue;
        }
    }
}

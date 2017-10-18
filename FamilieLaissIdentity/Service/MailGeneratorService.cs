using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models;
using FamilieLaissIdentity.Models.MailGenerator;
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

        public async Task<SendMailModel> GenerateRegisterMail(FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL)
        {
            //Deklaration
            SendMailModel ReturnValue = new SendMailModel();
            CountrySelectList CountryList = new CountrySelectList(_LocalizerCountry);
            SecurityQuestionList QuestionList = new SecurityQuestionList(_LocalizerQuestion);
            GenerateMailRegisterUserModel GenerateModel = new GenerateMailRegisterUserModel(callBackURL, tokenMailConfirm, user, CountryList, QuestionList);

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

        public async Task<SendMailModel> GenerateAdminUnlockAccountMail(FamilieLaissIdentityUser user, FamilieLaissIdentityUser admin)
        {
            //Deklaration 
            SendMailModel ReturnValue = new SendMailModel();
            GenerateMailAdminConfirmAccountModel GenerateModel = new GenerateMailAdminConfirmAccountModel(user);

            //Befüllen der Properties für Return-Value
            ReturnValue.IsBodyHtml = true;
            ReturnValue.ReceiverAdress = admin.Email;
            ReturnValue.ReceiverName = "Administrator";
            ReturnValue.Subject = _Localizer["Subject_AdminUnlockAccount"];

            //Generieren des HTML-Bodies
            ReturnValue.Body = await _ViewRenderer.RenderToStringAsync<GenerateMailAdminConfirmAccountModel>("~/Views/MailGenerator/AdminConfirmAccount.cshtml", GenerateModel);

            //Model zurückliefern
            return ReturnValue;
        }

        public async Task<SendMailModel> GenerateChangePasswordMail(FamilieLaissIdentityUser user, string tokenChangePassword, string callBackURL)
        {
            //Deklaration
            SendMailModel ReturnValue = new SendMailModel();
            GenerateMailChangePasswordModel GenerateModel = new GenerateMailChangePasswordModel(callBackURL, user);

            //Befüllen der Properties für Return-Value
            ReturnValue.IsBodyHtml = true;
            ReturnValue.ReceiverAdress = user.Email;
            ReturnValue.ReceiverName = user.FirstName + " " + user.FamilyName;
            ReturnValue.Subject = _Localizer["Subject_ChangePassword"];

            //Generieren des HTML-Bodies
            ReturnValue.Body = await _ViewRenderer.RenderToStringAsync<GenerateMailChangePasswordModel>("~/Views/MailGenerator/ChangePassword.cshtml", GenerateModel);

            //Model zurückliefern
            return ReturnValue;
        }
    }
}

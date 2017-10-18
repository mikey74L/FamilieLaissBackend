using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.ViewHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.MailGenerator
{
    public class GenerateMailRegisterUserModel
    {
        #region C'tor
        public GenerateMailRegisterUserModel(string urlVerification, string tokenVerification, FamilieLaissIdentityUser user, CountrySelectList countries, SecurityQuestionList questions)
        {
            URL_Verification = urlVerification;
            Token_Verification = tokenVerification;
            UserData = user;
            CountryList = countries;
            SecurityQuestions = questions;
        }
        #endregion

        public readonly string URL_Verification;
        public readonly string Token_Verification;
        public readonly SecurityQuestionList SecurityQuestions;
        public readonly CountrySelectList CountryList;
        public readonly FamilieLaissIdentityUser UserData;
    }
}

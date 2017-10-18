using FamilieLaissIdentity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.MailGenerator
{
    public class GenerateMailChangePasswordModel
    {
        #region C'tor
        public GenerateMailChangePasswordModel(string ChangePasswordURL, FamilieLaissIdentityUser user)
        {
            User = user;
            URL_ChangePassword = ChangePasswordURL;
        }
        #endregion

        public readonly string URL_ChangePassword;
        public readonly FamilieLaissIdentityUser User;
    }
}

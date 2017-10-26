using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class FamilieLaissSigninResultModel
    {
        #region C'tor
        public FamilieLaissSigninResultModel()
        {
            UsernameOrPasswordWrong = false;
            EMailConfirmed = true;
            IsLockedOut = false;
            IsAllowed = true;
            GeneralError = false;
        }
        #endregion

        #region Public Properties
        public bool UsernameOrPasswordWrong { get; set; }

        public bool EMailConfirmed { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsAllowed { get; set; }

        public bool GeneralError { get; set; }

        public bool Succeeded
        {
            get
            {
                if (UsernameOrPasswordWrong || !EMailConfirmed || IsLockedOut || !IsAllowed || GeneralError)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
    }
}

using FamilieLaissIdentity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models
{
    public class GenerateMailAdminConfirmAccountModel
    {
        #region C'tor
        public GenerateMailAdminConfirmAccountModel(FamilieLaissIdentityUser user)
        {
            User = user;
        }
        #endregion

        public readonly FamilieLaissIdentityUser User;
    }
}

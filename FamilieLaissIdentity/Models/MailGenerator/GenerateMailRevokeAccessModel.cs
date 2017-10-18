using FamilieLaissIdentity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.MailGenerator
{
    public class GenerateMailRevokeAccessModel
    {
        #region C'tor
        public GenerateMailRevokeAccessModel(FamilieLaissIdentityUser user)
        {
            User = user;
        }
        #endregion

        public readonly FamilieLaissIdentityUser User;
    }
}

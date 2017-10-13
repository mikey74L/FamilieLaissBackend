using FamilieLaissIdentity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models
{
    public class GenerateMailRegisterUserModel
    {
        #region C'tor
        public GenerateMailRegisterUserModel(FamilieLaissIdentityUser user)
        {
            UserData = user;
        }
        #endregion

        public readonly FamilieLaissIdentityUser UserData;
    }
}

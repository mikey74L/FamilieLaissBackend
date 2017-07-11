using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Account
{
    public class UserInfoDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Roles { get; set; }
    }
}
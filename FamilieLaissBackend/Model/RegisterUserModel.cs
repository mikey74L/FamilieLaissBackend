using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model
{
    public class RegisterUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string eMail { get; set; }
        public int Gender { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Street { get; set; }
        public string HNR { get; set; }
        public string PLZ { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
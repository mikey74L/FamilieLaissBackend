using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Account
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string eMail { get; set; }
        public string Password { get; set; }
        public byte Gender { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Street { get; set; }
        public string HNR { get; set; }
        public string PLZ { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public byte SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Account
{
    public class ForgotPasswordDTO
    {
        public string eMail { get; set; }
        public byte SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
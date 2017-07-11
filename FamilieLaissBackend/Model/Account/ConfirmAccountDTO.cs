using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Account
{
    public class ConfirmAccountDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
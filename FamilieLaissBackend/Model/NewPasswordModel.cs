using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model
{
    public class NewPasswordModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Model
{
    public class IdentityUserExtended : IdentityUser
    {
        public int Geschlecht { get; set; }
        public string Vorname { get; set; }
        public string Familienname { get; set; }
        public string Strasse { get; set; }
        public string HNR { get; set; }
        public string PLZ { get; set; }
        public string Stadt { get; set; }
        public string Land { get; set; }
        public bool IsAllowed { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
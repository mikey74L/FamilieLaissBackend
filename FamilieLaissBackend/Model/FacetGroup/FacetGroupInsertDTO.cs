using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.FacetGroup
{
    public class FacetGroupInsertDTO
    {
        public byte Typ { get; set; } 
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
    }
}
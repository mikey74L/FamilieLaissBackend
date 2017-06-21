using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.FacetValue
{
    public class FacetValueInsertDTO
    {
        public long ID_Group { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
    }
}
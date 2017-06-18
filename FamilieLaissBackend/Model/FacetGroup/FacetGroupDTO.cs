using FamilieLaissBackend.Model.FacetValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.FacetGroup
{
    public class FacetGroupDTO
    {
        public long ID { get; set; }
        public byte Type { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }

        //public List<FacetValueDTO> Values { get; set; }
    }
}
using FamilieLaissBackend.Model.FacetGroup;
using FamilieLaissBackend.Model.MediaItemFacet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.FacetValue
{
    public class FacetValueDTO
    {
        public long ID { get; set; }
        public long ID_Group { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public DateTimeOffset DDL_Create { get; set; }

        public FacetGroupDTO Group { get; set; }
        public List<MediaItemFacetDTO> MediaItemFacets { get; set; }
    }
}
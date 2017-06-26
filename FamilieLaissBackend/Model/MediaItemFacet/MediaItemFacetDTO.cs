using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.MediaItemFacet
{
    public class MediaItemFacetInsertDTO
    {
        public long ID_MediaItem { get; set; }
        public long ID_FacetValue { get; set; }
    }
}
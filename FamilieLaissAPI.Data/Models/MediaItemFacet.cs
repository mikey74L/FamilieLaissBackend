using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaItemFacet
    {
        [Key]
        public long MediaItemFacetId { get; set; }

        public long MediaItemId { get; set; }
        public MediaItem MediaItem { get; set; }

        public long FacetValueId { get; set; }
        public FacetValue FacetValue { get; set; }
    }
}

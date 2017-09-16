﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaItemFacet
    {
        [Key]
        public long MediaItemFacetId { get; set; }

        public long MediaItemId { get; set; }
        public virtual MediaItem MediaItem { get; set; }

        public long FacetValueId { get; set; }
        public virtual FacetValue FacetValue { get; set; }
    }
}

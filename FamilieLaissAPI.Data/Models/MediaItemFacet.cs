using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaItemFacet
    {
        [Key]
        public long MediaItemFacetId { get; set; }

        [Index("IDX_Media_Item_Facet", 1, IsUnique = true)]
        public long MediaItemId { get; set; }
        public virtual MediaItem MediaItem { get; set; }

        [Index("IDX_Media_Item_Facet", 2, IsUnique = true)]
        public long FacetValueId { get; set; }
        public virtual FacetValue FacetValue { get; set; }
    }
}

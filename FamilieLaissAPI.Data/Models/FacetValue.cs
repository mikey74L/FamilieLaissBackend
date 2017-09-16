using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class FacetValue
    {
        [Key]
        public long FacetValueId { get; set; }

        [Required]
        [MaxLength(70)]
        [Column("Name_German")]
        public string NameGerman { get; set; }

        [Required]
        [MaxLength(70)]
        [Column("Name_English")]
        public string NameEnglish { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }

        public long FacetGroupId { get; set; }
        public virtual FacetGroup FacetGroup { get; set; }

        public virtual ICollection<MediaItem> MediaItemFacets { get; set; }
    }
}

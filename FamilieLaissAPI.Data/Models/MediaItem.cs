using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaItem
    {
        [Key]
        public long MediaItemId { get; set; }

        [Required]
        [Index("IDX_Media_Item_Name_German", 1, IsUnique = true)]
        [Index("IDX_Media_Item_Name_English", 1, IsUnique = true)]
        public enMediaType MediaType { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Name_German")]
        [Index("IDX_Media_Item_Name_German", 2, IsUnique = true)]
        public string NameGerman { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Name_English")]
        [Index("IDX_Media_Item_Name_English", 2, IsUnique = true)]
        public string NameEnglish { get; set; }

        [MaxLength(2000)]
        [Column("Description_German")]
        public string DescriptionGerman { get; set; }

        [MaxLength(2000)]
        [Column("Description_English")]
        public string DescriptionEnglish { get; set; }

        [Required]
        [Column("Only_Family")]
        public bool OnlyFamily { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }

        public long MediaGroupId { get; set; }
        public virtual MediaGroup MediaGroup { get; set; }

        public virtual ICollection<MediaItemFacet> MediaItemFacets { get; set; }

        public virtual UploadPictureItem UploadPictureItem { get; set; }

        public virtual UploadVideoItem UploadVideoItem { get; set; }
    }
}

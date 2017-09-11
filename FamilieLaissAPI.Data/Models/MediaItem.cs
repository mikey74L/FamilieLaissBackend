using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaItem
    {
        [Key]
        public long MediaItemId { get; set; }

        [Required]
        public enMediaType MediaType { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Name_German")]
        public string NameGerman { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Name_English")]
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
        public MediaGroup MediaGroup { get; set; }

        public ICollection<MediaItemFacet> MediaItemFacets { get; set; }

        public UploadPictureItem UploadPictureItem { get; set; }

        public UploadVideoItem UploadVideoItem { get; set; }
    }
}

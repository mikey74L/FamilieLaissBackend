using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FamilieLaissAPI.Data.Models
{
    public class MediaGroup
    {
        [Key]
        public long MediaGroupId { get; set; }

        [Required]
        public enMediaType MediaType { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_German")]
        public string NameGerman { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_English")]
        public string NameEnglish { get; set; }

        [MaxLength(300)]
        [Column("Description_German")]
        public string DescriptionGerman { get; set; }

        [MaxLength(300)]
        [Column("Description_English")]
        public string DescriptionEnglish { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }

        public ICollection<MediaItem> MediaItems { get; set; }
    }
}

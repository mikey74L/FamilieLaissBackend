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
    public class MediaGroup
    {
        [Key]
        public long MediaGroupId { get; set; }

        [Required]
        [Index("IDX_Media_Group_Name_German", 1, IsUnique = true)]
        [Index("IDX_Media_Group_Name_English", 1, IsUnique = true)]
        public enMediaType MediaType { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_German")]
        [Index("IDX_Media_Group_Name_German", 2, IsUnique = true)]
        public string NameGerman { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_English")]
        [Index("IDX_Media_Group_Name_English", 2, IsUnique = true)]
        public string NameEnglish { get; set; }

        [MaxLength(300)]
        [Column("Description_German")]
        public string DescriptionGerman { get; set; }

        [MaxLength(300)]
        [Column("Description_English")]
        public string DescriptionEnglish { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }

        public virtual ICollection<MediaItem> MediaItems { get; set; }
    }
}

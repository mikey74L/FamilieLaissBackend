using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FamilieLaissAPI.Data.Models
{
    public class UploadPictureItem
    {
        [Key]
        public long UploadPictureItemId { get; set; }

        [MaxLength(255)]
        [Required]
        [Column("Name_Original")]
        public string NameOriginal { get; set; }

        [Required]
        [Column("Upload_Date")]
        public DateTimeOffset UploadDate { get; set; }

        [Required]
        [Column("Height_Original")]
        public int HeightOriginal { get; set; }

        [Required]
        [Column("Width_Original")]
        public int WidthOriginal { get; set; }

        [Required]
        public enUploadStatus Status { get; set; }
    }
}

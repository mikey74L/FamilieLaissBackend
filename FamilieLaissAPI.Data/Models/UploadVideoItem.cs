using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FamilieLaissAPI.Data.Models
{
    public class UploadVideoItem
    {
        [Key]
        public long UploadVideoItemId { get; set; }

        [MaxLength(255)]
        [Required]
        [Column("Original_Name")]
        public string OriginalName { get; set; }

        [Column("Upload_Date")]
        [Required]
        public DateTimeOffset UploadDate { get; set; }

        [Required]
        [Column("Original_Height")]
        public int OriginalHeight { get; set; }

        [Required]
        [Column("Original_Width")]
        public int OriginalWidth { get; set; }

        [Required]
        [Column("Duration_Hour")]
        public byte DurationHour { get; set; }

        [Required]
        [Column("Duration_Minute")]
        public byte DurationMinute { get; set; }

        [Required]
        [Column("Duration_Second")]
        public byte DurationSecond { get; set; }

        [Required]
        public enUploadStatus Status { get; set; }
    }
}

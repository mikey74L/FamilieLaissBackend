using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class UploadPictureItemExif
    {
        [Key]
        public long UploadPictureItemExifId { get; set; }

        [MaxLength(300)]
        public string Make { get; set; }

        [MaxLength(300)]
        public string Model { get; set; }

        [Column("Resolution_X")]
        public double ResolutionX { get; set; }

        [Column("Resolution_Y")]
        public double ResolutionY { get; set; }

        [Column("Resolution_Unit")]
        public string ResolutionUnit { get; set; }

        public Int16 Orientation { get; set; }

        public DateTimeOffset DDL_Recorded { get; set; }

        [Column("Exposure_Time")]
        public double ExposureTime { get; set; }

        [Column("Exposure_Program")]
        public Int16 ExposureProgramm { get; set; }

        [Column("Exposure_Mode")]
        public Int16 ExposureMode { get; set; }

        public double F_Number { get; set; }

        public int ISO_Sensitivity { get; set; }

        [Column("Shutter_Speed")]
        public double ShutterSpeed { get; set; }

        [Column("Metering_Mode")]
        public Int16 MeteringMode { get; set; }

        [Column("Flash_Mode")]
        public int FlashMode { get; set; }

        [Column("Focal_Length")]
        public double FocalLength { get; set; }

        [Column("Sensing_Mode")]
        public Int16 SensingMode { get; set; }

        [Column("White_Balance_Mode")]
        public Int16 WhiteBalanceMode { get; set; }

        public Int16 Sharpness { get; set; }

        public DbGeography GPS_Location { get; set; }

        public virtual UploadPictureItem UploadPictureItem { get; set; }
    }
}

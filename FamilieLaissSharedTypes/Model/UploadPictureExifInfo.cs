using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissSharedTypes.Model
{
    public class UploadPictureExifInfo
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public double? Resolution_X { get; set; }
        public double? Resolution_Y { get; set; }
        public string Resolution_Unit { get; set; }
        public int? Orientation { get; set; }
        public DateTimeOffset? DDL_Recorded { get; set; }
        public double? Exposure_Time { get; set; }
        public int? Exposure_Programm { get; set; }
        public int? Exposure_Mode { get; set; }
        public double? F_Number { get; set; }
        public int? ISO_Sensitivity { get; set; }
        public double? Shutter_Speed { get; set; }
        public int? Metering_Mode { get; set; }
        public int? Flash_Mode { get; set; }
        public double? Focal_Length { get; set; }
        public int? Sensing_Mode { get; set; }
        public int? White_Balance_Mode { get; set; }
        public int? Sharpness { get; set; }
        public double? Longitute { get; set; }
        public double? Latitude { get; set; }
    }
}

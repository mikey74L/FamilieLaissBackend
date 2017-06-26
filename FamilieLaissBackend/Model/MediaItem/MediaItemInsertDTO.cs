using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.MediaItem
{
    public class MediaItemInsertDTO
    {
        public long ID_Group { get; set; }
        public byte Type { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public string DescriptionGerman { get; set; }
        public string DescriptionEnglish { get; set; }
        public bool OnlyFamily { get; set; }
        public long? ID_UploadPicture { get; set; }
        public long? ID_UploadVideo { get; set; }
    }
}
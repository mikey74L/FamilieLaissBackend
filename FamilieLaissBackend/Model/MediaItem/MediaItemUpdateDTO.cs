using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.MediaItem
{
    public class MediaItemUpdateDTO
    {
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public string DescriptionGerman { get; set; }
        public string DescriptionEnglish { get; set; }
        public bool OnlyFamily { get; set; }
    }
}
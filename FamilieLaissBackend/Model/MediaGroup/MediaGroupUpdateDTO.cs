﻿using FamilieLaissBackend.Model.MediaItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.MediaGroup
{
    public class MediaGroupUpdateDTO
    {
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public string DescriptionGerman { get; set; }
        public string DescriptionEnglish { get; set; }
    }
}
﻿using FamilieLaissBackend.Model.MediaItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.MediaGroup
{
    public class MediaGroupDTO
    {
        public long ID { get; set; }
        public int Type { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public string DescriptionGerman { get; set; }
        public string DescriptionEnglish { get; set; }
        public DateTime DDL_Create { get; set; }

        public List<MediaItemDTO> MediaItems { get; set; }
    }
}
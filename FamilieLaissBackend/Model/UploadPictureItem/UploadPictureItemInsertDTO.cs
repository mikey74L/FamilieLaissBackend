using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.UploadPictureItem
{
    public class UploadPictureItemInsertDTO
    {
        public string NameOriginal { get; set; }
        public int HeightOriginal { get; set; }
        public int WidthOriginal { get; set; }
        public byte Status { get; set; }
   }
}
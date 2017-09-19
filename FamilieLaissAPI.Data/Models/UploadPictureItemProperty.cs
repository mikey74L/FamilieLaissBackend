using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class UploadPictureItemProperty
    {
        [Key]
        public long UploadPictureItemPropertyId { get; set; }

        [Required]
        public byte Rotate { get; set; }

        public virtual UploadPictureItem UploadPictureItem { get; set; }
    }
}

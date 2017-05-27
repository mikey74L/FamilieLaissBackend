//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FamilieLaissBackend.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UploadPictureItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UploadPictureItem()
        {
            this.MediaItems = new HashSet<MediaItem>();
        }
    
        public long ID { get; set; }
        public string NameOriginal { get; set; }
        public System.DateTimeOffset UploadDate { get; set; }
        public int HeightOriginal { get; set; }
        public int WidthOriginal { get; set; }
        public byte Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MediaItem> MediaItems { get; set; }
        public virtual UploadPictureImageProperty ImageProperty { get; set; }
    }
}

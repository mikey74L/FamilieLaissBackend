//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FamilieLaissBackend.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacetValue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacetValue()
        {
            this.MediaItemFacets = new HashSet<MediaItemFacet>();
        }
    
        public long ID { get; set; }
        public long ID_Group { get; set; }
        public string NameGerman { get; set; }
        public string NameEnglish { get; set; }
        public System.DateTimeOffset DDL_Create { get; set; }
    
        public virtual FacetGroup Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MediaItemFacet> MediaItemFacets { get; set; }
    }
}

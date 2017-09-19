using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class Blog
    {
        [Key]
        public long BlogId { get; set; }

        [MaxLength(70)]
        [Required]
        [Index("IDX_Blog_Title", 1, IsUnique = true)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }
    }
}

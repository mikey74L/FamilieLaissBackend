﻿using FamilieLaissAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Models
{
    public class FacetGroup
    {
        [Key]
        public long FacetGroupId { get; set; }

        [Required]
        public enFacetType FacetType { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_German")]
        public string NameGerman { get; set; }

        [MaxLength(70)]
        [Required]
        [Column("Name_English")]
        public string NameEnglish { get; set; }

        [Required]
        public DateTimeOffset DDL_Create { get; set; }

        public virtual ICollection<FacetValue> FacetValues { get; set; }
    }
}

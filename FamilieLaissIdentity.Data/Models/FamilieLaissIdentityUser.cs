﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FamilieLaissIdentity.Data.Models
{
    public class FamilieLaissIdentityUser : IdentityUser
    {
        [Required]
        public int Gender { get; set; }

        [MaxLength(150)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(150)]
        [Required]
        public string FamilyName { get; set; }

        [MaxLength(150)]
        [Required]
        public string Street { get; set; }

        [MaxLength(15)]
        [Required]
        public string Number { get; set; }

        [MaxLength(20)]
        [Required]
        public string ZIP { get; set; }

        [MaxLength(150)]
        [Required]
        public string City { get; set; }

        [MaxLength(10)]
        [Required]
        public string Country { get; set; }
    }
}
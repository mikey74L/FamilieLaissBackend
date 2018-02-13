using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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
        public string Street { get; set; }

        [MaxLength(15)]
        public string Number { get; set; }

        [MaxLength(20)]
        public string ZIP { get; set; }

        [MaxLength(150)]
        public string City { get; set; }

        [MaxLength(10)]
        public string Country { get; set; }

        [MaxLength(5)]
        [Required]
        public string SecurityQuestion { get; set; }

        [MaxLength(100)]
        [Required]
        public string SecurityAnswer { get; set; }

        [Required]
        public bool IsAllowed { get; set; }
    }
}

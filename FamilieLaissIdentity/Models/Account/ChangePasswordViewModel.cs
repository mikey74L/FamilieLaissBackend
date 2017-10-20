using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Display(Order = 1, Name = "Email_Display")]
        [Required(ErrorMessage = "Email_Required")]
        [EmailAddress(ErrorMessage = "Email_Valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Order = 2, Name = "SecurityQuestion_Display")]
        [Required(ErrorMessage = "SecurityQuestion_Required")]
        public string SecurityQuestion { get; set; }

        [Display(Order = 3, Name = "SecurityAnswer_Display")]
        [Required(ErrorMessage = "SecurityAnswer_Required")]
        [StringLength(100, ErrorMessage = "MaxLength")]
        public string SecurityAnswer { get; set; }
    }
}

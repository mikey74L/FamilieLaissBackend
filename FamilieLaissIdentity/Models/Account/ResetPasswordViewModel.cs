using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        [Display(Order = 1, Name = "Password_Display")]
        [Required(ErrorMessage = "Password_Required")]
        [StringLength(100, ErrorMessage = "MaxLength")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Order = 2, Name = "PasswordConfirm_Display")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "PasswordConfirm_Match")]
        public string PasswordConfirmation { get; set; }
    }
}

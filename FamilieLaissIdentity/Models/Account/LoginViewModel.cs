using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class LoginViewModel
    {
        [Display(Order = 1, Name = "UserName_Display")]
        [Required(ErrorMessage = "UserName_Required")]
        [StringLength(50, ErrorMessage = "MaxLength")]
        public string Username { get; set; }

        [Display(Order = 2, Name = "Password_Display")]
        [Required(ErrorMessage = "Password_Required")]
        [StringLength(100, ErrorMessage = "MaxLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Order = 0, Name = "Gender_Display")]
        public int Gender { get; set; }

        [Display(Order = 1, Name = "UserName_Display")]
        [Required(ErrorMessage = "UserName_Required")]
        [StringLength(50, ErrorMessage = "MaxLength")]
        public string UserName { get; set; }

        [Display(Order = 2, Name = "FirstName_Display")]
        [Required(ErrorMessage = "FirstName_Required")]
        [StringLength(150, ErrorMessage = "MaxLength")]
        public string FirstName { get; set; }

        [Display(Order = 3, Name = "FamilyName_Display")]
        [Required(ErrorMessage = "FamilyName_Required")]
        [StringLength(150, ErrorMessage = "MaxLength")]
        public string FamilyName { get; set; }

        [Display(Order = 4, Name = "Street_Display")]
        [Required(ErrorMessage = "Street_Required")]
        [StringLength(150, ErrorMessage = "MaxLength")]
        public string Street { get; set; }

        [Display(Order = 5, Name = "Number_Display")]
        [Required(ErrorMessage = "Number_Required")]
        [StringLength(15, ErrorMessage = "MaxLength")]
        public string Number { get; set; }

        [Display(Order = 6, Name = "ZIP_Display")]
        [Required(ErrorMessage = "ZIP_Required")]
        [StringLength(20, ErrorMessage = "MaxLength")]
        [DataType(DataType.PostalCode)]
        public string ZIP { get; set; }

        [Display(Order = 7, Name = "City_Display")]
        [Required(ErrorMessage = "City_Required")]
        [StringLength(150, ErrorMessage = "MaxLength")]
        public string City { get; set; }

        [Display(Order = 8, Name = "Country_Display")]
        public string Country { get; set; }

        [Display(Order = 9, Name = "Email_Display")]
        [Required(ErrorMessage = "Email_Required")]
        [EmailAddress(ErrorMessage = "Email_Valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Order = 10, Name = "Password_Display")]
        [Required(ErrorMessage = "Password_Required")]
        [StringLength(100, ErrorMessage = "MaxLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Order = 11, Name = "PasswordConfirm_Display")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PasswordConfirm_Match")]
        public string ConfirmPassword { get; set; }

        [Display(Order = 12, Name = "SecurityQuestion_Display")]
        [Required(ErrorMessage = "SecurityQuestion_Required")]
        public string SecurityQuestion { get; set; }

        [Display(Order = 13, Name = "SecurityAnswer_Display")]
        [Required(ErrorMessage = "SecurityAnswer_Required")]
        [StringLength(100, ErrorMessage = "MaxLength")]
        public string SecurityAnswer { get; set; }
    }
}

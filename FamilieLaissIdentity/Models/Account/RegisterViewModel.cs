﻿using FamilieLaissIdentity.Models.Account.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Order = 0, Name = "Gender_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        public int Gender { get; set; }

        [Display(Order = 1, Name = "UserName_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "UserName_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string UserName { get; set; }

        [Display(Order = 2, Name = "FirstName_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "FirstName_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string FirstName { get; set; }

        [Display(Order = 3, Name = "FamilyName_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "FamilyName_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string FamilyName { get; set; }

        [Display(Order = 4, Name = "Street_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "Street_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string Street { get; set; }

        [Display(Order = 5, Name = "Number_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "Number_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string Number { get; set; }

        [Display(Order = 6, Name = "ZIP_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "ZIP_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string ZIP { get; set; }

        [Display(Order = 7, Name = "City_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "City_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string City { get; set; }

        [Display(Order = 8, Name = "Country_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        public string Country { get; set; }

        [Display(Order = 9, Name = "Email_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "Email_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [EmailAddress(ErrorMessageResourceName = "Email_Valid", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string Email { get; set; }

        [Display(Order = 10, Name = "Password_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [Required(ErrorMessageResourceName = "Password_Required", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Order = 11, Name = "PasswordConfirm_Display", ResourceType = typeof(RegisterViewModel_Resources))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "PasswordConfirm_Match", ErrorMessageResourceType = typeof(RegisterViewModel_Resources))]
        public string ConfirmPassword { get; set; }
    }
}

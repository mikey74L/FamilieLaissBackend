using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Helper
{
    public class FamilieLaissIdentityErrorDescriber : IdentityErrorDescriber
    {
        #region Private Members
        private readonly IStringLocalizer<FamilieLaissIdentityErrorDescriber> Localizer;
        #endregion

        #region C'tor
        public FamilieLaissIdentityErrorDescriber(IStringLocalizer<FamilieLaissIdentityErrorDescriber> localizer)
        {
            Localizer = localizer;
        }
        #endregion

        public override IdentityError ConcurrencyFailure()
        {
            //return new IdentityError 
            //{ 
            //    Code = nameof(ConcurrencyFailure), 
            //    Description = Resources.ConcurrencyFailure 
            //}; 
            return base.ConcurrencyFailure();
        }

        public override IdentityError DefaultError()
        {
            return base.DefaultError();
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return base.DuplicateEmail(email);
        }

        public override IdentityError DuplicateRoleName(string name)
        {
            return base.DuplicateRoleName(name);
        }

        public override IdentityError DuplicateUserName(string name)
        {
            return base.DuplicateUserName(name);
        }

        public override IdentityError InvalidEmail(string email)
        {
            return base.InvalidEmail(email);
        }

        public override IdentityError InvalidRoleName(string name)
        {
            return base.InvalidRoleName(name);
        }

        public override IdentityError InvalidToken()
        {
            return base.InvalidToken();
        }

        public override IdentityError InvalidUserName(string name)
        {
            return base.InvalidUserName(name);
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return base.LoginAlreadyAssociated();
        }

        public override IdentityError PasswordMismatch()
        {
            return base.PasswordMismatch();
        }

        public override IdentityError PasswordRequiresDigit()
        {
            IdentityError ReturnValue = base.PasswordRequiresDigit();

            ReturnValue.Description = Localizer["PasswordRequiresDigit"];

            return ReturnValue;
        }

        public override IdentityError PasswordRequiresLower()
        {
            IdentityError ReturnValue = base.PasswordRequiresLower();

            ReturnValue.Description = Localizer["PasswordRequireLowercase"];

            return ReturnValue;
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            IdentityError ReturnValue = base.PasswordRequiresNonAlphanumeric();

            ReturnValue.Description = Localizer["PasswordRequireNonAlphanumeric"];

            return ReturnValue;
        }

        public override IdentityError PasswordRequiresUpper()
        {
            IdentityError ReturnValue = base.PasswordRequiresUpper();

            ReturnValue.Description = Localizer["PasswordRequireUppercase"];

            return ReturnValue;
        }

        public override IdentityError PasswordTooShort(int length)
        {
            IdentityError ReturnValue = base.PasswordTooShort(length);

            ReturnValue.Description = string.Format(Localizer["PasswordToShort"], length.ToString());

            return ReturnValue;
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return base.UserAlreadyHasPassword();
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return base.UserAlreadyInRole(role);
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return base.UserLockoutNotEnabled();
        }

        public override IdentityError UserNotInRole(string role)
        {
            return base.UserNotInRole(role);
        }
    }
}

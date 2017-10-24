using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FamilieLaissIdentity.Validators.Identity
{
    public class EmailAsPasswordValidator
    {

    }

    /// <summary>
    /// Validates that the supplied password is not the same as the user's Email
    /// </summary>
    public class EmailAsPasswordValidator<TUser, TKey>: IPasswordValidator<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        private readonly IStringLocalizer<EmailAsPasswordValidator> _Localizer;

        public EmailAsPasswordValidator(IStringLocalizer<EmailAsPasswordValidator> localizer)
        {
            _Localizer = localizer;
        }

        /// <inheritdoc />
        public Task<IdentityResult> ValidateAsync(
            UserManager<TUser> manager,
            TUser user,
            string password)
        {

            if (password == null) { throw new ArgumentNullException(nameof(password)); }
            if (manager == null) { throw new ArgumentNullException(nameof(manager)); }

            if (user != null && string.Equals(user.Email, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailAsPassword",
                    Description = _Localizer["Error"] // "You cannot use your Email as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

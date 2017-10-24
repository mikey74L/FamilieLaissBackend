using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FamilieLaissIdentity.Validators.Identity
{
    public class UsernameAsPasswordValidator
    {

    }

    /// <summary>
    /// Validates that the supplied password is not the same as the user's UserName
    /// </summary>
    public class UsernameAsPasswordValidator<TUser, TKey> : IPasswordValidator<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        private IStringLocalizer<UsernameAsPasswordValidator> _Localizer;

        public UsernameAsPasswordValidator(IStringLocalizer<UsernameAsPasswordValidator> localizer)
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

            if (user != null && string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = _Localizer["Error"] // "You cannot use your username as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

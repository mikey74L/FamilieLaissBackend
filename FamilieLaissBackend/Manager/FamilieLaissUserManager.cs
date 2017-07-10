using FamilieLaissBackend.Context;
using FamilieLaissBackend.Model.Account;
using FamilieLaissBackend.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Manager
{
    public class FamilieLaissUserManager : UserManager<IdentityUserExtended>
    {
        public FamilieLaissUserManager(IUserStore<IdentityUserExtended> store)
            : base(store)
        {
        }

        public static FamilieLaissUserManager Create(IdentityFactoryOptions<FamilieLaissUserManager> options, IOwinContext context)
        {
            //Den User-Manager erzeugen. Dabei wird der benötigte DB-Store (EF mit Code-First) aus dem aktuellen OWIN-Kontext gezogen.
            var manager = new FamilieLaissUserManager(new UserStore<IdentityUserExtended>(context.Get<FamilieLaissIdentityContext>()));

            //Konfigurieren der Benutzernamen-Validierung
            manager.UserValidator = new UserValidator<IdentityUserExtended>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            //Konfigurieren der Passwortsicherheit
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            //Lockout konfigurieren
            manager.UserLockoutEnabledByDefault = true;

            //Anzahl der möglichen Fehlversuche bevor der Benutzer für eine gewisse Zeit
            //gesperrt wird
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            //Bestimmen der Zeitspanne für die Ein User ausgesperrt wird, nach dem er
            //sich mehrmals falsch angemeldet hat
            manager.DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0);

            //Den eMail-Service konfigurieren
            manager.EmailService = new EmailService();

            //Bestimmen des Protection-Provider. Dieser wird für die Generierung von Tokens für EMail-Bestätigung
            //oder Passwort zurücksetzen benötigt
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUserExtended>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            //Den erstellten User-Manager zurückliefern
            return manager;
        }
    }
}
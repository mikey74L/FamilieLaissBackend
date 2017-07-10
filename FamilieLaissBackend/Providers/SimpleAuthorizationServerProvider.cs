using FamilieLaissBackend.Model.Account;
using FamilieLaissBackend.Repository;
using FamilieLaissBackend.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //Jeder Client ist erlaubt
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Deklaration
            string firstName = "";
            string familyName = "";
            string userName = "";
            IList<string> Roles = null;
            ClaimsIdentity oAuthIdentity = null;

            //CORS Zugriffe erlauben
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Mit dem Authorization-Repository überprüfen ob der User berechtigt ist
            using (AuthRepository _repo = new AuthRepository())
            {
                //Überprüfen ob der User ausgesperrt wurde wegen zu vieler falscher Anmeldeversuche
                if (await _repo.IsLockedOut(context.UserName))
                {
                    context.SetError("locked_out", LoginError_Resources.LockedOut);
                    return;
                }
                else
                {
                    //Ermitteln des Users
                    IdentityUserExtended user = await _repo.FindUser(context.UserName, context.Password);

                    //Wenn kein User gefunden wurde, dann ist der Benutzername oder das Passwort falsch
                    if (user == null)
                    {
                        //Setzen des Fehlers im Result
                        context.SetError("invalid_grant", LoginError_Resources.WrongUsernamePassword);

                        //Setzen des Counts für die Fehlanmeldungen im Account
                        IdentityResult result = await _repo.AccessFailed(context.UserName);

                        //Ermitteln des Counts für die Fehlversuche
                        int Count = await _repo.GetAccessFailedCount(context.UserName);

                        return;
                    }
                    else
                    {
                        //Überprüfen ob die eMail-Bestätigung durchgeführt wurde. Wenn nicht wird
                        //ein entsprechender Fehler zurückgeliefert.
                        if (!user.EmailConfirmed)
                        {
                            context.SetError("email_confirmation", LoginError_Resources.MissingConfirmation);
                            return;
                        }

                        //Überprüfen ob der Account schon freigeschalten wurde. Wenn nicht wird
                        //ein entsprechender Fehler zurückgeliefert.
                        if (!user.IsAllowed)
                        {
                            context.SetError("not_allowed", LoginError_Resources.NotAllowed);
                            return;
                        }

                        //Zurücksetzen des Counts für die Fehlanmeldungen wenn Anmeldung erfolgreich
                        await _repo.ResetAccesFailedCount(user.Id);

                        //Ermitteln der Rollen
                        Roles = await _repo.GetRolesForUser(user.Id);

                        //Erstellen der Identity
                        oAuthIdentity = await _repo.CreateIdentity(user, context.Options.AuthenticationType);

                        //Ermitteln des Vorname und des Familienname
                        firstName = user.Vorname;
                        familyName = user.Familienname;
                        userName = user.UserName;
                    }
                }
            }

            //Das Ticket mit den entsprechenden Properties erstellen
            AuthenticationProperties properties = CreateProperties(userName, firstName, familyName, Roles.ToArray());
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            //Das Token erzeugen
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string firstName, string familyName, string[] roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "firstName", firstName },
                { "familyName", familyName },
                { "roles", String.Join("," , roles) }
            };
            return new AuthenticationProperties(data);
        }
    }
}
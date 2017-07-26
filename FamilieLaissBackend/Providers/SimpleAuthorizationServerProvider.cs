using FamilieLaissBackend.Helper;
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
            //Deklarationen
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            ClientConfig client = null;

            //Ermitteln der Credentials für die ClientID und das Client-Secret aus dem aktuellen Request (Context).
            //
            //Diese können entweder im Header stehen (TryGetBasicCredentials), oder im Body stehen (TryGetFormCredentials)
            //Im body müssen diese aber mit x-www-form-urlencoded abgelegt sein
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            //Wenn keine ClientID mitgesendet wurde dann wird ein Fehler zurückgeliefert.
            //D.h. es dürfen nur bekannte Clients die auch eine ClientID mitliefern mit dem System arbeiten
            if (context.ClientId == null)
            {
                context.SetError("clientId:missing", "ClientId should be sent.");

                return;
            }

            //Ermitteln des Clients aus der Datenbank
            using (AuthRepository _repo = new AuthRepository())
            {
                client = await _repo.FindClient(context.ClientId);
            }

            //Wenn aus der Datenbank kein passender Client ermittelt werden konnte, dann wird
            //ein Fehler zurückgeliefert
            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client with id '{0}' is not registered in the system.", context.ClientId));

                return;
            }

            //Wenn es sich um einen nativen Client handelt, dann wird das Client-Secret
            //auf Richtigkeit überprüft
            if (client.ApplicationType == enApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("client_secret_missing", "Client secret should be sent.");

                    return;
                }
                else
                {
                    if (client.Secret != GeneralHelper.GetHash(clientSecret))
                    {
                        context.SetError("client_secret_invalid", "Client secret is invalid.");

                        return;
                    }
                }
            }

            //Wenn der Client als nicht aktiv in der Datenbank konfiguriert ist, dann gibt es eine Fehlermeldung
            if (!client.Active)
            {
                context.SetError("client_inactive", "Client is inactive.");

                return;
            }

            //Setzen der zulässigen IP-Ranges und der Lebensdauer der Refresh-Tokens im OWIN-Context
            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            //Den Client als validated ermitteln
            context.Validated();

            //Returning
            return;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Deklaration
            string firstName = "";
            string familyName = "";
            string userName = "";
            IList<string> Roles = null;
            ClaimsIdentity oAuthIdentity = null;

            //Auslesen der Erlaubten CORS Zugriffe aus dem OWIN-Context
            //Dieser kommt ursprünglich aus der Datenbank in der Tabelle Clients
            //Und wird in der Methode ValidateClientAuthentication ausgelesen
            //Wenn kein Wert gefunden wurde, dann wird alles für CORS erlaubt
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";

            //CORS Zugriffe erlauben
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

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
            AuthenticationProperties properties = CreateProperties(userName, firstName, familyName, (context.ClientId == null) ? string.Empty : context.ClientId, Roles.ToArray());
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

        public static AuthenticationProperties CreateProperties(string userName, string firstName, string familyName, string clientID, string[] roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "firstName", firstName },
                { "familyName", familyName },
                { "as:client_id", clientID},
                { "roles", String.Join("," , roles) }
            };
            return new AuthenticationProperties(data);
        }
    }
}
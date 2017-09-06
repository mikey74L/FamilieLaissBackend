using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Clients
{
    public class ConfigClientsInitial
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
               new Client
               {
                   //Id des Clients
                   ClientId = "aureliaweb",

                   //Name des Clients
                   ClientName = "Aurelia Web SPA",

                   //Es werden keine interaktiven User verwendet. Verwende daher die
                   //Client-Credentials die aus einerm Client-Secret bestehen.
                   AllowedGrantTypes = GrantTypes.ClientCredentials,

                   //Keinen Consent-Screen (Bestätigungs-Screen) anzeigen
                   RequireConsent = false,

                   //Das Client-Secret festlegen
                   ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },

                   //Zugriff auf die Resourcen festlegen
                   AllowedScopes = { "FamilieLaissAPI" },

                   //Offline-Zugriff ist nicht erlaubt
                   AllowOfflineAccess = false
               }
            };
        }
    }
}

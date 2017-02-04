using FamilieLaissBackend.Context;
using FamilieLaissBackend.Manager;
using FamilieLaissBackend.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.App_Start
{
    public class OAuthConfig
    {
        public static void ConfigureOAuth(IAppBuilder app)
        {
            //Den Authorization-Server konfigurieren
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            //Den DB-Kontext und den User-Manager für Identity konfigurieren
            //so dass diese eine Single-Instanz pro Request benutzen
            app.CreatePerOwinContext<FamilieLaissIdentityContext>(FamilieLaissIdentityContext.Create);
            app.CreatePerOwinContext<FamilieLaissUserManager>(FamilieLaissUserManager.Create);

            //OWIN mit OAuth2 verdrahten
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            //Bearer-Tokens sollen als Authorization verwendet werden
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(FamilieLaissBackend.Startup))]
namespace FamilieLaissBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Access-Tokens gegen den Identity-Server verifizieren
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ValidationMode = ValidationMode.Both,

                RequiredScopes = new[] { "FamilieLaissAPI" }
            });

            //OWIN mit der Web-API verdrahten. 
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            //CORS verwenden -- Muss im Produktivbetrieb noch auf die entsprechenden URLs eingeschränkt werden
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //Web-API verwenden
            app.UseWebApi(config);
        }
    }
}

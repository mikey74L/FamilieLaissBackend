using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using FamilieLaissBackend.App_Start;

[assembly: OwinStartup(typeof(FamilieLaissBackend.Startup))]
namespace FamilieLaissBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Konfigurieren von OAuth für OWIN
            OAuthConfig.ConfigureOAuth(app);

            //OWIN mit der Web-API verdrahten. Dabei wird auch CORS für WEB-API freigeschalten
            //Dabei gibt es bei CORS keine Beschränkung
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}

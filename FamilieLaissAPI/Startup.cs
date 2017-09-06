using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace FamilieLaissAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)  //Den Basis-Pfad der Web-Anwendung festlegen
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  //Die Appsettings.json hinzufügen
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)  //Die Appsettings der aktuellen Umgebung hinzufügen
                .AddEnvironmentVariables();  //Die Umgebungsvariablen hinzufügen
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()              //Die MVC Basis-Services hinzufügen
                .AddAuthorization()            //Die Authorization zu MVC hinzufügen
                .AddJsonFormatters(options => options.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); //Die JSON Formatter zu MVC hinzufügen
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "https://localhost:5000",
                ApiName = "laissapi_v1",

                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });


            app.UseMvc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FamilieLaissIdentity.Resources;
using FamilieLaissIdentity.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Data.DBContext;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace FamilieLaissIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Den DB-Context für ASP.NET Identity hinzufügen
            services.AddDbContext<AppIdentityDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            //ASP.NET Identity hinzufügen
            services.AddIdentity<FamilieLaissIdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDBContext>();

            //Den Identity-Server zum DI-Container hinzufügen
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(o => o.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                    options => options.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name)))
                .AddOperationalStore(o => o.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                    options => options.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name)))
                .AddAspNetIdentity<FamilieLaissIdentityUser>();

            //MVC hinzufügen
            services.AddMvc();
        }

        public IConfigurationRoot Configuration { get; set; }

        //Mit dieser Methode wird der EF-Context für IdentityServer Migrated und geseeded
        private void InitializeEntityServerDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                //Migraten der Datenbank
                context.Database.Migrate();

                //Seeden der Daten für Clients, Identity-Resourcen und API-Resourcen
                if (!context.Clients.Any())
                {
                    foreach (var client in ConfigClientsInitial.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in ConfigIdentityResourcesInitial.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in ConfigResourcesInitial.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Konfigurations-Dateien konfigurieren
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();

            //Migraten und seeden des EF-Contexts für IdentityServer
            InitializeEntityServerDB(app);

            //Das Logging für die Konsole hinzufügen und den
            //Logging-Level auf Debug festlegen
            loggerFactory.AddConsole(LogLevel.Debug);

            //Wenn sich der Server im Entwicklermodus befindet, dann
            //soll die Entwickler Exception-Page verwendet werden
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //ASP.NET Identity zur Pipeline hinzufügen
            app.UseIdentity();

            //Identity-Server verwenden
            app.UseIdentityServer();

            //Browsen für statisches Dateien aktivieren 
            app.UseStaticFiles();

            //MVC mit default Route aktivieren
            app.UseMvcWithDefaultRoute();
        }
    }
}

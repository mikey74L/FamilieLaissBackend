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
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Service;
using FamilieLaissIdentity.Models;
using FamilieLaissIdentity.Models.Account;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using FamilieLaissIdentity.Helper;

namespace FamilieLaissIdentity
{
    public class Startup
    {
        #region C'tor
        public Startup(IConfiguration configuration)
        {
            //Konfig-Objekt übernehmen
            Configuration = configuration;
        }
        #endregion

        #region Configure IOC-Container
        //Diese Methode wird aufgerufen
        public void ConfigureServices(IServiceCollection services)
        {
            //Hinzufügen der Konfiguration (App-Settings) zum IOC-Container
            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            //Den DB-Context für ASP.NET Identity hinzufügen
            services.AddDbContext<AppIdentityDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            //ASP.NET Identity hinzufügen
            services.AddIdentity<FamilieLaissIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>()
                .AddDefaultTokenProviders()
                .AddEmailAsPasswordValidator<FamilieLaissIdentityUser>()
                .AddUsernameAsPasswordValidator<FamilieLaissIdentityUser>();

            //ASP.NET.Core.Identity konfigurieren
            services.Configure<IdentityOptions>(options =>
            {
                //Festlegen der Passwort-Einstellungen
                options.Password.RequireDigit = true; //Das Passwort muss mindestens eine Zahl enthalten
                options.Password.RequiredLength = 8; //Das Passwort muss mindestens 8 Zeichen lang sein
                options.Password.RequireNonAlphanumeric = true; //Das Passwort muss mindestens ein nicht alphanumerisches Zeichen enthalten
                options.Password.RequireUppercase = true; //Das Passwort muss mindestens einen Großbuchstaben enthalten
                options.Password.RequireLowercase = true; //Das Passwort muss mindestens einen Kleinbuchstaben enthalten

                //Legt fest, dass eine Anmeldung nur erfolgen kann wenn die Email-Adresse bestätigt wurde
                options.SignIn.RequireConfirmedEmail = true;

                //Festlegen nach wie vielen Fehlversuchen der Account gesperrt wird
                options.Lockout.MaxFailedAccessAttempts = 5;

                //Festlegen, dass die Email-Adresse für jede User eindeutig sein muss
                options.User.RequireUniqueEmail = true;
            });

            //Lokalisierbare Fehlermeldungen für ASP.NET Core Identity hinzufügen
            services.AddTransient<IdentityErrorDescriber, FamilieLaissIdentityErrorDescriber>();

            //Den Service für den User-Operations hinzufügen
            services.AddTransient<IUserOperations, UserOperationsService>();

            //Den Service für die Signin-Operations hinzufügen
            services.AddTransient<ISigninOperations, SigninOperationsService>();

            //Den Service für den eMail-Generator hinzufügen
            services.AddTransient<IMailGenerator, MailGeneratorService>();

            //Den Service für den Mail-Versand hinzufügen
            services.AddTransient<EMailMailtrapUserManagerService>();
            services.AddTransient<EMailSendGridUserManagerService>();
            services.AddTransient(factory =>
            {
                Func<string, IMailSender> accesor = key =>
                {
                    switch (key)
                    {
                        case "Dev":
                            return factory.GetService<EMailMailtrapUserManagerService>();
                        case "Prod":
                            return factory.GetService<EMailSendGridUserManagerService>();
                        default:
                            return factory.GetService<EMailMailtrapUserManagerService>();
                    }
                };
                return accesor;
            });

            //Den Identity-Server zum DI-Container hinzufügen
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                       builder.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                       sql => sql.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                       builder.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                       sql => sql.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name));
                })
                .AddAspNetIdentity<FamilieLaissIdentityUser>();

            //Lokalisierung für ASP.NET Core hinzufügen
            services.AddLocalization(options => options.ResourcesPath = "Localize");

            //MVC hinzufügen mit Lokalisierungsfunktion für Views und für Data-Annotations
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();


            //Automapper konfigurieren und zum DI-Container hinzufügen
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, FamilieLaissIdentityUser>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //Den View-Renderer zum IOC-Container hinzufügen. Dieser wird für das generieren der Mail-Inhalte über Razor-Engine verwendet
            services.AddTransient<IViewRenderer, ViewRendererService>();
        }
        #endregion

        #region Migrate and Seed EF for Identity-Server
        //Mit dieser Methode wird der EF-Context für IdentityServer Migrated und geseeded
        private void InitializeIdentityServerDB(IApplicationBuilder app)
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
        #endregion

        #region Migrate and Seed EF for ASP.NET Identity
        private void InitializeIdentityDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppIdentityDBContext>();

                //Migraten der Datenbank
                context.Database.Migrate();

                //Seeden der Daten für den Admin-User
                if (!context.Users.Any())
                {

                }
            }
        }
        #endregion

        #region Configure Pipeline
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Die von der Website unterstützen Sprachen hinzufügen
            var supportedCultures = new[]
            {
                new CultureInfo("de"),
                new CultureInfo("en")
            };

            //Lokalisierung anhand von Requests zur Pipeline hinzufügen
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //Migraten und seeden des EF-Contexts für IdentityServer
            InitializeIdentityServerDB(app);

            //Migraten und seeden des EF-Contexts für ASP.NET Identity
            InitializeIdentityDB(app);

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
            app.UseAuthentication();

            //Identity-Server verwenden
            app.UseIdentityServer();

            //Browsen für statisches Dateien aktivieren 
            app.UseStaticFiles();

            //MVC mit default Route aktivieren
            app.UseMvcWithDefaultRoute();
        }
        #endregion

        #region Public Config-Property
        public IConfiguration Configuration { get; set; }
        #endregion
    }
}

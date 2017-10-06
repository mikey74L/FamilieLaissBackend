﻿using System;
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

namespace FamilieLaissIdentity
{
    public class Startup
    {
        #region C'tor
        public Startup(IHostingEnvironment env)
        {
            //Konfigurations-Dateien konfigurieren
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            //Konfig-Objekt erzeugen
            Configuration = builder.Build();
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
                .AddDefaultTokenProviders();

            //ASP.NET.Core.Identity konfigurieren
            services.Configure<IdentityOptions>(options =>
            {
                //Festlegen der Passwort-Einstellungen
                options.Password.RequireDigit = true; //Das Passwort muss mindestens eine Zahl enthalten
                options.Password.RequiredLength = 8; //Das Passwort muss mindestens 8 Zeichen lang sein
                options.Password.RequireNonAlphanumeric = false; //Das Passwort muss mindestens ein nicht alphanumerisches Zeichen enthalten
                options.Password.RequireUppercase = true; //Das Passwort muss mindestens einen Großbuchstaben enthalten
                options.Password.RequireLowercase = false; //Das Passwort muss mindestens einen Kleinbuchstaben enthalten

                //Legt fest, dass eine Anmeldung nur erfolgen kann wenn die Email-Adresse bestätigt wurde
                options.SignIn.RequireConfirmedEmail = true;

                //Festlegen nach wie vielen Fehlversuchen der Account gesperrt wird
                options.Lockout.MaxFailedAccessAttempts = 5;

                //Festlegen, dass die Email-Adresse für jede User eindeutig sein muss
                options.User.RequireUniqueEmail = true;
            });

            //Den Service für den User-Manager hinzufügen
            services.AddTransient<IUserOperations, UserOperationsService>();

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
                .AddTemporarySigningCredential()
                .AddConfigurationStore(o => o.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                    options => options.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name)))
                .AddOperationalStore(o => o.UseSqlServer(Configuration.GetConnectionString("IdentityServer4Connection"),
                    options => options.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name)))
                .AddAspNetIdentity<FamilieLaissIdentityUser>();

            //MVC hinzufügen
            services.AddMvc();

            //Automapper konfigurieren und zum DI-Container hinzufügen
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, FamilieLaissIdentityUser>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
        #endregion

        #region Migrate and Seed EF for Identity-Server
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
        #endregion

        #region Configure Pipeline
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
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
        #endregion

        #region Public Config-Property
        public IConfigurationRoot Configuration { get; set; }
        #endregion
    }
}

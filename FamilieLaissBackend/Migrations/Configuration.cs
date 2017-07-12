namespace FamilieLaissBackend.Migrations
{
    using FamilieLaissBackend.Model.Account;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FamilieLaissBackend.Context.FamilieLaissIdentityContext>
    {
        public Configuration()
        {
            //Automatische Migrationen sind erlaubt
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FamilieLaissBackend.Context.FamilieLaissIdentityContext context)
        {
            //Hinzufügen der Rollen
            context.Roles.AddOrUpdate(p => p.Name,
                new IdentityRole() { Name = "User" },
                new IdentityRole() { Name = "Admin" },
                new IdentityRole() { Name = "FamilyUser" });

            //Hinzufügen des 1. Admin-User
            if (!context.Users.Any(u => u.UserName == "mlaiss"))
            {

                IdentityUserExtended AdminUser = new IdentityUserExtended
                {
                    Email = "mikey74@hotmail.de",
                    Geschlecht = 1,
                    Familienname = "Laiß",
                    Vorname = "Michael",
                    UserName = "mlaiss",
                    Strasse = "Goldmühlestraße",
                    Stadt = "Sindelfingen",
                    Land = "DE",
                    HNR = "6",
                    IsAllowed = true,
                    PLZ = "71065",
                    SecurityAnswer = "Ohne Bedeutung",
                    SecurityQuestion = 1
                };

                var store = new UserStore<IdentityUserExtended>(context);
                var manager = new UserManager<IdentityUserExtended>(store);

                manager.Create(AdminUser, "Admin_Password_mlaiss");
                manager.AddToRole(AdminUser.Id, "User");
                manager.AddToRole(AdminUser.Id, "FamilyUser");
                manager.AddToRole(AdminUser.Id, "Admin");
            }

            //Hinzufügen des 2. Admin-User
            if (!context.Users.Any(u => u.UserName == "klaiss"))
            {

                IdentityUserExtended AdminUser = new IdentityUserExtended
                {
                    Email = "klaudija@klaudija-s.de",
                    Geschlecht = 2,
                    Familienname = "Laiß",
                    Vorname = "Klaudija",
                    UserName = "klaiss",
                    Strasse = "Goldmühlestraße",
                    Stadt = "Sindelfingen",
                    Land = "DE",
                    HNR = "6",
                    IsAllowed = true,
                    PLZ = "71065",
                    SecurityAnswer = "Ohne Bedeutung",
                    SecurityQuestion = 1
                };

                var store = new UserStore<IdentityUserExtended>(context);
                var manager = new UserManager<IdentityUserExtended>(store);

                manager.Create(AdminUser, "Admin_Password_klaiss");
                manager.AddToRole(AdminUser.Id, "User");
                manager.AddToRole(AdminUser.Id, "FamilyUser");
                manager.AddToRole(AdminUser.Id, "Admin");
            }
        }
    }
}

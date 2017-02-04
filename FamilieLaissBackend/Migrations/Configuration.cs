namespace FamilieLaissBackend.Migrations
{
    using FamilieLaissBackend.Model;
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

            //Der SQL-Generator wird auf Devart gesetzt
            SetSqlGenerator("Devart.Data.Oracle", new Devart.Data.Oracle.Entity.Migrations.OracleEntityMigrationSqlGenerator());
        }

        protected override void Seed(FamilieLaissBackend.Context.FamilieLaissIdentityContext context)
        {
            //Hinzufügen der Rollen
            context.Roles.AddOrUpdate(p => p.Name,
                new IdentityRole() { Name = "User" },
                new IdentityRole() { Name = "Admin" },
                new IdentityRole() { Name = "FamilyUser" });

            //Den Password-Hasher initialisieren und das Passwort für den Admin Hashen
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Sicalis_74");

            //Hinzufügen des Admin-User
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {

                IdentityUserExtended AdminUser = new IdentityUserExtended
                {
                    Email = "mikey74@hotmail.de",
                    Geschlecht = 1,
                    Familienname = "Laiß",
                    Vorname = "Michael",
                    UserName = "Admin",
                    Strasse = "Goldmühlestraße",
                    Stadt = "Sindelfingen",
                    Land = "Deutschland",
                    HNR = "6",
                    IsAllowed = true,
                    PLZ = "71065",
                    SecurityAnswer = "Ohne Bedeutung",
                    SecurityQuestion = 1
                };

                var store = new UserStore<IdentityUserExtended>(context);
                var manager = new UserManager<IdentityUserExtended>(store);

                manager.Create(AdminUser, "Sicalis_74");
                manager.AddToRole(AdminUser.Id, "User");
                manager.AddToRole(AdminUser.Id, "FamilyUser");
                manager.AddToRole(AdminUser.Id, "Admin");
            }
        }
    }
}

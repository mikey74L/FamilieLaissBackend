using FamilieLaissBackend.Migrations;
using FamilieLaissBackend.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Context
{
    public class FamilieLaissIdentityContext : IdentityDbContext<IdentityUserExtended>
    {
        public FamilieLaissIdentityContext()
                    : base("FamilieLaissIdentity", throwIfV1Schema: false)
        {
        }

        static FamilieLaissIdentityContext()
        {
            var config = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;

            //Lange Bezeichner die Identity in der Datenbank erstellen möchte werden für ORACLE auf 30 Zeichen gekürzt
            config.CodeFirstOptions.TruncateLongDefaultNames = true;

            //Der Schemname für alle Operationen (SELECT, etc.) für ORACLE entfernen
            config.Workarounds.IgnoreSchemaName = true;

            //Columns sollen Case-Sensitiv für ORACLE sein, da es sonst zu Problemen mit Identity kommt
            config.Workarounds.ColumnTypeCasingConventionCompatibility = true;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FamilieLaissIdentityContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Die Methode des Vaters aufrufen
            base.OnModelCreating(modelBuilder);

            //Für die Spalten ID und Provider die Länge des Strings auf 256 fetsetzen
            modelBuilder
              .Properties()
              .Where(p => p.PropertyType == typeof(string) &&
                          !p.Name.Contains("Id") &&
                          !p.Name.Contains("Provider"))
              .Configure(p => p.HasMaxLength(256));
        }

        public static FamilieLaissIdentityContext Create()
        {
            return new FamilieLaissIdentityContext();
        }
    }
}

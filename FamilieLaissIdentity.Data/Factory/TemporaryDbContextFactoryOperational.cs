using FamilieLaissIdentity.Data.DBContext;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FamilieLaissIdentity.Data.Factory
{
    public class TemporaryDbContextFactoryOperational : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseSqlServer("Data Source=MIKEY-BOOK\\SQLEXPRESS;Initial Catalog=FamilieLaiss;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name));

            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions());
        }
    }
}

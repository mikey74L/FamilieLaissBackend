using FamilieLaissIdentity.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FamilieLaissIdentity.Data.Factory
{
    public class TemporaryIdentityDBContextFactory : IDesignTimeDbContextFactory<AppIdentityDBContext>
    {
        public AppIdentityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDBContext>();

            optionsBuilder.UseSqlServer("Data Source=MIKEY-BOOK\\SQLEXPRESS;Initial Catalog=FamilieLaiss;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                opt => opt.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name));

            return new AppIdentityDBContext(optionsBuilder.Options);
        }
    }
}

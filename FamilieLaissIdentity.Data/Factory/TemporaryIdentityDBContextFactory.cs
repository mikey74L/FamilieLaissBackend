using FamilieLaissIdentity.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FamilieLaissIdentity.Data.Factory
{
    public class TemporaryIdentityDBContextFactory : IDbContextFactory<AppIdentityDBContext>
    {
        public AppIdentityDBContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<AppIdentityDBContext>();
            builder.UseSqlServer("Data Source=MIKEY-BOOK\\SQLEXPRESS;Initial Catalog=FamilieLaiss;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AppIdentityDBContext).GetTypeInfo().Assembly.GetName().Name));
            return new AppIdentityDBContext(builder.Options);
        }
    }
}

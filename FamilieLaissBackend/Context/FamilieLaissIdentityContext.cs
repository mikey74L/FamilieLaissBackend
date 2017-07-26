﻿using FamilieLaissBackend.Migrations;
using FamilieLaissBackend.Model.Account;
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

        public DbSet<ClientConfig> ClientConfigurations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public static FamilieLaissIdentityContext Create()
        {
            return new FamilieLaissIdentityContext();
        }
    }
}

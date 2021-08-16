using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleRestWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRestWebApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}





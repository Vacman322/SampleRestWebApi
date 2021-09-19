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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ClientTag> ClientTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ClientTag>()
                .HasOne(pt => pt.Client)
                .WithMany(p => p.ClientTags)
                .HasForeignKey(pt => pt.ClientID);

            builder.Entity<ClientTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ClientTags)
                .HasForeignKey(pt => pt.TagID);
        }
    } 
}





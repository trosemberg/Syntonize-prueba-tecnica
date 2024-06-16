using Microsoft.EntityFrameworkCore;
using System;
using TechTestData.Models;

namespace TechTestData.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        
        public DbSet<Users> Users { get; set; }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "Admin", Description = "Full Access" },
                new Roles { Id = 2, Name = "UserAccess", Description = "User Access Only" }
            );

            modelBuilder.Entity<Users>().HasData(
                // Password -> Admin123
                new Users { Id = 1, Name = "Admin", FullName = "Full Access", Password = "e80e4501581a999043e0ea28590aff2460514ade605418496e5a8768e5e14d1a", Email = "a@a.com", Salt = "23248", RolesId = 1 },
                // Password -> Password
                new Users { Id = 2, Name = "TestUser", FullName = "User For test", Password = "e80e4501581a999043e0ea28590aff2460514ade605418496e5a8768e5e14d1a", Email = "t@t.com", Salt = "23248", RolesId = 2 }
            );
        }
    }
}

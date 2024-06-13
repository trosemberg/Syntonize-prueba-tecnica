using Microsoft.EntityFrameworkCore;
using System;
using TechTest.Models;

namespace TechTest.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        
        public DbSet<Users> Users { get; set; }
    }
}

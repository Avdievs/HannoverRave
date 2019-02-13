using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Database
{
    public class RaveContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<DJ> DJs { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Seserver=localhost;database=HannoverRave;user=root;password=681567da");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MySql.Data.MySqlClient;
using Shared.Models;
using System;
using System.Data;

namespace Shared.Database
{
    public class RaveContext : DbContext
    {
        public static readonly LoggerFactory DBLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public DbSet<Event> Events { get; set; }
        public DbSet<DJ> DJs { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Location> Locations { get; private set; }
        public DbSet<EventDJ> EventDJs { get; private set; }

        private string ConnectionString = "server=localhost;port=3306;database=HannoverRave;user=root;password=";
        public bool IsConnected { get; set; }

        public RaveContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(DBLoggerFactory);
                optionsBuilder.UseMySQL(ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("EventId");
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.Location).WithMany().HasForeignKey(p => p.LocationId);

            });

            builder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationId).HasName("LocationId");
                entity.HasOne(e => e.Club).WithOne(p => p.Location).HasPrincipalKey<Club>(f => f.LocationId);
            });

            builder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.ClubId).HasName("ClubId");
            });

            builder.Entity<EventDJ>(entity =>
            {
                entity.HasKey(e => e.DJId).HasName("DJId");
                entity.HasOne(e => e.DJ).WithMany(p => p.EventDJs).HasForeignKey(f => f.DJId);
                entity.HasOne(e => e.Event).WithMany(p => p.EventDJs).HasForeignKey(f => f.EventId);
            });

            builder.Entity<DJ>(entity =>
            {
                entity.HasKey(e => e.DJId).HasName("DJId");
                entity.Property(e => e.Name).IsRequired();
            });
        }

        //TODO: Connection test
        public bool ConnectionTest()
        {
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                IsConnected = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (MySqlException mex)
            {
                Console.WriteLine(mex.ToString());

                switch (mex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return IsConnected;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shared.Database
{
    public class RaveContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        //public DbSet<DJ> DJs { get; set; }
        //public DbSet<Club> Clubs { get; set; }
        //public DbSet<Location> Locations { get; private set; }

        private string ConnectionString = "server=localhost;port=3306;database=HannoverRave;user=root;password=";
        public bool IsConnected { get; set; }

        //public RaveContext() : base(options)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<RaveContext>();
        //    optionsBuilder.
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("EventId");
                entity.Property<string>("Name").IsRequired();
                entity.Ignore(e => e.Location);

                //entity.Property(e => e.EventType).HasColumnName("EventType").IsRequired();


            });
        }

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

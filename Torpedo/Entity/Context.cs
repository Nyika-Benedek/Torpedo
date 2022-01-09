using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Torpedo.Models;

namespace Torpedo.Entity
{
    public class Context : DbContext
    {
        static string DatabaseName = "ScoreBoard";
        /*
        static string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string DatabasePath = System.IO.Path.Combine(FolderPath, DatabaseName);
        */
        public DbSet<DatabaseModel> ScoreBoard { get; set; }
        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabaseName}");
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ServerDb"));
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
            //base.OnConfiguring(optionsBuilder);
            //DatabaseModel database = new DatabaseModel();
        }


        /*
        public Context([NotNull] DbContextOptions options) : base(options)
        {
        }
        */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            // define Database properties
            //------------------------------
            // Date as Primary key
            modelBuilder.Entity<DatabaseModel>().HasKey(a => a.Date);

            // Date is required
            modelBuilder.Entity<DatabaseModel>().Property(a => a.Date).IsRequired();

            modelBuilder.Entity<DatabaseModel>().Property(a => a.Date).ValueGeneratedOnAdd();

            // Player1 max length = 20
            modelBuilder.Entity<DatabaseModel>().Property(a => a.Player1).HasMaxLength(20);

            // Player2 max length = 20
            modelBuilder.Entity<DatabaseModel>().Property(a => a.Player2).HasMaxLength(20);
        }

    }
}

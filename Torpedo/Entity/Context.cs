using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Torpedo.Models;

namespace Torpedo.Entity
{
    /// <summary>
    /// This class is responsible to create a connection between the database and the program.
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// The name of the database.
        /// </summary>
        private const string DatabaseName = "ScoreBoard";
        /*
        static string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string DatabasePath = System.IO.Path.Combine(FolderPath, DatabaseName);
        */

        /// <summary>
        /// This is the reference to the database.
        /// </summary>
        public DbSet<DatabaseModel> ScoreBoard { get; set; }

        /// <summary>
        /// Constructor of Context.
        /// </summary>
        public Context()
        {
        }

        /// <summary>
        /// This calls the parent database OnConfigurin method, then defines the meta propeties of the database.
        /// </summary>
        /// <param name="optionsBuilder">Provides a simple API surface for configuring the DbContextOptions.Database.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabaseName}");
            // optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ServerDb"));
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
            // base.OnConfiguring(optionsBuilder);
            // DatabaseModel database = new DatabaseModel();
        }


        /*
        public Context([NotNull] DbContextOptions options) : base(options)
        {
        }
        */


        /// <summary>
        /// This defines the propertis of the database.
        /// </summary>
        /// <param name="modelBuilder">Provides a simple API surface for configuring a Microsoft.EntityFrameworkCore.Metadata.IMutableModel that defines the shape of your entities.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API
            // define Database properties
            // ------------------------------

            // Date as Primary key
            modelBuilder.Entity<DatabaseModel>()
                .HasKey(a => a.Date);

            // Date is required
            modelBuilder.Entity<DatabaseModel>()
                .Property(a => a.Date)
                .IsRequired();

            // Generate the value on add
            modelBuilder.Entity<DatabaseModel>()
                .Property(a => a.Date)
                .ValueGeneratedOnAdd();

            // Player1 max length = 20
            modelBuilder.Entity<DatabaseModel>()
                .Property(a => a.Player1)
                .HasMaxLength(20);

            // Player2 max length = 20
            modelBuilder.Entity<DatabaseModel>()
                .Property(a => a.Player2)
                .HasMaxLength(20);
        }
    }
}

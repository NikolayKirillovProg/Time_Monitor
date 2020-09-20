using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using TimeMonitor.Entities;

namespace TimeMonitor
{
    public class dbContext : DbContext
    {
        public dbContext()
            : base("DbConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(o => o.User_Id);
            modelBuilder.Entity<Report>()
                .HasKey(o => o.Rep_id);
            modelBuilder.Entity<Report>().Property(o => o.Rep_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
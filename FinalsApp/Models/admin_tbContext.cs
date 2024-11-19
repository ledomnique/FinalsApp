using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class admin_tbContext : DbContext
    {
        static admin_tbContext()
        {
            Database.SetInitializer<admin_tbContext>(null);
        }

        public admin_tbContext() : base("Name=inkling_db")
        {

        }

        public virtual DbSet<admin_tbMap> admin_tb { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new admin_tbMap());
        }
    }
}
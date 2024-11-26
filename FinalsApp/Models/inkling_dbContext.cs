using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class inkling_dbContext : DbContext
    {
        static inkling_dbContext()
        {
            Database.SetInitializer<inkling_dbContext>(null);
        }

        public inkling_dbContext() : base("Name=inkling_db")
        {

        }

        public virtual DbSet<admin_tblMap> admin_tbl { get; set; }
        //if multiple tables in database, multiple virtual DbSet(s)

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new admin_tblMap());
        }
    }
}
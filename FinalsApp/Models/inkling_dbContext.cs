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

        public virtual DbSet<users_tblModel> users_tbl { get; set; }
        public virtual DbSet<books_tblModel> books_tbl { get; set; }
        /*
        public virtual DbSet<genres_tblModel> genres_tbl { get; set; }
        public virtual DbSet<genres_tblModel> admin_tbl { get; set; }
        public virtual DbSet<genres_tblModel> bookgenres_tbl { get; set; }
        public virtual DbSet<genres_tblModel> bookrequests_tbl { get; set; }
        public virtual DbSet<genres_tblModel> ratings_tbl { get; set; }
        public virtual DbSet<genres_tblModel> userlibrary_tbl { get; set; }
        public virtual DbSet<genres_tblModel> sessions_tbl { get; set; }
        */



        //if multiple tables in database, multiple virtual DbSet(s)

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new users_tblMap());
        }
    }
}
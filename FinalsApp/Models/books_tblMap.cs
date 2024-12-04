using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class books_tblMap : EntityTypeConfiguration<books_tblModel>
    {
        public books_tblMap()
        {
            HasKey(x => x.bookID);
            ToTable("books_tbl");
        }
    }
}
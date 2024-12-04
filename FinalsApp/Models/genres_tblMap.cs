using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class genres_tblMap : EntityTypeConfiguration<genres_tblModel>
    {
        public genres_tblMap()
        {
            HasKey(x => x.genreID);
            ToTable("books_tbl");
        }
    }
}
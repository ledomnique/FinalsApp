using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class bookrequest_tblMap : EntityTypeConfiguration<bookrequest_tblModel>
    {
        public bookrequest_tblMap()
        {
            HasKey(x => x.bookreqID);
            ToTable("bookrequest_tbl");
        }
    }
}

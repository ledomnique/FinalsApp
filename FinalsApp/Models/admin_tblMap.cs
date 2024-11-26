using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class admin_tblMap : EntityTypeConfiguration<admin_tblModel>
    {
        public admin_tblMap() 
        {
            HasKey(x => x.adminID);
            ToTable("admin_tbl");
        }
    }
}
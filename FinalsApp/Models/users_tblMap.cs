using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class users_tblMap : EntityTypeConfiguration<users_tblModel>
    {
        public users_tblMap()
        {
            HasKey(x => x.userID);
            ToTable("users_tbl");
        }
    }
}
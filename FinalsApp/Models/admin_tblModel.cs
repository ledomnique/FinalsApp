using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class admin_tblModel
    {
        public int adminID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime created_on { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class RegistrationModel
    {
        public string firstName { get; set; }
        public int userID { get; set; }

        public string lastName { get; set; }
        public string userName { get; set; }        
        public string email { get; set; }
        public string password { get; set; }
        public DateTime joined_on { get; set; }
    }
}
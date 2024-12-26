using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class bookrequest_tblModel
    {
        public int bookreqID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int published_on { get; set; }
        public DateTime created_on { get; set; }
    }
}
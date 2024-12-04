using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class books_tblModel
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int genreID { get; set; }
        public DateTime published_on { get; set; }
        public int cover_img { get; set; }
        public string description { get; set; }
    }
}
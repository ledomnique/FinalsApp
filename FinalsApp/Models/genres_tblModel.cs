using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class genres_tblModel
    {
        [Key]
        public int genreID { get; set; }

        public string genre_Name { get; set; }
    }
}
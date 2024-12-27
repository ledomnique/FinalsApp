using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class Review
    {
        public int Id { get; set; } // Primary key
        public int BookId { get; set; } // Foreign key to Book
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }

        // Navigation property for the related Book
        public Book Book { get; set; }
    }
}
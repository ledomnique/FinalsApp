using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalsApp.Models
{
    public class Book
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }

        // Navigation property for related reviews
        public ICollection<Review> Reviews { get; set; }
    }
}
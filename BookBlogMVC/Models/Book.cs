using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookBlogMVC.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required(ErrorMessage = "You must enter a book title.")]
        public int Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        [Required(ErrorMessage = "You must enter an author for this book.")]
        public ICollection<Author> Authors { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}

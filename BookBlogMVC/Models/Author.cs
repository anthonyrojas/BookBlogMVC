using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookBlogMVC.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "First name required for this author.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required for this author.")]
        public string LastName { get; set; }

    }
}

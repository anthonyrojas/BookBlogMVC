using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookBlogMVC.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "A valid username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A valid email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Your first name is required.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Your last name is required.")]
        public string LastName { get; set; }
    }
}

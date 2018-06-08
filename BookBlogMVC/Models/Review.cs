using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookBlogMVC.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int BookID { get; set; }
        [Required(ErrorMessage = "You must provide a rating for your review.")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "You must provide a review.")]
        public string ReviewText { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}

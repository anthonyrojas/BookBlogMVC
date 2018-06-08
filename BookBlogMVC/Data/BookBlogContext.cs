using System;
using Microsoft.EntityFrameworkCore;
using BookBlogMVC.Models;

namespace BookBlogMVC.Data
{
    public class BookBlogContext : DbContext
    {
        public BookBlogContext(DbContextOptions<BookBlogContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Review>().ToTable("Review");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBlogMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBlogMVC.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBlogMVC.Controllers
{
    public class BookController : Controller
    {
        private BookBlogContext context;
        public BookController (BookBlogContext context)
        {
            this.context = context;
        }
        // GET: /Book
        [HttpGet]
        public IActionResult Index()
        {
            var query = context.Books.OrderBy(book => book.Reviews.Count()).Take(10);
            ViewBag.TopBooks = query;
            return View();
        }

        [HttpPost]
        public IActionResult Search(string bookTitle)
        {
            if(bookTitle == null || bookTitle.Trim() == "")
            {
                ViewBag.ErrorMsg = "You must enter a valid book title";
                return View();
            }
            using(context)
            {
                try{
                    var query = context.Books.Include("Authors").Select(book => book.Title.ToString().Contains(bookTitle));
                    if (query != null && query.Count() > 0)
                    {
                        ViewBag.BookList = query;
                        return View();
                    }
                    ViewBag.ErrorMsg = "No search results. Please try again.";
                    return View();   
                }
                catch(Exception e)
                {
                    ViewBag.ErrorMsg = "No search results. Please try again.";
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            if(HttpContext.Session.GetString("_USERNAME") != null && HttpContext.Session.GetString("_USERNAME") != "")
            {
                return View();   
            }
            TempData["Login_Error"] = "You must sign in first.";
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public IActionResult Add(Book newBook)
        {
            if(HttpContext.Session.GetString("_USERNAME") != null && HttpContext.Session.GetString("_USERNAME") != "")
            {
                TempData["Login_Error"] = "You must sign in first.";
                return RedirectToAction("Login","User");
            }
            if(ModelState.IsValid)
            {
                try{
                    using(context)
                    {
                        var currBook = context.Books.Include("Authors").Single(book => book.Title == newBook.Title && book.Authors == newBook.Authors);
                        if (currBook != null)
                        {
                            //we found a result, so the book exists
                            ViewBag.ErrorMsg = "A book with that title already exists.";
                            return View();
                        }
                        //there was no book found so we can add this new book and save it
                        context.Books.Add(newBook);
                        context.SaveChanges();
                    }
                }catch(Exception e)
                {
                    ViewBag.ErrorMsg = "Unable to add a new book at this time.";
                    return View();
                }
            }
            return View();
        }
    }
}

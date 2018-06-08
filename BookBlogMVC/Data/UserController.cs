using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBlogMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBlogMVC.Data
{
    public class UserController : Controller
    {
        private BookBlogContext context;
        public UserController(BookBlogContext context){
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                using(context)
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = String.Format("{0} {1} successfully registered!", newUser.FirstName, newUser.LastName);
            }
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBlogMVC.Data;
using BookBlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBlogMVC.Controllers
{
    public class UserController : Controller
    {
        const string SESSION_USERNAME = "_USERNAME";
        const string SESSION_UID = "_USERID";
        private BookBlogContext context;
        public UserController(BookBlogContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString(SESSION_USERNAME) == null || HttpContext.Session.GetString(SESSION_USERNAME) == ""){
                return RedirectToAction("Login");
            }
            using(context)
            {
                var query = context.Users.Single(u => u.Username == HttpContext.Session.GetString(SESSION_USERNAME));
                ViewBag.User = query;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if(HttpContext.Session.GetString(SESSION_USERNAME) != null && HttpContext.Session.GetString(SESSION_USERNAME) != "")
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                string plainPassword = newUser.Password;
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
                newUser.Password = hashPassword;
                using (context)
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = String.Format("{0} {1} successfully registered!", newUser.FirstName, newUser.LastName);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString(SESSION_USERNAME) != null && HttpContext.Session.GetString(SESSION_USERNAME) != "")
            {
                return RedirectToAction("Index");
            }
            else if(TempData["Login_Error"] != null)
            {
                ViewBag.ErrorMsg = TempData["Login_Error"].ToString();
                return View();
            } 
            return View();
        }

        [HttpPost]
        public IActionResult Login (User user)
        {
            using(context)
            {
                try{
                    var userQ = context.Users.Single(u => u.Username == user.Username);
                    if (userQ != null && BCrypt.Net.BCrypt.Verify(user.Password, userQ.Password) == true)
                    {
                        HttpContext.Session.SetString(SESSION_USERNAME, userQ.Username);
                        HttpContext.Session.SetInt32(SESSION_UID, userQ.UserID);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or email is wrong.");
                        return View();
                    }
                }catch{
                    ModelState.AddModelError("", "Username or password is wrong.");
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult Logout(User user)
        {
            HttpContext.Session.Remove(SESSION_USERNAME);
            return RedirectToAction("Login");
        }
    }
}

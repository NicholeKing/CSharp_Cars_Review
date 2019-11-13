using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cars.Models;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("RegCheck")]
        public IActionResult RegCheck(User u)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                u.Password = Hasher.HashPassword(u, u.Password);
                dbContext.Add(u);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UID", u.UserId);
                return Redirect("Dashboard");
            }
            return View("Index");
        }

        [HttpPost("LogCheck")]
        public IActionResult LogCheck(LUser l)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == l.LEmail);
                // If no user exists with provided email
                if(userInDb == null)
                {
                // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("Index");
                }
            
                // Initialize hasher object
                var hasher = new PasswordHasher<LUser>();
            
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(l, userInDb.Password, l.LPassword);
            
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UID", userInDb.UserId);
                return Redirect("Dashboard");
            }
            return View("Index");
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            User loggedIn = dbContext.Users.FirstOrDefault(u => u.UserId == (int)Sess);
            ViewBag.User = loggedIn;
            ViewBag.MyCars = dbContext.Users.Where(u => u.UserId == (int)Sess).Include(c => c.Autos).ToList();
            return View("Dashboard");
        }

        [HttpPost("createCar")]
        public IActionResult createCar(Car c)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(c);
                dbContext.SaveChanges();
                return Redirect("Dashboard");
            }
            return View("Dashboard");
        }
    }
}

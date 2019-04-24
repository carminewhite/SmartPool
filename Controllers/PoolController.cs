using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPool.Models;

namespace SmartPool.Controllers
{
    public class PoolController : Controller
    {
        // User Test = new User();
        // Test.FirstName = "Freddy";
        // Test.LastName = "Fish";
        // Test.Phone = "(425) 883-2503";
        // Test.PwHash = "SECRET";
        // Test.Email = "mail@mail.com";
        // List<Commute> userCommutes = new List<Commute>();
        // Commute TestCommute = new Commute();
        // TestCommute.ArriveBy = DateTime.Now;
        // TestCommute.Day = DateTime.Today.DayOfWeek.
        // TestCommute.StartPt = "Adress blah blah";
        // TestCommute.EndPt = "Another Adress";
        // Carpool TestPool = new Carpool();
        // TestPool.Commutes = new List<Commute>();
        // TestPool.Commutes.Add(TestCommute);
        // TestCommute.carpool = TestPool;
        // userCommutes.Add(TestCommute);
        // Test.Commutes = userCommutes;
        // dbContext.Users.Add(Test);
        // dbContext.SaveChanges();

        private PoolContext dbContext;
        public PoolController(PoolContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            return View(logged_in_user);
        }

        [HttpGet("commutes")]
        public IActionResult Commutes()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            return View();
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.user = logged_in_user;

            return View();
        }

        [HttpPost("profile/update")]
        public IActionResult ProfileUpdate(UpdateUser form)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.user = logged_in_user;

            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<UpdateUser>();
                
                var result = hasher.VerifyHashedPassword(form, logged_in_user.PwHash, form.Password);
                
                if(result == 0)
                {
                    ModelState.AddModelError("UpdateEmail", "Please enter current password to update profile");
                    return View("Profile");
                }
                
                logged_in_user.FirstName = form.FirstName;
                logged_in_user.LastName = form.LastName;
                logged_in_user.Email = form.Email;
                logged_in_user.Phone = form.Phone;
                logged_in_user.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Profile");
            }
            else
            {
                return View("Profile");
            }
        }
    }

    

}
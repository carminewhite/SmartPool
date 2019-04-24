using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartPool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SmartPool.Controllers
{
    public class LoginRegController : Controller
    {
        private PoolContext dbContext;

        public LoginRegController(PoolContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegUser form)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == form.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Registration");
                }

                User newUser = new User()
                {
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    Phone = form.Phone,
                };
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.PwHash = Hasher.HashPassword(newUser, form.Password);

                dbContext.Add(newUser);
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("LoggedInUserId", newUser.Id);

                return RedirectToAction("Dashboard", "Pool");
            }
            return View("Registration");
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser form)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == form.LogEmail);

                if(userInDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<LogUser>();

                var result = hasher.VerifyHashedPassword(form, userInDb.PwHash, form.LogPassword);
                
                if(result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }

                HttpContext.Session.SetInt32("LoggedInUserId", userInDb.Id);

                return RedirectToAction("Dashboard", "Pool");
            }
            return View("Index");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

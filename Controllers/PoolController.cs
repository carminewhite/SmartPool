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
            return View();
        }

        [HttpGet("my_commutes")]
        public IActionResult Commutes()
        {
            return View();
        }

    }

    

}
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
using System.Net.Http;

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
    
        public PoolController(PoolContext context, HttpService httpservice)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("/commute/create")]
        public IActionResult CommuteForm()
        {
            return View();
        }

        [HttpPost("/commute/create")]
        public IActionResult AddCommute(CommuteForm form)
        {
            if(HttpContext.Session.Keys.Contains("LoggedInUserId"))
            {
                if(ModelState.IsValid)
                {
                    User loggedIn = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
                    
                    if(form.Monday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Monday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Tuesday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Tuesday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Wednesday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Wednesday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Thurday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Thursday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Friday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Friday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Saturday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Saturday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Sunday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Sunday;
                        newCommute.StartPt = form.StartPt;
                        newCommute.EndPt = form.EndPt;
                        newCommute.user = loggedIn;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }

                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return View("AddCommute");
                }
                
            }
            else
            {
                return RedirectToAction("Index", "LoginReg");
            }
        }

        


        [HttpGet("my_commutes")]
        public IActionResult Commutes()
        {
            return View();
        }

    }

    

}
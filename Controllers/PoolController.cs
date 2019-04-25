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

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var origin = await HttpService.GetGeoCode("6907 150th Ave Ne Redmond WA");
            if(origin[0] != "ERROR")
            {
                System.Console.WriteLine($"Lat: {origin[0]} Long: {origin[1]}");
            }
            else
            {
                System.Console.WriteLine("Invalid address");
            }

            var dest = await HttpService.GetGeoCode("Space Needle");
            if(dest[0] != "ERROR")
            {
                System.Console.WriteLine($"Lat: {dest[0]} Long: {dest[1]}");
            }
            else
            {
                System.Console.WriteLine("Invalid address");
            }

            Dictionary<string, string> Package = new Dictionary<string, string>();
            Package.Add("origin-lat", origin[0]);
            Package.Add("origin-lng", origin[1]);

            Package.Add("dest-lat", dest[0]);
            Package.Add("dest-lng", dest[1]);

            ViewBag.places = Package;

            return View();
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

        [HttpGet("/commute/create")]
        public IActionResult CommuteForm()
        {
            return View();
        }

        [HttpPost("/commute/create")]
        // public IActionResult AddCommute(CommuteForm form)
        // {
        //     if(HttpContext.Session.Keys.Contains("LoggedInUserId"))
        //     {
        //         if(ModelState.IsValid)
        //         {
        //             User loggedIn = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
                    
        //             if(form.Monday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Monday;
        //                 newCommute.StartLocationId = form.StartPoint;
        //                 newCommute.EndLocationId = form.EndPoint;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Tuesday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Tuesday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Wednesday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Wednesday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Thurday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Thursday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Friday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Friday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Saturday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Saturday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }
        //             if(form.Sunday == true)
        //             {
        //                 Commute newCommute = new Commute();
        //                 newCommute.ArriveBy = form.ArriveBy;
        //                 newCommute.Day = DayOfWeek.Sunday;
        //                 newCommute.StartPt = form.StartPt;
        //                 newCommute.EndPt = form.EndPt;
        //                 newCommute.user = loggedIn;

        //                 dbContext.Add(newCommute);
        //                 dbContext.SaveChanges();
        //             }

        //             return RedirectToAction("Dashboard");
        //         }
        //         else
        //         {
        //             return View("AddCommute");
        //         }
                
        //     }
        //     else
        //     {
        //         return RedirectToAction("Index", "LoginReg");
        //     }
        // }

        


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

        [HttpGet("mylocations")]
        public IActionResult MyLocations()
        {
            return View();
        }

        [HttpPost("create-mylocation")]
        public IActionResult CreateLocation(FormLocation form)
        {   
            if(ModelState.IsValid)
                {
                User CurrentUser = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId")).FirstOrDefault();
                if(CurrentUser == null)
                {
                    return Redirect("/logout");
                }

                Location newLocation = new Location()
                {
                    LocationNickname = form.LocationNickname,
                    Address = form.Address,
                    City = form.City,
                    State = form.State,
                    Zip = form.Zip,
                    UserId = CurrentUser.Id
                };
                dbContext.Add(newLocation);
                dbContext.SaveChanges();
                return RedirectToAction("CreateCommute");
            }
            return View("MyLocations");
        }
            
        [HttpGet("create-commute")]
        public IActionResult CreateCommute()
        {

            List<Location> allLocations = dbContext.Locations
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            ViewBag.ListofLocations = allLocations;
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

        // [HttpPost("create-new-commute")]
        // public IActionResult CreateNewCommute(FormCommute form)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         User CurrentUser = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId")).FirstOrDefault();
        //         if(CurrentUser == null)
        //         {
        //             return Redirect("/logout");
        //         }

        // }

    

}
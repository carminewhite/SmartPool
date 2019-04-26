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

            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                    .Include(u => u.carpools)
                                    .Include(u => u.riderships)
                                    .ThenInclude(r => r.carpool)
                                    .FirstOrDefault();
            ViewBag.logged_in_user = logged_in_user;
            return View();
        }
            
        [HttpGet("create/commute/{carpoolid}")]
        public IActionResult CreateCommute(int carpoolid)
        {

            List<Location> allLocations = dbContext.Locations
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            ViewBag.ListofLocations = allLocations;
            ViewBag.carpoolid = carpoolid;
            return View();
            
        }

        // [HttpGet("/commute/create")]
        // public IActionResult CommuteForm()
        // {

        //     return View();
        // }

        [HttpPost("/commute/create")]
        public IActionResult AddCommute(CommuteForm form)
        {
            if(HttpContext.Session.Keys.Contains("LoggedInUserId"))
            {
                System.Console.WriteLine("VERIFYIED LOGIN");
                if(ModelState.IsValid)
                {
                    System.Console.WriteLine("MODEL VALID");
                    // User loggedIn = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));

                    System.Console.WriteLine(form.ArriveBy);
                    
                    if(form.Monday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Monday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Tuesday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Tuesday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Wednesday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Wednesday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Thursday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Thursday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Friday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Friday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Saturday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Saturday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    if(form.Sunday == true)
                    {
                        Commute newCommute = new Commute();
                        newCommute.ArriveBy = form.ArriveBy;
                        newCommute.Day = DayOfWeek.Sunday;
                        newCommute.StartLocationId = form.StartLocationId;
                        newCommute.EndLocationId = form.EndLocationId;
                        newCommute.CarpoolId = form.CarpoolId;

                        dbContext.Add(newCommute);
                        dbContext.SaveChanges();
                    }
                    System.Console.WriteLine("POSTED TO DB?????");
                    return RedirectToAction("UpdateCarpool", new {id = form.CarpoolId});

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

        [HttpGet("carpools/{id}")]
        public IActionResult Carpools()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            List<Commute> AllCommutes = dbContext.Commutes
                .Where(c => c.Id!=null).Include(c => c.carpool)
                .ThenInclude(c => c.user).Include(c => c.carpool)
                .ThenInclude(c => c.riderships).ToList();
            bool idValid = Int32.TryParse(RouteData.Values["id"].ToString(), out int comId);
            if(idValid)
            {
                Commute clickedCommute = dbContext.Commutes
                    .Include(c => c.startLocation)
                    .Include(c => c.endLocation)
                    .FirstOrDefault(c => c.Id == comId);
                ViewPools Data = new ViewPools()
                {
                    ClickedCommute = clickedCommute,
                    AllCommutes = AllCommutes
                };
                return View(Data);
            }
            else
            {
                Commute defaultCommute = dbContext.Commutes
                    .Include(c => c.startLocation)
                    .Include(c => c.endLocation)
                    .Where(c => c.Id != null).FirstOrDefault();
                ViewPools Data = new ViewPools()
                {
                    ClickedCommute = defaultCommute,
                    AllCommutes = AllCommutes
                };
                return View(Data);
            }
        }

        [HttpGet("delete/commutes/{id}")]
        public IActionResult DeleteCommute(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            Commute thisCommute = dbContext.Commutes
                .Where(u => u.Id == id)
                .FirstOrDefault();
            dbContext.Remove(thisCommute);
            dbContext.SaveChanges();
            int carpoolId = thisCommute.CarpoolId;
            return RedirectToAction("UpdateCarpool", new { id = carpoolId });
        }

        [HttpGet("carpool-details")]
        public IActionResult CarpoolDetails()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }    
            return View();
        }


        [HttpGet("update/carpool/{id}")]
        public IActionResult UpdateCarpool(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Carpool carpool = dbContext.Carpools.Where(c => c.Id == id)
                                .Include(c => c.user)
                                .Include(c => c.commutes)
                                .ThenInclude(com => com.startLocation)
                                .Include(c => c.commutes)
                                .ThenInclude(com => com.endLocation)
                                .Include(c => c.riderships)
                                .ThenInclude(r => r.user)
                                .FirstOrDefault();
            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.logged_in_user = logged_in_user;
            return View(carpool);
        }

        [HttpPost("updatedetails/carpool")]
        public IActionResult UpdateCarpoolDetails(string Name, string Description, int CarpoolId)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Carpool thisCarpool = dbContext.Carpools.Where(c => c.Id == CarpoolId).FirstOrDefault();
            thisCarpool.Name = Name;
            thisCarpool.Description = Description;
            dbContext.Update(thisCarpool);
            dbContext.SaveChanges();
            return RedirectToAction("UpdateCarpool", new { id = CarpoolId });
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
        public async Task<IActionResult> CreateLocation(FormLocation form)
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
                    UserId = CurrentUser.Id,
                };

                string fullAddress = newLocation.Address+"+"+newLocation.City+"+"+newLocation.State+"+"+newLocation.Zip;


                var place = await HttpService.GetGeoCode(fullAddress);
                if(place[0] != "ERROR")
                {
                    System.Console.WriteLine($"Lat: {place[0]} Long: {place[1]}");
                    string coords = "{lat: "+place[0]+", lng: "+place[1]+"}";
                    newLocation.Coords = coords;

                    dbContext.Add(newLocation);
                    dbContext.SaveChanges();
                    return RedirectToAction("CreateCommute");
                }
                else
                {
                    System.Console.WriteLine("Invalid address");
                    ModelState.AddModelError("Address", "Invalid Address");
                    return View("MyLocations");
                }
                
                }
            return View("MyLocations");
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

        [HttpPost("create-carpool")]
        public IActionResult CreateCarpool(CreateCarpool form)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                    .Include(u => u.carpools)
                                    .FirstOrDefault();

            if (ModelState.IsValid)
            {
                Carpool newCarpool = new Carpool()
                {
                    Name = form.Name,
                    UserId = logged_in_user.Id
                };

                dbContext.Add(newCarpool);
                dbContext.SaveChanges();

                int createdCarpoolId = dbContext.Carpools.Last().Id;
                return RedirectToAction("UpdateCarpool", new { id = createdCarpoolId});
            }
            else
            {
                ViewBag.logged_in_user = logged_in_user;
                return View("Dashboard");
            }
        }

        [HttpGet("carpool/{id}")]
        public IActionResult Carpool(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Carpool carpool = dbContext.Carpools.Where(c => c.Id == id)
                                .Include(c => c.user)
                                .Include(c => c.commutes)
                                .ThenInclude(com => com.startLocation)
                                .Include(c => c.commutes)
                                .ThenInclude(com => com.endLocation)
                                .Include(c => c.riderships)
                                .ThenInclude(r => r.user)
                                .FirstOrDefault();
            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.logged_in_user = logged_in_user;
            return View(carpool);
        }

        [HttpGet("delete-carpool/{id}")]
        public IActionResult DeleteCarpool(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            Carpool carpoolToRemove = dbContext.Carpools.Where(c => c.Id == id)
                                            .Include(c => c.user)
                                            .FirstOrDefault();

            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                            .Include(u => u.carpools)
                                            .FirstOrDefault();
            if (carpoolToRemove is null)
            {
                ViewBag.logged_in_user = logged_in_user;
                ModelState.AddModelError("Error", "Carpool for deletion does not exist");
                return View("Dashboard");
            }
            if (logged_in_user.Id == carpoolToRemove.user.Id)
            {
                dbContext.Remove(carpoolToRemove);
                dbContext.SaveChanges();


                ViewBag.logged_in_user = logged_in_user;

                return RedirectToAction("Dashboard");
            }
            
            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Insufficient privileges to delete that carpool");
            return View("Dashboard");
        }

        [HttpGet("ridership/join/{userid}/{carpoolid}")]
        public IActionResult RidershipJoin(int userid, int carpoolid)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            if (HttpContext.Session.GetInt32("LoggedInUserId") == userid)
            {
                Ridership newRidership = new Ridership()
                {
                    UserId = userid,
                    CarpoolId = carpoolid
                };

                dbContext.Add(newRidership);
                dbContext.SaveChanges();

                return RedirectToAction("Carpool", new { id = carpoolid});
            }
            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                            .Include(u => u.carpools)
                                            .FirstOrDefault();
            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Cannot join ridership for another user");
            return View("Dashboard");
        }
        [HttpGet("ridership/leave/{userid}/{carpoolid}")]
        public IActionResult RidershipLeave(int userid, int carpoolid)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                        .Include(u => u.carpools)
                                        .FirstOrDefault();
            
            if (HttpContext.Session.GetInt32("LoggedInUserId") == userid)
            {
                Ridership ridershipToRemove = dbContext.Riderships.Where(r => (r.CarpoolId == carpoolid) && (r.UserId == userid))
                                                .FirstOrDefault();
                dbContext.Remove(ridershipToRemove);
                dbContext.SaveChanges();

                
                ViewBag.logged_in_user = logged_in_user;

                return RedirectToAction("Dashboard");
            }

            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Cannot leave ridership for another user");
            return View("Dashboard");
        }

        
    }
}
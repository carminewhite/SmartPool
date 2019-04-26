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
    public class CarpoolController : Controller
    {
        private PoolContext dbContext;
    
        public CarpoolController(PoolContext context, HttpService httpservice)
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

            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                    .Include(u => u.carpools)
                                    .Include(u => u.riderships)
                                    .ThenInclude(r => r.carpool)
                                    .FirstOrDefault();
            ViewBag.logged_in_user = logged_in_user;
            return View();
        }



        [HttpGet("search")]
        public IActionResult SearchDefault()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            List<Commute> AllCommutes = dbContext.Commutes
                                            .Include(c => c.startLocation)
                                            .Include(c => c.endLocation)
                                            .Include(c => c.carpool)
                                            .ThenInclude(c => c.user)
                                            .Include(c => c.carpool)
                                            .ThenInclude(c => c.riderships)
                                            .ThenInclude(r => r.user)
                                            .Where(c => (c.carpool.user.Id != HttpContext.Session.GetInt32("LoggedInUserId")) && (!(c.carpool.riderships.Any(r => r.user.Id == HttpContext.Session.GetInt32("LoggedInUserId")))))
                                            .OrderBy(c => c.Day)
                                            .ThenBy(c => c.ArriveBy.Hour)
                                            .ToList();

            Commute defaultCommute = dbContext.Commutes
                                            .Include(c => c.startLocation)
                                            .Include(c => c.endLocation)
                                            .FirstOrDefault();
            ViewPools Data = new ViewPools()
            {
                ClickedCommute = defaultCommute,
                AllCommutes = AllCommutes
            };
            return View("Search", Data);
        }



        [HttpGet("search/{mappedCommuteId}")]
        public IActionResult Search()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            List<Commute> AllCommutes = dbContext.Commutes
                                            .Include(c => c.startLocation)
                                            .Include(c => c.endLocation)
                                            .Include(c => c.carpool)
                                            .ThenInclude(c => c.user)
                                            .Include(c => c.carpool)
                                            .ThenInclude(c => c.riderships)
                                            .ThenInclude(r => r.user)
                                            .Where(c => (c.carpool.user.Id != HttpContext.Session.GetInt32("LoggedInUserId")) && (!(c.carpool.riderships.Any(r => r.user.Id == HttpContext.Session.GetInt32("LoggedInUserId")))))
                                            .OrderBy(c => c.Day)
                                            .ThenBy(c => c.ArriveBy.Hour)
                                            .ToList();

            bool idValid = Int32.TryParse(RouteData.Values["mappedCommuteId"].ToString(), out int comId);

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



        [HttpGet("carpool/edit/{id}")]
        public IActionResult CarpoolEdit(int id)
        {
            HttpContext.Session.SetInt32("cpId", id);
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



        [HttpPost("carpool/update")]
        public IActionResult CarpoolUpdate(string Name, string Description, int CarpoolId)
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
            return RedirectToAction("CarpoolDefault", new { id = CarpoolId });
        }



        [HttpPost("carpool/create")]
        public IActionResult CarpoolCreate(CreateCarpool form)
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
                return RedirectToAction("CarpoolEdit", new { id = createdCarpoolId});
            }
            else
            {
                ViewBag.logged_in_user = logged_in_user;
                return View("Dashboard");
            }
        }



        [HttpGet("carpool/{id}")]
        public IActionResult CarpoolDefault(int id)
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

            Commute defaultCommute = dbContext.Commutes.Include(c => c.startLocation)
                                                    .Include(c => c.endLocation)
                                                    .Where(c => c.carpool.Id == id)
                                                    .FirstOrDefault();
            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.logged_in_user = logged_in_user;
            ViewBag.ClickedCommute = defaultCommute;
            return View("Carpool", carpool);
        }



        [HttpGet("carpool/{id}/{mappedCommuteId}")]
        public IActionResult Carpool(int id, int mappedCommuteId)
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

            Commute clickedCommute = dbContext.Commutes
                                                .Include(c => c.startLocation)
                                                .Include(c => c.endLocation)
                                                .FirstOrDefault(c => c.Id == mappedCommuteId);

            User logged_in_user = dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"));
            ViewBag.logged_in_user = logged_in_user;
            ViewBag.ClickedCommute = clickedCommute;
            return View(carpool);
        }



        [HttpGet("carpool/delete/{id}")]
        public IActionResult CarpoolDelete(int id)
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
                                            .Include(u => u.riderships)
                                            .ThenInclude(r => r.carpool)
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
    }
}
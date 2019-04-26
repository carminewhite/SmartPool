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
    public class RidershipController : Controller
    {
        private PoolContext dbContext;
    
        public RidershipController(PoolContext context, HttpService httpservice)
        {
            dbContext = context;
        }



        [HttpGet("ridership/join/{userId}/{carpoolId}")]
        public IActionResult RidershipJoin(int userId, int carpoolId)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            if (HttpContext.Session.GetInt32("LoggedInUserId") == userId)
            {
                Ridership newRidership = new Ridership()
                {
                    UserId = userId,
                    CarpoolId = carpoolId
                };

                dbContext.Add(newRidership);
                dbContext.SaveChanges();
                return RedirectToAction("CarpoolDefault", "Carpool", new { id = carpoolId});
            }
            User logged_in_user = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId"))
                                            .Include(u => u.carpools)
                                            .Include(u => u.riderships)
                                            .ThenInclude(r => r.carpool)
                                            .FirstOrDefault();
            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Cannot join ridership for another user");
            return View("Dashboard", "Carpool");
        }



        [HttpGet("ridership/leave/{userId}/{carpoolId}")]
        public IActionResult RidershipLeave(int userId, int carpoolId)
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
            
            if (HttpContext.Session.GetInt32("LoggedInUserId") == userId)
            {
                Ridership ridershipToRemove = dbContext.Riderships.Where(r => (r.CarpoolId == carpoolId) && (r.UserId == userId))
                                                .FirstOrDefault();
                dbContext.Remove(ridershipToRemove);
                dbContext.SaveChanges();

                
                ViewBag.logged_in_user = logged_in_user;

                return RedirectToAction("Dashboard", "Carpool");
            }

            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Cannot leave ridership for another user");
            return View("Dashboard", "Carpool");
        }

        [HttpGet("ridership/kick/{userId}/{carpoolId}")]
        public IActionResult RidershipKick(int userId, int carpoolId)
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

            Carpool carpool = dbContext.Carpools.Where(c => c.Id == carpoolId)
                                        .Include(c => c.user)
                                        .FirstOrDefault();
            
            if (HttpContext.Session.GetInt32("LoggedInUserId") == carpool.user.Id)
            {
                Ridership ridershipToRemove = dbContext.Riderships.Where(r => (r.CarpoolId == carpoolId) && (r.UserId == userId))
                                                .FirstOrDefault();
                dbContext.Remove(ridershipToRemove);
                dbContext.SaveChanges();

                
                ViewBag.logged_in_user = logged_in_user;

                return Redirect($"/carpool/edit/{carpoolId}");
            }

            ViewBag.logged_in_user = logged_in_user;
            ModelState.AddModelError("Error", "Insufficient privileges to kick user");
            return View("Dashboard", "Carpool");
        }
    }
}
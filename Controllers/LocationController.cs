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
    public class LocationController : Controller
    {
        private PoolContext dbContext;

        public LocationController(PoolContext context, HttpService httpservice)
        {
            dbContext = context;
        }



        [HttpGet("location/new")]
        public IActionResult LocationNew()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            return View();
        }



        [HttpPost("location/create")]
        public async Task<IActionResult> LocationCreate(FormLocation form)
        {   
            User CurrentUser = dbContext.Users.Where(u => u.Id == HttpContext.Session.GetInt32("LoggedInUserId")).FirstOrDefault();
            if(CurrentUser == null)
            {
                return RedirectToAction("Logout", "LoginReg");
            }

            if(ModelState.IsValid)
            {
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
                    return Redirect($"/commute/new/{HttpContext.Session.GetInt32("cpId")}");
                }
                else
                {
                    System.Console.WriteLine("Invalid address");
                    ModelState.AddModelError("Address", "Invalid Address");
                    return View("LocationNew");
                }
                
            }
            return View("LocationNew");
        }
    }
}
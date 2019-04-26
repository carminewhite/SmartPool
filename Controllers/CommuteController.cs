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
    public class CommuteController : Controller
    {
        private PoolContext dbContext;

        public CommuteController(PoolContext context, HttpService httpservice)
        {
            dbContext = context;
        }



        [HttpGet("commute/new/{carpoolId}")]
        public IActionResult CommuteNew(int carpoolId)
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") is null)
            {
                return RedirectToAction("Index", "LoginReg");
            }

            HttpContext.Session.SetInt32("cpId", carpoolId);

            List<Location> allLocations = dbContext.Locations
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            ViewBag.ListofLocations = allLocations;
            ViewBag.carpoolid = carpoolId;
            return View();
        }



        [HttpPost("commute/create")]
        public IActionResult CommuteCreate(CommuteForm form)
        {
            if(HttpContext.Session.Keys.Contains("LoggedInUserId"))
            {
                if(ModelState.IsValid)
                {
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
                    return RedirectToAction("CarpoolEdit", "Carpool", new {id = form.CarpoolId});

                }
                else
                {
                    List<Location> allLocations = dbContext.Locations
                        .OrderByDescending(u => u.CreatedAt)
                        .ToList();
                    ViewBag.ListofLocations = allLocations;
                    ViewBag.carpoolid = HttpContext.Session.GetInt32("cpId");
                    return View("CommuteNew");
                }
                
            }
            else
            {
                return RedirectToAction("Index", "LoginReg");
            }
        }



        [HttpGet("commute/delete/{id}")]
        public IActionResult CommuteDelete(int id)
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
            return RedirectToAction("CarpoolEdit", "Carpool", new { id = carpoolId });
        }
    }
}
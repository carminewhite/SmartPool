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
        private PoolContext dbContext;
        public PoolController(PoolContext context)
        {
            dbContext = context;
        }

    }

}
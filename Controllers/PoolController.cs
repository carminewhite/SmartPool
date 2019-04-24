using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartPool.Controllers
{
    public class ActivityController : Controller
    {
        private PoolContext dbContext;
        public ActivityController(PoolContext context)
        {
            dbContext = context;
        }

    }

}
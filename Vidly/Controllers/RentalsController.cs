﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View("List");
        }
    }
}
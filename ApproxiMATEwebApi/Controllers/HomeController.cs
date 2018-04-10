﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApproxiMATEwebApi.Models;
//using Microsoft.Extensions.Configuration;

namespace ApproxiMATEwebApi.Controllers
{
    public class HomeController : Controller
    {
        //public IConfiguration Configuration { get; set; }
        //public HomeController(IConfiguration config)
        //{
        //    Configuration = config;
        //}

        public IActionResult Index()
        {
            //ViewData["myConnectionString"] = Configuration.GetConnectionString("DefaultConnection").Substring(0,12);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

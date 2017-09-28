﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Interfaces;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class Cat2Controller : Controller
    {
        // GET: Cat2
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Remember.Web.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Admin/Test/

        public ActionResult Index()
        {
            //return View();
            return Content("Test");
        }

    }
}

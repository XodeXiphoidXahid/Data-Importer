﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    public class CustomFieldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

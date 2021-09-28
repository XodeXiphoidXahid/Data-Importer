using DataImporter.Web.Areas.Customer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    [Area("Customer"), Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new DashboardInfo();
            model.GetDashboardInfo();
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}

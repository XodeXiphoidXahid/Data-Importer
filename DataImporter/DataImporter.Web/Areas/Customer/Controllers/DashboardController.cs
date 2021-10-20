using DataImporter.Membership.Entities;
using DataImporter.Web.Areas.Customer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = new Guid(_userManager.GetUserId(HttpContext.User));
            var model = new DashboardInfo();
            model.GetDashboardInfo(userId);
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}

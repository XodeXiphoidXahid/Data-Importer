using DataImporter.Membership.Entities;
using DataImporter.Web.Areas.Customer.Models;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    [Area("Customer"), Authorize]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupController(ILogger<GroupController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = new GroupListModel();
            return View(model);
        }
        public JsonResult GetGroupData()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GroupListModel();
            var data = model.GetGroups(dataTablesModel, userId);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateGroupModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            

            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateGroup(userId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Create Group");
                    _logger.LogError(ex, "Create Group Failed");
                }

            }
            return View(model);
        }
    }
}

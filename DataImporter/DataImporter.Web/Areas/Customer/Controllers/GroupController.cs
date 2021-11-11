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
        public IActionResult Index(bool message=false)
        {
            var model = new GroupListModel();
            ViewBag.message = message;
            return View(model);
        }
        public JsonResult GetGroupData()
        {
            var userId = new Guid(_userManager.GetUserId(HttpContext.User));
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
            var userId = new Guid (_userManager.GetUserId(HttpContext.User));
            


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

        public IActionResult ViewGroupData(int id)
        {
            var model = new GroupDataModel();

            model.LoadGroupData(id);

            

            if (model.GroupData == null)
            {
                return RedirectToAction("Index","Group", new { message = true });
            }
                

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditGroupModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Edit Group");
                    _logger.LogError(ex, "Edit Group Failed");
                }

            }

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new GroupListModel();
            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}

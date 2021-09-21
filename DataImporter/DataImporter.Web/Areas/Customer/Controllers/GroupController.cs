using DataImporter.Web.Areas.Customer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    [Area("Customer")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;

        public GroupController(ILogger<GroupController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new CreateGroupModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateGroup();
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

using DataImporter.Import.Services;
using DataImporter.Import.UnitOfWorks;
using DataImporter.Membership.Entities;
using DataImporter.Web.Areas.Customer.Models;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    [Area("Customer")]
    public class ExportController : Controller
    {
        private readonly IExportService _exportService;
        private readonly ILogger<ExportController> _logger;
        private readonly IImportUnitOfWork _importUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExportController(ILogger<ExportController> logger, IExportService exportService,  IImportUnitOfWork importUnitOfWork, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _exportService = exportService;
            _importUnitOfWork = importUnitOfWork;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var model = new ExportHistoryModel();
            return View(model);
        }

        public JsonResult GetExportHistory()
        {
            var userId = new Guid(_userManager.GetUserId(HttpContext.User));

            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new ExportHistoryModel();
            var data = model.GetExportHistories(dataTablesModel, userId);
            return Json(data);
        }

        public IActionResult ExportAsExcel(int id)//group Id
        {
             //PendingExportHistory te jdi age theke groupId ta thake tahole ekta error dekhaite hbe je "Your previous export request of this group is pending, please wait"
            _exportService.UpdateExportHistory(id);//update both pending and export history
            return RedirectToAction("index", "group");
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}

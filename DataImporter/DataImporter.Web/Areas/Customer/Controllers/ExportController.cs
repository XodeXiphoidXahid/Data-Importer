using DataImporter.Import.Services;
using Microsoft.AspNetCore.Mvc;
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
        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ExportAsExcel(int id)
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

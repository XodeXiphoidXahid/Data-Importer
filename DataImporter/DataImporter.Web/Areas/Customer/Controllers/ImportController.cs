using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using DataImporter.Import.UnitOfWorks;
using DataImporter.Membership.Entities;
using DataImporter.Web.Areas.Customer.Models;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.User.Controllers
{
    [Area("Customer"), Authorize]
    public class ImportController : Controller
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IImportService _importService;
        private IWebHostEnvironment _environment;
        private readonly IImportUnitOfWork _importUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public ImportController(ILogger<ImportController> logger, IImportService importService, IWebHostEnvironment environment, IImportUnitOfWork importUnitOfWork, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _importService = importService;
            _environment = environment;
            _importUnitOfWork = importUnitOfWork;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Upload()
        {
            var model = new FileLocationModel();
            
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upload(IFormFile file, FileLocationModel model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            string wwwPath = this._environment.WebRootPath;
            string contentPath = this._environment.ContentRootPath;

            string path = Path.Combine(this._environment.WebRootPath, "uploadExcel");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    model.SaveFileInfo(fileName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Create Group");
                    _logger.LogError(ex, "Create Group Failed");
                }

            }
            return View(model);
            
        }

        public IActionResult Read()
        {
            var excelFilePath = Path.Combine(_environment.WebRootPath, "uploadExcel");

            DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
            FileInfo[] fileInfo = directoryInfo.GetFiles();

            foreach(var file in fileInfo)
            
            {
                using (var stream = System.IO.File.OpenRead(file.ToString()))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package= new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                    }
                }
            }
            return View();
        }

        public JsonResult GetGroupList(string searchTerm)
        {
            var groupList = _importUnitOfWork.Groups.GetAll().ToList();

            if(searchTerm != null)
            {
                groupList = _importUnitOfWork.Groups.GetAll().Where(x => x.Name.Contains(searchTerm)).ToList();
            }

            var modifiedData = groupList.Select(x => new
            {
                id=x.Id,
                text=x.Name
            });

            return Json(modifiedData);
        }
    }
}

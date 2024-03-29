﻿using DataImporter.Common.Utilities;
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
            var model = new ImportHistoryModel();
            return View(model);
        }

        public JsonResult GetImportHistory()
        {
            var userId = new Guid( _userManager.GetUserId(HttpContext.User));

            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new ImportHistoryModel();
            var data = model.GetImportHistories(dataTablesModel, userId);
            return Json(data);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Upload(bool message=false)
        {
            var model = new FileLocationModel();
            ViewBag.message = message;
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upload(IFormFile file, FileLocationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.RightGroup(file);
                    if(result.rightGroup)
                    {
                        model.SaveFileInfo(file.FileName, file);//file ta save korar somoe import history taw save korte hbe
                    }
                       
                    
                    else
                    {
                        ModelState.AddModelError("CustomError", "Please select or create a appropriate Group for your file");
                        return RedirectToAction("Upload", new { message=true});
                    }
                    

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Name", "Failed to Upload file");
                    _logger.LogError(ex, "Failed to Upload file");
                    
                }
            }
            return RedirectToAction("Index", "Import");
            
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
            var userId = new Guid(_userManager.GetUserId(HttpContext.User));

            var groupList = _importUnitOfWork.Groups.GetAll().Where(x=>x.ApplicationUserId==userId ).ToList();
            

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

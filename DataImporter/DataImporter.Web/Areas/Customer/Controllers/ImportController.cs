using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using DataImporter.Web.Areas.Customer.Models;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Area("Customer")]
    public class ImportController : Controller
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IImportService _importService;
        private IWebHostEnvironment _environment;
        
        

        public ImportController(ILogger<ImportController> logger, IImportService importService, IWebHostEnvironment environment)
        {
            _logger = logger;
            _importService = importService;
            _environment = environment;
           
            
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
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upload(IFormFile file)
        {

            
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

            
            return View();
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
    }
}

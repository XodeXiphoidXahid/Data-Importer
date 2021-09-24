using ClosedXML.Excel;
using DataImporter.Import.UnitOfWorks;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public class ExportService : IExportService
    {
        private readonly IImportUnitOfWork _importUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExportService(IImportUnitOfWork importUnitOfWork, UserManager<ApplicationUser> userManager)
        {
            _importUnitOfWork = importUnitOfWork;
            _userManager = userManager;
        }
        public void ExportDbData()
        {
            var allRecords = _importUnitOfWork.ExcelDatas.GetAll();


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("TestFile");
                //ekhane column list ta lagbe
                List<string> colList = new List<string>();
                int grpColNumber = 4;
                int cnt = 0;
                foreach (var record in allRecords)
                {
                    cnt++;
                    if (cnt == grpColNumber)
                        break;

                    colList.Add(record.Key.ToString());
                }
                //xl er first row initialize kora holo
                int col = 1;
                foreach(var key in colList)
                {
                    worksheet.Cell(1, col).Value = key;
                    col++;
                }

                col = 1;
                int row = 2;

                foreach(var record in allRecords)
                {
                    worksheet.Cell(row, col).Value = record.Value;

                    if (col == grpColNumber)
                        col = 1;

                    col++;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    using (var fileStream = new FileStream("D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\DataImporter\\DataImporter.Web\\wwwroot", FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
                
        }
        //ei method ta ekta grpId rcv korbe, pore oi grp id theke userId ber kore oi userId diye grp create korbe, oi grp e grp er data export kore file baniye rakhbo, sei file er nam ta kono variable e save rakhbo jate file ta return korte pari
        public FileInfo GetFile(int groupId)
        {
            //Get the userId
            var userId = _importUnitOfWork.Groups.Get(x => x.Id == groupId, null).Select(x => x.UserId).FirstOrDefault();

            //Create/Check of the user's folder
            string path = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles"+userId;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = fileName + ".xlsx";
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }


        }
    }
}

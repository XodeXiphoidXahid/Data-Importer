using ClosedXML.Excel;
using DataImporter.Import.UnitOfWorks;
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
        public ExportService(IImportUnitOfWork importUnitOfWork)
        {
            _importUnitOfWork = importUnitOfWork;
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
    }
}

using DataImporter.Import.BusinessObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public interface IImportService
    {
        void SaveExcelInRoot(IFormFile file);
        void SaveExcelInDb(FileInfo[] fileInfo);
        void SaveFileInfo(FileLocation fileLocation, IFormFile file);
        (bool rightGroup, List<string> data, int? colNum) CheckColumn(IFormFile file, int groupId);
        void Import();
        (IList<ImportHistory> records, int total, int totalDisplay) GetImportHistories(int pageIndex, int pageSize, string searchText, DateTime startDate, DateTime endDate, string v, Guid userId);
        //bool CheckColumn(IFormFile file, int groupId);
    }
}

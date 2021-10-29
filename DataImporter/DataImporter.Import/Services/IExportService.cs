using DataImporter.Import.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public interface IExportService
    {
        void ExportDbData(int groupId);
        FileInfo GetFile(int groupId);
        void ExportFile();
        void UpdateExportHistory(int id, DateTime dateTime);
        (IList<ExportHistory> records, int total, int totalDisplay) GetExportHistories(int pageIndex, int pageSize, string searchText, DateTime startDate, DateTime endDate, string v, Guid userId);
    }
}

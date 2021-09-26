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
        void UpdateExportHistory(int id);
    }
}

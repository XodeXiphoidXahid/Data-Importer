using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public interface IExportService
    {
        void ExportDbData();
        object GetFile(int groupId);
    }
}

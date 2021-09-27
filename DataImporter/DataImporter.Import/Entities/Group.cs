using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Data;

namespace DataImporter.Import.Entities
{
    public class Group: IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public List<ExcelData> ExcelDatas { get; set; }

        public List<FileLocation> FileLocations { get; set; }

        public GroupColumnName GroupColumnName { get; set; }

        public PendingExportHistory PendingExportHistory { get; set; }
        
        public ExportEmailHit ExportEmailHit { get; set; }

        public List<ImportHistory> ImportHistories { get; set; }

        public List<ExportHistory> ExportHistories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.BusinessObjects
{
    public class ExportHistory
    {
        public int Id { get; set; }
        public DateTime ExportDate { get; set; }
        public string Status { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
    }
}

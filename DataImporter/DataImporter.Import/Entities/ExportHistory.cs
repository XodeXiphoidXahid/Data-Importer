using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Entities
{
    public class ExportHistory: IEntity<int>
    {
        public int Id { get; set; }
        public DateTime ExportDate { get; set; }
        public string Status { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}

using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Entities
{
    public class ImportHistory : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}

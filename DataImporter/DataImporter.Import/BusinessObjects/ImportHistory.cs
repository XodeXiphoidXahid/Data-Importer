using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.BusinessObjects
{
    public class ImportHistory
    {
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
    }
}

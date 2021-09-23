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

        public List<ExcelData> ExcelDatas { get; set; }

        public List<FileLocation> FileLocations { get; set; }

        public GroupColumnName GroupColumnName { get; set; }
        
    }
}

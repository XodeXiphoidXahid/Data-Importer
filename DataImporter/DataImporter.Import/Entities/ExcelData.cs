using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Entities
{
    public class ExcelData: IEntity<int>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Group Group { get; set; }
    }
}

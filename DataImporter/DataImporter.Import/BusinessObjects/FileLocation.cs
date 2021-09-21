using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.BusinessObjects
{
    public class FileLocation
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int GroupId { get; set; }
    }
}

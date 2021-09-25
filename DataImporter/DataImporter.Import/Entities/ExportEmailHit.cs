using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Entities
{
    public class ExportEmailHit: IEntity<int>//One to one with Group
    {
        public int Id { get; set; }
        public int ExportHit { get; set; }
        public int EmailHit { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}

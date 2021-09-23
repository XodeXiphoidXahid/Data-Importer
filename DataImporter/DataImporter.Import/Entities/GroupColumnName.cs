using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Entities
{
    public class GroupColumnName : IEntity<int>//One to One
    {
        public int Id { get; set; }
        public string ColumnList { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

    }
}

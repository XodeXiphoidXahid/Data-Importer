using DataImporter.Data;
using DataImporter.Import.Contexts;
using DataImporter.Import.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Repositories
{
    public class GroupColumnNameRepository : Repository<GroupColumnName, int>, IGroupColumnNameRepository
    {
        public GroupColumnNameRepository(ImportDbContext context)
            : base((DbContext)context)
        {

        }
    }
}

using DataImporter.Data;
using DataImporter.Import.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.UnitOfWorks
{
    public interface IImportUnitOfWork: IUnitOfWork
    {
        IGroupRepository Groups { get; }
        IExcelDataRepository ExcelDatas { get; }
    }
}

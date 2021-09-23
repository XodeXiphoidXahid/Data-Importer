using DataImporter.Data;
using DataImporter.Import.Contexts;
using DataImporter.Import.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.UnitOfWorks
{
    public class ImportUnitOfWork: UnitOfWork, IImportUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public IExcelDataRepository ExcelDatas { get; private set; }
        public IFileLocationRepository FileLocations { get; private set; }
        public IGroupColumnNameRepository GroupColumnNames { get; private set; }
        public ImportUnitOfWork(ImportDbContext context,
            IGroupRepository groups, IExcelDataRepository excelDatas, IFileLocationRepository fileLocations, IGroupColumnNameRepository groupColumnNames)
            : base(context)
        {
            Groups = groups;
            ExcelDatas = excelDatas;
            FileLocations = fileLocations;
            GroupColumnNames = groupColumnNames;
        }
    }
}

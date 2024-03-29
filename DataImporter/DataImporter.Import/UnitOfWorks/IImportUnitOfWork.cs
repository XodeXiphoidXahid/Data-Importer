﻿using DataImporter.Data;
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
        IFileLocationRepository FileLocations { get; }
        IGroupColumnNameRepository GroupColumnNames { get; }
        IPendingExportHistoryRepository PendingExportHistories { get; }
        IImportHistoryRepository ImportHistories { get; }
        IExportHistoryRepository ExportHistories { get; }
        IExportEmailHitRepository ExportEmailHits { get; }
        IEmailFileRepository EmailFiles { get; }
    }
}

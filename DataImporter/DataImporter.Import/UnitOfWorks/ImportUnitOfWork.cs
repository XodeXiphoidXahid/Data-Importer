﻿using DataImporter.Data;
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
        public IPendingExportHistoryRepository PendingExportHistories { get; private set; }

        public IImportHistoryRepository ImportHistories { get; private set; }

        public IExportHistoryRepository ExportHistories { get; private set; }

        public IExportEmailHitRepository ExportEmailHits { get; private set; }
        
        public IEmailFileRepository EmailFiles { get; private set; }

        

        public ImportUnitOfWork(ImportDbContext context,
            IGroupRepository groups, IExcelDataRepository excelDatas, IFileLocationRepository fileLocations, IGroupColumnNameRepository groupColumnNames, IPendingExportHistoryRepository pendingExportHistories, IImportHistoryRepository importHistories, IExportHistoryRepository exportHistories, IExportEmailHitRepository exportEmailHits, IEmailFileRepository emailFiles)
            : base(context)
        {
            Groups = groups;
            ExcelDatas = excelDatas;
            FileLocations = fileLocations;
            GroupColumnNames = groupColumnNames;
            PendingExportHistories = pendingExportHistories;
            ImportHistories = importHistories;
            ExportHistories = exportHistories;
            ExportEmailHits = exportEmailHits;
            EmailFiles = emailFiles;
        }
    }
}

using Autofac;
using DataImporter.Import.Contexts;
using DataImporter.Import.Repositories;
using DataImporter.Import.Services;
using DataImporter.Import.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import
{
    public class ImportModule: Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public ImportModule(string connectionString, string migrationAssemblyName, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImportDbContext>().AsSelf().WithParameter("connectionString", _connectionString).WithParameter("migrationAssemblyName", _migrationAssemblyName).InstancePerLifetimeScope();

            builder.RegisterType<ImportDbContext>().As<IImportDbContext>().WithParameter("connectionString", _connectionString).WithParameter("migrationAssemblyName", _migrationAssemblyName).InstancePerLifetimeScope();

            builder.RegisterType<ImportService>().As<IImportService>().InstancePerLifetimeScope();
            builder.RegisterType<ExportService>().As<IExportService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupService>().As<IGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailFileService>().As<IEmailFileService>().InstancePerLifetimeScope();


            builder.RegisterType<ImportUnitOfWork>().As<IImportUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileLocationRepository>().As<IFileLocationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GroupColumnNameRepository>().As<IGroupColumnNameRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PendingExportHistoryRepository>().As<IPendingExportHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ImportHistoryRepository>().As<IImportHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryRepository>().As<IExportHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExportEmailHitRepository>().As<IExportEmailHitRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmailFileRepository>().As<IEmailFileRepository>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

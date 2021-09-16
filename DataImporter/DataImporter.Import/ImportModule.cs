﻿using Autofac;
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
            builder.RegisterType<ImportService>().As<IImportService>().InstancePerLifetimeScope();

            builder.RegisterType<ImportUnitOfWork>().As<IImportUnitOfWork>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

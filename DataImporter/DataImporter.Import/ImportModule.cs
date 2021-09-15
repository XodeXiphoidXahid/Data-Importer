using Autofac;
using DataImporter.Import.Services;
using DataImporter.Import.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import
{
    public class ImportModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImportService>().As<IImportService>().InstancePerLifetimeScope();

            builder.RegisterType<ImportUnitOfWork>().As<IImportUnitOfWork>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

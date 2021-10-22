using Autofac;
using DataImporter.Common.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common
{
    public class CommonModule: Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ImportExportStatus>().As<IImportExportStatus>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>().As<IEmailService>()
                .WithParameter("host", "smtp.gmail.com")
                .WithParameter("port", 465)
                .WithParameter("username", "zahidsheikh1521996@gmail.com")
                .WithParameter("password", "dotnetdeveloperzahidsheikhuap9696")
                .WithParameter("useSSL", true)
                .WithParameter("from", "zahidsheikh1521996@gmail.com")
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

using Autofac;
using DataImporter.Import.BusinessObjects;
using DataImporter.Import.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class FileLocationModel
    {
        public int GroupId { get; set; }

        private readonly IImportService _importService;

        public FileLocationModel()
        {
            _importService = Startup.AutofacContainer.Resolve<IImportService>();
        }
        public FileLocationModel(IImportService importService)
        {
            _importService = importService;
        }

        internal void SaveFileInfo(string fileName)
        {
            var fileLocation = new FileLocation
            {
                GroupId=GroupId,
                FileName=fileName
            };

            _importService.SaveFileInfo(fileLocation);
        }
    }
}

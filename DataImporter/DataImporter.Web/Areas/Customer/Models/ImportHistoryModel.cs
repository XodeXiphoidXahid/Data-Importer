using Autofac;
using DataImporter.Import.Services;
using DataImporter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class ImportHistoryModel
    {
        private IImportService _importService;

        public ImportHistoryModel()
        {
            _importService = Startup.AutofacContainer.Resolve<IImportService>();
        }

        public ImportHistoryModel(IImportService importService)
        {
            _importService = importService;
        }

        internal object GetImportHistories(DataTablesAjaxRequestModel tableModel, Guid userId)
        {
            var data = _importService.GetImportHistories(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "ImportDate", "GroupId" }), userId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.CreateDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}

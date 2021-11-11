using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using DataImporter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class ExportHistoryModel
    {
        private IExportService _exportService;
        private IDateTimeUtility _dateTimeUtility;

        public ExportHistoryModel()
        {
            _exportService = Startup.AutofacContainer.Resolve<IExportService>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();
        }

        

        public ExportHistoryModel(IExportService exportService, IDateTimeUtility dateTimeUtility)
        {
            _exportService = exportService;
            _dateTimeUtility = dateTimeUtility;
        }
        internal bool GroupIdAlreadyExistOrNot(int id)
        {
            return _exportService.GroupIdAlreadyExistOrNot(id);
        }

        internal object GetExportHistories(DataTablesAjaxRequestModel tableModel, Guid userId)
        {
            var data = _exportService.GetExportHistories(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.StartDate,
                tableModel.EndDate,
                tableModel.GetSortText(new string[] { "GroupId", "ExportDate", "Status" }), userId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.ExportDate.ToString(),
                                record.Status,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void UpdateExportHistory(int id)
        {
            _exportService.UpdateExportHistory(id, _dateTimeUtility.Now);
        }
    }
}


using Autofac;
using DataImporter.Import.Services;
using DataImporter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class GroupListModel
    {
        private IGroupService _groupService;

        public GroupListModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }

        public GroupListModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        internal object GetGroups(DataTablesAjaxRequestModel tableModel, Guid userId)
        {
            var data = _groupService.GetGroups(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.StartDate,
                tableModel.EndDate,
                tableModel.GetSortText(new string[] { "Name", "CreateDate" }), userId);

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

        internal void Delete(int id)
        {
            _groupService.DeleteGroup(id);
        }
    }
}

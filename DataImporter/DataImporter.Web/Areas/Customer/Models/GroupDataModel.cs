using Autofac;
using DataImporter.Import.Entities;
using DataImporter.Import.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class GroupDataModel
    {
        public List<Dictionary<string, string>> GroupData;
        public List<string> AllColumns;
        public List<string> rowValues;

        private readonly IGroupService _groupService;
        public GroupDataModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();

        }
        public GroupDataModel(IGroupService groupService)
        {
            _groupService = groupService;

        }
        internal void LoadGroupData(int id)
        {
            var GroupInfo=_groupService.GetGroupData(id);

            GroupData = GroupInfo.groupData;
            AllColumns = GroupInfo.allColumns;
            
        }
    }
}

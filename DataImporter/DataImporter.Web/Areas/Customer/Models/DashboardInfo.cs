using Autofac;
using DataImporter.Import.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class DashboardInfo
    {
        public int TotalGroup { get; set; }
        public int TotalImport { get; set; }
        public int TotalExport { get; set; }

        private readonly IGroupService _groupService;
        

        public DashboardInfo()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
           
        }
        public DashboardInfo(IGroupService groupService)
        {
            _groupService = groupService;

        }

        internal void GetDashboardInfo()
        {
            var dashboardInfo = _groupService.GetDashboardInfo();

            TotalGroup = dashboardInfo.TotalGroup;
            TotalExport = dashboardInfo.TotalExport;
            TotalImport = dashboardInfo.TotalImport;
        }
    }
}

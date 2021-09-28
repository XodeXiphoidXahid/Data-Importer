using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Import.BusinessObjects;
using DataImporter.Import.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class CreateGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        

        private readonly IGroupService _groupService;
        private readonly IDateTimeUtility _dateTimeUtility;
        
        public CreateGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _dateTimeUtility= Startup.AutofacContainer.Resolve<IDateTimeUtility>();
        }
        public CreateGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
            
        }
        internal void CreateGroup(string userId)
        {
            var group = new Group
            {
                Name = Name,
                CreateDate=_dateTimeUtility.Now
            };

            _groupService.CreateGroup(group, userId);
        }
    }
}

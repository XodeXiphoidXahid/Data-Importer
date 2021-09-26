using Autofac;
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
        public DateTime CreateDate { get; set; }

        private readonly IGroupService _groupService;
        
        public CreateGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
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
                CreateDate=CreateDate
            };

            _groupService.CreateGroup(group, userId);
        }
    }
}

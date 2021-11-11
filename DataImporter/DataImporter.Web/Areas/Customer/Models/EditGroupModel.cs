using Autofac;
using DataImporter.Import.BusinessObjects;
using DataImporter.Import.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class EditGroupModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        private readonly IGroupService _groupService;

        public EditGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }

        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);

            Id = group?.Id;
            Name = group?.Name;
        }

        internal void Update()
        {
            var group = new Group
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name
            };

            _groupService.UpdateGroup(group);
        }
    }
}

using DataImporter.Import.BusinessObjects;
using DataImporter.Import.Exceptions;
using DataImporter.Import.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public class GroupService : IGroupService
    {
        private readonly IImportUnitOfWork _importUnitOfWork;

        public GroupService(IImportUnitOfWork importUnitOfWork)
        {
            _importUnitOfWork = importUnitOfWork;
        }
        public void CreateGroup(Group group, string userId)
        {
            if (group == null)
                throw new InvalidParameterException("Group was not provided");

            _importUnitOfWork.Groups.Add(
                new Entities.Group
                {
                    Name = group.Name,
                    UserId=userId
                }
            );

            _importUnitOfWork.Save();
        }
    }
}

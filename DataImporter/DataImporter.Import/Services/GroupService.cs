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
                    UserId = userId
                }
            );

            _importUnitOfWork.Save();
        }

        public (IList<Group> records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize, string searchText, string sortText,string userId)
        {
            var groupData = _importUnitOfWork.Groups.GetDynamic(
                 string.IsNullOrWhiteSpace(searchText) ? null : x=>(x.Name.Contains(searchText)) && (x.UserId==userId)
                , sortText, string.Empty, pageIndex, pageSize);

            //var groupList = _importUnitOfWork.Groups.Get(x => x.UserId == userId, string.Empty);
            // GetDynamic(
            //string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText)
            //, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from grp in groupData.data.Where(x=>x.UserId==userId)
                              select new Group
                              {
                                  Id = grp.Id,
                                  Name = grp.Name,
                                  CreateDate = grp.CreateDate
                                  
                              }).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }
    }

}


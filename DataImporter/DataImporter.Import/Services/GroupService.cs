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
        public void CreateGroup(Group group, Guid userId)
        {
            if (group == null)
                throw new InvalidParameterException("Group was not provided");

            _importUnitOfWork.Groups.Add(
                new Entities.Group
                {
                    Name = group.Name,
                    ApplicationUserId = userId,
                    CreateDate=group.CreateDate
                }
            );

            _importUnitOfWork.Save();
        }

        public DashboardInfo GetDashboardInfo(Guid userId)
        {
            //var TotalImport = _importUnitOfWork.Groups.Get(x => x.ApplicationUserId == userId, String.Empty).Select(x => x.ImportHistories).Count();
            //var TotalImport = _importUnitOfWork.ImportHistories.GetAll().Where(x => x.Group.ApplicationUserId == userId).Count();

            var groups = _importUnitOfWork.Groups.Get(x => x.ApplicationUserId == userId, string.Empty).Select(x=>x.Id).ToList();

            var groupImport= _importUnitOfWork.ImportHistories.GetAll().Select(x => x.GroupId).ToList();
            var groupExport = _importUnitOfWork.ExportHistories.GetAll().Select(x => x.GroupId).ToList();
            var totalGroup = _importUnitOfWork.Groups.Get(x => x.ApplicationUserId == userId, String.Empty).Count();

            int totalImport = 0;
            foreach(var groupId in groups)
            {
                totalImport += CountGroupOccurence(groupImport, groupId);
            }

            int totalExport = 0;
            foreach (var groupId in groups)
            {
                totalExport += CountGroupOccurence(groupExport, groupId);
            }

            return new DashboardInfo
            {
                TotalExport= totalExport,
                TotalImport= totalImport,
                TotalGroup= totalGroup
            };

        }

        private int CountGroupOccurence(List<int> list, int target)
        {
            return  list.Where(groupId => groupId.Equals(target))
                    .Select(groupId => groupId)
                    .Count(); 
        }

        public (List<Dictionary<string, string>> groupData, List<string> allColumns) GetGroupData(int groupId)
        {
            var groupData = _importUnitOfWork.ExcelDatas.Get(x => x.GroupId == groupId, string.Empty);
            if (groupData.Count == 0)
                return (null, null);
            var columnList = _importUnitOfWork.GroupColumnNames.Get(x => x.GroupId == groupId, string.Empty).Select(x=>x.ColumnList).FirstOrDefault();
            var allColumns = GetColumns(columnList);

            allColumns.RemoveAt(allColumns.Count - 1);

            var columnNumber = allColumns.Count;

            List<Dictionary<string, string>> rowDataList = new List<Dictionary<string,string>>();
            Dictionary<string,string> tempList = new Dictionary<string, string>();


            foreach(var data in groupData)
            {
                tempList.Add(data.Key, data.Value);

                if(tempList.Count==columnNumber)
                {
                    
                    rowDataList.Add(new Dictionary<string, string>(tempList));
                    tempList.Clear();
                    
                }
                //if (tempList.Count == columnNumber)
                //    tempList.Clear();
            }
            return (rowDataList, allColumns);
        }

        private List<string> GetColumns(string columnList)
        {
            var splitColumnList = columnList.Split("~");
            List<string> result = new List<string>();

            foreach (var column in splitColumnList)
                result.Add(column);

            return result;
        }

        public (IList<Group> records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize, string searchText, string sortText,Guid userId)
        {
            var groupData = _importUnitOfWork.Groups.GetDynamic(
                 string.IsNullOrWhiteSpace(searchText) ? null : x=>(x.Name.Contains(searchText)) && (x.ApplicationUserId ==userId)
                , sortText, string.Empty, pageIndex, pageSize);

            //var groupList = _importUnitOfWork.Groups.Get(x => x.UserId == userId, string.Empty);
            // GetDynamic(
            //string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText)
            //, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from grp in groupData.data.Where(x=>x.ApplicationUserId==userId)
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


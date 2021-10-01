﻿using DataImporter.Import.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public interface IGroupService
    {
        void CreateGroup(Group group, Guid userId);
        (IList<Group> records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize, string searchText, string sortText, Guid userId);
        DashboardInfo GetDashboardInfo();
        (List<Dictionary<string, string>> groupData, List<string> allColumns) GetGroupData(int id);
    }
}

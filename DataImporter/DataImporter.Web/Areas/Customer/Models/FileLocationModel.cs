﻿using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Import.BusinessObjects;
using DataImporter.Import.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Areas.Customer.Models
{
    public class FileLocationModel
    {
        public int GroupId { get; set; }
        public DateTime ImportDate { get; set; }

        private readonly IImportService _importService;
        private readonly IDateTimeUtility _dateTimeUtility;

        public FileLocationModel()
        {
            _importService = Startup.AutofacContainer.Resolve<IImportService>();
        }
        public FileLocationModel(IImportService importService, IDateTimeUtility dateTimeUtility)
        {
            _importService = importService;
            _dateTimeUtility = dateTimeUtility;
        }

        internal void SaveFileInfo(string fileName, IFormFile file)
        {
            fileName = RandomString(fileName.Length);

            var fileLocation = new FileLocation
            {
                GroupId=GroupId,
                FileName=fileName,
                ImportDate=_dateTimeUtility.Now
            };

            _importService.SaveFileInfo(fileLocation, file);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal (bool rightGroup, List<string> data,int? colNum) RightGroup(IFormFile file)
        {
            //return _importService.CheckColumn(file, GroupId);
            //public (bool rightGroup, List<string> data) CheckColumn(IFormFile file, int groupId);
            var rightGroupInfo = _importService.CheckColumn(file, GroupId);
            return (rightGroupInfo.rightGroup, rightGroupInfo.data, rightGroupInfo.colNum);
        }
    }
}

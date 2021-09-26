﻿using ClosedXML.Excel;
using DataImporter.Import.UnitOfWorks;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public class ExportService : IExportService
    {
        private readonly IImportUnitOfWork _importUnitOfWork;
        
        public ExportService(IImportUnitOfWork importUnitOfWork)
        {
            _importUnitOfWork = importUnitOfWork;
            
        }
        public void ExportDbData(int groupId)
        {
            var allRecords = _importUnitOfWork.ExcelDatas.Get(x=>x.GroupId==groupId, string.Empty);


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("TestFile");
                //ekhane column list ta lagbe--GroupColumnNames theke ante hbe.
                List<string> colList = new List<string>();
                int grpColNumber = 4;
                int cnt = 0;
                foreach (var record in allRecords)
                {
                    colList.Add(record.Key.ToString());

                    cnt++;
                    if (cnt == grpColNumber)
                        break;

                    
                }
                //xl er first row initialize kora holo
                int col = 1;
                foreach(var key in colList)
                {
                    worksheet.Cell(1, col).Value = key;
                    col++;
                }

                col = 1;
                int row = 2;

                foreach(var record in allRecords)
                {
                    worksheet.Cell(row, col).Value = record.Value;//21

                    if (col == grpColNumber)
                    {
                        col = 0;
                        row++;
                    }

                    col++;
                    
                }
                var groupName = _importUnitOfWork.Groups.GetById(groupId).Name;
                var userId = _importUnitOfWork.Groups.Get(x => x.Id == groupId, string.Empty).Select(x => x.UserId).FirstOrDefault();
                //--Here we need to create specific folder for each user to save their group files--
                string path = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles\\" + groupId+"\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = groupName + ".xlsx";
                path += fileName;
                 //path = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles\\" + "FileName.xlsx";
                workbook.SaveAs(path);
                //using (var stream = new MemoryStream())
                //{
                //    worksheet.SaveAs(stream);

                //    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                //    {
                //        stream.CopyTo(fileStream);
                //    }
                //}
            }
                
        }

        public void ExportFile()
        {
            //first e PendingExportHistory check korbo
            //sekhan theke grpId niye, ExportFile folder e dhuke user er folder e dhuke je GroupName_UserId file ta delete korbe(Jdi age theke oi file ta exist kore rki)
            //pore oi groupid er data gulo ekta file e save kore sei file ta user folder e save rakhbe.
            //PendingExportHistory theke oi grpid ta delete korbe
            //ExportEmailHit entity te exporthit 1+ kore dibe

            //---Need to write the code to implement the above concept--//
            ExportDbData(19);
        }

        //ei method ta ekta grpId rcv korbe, pore oi grp id theke userId ber kore oi userId diye grp create korbe, oi grp e grp er data export kore file baniye rakhbo, sei file er nam ta kono variable e save rakhbo jate file ta return korte pari
        public FileInfo GetFile(int groupId)//eta SendEmail WorkerService theke call hbe.
        {
            //Get the userId
            var userId = _importUnitOfWork.Groups.Get(x => x.Id == groupId, null).Select(x => x.UserId).FirstOrDefault();
            var groupName = _importUnitOfWork.Groups.Get(x => x.Id == groupId, null).Select(x => x.Name).FirstOrDefault();
            var fileName = groupName + "_" + userId.ToString() + ".xlsx";

            //Create/Check of the user's folder
            string path = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles"+userId;

            if (!Directory.Exists(path))//Eta naw drkr porte pare, karon group age theke delete korle sei user
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = directoryInfo.GetFiles();//user er grp er shb file gulo ke fetch kora holo.

            FileInfo expectedFile = null;

            foreach(var file in fileInfo)
            {
                if (file.Name.Equals(fileName))
                {
                    expectedFile = file;
                    break;
                }
            }
            return expectedFile;

            //fileName = fileName + ".xlsx";
            //using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            //{
            //    file.CopyTo(stream);
            //}


        }
    }
}

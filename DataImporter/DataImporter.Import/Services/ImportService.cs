﻿using DataImporter.Import.BusinessObjects;
using DataImporter.Import.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public class ImportService : IImportService
    {
        private readonly IImportUnitOfWork _importUnitOfWork;


        public ImportService(IImportUnitOfWork importUnitOfWork)
        {
            _importUnitOfWork = importUnitOfWork;
        }
        public void SaveExcelInDb(FileInfo[] fileInfo)
        {
            foreach (var file in fileInfo)
            {
                List<string> colList = new List<string>();

                using (var stream = System.IO.File.OpenRead(file.ToString()))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                        var rowAndColInfo = GetRowAndColNumber(workSheet);

                        for (int col=1; col<= rowAndColInfo.colCount; col++)
                        {
                            colList.Add(workSheet.Cells[1, col].Value.ToString().Trim());
                        }

                        for(int row=2; row<= rowAndColInfo.rowCount; row++)
                        {
                            for(int col=1;col<= rowAndColInfo.colCount; col++)
                            {
                                Console.WriteLine("Key: " + colList[col - 1] + " Value: " + workSheet.Cells[row, col].Value.ToString().Trim());

                                _importUnitOfWork.ExcelDatas.Add(
                                    new Entities.ExcelData
                                    {
                                        Key = colList[col - 1],
                                        Value = workSheet.Cells[row, col].Value.ToString().Trim()
                                    }
                                    );
                            }
                        }
                        _importUnitOfWork.Save();
                        
                    }
                }
                DeleteFile(file);
            }
        }

        private void DeleteFile(FileInfo file)
        {
            var fileName = file.Name;
            DeleteFileLocation(fileName);
            file.Delete();
            
        }

        private void DeleteFileLocation(string fileName)
        {
            var fileId = _importUnitOfWork.FileLocations.GetAll().Where(f => f.FileName==fileName).FirstOrDefault().Id;

            _importUnitOfWork.FileLocations.Remove(fileId);
        }

        private (int rowCount, int colCount) GetRowAndColNumber(ExcelWorksheet workSheet)
        {
            var rowCount = workSheet.Dimension.Rows;
            var colCount = workSheet.Dimension.Columns;

            return (rowCount, colCount);
        }

        public void SaveExcelInRoot(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void SaveFileInfo(FileLocation fileLocation)
        {
            _importUnitOfWork.FileLocations.Add(

                new Entities.FileLocation
                {
                    GroupId = fileLocation.GroupId,
                    FileName = fileLocation.FileName

                });

            _importUnitOfWork.Save();
        }
    }
}

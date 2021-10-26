using DataImporter.Common.Utilities;
using DataImporter.Import.BusinessObjects;
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
                //Import history te ei file er nam e je record ta ase setar status ta Processing kore dibo
                UpdateStatus(file, "Processing");
               
                var groupId = GetGroupId(file.Name.Split(".")[0]);

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
                                        Value = workSheet.Cells[row, col].Value.ToString().Trim(),
                                        GroupId=groupId
                                    }
                                    );
                            }
                        }
                     
                        _importUnitOfWork.Save();
                        
                    }
                }
                UpdateStatus(file, "Completed");
                DeleteFile(file);
            }
        }

        private void UpdateStatus(FileInfo file, string Status)
        {
            var importId = GetImportId(file.Name.Split(".")[0]);

            var importEntity = _importUnitOfWork.ImportHistories.GetById(importId);

            if(importEntity!=null)
            {
                importEntity.Status = Status;
                _importUnitOfWork.Save();
            }
        }

        private int GetImportId(string fileName)
        {
            var importId = _importUnitOfWork.ImportHistories.Get(x => x.FileName == fileName, string.Empty).Select(x => x.Id).FirstOrDefault();

            return importId;
        }

        private int GetGroupId(string name)
        {
            return _importUnitOfWork.FileLocations.Get(x => x.FileName == name, string.Empty).Select(x => x.GroupId).FirstOrDefault();
        }

        private void DeleteFile(FileInfo file)
        {
            var fileName = file.Name;
            DeleteFileLocation(fileName.Split(".")[0]);
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

        public void SaveFileInfo(FileLocation fileLocation, IFormFile file)
        {
            _importUnitOfWork.FileLocations.Add(

                new Entities.FileLocation
                {
                    GroupId = fileLocation.GroupId,
                    FileName = fileLocation.FileName

                });

            

            _importUnitOfWork.Save();

            if (_importUnitOfWork.GroupColumnNames.GetCount(x => x.GroupId == fileLocation.GroupId) == 0)
            {
                SaveFileColumnName(file, fileLocation.GroupId);
            }
                
            SaveFileInStorage(file, fileLocation.FileName);//ekhane file and file er name ta pathaite hbe.
            UpdateImportHistory(fileLocation);//Import history te ei file er nam e je record ta ase setar status ta Pending kore dibo
        }

        private void UpdateImportHistory(FileLocation fileLocation)
        {
            _importUnitOfWork.ImportHistories.Add(
                new Entities.ImportHistory
                {
                    GroupId=fileLocation.GroupId,
                    ImportDate=fileLocation.ImportDate,
                    FileName=fileLocation.FileName,
                    Status="Pending"
                }
                );

            _importUnitOfWork.Save();
        }

        private void SaveFileColumnName(IFormFile file, int groupId)
        {
            
            List<string> colList = new List<string>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                    var rowAndColInfo = GetRowAndColNumber(workSheet);

                    for (int col = 1; col <= rowAndColInfo.colCount; col++)
                    {
                        colList.Add(workSheet.Cells[1, col].Value.ToString().Trim());
                    }
                }
            }
            string allColumn = null;
            foreach(var val in colList)
            {
                allColumn += val+"~";//last e je ~ add hbe seita delete dite hbe
            }
            
            _importUnitOfWork.GroupColumnNames.Add(new Entities.GroupColumnName
                {
                    GroupId=groupId,
                    ColumnList=allColumn
                });

            _importUnitOfWork.Save();

        }


        public void SaveFileInStorage(IFormFile file, string fileName)
        {
            string path = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExcelFiles";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = fileName + ".xlsx";
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        
        public (bool rightGroup, List<string> data, int? colNum) CheckColumn(IFormFile file, int groupId)
        {
            var rightGroup = true;
            
            if (_importUnitOfWork.GroupColumnNames.GetCount(x => x.GroupId == groupId) == 0)//ekdm new group jedik kono file upload kora hoe nai.
            {
                var dataList = GetPreviewData(file);
                //ekhane file save korar function call dite hbe, jeta Import controller er upload action e dewa ase
                return (rightGroup, dataList.data, dataList.colNum);
            }
            //ei line e ashar por group ta exist kore, so sekhane file ta upload kora jabe kina setar checking dite hbe
            else
            {
                var columnList = _importUnitOfWork.GroupColumnNames.GetAll().Where(x => x.GroupId == groupId).Select(x=>x.ColumnList).FirstOrDefault();
                
                //var test = _importUnitOfWork.Groups.Get(x=>x.Id==groupId, "GroupColumnName").FirstOrDefault().GroupColumnName

                var splitColumnList = columnList.Split("~").ToList();
                
                var inputFileColList = fetchColList(file);

                

                if (splitColumnList.OrderBy(m => m).SequenceEqual(inputFileColList.OrderBy(m => m)))
                {
                    var dataList = GetPreviewData(file);
                    return (rightGroup, dataList.data, dataList.colNum);
                }

                rightGroup = false;
                return (rightGroup, null, null);
            }

            
        }

        private List<string> fetchColList(IFormFile file)
        {
            List<string> colList = new List<string>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                    var rowAndColInfo = GetRowAndColNumber(workSheet);

                    for (int col = 1; col <= rowAndColInfo.colCount; col++)
                    {
                        colList.Add(workSheet.Cells[1, col].Value.ToString().Trim());
                    }
                }
            }
            colList.Add("");
            return colList.ToList();
        }

        private (List<string> data, int colNum) GetPreviewData(IFormFile file)
        {
            List<string> dataList = new List<string>();
            var colNum = 0;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                    var rowAndColInfo = GetRowAndColNumber(workSheet);

                    int minRow = 10;
                    colNum = rowAndColInfo.colCount;

                    if (rowAndColInfo.rowCount < 10)
                        minRow = rowAndColInfo.rowCount;

                    for(int row=1;  row<=minRow;row++)
                    {
                        for (int col = 1; col <= rowAndColInfo.colCount; col++)
                        {
                            dataList.Add(workSheet.Cells[row, col].Value.ToString().Trim());
                        }
                    }
                    
                }
            }

            return (dataList, colNum);

        }

        public void Import()
        {
            var excelFilePath = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExcelFiles";

            DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
            FileInfo[] fileInfo = directoryInfo.GetFiles();

            if (fileInfo.Count() > 0)
            {
                SaveExcelInDb(fileInfo);// Ekhane groupId taw pass korte hbe.
            }
        }

        public (IList<ImportHistory> records, int total, int totalDisplay) GetImportHistories(int pageIndex, int pageSize, string searchText, DateTime startDate, DateTime endDate, string sortText, Guid userId)
        {
            var importData = _importUnitOfWork.ImportHistories.GetDynamic(
                 string.IsNullOrWhiteSpace(searchText) ? null : x => x.Group.Name.Contains(searchText) 
                , sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from import in importData.data.Where(x => (x.Group.ApplicationUserId == userId) && (x.ImportDate>=startDate && x.ImportDate<=endDate) )
                              select new ImportHistory
                              {
                                  Id = import.Id,
                                  ImportDate = import.ImportDate,
                                  GroupName = import.Group.Name,
                                  Status=import.Status

                              }).ToList();

            return (resultData, importData.total, importData.totalDisplay);
        }
    }
}

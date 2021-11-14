using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using DataImporter.Import.UnitOfWorks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //private readonly IExportService _exportService;
        //private readonly IEmailService _emailService;
        private readonly IEmailFileService _emailFileService;
        private readonly IImportUnitOfWork _importUnitOfWork;
        //private readonly IEmailService _emailService;
        public Worker(ILogger<Worker> logger, IImportUnitOfWork importUnitOfWork, IEmailService emailService, IEmailFileService emailFileService)
        {
            _logger = logger;
            //_exportService = exportService;
            //_emailService = emailService;
            _emailFileService = emailFileService;
            _importUnitOfWork = importUnitOfWork;
        }
        //ei worker service PendingExportFileHistory theke group id onujae data niye file create kore sei file ta sent korbe
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ////Ekhane PendingExportHistory theke group id niye sei group id theke user er id and oi user id theke user mail ta ber korte hbe. Subject and file er nam ta hbe group er nam onujae.

                ////GroupId theke userId extract kore sei user er id diye folder create kore sei folder e shb extraked file gulo rakhbo, pore je fil
                //int groupId = 10;//Ei grpId ta ashbe PendingExporthistory theke.
                //var file = _exportService.GetFile(groupId);
                //DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
                //FileInfo[] fileInfo = directoryInfo.GetFiles();

                //foreach (var file in fileInfo)
                //{
                //    _emailService.SendEmail("zahidsheikhuap96@gmail.com", "Testing", "This is a part of test", file);
                //}


                //var emailFileRecords = _importUnitOfWork.EmailFiles.GetAll().ToList();

                //foreach (var emailFile in emailFileRecords)
                //{
                //    var groupName = _importUnitOfWork.Groups.GetById(emailFile.GroupId).Name;
                //    var folderName = _importUnitOfWork.EmailFiles.Get(x => x.FolderName == emailFile.FolderName, string.Empty).Select(x => x.FolderName).FirstOrDefault();

                //    var userMail = _importUnitOfWork.Groups.Get(x => x.Id == emailFile.GroupId, string.Empty).Select(x => x.ApplicationUser).FirstOrDefault().Email;

                //    DirectoryInfo directoryInfo = new DirectoryInfo("D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles\\" + folderName);
                //    FileInfo[] fileInfo = directoryInfo.GetFiles();
                //    var file = fileInfo.ElementAt(0);

                //    _emailService.SendEmail(userMail, "Exported file from DataImporter", "Group Name " + file.FullName, file);

                //    DeleteCurrentRecord(emailFile);
                //}

                _emailFileService.SendMail();
                ///--_emailService.SendEmail("zahidsheikhuap96@gmail.com", "Testing", "This is a part of test", file);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        //private void DeleteCurrentRecord(Import.Entities.EmailFile emailFile)
        //{
        //    _importUnitOfWork.EmailFiles.Remove(emailFile);

        //    _importUnitOfWork.Save();
        //}
    }
}

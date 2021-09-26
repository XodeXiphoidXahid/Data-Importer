using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IExportService _exportService;
        private readonly IEmailService _emailService;
        public Worker(ILogger<Worker> logger, IExportService exportService, IEmailService emailService)
        {
            _logger = logger;
            _exportService = exportService;
            _emailService = emailService;
        }
        //ei worker service PendingExportFileHistory theke group id onujae data niye file create kore sei file ta sent korbe
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Ekhane PendingExportHistory theke group id niye sei group id theke user er id and oi user id theke user mail ta ber korte hbe. Subject and file er nam ta hbe group er nam onujae.

                //GroupId theke userId extract kore sei user er id diye folder create kore sei folder e shb extraked file gulo rakhbo, pore je fil
                int groupId = 10;//Ei grpId ta ashbe PendingExporthistory theke.
                var file = _exportService.GetFile(groupId);
                //DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
                //FileInfo[] fileInfo = directoryInfo.GetFiles();

                //foreach(var file in fileInfo)
                //{
                //    _emailService.SendEmail("zahidsheikhuap96@gmail.com", "Testing", "This is a part of test", file);
                //}
                _emailService.SendEmail("zahidsheikhuap96@gmail.com", "Testing", "This is a part of test", file);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

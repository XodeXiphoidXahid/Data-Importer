using DataImporter.Common.Utilities;
using DataImporter.Import.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IExportService _exportService;
        public Worker(ILogger<Worker> logger, IEmailService emailService, IConfiguration configuration, IExportService exportService)
        {
            _logger = logger;
            _emailService = emailService;
            _configuration = configuration;
            _exportService = exportService;
        }

        //ei worker service PendingExportFileHistory theke group id onujae data niye file create kore sei file ta sent korbe
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var excelFilePath = _configuration["wwwroot:UploadedExcel"];

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

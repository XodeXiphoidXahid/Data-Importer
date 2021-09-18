using DataImporter.Common.Utilities;
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
        public Worker(ILogger<Worker> logger, IEmailService emailService, IConfiguration configuration)
        {
            _logger = logger;
            _emailService = emailService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var excelFilePath = _configuration["wwwroot:UploadedExcel"];

            while (!stoppingToken.IsCancellationRequested)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
                FileInfo[] fileInfo = directoryInfo.GetFiles();

                foreach(var file in fileInfo)
                {
                    _emailService.SendEmail("zahidsheikhuap96@gmail.com", "Testing", "This is a part of test", file);
                }
                

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        
    }
}

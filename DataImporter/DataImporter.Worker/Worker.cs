using DataImporter.Import.Services;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;


        private readonly IImportService _importService;
        public Worker(ILogger<Worker> logger, IImportService importService, IConfiguration configuration)
        {
            _logger = logger;
            _importService = importService;
            _configuration = configuration;
        }
       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var excelFilePath = _configuration["wwwroot:UploadedExcel"];

                DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
                FileInfo[] fileInfo = directoryInfo.GetFiles();
                Console.WriteLine("Bro I am here");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

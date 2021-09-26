using DataImporter.Import.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.ImportWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImportService _importService;

        public Worker(ILogger<Worker> logger, IImportService importService)
        {
            _logger = logger;
            _importService = importService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var excelFilePath = "D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExcelFiles";

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("OK");

                DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
                FileInfo[] fileInfo = directoryInfo.GetFiles();

                if (fileInfo.Count() > 0)
                {
                    _importService.SaveExcelInDb(fileInfo);// Ekhane groupId taw pass korte hbe.
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}


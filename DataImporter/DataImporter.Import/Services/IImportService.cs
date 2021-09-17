﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Import.Services
{
    public interface IImportService
    {
        void SaveExcelInRoot(IFormFile file);
        void SaveExcelInDb(FileInfo[] fileInfo);
    }
}

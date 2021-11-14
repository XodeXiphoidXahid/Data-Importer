using DataImporter.Common.Utilities;
using DataImporter.Import.UnitOfWorks;
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
    public class EmailFileService: IEmailFileService
    {
        private readonly IImportUnitOfWork _importUnitOfWork;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailFileService(IImportUnitOfWork importUnitOfWork, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _importUnitOfWork = importUnitOfWork;
            _emailService = emailService;
            _userManager = userManager;
        }

        public void SendMail()
        {
            var emailFileRecords = _importUnitOfWork.EmailFiles.GetAll().ToList();

            foreach(var emailFile in emailFileRecords)
            {
                var groupName = _importUnitOfWork.Groups.GetById(emailFile.GroupId).Name;
                var folderName = _importUnitOfWork.EmailFiles.Get(x => x.FolderName == emailFile.FolderName, string.Empty).Select(x=>x.FolderName).FirstOrDefault();

                var userId = _importUnitOfWork.Groups.Get(x => x.Id == emailFile.GroupId, string.Empty).Select(x => x.ApplicationUserId).FirstOrDefault();
                var userMail = _userManager.FindByIdAsync(userId.ToString()).Result.Email;

                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\ASP.Net Core(Devskill)\\Asp_Dot_Net_Core\\ExportedFiles\\" + folderName);
                FileInfo[] fileInfo = directoryInfo.GetFiles();
                var file = fileInfo.ElementAt(0);

                _emailService.SendEmail(userMail, "Exported file from DataImporter", "Group Name: "+file.Name, file);

                DeleteCurrentRecord(emailFile);
            }
        }

        private void DeleteCurrentRecord(Entities.EmailFile emailFile)
        {
            _importUnitOfWork.EmailFiles.Remove(emailFile);

            _importUnitOfWork.Save();
        }
    }
}

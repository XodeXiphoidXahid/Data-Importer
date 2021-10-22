using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class ImportExportStatus: IImportExportStatus
    {
        public string Name { get; set; }
        public Status State { get; set; }

        public static string GetStatus(Status status)
        {
            switch (status)
            {
                case Status.Pending:
                    return "Pending";

                case Status.Processing:
                    return "Processing";

                case Status.Completed:
                    return "Completed";

                default:
                    return "Invalid status detected";
            }
        }

        public enum Status
        {
            Pending,
            Processing,
            Completed
        }
    }

    

    
}

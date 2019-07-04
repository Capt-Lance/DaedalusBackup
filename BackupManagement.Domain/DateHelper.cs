using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.Helpers
{
    public class DateHelper
    {
        public static string FileUTCNow()
        {
            return DateTime.UtcNow.ToString("yyyyMMddTHHmmss");
        }
    }
}

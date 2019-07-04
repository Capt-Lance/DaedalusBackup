using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// Returns a file safe string of the current UTC time in the format "yyyyMMddTHHmmss"
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FileUTCNow()
        {
            return DateTime.UtcNow.ToString("yyyyMMddTHHmmss");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class FullBackupJob: Job
    {
        public FullBackup Backup { get; private set; }

        private FullBackupJob(DateTime dateCreated, DateTime dateModified, FullBackup backup): base(dateCreated, dateModified)
        {
            Backup = backup;
        }

        public static FullBackupJob CreateNew()
        {
            FullBackupJob job = new FullBackupJob(DateTime.UtcNow, DateTime.UtcNow, null);
            return job;
        }

        public override Task Run()
        {
            throw new NotImplementedException();
        }
    }
}

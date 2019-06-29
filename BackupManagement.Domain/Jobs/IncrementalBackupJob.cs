using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Jobs
{
    public class IncrementalBackupJob : BackupJob<IncrementalBackup>
    {
        private IncrementalBackupJob(
            DateTime dateCreated,
            DateTime dateModified,
            BackupLocationType targetLocationType,
            string targetLocation
            ) : base(dateCreated, dateModified, targetLocationType, targetLocation)
        {
        }

        public IncrementalBackupJob CreateNew(List<VirtualMachine> vms, BackupLocationType backupType, string path)
        {
            IncrementalBackupJob job = new IncrementalBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path);
            return job;
        }
        public override Task Run()
        {
            throw new NotImplementedException();
        }
    }
}

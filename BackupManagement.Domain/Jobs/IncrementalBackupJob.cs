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
            string targetLocation,
            List<VirtualMachine> vms
            ) : base(dateCreated, dateModified, targetLocationType, targetLocation, vms)
        {
        }

        public IncrementalBackupJob CreateNew(List<VirtualMachine> vms, BackupLocationType backupType, string path)
        {
            IncrementalBackupJob job = new IncrementalBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path, vms);
            return job;
        }
        public override Task Run(IBackupLocationFactoryResolver backupLocationFactoryResolver)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Jobs
{
    public class IncrementalBackupJob : BackupJob<IncrementalBackup>
    {
        private IncrementalBackupJob(DateTime dateCreated, DateTime dateModified, FullBackup backup) : base(dateCreated, dateModified)
        {
        }
        public override Task Run()
        {
            throw new NotImplementedException();
        }
    }
}

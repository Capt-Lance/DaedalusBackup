using BackupManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTests.Jobs
{
    public class FullBackupJobTest
    {
        [Fact]
        public async Task FullBackupCreated()
        {
            List<VirtualMachine> vms = new List<VirtualMachine>();
            FullBackupJob backupJob = FullBackupJob.CreateNew(vms, BackupStreamType.CIFS, "testlocation");
        }
    }
}

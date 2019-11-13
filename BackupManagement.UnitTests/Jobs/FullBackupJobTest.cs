using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            FullBackupJob backupJob = FullBackupJob.CreateNew(vms, LocationType.CIFS, "testlocation");
        }

        // Need to revist this. Probably don't need to run a backup when the job is created
        //[Fact]
        //public async Task FullBackupAddedToJob()
        //{
        //    // setup data
        //    List<string> vhdPaths = new List<string> { "path1" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    List<VirtualMachine> vms = new List<VirtualMachine>();
        //    vms.Add(vm);

        //    // create job
        //    FullBackupJob backupJob = FullBackupJob.CreateNew(vms, LocationType.CIFS, "testlocation");
        //    backupJob.Run();

        //    // Test
        //    Assert.True(backupJob.Backups.Count > 0, "Backup was not added to backupJob");
        //}

    }
}

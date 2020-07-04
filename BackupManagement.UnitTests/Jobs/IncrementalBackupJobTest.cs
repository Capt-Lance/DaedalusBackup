using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTests.Jobs
{
    public class IncrementalBackupJobTest
    {
        //[Fact]
        //public async Task IncrementalBackupCreated()
        //{
        //    // Setup
        //    List<string> vhdPaths = new List<string> { "path1" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    List<VirtualMachine> vms = new List<VirtualMachine>();
        //    vms.Add(vm);
        //    LocationType targetLocationType = LocationType.CIFS;
        //    string targetLocation = "backups";
        //    IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();

        //    // Run
        //    IncrementalBackupJob job = IncrementalBackupJob.CreateNew(vms, targetLocationType, targetLocation);
        //    job.Run();

        //    // Test
        //    Assert.True(job.Backups.Count > 0, "Backup not created");
        //}
    }
}

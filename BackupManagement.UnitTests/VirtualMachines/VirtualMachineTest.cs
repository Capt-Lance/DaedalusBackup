using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTest.VirtualMachines
{
    [Collection("Unit Tests")]
    public class VirtualMachineTest
    {
        [Fact]
        public async Task BackupPathCorrect()
        {
            // Setup
            List<string> vhdPaths = new List<string> { "mypath/disk1.vhd" };
            Guid vmId = Guid.NewGuid();
            string testVmName = "test1";
            VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
            IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
            string backupLocation = "newBackupLocation";

            // Run
            FullBackup backup = await vm.CreateFullBackupAsync(resolver, BackupLocationType.CIFS, backupLocation);
            //FullBackup backup = await FullBackup.CreateNewAsync(vm, resolver, BackupLocationType.CIFS, backupLocation);

            // Test
            string expectedPath = $"{backupLocation}/{vm.Name}";
            Assert.True(backup.Path == expectedPath, "Backup.Path does not equal the expected path");
        }
    }
}

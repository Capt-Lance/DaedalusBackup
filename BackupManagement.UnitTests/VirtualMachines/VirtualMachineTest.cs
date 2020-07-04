using BackupManagement.Domain;
using BackupManagement.Domain.Backups;
using BackupManagement.Domain.FullBackups;
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
        public void BackupPathCorrect()
        {
            // Setup
            List<string> vhdPaths = new List<string> { "mypath/disk1.vhd" };
            Guid vmId = Guid.NewGuid();
            string testVmName = "test1";
            VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
            LocationType locationType = LocationType.CIFS;
            string backupLocation = "newBackupLocation";
            BackupConfiguration backupConfig = new BackupConfiguration { BackupLocation = backupLocation, TargetLocationType = locationType };
            // Run
            FullBackup backup = FullBackup.CreateNew(vm, backupConfig);
            //FullBackup backup = await FullBackup.CreateNewAsync(vm, resolver, BackupLocationType.CIFS, backupLocation);

            // Test
            string expectedPath = $"{backupLocation}/{vm.Name}";
            Assert.True(backup.Path == expectedPath, "Backup.Path does not equal the expected path");
        }

        [Fact]
        public void BackupLocationTypeCorrect()
        {
            // Setup
            List<string> vhdPaths = new List<string> { "mypath/disk1.vhd" };
            Guid vmId = Guid.NewGuid();
            string testVmName = "test1";
            VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
            IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
            string backupLocation = "newBackupLocation";
            LocationType targetLocationType = LocationType.CIFS;
            BackupConfiguration backupConfig = new BackupConfiguration() { BackupLocation = backupLocation, TargetLocationType = targetLocationType };
            // Run
            FullBackup backup = FullBackup.CreateNew(vm, backupConfig);


        }

    }
}

﻿using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTests.Backups
{
    public class FullBackupTest
    {
        //[Fact]
        //public async Task DataNotCorrupt()
        //{
        //    // Setup
        //    List<string> vhdPaths = new List<string> { "mypath/disk1.vhd" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
        //    string backupLocation = "newBackupLocation";

        //    // Run
        //    FullBackup backup = await FullBackup.BackupAsync(vm, resolver, LocationType.CIFS, backupLocation);

        //    // Test
        //    IBackupLocationFactory factory = resolver.Resolve(LocationType.CIFS);
        //    // Get the byte[] that was written (our backup)
        //    string virtualDiskBackupLocation = $"{backup.Path}/{vm.VirtualDisks[0].FileName}";
        //    Stream savedDataStream = factory.Open(virtualDiskBackupLocation);
        //    MemoryStream savedDataMs = new MemoryStream();
        //    savedDataStream.CopyTo(savedDataMs);
        //    byte[] savedData = savedDataMs.ToArray();

        //    // Get the byte[] we started with
        //    Stream originalDataStream = factory.Open(vm.VirtualDisks[0]);
        //    MemoryStream originalDataMs = new MemoryStream();
        //    originalDataStream.CopyTo(originalDataMs);
        //    byte[] originalData = originalDataMs.ToArray();
        //    Assert.True(originalData.SequenceEqual(savedData), "Data saved does not match original data");

        //}

        //[Fact]
        //public async Task FullBackupNotCorrupt()
        //{
        //    // setup data
        //    List<string> vhdPaths = new List<string> { "mypath/disk1.vhd" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    List<VirtualMachine> vms = new List<VirtualMachine>();
        //    vms.Add(vm);

        //    // create job
        //    FullBackupJob backupJob = FullBackupJob.CreateNew(vms, LocationType.CIFS, "testlocation");
        //    IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
        //    backupJob.Run();

        //    // test
        //    IBackupLocationFactory factory = resolver.Resolve(LocationType.CIFS);
        //    // Get the byte[] that was written (our backup)
        //    string virtualDiskBackupLocation = $"{backupJob.Backups[0].Path}/{vm.VirtualDisks[0].FileName}";
        //    Stream savedDataStream = factory.Open(virtualDiskBackupLocation);
        //    MemoryStream savedDataMs = new MemoryStream();
        //    savedDataStream.CopyTo(savedDataMs);
        //    byte[] savedData = savedDataMs.ToArray();

        //    // Get the byte[] we started with
        //    Stream originalDataStream = factory.Open(vhdPaths[0]);
        //    MemoryStream originalDataMs = new MemoryStream();
        //    originalDataStream.CopyTo(originalDataMs);
        //    byte[] originalData = originalDataMs.ToArray();
        //    Assert.True(originalData.SequenceEqual(savedData), "Data saved does not match original data");
        //}
    }
}

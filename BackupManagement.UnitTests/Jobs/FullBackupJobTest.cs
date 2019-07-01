﻿using BackupManagement.Domain;
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
            FullBackupJob backupJob = FullBackupJob.CreateNew(vms, BackupLocationType.CIFS, "testlocation");
        }

        [Fact]
        public async Task FullBackupNotCorrupt()
        {
            // setup data
            byte[] newData = new byte[8923];
            Random rndm = new Random();
            rndm.NextBytes(newData);
            //var readStream = new MemoryStream(newData);
            //var streamFactory = new MemoryBackupLocationFactory();
            List<string> vhdPaths = new List<string> { "path1" };
            Guid vmId = Guid.NewGuid();
            string testVmName = "test1";
            VirtualMachine vm = VirtualMachine.CreateNew(vmId, testVmName, vhdPaths);
            List<VirtualMachine> vms = new List<VirtualMachine>();
            vms.Add(vm);

            // create job
            FullBackupJob backupJob = FullBackupJob.CreateNew(vms, BackupLocationType.CIFS, "testlocation");
            IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
            await backupJob.Run(resolver);

            // test
            IBackupLocationFactory factory = resolver.Resolve(BackupLocationType.CIFS);
            Stream readStream = factory.Open(vm.VirtualDisks[0]);
            MemoryStream ms = new MemoryStream();
            readStream.CopyTo(ms);
            byte[] backedupData = ms.ToArray();
            Assert.True(newData.SequenceEqual(backedupData));
        }
    }
}

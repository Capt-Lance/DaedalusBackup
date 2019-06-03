using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
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
        public async Task BackupVirtualMachineWithoutCorruption()
        {
            //VirtualDiskMemoryRepository vdRepo = new VirtualDiskMemoryRepository();
            VirtualMachineMemoryRepository vmRepo = new VirtualMachineMemoryRepository();
            var vms = vmRepo.GetAll().ToList();
            var vm = vms[0];
            var vd = vm.VirtualDisks[0];
            //var sourceStream = vdRepo.GetVirtualDiskStream(vd);
            IBackupStreamFactory streamFactory = new MemoryBackupStreamFactory();
            Stream originialStream = streamFactory.Open(vd);
            byte[] targetData = new byte[originialStream.Length];
            MemoryStream targetStream = new MemoryStream(targetData);
            await vm.BackupVirtualDiskAsync(vd, streamFactory, targetStream);

            Stream targetComparisonStream = new MemoryStream(targetData);
            //sourceStream.Position = 0;
            //targetStream.Position = 0;
            bool areSame = originialStream.Length == targetComparisonStream.Length;
            while((originialStream.Position < originialStream.Length) && (targetComparisonStream.Position < targetComparisonStream.Length) && areSame)
            {
                byte[] sourceByte = new byte[1];
                byte[] targetByte = new byte[1];
                originialStream.Read(sourceByte);
                targetComparisonStream.Read(targetByte);
                areSame = sourceByte.SequenceEqual(targetByte) && areSame;

            }
            Assert.True(areSame, "Data is not the same");
            

        }
    }
}

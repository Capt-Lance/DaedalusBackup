using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.UnitTests.Services
{
    public class IncrementalBackupServiceTest
    {
        //[Fact]
        //public async Task DataNotCorrupted()
        //{
        //    // setup data
        //    List<string> vhdPaths = new List<string> { "path1" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    string targetLocation = "backup";
        //    IBackupLocationFactoryResolver resolver = new MemoryBackupLocationFactoryResolver();
        //    LocationType targetLocationType = LocationType.CIFS;

        //    // Run
        //    IncrementalBackup backup = await IncrementalBackup.BackupAsync(vm, targetLocationType, targetLocation);

        //    // Test
        //    IBackupLocationFactory sourceFactory = resolver.Resolve(LocationType.CIFS);
        //    MemoryStream ms = new MemoryStream();
        //    sourceFactory.Open(vm.VirtualDisks[0]).CopyTo(ms);
        //    List<byte> sourceData = ms.ToArray().ToList();
        //    IBackupLocationFactory targetFactory = resolver.Resolve(targetLocationType);
        //    string test1ChunkLocation = $"{targetLocation}/{testVmName}";

        //    int sourceDataPosition = 0;
        //    bool isSame = true;
        //    foreach (Chunk chunk in backup.IncrementCollection.OriginalIncrement.Chunks)
        //    {
        //        MemoryStream chunkMS = new MemoryStream();
        //        targetFactory.Open(chunk, test1ChunkLocation).CopyTo(chunkMS);
        //        byte[] chunkData = chunkMS.ToArray();
        //        IEnumerable<byte> sourceDataToCompare = sourceData.GetRange(sourceDataPosition, chunkData.Count());
        //        isSame = chunkData.SequenceEqual(sourceDataToCompare) && isSame;
        //        sourceDataPosition = chunkData.Count();

        //    }

        //    Assert.True(isSame, "Written data does not match original data");
        //}

        //[Fact]
        //public async Task IncrementCollectionHasOriginalIncrement()
        //{
        //    // setup data
        //    List<string> vhdPaths = new List<string> { "path1" };
        //    Guid vmId = Guid.NewGuid();
        //    string testVmName = "test1";
        //    VirtualMachine vm = VirtualMachine.FromExisting(vmId, testVmName, vhdPaths);
        //    string targetLocation = "";
        //    IncrementalBackup backup = await IncrementalBackup.BackupAsync(vm, LocationType.CIFS, targetLocation);
        //    Assert.True(backup.IncrementCollection.OriginalIncrement != null, "Original Increment missing");
        //}
    }
}

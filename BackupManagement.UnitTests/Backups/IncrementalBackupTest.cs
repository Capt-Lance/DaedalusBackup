using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTests.Backups
{
    public class IncrementalBackupTest
    {
        [Fact]
        public async Task OriginalIncrementCreated()
        {
            byte[] newData = new byte[789];
            Random rndm = new Random();
            rndm.NextBytes(newData);
            var readStream = new MemoryStream(newData);
            var streamFactory = new MemoryBackupLocationFactory();
            int incrementSize = 536870912; //512 MB
            string path = "";
            IncrementalBackup backup = await IncrementalBackup.CreateFromStreamAsync(readStream, streamFactory, path, incrementSize);
            Assert.True(backup.OriginalIncrement != null, "No Increments created");
        }

        [Fact]
        public async Task DataNotCorrupted()
        {
            int dataLength = 789;
            byte[] newData = new byte[dataLength];
            Random rndm = new Random();
            rndm.NextBytes(newData);
            var readStream = new MemoryStream(newData);
            var streamFactory = new MemoryBackupLocationFactory();
            int incrementSize = 536870912; //512 MB
            string path = "";
            IncrementalBackup backup = await IncrementalBackup.CreateFromStreamAsync(readStream, streamFactory, path, incrementSize);

            string incrementName = backup.OriginalIncrement.Chunks[0].Hash;
            Stream targetStream = streamFactory.Open(incrementName);
            MemoryStream targetMs = new MemoryStream();
            targetStream.CopyTo(targetMs);
            byte[] targetData = targetMs.ToArray();
            Assert.True(newData.SequenceEqual(targetData), "Written data does not match original data");
        }
    }
}

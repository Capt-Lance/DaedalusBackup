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
            byte[] newData = new byte[8923];
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
            int dataLength = 8923;
            byte[] newData = new byte[dataLength];
            Random rndm = new Random();
            rndm.NextBytes(newData);
            var readStream = new MemoryStream(newData);
            var streamFactory = new MemoryBackupLocationFactory();
            int incrementSize = 536870912; //512 MB
            string path = "";
            IncrementalBackup backup = await IncrementalBackup.CreateFromStreamAsync(readStream, streamFactory, path, incrementSize);
            bool isSame = backup.OriginalIncrement.Chunks.Count == 1;
            if (isSame)
            {
                var sourceStream = new MemoryStream(newData);
                int chunkIndex = 0;
                while (sourceStream.Position < sourceStream.Length)
                {
                    Chunk chunk = backup.OriginalIncrement.Chunks.First(x => x.Id == chunkIndex);
                    Stream targetChunkStream = streamFactory.Open(chunk, path);
                    int comparisonSize = dataLength < incrementSize ? dataLength : incrementSize;
                    byte[] sourceChunkData = new byte[comparisonSize];
                    byte[] targetChunkData = new byte[comparisonSize];
                    await sourceStream.ReadAsync(sourceChunkData);
                    await targetChunkStream.ReadAsync(targetChunkData);
                    isSame = sourceChunkData.SequenceEqual(targetChunkData) && isSame;
                    chunkIndex++;
                }
            }
            Assert.True(isSame, "Written data does not match original data");
        }
    }
}

using BackupManagement.Domain;
using BackupManagement.UnitTests.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.UnitTests.Backups
{
    public class IncrementalBackupTest
    {
        //[Fact]
        public async Task CorrectNumberOfChunks()
        {
            byte[] newData = new byte[8923];
            Random rndm = new Random();
            rndm.NextBytes(newData);
            var readStream = new MemoryStream(newData);
            var streamFactory = new MemoryBackupStreamFactory();
            int incrementSize = 536870912;
            IncrementalBackup backup = await IncrementalBackup.CreateFromStreamAsync(readStream, streamFactory, incrementSize);
        }
    }
}

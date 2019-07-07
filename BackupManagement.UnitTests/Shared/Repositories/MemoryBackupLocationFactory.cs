using BackupManagement.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    /// <summary>
    /// Mocked BackupLocationFactory that is in memory. This setup is hacky as all the byte[] inside data have to have the same length
    /// </summary>
    public class MemoryBackupLocationFactory : IBackupLocationFactory
    {
        private Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();
        private const int byteArrayLength = 789;

        public Stream Open(VirtualDisk vd)
        {
            if (!data.ContainsKey(vd.Location))
            {
                byte[] newData = new byte[byteArrayLength];
                Random rndm = new Random();
                rndm.NextBytes(newData);
                data[vd.Location] = newData;
                return new MemoryStream(data[vd.Location]);
            }
            return new MemoryStream(data[vd.Location]);

        }

        public Stream Open(Chunk chunk, string path)
        {
            if (!data.ContainsKey(chunk.Hash))
            {
                data[chunk.Hash] = new byte[byteArrayLength];
            }
            return new MemoryStream(data[chunk.Hash]);
        }

        public Stream Open(IncrementalBackup backup)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path)
        {
            if (!data.ContainsKey(path))
            {
                data[path] = new byte[byteArrayLength];
            }
            return new MemoryStream(data[path]);
        }

        public byte[] CreateRandomData(string path)
        {
            if (!data.ContainsKey(path))
            {
                byte[] newData = new byte[byteArrayLength];
                Random rndm = new Random();
                rndm.NextBytes(newData);
                data[path] = newData;
            }
            return data[path];
        }
    }
}

using BackupManagement.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    public class MemoryBackupLocationFactory : IBackupLocationFactory
    {
        private Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();

        public Stream Open(VirtualDisk vd)
        {
            if (!data.ContainsKey(vd.Location))
            {
                byte[] newData = new byte[789];
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
                data[chunk.Hash] = new byte[1073741824];
            }
            return new MemoryStream(data[chunk.Hash]);
        }

        public Stream Open(Backup backup)
        {
            throw new NotImplementedException();
        }
    }
}

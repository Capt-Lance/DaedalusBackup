using BackupManagement.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    public class MemoryBackupStreamFactory : IBackupStreamFactory
    {
        private Dictionary<string, byte[]> streams = new Dictionary<string, byte[]>();

        public Stream Open(VirtualDisk vd)
        {
            if (!streams.ContainsKey(vd.Location))
            {
                byte[] newData = new byte[789];
                Random rndm = new Random();
                rndm.NextBytes(newData);
                streams[vd.Location] = newData;
                return new MemoryStream(streams[vd.Location]);
            }
            return new MemoryStream(streams[vd.Location]);

        }
    }
}

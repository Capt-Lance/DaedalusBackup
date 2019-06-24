using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackupManagement.Domain
{
    public class CIFSBackupStreamFactory : IBackupStreamFactory
    {
        public Stream Open(VirtualDisk vd)
        {
            FileStream fs = new FileStream(vd.Location, FileMode.OpenOrCreate);
            return fs;
        }

        public Stream Open(Backup backup)
        {
            throw new NotImplementedException();
        }

        public Stream Open(Chunk chunk, string path)
        {
            string location = $"{path}/{chunk.Hash}";
            FileStream fs = new FileStream(location, FileMode.OpenOrCreate);
            return fs;
        }
    }
}

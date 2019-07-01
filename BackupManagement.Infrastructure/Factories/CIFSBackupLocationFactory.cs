using BackupManagement.Domain;
using System;
using System.IO;

namespace BackupManagement.Infrastructure.Factories
{
    public class CIFSBackupLocationFactory : IBackupLocationFactory
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

        public Stream Open(string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            return fs;
        }
    }
}
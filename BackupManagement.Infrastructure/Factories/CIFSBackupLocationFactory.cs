using BackupManagement.Domain;
using BackupManagement.Domain.Backups.IncrementalBackups;
using BackupManagement.Domain.VirtualMachines;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.Infrastructure.Factories
{
    public class CIFSBackupLocationFactory : IBackupLocationFactory
    {
        public IncrementCollection GetIncrementCollection(string targetLocation)
        {
            throw new NotImplementedException();
        }

        public Task<IncrementCollection> GetIncrementCollectionAsync(string targetLocation)
        {
            throw new NotImplementedException();
        }

        public Stream Open(VirtualDisk vd)
        {
            FileStream fs = new FileStream(vd.Location, FileMode.OpenOrCreate);
            return fs;
        }

        public Stream Open(IncrementalBackup backup)
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

        public Task SaveIncrementCollectionAsync(IncrementCollection incrementCollection, string targetLocation)
        {
            throw new NotImplementedException();
        }
    }
}
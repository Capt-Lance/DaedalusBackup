using BackupManagement.Domain;
using BackupManagement.Domain.Backups.IncrementalBackups;
using BackupManagement.Domain.VirtualMachines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    /// <summary>
    /// Mocked BackupLocationFactory that is in memory. This setup is hacky as all the 
    /// byte[]'s inside data have to have the same length since we can't create them at runtime
    /// TODO: MAKE THIS NOT HACKY
    /// </summary>
    public class MemoryBackupLocationFactory : IBackupLocationFactory
    {
        private Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();
        private Dictionary<string, IncrementCollection> incrementCollections = new Dictionary<string, IncrementCollection>();
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

        public Task<IncrementCollection> GetIncrementCollectionAsync(string targetLocation)
        {
            IncrementCollection collection = incrementCollections.ContainsKey(targetLocation) ? incrementCollections[targetLocation] : null;
            return Task.FromResult(collection);
        }

        public Task SaveIncrementCollectionAsync(IncrementCollection incrementCollection, string targetLocation)
        {
            incrementCollections[targetLocation] = incrementCollection;
            return Task.CompletedTask;
        }


    }
}

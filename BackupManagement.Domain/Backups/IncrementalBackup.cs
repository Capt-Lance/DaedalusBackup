using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class IncrementalBackup: Backup
    {
        public DateTime DateModified {get; private set; }

        public Increment OriginalIncrement { get; private set; }
        public List<Increment> Increments { get; private set; }

        public int IncrementSize;

        private IncrementalBackup(DateTime dateCreated, DateTime dateModified, string path, int incrementSize) : base(dateCreated, path)
        {
            IncrementSize = incrementSize;
            DateModified = dateModified;
            Increments = new List<Increment>();
        }

        private HashSet<string> GetChunkHashes()
        {
            HashSet<string> chunkHashesHashSet = new HashSet<string>();
            if (OriginalIncrement != null)
            {
                chunkHashesHashSet.UnionWith(OriginalIncrement.GetChunkHashes());
            }
            foreach(Increment increment in Increments)
            {
                chunkHashesHashSet.UnionWith(increment.GetChunkHashes());
            }
            return chunkHashesHashSet;
        }

        public static async Task<IncrementalBackup> CreateFromStreamAsync(Stream readStream, IBackupStreamFactory streamFactory, string path, int incrementSize)
        {
            IncrementalBackup backup = new IncrementalBackup(DateTime.Now, DateTime.Now, path, incrementSize);
            backup.OriginalIncrement = await Increment.CreateNewAsync(readStream, streamFactory, path, incrementSize, new HashSet<string>());
            return backup;
        }

        public async Task Backup(Stream readStream, IBackupStreamFactory streamFactory)
        {
            Increment increment = await Increment.CreateNewAsync(readStream, streamFactory, Path, IncrementSize, GetChunkHashes());
            Increments.Add(increment);
        }
    }
}

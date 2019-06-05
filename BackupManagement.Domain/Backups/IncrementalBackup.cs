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
        public IEnumerable<Increment> Increments { get; private set; }

        private IncrementalBackup(DateTime dateModified)
        {
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

        public static async Task<IncrementalBackup> CreateFromStreamAsync(Stream readStream, IBackupStreamFactory streamFactory, int incrementSize)
        {
            IncrementalBackup backup = new IncrementalBackup(DateTime.Now);
            //byte[] buffer = new byte[536870912];
            int chunk = 0;
            //HashSet<string> existingChunkHashes = GetChunkHashes()
            backup.OriginalIncrement = await Increment.CreateNewAsync(readStream, streamFactory, incrementSize, new HashSet<string>());
 
            return backup;
        }
    }
}

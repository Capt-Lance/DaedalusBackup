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

        public IEnumerable<Increment> OriginalIncrements { get; private set; }
        public IEnumerable<Increment> Increments { get; private set; }

        private IncrementalBackup(DateTime dateModified)
        {
            DateModified = dateModified;
            OriginalIncrements = new List<Increment>();
            Increments = new List<Increment>();
        }

        public static async Task<IncrementalBackup> CreateFromStreamAsync(Stream readStream, IBackupStreamFactory streamFactory, int incrementSize)
        {
            IncrementalBackup backup = new IncrementalBackup(DateTime.Now);
            //byte[] buffer = new byte[536870912];
            int chunk = 0;
            Increment increment = await Increment.CreateNewAsync(readStream, streamFactory, incrementSize);
 
            return backup;
        }
    }
}

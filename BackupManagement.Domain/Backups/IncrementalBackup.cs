using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class IncrementalBackup : Backup
    {
        public DateTime DateModified {get; private set; }

        public IncrementCollection IncrementCollection { get; private set; }

        public int IncrementSize;
        public const string incrementFileName = "increments.json";

        private static int _incrementSize = 512 * 1024 * 1024; //512MB -> KB -> B

        private IncrementalBackup(DateTime dateCreated, DateTime dateModified, string path, int incrementSize) : base(dateCreated, path)
        {
            IncrementSize = incrementSize;
            DateModified = dateModified;
        }



        //public static async Task<IncrementalBackup> CreateFromStreamAsync(
        //    Stream readStream, 
        //    IBackupLocationFactory streamFactory, string path, int incrementSize)
        //{
        //    IncrementalBackup backup = new IncrementalBackup(DateTime.Now, DateTime.Now, path, incrementSize);
        //    backup.OriginalIncrement = await Increment.CreateNewAsync(readStream, streamFactory, path, incrementSize, new HashSet<string>());
        //    return backup;
        //}

        public static async Task<IncrementalBackup> BackupAsync(
            VirtualMachine vm,
            IBackupLocationFactoryResolver locationFactoryResolver, 
            LocationType targetLocationType,
            string targetLocation)
        {
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(vm.SourceLocationType);
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            IncrementalBackup backup = new IncrementalBackup(DateTime.UtcNow, DateTime.UtcNow, targetLocation, _incrementSize);
            IncrementCollection incrementCollection = await targetFactory.GetIncrementCollectionAsync(targetLocation);

            backup.IncrementCollection = incrementCollection ?? IncrementCollection.CreateNew();
            HashSet<string> existingHashSet = incrementCollection?.GetChunkHashes() ?? new HashSet<string>();
            foreach(VirtualDisk vd in vm.VirtualDisks)
            {
                VirtualDiskIncrement increment = await VirtualDiskIncrement.CreateNewAsync(vd, sourceFactory, targetFactory, targetLocation, _incrementSize, existingHashSet);
                backup.IncrementCollection.AddIncrement(increment);
            }
            await targetFactory.SaveIncrementCollectionAsync(backup.IncrementCollection, targetLocation);
            return backup;

        }
    }
}

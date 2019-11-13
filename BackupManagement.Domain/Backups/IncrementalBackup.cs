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

        private static int incrementSize = 512 * 1024 * 1024; //512MB -> KB -> B

        private IncrementalBackup(LocationType locationType, string path, int incrementSize, DateTime dateCreated, DateTime dateModified) : base(locationType, path, dateCreated)
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

        public static IncrementalBackup CreateNew(LocationType targetLocationType, string targetLocation, int incrementSize)
        {
            return new IncrementalBackup(targetLocationType, targetLocation, incrementSize, DateTime.UtcNow, DateTime.UtcNow);
        }

        public static async Task<IncrementalBackup> BackupAsync(
            VirtualMachine vm,
            //IBackupLocationFactoryResolver locationFactoryResolver, 
            LocationType targetLocationType,
            string targetLocation)
        {
            //IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(vm.SourceLocationType);
            //IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            IncrementalBackup backup = new IncrementalBackup(targetLocationType, targetLocation, incrementSize, DateTime.UtcNow, DateTime.UtcNow);
            //IncrementCollection incrementCollection = await targetFactory.GetIncrementCollectionAsync(targetLocation);

            //backup.IncrementCollection = incrementCollection ?? IncrementCollection.CreateNew();
            //HashSet<string> existingHashSet = incrementCollection?.GetChunkHashes() ?? new HashSet<string>();
            //foreach(VirtualDisk vd in vm.VirtualDisks)
            //{
            //    VirtualDiskIncrement increment = await VirtualDiskIncrement.CreateNewAsync(vd, sourceFactory, targetFactory, targetLocation, _incrementSize, existingHashSet);
            //    backup.IncrementCollection.AddIncrement(increment);
            //}
            //await targetFactory.SaveIncrementCollectionAsync(backup.IncrementCollection, targetLocation);
            return backup;

        }
    }
}

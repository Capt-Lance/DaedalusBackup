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

        public static async Task BackupAsync(
            IBackupLocationFactoryResolver locationFactoryResolver, 
            LocationType sourceLocationType, 
            LocationType targetLocationType,
            string targetLocation)
        {
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(sourceLocationType);
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            IncrementCollection incrementCollection = targetFactory.GetIncrementCollection(this, targetLocation);
            Increment increment = await Increment.CreateNewAsync(sourceFactory, targetFactory, targetLocation, IncrementSize, incrementCollection);
        }
    }
}

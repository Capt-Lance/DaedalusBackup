using BackupManagement.Domain.Backups;
using BackupManagement.Domain.Backups.IncrementalBackups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public class IncrementalBackupService : IBackupService<IncrementalBackup>
    {
        public static async Task<IncrementalBackup> BackupAsync(
        VirtualMachine vm,
        //IBackupLocationFactoryResolver locationFactoryResolver, 
        LocationType targetLocationType,
        string targetLocation)
        {
            throw new NotImplementedException();
            //IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(vm.SourceLocationType);
            //IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            //IncrementalBackup backup = new IncrementalBackup(targetLocationType, targetLocation, incrementSize, DateTime.UtcNow, DateTime.UtcNow);
            //IncrementCollection incrementCollection = await targetFactory.GetIncrementCollectionAsync(targetLocation);

            //backup.IncrementCollection = incrementCollection ?? IncrementCollection.CreateNew();
            //HashSet<string> existingHashSet = incrementCollection?.GetChunkHashes() ?? new HashSet<string>();
            //foreach(VirtualDisk vd in vm.VirtualDisks)
            //{
            //    VirtualDiskIncrement increment = await VirtualDiskIncrement.CreateNewAsync(vd, sourceFactory, targetFactory, targetLocation, _incrementSize, existingHashSet);
            //    backup.IncrementCollection.AddIncrement(increment);
            //}
            //await targetFactory.SaveIncrementCollectionAsync(backup.IncrementCollection, targetLocation);
            //return backup;

        }

        public Task<IncrementalBackup> BackupAsync(BackupConfiguration backupConfiguration, IBackupLocationFactoryResolver locationFactoryResolver)
        {
            throw new NotImplementedException();
        }
    }
}

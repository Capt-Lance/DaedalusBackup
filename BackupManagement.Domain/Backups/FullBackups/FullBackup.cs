using BackupManagement.Domain.Backups.FullBackups;
using BackupManagement.Domain.VirtualMachines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.FullBackups
{
    public class FullBackup : Backup
    {
        // might need to make this a concurrent dictionary
        private Dictionary<string, string> _virtualDiskBackupLocations;
        public IReadOnlyDictionary<string ,string> VirtualDiskBackupLocations { get
            {
                return new ReadOnlyDictionary<string, string>(_virtualDiskBackupLocations);
            } 
        }
        private FullBackup(VirtualMachine vm, LocationType locationType, string path, DateTime dateCreated): base(vm, locationType, path, dateCreated)
        {
            _virtualDiskBackupLocations = new Dictionary<string, string>();
        }

        public static FullBackup CreateNew(VirtualMachine vm, LocationType locationType, string path)
        {
            FullBackup fullBackup = new FullBackup(vm, locationType, path, DateTime.UtcNow);
            return fullBackup;
        }

        public async Task Run(IBackupLocationFactoryResolver locationFactoryResolver)
        {
            foreach(VirtualDisk vd in VirtualMachine.VirtualDisks)
            {
                await BackupVirtualDisk(vd, locationFactoryResolver);
            }
        }

        private async Task BackupVirtualDisk(VirtualDisk vd, IBackupLocationFactoryResolver locationFactoryResolver)
        {
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(LocationType);
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(VirtualMachine.SourceLocationType);
            string backupLocation = $"{Path}/{vd.FileName}";
            Stream targetStream = targetFactory.Open(backupLocation);
            Stream sourceStream = sourceFactory.Open(vd);
            byte[] buffer = new byte[512];
            while (sourceStream.Position < sourceStream.Length)
            {
                long remainingBytes = sourceStream.Length - sourceStream.Position;
                if (remainingBytes < buffer.Length)
                {
                    buffer = new byte[remainingBytes];
                }
                await sourceStream.ReadAsync(buffer);
                await targetStream.WriteAsync(buffer);
            }
            targetStream.Close();
            sourceStream.Close();
            _virtualDiskBackupLocations.Add(vd.FileName, backupLocation);
        }

        //public static async Task<FullBackup> BackupAsync(VirtualMachine vm, IBackupLocationFactoryResolver locationFactoryResolver, LocationType targetLocationType, string backupLocation)
        //{
        //    FullBackup backup = new FullBackup(DateTime.UtcNow, backupLocation);
        //    Task[] backupTasks = new Task[vm.VirtualDisks.Count];
        //    for(int i = 0; i < vm.VirtualDisks.Count; i++)
        //    {
        //        VirtualDisk vd = vm.VirtualDisks[i];
        //        backupTasks[i] = backup.BackupVirtualDisk(vd, locationFactoryResolver, vm.SourceLocationType, targetLocationType, backupLocation);
        //    }
        //    await Task.WhenAll(backupTasks);
        //    return backup;
        //}

        public void AddVirtualDiskBackupLocation(string fileName, string location)
        {
            if (VirtualDiskBackupLocations.ContainsKey(fileName)) { throw new InvalidOperationException($"File {fileName} has already been added"); }
            _virtualDiskBackupLocations.Add(fileName, location);
        }
    }
}

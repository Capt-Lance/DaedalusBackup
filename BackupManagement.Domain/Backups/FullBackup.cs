﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
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
        private FullBackup(LocationType locationType, string path, DateTime dateCreated): base(locationType, path, dateCreated)
        {
            _virtualDiskBackupLocations = new Dictionary<string, string>();
        }

        public static FullBackup CreateNew(LocationType locationType, string path)
        {
            return new FullBackup(locationType, path, DateTime.UtcNow);
        }


        //private  async Task BackupVirtualDisk(VirtualDisk vd, IBackupLocationFactoryResolver locationFactoryResolver, LocationType sourceLocationType, LocationType targetLocationType, string basePath)
        //{
        //    IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
        //    IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(sourceLocationType);
        //    string backupLocation = $"{basePath}/{vd.FileName}";
        //    Stream targetStream = targetFactory.Open(backupLocation);
        //    Stream sourceStream = sourceFactory.Open(vd);
        //    byte[] buffer = new byte[512];
        //    while (sourceStream.Position < sourceStream.Length)
        //    {
        //        long remainingBytes = sourceStream.Length - sourceStream.Position;
        //        if (remainingBytes < buffer.Length)
        //        {
        //            buffer = new byte[remainingBytes];
        //        }
        //        await sourceStream.ReadAsync(buffer);
        //        await targetStream.WriteAsync(buffer);
        //    }
        //    targetStream.Close();
        //    sourceStream.Close();
        //    VirtualDiskBackupLocations.Add(vd.FileName, backupLocation);
        //}
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

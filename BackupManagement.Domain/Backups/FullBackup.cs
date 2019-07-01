﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class FullBackup : Backup
    {
        public Dictionary<string, string> VirtualDiskBackupLocations { get; private set; }
        public FullBackup(DateTime dateCreated, string path): base( dateCreated, path) { }

        private  async Task BackupVirtualDisk(VirtualDisk vd, IBackupLocationFactoryResolver locationFactoryResolver, BackupLocationType targetLocationType, string basePath)
        {
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(BackupLocationType.CIFS);// assume disk is on same box for now
            string backupLocation = $"{basePath}/{vd.FileName}";
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
            VirtualDiskBackupLocations.Add(vd.FileName, backupLocation);
        }
        public static async Task<FullBackup> CreateNewAsync(VirtualMachine vm, IBackupLocationFactoryResolver locationFactoryResolver, BackupLocationType targetLocationType, string targetPath)
        {
            FullBackup backup = new FullBackup(DateTime.UtcNow, targetPath);
            Task[] backupTasks = new Task[vm.VirtualDisks.Count];
            for(int i = 0; i < vm.VirtualDisks.Count; i++)
            {
                VirtualDisk vd = vm.VirtualDisks[i];
                backupTasks[i] = backup.BackupVirtualDisk(vd, locationFactoryResolver, targetLocationType, targetPath);
            }
            await Task.WhenAll(backupTasks);
            return backup;
        }
    }
}

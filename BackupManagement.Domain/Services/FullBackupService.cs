using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public class FullBackupService
    {
        private async Task BackupVirtualDisk(FullBackup backup, VirtualDisk vd, IBackupLocationFactoryResolver locationFactoryResolver, LocationType sourceLocationType, LocationType targetLocationType, string basePath)
        {
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(targetLocationType);
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(sourceLocationType);
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
            backup.AddVirtualDiskBackupLocation(vd.FileName, backupLocation);
        }

        public async Task<FullBackup> BackupAsync(VirtualMachine vm, IBackupLocationFactoryResolver locationFactoryResolver, LocationType targetLocationType, string backupLocation)
        {
            FullBackup backup = FullBackup.CreateNew(backupLocation);
            Task[] backupTasks = new Task[vm.VirtualDisks.Count];
            for (int i = 0; i < vm.VirtualDisks.Count; i++)
            {
                VirtualDisk vd = vm.VirtualDisks[i];
                backupTasks[i] = BackupVirtualDisk(backup, vd, locationFactoryResolver, vm.SourceLocationType, targetLocationType, backupLocation);
            }
            await Task.WhenAll(backupTasks);
            return backup;
        }
    }
}

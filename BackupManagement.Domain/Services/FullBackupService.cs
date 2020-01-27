using BackupManagement.Domain.FullBackups;
using BackupManagement.Domain.VirtualMachines;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public class FullBackupService : IBackupService<FullBackup>
    {
        public async Task<FullBackup> BackupAsync(FullBackup backup, IBackupLocationFactoryResolver locationFactoryResolver)
        {
            var vm = backup.VirtualMachine;
            //FullBackup backup = FullBackup.CreateNew(targetLocationType, backupLocation);
            Task[] backupTasks = new Task[vm.VirtualDisks.Count];
            for (int i = 0; i < vm.VirtualDisks.Count; i++)
            {
                VirtualDisk vd = vm.VirtualDisks[i];
                backupTasks[i] = BackupVirtualDisk(backup, vd, locationFactoryResolver, vm.SourceLocationType);
            }
            await Task.WhenAll(backupTasks);
            return backup;
        }

        private async Task BackupVirtualDisk(FullBackup backup, VirtualDisk vd, IBackupLocationFactoryResolver locationFactoryResolver, LocationType sourceLocationType)
        {
            IBackupLocationFactory targetFactory = locationFactoryResolver.Resolve(backup.LocationType);
            IBackupLocationFactory sourceFactory = locationFactoryResolver.Resolve(sourceLocationType);
            string backupLocation = $"{backup.Path}/{vd.FileName}";
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
    }
}
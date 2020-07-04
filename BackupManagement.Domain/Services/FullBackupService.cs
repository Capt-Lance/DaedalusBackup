using BackupManagement.Domain.Backups;
using BackupManagement.Domain.FullBackups;
using BackupManagement.Domain.VirtualMachines;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public class FullBackupService : IBackupService<FullBackup>
    {
        private IBackupLocationFactoryResolver locationFactoryResolver;
        public FullBackupService(IBackupLocationFactoryResolver locationFactoryResolver)
        {
            this.locationFactoryResolver = locationFactoryResolver;
        }
        public async Task<FullBackup> BackupAsync(VirtualMachine vm, BackupConfiguration backupConfiguration)
        {
            FullBackup backup = FullBackup.CreateNew(vm, backupConfiguration);
            Task[] backupTasks = new Task[vm.VirtualDisks.Count];
            for (int i = 0; i < vm.VirtualDisks.Count; i++)
            {
                VirtualDisk vd = vm.VirtualDisks[i];
                backupTasks[i] = BackupVirtualDisk(backup, vd, vm.SourceLocationType);
            }
            await Task.WhenAll(backupTasks);
            return backup;
        }

        private async Task BackupVirtualDisk(FullBackup backup, VirtualDisk vd, LocationType sourceLocationType)
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
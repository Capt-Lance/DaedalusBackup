using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class IncrementalBackupJob : BackupJob<IncrementalBackup>
    {
        private IncrementalBackupJob(
            DateTime dateCreated,
            DateTime dateModified,
            LocationType targetLocationType,
            string targetLocation,
            List<VirtualMachine> vms
            ) : base(dateCreated, dateModified, targetLocationType, targetLocation, vms)
        {
        }

        public static IncrementalBackupJob CreateNew(List<VirtualMachine> vms, LocationType backupType, string path)
        {
            IncrementalBackupJob job = new IncrementalBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path, vms);
            return job;
        }
        public override async Task RunAsync(IBackupLocationFactoryResolver backupLocationFactoryResolver)
        {
            foreach(VirtualMachine vm in VirtualMachines)
            {
                IncrementalBackup backup = await vm.CreateIncrementalBackupAsync(backupLocationFactoryResolver, TargetLocationType, TargetLocation);
                Backups.Add(backup);
            }
        }
    }
}

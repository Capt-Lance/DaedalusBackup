using BackupManagement.Domain.Common.Helpers;
using BackupManagement.Domain.FullBackups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class FullBackupJob:  BackupJob<FullBackup>
    {


        private FullBackupJob(
            DateTime dateCreated, 
            DateTime dateModified, 
            LocationType targetLocationType, 
            string targetLocation,
            List<VirtualMachine> vms
            ): base(dateCreated, dateModified, targetLocationType, targetLocation, vms)
        {

        }

        public static FullBackupJob CreateNew(List<VirtualMachine> vms, LocationType backupType, string path)
        {
            FullBackupJob job = new FullBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path, vms);
            return job;
        }

        public override void Run()
        {
            List<FullBackup> backups = new List<FullBackup>(VirtualMachines.Count);
            int i = 0;
            string subdirectory = $"{TargetLocation}/{DateHelper.FileUTCNow()}";
            foreach(VirtualMachine vm in VirtualMachines)
            {
                FullBackup backup = vm.CreateFullBackup(TargetLocationType, subdirectory);
                backups[0] = backup;
                i++;
            }
            Backups.AddRange(backups);
        }
    }
}

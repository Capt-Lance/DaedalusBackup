using BackupManagement.Domain.Helpers;
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

        public override async Task Run(IBackupLocationFactoryResolver backupLocationFactoryResolver)
        {
            Task<FullBackup>[] tasks = new Task<FullBackup>[VirtualMachines.Count];
            int i = 0;
            string subdirectory = $"{TargetLocation}/{DateHelper.FileUTCNow()}";
            foreach(VirtualMachine vm in VirtualMachines)
            {
                Task<FullBackup> task = vm.CreateFullBackupAsync(backupLocationFactoryResolver, TargetLocationType, subdirectory);
                tasks[0] = task;
                i++;
            }
            await Task.WhenAll(tasks);
            foreach (Task<FullBackup> task in tasks)
            {
                Backups.Add(task.Result);
            }
        }
    }
}

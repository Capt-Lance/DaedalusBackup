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
            BackupLocationType targetLocationType, 
            string targetLocation,
            List<VirtualMachine> vms
            ): base(dateCreated, dateModified, targetLocationType, targetLocation, vms)
        {

        }

        private async Task BackupVirtualMachine(VirtualMachine vm)
        {
            throw new NotImplementedException();
        }

        public static FullBackupJob CreateNew(List<VirtualMachine> vms, BackupLocationType backupType, string path)
        {
            FullBackupJob job = new FullBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path, vms);
            return job;
        }

        public override async Task Run(IBackupLocationFactoryResolver backupLocationFactoryResolver)
        {
            Task[] tasks = new Task[VirtualMachines.Count];
            int i = 0;
            string subdirectory = $"{TargetLocation}/{DateHelper.FileUTCNow()}";
            foreach(VirtualMachine vm in VirtualMachines)
            {
                Task task = BackupVirtualMachine(vm);//vm.FullBackup(backupLocationFactoryResolver, TargetLocationType);
                tasks[0] = task;
                i++;
            }
            await Task.WhenAll(tasks);
        }
    }
}

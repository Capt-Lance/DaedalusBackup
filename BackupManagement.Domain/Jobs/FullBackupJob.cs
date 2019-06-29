﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class FullBackupJob:  BackupJob<FullBackup>
    {

        public List<VirtualMachine> VirtualMachines { get; private set; }

        private FullBackupJob(
            DateTime dateCreated, 
            DateTime dateModified, 
            BackupLocationType targetLocationType, 
            string targetLocation
            ): base(dateCreated, dateModified, targetLocationType, targetLocation)
        {

        }

        public static FullBackupJob CreateNew(List<VirtualMachine> vms, BackupLocationType backupType, string path)
        {
            FullBackupJob job = new FullBackupJob(DateTime.UtcNow, DateTime.UtcNow, backupType, path);
            return job;
        }

        public override async Task Run()
        {
            Task[] tasks = new Task[VirtualMachines.Count];
            int i = 0;
            foreach(VirtualMachine vm in VirtualMachines)
            {
                Task task = vm.FullBackup();
                tasks[0] = task;
                i++;
            }
            await Task.WhenAll(tasks);
        }
    }
}

using BackupManagement.Domain.Backups.IncrementalBackups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.DomainEvents
{
    public class VirtualMachineIncrementalBackupCreated : INotification
    {
        public VirtualMachine VirtualMachine { get; }
        public IncrementalBackup Backup { get; }
        public VirtualMachineIncrementalBackupCreated(VirtualMachine virtualMachine, IncrementalBackup backup)
        {
            VirtualMachine = virtualMachine;
            Backup = backup;
        }
    }
}

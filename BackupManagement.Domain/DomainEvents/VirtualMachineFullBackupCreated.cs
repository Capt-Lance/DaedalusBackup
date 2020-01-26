using BackupManagement.Domain.FullBackups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.DomainEvents
{
    public class VirtualMachineFullBackupCreated : INotification
    {
        public VirtualMachine VirtualMachine { get; }
        public FullBackup Backup { get; }
        public VirtualMachineFullBackupCreated(VirtualMachine virtualMachine, FullBackup backup)
        {
            VirtualMachine = virtualMachine;
            Backup = backup;
        }
    }
}

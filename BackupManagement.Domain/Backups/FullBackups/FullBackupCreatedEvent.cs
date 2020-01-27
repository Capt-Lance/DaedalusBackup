using BackupManagement.Domain.FullBackups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.Backups.FullBackups
{
    public class FullBackupCreatedEvent : INotification
    {
        public FullBackup FullBackup { get; private set; }
        
        public FullBackupCreatedEvent(FullBackup backup)
        {
            FullBackup = backup;
        }
    }
}

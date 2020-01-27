using BackupManagement.Domain.Backups.FullBackups;
using BackupManagement.Domain.FullBackups;
using BackupManagement.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupManagement.Application.FullBackupJob.Handlers
{
    public class CreateFullBackup : INotificationHandler<FullBackupCreatedEvent>
    {
        private readonly IBackupService<FullBackup> backupService;

        public CreateFullBackup(IBackupService<FullBackup> backupService)
        {
            this.backupService = backupService;
        }
        public async Task Handle(FullBackupCreatedEvent fullBackupCreatedEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //ait backupService.BackupAsync(fullBackupCreatedEvent.FullBackup, locationFactoryResolver);
            throw new NotImplementedException();
        }


    }
}

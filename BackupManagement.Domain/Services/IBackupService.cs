using BackupManagement.Domain.FullBackups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public interface IBackupService<T> where T : Backup
    {
        public Task<T> BackupAsync(T backup, IBackupLocationFactoryResolver locationFactoryResolver);
    }
}

using BackupManagement.Domain.Backups;
using System.Threading.Tasks;

namespace BackupManagement.Domain.Services
{
    public interface IBackupService<T> where T : Backup
    {
        public Task<T> BackupAsync(BackupConfiguration backupConfiguration, IBackupLocationFactoryResolver locationFactoryResolver);
    }
}
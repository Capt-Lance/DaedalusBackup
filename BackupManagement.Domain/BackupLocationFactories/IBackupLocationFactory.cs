using BackupManagement.Domain.Backups.IncrementalBackups;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    //Todo: break this up into VirtualDiskRepo, IncrementCollectionRepo and rework IBackupLocationFactoryResolver to resolve the different ones
    public interface IBackupLocationFactory
    {
        Stream Open(VirtualDisk vd);

        Task<IncrementCollection> GetIncrementCollectionAsync(string targetLocation);
        Task SaveIncrementCollectionAsync(IncrementCollection incrementCollection, string targetLocation);
        Stream Open(Chunk chunk, string path);
        Stream Open(string path);

    }
}

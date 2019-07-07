using System.IO;

namespace BackupManagement.Domain
{
    public interface IBackupLocationFactory
    {
        Stream Open(VirtualDisk vd);

        IncrementCollection GetIncrementCollection(IncrementalBackup backup, string targetLocation);
        Stream Open(Chunk chunk, string path);
        Stream Open(string path);

    }
}

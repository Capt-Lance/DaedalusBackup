using System.IO;

namespace BackupManagement.Domain
{
    public interface IBackupStreamFactory
    {
        Stream Open(VirtualDisk vd);
        Stream Open(Backup backup);
        Stream Open(Chunk chunk, string path);

    }
}

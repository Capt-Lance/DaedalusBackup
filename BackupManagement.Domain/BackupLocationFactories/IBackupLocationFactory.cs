using System.IO;

namespace BackupManagement.Domain
{
    public interface IBackupLocationFactory
    {
        Stream Open(VirtualDisk vd);
        Stream Open(Backup backup);
        Stream Open(Chunk chunk, string path);
        Stream Open(string path);

    }
}

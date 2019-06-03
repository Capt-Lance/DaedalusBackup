using System.IO;

namespace BackupManagement.Domain
{
    public interface IBackupStreamFactory
    {
        Stream Open(VirtualDisk vd);

    }
}

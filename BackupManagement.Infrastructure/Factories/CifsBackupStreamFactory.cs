using BackupManagement.Domain;
using System.IO;

namespace BackupManagement.Infrastructure.Factories
{
    public class CifsBackupStreamFactory : IBackupStreamFactory
    {
        public Stream Open(VirtualDisk vd)
        {
            return File.Open(vd.Location, FileMode.Open, FileAccess.Read);
        }

        public Stream Open(Chunk chunk)
        {
            throw new System.NotImplementedException();
        }
    }
}

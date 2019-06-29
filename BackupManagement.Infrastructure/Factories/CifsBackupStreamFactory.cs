using BackupManagement.Domain;
using System.IO;

namespace BackupManagement.Infrastructure.Factories
{
    public class CifsBackupStreamFactory : IBackupLocationFactory
    {
        public Stream Open(VirtualDisk vd)
        {
            return File.Open(vd.Location, FileMode.Open, FileAccess.Read);
        }

        public Stream Open(Chunk chunk, string path)
        {
            throw new System.NotImplementedException();
        }

        public Stream Open(Backup backup)
        {
            throw new System.NotImplementedException();
        }
    }
}

using BackupManagement.Domain;
using System;

namespace BackupManagement.Infrastructure.Factories
{
    public class BackupLocationFactoryResolver
    {
        public IBackupLocationFactory Resolve(LocationType backupType)
        {
            switch (backupType)
            {
                case LocationType.CIFS:
                    {
                        return new CIFSBackupLocationFactory();
                    }
                default:
                    {
                        throw new ArgumentException($"Backup type {backupType.ToString()} does not exist");
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class BackupStreamFactoryResolver
    {
        public static IBackupLocationFactory Resolve(BackupLocationType backupType)
        {
            switch (backupType)
            {
                case BackupLocationType.CIFS:
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

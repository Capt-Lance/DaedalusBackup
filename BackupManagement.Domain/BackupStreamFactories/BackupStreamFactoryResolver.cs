using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class BackupStreamFactoryResolver
    {
        public static IBackupStreamFactory Resolve(BackupStreamType backupType)
        {
            switch (backupType)
            {
                case BackupStreamType.CIFS:
                    {
                        return new CIFSBackupStreamFactory();
                    }
                default:
                    {
                        throw new ArgumentException($"Backup type {backupType.ToString()} does not exist");
                    }
            }
        }
    }
}

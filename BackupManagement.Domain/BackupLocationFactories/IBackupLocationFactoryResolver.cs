using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public interface IBackupLocationFactoryResolver
    {
        IBackupLocationFactory Resolve(BackupLocationType backupType);
    }
}

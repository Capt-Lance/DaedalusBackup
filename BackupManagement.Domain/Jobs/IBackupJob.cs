using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public interface IJob
    {
        Task RunAsync(IBackupLocationFactoryResolver backupLocationFactoryResolver);
    }
}

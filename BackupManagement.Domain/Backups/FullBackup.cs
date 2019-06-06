using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.Backups
{
    public class FullBackup : Backup
    {
        public FullBackup(DateTime dateCreated, string path): base( dateCreated, path) { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class FullBackup : Backup
    {
        public FullBackup(DateTime dateCreated, string path): base( dateCreated, path) { }
    }
}

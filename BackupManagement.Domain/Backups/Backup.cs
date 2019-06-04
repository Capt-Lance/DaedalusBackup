using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class Backup
    {
        public int Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public string Path { get; protected set; }

        public Backup()
        {
            DateCreated = DateTime.Now;
        }
    }
}

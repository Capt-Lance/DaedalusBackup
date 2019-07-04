using System;

namespace BackupManagement.Domain
{
    public class Backup
    {
        public int Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public string Path { get; protected set; }

        public Backup(DateTime dateCreated, string path)
        {
            DateCreated = dateCreated;
            Path = path;
        }

    }
}

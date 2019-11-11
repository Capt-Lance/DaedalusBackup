using System;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public abstract class Backup : Entity
    {
        public int Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public string Path { get; protected set; }

        public Backup(DateTime dateCreated, string path)
        {
            DateCreated = dateCreated;
            Path = path;
        }

        // Right now backups are static methods. Need to decide if they should be static or instance methods
        //public abstract Task<Backup> BackupAsync(IBackupLocationFactoryResolver factoryResolver, VirtualMachine vm, LocationType targetLocationType, string targetLocation)

    }
}

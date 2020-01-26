using BackupManagement.Domain.Common;
using System;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public abstract class Backup : Entity
    {
        public int Id { get; protected set; }
        public DateTime DateCreated { get; private set; }
        public LocationType LocationType { get; private set; }
        public string Path { get; protected set; }

        public Backup(LocationType locationType, string path, DateTime dateCreated)
        {
            DateCreated = dateCreated;
            LocationType = locationType;
            Path = path;
        }

        // Right now backups are static methods. Need to decide if they should be static or instance methods
        //public abstract Task<Backup> BackupAsync(IBackupLocationFactoryResolver factoryResolver, VirtualMachine vm, LocationType targetLocationType, string targetLocation)

    }
}

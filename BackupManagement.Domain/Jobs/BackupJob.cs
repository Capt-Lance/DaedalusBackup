using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public abstract class BackupJob<T>: IJob where T: Backup
    {
        public int Id { get; private set; }
        public List<T> Backups { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; protected set; }
        public BackupLocationType TargetLocationType { get; protected set; }
        public string TargetLocation { get; protected set; }

        protected BackupJob(
            DateTime dateCreated, 
            DateTime dateModified, 
            BackupLocationType targetLocationType, 
            string targetLocation)
        {
            DateCreated = dateCreated;
            DateModified = dateModified;
            Backups = new List<T>();
        }

        // TODO:
        // Might not make this abstract as each job type might need it's own signature.
        // Need to think about this more
        //public abstract BackupJob CreateNew()
        public abstract Task Run();

    }
}

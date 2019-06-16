using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public abstract class Job
    {
        public int Id { get; private set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; protected set; }

        protected Job(DateTime dateCreated, DateTime dateModified)
        {
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public abstract Task Run();

    }
}

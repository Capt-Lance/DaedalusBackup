﻿using BackupManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public abstract class BackupJob<T>: AggregateRoot where T: Backup
    {
        public int Id { get; private set; }
        public List<T> Backups { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; protected set; }
        public LocationType TargetLocationType { get; protected set; }
        public string TargetLocation { get; protected set; }
        public List<VirtualMachine> VirtualMachines { get; private set; }

        protected BackupJob(
            DateTime dateCreated, 
            DateTime dateModified, 
            LocationType targetLocationType, 
            string targetLocation,
            List<VirtualMachine> vms)
        {
            DateCreated = dateCreated;
            DateModified = dateModified;
            Backups = new List<T>();
            TargetLocationType = targetLocationType;
            TargetLocation = targetLocation;
            VirtualMachines = vms ?? new List<VirtualMachine>();
        }
    }
}

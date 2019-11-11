﻿using BackupManagement.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BackupManagement.Domain
{
    public class VirtualMachine: Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public List<VirtualDisk> VirtualDisks { get; private set; }

        public LocationType SourceLocationType { get {
                return LocationType.CIFS;
        } }

        private VirtualMachine(Guid id, string name, List<string> vhdPaths)
        {
            Id = id;
            Name = name;
            VirtualDisks = new List<VirtualDisk>();
            foreach (string vhdPath in vhdPaths)
            {
                VirtualDisks.Add(VirtualDisk.FromPath(vhdPath));
            }
        }

        public static VirtualMachine FromExisting(Guid id, string name, List<string> vhdPaths)
        {
            VirtualMachine vm = new VirtualMachine(id, name, vhdPaths);
            return vm;
        }


       
        /// <summary>
        /// Backup the virtual machine to the backupLocation
        /// </summary>
        /// <param name="factoryResolver"></param>
        /// <param name="backupLocationType"></param>
        /// <param name="backupLocation"></param>
        /// <returns></returns>
        public async Task<FullBackup> CreateFullBackupAsync(
            IBackupLocationFactoryResolver factoryResolver, 
            LocationType backupLocationType,
            string backupLocation
            )
        {
            FullBackup backup = FullBackup.CreateNew($"{backupLocation}/{Name}");
            var backupCreatedEvent = new VirtualMachineFullBackupCreated(this, backup);
            AddDomainEvent(backupCreatedEvent);
            return backup;
            //throw new NotImplementedException();//either get rid of this or make it use events. If keeping this, use defered events instead of explicitly calling the eventBus
            //string baseDirectory = $"{backupLocation}/{Name}";
            //FullBackup backup = await FullBackup.BackupAsync(this, factoryResolver, backupLocationType, baseDirectory);
            //return backup;
        }

        /// <summary>
        /// Creates an incremental backup in the backupLocation.
        /// If an incremental backup has been ran in the backup location, only changed data will be saved.
        /// </summary>
        /// <returns></returns>
        public async Task<IncrementalBackup> CreateIncrementalBackupAsync(
            IBackupLocationFactoryResolver factoryResolver,
            LocationType targetLocationType,
            string targetLocation
            )
        {
            throw new NotImplementedException();// either get rid of this or make it use events (see above)
            //string baseDirectory = $"{targetLocation}/{Name}";
            //IncrementalBackup backup = await IncrementalBackup.BackupAsync(this, factoryResolver, targetLocationType, targetLocation);
            //return backup;
        }

    }
}

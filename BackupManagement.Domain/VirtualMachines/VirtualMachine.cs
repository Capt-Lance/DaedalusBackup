using BackupManagement.Domain.DomainEvents;
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

        private string GetBaseDirectory(string location)
        {
            return $"{location}/{Name}";
        }

        /// <summary>
        /// Creates an instance of FullBackup with the VirtualMachineFullBackupCreated domain event added
        /// </summary>
        /// <param name="factoryResolver"></param>
        /// <param name="targetLocationType"></param>
        /// <param name="backupLocation"></param>
        /// <returns></returns>
        public FullBackup CreateFullBackup(
            LocationType targetLocationType,
            string backupLocation
            )
        {
            FullBackup backup = FullBackup.CreateNew(targetLocationType, GetBaseDirectory(backupLocation));
            var backupCreatedEvent = new VirtualMachineFullBackupCreated(this, backup);
            AddDomainEvent(backupCreatedEvent);
            return backup;
        }

        /// <summary>
        /// Creates an instance of IncrementalBackup with the VirtualMachineIncrementalBackupCreated domain event added
        /// </summary>
        /// <returns></returns>
        public IncrementalBackup CreateIncrementalBackup(
            LocationType targetLocationType,
            string targetLocation
            )
        {
            string baseDirectory = $"{targetLocation}/{Name}";
            //todo: Don't hardcode size
            IncrementalBackup backup = IncrementalBackup.CreateNew(targetLocationType, baseDirectory, 512);
            var backupCreatedEvent = new VirtualMachineIncrementalBackupCreated(this, backup);
            AddDomainEvent(backupCreatedEvent);
            return backup;
        }

    }
}

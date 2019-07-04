using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BackupManagement.Domain
{
    public class VirtualMachine
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public List<VirtualDisk> VirtualDisks { get; private set; }

        public BackupLocationType SourceLocationType { get {
                return BackupLocationType.CIFS;
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
        /// Backup the virtual machine to the specified directory
        /// </summary>
        /// <param name="factoryResolver"></param>
        /// <param name="backupLocationType"></param>
        /// <param name="backupLocation"></param>
        /// <returns></returns>
        public async Task<FullBackup> CreateFullBackupAsync(
            IBackupLocationFactoryResolver factoryResolver, 
            BackupLocationType backupLocationType,
            string backupLocation
            )
        {
            string baseDirectory = $"{backupLocation}/{Name}";
            FullBackup backup = await FullBackup.CreateNewAsync(this, factoryResolver, backupLocationType, baseDirectory);
            return backup;
        }

        //public async Task BackupVirtualDiskIncrementallyAsync(VirtualDisk vd, IBackupStreamFactory streamFactory, Stream targetStream)
        //{
        //    throw new NotImplementedException();
        //}

        public string GenerateBackupFileName()
        {
            throw new NotImplementedException();
            //return
        }
    }
}

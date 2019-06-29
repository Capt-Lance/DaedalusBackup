using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class VirtualMachine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<VirtualDisk> VirtualDisks { get; set; }

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

        public static VirtualMachine New(Guid id, string name, List<string> vhdPaths)
        {
            VirtualMachine vm = new VirtualMachine(id, name, vhdPaths);
            return vm;
        }

        //public async Task BackupVirtualDiskAsync(VirtualDisk vd, IBackupStreamFactory streamFactory, Stream targetStream)
        //{
        //    Stream readStream = streamFactory.Open(vd);
        //    int bufferSize = 512;
        //    byte[] buffer = new byte[bufferSize];
        //    while (readStream.Position < readStream.Length)
        //    {
        //        long remainingBytes = readStream.Length - readStream.Position;
        //        if (remainingBytes < bufferSize)
        //        {
        //            buffer = new byte[remainingBytes];
        //        }
        //        await readStream.ReadAsync(buffer);
        //        await targetStream.WriteAsync(buffer);
        //    }
        //    readStream.Close();
        //    targetStream.Close();
        //}

        public async Task FullBackup()
        {
            throw new NotImplementedException();
        }

        public async Task FullBackup(IBackupLocationFactory sourceLocation, IBackupLocationFactory targetLocationFactory)
        {
            throw new NotImplementedException();
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

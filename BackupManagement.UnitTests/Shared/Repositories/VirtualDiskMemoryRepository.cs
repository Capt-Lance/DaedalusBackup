//using BackupManagement.Domain.VirtualDisks;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace BackupManagement.Test.Shared.Repositories
//{
//    public class VirtualDiskMemoryRepository : IVirtualDiskRepository
//    {
//        private Dictionary<string, MemoryStream> streams = new Dictionary<string, MemoryStream>();


//        public Stream GetVirtualDiskStream(VirtualDisk vd)
//        {
//            if (!streams.ContainsKey(vd.Location))
//            {
//                byte[] newData = new byte[789];
//                Random rndm = new Random();
//                rndm.NextBytes(newData);
//                streams[vd.Location] = new MemoryStream(newData);
//            }
//            return streams[vd.Location];
            
//        }

//        public async Task Write(string location, byte[] dataToWrite)
//        {
//            if (!streams.ContainsKey(location)) { throw new InvalidOperationException("Location does not exist"); }
//            await streams[location].WriteAsync(dataToWrite);
//        }
//    }
//}

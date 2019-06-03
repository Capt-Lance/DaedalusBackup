//using BackupManagement.Domain.VirtualDisks;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace BackupManagement.Domain.Services
//{
//    public class BackupService
//    {
//        private readonly IVirtualDiskRepository vdRepo;
//        public BackupService(IVirtualDiskRepository vdRepo)
//        {
//            this.vdRepo = vdRepo;
//        }

//        // Future change: Make target a stream so that it doesn't matter if it is local/smb/sftp/nfs/etc

//        public async Task SaveVirtualDisk(VirtualDisk vd, string targetLocation)
//        {
//            Stream readStream = vdRepo.GetVirtualDiskStream(vd);
//            byte[] buffer = new byte[512];
//            while(readStream.Position <= readStream.Length)
//            {
//                await readStream.ReadAsync(buffer);
//                await vdRepo.Write(targetLocation, buffer);
//            }
//        }
//    }
//}

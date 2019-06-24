
using BackupManagement.Domain;
using BackupManagement.Infrastructure.Factories;
using BackupManagement.Infrastructure.HyperV.Repositories;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace BackupManagement.IntegrationTests.VirtualMachines
{
    public class VirtualMachineTest
    {
        // Redoing who controls backups
        //[Fact]
        //public async Task CopyOPNSenseVHDs()
        //{
        //    string vmName = "OPNSense";
        //    string locationToBackup = "C:\\Test";
        //    IVirtualMachineRepository vmRepo = new HyperVVirtualMachineRepository();
        //    VirtualMachine vm = vmRepo.Get(vmName);
        //    foreach(VirtualDisk vd in vm.VirtualDisks)
        //    {
        //        string fileName = vd.GenerateBackupDiskName();
        //        string filePath = $"{locationToBackup}\\{fileName}";
        //        FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write);
        //        IBackupStreamFactory backupFactory = new CifsBackupStreamFactory();
        //        await vm.BackupVirtualDiskAsync(vd, backupFactory, fs);
        //    }
        //}

        // 16GB file takes 9min to run. 
        //[Fact]
        //public async Task CopyKaliVHDs()
        //{
        //    string vmName = "Kali";
        //    string locationToBackup = "C:\\Test";
        //    IVirtualMachineRepository vmRepo = new HyperVVirtualMachineRepository();
        //    VirtualMachine vm = vmRepo.Get(vmName);
        //    foreach (VirtualDisk vd in vm.VirtualDisks)
        //    {
        //        string fileName = vd.GenerateBackupDiskName();
        //        string filePath = $"{locationToBackup}\\{fileName}";
        //        FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write);
        //        IBackupStreamFactory backupFactory = new CifsBackupStreamFactory();
        //        await vm.BackupVirtualDiskAsync(vd, backupFactory, fs);
        //    }
        //}
    }
}

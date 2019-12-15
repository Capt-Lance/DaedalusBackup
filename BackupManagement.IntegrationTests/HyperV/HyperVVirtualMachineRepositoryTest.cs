using BackupManagement.Domain;
using BackupManagement.Infrastructure.HyperV.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace BackupManagement.IntegrationTests.HyperV
{
    public class HyperVVirtualMachineRepositoryTest
    {
        [Fact]
        public void GetVirtualMachines()
        {
            HyperVVirtualMachineRepository vmRepo = new HyperVVirtualMachineRepository();
            IEnumerable<VirtualMachine> vms = vmRepo.GetAll();
            Assert.True(vms.Count() > 0, "No VMs returned");

        }

        //[TestMethod]
        //public void GetVirtualMachineByName()
        //{
        //    VirtualMachineRepository
        //}

        [Fact]
       public void VirtualDisksExist()
        {
            HyperVVirtualMachineRepository vmRepo = new HyperVVirtualMachineRepository();
            IEnumerable<VirtualMachine> vms = vmRepo.GetAll();
            bool allExist = vms.Count() > 0;
            foreach(VirtualMachine vm in vms)
            {
                foreach(VirtualDisk vd in vm.VirtualDisks)
                {
                    allExist = allExist && File.Exists(vd.Location);
                }
            }
            Assert.True(allExist, "Not all Virtual Disks exist");
        }
    }
}

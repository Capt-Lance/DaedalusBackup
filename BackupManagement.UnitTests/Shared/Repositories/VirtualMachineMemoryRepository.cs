using BackupManagement.Domain;
using System;
using System.Collections.Generic;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    public class VirtualMachineMemoryRepository : IVirtualMachineRepository
    {
        private List<VirtualMachine> vms = new List<VirtualMachine>() {
                VirtualMachine.New(Guid.Parse("36f32421-2d18-4545-9466-6f86d62f53f7"), "test1", new List<string>(){"test1/test1.vhd" }),
                VirtualMachine.New(Guid.Parse("d0787607-309e-4ecc-85d5-4e58093f2b27"), "test2", new List<string>(){"test2/test2.vhd" }),
                VirtualMachine.New(Guid.Parse("36f32421-2d18-4545-9466-6f86d62f53f7"), "test3", new List<string>(){"test3/test3.vhd" }),
            };
        public VirtualMachine Get(string name)
        {
            VirtualMachine vm = vms.Find(x => x.Name == name);
            if (vm == null) { throw new KeyNotFoundException($"Could not find virtual machine with name of '{name}'"); }
            return vm;

        }

        public IEnumerable<VirtualMachine> GetAll()
        {
            
            return vms;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public interface IVirtualMachineRepository
    {
        IEnumerable<VirtualMachine> GetAll();

        VirtualMachine Get(string name);
    }
}

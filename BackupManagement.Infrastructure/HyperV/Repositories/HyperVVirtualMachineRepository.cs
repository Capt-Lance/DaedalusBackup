using System;
using System.Collections.Generic;
using System.Management;
using System.IO;
using BackupManagement.Domain;

namespace BackupManagement.Infrastructure.HyperV.Repositories
{
    public class HyperVVirtualMachineRepository : IVirtualMachineRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm">ManagementObject for the virtual machine from Msvm_ComputerSystem class</param>
        /// <returns></returns>
        private List<string> GetVhdPaths(ManagementObject vm)
        {
            var vmSettings = WmiUtilities.GetVirtualMachineSettings(vm);
            var vhdSettings = WmiUtilities.GetVhdSettingsFromVirtualMachineSettings(vmSettings);
            List<string> vhdPaths = new List<string>();
            foreach (ManagementObject mo in vhdSettings)
            {
                var test = mo.Properties;
                var vhdPath = ((string[])mo.Properties["HostResource"].Value)[0];
                vhdPaths.Add(vhdPath);
            }
            return vhdPaths;
        }

        private VirtualMachine FromManagementObject(ManagementObject wm)
        {
            Guid id = Guid.Parse(wm["Name"].ToString());
            ObjectQuery storageQuery = new ObjectQuery("SELECT * FROM Msvm_");
            string name = wm["ElementName"].ToString();
            List<string> vhdPaths = GetVhdPaths(wm);
            VirtualMachine vm = VirtualMachine.New(id, name, vhdPaths);
            return vm;
        }

        public IEnumerable<VirtualMachine> GetAll()
        {
            // add this namespace in order to access WMI
            // also need to add a reference to System.Management.dll

            // we want to connect locally (".") to the Hyper-V namespace ("root\virtualization")
            ManagementScope manScope = new ManagementScope(@"\\.\root\virtualization\v2");

            // define the information we want to query - in this case, just grab all properties of the object
            ObjectQuery vmQuery = new ObjectQuery("SELECT * FROM Msvm_ComputerSystem");

            // connect and set up our search
            ManagementObjectSearcher vmSearcher = new ManagementObjectSearcher(manScope, vmQuery);
            ManagementObjectCollection vmCollection = vmSearcher.Get();
            List<VirtualMachine> vms = new List<VirtualMachine>();
            // loop through the machines
            foreach (ManagementObject wm in vmCollection)
            {
                var test = wm.Properties;
                if (wm["Caption"].ToString() == "Virtual Machine")
                {
                    //Guid id = Guid.Parse(wm["Name"].ToString());
                    //ObjectQuery storageQuery = new ObjectQuery("SELECT * FROM Msvm_");
                    //string name = wm["ElementName"].ToString();
                    //List<string> vhdPaths = GetVhdPaths(wm);
                    //VirtualMachine vm = VirtualMachine.New(id, name, vhdPaths);
                    VirtualMachine vm = FromManagementObject(wm);
                    vms.Add(vm);
                }

            }
            return vms;
        }

        public Stream GetVhd(string path)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException($"Could not find {path}"); }
            FileStream fs = File.OpenRead(path);
            return fs;
        }

        public VirtualMachine Get(string name)
        {
            var wm = WmiUtilities.GetVirtualMachine(name, new ManagementScope(@"\\.\root\virtualization\v2"));
            return FromManagementObject(wm);
        }



        //    /// <summary>
        //    /// Gets the array of Msvm_StorageAllocationSettingData of VHDs associated with the virtual machine.
        //    /// </summary>
        //    /// <param name="virtualMachine">The virtual machine object.</param>
        //    /// <returns>Array of Msvm_StorageAllocationSettingData of VHDs associated with the virtual machine.</returns>
        //    public static
        //    ManagementObject[]
        //    GetVhdSettings(
        //        ManagementObject virtualMachine)
        //    {
        //        // Get the virtual machine settings (Msvm_VirtualSystemSettingData object).
        //        using (ManagementObject vssd = GetVirtualMachineSettings(virtualMachine))
        //        {
        //            return GetVhdSettingsFromVirtualMachineSettings(vssd);
        //        }
        //    }

        //    /// <summary>
        //    /// Gets the array of Msvm_StorageAllocationSettingData of VHDs associated with the given virtual
        //    /// machine settings.
        //    /// </summary>
        //    /// <param name="virtualMachineSettings">A ManagementObject representing the settings of a virtual
        //    /// machine or snapshot.</param>
        //    /// <returns>Array of Msvm_StorageAllocationSettingData of VHDs associated with the given settings.</returns>
        //    public static
        //    ManagementObject[]
        //    GetVhdSettingsFromVirtualMachineSettings(
        //        ManagementObject virtualMachineSettings)
        //    {
        //        const UInt16 SASDResourceTypeLogicalDisk = 31;

        //        List<ManagementObject> sasdList = new List<ManagementObject>();

        //        //
        //        // Get all the SASDs (Msvm_StorageAllocationSettingData)
        //        // and look for VHDs.
        //        //
        //        using (ManagementObjectCollection sasdCollection =
        //            virtualMachineSettings.GetRelated("Msvm_StorageAllocationSettingData",
        //                "Msvm_VirtualSystemSettingDataComponent",
        //                null, null, null, null, false, null))
        //        {
        //            foreach (ManagementObject sasd in sasdCollection)
        //            {
        //                var test = sasd.Properties;
        //                if ((UInt16)sasd["ResourceType"] == SASDResourceTypeLogicalDisk)
        //                {
        //                    sasdList.Add(sasd);
        //                }
        //                else
        //                {
        //                    sasd.Dispose();
        //                }
        //            }
        //        }

        //        if (sasdList.Count == 0)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return sasdList.ToArray();
        //        }
        //    }

        //    /// <summary>
        //    /// Gets the virtual machine's configuration settings object.
        //    /// </summary>
        //    /// <param name="virtualMachine">The virtual machine.</param>
        //    /// <returns>The virtual machine's configuration object.</returns>
        //    public static ManagementObject
        //    GetVirtualMachineSettings(
        //        ManagementObject virtualMachine)
        //    {
        //        using (ManagementObjectCollection settingsCollection =
        //                virtualMachine.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState",
        //                null, null, null, null, false, null))
        //        {
        //            ManagementObject virtualMachineSettings =
        //                GetFirstObjectFromCollection(settingsCollection);

        //            return virtualMachineSettings;
        //        }
        //    }

        //    /// <summary>
        //    /// Gets the first object in a collection of ManagementObject instances.
        //    /// </summary>
        //    /// <param name="collection">The collection of ManagementObject instances.</param>
        //    /// <returns>The first object in the collection</returns>
        //    public static ManagementObject
        //    GetFirstObjectFromCollection(
        //        ManagementObjectCollection collection)
        //    {
        //        if (collection.Count == 0)
        //        {
        //            throw new ArgumentException("The collection contains no objects", "collection");
        //        }

        //        foreach (ManagementObject managementObject in collection)
        //        {
        //            return managementObject;
        //        }

        //        return null;
        //    }
    }
}

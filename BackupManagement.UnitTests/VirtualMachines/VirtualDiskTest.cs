using BackupManagement.Domain;
using BackupManagement.Domain.VirtualMachines;
using Xunit;

namespace BackupManagement.UnitTest.VirtualMachines
{
    [Collection("Unit Tests")]
    public class VirtualDiskTest
    {
        [Fact]
        public void FromPathParsesPath()
        {
            string path = "C:/Test/disks";
            string name = "mydisk";
            string extension = "vhd";
            string absolutePath = path + "/" + name + "." + extension;
            VirtualDisk vd = VirtualDisk.FromPath(absolutePath);
            Assert.True(vd.Path == path, "Path was not property parsed");
        }

        [Fact]
        public void FromPathReplacesWindowsNotation()
        {
            string path = "C:/Test/disks";
            string windowsPath = path.Replace("/", "\\");
            string name = "mydisk";
            string extension = "vhd";
            string absolutePath = windowsPath + "\\" + name + "." + extension;
            VirtualDisk vd = VirtualDisk.FromPath(absolutePath);
            Assert.True(vd.Path == path, "Path was not property parsed");
        }

        [Fact]
        public void FromPathParsesExtension()
        {
            string path = "C:/Test/disks";
            string name = "mydisk";
            string extension = "vhd";
            string location = path + "/" + name + "." + extension;
            VirtualDisk vd = VirtualDisk.FromPath(location);
            Assert.True(vd.Extension == extension, "Extension was not combined correclty");
        }

        [Fact]
        public void FromPathParsesName()
        {
            string path = "C:\\Test\\disks";
            string name = "mydisk";
            string extension = "vhd";
            string absolutePath = path + "\\" + name + "." + extension;
            VirtualDisk vd = VirtualDisk.FromPath(absolutePath);
            Assert.True(vd.Name == name, "Name was not parsed from path correclty");
        }

        [Fact]
        public void LocationCombinesPathAndNameCorrectly()
        {
            string path = "C:/Test/disks";
            string name = "mydisk";
            string extension = "vhd";
            string location = path + "/" + name + "." + extension;
            VirtualDisk vd = VirtualDisk.FromPath(location);
            Assert.True(vd.Location == location, "Location was not combined correclty");
        }
    }
}

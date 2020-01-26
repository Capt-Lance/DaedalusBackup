using BackupManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class VirtualDisk : Entity
    {
        /// <summary>
        /// Name of the VirtualDisk w/o extension
        /// </summary>
        public string Name { get; set; }

        public string Extension { get; set; }
        /// <summary>
        /// Absolute Path of the directory the VirtualDisk is in
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Absolute path to the VirtualDisk (Path + Name)
        /// </summary>
        public string Location {
            get {
                return $"{Path}/{Name}.{Extension}";
            }
        }

        public string FileName { get
            {
                return $"{Name}.{Extension}";
            } }

        private VirtualDisk(string location)
        {
            string correctedPath = location.Replace("\\", "/");
            string[] pathParts = correctedPath.Split("/");
            SetNameAndExtension(pathParts[pathParts.Length - 1]);
            string path = "";
            for (int i = 0; i < pathParts.Length - 1; i++)
            {
                path = i == 0? pathParts[i] : path + "/" + pathParts[i];
            }
            Path = path;
        }

        private void SetNameAndExtension(string nameWithExtension)
        {
            string[] nameWithExtensionParts = nameWithExtension.Split('.');
            Extension = nameWithExtensionParts[nameWithExtensionParts.Length - 1];
            string[] nameParts = new string[nameWithExtensionParts.Length - 1];
            Array.Copy(nameWithExtensionParts, nameParts, nameWithExtensionParts.Length - 1);
            Name = String.Join('.', nameParts);
        }

        public static VirtualDisk FromPath(string path)
        {
            return new VirtualDisk(path);
        }

        public string GenerateBackupDiskName()
        {
            string currentTimeString = DateTime.UtcNow.ToString("s");
            currentTimeString = currentTimeString.Replace(':', '-');
            return $"{Name}_{currentTimeString}.{Extension}";
        }
    }
}

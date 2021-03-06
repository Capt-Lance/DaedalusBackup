﻿using BackupManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.UnitTests.Shared.Repositories
{
    public class MemoryBackupLocationFactoryResolver : IBackupLocationFactoryResolver
    {
        private MemoryBackupLocationFactory memoryFactory;
        public IBackupLocationFactory Resolve(LocationType backupType)
        {
            if (memoryFactory == null)
            {
                memoryFactory = new MemoryBackupLocationFactory();
            }
            return memoryFactory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.Backups.IncrementalBackups
{
    public class IncrementCollection
    {
        public VirtualDiskIncrement OriginalIncrement { get; private set; }
        private List<VirtualDiskIncrement> _increments;
        public IEnumerable<VirtualDiskIncrement> Increments {
            get
            {
                return _increments.AsReadOnly();
            }
        }

        private IncrementCollection()
        {
            _increments = new List<VirtualDiskIncrement>();
        }
        public HashSet<string> GetChunkHashes()
        {
            HashSet<string> chunkHashesHashSet = new HashSet<string>();
            if (OriginalIncrement != null)
            {
                chunkHashesHashSet.UnionWith(OriginalIncrement.GetChunkHashes());
            }
            foreach (VirtualDiskIncrement increment in Increments)
            {
                chunkHashesHashSet.UnionWith(increment.GetChunkHashes());
            }
            return chunkHashesHashSet;
        }

        public static IncrementCollection CreateNew()
        {
            return new IncrementCollection();
        }

        public void AddIncrement(VirtualDiskIncrement increment)
        {
            if (OriginalIncrement == null)
            {
                OriginalIncrement = increment;
            }
            else
            {
                _increments.Add(increment);
            }
        }

    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public class IncrementCollection
    {
        public Increment OriginalIncrement { get; private set; }
        public List<Increment> Increments { get; private set; }

        public HashSet<string> GetChunkHashes()
        {
            HashSet<string> chunkHashesHashSet = new HashSet<string>();
            if (OriginalIncrement != null)
            {
                chunkHashesHashSet.UnionWith(OriginalIncrement.GetChunkHashes());
            }
            foreach (Increment increment in Increments)
            {
                chunkHashesHashSet.UnionWith(increment.GetChunkHashes());
            }
            return chunkHashesHashSet;
        }
    }


}

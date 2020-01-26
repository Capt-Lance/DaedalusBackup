using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain.Backups.IncrementalBackups
{
    public class Chunk
    {
        public int Id { get; private set; }
        public string Hash { get; private set; }

        private Chunk() { }

        public static Chunk CreateNew(int id, string hash)
        {
            Chunk chunk = new Chunk();
            chunk.Id = id;
            chunk.Hash = hash;
            return chunk;
        }
    }
}

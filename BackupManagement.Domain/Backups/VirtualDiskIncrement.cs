using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class VirtualDiskIncrement
    {
        public DateTime DateCreated { get; private set; }
        public List<Chunk> Chunks { get; private set; }
        public string VirtualDiskName { get; private set; }
        private const int incrementSize = 536870912; //512MB
        private VirtualDiskIncrement()
        {
            DateCreated = DateTime.Now;
            Chunks = new List<Chunk>();
        }

        public static async Task<VirtualDiskIncrement> CreateNewAsync(
            IBackupLocationFactory sourceLocationFactory, 
            VirtualDisk vd, 
            IBackupLocationFactory targetLocationFactory, 
            string targetLocation, 
            int incrementSize, 
            HashSet<string> existingChunkHashes
            )
        {
            byte[] buffer = new byte[incrementSize];
            int chunkIndex = 0;
            VirtualDiskIncrement increment = new VirtualDiskIncrement();
            Stream sourceStream = sourceLocationFactory.Open(vd);
            while (sourceStream.Position < sourceStream.Length)
            {
                long remainingBytes = sourceStream.Length - sourceStream.Position;
                if (remainingBytes < buffer.Length)
                {
                    buffer = new byte[remainingBytes];
                }
                await sourceStream.ReadAsync(buffer);
                var sha = SHA256.Create();
                byte[] hashBytes = sha.ComputeHash(buffer);
                string hash = Convert.ToBase64String(hashBytes);
                Chunk chunk = Chunk.CreateNew(chunkIndex, hash);
                increment.Chunks.Add(chunk);
                if (!existingChunkHashes.Contains(chunk.Hash))
                {
                    Stream targetStream = targetLocationFactory.Open(chunk, targetLocation);
                    await targetStream.WriteAsync(buffer);
                    targetStream.Close();
                    existingChunkHashes.Add(chunk.Hash);
                }

                chunkIndex++;
            }
            return increment;
        }

        public IEnumerable<string> GetChunkHashes()
        {
            return Chunks.Select(x => x.Hash);
        }
    }
}

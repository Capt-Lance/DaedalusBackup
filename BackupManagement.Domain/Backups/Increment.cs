using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class Increment
    {
        public DateTime DateCreated { get; private set; }
        public List<Chunk> Chunks { get; private set; }

        private Increment()
        {
            DateCreated = DateTime.Now;
            Chunks = new List<Chunk>();
        }

        public static async Task<Increment> CreateNewAsync(Stream readStream, IBackupStreamFactory streamFactory, string path, int incrementSize, HashSet<string> existingChunkHashes)
        {
            byte[] buffer = new byte[incrementSize];
            int chunkIndex = 0;
            Increment increment = new Increment();
            while (readStream.Position < readStream.Length)
            {
                long remainingBytes = readStream.Length - readStream.Position;
                if (remainingBytes < buffer.Length)
                {
                    buffer = new byte[remainingBytes];
                }
                await readStream.ReadAsync(buffer);
                var sha = SHA256.Create();
                byte[] hashBytes = sha.ComputeHash(buffer);
                string hash = Convert.ToBase64String(hashBytes);
                Chunk chunk = Chunk.CreateNew(chunkIndex, hash);
                increment.Chunks.Add(chunk);
                if (!existingChunkHashes.Contains(chunk.Hash))
                {
                    Stream targetStream = streamFactory.Open(chunk, path);
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

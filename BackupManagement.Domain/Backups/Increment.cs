using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackupManagement.Domain
{
    public class Increment
    {
        public DateTime DateCreated { get; private set; }

        public static async Task<Increment> CreateNewAsync(Stream readStream, IBackupStreamFactory streamFactory, int incrementSize)
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
                Stream targetStream = streamFactory.Open(increment);
                await targetStream.WriteAsync(buffer);
                chunkIndex++;
            }
            return increment;
        }
    }
}

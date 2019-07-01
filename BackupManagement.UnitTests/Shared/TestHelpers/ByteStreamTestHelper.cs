//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace BackupManagement.UnitTests.Shared.Helpers
//{
//    public class ByteStreamTestHelper
//    {
//        public byte[] ReadAll(Stream stream)
//        {
//            byte[] data = new byte[stream.Length];
//            while (stream.Position < stream.Length)
//            {
                
//                int comparisonSize = dataLength < incrementSize ? dataLength : incrementSize;
//                byte[] sourceChunkData = new byte[comparisonSize];
//                byte[] targetChunkData = new byte[comparisonSize];
//                await sourceStream.ReadAsync(sourceChunkData);
//                await targetChunkStream.ReadAsync(targetChunkData);
//                isSame = sourceChunkData.SequenceEqual(targetChunkData) && isSame;
//                chunkIndex++;
//            }
//        }
//    } 
//    }
//}

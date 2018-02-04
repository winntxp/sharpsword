/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/1 13:19:02
 * ****************************************************************/
using System;
using System.IO;
using System.Text;

namespace SharpSword.Serializers
{
    /// <summary>
    /// Contains helper functions for serialization and deserialization of data items (e.g.cached items)
    /// </summary>
    public static class BinarySerialization
    {
        /// <summary>
        /// A helper method to serialize objects with BinaryWriter. Creates a memory stream 
        /// and a BinaryWriter on it, and invokes the callback specified.
        /// </summary>
        /// <param name="serialize">Serialization delegate</param>
        public static byte[] Serialize(Action<BinaryWriter> serialize)
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            using (var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
            {
                serialize(binaryWriter);
                data = memoryStream.ToArray();
            }

            return data;
        }

        /// <summary>
        /// A helper method to deserialize objects with BinaryWriter. Creates a memory stream 
        /// and a BinaryReader on it, and invokes the callback specified.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="input">input</param>
        /// <param name="deserialize">Deserialization delegate</param>
        public static TValue Deserialize<TValue>(byte[] input, Func<BinaryReader, TValue> deserialize)
        {
            using (var memoryStream = new MemoryStream(input))
            using (var binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
            {
                return deserialize(binaryReader);
            }
        }
    }
}

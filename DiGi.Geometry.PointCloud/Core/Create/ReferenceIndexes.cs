using System;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Decodes the per-point model object identifiers from a binary payload produced by <see cref="Convert.ToSystem_Bytes(int[], Classes.PointCloudReferenceCollection)"/>.
        /// <para>Every failure mode returns <see langword="null"/> rather than throwing, so a truncated, misaligned, foreign or future-versioned buffer degrades to an empty result instead of propagating an exception out of a deserialization path.</para>
        /// <para>The embedded reference table, when present, is ignored here. Read it with <see cref="PointCloudReferenceCollection(byte[], int)"/>.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <param name="startIndex">The offset at which the block starts, which is non-zero when the block follows a coordinate block in the same buffer.</param>
        /// <returns>An <see cref="int"/> array holding one identifier per point, where -1 marks a point that links to nothing, or <see langword="null"/> when the buffer could not be decoded.</returns>
        public static int[]? ReferenceIndexes(byte[]? bytes, int startIndex = 0)
        {
            if (bytes == null || startIndex < 0 || startIndex > bytes.Length - Constants.PointCloud.BinaryReferenceHeaderLength)
            {
                return null;
            }

            if (bytes[startIndex] != Constants.PointCloud.BinaryReferenceMagic_0 || bytes[startIndex + 1] != Constants.PointCloud.BinaryReferenceMagic_1 || bytes[startIndex + 2] != Constants.PointCloud.BinaryReferenceMagic_2 || bytes[startIndex + 3] != Constants.PointCloud.BinaryReferenceMagic_3)
            {
                return null;
            }

            int readUInt16(int offset)
            {
                return bytes[startIndex + offset] | (bytes[startIndex + offset + 1] << 8);
            }

            long readInt64(int offset)
            {
                long result_Temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    result_Temp |= (long)bytes[startIndex + offset + i] << (i * 8);
                }

                return result_Temp;
            }

            if (readUInt16(4) != Constants.PointCloud.BinaryReferenceVersion)
            {
                return null;
            }

            int elementSize = readUInt16(6);
            if (elementSize != Constants.PointCloud.BinaryReferenceElementSize)
            {
                return null;
            }

            long count = readInt64(8);
            if (count < 0 || count > int.MaxValue)
            {
                return null;
            }

            long length_Expected = Constants.PointCloud.BinaryReferenceHeaderLength + (count * elementSize);
            if (startIndex + length_Expected > bytes.Length)
            {
                return null;
            }

            int count_Points = (int)count;
            int length_Indexes = count_Points * elementSize;
            int offset_Payload = startIndex + Constants.PointCloud.BinaryReferenceHeaderLength;

            int[] result = new int[count_Points];
            Buffer.BlockCopy(bytes, offset_Payload, result, 0, length_Indexes);

            if (!BitConverter.IsLittleEndian)
            {
                // The format is little-endian; block copy reads machine order. Dead code on every currently supported runtime.
                byte[] bytes_Indexes = new byte[length_Indexes];
                Buffer.BlockCopy(bytes, offset_Payload, bytes_Indexes, 0, length_Indexes);
                for (int i = 0; i < length_Indexes; i += elementSize)
                {
                    Array.Reverse(bytes_Indexes, i, elementSize);
                }

                Buffer.BlockCopy(bytes_Indexes, 0, result, 0, length_Indexes);
            }

            return result;
        }
    }
}

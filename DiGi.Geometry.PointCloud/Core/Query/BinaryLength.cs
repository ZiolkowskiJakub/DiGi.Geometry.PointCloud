namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the total length in bytes of the binary point cloud block starting at an offset, header included.
        /// <para>The length follows entirely from the header, which is what allows a block to be located inside a longer buffer: a file holding a cloud and its per-point model object links stores the two blocks one after the other, and this is how the reader finds where the first one ends.</para>
        /// <para>Returns -1 rather than throwing for every malformed input, matching the decoders.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <param name="startIndex">The offset at which the block starts.</param>
        /// <returns>The total length of the block in bytes, or -1 when no valid block starts at the offset.</returns>
        public static int BinaryLength(byte[]? bytes, int startIndex = 0)
        {
            if (bytes == null || startIndex < 0 || startIndex > bytes.Length - Constants.PointCloud.BinaryHeaderLength)
            {
                return -1;
            }

            if (bytes[startIndex] != Constants.PointCloud.BinaryMagic_0 || bytes[startIndex + 1] != Constants.PointCloud.BinaryMagic_1 || bytes[startIndex + 2] != Constants.PointCloud.BinaryMagic_2 || bytes[startIndex + 3] != Constants.PointCloud.BinaryMagic_3)
            {
                return -1;
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

            if (readUInt16(4) != Constants.PointCloud.BinaryVersion)
            {
                return -1;
            }

            int dimension = readUInt16(6);
            if (dimension <= 0)
            {
                return -1;
            }

            long count = readInt64(8);
            if (count < 0 || count > int.MaxValue)
            {
                return -1;
            }

            long result = Constants.PointCloud.BinaryHeaderLength + (count * dimension * sizeof(double));
            if (result > int.MaxValue)
            {
                return -1;
            }

            return (int)result;
        }
    }
}

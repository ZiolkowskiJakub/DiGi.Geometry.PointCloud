using System;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Decodes a coordinate-major point payload from the binary point cloud format produced by <see cref="Convert.ToSystem_Bytes(double[][])"/>.
        /// <para>Every failure mode returns <see langword="null"/> rather than throwing, so a truncated, misaligned, foreign or future-versioned buffer degrades to an empty result instead of propagating an exception out of a deserialization path.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <param name="dimension">The expected number of coordinate axes, or a value of zero or less to accept whatever the header declares.</param>
        /// <returns>A jagged <see cref="double"/> array holding one array per axis, or <see langword="null"/> when the buffer could not be decoded.</returns>
        public static double[][]? Coordinates(byte[]? bytes, int dimension = 0)
        {
            int length = Query.BinaryLength(bytes, 0);
            if (length < 0 || length != bytes!.Length)
            {
                return null;
            }

            return Coordinates(bytes, dimension, 0);
        }

        /// <summary>
        /// Decodes a coordinate-major point payload from a binary point cloud block starting at an offset inside a longer buffer.
        /// <para>Unlike <see cref="Coordinates(byte[], int)"/> this accepts trailing bytes, because a block located by offset is expected to be followed by something: a file holding a cloud together with its per-point model object links stores the two blocks one after the other.</para>
        /// <para>Every failure mode returns <see langword="null"/> rather than throwing, matching the exact length form.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <param name="dimension">The expected number of coordinate axes, or a value of zero or less to accept whatever the header declares.</param>
        /// <param name="startIndex">The offset at which the block starts.</param>
        /// <returns>A jagged <see cref="double"/> array holding one array per axis, or <see langword="null"/> when the block could not be decoded.</returns>
        public static double[][]? Coordinates(byte[]? bytes, int dimension, int startIndex)
        {
            if (bytes == null || startIndex < 0 || startIndex > bytes.Length - Constants.PointCloud.BinaryHeaderLength)
            {
                return null;
            }

            if (bytes[startIndex] != Constants.PointCloud.BinaryMagic_0 || bytes[startIndex + 1] != Constants.PointCloud.BinaryMagic_1 || bytes[startIndex + 2] != Constants.PointCloud.BinaryMagic_2 || bytes[startIndex + 3] != Constants.PointCloud.BinaryMagic_3)
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

            if (readUInt16(4) != Constants.PointCloud.BinaryVersion)
            {
                return null;
            }

            int dimension_Header = readUInt16(6);
            if (dimension_Header <= 0)
            {
                return null;
            }

            if (dimension > 0 && dimension_Header != dimension)
            {
                return null;
            }

            long count = readInt64(8);
            if (count < 0 || count > int.MaxValue)
            {
                return null;
            }

            long length_Expected = Constants.PointCloud.BinaryHeaderLength + (count * dimension_Header * sizeof(double));
            if (startIndex + length_Expected > bytes.Length)
            {
                return null;
            }

            int count_Points = (int)count;
            int length_Axis = count_Points * sizeof(double);

            double[][] result = new double[dimension_Header][];

            int offset_Axis = startIndex + Constants.PointCloud.BinaryHeaderLength;
            for (int i = 0; i < dimension_Header; i++)
            {
                double[] values = new double[count_Points];
                Buffer.BlockCopy(bytes, offset_Axis, values, 0, length_Axis);

                if (!BitConverter.IsLittleEndian)
                {
                    // The format is little-endian; block copy reads machine order. Dead code on every currently supported runtime.
                    byte[] bytes_Axis = new byte[length_Axis];
                    Buffer.BlockCopy(bytes, offset_Axis, bytes_Axis, 0, length_Axis);
                    for (int j = 0; j < length_Axis; j += sizeof(double))
                    {
                        Array.Reverse(bytes_Axis, j, sizeof(double));
                    }

                    Buffer.BlockCopy(bytes_Axis, 0, values, 0, length_Axis);
                }

                result[i] = values;
                offset_Axis += length_Axis;
            }

            return result;
        }
    }
}

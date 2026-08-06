using System;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Convert
    {
        /// <summary>
        /// Encodes a coordinate-major point payload into the binary point cloud format.
        /// <para>The layout is a fixed <see cref="Constants.PointCloud.BinaryHeaderLength"/> byte little-endian header holding the magic identifier, version, dimension, point count and flags, followed by the coordinate arrays one after another. The payload is planar rather than interleaved so it matches the in-memory layout exactly and each axis copies with a single block move.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length. Index zero is X, index one is Y and index two, when present, is Z.</param>
        /// <returns>A <see cref="byte"/> array holding the encoded cloud, or <see langword="null"/> when the input is null, empty, ragged or too large to address.</returns>
        public static byte[]? ToSystem_Bytes(this double[][]? coordinates)
        {
            if (coordinates == null || coordinates.Length == 0 || coordinates.Length > ushort.MaxValue)
            {
                return null;
            }

            int dimension = coordinates.Length;

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return null;
            }

            int count = values_First.Length;
            for (int i = 1; i < dimension; i++)
            {
                double[]? values = coordinates[i];
                if (values == null || values.Length != count)
                {
                    return null;
                }
            }

            long length_Payload = (long)count * dimension * sizeof(double);
            long length_Total = Constants.PointCloud.BinaryHeaderLength + length_Payload;
            if (length_Total > int.MaxValue)
            {
                return null;
            }

            byte[] result = new byte[(int)length_Total];

            result[0] = Constants.PointCloud.BinaryMagic_0;
            result[1] = Constants.PointCloud.BinaryMagic_1;
            result[2] = Constants.PointCloud.BinaryMagic_2;
            result[3] = Constants.PointCloud.BinaryMagic_3;

            void writeUInt16(int offset, int value)
            {
                result[offset] = (byte)(value & 0xFF);
                result[offset + 1] = (byte)((value >> 8) & 0xFF);
            }

            void writeInt64(int offset, long value)
            {
                for (int i = 0; i < 8; i++)
                {
                    result[offset + i] = (byte)((value >> (i * 8)) & 0xFF);
                }
            }

            writeUInt16(4, Constants.PointCloud.BinaryVersion);
            writeUInt16(6, dimension);
            writeInt64(8, count);

            // Bytes 16 to 19 hold the flags and bytes 20 to 31 are reserved. Both are left zeroed in version one.

            int offset_Axis = Constants.PointCloud.BinaryHeaderLength;
            int length_Axis = count * sizeof(double);
            for (int i = 0; i < dimension; i++)
            {
                Buffer.BlockCopy(coordinates[i], 0, result, offset_Axis, length_Axis);
                offset_Axis += length_Axis;
            }

            if (!BitConverter.IsLittleEndian)
            {
                // The format is defined as little-endian. Block copy emits machine order, so the payload is
                // byte-reversed per value on a big-endian host. Dead code on every currently supported runtime.
                for (int i = Constants.PointCloud.BinaryHeaderLength; i < result.Length; i += sizeof(double))
                {
                    Array.Reverse(result, i, sizeof(double));
                }
            }

            return result;
        }
    }
}

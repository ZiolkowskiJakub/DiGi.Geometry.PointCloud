using System;
using System.Text;

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

        /// <summary>
        /// Encodes per-point model object identifiers, and optionally the reference table they index into, as a self-contained binary payload.
        /// <para>The layout is a fixed <see cref="Constants.PointCloud.BinaryReferenceHeaderLength"/> byte little-endian header holding the magic identifier, version, identifier size, point count and flags, followed by the identifiers and then, when the table is embedded, its length prefixed UTF-8 JSON.</para>
        /// <para>The payload is self-describing, carrying its own point count, because the reflection serializer applies members in the order they appear in the JSON document rather than in the order the type declares them. A payload that relied on the coordinates having been read first would decode correctly or not depending on how a document happened to be written.</para>
        /// <para>The table is embedded only when the payload has to stand alone, as it does in a file. Inside a serialized object the table is already a member, and embedding it there would write the same fact twice and invite the two copies to disagree.</para>
        /// </summary>
        /// <param name="referenceIndexes">The per-point identifiers, where -1 marks a point that links to nothing.</param>
        /// <param name="pointCloudReferenceCollection">The reference table to embed, or <see langword="null"/> to write the identifiers alone.</param>
        /// <returns>A <see cref="byte"/> array holding the encoded payload, or <see langword="null"/> when the input is null or too large to address.</returns>
        public static byte[]? ToSystem_Bytes(this int[]? referenceIndexes, Classes.PointCloudReferenceCollection? pointCloudReferenceCollection)
        {
            if (referenceIndexes == null)
            {
                return null;
            }

            byte[]? bytes_Collection = null;
            if (pointCloudReferenceCollection != null)
            {
                string? @string = DiGi.Core.Convert.ToSystem_String(pointCloudReferenceCollection);
                if (@string == null)
                {
                    return null;
                }

                bytes_Collection = Encoding.UTF8.GetBytes(@string);
            }

            int count = referenceIndexes.Length;

            long length_Payload = (long)count * Constants.PointCloud.BinaryReferenceElementSize;
            long length_Collection = bytes_Collection == null ? 0 : sizeof(uint) + bytes_Collection.Length;
            long length_Total = Constants.PointCloud.BinaryReferenceHeaderLength + length_Payload + length_Collection;
            if (length_Total > int.MaxValue)
            {
                return null;
            }

            byte[] result = new byte[(int)length_Total];

            result[0] = Constants.PointCloud.BinaryReferenceMagic_0;
            result[1] = Constants.PointCloud.BinaryReferenceMagic_1;
            result[2] = Constants.PointCloud.BinaryReferenceMagic_2;
            result[3] = Constants.PointCloud.BinaryReferenceMagic_3;

            void writeUInt16(int offset, int value)
            {
                result[offset] = (byte)(value & 0xFF);
                result[offset + 1] = (byte)((value >> 8) & 0xFF);
            }

            void writeUInt32(int offset, uint value)
            {
                for (int i = 0; i < 4; i++)
                {
                    result[offset + i] = (byte)((value >> (i * 8)) & 0xFF);
                }
            }

            void writeInt64(int offset, long value)
            {
                for (int i = 0; i < 8; i++)
                {
                    result[offset + i] = (byte)((value >> (i * 8)) & 0xFF);
                }
            }

            writeUInt16(4, Constants.PointCloud.BinaryReferenceVersion);
            writeUInt16(6, Constants.PointCloud.BinaryReferenceElementSize);
            writeInt64(8, count);
            writeUInt32(16, bytes_Collection == null ? 0 : Constants.PointCloud.BinaryReferenceFlagCollection);

            // Bytes 20 to 31 are reserved and left zeroed in version one.

            int offset_Payload = Constants.PointCloud.BinaryReferenceHeaderLength;
            int length_Indexes = count * Constants.PointCloud.BinaryReferenceElementSize;
            Buffer.BlockCopy(referenceIndexes, 0, result, offset_Payload, length_Indexes);

            if (!BitConverter.IsLittleEndian)
            {
                // The format is defined as little-endian. Block copy emits machine order, so each identifier is
                // byte-reversed on a big-endian host. Dead code on every currently supported runtime.
                for (int i = offset_Payload; i < offset_Payload + length_Indexes; i += Constants.PointCloud.BinaryReferenceElementSize)
                {
                    Array.Reverse(result, i, Constants.PointCloud.BinaryReferenceElementSize);
                }
            }

            if (bytes_Collection != null)
            {
                writeUInt32(offset_Payload + length_Indexes, (uint)bytes_Collection.Length);
                Buffer.BlockCopy(bytes_Collection, 0, result, offset_Payload + length_Indexes + sizeof(uint), bytes_Collection.Length);
            }

            return result;
        }
    }
}

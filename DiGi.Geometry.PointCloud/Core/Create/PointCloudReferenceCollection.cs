using DiGi.Core.Interfaces;
using DiGi.Geometry.PointCloud.Core.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a reference table from a sequence of references, discarding nulls and duplicates.
        /// <para>The first occurrence of a reference fixes its identifier, and later duplicates are dropped. Deduplication happens here rather than in the constructor because a duplicate is a defect to clean up, not a shape to validate, and the constructor sits on the hot path of every clone and copy.</para>
        /// </summary>
        /// <param name="references">The references to store. This value can be null.</param>
        /// <returns>A new <see cref="Classes.PointCloudReferenceCollection"/>, or <see langword="null"/> when no valid reference was supplied.</returns>
        public static PointCloudReferenceCollection? PointCloudReferenceCollection(IEnumerable<ISerializableReference>? references)
        {
            if (references == null)
            {
                return null;
            }

            HashSet<ISerializableReference> references_Distinct = [];

            List<ISerializableReference> references_Result = [];
            foreach (ISerializableReference reference in references)
            {
                if (reference == null || !references_Distinct.Add(reference))
                {
                    continue;
                }

                references_Result.Add(reference);
            }

            if (references_Result.Count == 0)
            {
                return null;
            }

            return new PointCloudReferenceCollection(references_Result);
        }

        /// <summary>
        /// Builds a reference table and the matching per-point identifier array from a sequence holding one reference per point.
        /// <para>This is the shape a segmentation pass produces: it walks the points once, assigning a new identifier the first time it meets a model object and reusing it afterwards. Building both halves here is what keeps them consistent, because the table and the identifiers are the same fact recorded twice and nothing else in the library can check that they agree.</para>
        /// <para>A null entry in the sequence marks a point that links to nothing and is recorded as -1.</para>
        /// </summary>
        /// <param name="references">The references, one per point and in point order. This value can be null.</param>
        /// <param name="referenceIndexes">When this method returns, contains the per-point identifiers, or <see langword="null"/> when the sequence was null.</param>
        /// <returns>A new <see cref="Classes.PointCloudReferenceCollection"/>, or <see langword="null"/> when every point links to nothing.</returns>
        public static PointCloudReferenceCollection? PointCloudReferenceCollection(IEnumerable<ISerializableReference>? references, out int[]? referenceIndexes)
        {
            referenceIndexes = null;

            if (references == null)
            {
                return null;
            }

            Dictionary<ISerializableReference, int> ids = [];

            List<ISerializableReference> references_Result = [];
            List<int> referenceIndexes_Result = [];

            foreach (ISerializableReference reference in references)
            {
                if (reference == null)
                {
                    referenceIndexes_Result.Add(-1);

                    continue;
                }

                if (!ids.TryGetValue(reference, out int id))
                {
                    id = references_Result.Count;
                    ids[reference] = id;
                    references_Result.Add(reference);
                }

                referenceIndexes_Result.Add(id);
            }

            referenceIndexes = [.. referenceIndexes_Result];

            if (references_Result.Count == 0)
            {
                return null;
            }

            return new PointCloudReferenceCollection(references_Result);
        }

        /// <summary>
        /// Decodes the embedded reference table from a binary payload produced by <see cref="Convert.ToSystem_Bytes(int[], Classes.PointCloudReferenceCollection)"/>.
        /// <para>Returns <see langword="null"/> when the payload carries identifiers alone, which is the normal case for a payload travelling inside a serialized object that already holds the table as a member. Every failure mode returns <see langword="null"/> rather than throwing, matching <see cref="ReferenceIndexes(byte[], int)"/>.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <param name="startIndex">The offset at which the block starts, which is non-zero when the block follows a coordinate block in the same buffer.</param>
        /// <returns>The embedded <see cref="Classes.PointCloudReferenceCollection"/>, or <see langword="null"/> when none is present or the buffer could not be decoded.</returns>
        public static PointCloudReferenceCollection? PointCloudReferenceCollection(byte[]? bytes, int startIndex = 0)
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

            uint readUInt32(int offset)
            {
                uint result_Temp = 0;
                for (int i = 0; i < 4; i++)
                {
                    result_Temp |= (uint)bytes[startIndex + offset + i] << (i * 8);
                }

                return result_Temp;
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

            if ((readUInt32(16) & Constants.PointCloud.BinaryReferenceFlagCollection) == 0)
            {
                return null;
            }

            long count = readInt64(8);
            if (count < 0 || count > int.MaxValue)
            {
                return null;
            }

            long offset_Collection = Constants.PointCloud.BinaryReferenceHeaderLength + (count * elementSize);
            if (startIndex + offset_Collection + sizeof(uint) > bytes.Length)
            {
                return null;
            }

            uint length_Collection = readUInt32((int)offset_Collection);
            if (startIndex + offset_Collection + sizeof(uint) + length_Collection > bytes.Length)
            {
                return null;
            }

            string @string = Encoding.UTF8.GetString(bytes, startIndex + (int)offset_Collection + sizeof(uint), (int)length_Collection);

            return DiGi.Core.Convert.ToDiGi<PointCloudReferenceCollection>(@string)?.FirstOrDefault();
        }
    }
}

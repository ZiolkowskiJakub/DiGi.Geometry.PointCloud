using System.Numerics;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Modify
    {
        /// <summary>
        /// Adds a per-axis offset to every coordinate in the supplied arrays, in place.
        /// <para>Large inputs are split across partitions. Because each partition writes a disjoint range of the same arrays, no synchronization is needed at all. The ranged overload validates before it writes, and the partitions are checked up front, so a failure cannot leave the arrays half-modified.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="offsets">The offset to add to each axis. Must hold one value per axis.</param>
        /// <returns><see langword="true"/> when the offset was applied; otherwise <see langword="false"/>.</returns>
        public static bool OffsetCoordinates(this double[][]? coordinates, double[]? offsets)
        {
            if (coordinates == null || offsets == null || coordinates.Length == 0 || coordinates.Length != offsets.Length)
            {
                return false;
            }

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return false;
            }

            int count = values_First.Length;
            for (int axis = 1; axis < coordinates.Length; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != count)
                {
                    return false;
                }
            }

            int partitionCount = Query.PartitionCount(count, Constants.PointCloud.ParallelThreshold, Constants.PointCloud.StreamingProcessorFraction);
            if (partitionCount <= 1)
            {
                return OffsetCoordinates(coordinates, offsets, 0, count);
            }

            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, i =>
            {
                int startIndex = i * size;
                int length = count - startIndex;
                if (length > size)
                {
                    length = size;
                }

                if (length > 0)
                {
                    OffsetCoordinates(coordinates, offsets, startIndex, length);
                }
            });

            return true;
        }

        /// <summary>
        /// Adds a per-axis offset to a contiguous range of coordinates, in place, using a vectorised loop with a scalar tail.
        /// <para>Every axis is validated before any value is written, so a ragged input leaves the arrays untouched rather than partially modified.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="offsets">The offset to add to each axis. Must hold one value per axis.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns><see langword="true"/> when the offset was applied; otherwise <see langword="false"/>.</returns>
        public static bool OffsetCoordinates(this double[][]? coordinates, double[]? offsets, int startIndex, int count)
        {
            if (coordinates == null || offsets == null || coordinates.Length == 0 || coordinates.Length != offsets.Length || startIndex < 0)
            {
                return false;
            }

            for (int axis = 0; axis < coordinates.Length; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || count < 0 || startIndex > values.Length - count)
                {
                    return false;
                }
            }

            if (count == 0)
            {
                return true;
            }

            int width = Vector<double>.Count;
            bool vectorise = Vector.IsHardwareAccelerated && width > 1 && count >= width + width;

            for (int axis = 0; axis < coordinates.Length; axis++)
            {
                double offset = offsets[axis];
                if (offset == 0)
                {
                    continue;
                }

                double[] values = coordinates[axis]!;

                int index = startIndex;
                int end = startIndex + count;

                if (vectorise)
                {
                    Vector<double> vector_Offset = new(offset);

                    int end_Vector = end - width;
                    for (; index <= end_Vector; index += width)
                    {
                        Vector<double> vector = new(values, index);
                        (vector + vector_Offset).CopyTo(values, index);
                    }
                }

                for (; index < end; index++)
                {
                    values[index] += offset;
                }
            }

            return true;
        }
    }
}

using System.Numerics;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Counts the points of a coordinate-major payload that fall inside an axis-aligned box.
        /// <para>The bounds are expected to already include any tolerance. Folding the tolerance in once, before the scan, keeps it out of the inner loop and makes the result agree exactly with the per-point bounding box test, which compares against bounds widened by the same amount.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="minimums">The inclusive lower bound of each axis.</param>
        /// <param name="maximums">The inclusive upper bound of each axis.</param>
        /// <returns>The number of points inside the box, or -1 when the input is null, ragged or mismatched.</returns>
        public static int InRangeCount(this double[][]? coordinates, double[]? minimums, double[]? maximums)
        {
            if (coordinates == null || minimums == null || maximums == null || coordinates.Length == 0 || coordinates.Length != minimums.Length || coordinates.Length != maximums.Length)
            {
                return -1;
            }

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return -1;
            }

            int count = values_First.Length;
            for (int axis = 1; axis < coordinates.Length; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != count)
                {
                    return -1;
                }
            }

            if (count == 0)
            {
                return 0;
            }

            int partitionCount = PartitionCount(count, Constants.PointCloud.ParallelThreshold, Constants.PointCloud.StreamingProcessorFraction);
            if (partitionCount <= 1)
            {
                return InRangeCount(coordinates, minimums, maximums, 0, count);
            }

            int[] counts = new int[partitionCount];

            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, i =>
            {
                int startIndex = i * size;
                int length = count - startIndex;
                if (length > size)
                {
                    length = size;
                }

                counts[i] = length <= 0 ? 0 : InRangeCount(coordinates, minimums, maximums, startIndex, length);
            });

            int result = 0;
            for (int i = 0; i < partitionCount; i++)
            {
                result += counts[i];
            }

            return result;
        }

        /// <summary>
        /// Counts the points of a contiguous range of a coordinate-major payload that fall inside an axis-aligned box.
        /// <para>The counting pass is fully vectorised and needs no per-lane extraction. A lane-wise comparison yields a mask whose true lanes hold all bits set, which as a signed integer is minus one, so subtracting the mask from a running vector increments exactly the lanes that passed. Only one horizontal reduction is needed, at the very end.</para>
        /// <para>This matters because there is no portable way to extract a comparison mask on this target: the move-mask instruction lives behind the hardware intrinsics surface, which is not available here.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="minimums">The inclusive lower bound of each axis.</param>
        /// <param name="maximums">The inclusive upper bound of each axis.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns>The number of points inside the box, or -1 when the range is out of bounds.</returns>
        public static int InRangeCount(this double[][]? coordinates, double[]? minimums, double[]? maximums, int startIndex, int count)
        {
            if (coordinates == null || minimums == null || maximums == null || startIndex < 0 || count < 0)
            {
                return -1;
            }

            int dimension = coordinates.Length;
            if (dimension == 0 || dimension != minimums.Length || dimension != maximums.Length)
            {
                return -1;
            }

            for (int axis = 0; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || startIndex > values.Length - count)
                {
                    return -1;
                }
            }

            if (count == 0)
            {
                return 0;
            }

            int result = 0;

            int index = startIndex;
            int end = startIndex + count;

            int width = Vector<double>.Count;
            if (Vector.IsHardwareAccelerated && width > 1 && count >= width + width)
            {
                Vector<double>[] vectors_Minimum = new Vector<double>[dimension];
                Vector<double>[] vectors_Maximum = new Vector<double>[dimension];
                for (int axis = 0; axis < dimension; axis++)
                {
                    vectors_Minimum[axis] = new Vector<double>(minimums[axis]);
                    vectors_Maximum[axis] = new Vector<double>(maximums[axis]);
                }

                Vector<long> vector_Count = Vector<long>.Zero;
                Vector<long> vector_True = new(-1L);

                int end_Vector = end - width;
                for (; index <= end_Vector; index += width)
                {
                    Vector<long> vector_Mask = vector_True;
                    for (int axis = 0; axis < dimension; axis++)
                    {
                        Vector<double> vector = new(coordinates[axis]!, index);
                        vector_Mask = Vector.BitwiseAnd(vector_Mask, Vector.GreaterThanOrEqual(vector, vectors_Minimum[axis]));
                        vector_Mask = Vector.BitwiseAnd(vector_Mask, Vector.LessThanOrEqual(vector, vectors_Maximum[axis]));
                    }

                    vector_Count = Vector.Subtract(vector_Count, vector_Mask);
                }

                for (int i = 0; i < width; i++)
                {
                    result += (int)vector_Count[i];
                }
            }

            for (; index < end; index++)
            {
                bool inRange = true;
                for (int axis = 0; axis < dimension; axis++)
                {
                    double value = coordinates[axis]![index];
                    if (value < minimums[axis] || value > maximums[axis])
                    {
                        inRange = false;

                        break;
                    }
                }

                if (inRange)
                {
                    result++;
                }
            }

            return result;
        }
    }
}

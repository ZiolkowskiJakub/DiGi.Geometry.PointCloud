using System;
using System.Numerics;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the points of a cloud closest to a query position, nearest first.
        /// <para>Chooses between the two searches the same way the box queries do. Above <see cref="Constants.PointCloud.IndexThreshold"/> the cloud carries a spatial index and the descent visits a few dozen nodes regardless of how large the cloud is; below it there is no index and an exhaustive vectorised sweep is cheaper than building one.</para>
        /// <para>Both searches return the same answer for the same input, including on duplicated points, because both order their candidates through <see cref="Modify.InsertNeighbor"/>.</para>
        /// <para>Nothing is allocated on either path. The caller owns the result buffers, and the search runs on scalar coordinates without materializing a point object.</para>
        /// </summary>
        /// <param name="pointCloud">The cloud to search.</param>
        /// <param name="query">The query position, holding one value per axis.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <returns>The number of neighbours written, which is smaller than the requested count when the cloud holds fewer points, or -1 when the cloud is empty or the request is mismatched.</returns>
        public static int NearestIndexes(this Classes.PointCloud? pointCloud, ReadOnlySpan<double> query, Span<int> indexes, Span<double> distancesSquared)
        {
            double[][]? coordinates = pointCloud?.GetCoordinates(false);
            if (coordinates == null || query.Length < coordinates.Length)
            {
                return -1;
            }

            Classes.PointCloudIndex? pointCloudIndex = pointCloud!.EnsureIndex();
            if (pointCloudIndex != null)
            {
                int result = pointCloudIndex.NearestIndexes(coordinates, query[0], query[1], coordinates.Length == 3 ? query[2] : 0, indexes, distancesSquared);

                // A negative result means the index could not answer, which for a well-formed index is
                // unreachable. Falling through to the sweep keeps the answer correct rather than absent.
                if (result >= 0)
                {
                    return result;
                }
            }

            return NearestIndexes(coordinates, query, indexes, distancesSquared);
        }

        /// <summary>
        /// Retrieves the indexes of the points of a coordinate-major payload closest to a query position, nearest first.
        /// <para>This is the path taken when no spatial index exists, which is every cloud below <see cref="Constants.PointCloud.IndexThreshold"/>. An exhaustive vectorised sweep over that many points finishes in tens of microseconds, which is less than building an index would cost, so there is nothing to gain from a hierarchy at that size.</para>
        /// <para>Parallelised only above <see cref="Constants.PointCloud.ParallelThreshold"/>, which in practice means only when a cloud large enough to be indexed failed to produce an index. Each partition collects its own candidate set and they are merged afterwards, which is exact rather than approximate: a global winner is a winner in whichever partition holds it.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="query">The query position, holding one value per axis.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <returns>The number of neighbours written, which is smaller than the requested count when the payload holds fewer points, or -1 when the input is null, ragged or mismatched.</returns>
        public static int NearestIndexes(this double[][]? coordinates, ReadOnlySpan<double> query, Span<int> indexes, Span<double> distancesSquared)
        {
            int count = PointCount(coordinates);
            if (count < 0)
            {
                return -1;
            }

            int count_Requested = indexes.Length;
            if (count_Requested <= 0 || distancesSquared.Length < count_Requested || query.Length < coordinates!.Length)
            {
                return -1;
            }

            int partitionCount = PartitionCount(count, Constants.PointCloud.ParallelThreshold, Constants.PointCloud.StreamingProcessorFraction);
            if (partitionCount <= 1)
            {
                return NearestIndexes(coordinates, query, indexes, distancesSquared, 0, count);
            }

            // A span cannot cross into the loop body, so the per-partition sets live in two flat arrays
            // that the body slices. Two allocations of at most a few hundred entries, against a sweep
            // of a hundred thousand points or more.
            int[] indexes_Partition = new int[partitionCount * count_Requested];
            double[] distancesSquared_Partition = new double[partitionCount * count_Requested];
            int[] counts = new int[partitionCount];

            double[] query_Partition = query.ToArray();

            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, i =>
            {
                int startIndex = i * size;
                int length = count - startIndex;
                if (length > size)
                {
                    length = size;
                }

                counts[i] = length <= 0 ? 0 : NearestIndexes(coordinates, query_Partition, new Span<int>(indexes_Partition, i * count_Requested, count_Requested), new Span<double>(distancesSquared_Partition, i * count_Requested, count_Requested), startIndex, length);
            });

            for (int i = 0; i < count_Requested; i++)
            {
                indexes[i] = -1;
                distancesSquared[i] = double.PositiveInfinity;
            }

            int count_Filled = 0;
            double worst = double.PositiveInfinity;

            for (int i = 0; i < partitionCount; i++)
            {
                int count_Partition = counts[i];
                for (int j = 0; j < count_Partition; j++)
                {
                    int offset = (i * count_Requested) + j;

                    Modify.InsertNeighbor(indexes, distancesSquared, indexes_Partition[offset], distancesSquared_Partition[offset], ref count_Filled, ref worst);
                }
            }

            return count_Filled;
        }

        /// <summary>
        /// Retrieves the indexes of the points of a contiguous range of a coordinate-major payload closest to a query position, nearest first.
        /// <para>The sweep is vectorised, and the shape of it is what makes it cheap. A lane-wise squared distance is compared against a broadcast of the current rejection radius, and when no lane beats it the whole block is skipped with a single test. Only a block that actually contains a candidate is unpacked lane by lane, and only then is the broadcast rebuilt.</para>
        /// <para>That asymmetry is the point. The radius collapses within the first few hundred points and almost never moves again, so the steady state is a handful of arithmetic operations per block with a perfectly predicted branch, and no per-lane extraction at all. Extraction would otherwise dominate, because the move-mask instruction that makes it cheap lives behind the hardware intrinsics surface, which is not available on this target.</para>
        /// <para>Squared distances are compared throughout. Squaring is monotonic, so every comparison is exact, and the square root is never needed.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="query">The query position, holding one value per axis.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns>The number of neighbours written, or -1 when the input is mismatched or the range is out of bounds.</returns>
        public static int NearestIndexes(this double[][]? coordinates, ReadOnlySpan<double> query, Span<int> indexes, Span<double> distancesSquared, int startIndex, int count)
        {
            if (coordinates == null || startIndex < 0 || count < 0)
            {
                return -1;
            }

            int dimension = coordinates.Length;
            if (dimension < 2 || dimension > 3 || query.Length < dimension)
            {
                return -1;
            }

            int count_Requested = indexes.Length;
            if (count_Requested <= 0 || distancesSquared.Length < count_Requested)
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

            for (int i = 0; i < count_Requested; i++)
            {
                indexes[i] = -1;
                distancesSquared[i] = double.PositiveInfinity;
            }

            if (count == 0)
            {
                return 0;
            }

            double[] values_X = coordinates[0]!;
            double[] values_Y = coordinates[1]!;
            double[] values_Z = dimension == 3 ? coordinates[2]! : values_X;

            double x = query[0];
            double y = query[1];
            double z = dimension == 3 ? query[2] : 0;

            int count_Filled = 0;
            double worst = double.PositiveInfinity;

            int index = startIndex;
            int end = startIndex + count;

            int width = Vector<double>.Count;
            if (Vector.IsHardwareAccelerated && width > 1 && count >= width + width)
            {
                Vector<double> vector_X = new(x);
                Vector<double> vector_Y = new(y);
                Vector<double> vector_Z = new(z);
                Vector<double> vector_Worst = new(worst);

                int end_Vector = end - width;
                for (; index <= end_Vector; index += width)
                {
                    Vector<double> vector_Delta = Vector.Subtract(new Vector<double>(values_X, index), vector_X);
                    Vector<double> vector_DistanceSquared = Vector.Multiply(vector_Delta, vector_Delta);

                    vector_Delta = Vector.Subtract(new Vector<double>(values_Y, index), vector_Y);
                    vector_DistanceSquared = Vector.Add(vector_DistanceSquared, Vector.Multiply(vector_Delta, vector_Delta));

                    if (dimension == 3)
                    {
                        vector_Delta = Vector.Subtract(new Vector<double>(values_Z, index), vector_Z);
                        vector_DistanceSquared = Vector.Add(vector_DistanceSquared, Vector.Multiply(vector_Delta, vector_Delta));
                    }

                    // The overwhelmingly common case: nothing in this block can improve the set.
                    if (Vector.GreaterThanOrEqualAll(vector_DistanceSquared, vector_Worst))
                    {
                        continue;
                    }

                    for (int lane = 0; lane < width; lane++)
                    {
                        Modify.InsertNeighbor(indexes, distancesSquared, index + lane, vector_DistanceSquared[lane], ref count_Filled, ref worst);
                    }

                    vector_Worst = new Vector<double>(worst);
                }
            }

            for (; index < end; index++)
            {
                double dx = values_X[index] - x;
                double dy = values_Y[index] - y;
                double distanceSquared = (dx * dx) + (dy * dy);

                if (dimension == 3)
                {
                    double dz = values_Z[index] - z;
                    distanceSquared += dz * dz;
                }

                if (count_Filled == count_Requested && distanceSquared > worst)
                {
                    continue;
                }

                Modify.InsertNeighbor(indexes, distancesSquared, index, distanceSquared, ref count_Filled, ref worst);
            }

            return count_Filled;
        }
    }
}

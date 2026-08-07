using DiGi.Geometry.PointCloud.Planar.Classes;
using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Create
    {
        /// <summary>
        /// Builds one <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/> per point of a query cloud from the points of a source cloud nearest to it.
        /// <para>This is where a many-core machine earns its keep. A single query is answered by a descent over a few dozen nodes and finishes in microseconds, so parallelising one would cost more in dispatch than the whole search; a batch of queries is a different problem entirely. Each query is independent, reads a shared index that is never written, and writes to its own slot of the result, so there is no shared mutable state, no lock and no contention.</para>
        /// <para>The index is built once before the fan-out. Its lazy construction is thread safe, but arriving at it with every worker at once would leave all but one of them waiting on the lock for the build.</para>
        /// <para>The partitioning uses every processor rather than the fraction the bulk coordinate passes use. Those are limited by memory bandwidth and saturate well before every core is busy; a descent walks a small, cache-resident node table and is bound by latency and arithmetic instead, so it keeps scaling.</para>
        /// <para>The result is aligned with the query cloud, one entry per query point in the same order, holding <see langword="null"/> wherever no non-degenerate triple exists. Compacting the nulls away would break the correspondence that makes the result usable.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to take the corners from.</param>
        /// <param name="pointCloud2D_Query">The cloud of query positions.</param>
        /// <param name="tolerance">The distance below which the third corner counts as lying on the line through the other two.</param>
        /// <returns>A <see cref="List{T}"/> holding one <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/> per query point, or <see langword="null"/> when either cloud is empty.</returns>
        public static List<Triangle2D?>? Triangle2Ds(this PointCloud2D? pointCloud2D, PointCloud2D? pointCloud2D_Query, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates_Query = pointCloud2D_Query?.GetCoordinates(false);
            if (pointCloud2D == null || coordinates_Query == null || coordinates_Query.Length != 2)
            {
                return null;
            }

            double[] values_X = coordinates_Query[0];
            double[] values_Y = coordinates_Query[1];

            int count = values_X.Length;
            if (count == 0)
            {
                return null;
            }

            pointCloud2D.EnsureIndex();

            Triangle2D?[] triangle2Ds = new Triangle2D?[count];

            int partitionCount = Core.Query.PartitionCount(count, Core.Constants.PointCloud.ParallelThresholdNeighbor);
            if (partitionCount <= 1)
            {
                for (int i = 0; i < count; i++)
                {
                    triangle2Ds[i] = Triangle2D(pointCloud2D, values_X[i], values_Y[i], tolerance);
                }

                return [.. triangle2Ds];
            }

            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, i =>
            {
                int startIndex = i * size;

                int end = startIndex + size;
                if (end > count)
                {
                    end = count;
                }

                for (int j = startIndex; j < end; j++)
                {
                    triangle2Ds[j] = Triangle2D(pointCloud2D, values_X[j], values_Y[j], tolerance);
                }
            });

            return [.. triangle2Ds];
        }
    }
}

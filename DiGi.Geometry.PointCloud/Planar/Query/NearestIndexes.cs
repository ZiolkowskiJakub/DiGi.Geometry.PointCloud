using System;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the points of a cloud closest to a query position, nearest first.
        /// <para>Allocation free. The caller owns the result buffers, which are typically stack-allocated.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to search.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <returns>The number of neighbours written, or -1 when the cloud is empty or the request is mismatched.</returns>
        public static int NearestIndexes(this Classes.PointCloud2D? pointCloud2D, double x, double y, Span<int> indexes, Span<double> distancesSquared)
        {
            Span<double> query = stackalloc double[2];

            query[0] = x;
            query[1] = y;

            return Core.Query.NearestIndexes(pointCloud2D, query, indexes, distancesSquared);
        }
    }
}

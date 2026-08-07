using System;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the points of a cloud closest to a query position, nearest first.
        /// <para>Allocation free. The caller owns the result buffers, which are typically stack-allocated.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to search.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="z">The Z coordinate of the query position.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <returns>The number of neighbours written, or -1 when the cloud is empty or the request is mismatched.</returns>
        public static int NearestIndexes(
            this Classes.PointCloud3D? pointCloud3D,
            double x,
            double y,
            double z,
            Span<int> indexes,
            Span<double> distancesSquared)
        {
            Span<double> query = stackalloc double[3];

            query[0] = x;
            query[1] = y;
            query[2] = z;

            return Core.Query.NearestIndexes(pointCloud3D, query, indexes, distancesSquared);
        }
    }
}

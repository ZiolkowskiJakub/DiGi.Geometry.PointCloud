using DiGi.Geometry.Planar.Classes;
using System;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the three points of a cloud closest to a query position.
        /// <para>The whole search allocates nothing. The query is taken as two loose coordinates rather than a <see cref="Point2D"/> so that a caller sweeping many positions never constructs one, and the three results are returned as separate values rather than a collection so that nothing is constructed on the way out either.</para>
        /// <para>Three neighbours are what a triangle needs, which is why this exact arity is worth a dedicated member. Use <see cref="NearestIndexes(Classes.PointCloud2D, double, double, Span{int}, Span{double})"/> for any other count.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to search.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="index_1">When this method returns, contains the index of the closest point.</param>
        /// <param name="index_2">When this method returns, contains the index of the second closest point.</param>
        /// <param name="index_3">When this method returns, contains the index of the third closest point.</param>
        /// <returns><see langword="true"/> when three distinct points were found; otherwise <see langword="false"/>.</returns>
        public static bool TryGetNearestIndexes(this Classes.PointCloud2D? pointCloud2D, double x, double y, out int index_1, out int index_2, out int index_3)
        {
            index_1 = -1;
            index_2 = -1;
            index_3 = -1;

            Span<int> indexes = stackalloc int[3];
            Span<double> distancesSquared = stackalloc double[3];

            if (NearestIndexes(pointCloud2D, x, y, indexes, distancesSquared) != 3)
            {
                return false;
            }

            index_1 = indexes[0];
            index_2 = indexes[1];
            index_3 = indexes[2];

            return true;
        }
    }
}

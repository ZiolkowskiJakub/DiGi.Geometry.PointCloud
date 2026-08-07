using DiGi.Geometry.PointCloud.Planar.Classes;
using DiGi.Geometry.Planar.Classes;
using System;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/> from the points of a cloud nearest to a query point.
        /// </summary>
        /// <param name="pointCloud2D">The cloud to take the corners from.</param>
        /// <param name="point2D">The query point.</param>
        /// <param name="tolerance">The distance below which the third corner counts as lying on the line through the other two.</param>
        /// <returns>A new <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/>, or <see langword="null"/> when the cloud holds too few points or offers no non-degenerate triple.</returns>
        public static Triangle2D? Triangle2D(this PointCloud2D? pointCloud2D, Point2D? point2D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            if (pointCloud2D == null || point2D == null)
            {
                return null;
            }

            return Triangle2D(pointCloud2D, point2D.X, point2D.Y, tolerance);
        }

        /// <summary>
        /// Builds a <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/> from the points of a cloud nearest to a query position.
        /// <para>The three nearest points are taken first, and are used as they are whenever they form a usable triangle. They frequently do not: a query sitting on a scan line or a grid line of the source data has three nearest points that are exactly collinear, and three collinear points enclose no area. Rather than fail there, the search collects <see cref="Core.Constants.PointCloud.MaximumNeighborCandidateCount"/> neighbours in one traversal and steps through the pairs beyond the first until a triple stands clear of a line. Those extra neighbours come from leaves the traversal already visited, so the widening costs a handful of comparisons and no second search.</para>
        /// <para>The nearest point always stays as a corner. Any candidate that would displace it is a duplicate of it, which is interchangeable, so anchoring there costs nothing and keeps the triangle attached to the point the caller actually asked about.</para>
        /// <para>Everything up to the result allocates nothing: the candidate set is stack-allocated and the corners are selected from raw coordinates. The three <see cref="Point2D"/> objects are created only once a triangle is known to exist.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to take the corners from.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="tolerance">The distance below which the third corner counts as lying on the line through the other two.</param>
        /// <returns>A new <see cref="DiGi.Geometry.Planar.Classes.Triangle2D"/>, or <see langword="null"/> when the cloud holds too few points or offers no non-degenerate triple.</returns>
        public static Triangle2D? Triangle2D(this PointCloud2D? pointCloud2D, double x, double y, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud2D?.GetCoordinates(false);
            if (coordinates == null || coordinates.Length != 2)
            {
                return null;
            }

            Span<int> indexes = stackalloc int[Core.Constants.PointCloud.MaximumNeighborCandidateCount];
            Span<double> distancesSquared = stackalloc double[Core.Constants.PointCloud.MaximumNeighborCandidateCount];

            int count = Query.NearestIndexes(pointCloud2D, x, y, indexes, distancesSquared);
            if (count < 3)
            {
                return null;
            }

            double[] values_X = coordinates[0];
            double[] values_Y = coordinates[1];

            double toleranceSquared = tolerance * tolerance;

            // A triple is usable when its third corner stands clear of the line through the other two.
            // That perpendicular distance is the magnitude of the two-dimensional cross product divided
            // by the length of the first edge, so comparing the squared cross product against the
            // squared edge length scaled by the tolerance tests it exactly, without a square root and
            // without the result depending on how large the triangle happens to be.
            bool usable(int index_1, int index_2, int index_3)
            {
                double x_1 = values_X[index_1];
                double y_1 = values_Y[index_1];

                double dx_1 = values_X[index_2] - x_1;
                double dy_1 = values_Y[index_2] - y_1;

                double lengthSquared = (dx_1 * dx_1) + (dy_1 * dy_1);
                if (lengthSquared <= toleranceSquared)
                {
                    return false;
                }

                double dx_2 = values_X[index_3] - x_1;
                double dy_2 = values_Y[index_3] - y_1;

                double cross = (dx_1 * dy_2) - (dy_1 * dx_2);

                return cross * cross > toleranceSquared * lengthSquared;
            }

            int index_A = indexes[0];

            for (int j = 1; j < count; j++)
            {
                int index_B = indexes[j];

                for (int k = j + 1; k < count; k++)
                {
                    int index_C = indexes[k];

                    if (!usable(index_A, index_B, index_C))
                    {
                        continue;
                    }

                    return new Triangle2D(
                        new Point2D(values_X[index_A], values_Y[index_A]),
                        new Point2D(values_X[index_B], values_Y[index_B]),
                        new Point2D(values_X[index_C], values_Y[index_C]));
                }
            }

            return null;
        }
    }
}

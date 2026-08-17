using DiGi.Geometry.PointCloud.Spatial.Classes;
using DiGi.Geometry.Spatial.Classes;
using System;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a <see cref="Geometry.Spatial.Classes.Triangle3D"/> from the points of a cloud nearest to a query point.
        /// </summary>
        /// <param name="pointCloud3D">The cloud to take the corners from.</param>
        /// <param name="point3D">The query point.</param>
        /// <param name="tolerance">The distance below which the third corner counts as lying on the line through the other two.</param>
        /// <returns>A new <see cref="Geometry.Spatial.Classes.Triangle3D"/>, or <see langword="null"/> when the cloud holds too few points or offers no non-degenerate triple.</returns>
        public static Triangle3D? Triangle3D(this PointCloud3D? pointCloud3D, Point3D? point3D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            if (pointCloud3D == null || point3D == null)
            {
                return null;
            }

            return Triangle3D(pointCloud3D, point3D.X, point3D.Y, point3D.Z, tolerance);
        }

        /// <summary>
        /// Builds a <see cref="Geometry.Spatial.Classes.Triangle3D"/> from the points of a cloud nearest to a query position.
        /// <para>The three nearest points are taken first, and are used as they are whenever they form a usable triangle. They frequently do not: a query sitting on a scan line or a grid line of the source data has three nearest points that are exactly collinear, and three collinear points describe no plane. Rather than fail there, the search collects <see cref="Core.Constants.PointCloud.MaximumNeighborCandidateCount"/> neighbours in one traversal and steps through the pairs beyond the first until a triple stands clear of a line. Those extra neighbours come from leaves the traversal already visited, so the widening costs a handful of comparisons and no second search.</para>
        /// <para>The nearest point always stays as a corner. Any candidate that would displace it is a duplicate of it, which is interchangeable, so anchoring there costs nothing and keeps the triangle attached to the point the caller actually asked about.</para>
        /// <para>Everything up to the result allocates nothing: the candidate set is stack-allocated and the corners are selected from raw coordinates. The three <see cref="Point3D"/> objects are created only once a triangle is known to exist.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to take the corners from.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="z">The Z coordinate of the query position.</param>
        /// <param name="tolerance">The distance below which the third corner counts as lying on the line through the other two.</param>
        /// <returns>A new <see cref="Geometry.Spatial.Classes.Triangle3D"/>, or <see langword="null"/> when the cloud holds too few points or offers no non-degenerate triple.</returns>
        public static Triangle3D? Triangle3D(this PointCloud3D? pointCloud3D, double x, double y, double z, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
            if (coordinates == null || coordinates.Length != 3)
            {
                return null;
            }

            Span<int> indexes = stackalloc int[Core.Constants.PointCloud.MaximumNeighborCandidateCount];
            Span<double> distancesSquared = stackalloc double[Core.Constants.PointCloud.MaximumNeighborCandidateCount];

            int count = Query.NearestIndexes(pointCloud3D, x, y, z, indexes, distancesSquared);
            if (count < 3)
            {
                return null;
            }

            double[] values_X = coordinates[0];
            double[] values_Y = coordinates[1];
            double[] values_Z = coordinates[2];

            double toleranceSquared = tolerance * tolerance;

            // A triple is usable when its third corner stands clear of the line through the other two.
            // That perpendicular distance is the length of the cross product divided by the length of
            // the first edge, so comparing the squared cross product against the squared edge length
            // scaled by the tolerance tests it exactly, without a square root and without the result
            // depending on how large the triangle happens to be.
            bool usable(int index_1, int index_2, int index_3)
            {
                double x_1 = values_X[index_1];
                double y_1 = values_Y[index_1];
                double z_1 = values_Z[index_1];

                double dx_1 = values_X[index_2] - x_1;
                double dy_1 = values_Y[index_2] - y_1;
                double dz_1 = values_Z[index_2] - z_1;

                double lengthSquared = (dx_1 * dx_1) + (dy_1 * dy_1) + (dz_1 * dz_1);
                if (lengthSquared <= toleranceSquared)
                {
                    return false;
                }

                double dx_2 = values_X[index_3] - x_1;
                double dy_2 = values_Y[index_3] - y_1;
                double dz_2 = values_Z[index_3] - z_1;

                double normalX = (dy_1 * dz_2) - (dz_1 * dy_2);
                double normalY = (dz_1 * dx_2) - (dx_1 * dz_2);
                double normalZ = (dx_1 * dy_2) - (dy_1 * dx_2);

                double normalLengthSquared = (normalX * normalX) + (normalY * normalY) + (normalZ * normalZ);

                return normalLengthSquared > toleranceSquared * lengthSquared;
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

                    return new Triangle3D(
                        new Point3D(values_X[index_A], values_Y[index_A], values_Z[index_A]),
                        new Point3D(values_X[index_B], values_Y[index_B], values_Z[index_B]),
                        new Point3D(values_X[index_C], values_Y[index_C], values_Z[index_C]));
                }
            }

            return null;
        }
    }
}

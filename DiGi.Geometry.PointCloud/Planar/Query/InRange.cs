using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Query
    {
        /// <summary>
        /// Filters a cloud down to the points that fall inside an axis-aligned box.
        /// <para>No <see cref="Point2D"/> object is created anywhere on this path. The result is built directly as coordinate arrays and handed to the adopting constructor.</para>
        /// <para>The tolerance is folded into the bounds once, before the scan, so the result agrees exactly with <see cref="BoundingBox2D.InRange(Point2D, double)"/> applied point by point.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to filter.</param>
        /// <param name="boundingBox2D">The box to filter against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/> holding the points inside the box, or <see langword="null"/> when nothing qualifies.</returns>
        public static Classes.PointCloud2D? InRange(this Classes.PointCloud2D? pointCloud2D, BoundingBox2D? boundingBox2D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud2D?.GetCoordinates(false);
            if (coordinates == null || boundingBox2D == null)
            {
                return null;
            }

            double[] minimums = Minimums(boundingBox2D, tolerance);
            double[] maximums = Maximums(boundingBox2D, tolerance);

            // When the query box already encloses the whole cloud the answer is a copy, and a copy is a
            // block move rather than a scan. This turns the worst case of the filter into its cheapest case.
            BoundingBox2D? boundingBox2D_Cloud = pointCloud2D!.GetBoundingBox();
            if (boundingBox2D_Cloud != null)
            {
                // Hoisted: the corner properties construct a fresh point on every read, so comparing
                // boundingBox2D_Cloud.Min.X directly would allocate once per comparison.
                Point2D point2D_Min = boundingBox2D_Cloud.Min;
                Point2D point2D_Max = boundingBox2D_Cloud.Max;

                if (point2D_Min.X >= minimums[0] && point2D_Max.X <= maximums[0]
                    && point2D_Min.Y >= minimums[1] && point2D_Max.Y <= maximums[1])
                {
                    return new Classes.PointCloud2D(pointCloud2D);
                }
            }

            // Above the index threshold the hierarchy is worth building: a fully contained node contributes
            // its whole range with no per-point test, so the work becomes proportional to the answer rather
            // than to the cloud. Below it, an exhaustive vectorised scan is cheaper than any build.
            Core.Classes.PointCloudIndex? pointCloudIndex = pointCloud2D!.EnsureIndex();
            if (pointCloudIndex != null)
            {
                int[]? indexes = pointCloudIndex.InRangeIndexes(coordinates, minimums, maximums);
                if (indexes == null || indexes.Length == 0)
                {
                    return null;
                }

                double[] x = new double[indexes.Length];
                double[] y = new double[indexes.Length];

                double[] x_Source = coordinates[0];
                double[] y_Source = coordinates[1];

                for (int i = 0; i < indexes.Length; i++)
                {
                    int index = indexes[i];

                    x[i] = x_Source[index];
                    y[i] = y_Source[index];
                }

                return new Classes.PointCloud2D(x, y, false);
            }

            double[][]? coordinates_InRange = Core.Create.CoordinatesInRange(coordinates, minimums, maximums);
            if (coordinates_InRange == null)
            {
                return null;
            }

            return new Classes.PointCloud2D(coordinates_InRange[0], coordinates_InRange[1], false);
        }

        /// <summary>
        /// Counts the points of a cloud that fall inside an axis-aligned box, without materializing them.
        /// </summary>
        /// <param name="pointCloud2D">The cloud to test.</param>
        /// <param name="boundingBox2D">The box to test against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>The number of points inside the box, or -1 when the cloud or box is null.</returns>
        public static int InRangeCount(this Classes.PointCloud2D? pointCloud2D, BoundingBox2D? boundingBox2D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud2D?.GetCoordinates(false);
            if (coordinates == null || boundingBox2D == null)
            {
                return -1;
            }

            return Core.Query.InRangeCount(coordinates, Minimums(boundingBox2D, tolerance), Maximums(boundingBox2D, tolerance));
        }

        /// <summary>
        /// Retrieves the indexes of the points of a cloud that fall inside an axis-aligned box.
        /// </summary>
        /// <param name="pointCloud2D">The cloud to test.</param>
        /// <param name="boundingBox2D">The box to test against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>A <see cref="List{T}"/> of zero-based point indexes, or <see langword="null"/> when the cloud or box is null.</returns>
        public static List<int>? InRangeIndexes(this Classes.PointCloud2D? pointCloud2D, BoundingBox2D? boundingBox2D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud2D?.GetCoordinates(false);
            if (coordinates == null || boundingBox2D == null)
            {
                return null;
            }

            double[] minimums = Minimums(boundingBox2D, tolerance);
            double[] maximums = Maximums(boundingBox2D, tolerance);

            Core.Classes.PointCloudIndex? pointCloudIndex = pointCloud2D!.EnsureIndex();
            if (pointCloudIndex != null)
            {
                int[]? indexes = pointCloudIndex.InRangeIndexes(coordinates, minimums, maximums);
                if (indexes != null)
                {
                    return [.. indexes];
                }
            }

            double[] x = coordinates[0];
            double[] y = coordinates[1];

            List<int> result = [];
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < minimums[0] || x[i] > maximums[0] || y[i] < minimums[1] || y[i] > maximums[1])
                {
                    continue;
                }

                result.Add(i);
            }

            return result;
        }

        /// <summary>
        /// Produces the per-axis lower bounds of a box widened by a tolerance.
        /// </summary>
        /// <param name="boundingBox2D">The box to read.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis.</param>
        /// <returns>A two element <see cref="double"/> array holding the widened lower bounds.</returns>
        public static double[] Minimums(this BoundingBox2D boundingBox2D, double tolerance)
        {
            // Hoisted into a local: the corner property constructs a fresh point on every read.
            Point2D point2D = boundingBox2D.Min;

            return [point2D.X - tolerance, point2D.Y - tolerance];
        }

        /// <summary>
        /// Produces the per-axis upper bounds of a box widened by a tolerance.
        /// </summary>
        /// <param name="boundingBox2D">The box to read.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis.</param>
        /// <returns>A two element <see cref="double"/> array holding the widened upper bounds.</returns>
        public static double[] Maximums(this BoundingBox2D boundingBox2D, double tolerance)
        {
            // Hoisted into a local: the corner property constructs a fresh point on every read.
            Point2D point2D = boundingBox2D.Max;

            return [point2D.X + tolerance, point2D.Y + tolerance];
        }
    }
}

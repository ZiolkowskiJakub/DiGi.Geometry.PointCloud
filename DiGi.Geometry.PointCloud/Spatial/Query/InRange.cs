using DiGi.Geometry.Spatial.Classes;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Query
    {
        /// <summary>
        /// Filters a cloud down to the points that fall inside an axis-aligned box.
        /// <para>No <see cref="Point3D"/> object is created anywhere on this path. The result is built directly as coordinate arrays and handed to the adopting constructor, so filtering a cloud of ten million points allocates three arrays and nothing else.</para>
        /// <para>The tolerance is folded into the bounds once, before the scan, so the result agrees exactly with <see cref="BoundingBox3D.InRange(Point3D, double)"/> applied point by point.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to filter.</param>
        /// <param name="boundingBox3D">The box to filter against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/> holding the points inside the box, or <see langword="null"/> when nothing qualifies.</returns>
        public static Classes.PointCloud3D? InRange(this Classes.PointCloud3D? pointCloud3D, BoundingBox3D? boundingBox3D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
            if (coordinates == null || boundingBox3D == null)
            {
                return null;
            }

            double[] minimums = Minimums(boundingBox3D, tolerance);
            double[] maximums = Maximums(boundingBox3D, tolerance);

            // When the query box already encloses the whole cloud the answer is a copy, and a copy is a
            // block move rather than a scan. This turns the worst case of the filter into its cheapest case.
            BoundingBox3D? boundingBox3D_Cloud = pointCloud3D!.GetBoundingBox();
            if (boundingBox3D_Cloud != null
                && boundingBox3D_Cloud.MinX >= minimums[0] && boundingBox3D_Cloud.MaxX <= maximums[0]
                && boundingBox3D_Cloud.MinY >= minimums[1] && boundingBox3D_Cloud.MaxY <= maximums[1]
                && boundingBox3D_Cloud.MinZ >= minimums[2] && boundingBox3D_Cloud.MaxZ <= maximums[2])
            {
                return new Classes.PointCloud3D(pointCloud3D);
            }

            // Above the index threshold the hierarchy is worth building: a fully contained node contributes
            // its whole range with no per-point test, so the work becomes proportional to the answer rather
            // than to the cloud. Below it, an exhaustive vectorised scan is cheaper than any build.
            Core.Classes.PointCloudIndex? pointCloudIndex = pointCloud3D!.EnsureIndex();
            if (pointCloudIndex != null)
            {
                int[]? indexes = pointCloudIndex.InRangeIndexes(coordinates, minimums, maximums);
                if (indexes == null || indexes.Length == 0)
                {
                    return null;
                }

                double[] x = new double[indexes.Length];
                double[] y = new double[indexes.Length];
                double[] z = new double[indexes.Length];

                double[] x_Source = coordinates[0];
                double[] y_Source = coordinates[1];
                double[] z_Source = coordinates[2];

                for (int i = 0; i < indexes.Length; i++)
                {
                    int index = indexes[i];

                    x[i] = x_Source[index];
                    y[i] = y_Source[index];
                    z[i] = z_Source[index];
                }

                return new Classes.PointCloud3D(x, y, z, false);
            }

            double[][]? coordinates_InRange = Core.Create.CoordinatesInRange(coordinates, minimums, maximums);
            if (coordinates_InRange == null)
            {
                return null;
            }

            return new Classes.PointCloud3D(coordinates_InRange[0], coordinates_InRange[1], coordinates_InRange[2], false);
        }

        /// <summary>
        /// Filters a cloud that carries per-point model object links down to the points that fall inside an axis-aligned box, carrying the links with them.
        /// <para>This overload exists because extension methods bind statically. Without it a filtered cloud would come back as a plain <see cref="Classes.PointCloud3D"/> and the links would be gone; with it, the links survive as long as the variable is typed as the referenced cloud at the call site.</para>
        /// <para>The points and their identifiers are compacted by ONE permutation, obtained from <see cref="InRangeIndexes(Classes.PointCloud3D, BoundingBox3D, double)"/>. Gathering them separately is what would let them drift apart, and a cloud whose identifiers are offset by one looks entirely healthy while attributing every point to the wrong model object.</para>
        /// <para>The reference table is shared with the source rather than copied, which is safe because the table has no mutating members and identifiers stay valid under filtering. An entry that keeps no points simply goes unused.</para>
        /// </summary>
        /// <param name="referencedPointCloud3D">The cloud to filter.</param>
        /// <param name="boundingBox3D">The box to filter against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/> holding the points inside the box, or <see langword="null"/> when nothing qualifies.</returns>
        public static Classes.ReferencedPointCloud3D? InRange(this Classes.ReferencedPointCloud3D? referencedPointCloud3D, BoundingBox3D? boundingBox3D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = referencedPointCloud3D?.GetCoordinates(false);
            if (coordinates == null || boundingBox3D == null)
            {
                return null;
            }

            double[] minimums = Minimums(boundingBox3D, tolerance);
            double[] maximums = Maximums(boundingBox3D, tolerance);

            // The same early-out the base filter makes, and correct for the same reason: the copy constructor
            // deep-copies the identifiers and the table, so the copy shares nothing with the source.
            BoundingBox3D? boundingBox3D_Cloud = referencedPointCloud3D!.GetBoundingBox();
            if (boundingBox3D_Cloud != null
                && boundingBox3D_Cloud.MinX >= minimums[0] && boundingBox3D_Cloud.MaxX <= maximums[0]
                && boundingBox3D_Cloud.MinY >= minimums[1] && boundingBox3D_Cloud.MaxY <= maximums[1]
                && boundingBox3D_Cloud.MinZ >= minimums[2] && boundingBox3D_Cloud.MaxZ <= maximums[2])
            {
                return new Classes.ReferencedPointCloud3D(referencedPointCloud3D);
            }

            List<int>? indexes = InRangeIndexes(referencedPointCloud3D, boundingBox3D, tolerance);
            if (indexes == null || indexes.Count == 0)
            {
                return null;
            }

            double[][]? coordinates_InRange = Core.Create.GatheredCoordinates(coordinates, indexes);
            if (coordinates_InRange == null)
            {
                return null;
            }

            int[]? referenceIndexes = Core.Create.GatheredReferenceIndexes(referencedPointCloud3D.GetReferenceIndexes(false), indexes);

            return new Classes.ReferencedPointCloud3D(coordinates_InRange[0], coordinates_InRange[1], coordinates_InRange[2], referenceIndexes, referencedPointCloud3D.GetPointCloudReferenceCollection(false), false);
        }

        /// <summary>
        /// Counts the points of a cloud that fall inside an axis-aligned box, without materializing them.
        /// <para>Useful for sizing a buffer once when a caller intends to filter repeatedly, which avoids repeatedly allocating and discarding large object heap arrays.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to test.</param>
        /// <param name="boundingBox3D">The box to test against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>The number of points inside the box, or -1 when the cloud or box is null.</returns>
        public static int InRangeCount(this Classes.PointCloud3D? pointCloud3D, BoundingBox3D? boundingBox3D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
            if (coordinates == null || boundingBox3D == null)
            {
                return -1;
            }

            return Core.Query.InRangeCount(coordinates, Minimums(boundingBox3D, tolerance), Maximums(boundingBox3D, tolerance));
        }

        /// <summary>
        /// Retrieves the indexes of the points of a cloud that fall inside an axis-aligned box.
        /// </summary>
        /// <param name="pointCloud3D">The cloud to test.</param>
        /// <param name="boundingBox3D">The box to test against.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis before testing.</param>
        /// <returns>A <see cref="List{T}"/> of zero-based point indexes, or <see langword="null"/> when the cloud or box is null.</returns>
        public static List<int>? InRangeIndexes(this Classes.PointCloud3D? pointCloud3D, BoundingBox3D? boundingBox3D, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
            if (coordinates == null || boundingBox3D == null)
            {
                return null;
            }

            double[] minimums = Minimums(boundingBox3D, tolerance);
            double[] maximums = Maximums(boundingBox3D, tolerance);

            Core.Classes.PointCloudIndex? pointCloudIndex = pointCloud3D!.EnsureIndex();
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
            double[] z = coordinates[2];

            List<int> result = [];
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < minimums[0] || x[i] > maximums[0] || y[i] < minimums[1] || y[i] > maximums[1] || z[i] < minimums[2] || z[i] > maximums[2])
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
        /// <param name="boundingBox3D">The box to read.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis.</param>
        /// <returns>A three element <see cref="double"/> array holding the widened lower bounds.</returns>
        public static double[] Minimums(this BoundingBox3D boundingBox3D, double tolerance)
        {
            return [boundingBox3D.MinX - tolerance, boundingBox3D.MinY - tolerance, boundingBox3D.MinZ - tolerance];
        }

        /// <summary>
        /// Produces the per-axis upper bounds of a box widened by a tolerance.
        /// </summary>
        /// <param name="boundingBox3D">The box to read.</param>
        /// <param name="tolerance">The distance by which the box is widened on every axis.</param>
        /// <returns>A three element <see cref="double"/> array holding the widened upper bounds.</returns>
        public static double[] Maximums(this BoundingBox3D boundingBox3D, double tolerance)
        {
            return [boundingBox3D.MaxX + tolerance, boundingBox3D.MaxY + tolerance, boundingBox3D.MaxZ + tolerance];
        }
    }
}

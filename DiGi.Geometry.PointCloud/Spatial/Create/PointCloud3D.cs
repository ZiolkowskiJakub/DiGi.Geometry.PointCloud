using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.PointCloud.Spatial.Classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a <see cref="Classes.PointCloud3D"/> from a sequence of points, dropping null entries and points with a non-finite coordinate.
        /// <para>The filtering lives here rather than in the constructor because it is an order-of-count sweep over the input, and a caller holding data that is already clean should not pay for it. See <see cref="Core.Create.FiniteCoordinates(double[][])"/> for why non-finite coordinates must not reach the vectorised paths.</para>
        /// </summary>
        /// <param name="point3Ds">The points to store.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the input is null or holds no usable point.</returns>
        public static PointCloud3D? PointCloud3D(this IEnumerable<Point3D?>? point3Ds)
        {
            if (point3Ds == null)
            {
                return null;
            }

            PointCloud3D pointCloud3D = new(point3Ds);

            return PointCloud3D(pointCloud3D.GetCoordinates(false));
        }

        /// <summary>
        /// Creates a single <see cref="Classes.PointCloud3D"/> holding every point of the supplied clouds, in the order the clouds are given.
        /// <para>Null and empty clouds in the sequence are skipped. Points with a non-finite coordinate are dropped, as they are by every other overload here.</para>
        /// <para>No <see cref="Point3D"/> object is created anywhere on this path: the sources are read through <see cref="Core.Classes.PointCloud.GetCoordinates(bool)"/> without cloning and block copied into arrays allocated once at the combined size.</para>
        /// <para>IMPORTANT: the result is a plain cloud. A <see cref="Classes.ReferencedPointCloud3D"/> passed in here comes back with its per-point model object links gone, and because extension methods bind statically nothing at the call site warns about it. Merging referenced clouds means merging their reference tables and renumbering their identifiers; there is deliberately no overload for it.</para>
        /// </summary>
        /// <param name="pointCloud3Ds">The clouds to concatenate.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the input is null or holds no usable point.</returns>
        public static PointCloud3D? PointCloud3D(this IEnumerable<PointCloud3D?>? pointCloud3Ds)
        {
            if (pointCloud3Ds == null)
            {
                return null;
            }

            List<double[][]> coordinates_Sources = [];

            int count = 0;
            foreach (PointCloud3D? pointCloud3D in pointCloud3Ds)
            {
                double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
                if (coordinates == null || coordinates.Length != 3)
                {
                    continue;
                }

                int count_Source = coordinates[0].Length;
                if (count_Source == 0)
                {
                    continue;
                }

                coordinates_Sources.Add(coordinates);
                count += count_Source;
            }

            if (count == 0)
            {
                return null;
            }

            double[] x = new double[count];
            double[] y = new double[count];
            double[] z = new double[count];

            int index = 0;
            foreach (double[][] coordinates in coordinates_Sources)
            {
                int count_Source = coordinates[0].Length;

                Array.Copy(coordinates[0], 0, x, index, count_Source);
                Array.Copy(coordinates[1], 0, y, index, count_Source);
                Array.Copy(coordinates[2], 0, z, index, count_Source);

                index += count_Source;
            }

            return PointCloud3D([x, y, z]);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud3D"/> from three coordinate arrays, dropping points with a non-finite coordinate.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="z">The Z coordinates.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the arrays are null, of unequal length, or hold no finite point.</returns>
        public static PointCloud3D? PointCloud3D(double[]? x, double[]? y, double[]? z)
        {
            if (x == null || y == null || z == null)
            {
                return null;
            }

            return PointCloud3D([x, y, z]);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud3D"/> from coordinate arrays, dropping points with a non-finite coordinate.
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length. Exactly three axes are required.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the input is null, not three-dimensional, ragged, or holds no finite point.</returns>
        public static PointCloud3D? PointCloud3D(double[][]? coordinates)
        {
            if (coordinates == null || coordinates.Length != 3)
            {
                return null;
            }

            double[][]? coordinates_Finite = Core.Create.FiniteCoordinates(coordinates);
            if (coordinates_Finite == null)
            {
                return null;
            }

            return new PointCloud3D(coordinates_Finite[0], coordinates_Finite[1], coordinates_Finite[2], false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud3D"/> from a buffer in the binary point cloud format.
        /// <para>Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields <see langword="null"/>.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the buffer could not be decoded.</returns>
        public static PointCloud3D? PointCloud3D(byte[]? bytes)
        {
            double[][]? coordinates = Core.Create.Coordinates(bytes, 3);
            if (coordinates == null)
            {
                return null;
            }

            return new PointCloud3D(coordinates[0], coordinates[1], coordinates[2], false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud3D"/> by reading a file written in the binary point cloud format.
        /// <para>Never throws. A missing or unreadable file yields <see langword="null"/>.</para>
        /// </summary>
        /// <param name="fileInfo">The file to read.</param>
        /// <returns>A new <see cref="Classes.PointCloud3D"/>, or <see langword="null"/> when the file could not be read or decoded.</returns>
        public static PointCloud3D? PointCloud3D(this FileInfo? fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                return null;
            }

            byte[] bytes;
            try
            {
                bytes = File.ReadAllBytes(fileInfo.FullName);
            }
            catch (IOException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }

            return PointCloud3D(bytes);
        }
    }
}

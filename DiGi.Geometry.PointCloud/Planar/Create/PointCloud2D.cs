using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.PointCloud.Planar.Classes;
using System.Collections.Generic;
using System.IO;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a <see cref="Classes.PointCloud2D"/> from a sequence of points, dropping null entries and points with a non-finite coordinate.
        /// <para>The filtering lives here rather than in the constructor because it is an order-of-count sweep over the input, and a caller holding data that is already clean should not pay for it. See <see cref="Core.Create.FiniteCoordinates(double[][])"/> for why non-finite coordinates must not reach the vectorised paths.</para>
        /// </summary>
        /// <param name="point2Ds">The points to store.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/>, or <see langword="null"/> when the input is null or holds no usable point.</returns>
        public static PointCloud2D? PointCloud2D(this IEnumerable<Point2D?>? point2Ds)
        {
            if (point2Ds == null)
            {
                return null;
            }

            PointCloud2D pointCloud2D = new(point2Ds);

            return PointCloud2D(pointCloud2D.GetCoordinates(false));
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud2D"/> from two coordinate arrays, dropping points with a non-finite coordinate.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/>, or <see langword="null"/> when the arrays are null, of unequal length, or hold no finite point.</returns>
        public static PointCloud2D? PointCloud2D(double[]? x, double[]? y)
        {
            if (x == null || y == null)
            {
                return null;
            }

            return PointCloud2D([x, y]);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud2D"/> from coordinate arrays, dropping points with a non-finite coordinate.
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length. Exactly two axes are required.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/>, or <see langword="null"/> when the input is null, not two-dimensional, ragged, or holds no finite point.</returns>
        public static PointCloud2D? PointCloud2D(double[][]? coordinates)
        {
            if (coordinates == null || coordinates.Length != 2)
            {
                return null;
            }

            double[][]? coordinates_Finite = Core.Create.FiniteCoordinates(coordinates);
            if (coordinates_Finite == null)
            {
                return null;
            }

            return new PointCloud2D(coordinates_Finite[0], coordinates_Finite[1], false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud2D"/> from a buffer in the binary point cloud format.
        /// <para>Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields <see langword="null"/>.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/>, or <see langword="null"/> when the buffer could not be decoded.</returns>
        public static PointCloud2D? PointCloud2D(byte[]? bytes)
        {
            double[][]? coordinates = Core.Create.Coordinates(bytes, 2);
            if (coordinates == null)
            {
                return null;
            }

            return new PointCloud2D(coordinates[0], coordinates[1], false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.PointCloud2D"/> by reading a file written in the binary point cloud format.
        /// <para>Never throws. A missing or unreadable file yields <see langword="null"/>.</para>
        /// </summary>
        /// <param name="fileInfo">The file to read.</param>
        /// <returns>A new <see cref="Classes.PointCloud2D"/>, or <see langword="null"/> when the file could not be read or decoded.</returns>
        public static PointCloud2D? PointCloud2D(this FileInfo? fileInfo)
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
            catch (System.UnauthorizedAccessException)
            {
                return null;
            }

            return PointCloud2D(bytes);
        }
    }
}

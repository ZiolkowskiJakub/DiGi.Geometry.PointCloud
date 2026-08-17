using DiGi.Core.Interfaces;
using DiGi.Geometry.PointCloud.Spatial.Classes;
using DiGi.Geometry.Spatial.Classes;
using System.Collections.Generic;
using System.IO;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a <see cref="Classes.ReferencedPointCloud3D"/> from a sequence of points and the model object each one belongs to, dropping null entries and points with a non-finite coordinate.
        /// <para>The two sequences are consumed in lockstep, so a null point discards its reference with it. This is why the cloud cannot simply be built from the point constructor and then handed a separate array: that constructor drops null points silently, which would shift every later reference onto the wrong point.</para>
        /// <para>The reference table and the identifiers are built together, and the non-finite filter is applied to both by one permutation, so the two halves cannot come out of step.</para>
        /// </summary>
        /// <param name="point3Ds">The points to store.</param>
        /// <param name="references">The model object each point belongs to, in the same order. A null entry marks a point that links to nothing.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/>, or <see langword="null"/> when either input is null or no usable point remains.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(this IEnumerable<Point3D?>? point3Ds, IEnumerable<ISerializableReference>? references)
        {
            if (point3Ds == null || references == null)
            {
                return null;
            }

            List<double> x = [];
            List<double> y = [];
            List<double> z = [];

            List<ISerializableReference> references_Paired = [];

            using IEnumerator<ISerializableReference> enumerator = references.GetEnumerator();
            foreach (Point3D? point3D in point3Ds)
            {
                ISerializableReference? reference = enumerator.MoveNext() ? enumerator.Current : null;
                if (point3D == null)
                {
                    continue;
                }

                x.Add(point3D.X);
                y.Add(point3D.Y);
                z.Add(point3D.Z);

                references_Paired.Add(reference!);
            }

            if (x.Count == 0)
            {
                return null;
            }

            Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection = Core.Create.PointCloudReferenceCollection(references_Paired, out int[]? referenceIndexes);

            return ReferencedPointCloud3D([[.. x], [.. y], [.. z]], referenceIndexes, pointCloudReferenceCollection);
        }

        /// <summary>
        /// Creates a <see cref="Classes.ReferencedPointCloud3D"/> from three coordinate arrays and their per-point identifiers, dropping points with a non-finite coordinate.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="z">The Z coordinates.</param>
        /// <param name="referenceIndexes">The per-point identifiers, one per point, where -1 marks a point that links to nothing.</param>
        /// <param name="pointCloudReferenceCollection">The reference table the identifiers index into.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/>, or <see langword="null"/> when the arrays are null, of unequal length, or hold no finite point.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection)
        {
            if (x == null || y == null || z == null)
            {
                return null;
            }

            return ReferencedPointCloud3D([x, y, z], referenceIndexes, pointCloudReferenceCollection);
        }

        /// <summary>
        /// Creates a <see cref="Classes.ReferencedPointCloud3D"/> from coordinate arrays and their per-point identifiers, dropping points with a non-finite coordinate.
        /// <para>The filter is expressed as a permutation and applied to the coordinates and the identifiers by the same gather, which is what keeps a point and its model object together across a change of point count.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length. Exactly three axes are required.</param>
        /// <param name="referenceIndexes">The per-point identifiers, one per point, where -1 marks a point that links to nothing.</param>
        /// <param name="pointCloudReferenceCollection">The reference table the identifiers index into.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/>, or <see langword="null"/> when the input is null, not three-dimensional, ragged, or holds no finite point.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(double[][]? coordinates, int[]? referenceIndexes, Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection)
        {
            if (coordinates == null || coordinates.Length != 3)
            {
                return null;
            }

            int[]? indexes = Core.Create.FiniteIndexes(coordinates);
            if (indexes == null)
            {
                return null;
            }

            double[][]? coordinates_Finite = Core.Create.GatheredCoordinates(coordinates, indexes);
            if (coordinates_Finite == null)
            {
                return null;
            }

            int[]? referenceIndexes_Finite = Core.Create.GatheredReferenceIndexes(referenceIndexes, indexes);

            return new ReferencedPointCloud3D(coordinates_Finite[0], coordinates_Finite[1], coordinates_Finite[2], referenceIndexes_Finite, pointCloudReferenceCollection, false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.ReferencedPointCloud3D"/> from a buffer holding a coordinate block optionally followed by an identifier block.
        /// <para>Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields <see langword="null"/>, and a buffer holding coordinates alone yields a cloud that carries no links.</para>
        /// </summary>
        /// <param name="bytes">The encoded buffer.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/>, or <see langword="null"/> when the buffer could not be decoded.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(byte[]? bytes)
        {
            int length = Core.Query.BinaryLength(bytes, 0);
            if (length < 0)
            {
                return null;
            }

            double[][]? coordinates = Core.Create.Coordinates(bytes, 3, 0);
            if (coordinates == null)
            {
                return null;
            }

            int[]? referenceIndexes = null;
            Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection = null;

            if (length < bytes!.Length)
            {
                referenceIndexes = Core.Create.ReferenceIndexes(bytes, length);
                pointCloudReferenceCollection = Core.Create.PointCloudReferenceCollection(bytes, length);
            }

            return new ReferencedPointCloud3D(coordinates[0], coordinates[1], coordinates[2], referenceIndexes, pointCloudReferenceCollection, false);
        }

        /// <summary>
        /// Creates a <see cref="Classes.ReferencedPointCloud3D"/> by reading a file holding a coordinate block optionally followed by an identifier block.
        /// <para>Never throws. A missing or unreadable file yields <see langword="null"/>.</para>
        /// </summary>
        /// <param name="fileInfo">The file to read.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/>, or <see langword="null"/> when the file could not be read or decoded.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(this FileInfo? fileInfo)
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

            return ReferencedPointCloud3D(bytes);
        }

        /// <summary>
        /// Extracts the points of a cloud that link to one model object, as a cloud in its own right.
        /// <para>The extracted cloud keeps the identifiers and the whole reference table rather than being renumbered, so an identifier means the same thing in the extract as in the source and the two can be compared without a translation step. The entries that keep no points simply go unread.</para>
        /// </summary>
        /// <param name="referencedPointCloud3D">The cloud to extract from.</param>
        /// <param name="reference">The model object to extract the points of.</param>
        /// <returns>A new <see cref="Classes.ReferencedPointCloud3D"/> holding the points of the model object, or <see langword="null"/> when the cloud carries no link to it.</returns>
        public static ReferencedPointCloud3D? ReferencedPointCloud3D(this ReferencedPointCloud3D? referencedPointCloud3D, ISerializableReference? reference)
        {
            int[]? indexes = Query.PointIndexes(referencedPointCloud3D, reference);
            if (indexes == null)
            {
                return null;
            }

            double[][]? coordinates = Core.Create.GatheredCoordinates(referencedPointCloud3D!.GetCoordinates(false), indexes);
            if (coordinates == null)
            {
                return null;
            }

            int[]? referenceIndexes = Core.Create.GatheredReferenceIndexes(referencedPointCloud3D.GetReferenceIndexes(false), indexes);

            return new ReferencedPointCloud3D(coordinates[0], coordinates[1], coordinates[2], referenceIndexes, referencedPointCloud3D.GetPointCloudReferenceCollection(false), false);
        }
    }
}

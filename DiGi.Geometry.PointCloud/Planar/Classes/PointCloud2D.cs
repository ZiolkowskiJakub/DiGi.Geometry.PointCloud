using DiGi.Core.Interfaces;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DiGi.Geometry.PointCloud.Planar.Classes
{
    /// <summary>
    /// Represents a cloud of two-dimensional points stored as two parallel coordinate arrays.
    /// <para>See <see cref="Core.Classes.PointCloud"/> for why the storage is coordinate-major rather than a list of <see cref="Point2D"/> objects, and for the concurrency contract.</para>
    /// <para>Construct through <see cref="Create.PointCloud2D(IEnumerable{Point2D})"/> when the input may contain non-finite coordinates. The constructors here only assign and copy; the factory performs the filtering.</para>
    /// </summary>
    public class PointCloud2D : Core.Classes.PointCloud, IGeometry2D, IBoundable2D, ICollectable2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud2D"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> holding the serialized cloud.</param>
        public PointCloud2D(JsonObject? jsonObject)
            : base(jsonObject, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud2D"/> class by copying an existing cloud.
        /// </summary>
        /// <param name="pointCloud2D">The cloud to copy from.</param>
        public PointCloud2D(PointCloud2D? pointCloud2D)
            : base(pointCloud2D, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud2D"/> class from a sequence of points.
        /// <para>Null entries are skipped, since a null has no representation in a coordinate array. Non-finite coordinates are NOT filtered here; use <see cref="Create.PointCloud2D(IEnumerable{Point2D})"/> for that.</para>
        /// </summary>
        /// <param name="point2Ds">The points to store.</param>
        public PointCloud2D(IEnumerable<Point2D?>? point2Ds)
            : base(2)
        {
            if (point2Ds == null)
            {
                return;
            }

            int capacity = point2Ds is ICollection<Point2D?> point2Ds_Collection ? point2Ds_Collection.Count : 4;

            double[] x = new double[capacity];
            double[] y = new double[capacity];

            int count = 0;
            foreach (Point2D? point2D in point2Ds)
            {
                if (point2D == null)
                {
                    continue;
                }

                if (count == x.Length)
                {
                    int capacity_Temp = x.Length == 0 ? 4 : x.Length * 2;
                    System.Array.Resize(ref x, capacity_Temp);
                    System.Array.Resize(ref y, capacity_Temp);
                }

                x[count] = point2D.X;
                y[count] = point2D.Y;
                count++;
            }

            if (count != x.Length)
            {
                System.Array.Resize(ref x, count);
                System.Array.Resize(ref y, count);
            }

            coordinates = [x, y];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud2D"/> class by copying two coordinate arrays.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        public PointCloud2D(double[]? x, double[]? y)
            : base(Coordinates(x, y), 2, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud2D"/> class from two prebuilt coordinate arrays.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="clone">When <see langword="true"/>, the arrays are defensively copied; when <see langword="false"/>, they are adopted directly. Use <see langword="false"/> only when the caller owns freshly created arrays that are not shared.</param>
        internal PointCloud2D(double[]? x, double[]? y, bool clone)
            : base(Coordinates(x, y), 2, clone)
        {
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A new <see cref="ISerializableObject"/> instance that is a clone of the current object.</returns>
        public override ISerializableObject? Clone()
        {
            return new PointCloud2D(this);
        }

        /// <summary>
        /// Calculates the axis-aligned bounding box enclosing every point in the cloud.
        /// </summary>
        /// <returns>A <see cref="BoundingBox2D"/> enclosing the cloud, or <see langword="null"/> when the cloud is empty.</returns>
        public BoundingBox2D? GetBoundingBox()
        {
            double[]? coordinateExtremes = Core.Query.CoordinateExtremes(coordinates);
            if (coordinateExtremes == null)
            {
                return null;
            }

            return new BoundingBox2D(new Point2D(coordinateExtremes[0], coordinateExtremes[2]), new Point2D(coordinateExtremes[1], coordinateExtremes[3]));
        }

        /// <summary>
        /// Retrieves the point at the specified index.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <returns>A new <see cref="Point2D"/>, or <see langword="null"/> when the index is out of range.</returns>
        public Point2D? GetPoint(int index)
        {
            if (!TryGetPoint(index, out double x, out double y))
            {
                return null;
            }

            return new Point2D(x, y);
        }

        /// <summary>
        /// Materializes every point in the cloud as a list of <see cref="Point2D"/> objects.
        /// <para>This allocates one object per point and is intended for interoperability, not for bulk processing. Prefer the coordinate array accessors when working at scale.</para>
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Point2D"/>, or <see langword="null"/> when the cloud is empty.</returns>
        public List<Point2D>? GetPoints()
        {
            return GetPoints(0, Count);
        }

        /// <summary>
        /// Materializes a contiguous range of the cloud as a list of <see cref="Point2D"/> objects.
        /// </summary>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Point2D"/>, or <see langword="null"/> when the cloud is empty or the range is out of bounds.</returns>
        public List<Point2D>? GetPoints(int startIndex, int count)
        {
            if (coordinates == null)
            {
                return null;
            }

            double[] x = coordinates[0];
            double[] y = coordinates[1];

            if (startIndex < 0 || count < 0 || startIndex > x.Length - count)
            {
                return null;
            }

            List<Point2D> result = new(count);

            int end = startIndex + count;
            for (int i = startIndex; i < end; i++)
            {
                result.Add(new Point2D(x[i], y[i]));
            }

            return result;
        }

        /// <summary>
        /// Retrieves the X coordinate array.
        /// </summary>
        /// <returns>A copy of the X coordinates, or <see langword="null"/> when the cloud is empty.</returns>
        public double[]? GetX()
        {
            return GetCoordinates(0);
        }

        /// <summary>
        /// Retrieves the Y coordinate array.
        /// </summary>
        /// <returns>A copy of the Y coordinates, or <see langword="null"/> when the cloud is empty.</returns>
        public double[]? GetY()
        {
            return GetCoordinates(1);
        }

        /// <summary>
        /// Translates every point in the cloud by the specified vector.
        /// </summary>
        /// <param name="vector2D">The translation vector.</param>
        /// <returns><see langword="true"/> when the cloud was moved; otherwise <see langword="false"/>.</returns>
        public bool Move(Vector2D? vector2D)
        {
            if (vector2D == null || coordinates == null)
            {
                return false;
            }

            if (!Core.Modify.OffsetCoordinates(coordinates, [vector2D.X, vector2D.Y]))
            {
                return false;
            }

            InvalidateIndex();

            return true;
        }

        /// <summary>
        /// Applies a transform to every point in the cloud.
        /// <para>The transform is flattened into an affine matrix once and then streamed over the coordinate arrays, rather than being replayed per point.</para>
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        /// <returns><see langword="true"/> when the cloud was transformed; otherwise <see langword="false"/>.</returns>
        public bool Transform(ITransform2D? transform)
        {
            if (transform == null || coordinates == null)
            {
                return false;
            }

            double[]? affine = Query.Affine(transform);
            if (affine == null)
            {
                return false;
            }

            if (!Core.Modify.TransformCoordinates(coordinates, affine))
            {
                return false;
            }

            InvalidateIndex();

            return true;
        }

        /// <summary>
        /// Retrieves a single point without allocating.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <param name="x">When this method returns, contains the X coordinate.</param>
        /// <param name="y">When this method returns, contains the Y coordinate.</param>
        /// <returns><see langword="true"/> when the point was retrieved; otherwise <see langword="false"/>.</returns>
        public bool TryGetPoint(int index, out double x, out double y)
        {
            x = double.NaN;
            y = double.NaN;

            if (coordinates == null || index < 0 || index >= Count)
            {
                return false;
            }

            x = coordinates[0][index];
            y = coordinates[1][index];

            return true;
        }

        /// <summary>
        /// Returns a zero-copy view over the whole cloud.
        /// <para>The view is a ref struct holding two spans, so it cannot escape to the heap and cannot outlive the arrays it points at. Reading through it allocates nothing.</para>
        /// </summary>
        /// <returns>A <see cref="PointCloud2DView"/> over the cloud.</returns>
        public PointCloud2DView AsView()
        {
            return new PointCloud2DView(AsSpan(0), AsSpan(1));
        }

        /// <summary>
        /// Returns a zero-copy view over a contiguous range of the cloud.
        /// </summary>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns>A <see cref="PointCloud2DView"/> over the range, empty when the range is out of bounds.</returns>
        public PointCloud2DView AsView(int startIndex, int count)
        {
            return new PointCloud2DView(AsSpan(0, startIndex, count), AsSpan(1, startIndex, count));
        }

        /// <summary>
        /// Returns an enumerator that walks the cloud without allocating.
        /// <para>This is duck-typed rather than an <see cref="IEnumerable{T}"/> implementation, and deliberately so. Implementing <see cref="IEnumerable{T}"/> of <see cref="Point2D"/> would make this type match both <c>ToSystem_Bytes</c> overloads in DiGi.Core, and it would invite query operators that materialize one object per point and undo the entire storage design.</para>
        /// </summary>
        /// <returns>An <see cref="Enumerator"/> positioned before the first point.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns the cloud as a lazy sequence of <see cref="Point2D"/> objects for interoperability with APIs that require them.
        /// <para>Named explicitly rather than exposed through <see cref="IEnumerable{T}"/> so that the per-point allocation is a visible choice at the call site.</para>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Point2D"/>.</returns>
        public IEnumerable<Point2D> Point2Ds()
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                yield return new Point2D(coordinates![0][i], coordinates[1][i]);
            }
        }

        private static double[][]? Coordinates(double[]? x, double[]? y)
        {
            if (x == null || y == null)
            {
                return null;
            }

            return [x, y];
        }

        /// <summary>
        /// Represents a single point of a <see cref="PointCloud2D"/> as a value.
        /// <para>A plain readonly struct rather than a ref struct: a point holds two doubles and no reference, so the ref struct restrictions would buy nothing while preventing use in generics, lambdas, arrays and lists.</para>
        /// </summary>
        public readonly struct Point
        {
            private readonly double x;
            private readonly double y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point"/> struct.
            /// </summary>
            /// <param name="x">The X coordinate.</param>
            /// <param name="y">The Y coordinate.</param>
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            /// <summary>
            /// Gets the X coordinate.
            /// </summary>
            /// <value>A <see cref="double"/> holding the X coordinate.</value>
            public double X
            {
                get
                {
                    return x;
                }
            }

            /// <summary>
            /// Gets the Y coordinate.
            /// </summary>
            /// <value>A <see cref="double"/> holding the Y coordinate.</value>
            public double Y
            {
                get
                {
                    return y;
                }
            }

            /// <summary>
            /// Materializes this value as a <see cref="Point2D"/> object.
            /// </summary>
            /// <returns>A new <see cref="Point2D"/>.</returns>
            public Point2D ToPoint2D()
            {
                return new Point2D(x, y);
            }
        }

        /// <summary>
        /// Walks a <see cref="PointCloud2D"/> one point at a time without allocating.
        /// <para>A plain struct rather than a ref struct, so it remains usable inside iterators, lambdas and asynchronous methods. The span-based counterpart lives on <see cref="PointCloud2DView"/>.</para>
        /// </summary>
        public struct Enumerator
        {
            private readonly double[]? x;
            private readonly double[]? y;
            private readonly int count;
            private int index;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct.
            /// </summary>
            /// <param name="pointCloud2D">The cloud to walk.</param>
            public Enumerator(PointCloud2D? pointCloud2D)
            {
                double[][]? coordinates = pointCloud2D?.GetCoordinates(false);

                x = coordinates?[0];
                y = coordinates?[1];
                count = x == null ? 0 : x.Length;
                index = -1;
            }

            /// <summary>
            /// Gets the point at the current position.
            /// </summary>
            /// <value>A <see cref="Point"/> holding the current coordinates.</value>
            public Point Current
            {
                get
                {
                    return new Point(x![index], y![index]);
                }
            }

            /// <summary>
            /// Advances to the next point.
            /// </summary>
            /// <returns><see langword="true"/> when a further point is available; otherwise <see langword="false"/>.</returns>
            public bool MoveNext()
            {
                index++;

                return index < count;
            }
        }
    }
}

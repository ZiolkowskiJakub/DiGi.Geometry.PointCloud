using System;

namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    /// <summary>
    /// Represents a zero-copy, read-only window onto the coordinate arrays of a <see cref="PointCloud3D"/>.
    /// <para>Declared as a ref struct because it holds spans. That restriction is the point: the view cannot be boxed, stored in a field, captured by a lambda or held across an await, so it cannot outlive the arrays it points at.</para>
    /// <para>Slicing produces another view rather than copying, which makes it the natural way to hand a partition of a large cloud to a worker without allocating anything.</para>
    /// </summary>
    public readonly ref struct PointCloud3DView
    {
        private readonly ReadOnlySpan<double> x;
        private readonly ReadOnlySpan<double> y;
        private readonly ReadOnlySpan<double> z;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud3DView"/> struct.
        /// <para>When the three spans are not of equal length the view is empty, so a mismatched construction cannot produce out-of-range reads.</para>
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="z">The Z coordinates.</param>
        public PointCloud3DView(ReadOnlySpan<double> x, ReadOnlySpan<double> y, ReadOnlySpan<double> z)
        {
            if (x.Length != y.Length || x.Length != z.Length)
            {
                this.x = default;
                this.y = default;
                this.z = default;

                return;
            }

            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Gets the number of points in the view.
        /// </summary>
        /// <value>An <see cref="int"/> point count of zero or more.</value>
        public int Count
        {
            get
            {
                return x.Length;
            }
        }

        /// <summary>
        /// Gets the X coordinates.
        /// </summary>
        /// <value>A <see cref="ReadOnlySpan{T}"/> over the X coordinates.</value>
        public ReadOnlySpan<double> X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Gets the Y coordinates.
        /// </summary>
        /// <value>A <see cref="ReadOnlySpan{T}"/> over the Y coordinates.</value>
        public ReadOnlySpan<double> Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Gets the Z coordinates.
        /// </summary>
        /// <value>A <see cref="ReadOnlySpan{T}"/> over the Z coordinates.</value>
        public ReadOnlySpan<double> Z
        {
            get
            {
                return z;
            }
        }

        /// <summary>
        /// Gets the point at the specified index.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <returns>A <see cref="PointCloud3D.Point"/> holding the coordinates at that index.</returns>
        public PointCloud3D.Point this[int index]
        {
            get
            {
                return new PointCloud3D.Point(x[index], y[index], z[index]);
            }
        }

        /// <summary>
        /// Returns an enumerator that walks the view without allocating.
        /// </summary>
        /// <returns>An <see cref="Enumerator"/> positioned before the first point.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns a view over a contiguous range of this view, without copying.
        /// </summary>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns>A <see cref="PointCloud3DView"/> over the range, empty when the range is out of bounds.</returns>
        public PointCloud3DView Slice(int startIndex, int count)
        {
            if (startIndex < 0 || count < 0 || startIndex > x.Length - count)
            {
                return default;
            }

            return new PointCloud3DView(x.Slice(startIndex, count), y.Slice(startIndex, count), z.Slice(startIndex, count));
        }

        /// <summary>
        /// Retrieves a single point without allocating.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <param name="x">When this method returns, contains the X coordinate.</param>
        /// <param name="y">When this method returns, contains the Y coordinate.</param>
        /// <param name="z">When this method returns, contains the Z coordinate.</param>
        /// <returns><see langword="true"/> when the point was retrieved; otherwise <see langword="false"/>.</returns>
        public bool TryGetPoint(int index, out double x, out double y, out double z)
        {
            x = double.NaN;
            y = double.NaN;
            z = double.NaN;

            if (index < 0 || index >= this.x.Length)
            {
                return false;
            }

            x = this.x[index];
            y = this.y[index];
            z = this.z[index];

            return true;
        }

        /// <summary>
        /// Walks a <see cref="PointCloud3DView"/> one point at a time without allocating.
        /// <para>This one must be a ref struct, because it holds the spans of the view it walks.</para>
        /// </summary>
        public ref struct Enumerator
        {
            private readonly PointCloud3DView pointCloud3DView;
            private int index;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct.
            /// </summary>
            /// <param name="pointCloud3DView">The view to walk.</param>
            public Enumerator(PointCloud3DView pointCloud3DView)
            {
                this.pointCloud3DView = pointCloud3DView;
                index = -1;
            }

            /// <summary>
            /// Gets the point at the current position.
            /// </summary>
            /// <value>A <see cref="PointCloud3D.Point"/> holding the current coordinates.</value>
            public PointCloud3D.Point Current
            {
                get
                {
                    return pointCloud3DView[index];
                }
            }

            /// <summary>
            /// Advances to the next point.
            /// </summary>
            /// <returns><see langword="true"/> when a further point is available; otherwise <see langword="false"/>.</returns>
            public bool MoveNext()
            {
                index++;

                return index < pointCloud3DView.Count;
            }
        }
    }
}

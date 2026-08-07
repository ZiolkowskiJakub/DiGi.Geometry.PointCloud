namespace DiGi.Geometry.PointCloud.Planar.Classes
{
    public partial class PointCloud2D
    {
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

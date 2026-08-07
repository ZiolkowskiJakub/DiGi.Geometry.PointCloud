namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    public partial class PointCloud3D
    {
        /// <summary>
        /// Walks a <see cref="PointCloud3D"/> one point at a time without allocating.
        /// <para>A plain struct rather than a ref struct, so it remains usable inside iterators, lambdas and asynchronous methods. The span-based counterpart lives on <see cref="PointCloud3DView"/>.</para>
        /// </summary>
        public struct Enumerator
        {
            private readonly double[]? x;
            private readonly double[]? y;
            private readonly double[]? z;
            private readonly int count;
            private int index;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct.
            /// </summary>
            /// <param name="pointCloud3D">The cloud to walk.</param>
            public Enumerator(PointCloud3D? pointCloud3D)
            {
                double[][]? coordinates = pointCloud3D?.GetCoordinates(false);

                x = coordinates?[0];
                y = coordinates?[1];
                z = coordinates?[2];
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
                    return new Point(x![index], y![index], z![index]);
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

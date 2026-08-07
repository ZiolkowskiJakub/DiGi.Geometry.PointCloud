using DiGi.Geometry.Planar.Classes;

namespace DiGi.Geometry.PointCloud.Planar.Classes
{
    public partial class PointCloud2D
    {
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
    }
}

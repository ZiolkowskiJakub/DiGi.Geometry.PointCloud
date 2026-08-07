using DiGi.Geometry.Spatial.Classes;

namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    public partial class PointCloud3D
    {
        /// <summary>
        /// Represents a single point of a <see cref="PointCloud3D"/> as a value.
        /// <para>A plain readonly struct rather than a ref struct: a point holds three doubles and no reference, so the ref struct restrictions would buy nothing while preventing use in generics, lambdas, arrays and lists.</para>
        /// </summary>
        public readonly struct Point
        {
            private readonly double x;
            private readonly double y;
            private readonly double z;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point"/> struct.
            /// </summary>
            /// <param name="x">The X coordinate.</param>
            /// <param name="y">The Y coordinate.</param>
            /// <param name="z">The Z coordinate.</param>
            public Point(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
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
            /// Gets the Z coordinate.
            /// </summary>
            /// <value>A <see cref="double"/> holding the Z coordinate.</value>
            public double Z
            {
                get
                {
                    return z;
                }
            }

            /// <summary>
            /// Materializes this value as a <see cref="Point3D"/> object.
            /// </summary>
            /// <returns>A new <see cref="Point3D"/>.</returns>
            public Point3D ToPoint3D()
            {
                return new Point3D(x, y, z);
            }
        }
    }
}

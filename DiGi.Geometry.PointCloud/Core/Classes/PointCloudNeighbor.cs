namespace DiGi.Geometry.PointCloud.Core.Classes
{
    /// <summary>
    /// Represents one result of a nearest neighbour query: the index of a point within its cloud, together with its squared distance from the query position.
    /// <para>The distance is held squared because that is what the search actually computes. A nearest neighbour search compares distances, never uses them, and squaring is monotonic, so every comparison along the way is exact and the square root is deferred until a caller asks for one. On a query that examines a few hundred candidates this removes a few hundred square roots from the hot path.</para>
    /// <para>A plain readonly struct rather than a record struct: the target framework has no <c>IsExternalInit</c>, so init-only accessors would need a shim, and the type has no use for value equality that the two fields do not already provide by inspection. This matches <see cref="PointCloudIndexNode"/> and <see cref="Spatial.Classes.PointCloud3D.Point"/>.</para>
    /// </summary>
    public readonly struct PointCloudNeighbor
    {
        private readonly int index;
        private readonly double distanceSquared;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudNeighbor"/> struct.
        /// </summary>
        /// <param name="index">The zero-based index of the point within its cloud.</param>
        /// <param name="distanceSquared">The squared distance from the query position to the point.</param>
        public PointCloudNeighbor(int index, double distanceSquared)
        {
            this.index = index;
            this.distanceSquared = distanceSquared;
        }

        /// <summary>
        /// Gets the zero-based index of the point within its cloud.
        /// </summary>
        /// <value>An <see cref="int"/> point index, or -1 when the neighbour is unset.</value>
        public int Index
        {
            get
            {
                return index;
            }
        }

        /// <summary>
        /// Gets the squared distance from the query position to the point.
        /// </summary>
        /// <value>A <see cref="double"/> squared distance, or <see cref="double.PositiveInfinity"/> when the neighbour is unset.</value>
        public double DistanceSquared
        {
            get
            {
                return distanceSquared;
            }
        }

        /// <summary>
        /// Gets the distance from the query position to the point.
        /// <para>Computed on demand. Prefer <see cref="DistanceSquared"/> when the value is only being compared against another distance from the same query.</para>
        /// </summary>
        /// <value>A <see cref="double"/> distance.</value>
        public double Distance
        {
            get
            {
                return System.Math.Sqrt(distanceSquared);
            }
        }
    }
}

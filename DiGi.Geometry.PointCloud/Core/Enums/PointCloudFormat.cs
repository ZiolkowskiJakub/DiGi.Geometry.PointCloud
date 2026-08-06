namespace DiGi.Geometry.PointCloud.Core.Enums
{
    /// <summary>
    /// Specifies the byte representation produced for a point cloud.
    /// </summary>
    public enum PointCloudFormat
    {
        /// <summary>
        /// The compact binary point cloud format, holding a fixed header followed by a coordinate-major payload of raw doubles.
        /// <para>This is the only representation that scales to tens of millions of points.</para>
        /// </summary>
        Binary = 0,

        /// <summary>
        /// The UTF-8 encoded JSON representation, in which the coordinate payload appears as a single Base64 string.
        /// <para>Convenient and self-describing, but a round trip holds several copies of the payload in memory at once, so it is impractical much beyond a few million points.</para>
        /// </summary>
        Json = 1
    }
}

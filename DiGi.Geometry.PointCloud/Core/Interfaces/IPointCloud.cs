using DiGi.Geometry.Core.Interfaces;

namespace DiGi.Geometry.PointCloud.Core.Interfaces
{
    /// <summary>
    /// Represents an unordered collection of points held in a coordinate-major layout, sized for bulk streaming rather than random access.
    /// </summary>
    public interface IPointCloud : IGeometry
    {
        /// <summary>
        /// Gets the number of points in the cloud, or zero when the cloud holds no coordinate data.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the number of coordinate axes, which is two for a planar cloud and three for a spatial one.
        /// </summary>
        int Dimension { get; }
    }
}

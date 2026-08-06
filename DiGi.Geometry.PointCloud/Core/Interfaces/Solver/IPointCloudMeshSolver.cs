using DiGi.Geometry.Core.Interfaces;

namespace DiGi.Geometry.PointCloud.Core.Interfaces
{
    /// <summary>
    /// Represents a pluggable strategy for reconstructing a mesh from a point cloud.
    /// <para>Surface reconstruction has no single right answer, so the strategy is a parameter rather than a hard-coded step. A height field triangulation is the right tool for terrain, floors and roofs; an isosurface extraction is the right tool for an arbitrary scan. Each carries limitations that make it wrong for the other's data, so the choice belongs to the caller.</para>
    /// </summary>
    /// <typeparam name="TPointCloud">The point cloud type consumed.</typeparam>
    /// <typeparam name="TMesh">The mesh type produced.</typeparam>
    public interface IPointCloudMeshSolver<TPointCloud, TMesh> : IOneToOneGeometrySolver<TPointCloud, TMesh> where TPointCloud : IPointCloud where TMesh : IMesh
    {
        /// <summary>
        /// Gets or sets the edge length of the working grid, in model units.
        /// <para>This is the single knob that trades detail against cost, and it is not optional: reconstruction cost grows far faster than linearly with the number of sites, so it is what keeps a cloud of millions tractable.</para>
        /// </summary>
        double CellSize { get; set; }

        /// <summary>
        /// Gets or sets the distance tolerance used when comparing coordinates.
        /// </summary>
        double Tolerance { get; set; }
    }
}

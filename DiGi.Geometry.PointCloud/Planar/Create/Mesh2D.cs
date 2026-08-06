using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.PointCloud.Core.Interfaces;
using DiGi.Geometry.PointCloud.Planar.Classes;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Create
    {
        /// <summary>
        /// Reconstructs a <see cref="Mesh2D"/> from a <see cref="Classes.PointCloud2D"/> using the supplied strategy.
        /// </summary>
        /// <param name="pointCloud2D">The cloud to reconstruct.</param>
        /// <param name="pointCloudMeshSolver">The reconstruction strategy to apply.</param>
        /// <returns>The reconstructed <see cref="Mesh2D"/>, or <see langword="null"/> when either argument is null or the reconstruction produced nothing.</returns>
        public static Mesh2D? Mesh2D(this PointCloud2D? pointCloud2D, IPointCloudMeshSolver<PointCloud2D, Mesh2D>? pointCloudMeshSolver)
        {
            if (pointCloud2D == null || pointCloudMeshSolver == null)
            {
                return null;
            }

            pointCloudMeshSolver.Input = pointCloud2D;

            if (!pointCloudMeshSolver.Solve())
            {
                return null;
            }

            return pointCloudMeshSolver.Output;
        }
    }
}

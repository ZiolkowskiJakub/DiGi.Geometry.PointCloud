using DiGi.Geometry.PointCloud.Core.Interfaces;
using DiGi.Geometry.PointCloud.Spatial.Classes;
using DiGi.Geometry.Spatial.Classes;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Create
    {
        /// <summary>
        /// Reconstructs a <see cref="Mesh3D"/> from a <see cref="Classes.PointCloud3D"/> using the supplied strategy.
        /// <para>The strategy is required rather than defaulted. Reconstruction from an unstructured cloud is a modelling decision, not a calculation, and every available strategy is wrong for some input: a height field cannot express a vertical face, and an isosurface without surface normals cannot distinguish inside from outside. Making the caller choose keeps that decision visible.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to reconstruct.</param>
        /// <param name="pointCloudMeshSolver">The reconstruction strategy to apply.</param>
        /// <returns>The reconstructed <see cref="Mesh3D"/>, or <see langword="null"/> when either argument is null or the reconstruction produced nothing.</returns>
        public static Mesh3D? Mesh3D(this PointCloud3D? pointCloud3D, IPointCloudMeshSolver<PointCloud3D, Mesh3D>? pointCloudMeshSolver)
        {
            if (pointCloud3D == null || pointCloudMeshSolver == null)
            {
                return null;
            }

            pointCloudMeshSolver.Input = pointCloud3D;

            if (!pointCloudMeshSolver.Solve())
            {
                return null;
            }

            return pointCloudMeshSolver.Output;
        }
    }
}

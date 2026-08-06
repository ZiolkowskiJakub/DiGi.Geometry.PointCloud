using DiGi.Geometry.Core.Interfaces;
using DiGi.Geometry.PointCloud.Core.Interfaces;

namespace DiGi.Geometry.PointCloud.Core.Classes
{
    /// <summary>
    /// Represents an abstract base for point cloud mesh reconstruction strategies, holding the shared settings and the produced mesh.
    /// <para>Not a serializable object, matching the other solvers in the geometry library: a solver is a transient piece of machinery, not part of a model.</para>
    /// </summary>
    /// <typeparam name="TPointCloud">The point cloud type consumed.</typeparam>
    /// <typeparam name="TMesh">The mesh type produced.</typeparam>
    public abstract class PointCloudMeshSolver<TPointCloud, TMesh> : IPointCloudMeshSolver<TPointCloud, TMesh> where TPointCloud : IPointCloud where TMesh : IMesh
    {
        /// <summary>
        /// The edge length of the working grid, in model units.
        /// </summary>
        protected double cellSize;

        /// <summary>
        /// The distance tolerance used when comparing coordinates.
        /// </summary>
        protected double tolerance;

        /// <summary>
        /// The mesh produced by the most recent successful solve.
        /// </summary>
        protected TMesh? output;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudMeshSolver{TPointCloud, TMesh}"/> class.
        /// </summary>
        /// <param name="cellSize">The edge length of the working grid, in model units.</param>
        /// <param name="tolerance">The distance tolerance used when comparing coordinates.</param>
        protected PointCloudMeshSolver(double cellSize, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
        {
            this.cellSize = cellSize;
            this.tolerance = tolerance;
        }

        /// <summary>
        /// Gets or sets the edge length of the working grid, in model units.
        /// </summary>
        /// <value>A <see cref="double"/> holding the grid edge length.</value>
        public double CellSize
        {
            get
            {
                return cellSize;
            }

            set
            {
                cellSize = value;
            }
        }

        /// <summary>
        /// Sets the cloud to reconstruct.
        /// </summary>
        /// <value>The point cloud to consume.</value>
        public abstract TPointCloud? Input { set; }

        /// <summary>
        /// Gets the mesh produced by the most recent successful solve.
        /// </summary>
        /// <value>The reconstructed mesh, or <see langword="null"/> when no solve has succeeded.</value>
        public TMesh? Output
        {
            get
            {
                return output;
            }
        }

        /// <summary>
        /// Gets or sets the distance tolerance used when comparing coordinates.
        /// </summary>
        /// <value>A <see cref="double"/> holding the distance tolerance.</value>
        public double Tolerance
        {
            get
            {
                return tolerance;
            }

            set
            {
                tolerance = value;
            }
        }

        /// <summary>
        /// Runs the reconstruction.
        /// </summary>
        /// <returns><see langword="true"/> when a mesh was produced; otherwise <see langword="false"/>.</returns>
        public abstract bool Solve();
    }
}

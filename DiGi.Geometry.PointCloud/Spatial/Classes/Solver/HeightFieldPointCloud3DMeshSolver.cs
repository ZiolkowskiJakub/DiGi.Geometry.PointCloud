using DiGi.Geometry.PointCloud.Core.Enums;
using DiGi.Geometry.Spatial.Classes;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    /// <summary>
    /// Reconstructs a <see cref="Mesh3D"/> from a <see cref="PointCloud3D"/> by triangulating the cloud on the XY plane and carrying each vertex's Z through unchanged.
    /// <para>This is a two-and-a-half dimensional reconstruction: exactly one height per XY position. That makes it fast, robust and free of tuning for terrain, floors, roofs and any other surface that is a function of plan position.</para>
    /// <para>IMPORTANT: it cannot represent a vertical face, an overhang, or canopy above ground. Given a facade scan or a full building interior it will produce confident nonsense, because the model itself cannot express what the data contains. Use the isosurface solver for arbitrary geometry.</para>
    /// <para>Decimation through <see cref="Core.Classes.PointCloudMeshSolver{TPointCloud, TMesh}.CellSize"/> is effectively mandatory at scale, and <see cref="PointCloudHeightSelection"/> decides which measurement in a cell survives it: the lowest for bare ground, the highest for a surface model.</para>
    /// </summary>
    public class HeightFieldPointCloud3DMeshSolver : Core.Classes.PointCloudMeshSolver<PointCloud3D, Mesh3D>
    {
        private PointCloud3D? pointCloud3D;
        private double maximumEdgeLength;
        private PointCloudHeightSelection pointCloudHeightSelection;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightFieldPointCloud3DMeshSolver"/> class.
        /// </summary>
        /// <param name="cellSize">The edge length of the decimation grid, in model units. Values of zero or less triangulate every point.</param>
        /// <param name="maximumEdgeLength">The longest edge a triangle may have, in model units. Values of zero or less keep every triangle.</param>
        /// <param name="pointCloudHeightSelection">Which measurement in a cell survives decimation.</param>
        /// <param name="tolerance">The distance tolerance used when comparing coordinates.</param>
        public HeightFieldPointCloud3DMeshSolver(double cellSize = 0, double maximumEdgeLength = 0, PointCloudHeightSelection pointCloudHeightSelection = PointCloudHeightSelection.Lowest, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
            : base(cellSize, tolerance)
        {
            this.maximumEdgeLength = maximumEdgeLength;
            this.pointCloudHeightSelection = pointCloudHeightSelection;
        }

        /// <summary>
        /// Sets the cloud to reconstruct.
        /// </summary>
        /// <value>The <see cref="PointCloud3D"/> to consume.</value>
        public override PointCloud3D? Input
        {
            set
            {
                pointCloud3D = value;
                output = null;
            }
        }

        /// <summary>
        /// Gets or sets the longest edge a triangle may have, in model units.
        /// <para>Without this, the triangulation spans the convex hull of the data and bridges every concave boundary and hole with long thin triangles.</para>
        /// </summary>
        /// <value>A <see cref="double"/> holding the maximum edge length. Values of zero or less keep every triangle.</value>
        public double MaximumEdgeLength
        {
            get
            {
                return maximumEdgeLength;
            }

            set
            {
                maximumEdgeLength = value;
            }
        }

        /// <summary>
        /// Gets or sets which measurement in a grid cell survives decimation.
        /// </summary>
        /// <value>A <see cref="Core.Enums.PointCloudHeightSelection"/> value.</value>
        public PointCloudHeightSelection PointCloudHeightSelection
        {
            get
            {
                return pointCloudHeightSelection;
            }

            set
            {
                pointCloudHeightSelection = value;
            }
        }

        /// <summary>
        /// Runs the reconstruction.
        /// </summary>
        /// <returns><see langword="true"/> when a mesh was produced; otherwise <see langword="false"/>.</returns>
        public override bool Solve()
        {
            output = null;

            double[][]? coordinates = pointCloud3D?.GetCoordinates(false);
            if (coordinates == null)
            {
                return false;
            }

            if (cellSize > 0)
            {
                coordinates = Core.Create.DecimatedCoordinates(coordinates, cellSize, pointCloudHeightSelection);
                if (coordinates == null)
                {
                    return false;
                }
            }

            List<int[]>? indexes = Core.Query.DelaunayIndexes(coordinates[0], coordinates[1], maximumEdgeLength);
            if (indexes == null)
            {
                return false;
            }

            double[] x = coordinates[0];
            double[] y = coordinates[1];
            double[] z = coordinates[2];

            // Z comes from the caller's own arrays, indexed by the triangulation result. It is never read
            // back out of the triangulator, whose internal representation carries no guarantee about it.
            List<Point3D> point3Ds = new(x.Length);
            for (int i = 0; i < x.Length; i++)
            {
                point3Ds.Add(new Point3D(x[i], y[i], z[i]));
            }

            output = new Mesh3D(point3Ds, indexes);

            return true;
        }
    }
}

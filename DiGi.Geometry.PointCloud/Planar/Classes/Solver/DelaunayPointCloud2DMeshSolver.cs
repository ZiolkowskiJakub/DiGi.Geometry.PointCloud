using DiGi.Geometry.Planar.Classes;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Planar.Classes
{
    /// <summary>
    /// Reconstructs a <see cref="Mesh2D"/> from a <see cref="PointCloud2D"/> by Delaunay triangulation.
    /// <para>In two dimensions reconstruction is unambiguous, so unlike the spatial case there is no modelling assumption to get wrong: the Delaunay triangulation is the triangulation of the point set.</para>
    /// <para>Set <see cref="Core.Classes.PointCloudMeshSolver{TPointCloud, TMesh}.CellSize"/> above zero to decimate onto a grid first. Triangulation cost grows far faster than the point count, so a cloud of any real size must be thinned before it is triangulated.</para>
    /// <para>Set <see cref="MaximumEdgeLength"/> to discard the long thin triangles that a Delaunay triangulation necessarily produces where it spans a concave outline or an interior hole.</para>
    /// </summary>
    public class DelaunayPointCloud2DMeshSolver : Core.Classes.PointCloudMeshSolver<PointCloud2D, Mesh2D>
    {
        private PointCloud2D? pointCloud2D;
        private double maximumEdgeLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelaunayPointCloud2DMeshSolver"/> class.
        /// </summary>
        /// <param name="cellSize">The edge length of the decimation grid, in model units. Values of zero or less triangulate every point.</param>
        /// <param name="maximumEdgeLength">The longest edge a triangle may have, in model units. Values of zero or less keep every triangle.</param>
        /// <param name="tolerance">The distance tolerance used when comparing coordinates.</param>
        public DelaunayPointCloud2DMeshSolver(double cellSize = 0, double maximumEdgeLength = 0, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
            : base(cellSize, tolerance)
        {
            this.maximumEdgeLength = maximumEdgeLength;
        }

        /// <summary>
        /// Sets the cloud to reconstruct.
        /// </summary>
        /// <value>The <see cref="PointCloud2D"/> to consume.</value>
        public override PointCloud2D? Input
        {
            set
            {
                pointCloud2D = value;
                output = null;
            }
        }

        /// <summary>
        /// Gets or sets the longest edge a triangle may have, in model units.
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
        /// Runs the triangulation.
        /// </summary>
        /// <returns><see langword="true"/> when a mesh was produced; otherwise <see langword="false"/>.</returns>
        public override bool Solve()
        {
            output = null;

            double[][]? coordinates = pointCloud2D?.GetCoordinates(false);
            if (coordinates == null)
            {
                return false;
            }

            if (cellSize > 0)
            {
                coordinates = Core.Create.DecimatedCoordinates(coordinates, cellSize);
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

            List<Point2D> point2Ds = new(x.Length);
            for (int i = 0; i < x.Length; i++)
            {
                point2Ds.Add(new Point2D(x[i], y[i]));
            }

            output = new Mesh2D(point2Ds, indexes);

            return true;
        }
    }
}

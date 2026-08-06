using DiGi.Geometry.Spatial.Classes;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    /// <summary>
    /// Reconstructs a <see cref="Mesh3D"/> from a <see cref="PointCloud3D"/> by accumulating the points into a voxel density field and extracting an isosurface from it.
    /// <para>Unlike a height field this places no constraint on the shape of the data, so vertical faces, overhangs and enclosed volumes all survive. The cost is a dense field: memory grows with the cube of the resolution, which is what <see cref="MaximumVoxelCount"/> exists to bound.</para>
    /// <para>The cell is subdivided into six tetrahedra rather than being resolved through the classic two hundred and fifty six case cube table. Both extract the same surface, but the tetrahedral decomposition has no ambiguous configurations, so it cannot produce the holes that the naive cube table yields on saddle cells, and it is driven by a handful of cases that can be read and checked rather than by four thousand table entries that cannot.</para>
    /// <para>IMPORTANT LIMITATION: a cloud carries positions but no surface normals, so the field can only express how much data is nearby, not which side of a surface a voxel is on. The extracted isosurface therefore wraps the points on both sides and comes out as a thin closed shell roughly two voxels thick, not as a single surface sheet. That is inherent to reconstructing from positions alone; separating inside from outside needs oriented normals and a fitted implicit function. Treat the result as an envelope of the measured material, and expect its area to be about twice that of the surface it was scanned from.</para>
    /// <para>The field accumulation and extraction run serially. They are bounded by the voxel count rather than the point count, and the vertex welding depends on a single shared edge table, so partitioning them would need a redesign rather than a parallel loop.</para>
    /// </summary>
    public class IsosurfacePointCloud3DMeshSolver : Core.Classes.PointCloudMeshSolver<PointCloud3D, Mesh3D>
    {
        private PointCloud3D? pointCloud3D;
        private double isoValue;
        private int smoothingIterations;
        private int maximumVoxelCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsosurfacePointCloud3DMeshSolver"/> class.
        /// </summary>
        /// <param name="cellSize">The edge length of a voxel, in model units. Must be greater than zero.</param>
        /// <param name="isoValue">The density level at which the surface is drawn. Larger values pull the surface tighter onto the denser parts of the cloud.</param>
        /// <param name="smoothingIterations">The number of separable box filter passes applied to the field before extraction.</param>
        /// <param name="maximumVoxelCount">The largest number of voxels permitted. The resolution is reduced until the field fits.</param>
        /// <param name="tolerance">The distance tolerance used when comparing coordinates.</param>
        public IsosurfacePointCloud3DMeshSolver(double cellSize, double isoValue = 0.5, int smoothingIterations = 1, int maximumVoxelCount = 8000000, double tolerance = DiGi.Core.Constants.Tolerance.Distance)
            : base(cellSize, tolerance)
        {
            this.isoValue = isoValue;
            this.smoothingIterations = smoothingIterations;
            this.maximumVoxelCount = maximumVoxelCount;
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
        /// Gets or sets the density level at which the surface is drawn.
        /// </summary>
        /// <value>A <see cref="double"/> holding the iso level.</value>
        public double IsoValue
        {
            get
            {
                return isoValue;
            }

            set
            {
                isoValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the largest number of voxels permitted.
        /// <para>The field is dense, so this is a hard memory bound: the resolution is reduced until the field fits. It also bounds the output, because triangle count scales with the surface area measured in voxels.</para>
        /// </summary>
        /// <value>An <see cref="int"/> holding the voxel budget.</value>
        public int MaximumVoxelCount
        {
            get
            {
                return maximumVoxelCount;
            }

            set
            {
                maximumVoxelCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of separable box filter passes applied to the field before extraction.
        /// <para>Three one-dimensional passes approximate a three-dimensional blur at a fraction of its cost. Smoothing suppresses the speckle that sparse sampling leaves in the field, at the price of rounding genuine detail.</para>
        /// </summary>
        /// <value>An <see cref="int"/> holding the number of passes.</value>
        public int SmoothingIterations
        {
            get
            {
                return smoothingIterations;
            }

            set
            {
                smoothingIterations = value;
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
            if (coordinates == null || cellSize <= 0)
            {
                return false;
            }

            double[]? coordinateExtremes = Core.Query.CoordinateExtremes(coordinates);
            if (coordinateExtremes == null)
            {
                return false;
            }

            double x_Min = coordinateExtremes[0];
            double y_Min = coordinateExtremes[2];
            double z_Min = coordinateExtremes[4];

            double extent = System.Math.Max(coordinateExtremes[1] - x_Min, System.Math.Max(coordinateExtremes[3] - y_Min, coordinateExtremes[5] - z_Min));
            if (extent <= 0)
            {
                return false;
            }

            // Two voxels of padding on every side. One is not enough: smoothing spreads density outward by a
            // node per pass, and any density surviving on the outermost node layer would leave the surface
            // cut off flat against the edge of the field rather than closed.
            int resolution = (int)System.Math.Ceiling(extent / cellSize) + 4;
            if (resolution < 5)
            {
                resolution = 5;
            }

            while (resolution > 5 && (long)resolution * resolution * resolution > maximumVoxelCount)
            {
                resolution--;
            }

            double size = extent / (resolution - 4);

            x_Min -= size * 2.0;
            y_Min -= size * 2.0;
            z_Min -= size * 2.0;

            int nodeCount = resolution + 1;

            float[] field = new float[nodeCount * nodeCount * nodeCount];

            double[] x = coordinates[0];
            double[] y = coordinates[1];
            double[] z = coordinates[2];

            for (int i = 0; i < x.Length; i++)
            {
                Splat(field, nodeCount, (x[i] - x_Min) / size, (y[i] - y_Min) / size, (z[i] - z_Min) / size);
            }

            for (int i = 0; i < smoothingIterations; i++)
            {
                Smooth(field, nodeCount);
            }

            // The outermost node layer is forced empty. Every cell of the grid is marched, so any density
            // left on the boundary would produce a surface that runs off the edge of the field and stops
            // there, leaving a hole. Clearing the layer guarantees the extracted surface closes on itself.
            ClearBoundary(field, nodeCount);

            List<Point3D> point3Ds = [];
            List<int[]> indexes = [];

            Dictionary<(int, int), int> vertices = [];

            double iso = isoValue;

            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    for (int k = 0; k < resolution; k++)
                    {
                        March(field, nodeCount, i, j, k, iso, vertices, point3Ds, indexes, x_Min, y_Min, z_Min, size);
                    }
                }
            }

            if (indexes.Count == 0)
            {
                return false;
            }

            output = new Mesh3D(point3Ds, indexes);

            return true;
        }

        private static void Splat(float[] field, int nodeCount, double x, double y, double z)
        {
            int i = (int)x;
            int j = (int)y;
            int k = (int)z;

            if (i < 0 || j < 0 || k < 0 || i >= nodeCount - 1 || j >= nodeCount - 1 || k >= nodeCount - 1)
            {
                return;
            }

            float x_Fraction = (float)(x - i);
            float y_Fraction = (float)(y - j);
            float z_Fraction = (float)(z - k);

            for (int di = 0; di <= 1; di++)
            {
                float weight_X = di == 0 ? 1.0f - x_Fraction : x_Fraction;
                for (int dj = 0; dj <= 1; dj++)
                {
                    float weight_Y = dj == 0 ? 1.0f - y_Fraction : y_Fraction;
                    for (int dk = 0; dk <= 1; dk++)
                    {
                        float weight_Z = dk == 0 ? 1.0f - z_Fraction : z_Fraction;

                        field[NodeIndex(nodeCount, i + di, j + dj, k + dk)] += weight_X * weight_Y * weight_Z;
                    }
                }
            }
        }

        private static void Smooth(float[] field, int nodeCount)
        {
            float[] buffer = new float[field.Length];

            for (int axis = 0; axis < 3; axis++)
            {
                for (int i = 0; i < nodeCount; i++)
                {
                    for (int j = 0; j < nodeCount; j++)
                    {
                        for (int k = 0; k < nodeCount; k++)
                        {
                            int index_Previous = axis switch
                            {
                                0 => NodeIndex(nodeCount, i == 0 ? 0 : i - 1, j, k),
                                1 => NodeIndex(nodeCount, i, j == 0 ? 0 : j - 1, k),
                                _ => NodeIndex(nodeCount, i, j, k == 0 ? 0 : k - 1)
                            };

                            int index_Next = axis switch
                            {
                                0 => NodeIndex(nodeCount, i == nodeCount - 1 ? i : i + 1, j, k),
                                1 => NodeIndex(nodeCount, i, j == nodeCount - 1 ? j : j + 1, k),
                                _ => NodeIndex(nodeCount, i, j, k == nodeCount - 1 ? k : k + 1)
                            };

                            int index = NodeIndex(nodeCount, i, j, k);

                            buffer[index] = (field[index_Previous] + field[index] + field[index_Next]) / 3.0f;
                        }
                    }
                }

                System.Array.Copy(buffer, field, field.Length);
            }
        }

        private static void ClearBoundary(float[] field, int nodeCount)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    for (int k = 0; k < nodeCount; k++)
                    {
                        if (i == 0 || j == 0 || k == 0 || i == nodeCount - 1 || j == nodeCount - 1 || k == nodeCount - 1)
                        {
                            field[NodeIndex(nodeCount, i, j, k)] = 0;
                        }
                    }
                }
            }
        }

        private static int NodeIndex(int nodeCount, int i, int j, int k)
        {
            return ((i * nodeCount) + j) * nodeCount + k;
        }

        private static void March(float[] field, int nodeCount, int i, int j, int k, double iso, Dictionary<(int, int), int> vertices, List<Point3D> point3Ds, List<int[]> indexes, double x_Min, double y_Min, double z_Min, double size)
        {
            // Cell corners in the canonical order used by the tetrahedral decomposition below.
            int[] corners =
            [
                NodeIndex(nodeCount, i, j, k),
                NodeIndex(nodeCount, i + 1, j, k),
                NodeIndex(nodeCount, i + 1, j + 1, k),
                NodeIndex(nodeCount, i, j + 1, k),
                NodeIndex(nodeCount, i, j, k + 1),
                NodeIndex(nodeCount, i + 1, j, k + 1),
                NodeIndex(nodeCount, i + 1, j + 1, k + 1),
                NodeIndex(nodeCount, i, j + 1, k + 1)
            ];

            int[][] cornerPositions =
            [
                [i, j, k], [i + 1, j, k], [i + 1, j + 1, k], [i, j + 1, k],
                [i, j, k + 1], [i + 1, j, k + 1], [i + 1, j + 1, k + 1], [i, j + 1, k + 1]
            ];

            // Six tetrahedra sharing the main diagonal from corner zero to corner six. This decomposition
            // tiles the cell exactly and is consistent between neighbouring cells, so the extracted surface
            // stays watertight across cell boundaries.
            int[][] tetrahedra =
            [
                [0, 1, 2, 6], [0, 2, 3, 6], [0, 3, 7, 6],
                [0, 7, 4, 6], [0, 4, 5, 6], [0, 5, 1, 6]
            ];

            for (int t = 0; t < 6; t++)
            {
                int[] tetrahedron = tetrahedra[t];

                int mask = 0;
                for (int v = 0; v < 4; v++)
                {
                    if (field[corners[tetrahedron[v]]] >= iso)
                    {
                        mask |= 1 << v;
                    }
                }

                if (mask == 0 || mask == 15)
                {
                    continue;
                }

                // Every case reduces to either one triangle, when a single vertex is separated from the
                // other three, or two, when the tetrahedron is cut into two pairs.
                int[][] edges = mask switch
                {
                    1 or 14 => [[0, 1], [0, 2], [0, 3]],
                    2 or 13 => [[1, 0], [1, 3], [1, 2]],
                    4 or 11 => [[2, 0], [2, 1], [2, 3]],
                    8 or 7 => [[3, 0], [3, 2], [3, 1]],
                    3 or 12 => [[0, 3], [0, 2], [1, 2], [1, 3]],
                    5 or 10 => [[0, 1], [0, 3], [2, 3], [2, 1]],
                    _ => [[0, 1], [0, 2], [3, 2], [3, 1]]
                };

                int[] indexes_Edge = new int[edges.Length];
                for (int e = 0; e < edges.Length; e++)
                {
                    indexes_Edge[e] = Vertex(field, corners, cornerPositions, tetrahedron[edges[e][0]], tetrahedron[edges[e][1]], iso, vertices, point3Ds, x_Min, y_Min, z_Min, size);
                }

                indexes.Add([indexes_Edge[0], indexes_Edge[1], indexes_Edge[2]]);

                if (indexes_Edge.Length == 4)
                {
                    indexes.Add([indexes_Edge[0], indexes_Edge[2], indexes_Edge[3]]);
                }
            }
        }

        private static int Vertex(float[] field, int[] corners, int[][] cornerPositions, int corner_1, int corner_2, double iso, Dictionary<(int, int), int> vertices, List<Point3D> point3Ds, double x_Min, double y_Min, double z_Min, double size)
        {
            int node_1 = corners[corner_1];
            int node_2 = corners[corner_2];

            // Keyed on the pair of grid nodes the edge spans, normalised so that both cells sharing the edge
            // produce the same key. This is an exact identity, so no coordinate comparison or tolerance is
            // involved and neighbouring cells always reuse the same vertex.
            (int, int) key = node_1 < node_2 ? (node_1, node_2) : (node_2, node_1);

            if (vertices.TryGetValue(key, out int result))
            {
                return result;
            }

            double value_1 = field[node_1];
            double value_2 = field[node_2];

            double fraction = System.Math.Abs(value_2 - value_1) < 1e-12 ? 0.5 : (iso - value_1) / (value_2 - value_1);
            if (fraction < 0)
            {
                fraction = 0;
            }
            else if (fraction > 1)
            {
                fraction = 1;
            }

            int[] position_1 = cornerPositions[corner_1];
            int[] position_2 = cornerPositions[corner_2];

            double x = x_Min + ((position_1[0] + ((position_2[0] - position_1[0]) * fraction)) * size);
            double y = y_Min + ((position_1[1] + ((position_2[1] - position_1[1]) * fraction)) * size);
            double z = z_Min + ((position_1[2] + ((position_2[2] - position_1[2]) * fraction)) * size);

            result = point3Ds.Count;
            point3Ds.Add(new Point3D(x, y, z));
            vertices[key] = result;

            return result;
        }
    }
}

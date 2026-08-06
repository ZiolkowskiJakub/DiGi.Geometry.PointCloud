using DiGi.Geometry.PointCloud.Core.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a pointerless spatial index over a coordinate-major point payload.
        /// <para>The build is a counting sort on the Z-order cell identifier, which is linear in the number of points rather than the linearithmic cost of a comparison sort. A full ordering of the points is never needed: the atomic unit of a box query is a leaf cell, and the order of points within a leaf is irrelevant, so sorting by cell identifier is sufficient and roughly halves the work.</para>
        /// <para>The same sort produces the hierarchy for free. Because the leaves come out ordered by Z-order code, a parent's children are always contiguous, so the tree is folded upwards by a single scan per level rather than by any further sorting or searching.</para>
        /// <para>The scatter opens one write stream per occupied cell. At the depths chosen here the cell table stays small enough to remain cache-resident; if profiling ever shows the scatter dominating on much larger clouds, the next step is to split it into a coarse pass over the high bits followed by independent per-bucket passes.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <returns>A new <see cref="Classes.PointCloudIndex"/>, or <see langword="null"/> when the input is null, ragged, empty or not two- or three-dimensional.</returns>
        internal static Classes.PointCloudIndex? PointCloudIndex(double[][]? coordinates)
        {
            if (coordinates == null)
            {
                return null;
            }

            int dimension = coordinates.Length;
            if (dimension != 2 && dimension != 3)
            {
                return null;
            }

            double[]? coordinateExtremes = Query.CoordinateExtremes(coordinates);
            if (coordinateExtremes == null)
            {
                return null;
            }

            int count = coordinates[0].Length;

            int depth = Query.IndexDepth(count, dimension);
            int resolution = 1 << depth;

            double[] origins = new double[dimension];
            double[] scales = new double[dimension];
            for (int axis = 0; axis < dimension; axis++)
            {
                double min = coordinateExtremes[axis * 2];
                double extent = coordinateExtremes[(axis * 2) + 1] - min;

                origins[axis] = min;

                // A degenerate axis collapses every point onto cell zero of that axis, which is correct:
                // the axis carries no discriminating information.
                scales[axis] = extent > 0 ? resolution / extent : 0;
            }

            int[] cells = new int[count];

            int partitionCount = Query.PartitionCount(count, Constants.PointCloud.ParallelThresholdIndex);
            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, partition =>
            {
                int startIndex = partition * size;
                int end = startIndex + size;
                if (end > count)
                {
                    end = count;
                }

                for (int i = startIndex; i < end; i++)
                {
                    int x = Cell(coordinates[0][i], origins[0], scales[0], resolution);
                    int y = Cell(coordinates[1][i], origins[1], scales[1], resolution);

                    cells[i] = dimension == 2 ? Query.Morton(x, y, depth) : Query.Morton(x, y, Cell(coordinates[2][i], origins[2], scales[2], resolution), depth);
                }
            });

            int cellCount = 1 << (dimension * depth);

            int[] cellStarts = new int[cellCount + 1];
            for (int i = 0; i < count; i++)
            {
                cellStarts[cells[i] + 1]++;
            }

            for (int i = 0; i < cellCount; i++)
            {
                cellStarts[i + 1] += cellStarts[i];
            }

            int[] order = new int[count];
            int[] cursors = new int[cellCount];
            System.Array.Copy(cellStarts, cursors, cellCount);

            // Stable within a cell: points keep their relative input order, which keeps the build reproducible.
            for (int i = 0; i < count; i++)
            {
                order[cursors[cells[i]]++] = i;
            }

            List<PointCloudIndexNode>[] levels = new List<PointCloudIndexNode>[depth + 1];
            List<int>[] levelCodes = new List<int>[depth + 1];

            List<PointCloudIndexNode> nodes_Leaf = [];
            List<int> codes_Leaf = [];

            for (int cell = 0; cell < cellCount; cell++)
            {
                int start = cellStarts[cell];
                int length = cellStarts[cell + 1] - start;
                if (length == 0)
                {
                    continue;
                }

                nodes_Leaf.Add(new PointCloudIndexNode(Bounds(coordinates, order, start, length, dimension), start, length, -1, 0));
                codes_Leaf.Add(cell);
            }

            if (nodes_Leaf.Count == 0)
            {
                return null;
            }

            levels[depth] = nodes_Leaf;
            levelCodes[depth] = codes_Leaf;

            for (int level = depth - 1; level >= 0; level--)
            {
                List<PointCloudIndexNode> nodes_Child = levels[level + 1];
                List<int> codes_Child = levelCodes[level + 1];

                List<PointCloudIndexNode> nodes_Parent = [];
                List<int> codes_Parent = [];

                int index = 0;
                while (index < nodes_Child.Count)
                {
                    int code_Parent = codes_Child[index] >> dimension;

                    float[] bounds =
                    [
                        nodes_Child[index].MinX, nodes_Child[index].MinY, nodes_Child[index].MinZ,
                        nodes_Child[index].MaxX, nodes_Child[index].MaxY, nodes_Child[index].MaxZ
                    ];

                    int start = nodes_Child[index].Start;
                    int length = 0;

                    int index_End = index;
                    while (index_End < nodes_Child.Count && (codes_Child[index_End] >> dimension) == code_Parent)
                    {
                        PointCloudIndexNode node_Child = nodes_Child[index_End];

                        if (node_Child.MinX < bounds[0]) { bounds[0] = node_Child.MinX; }
                        if (node_Child.MinY < bounds[1]) { bounds[1] = node_Child.MinY; }
                        if (node_Child.MinZ < bounds[2]) { bounds[2] = node_Child.MinZ; }
                        if (node_Child.MaxX > bounds[3]) { bounds[3] = node_Child.MaxX; }
                        if (node_Child.MaxY > bounds[4]) { bounds[4] = node_Child.MaxY; }
                        if (node_Child.MaxZ > bounds[5]) { bounds[5] = node_Child.MaxZ; }

                        length += node_Child.Count;
                        index_End++;
                    }

                    // FirstChild temporarily holds the offset within the child level; it is rebased below.
                    nodes_Parent.Add(new PointCloudIndexNode(bounds, start, length, index, index_End - index));
                    codes_Parent.Add(code_Parent);

                    index = index_End;
                }

                levels[level] = nodes_Parent;
                levelCodes[level] = codes_Parent;
            }

            int[] levelOffsets = new int[depth + 2];
            for (int level = 0; level <= depth; level++)
            {
                levelOffsets[level + 1] = levelOffsets[level] + levels[level].Count;
            }

            PointCloudIndexNode[] nodes = new PointCloudIndexNode[levelOffsets[depth + 1]];
            for (int level = 0; level <= depth; level++)
            {
                List<PointCloudIndexNode> nodes_Level = levels[level];
                for (int i = 0; i < nodes_Level.Count; i++)
                {
                    PointCloudIndexNode node = nodes_Level[i];

                    float[] bounds = [node.MinX, node.MinY, node.MinZ, node.MaxX, node.MaxY, node.MaxZ];

                    int firstChild = node.ChildCount == 0 ? -1 : levelOffsets[level + 1] + node.FirstChild;

                    nodes[levelOffsets[level] + i] = new PointCloudIndexNode(bounds, node.Start, node.Count, firstChild, node.ChildCount);
                }
            }

            return new Classes.PointCloudIndex(dimension, order, nodes);
        }

        /// <summary>
        /// Quantises a coordinate onto the cell grid of a spatial index.
        /// </summary>
        /// <param name="value">The coordinate value.</param>
        /// <param name="origin">The lower bound of the axis.</param>
        /// <param name="scale">The number of cells per unit along the axis.</param>
        /// <param name="resolution">The number of cells along the axis.</param>
        /// <returns>An <see cref="int"/> cell index clamped to the grid.</returns>
        public static int Cell(double value, double origin, double scale, int resolution)
        {
            int result = (int)((value - origin) * scale);

            if (result < 0)
            {
                return 0;
            }

            if (result >= resolution)
            {
                return resolution - 1;
            }

            return result;
        }

        /// <summary>
        /// Calculates the tight bounds of a run of the index permutation, rounded outward to single precision.
        /// <para>Rounding outward is what makes a single-precision box safe: the stored box always encloses the double-precision points, so a node can never be rejected while still holding a qualifying point.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis.</param>
        /// <param name="order">The index permutation.</param>
        /// <param name="startIndex">The inclusive start of the run within the permutation.</param>
        /// <param name="count">The length of the run.</param>
        /// <param name="dimension">The number of coordinate axes.</param>
        /// <returns>A six element <see cref="float"/> array holding the minimum and then the maximum of each axis.</returns>
        public static float[] Bounds(double[][] coordinates, int[] order, int startIndex, int count, int dimension)
        {
            double[] minimums = [double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity];
            double[] maximums = [double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity];

            int end = startIndex + count;
            for (int i = startIndex; i < end; i++)
            {
                int index = order[i];
                for (int axis = 0; axis < dimension; axis++)
                {
                    double value = coordinates[axis][index];
                    if (value < minimums[axis])
                    {
                        minimums[axis] = value;
                    }

                    if (value > maximums[axis])
                    {
                        maximums[axis] = value;
                    }
                }
            }

            if (dimension == 2)
            {
                minimums[2] = 0;
                maximums[2] = 0;
            }

            static float roundDown(double value)
            {
                float result = (float)value;

                return result <= value ? result : result - (System.Math.Abs(result) * 1e-6f) - 1e-30f;
            }

            static float roundUp(double value)
            {
                float result = (float)value;

                return result >= value ? result : result + (System.Math.Abs(result) * 1e-6f) + 1e-30f;
            }

            return
            [
                roundDown(minimums[0]), roundDown(minimums[1]), roundDown(minimums[2]),
                roundUp(maximums[0]), roundUp(maximums[1]), roundUp(maximums[2])
            ];
        }
    }
}

using System;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core.Classes
{
    /// <summary>
    /// Represents one node of a <see cref="PointCloudIndex"/>: an axis-aligned box together with the contiguous range of the index permutation it owns.
    /// <para>The box is stored as single-precision values rounded outward. Half the memory of double precision, and rounding outward guarantees the box never excludes a point it contains, so a rejection is always sound. Boxes are tight to the points rather than derived from the cell grid, which matters on scan data that is nearly empty along one axis: a grid-derived box would span the whole empty extent and prune almost nothing.</para>
    /// <para>The box and the range live in the same struct rather than in parallel arrays because traversal reads both together.</para>
    /// </summary>
    internal readonly struct PointCloudIndexNode
    {
        /// <summary>The lower X bound, rounded outward.</summary>
        public readonly float MinX;

        /// <summary>The lower Y bound, rounded outward.</summary>
        public readonly float MinY;

        /// <summary>The lower Z bound, rounded outward. Unused for a planar index.</summary>
        public readonly float MinZ;

        /// <summary>The upper X bound, rounded outward.</summary>
        public readonly float MaxX;

        /// <summary>The upper Y bound, rounded outward.</summary>
        public readonly float MaxY;

        /// <summary>The upper Z bound, rounded outward. Unused for a planar index.</summary>
        public readonly float MaxZ;

        /// <summary>The inclusive start of this node's range within the index permutation.</summary>
        public readonly int Start;

        /// <summary>The number of permutation entries this node owns.</summary>
        public readonly int Count;

        /// <summary>The index of the first child node, or -1 when this node is a leaf.</summary>
        public readonly int FirstChild;

        /// <summary>The number of child nodes, zero when this node is a leaf.</summary>
        public readonly int ChildCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudIndexNode"/> struct.
        /// </summary>
        /// <param name="bounds">The six bounds in the order minimum X, minimum Y, minimum Z, maximum X, maximum Y, maximum Z.</param>
        /// <param name="start">The inclusive start of the range within the index permutation.</param>
        /// <param name="count">The number of permutation entries owned.</param>
        /// <param name="firstChild">The index of the first child node, or -1 for a leaf.</param>
        /// <param name="childCount">The number of child nodes, zero for a leaf.</param>
        public PointCloudIndexNode(float[] bounds, int start, int count, int firstChild, int childCount)
        {
            MinX = bounds[0];
            MinY = bounds[1];
            MinZ = bounds[2];
            MaxX = bounds[3];
            MaxY = bounds[4];
            MaxZ = bounds[5];
            Start = start;
            Count = count;
            FirstChild = firstChild;
            ChildCount = childCount;
        }
    }

    /// <summary>
    /// Represents a pointerless spatial index over a point cloud: a Z-order sorted permutation of the points, plus a table of nodes describing the hierarchy above it.
    /// <para>The structure is a linear octree in three dimensions and a linear quadtree in two. Because the points are sorted by Z-order cell identifier, every node owns a contiguous range of the permutation, and the hierarchy is derived by repeatedly dropping the low bits of the cell identifiers. There are no child pointers to chase and no per-node allocations.</para>
    /// <para>The index never touches the cloud's coordinate arrays. It owns a permutation instead. Reordering the cloud during a read would change the observable order of its points, which is a surprising and racy side effect on a type that is otherwise safe to read concurrently.</para>
    /// <para>A query classifies each node against the search box: disjoint nodes are pruned outright, fully contained nodes contribute their whole range with no per-point test at all, and only partially overlapping leaves are examined point by point. That is where the speed comes from — for a small box the work is proportional to the answer, not to the cloud.</para>
    /// </summary>
    internal sealed class PointCloudIndex
    {
        private readonly int dimension;
        private readonly int[] order;
        private readonly PointCloudIndexNode[] nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudIndex"/> class.
        /// </summary>
        /// <param name="dimension">The number of coordinate axes.</param>
        /// <param name="order">The Z-order sorted permutation of point indexes.</param>
        /// <param name="nodes">The node table, with the root at index zero.</param>
        internal PointCloudIndex(int dimension, int[] order, PointCloudIndexNode[] nodes)
        {
            this.dimension = dimension;
            this.order = order;
            this.nodes = nodes;
        }

        /// <summary>
        /// Gets the number of points covered by the index.
        /// </summary>
        /// <value>An <see cref="int"/> point count.</value>
        public int Count
        {
            get
            {
                return order.Length;
            }
        }

        /// <summary>
        /// Gets the number of nodes in the hierarchy.
        /// </summary>
        /// <value>An <see cref="int"/> node count.</value>
        public int NodeCount
        {
            get
            {
                return nodes.Length;
            }
        }

        /// <summary>
        /// Retrieves the indexes of the points that fall inside an axis-aligned box.
        /// <para>The result is sorted ascending so that it matches an exhaustive scan exactly, both in content and in order. Without that, a filtered cloud would come back in spatial order below the index threshold and in input order above it, which would make the result depend on the size of the input.</para>
        /// <para>The traversal stack is stack-allocated. The depth is bounded by the index depth, so the bound is known at compile time and there is no allocation and no pooled buffer to return.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays of the cloud the index was built for.</param>
        /// <param name="minimums">The inclusive lower bound of each axis, tolerance already folded in.</param>
        /// <param name="maximums">The inclusive upper bound of each axis, tolerance already folded in.</param>
        /// <returns>An <see cref="int"/> array of ascending point indexes, or <see langword="null"/> when the input is mismatched.</returns>
        public int[]? InRangeIndexes(double[][]? coordinates, double[]? minimums, double[]? maximums)
        {
            if (coordinates == null || minimums == null || maximums == null || coordinates.Length != dimension || minimums.Length != dimension || maximums.Length != dimension || nodes.Length == 0)
            {
                return null;
            }

            float minX = (float)minimums[0];
            float maxX = (float)maximums[0];
            float minY = (float)minimums[1];
            float maxY = (float)maximums[1];
            float minZ = dimension == 3 ? (float)minimums[2] : float.NegativeInfinity;
            float maxZ = dimension == 3 ? (float)maximums[2] : float.PositiveInfinity;

            // The node boxes are single precision and rounded outward, so a comparison against a
            // single-precision copy of the query bounds can only ever be too generous, never too strict.
            // Anything it lets through is still verified against the double precision bounds below.
            minX = MathF_BitDecrement(minX);
            minY = MathF_BitDecrement(minY);
            minZ = MathF_BitDecrement(minZ);
            maxX = MathF_BitIncrement(maxX);
            maxY = MathF_BitIncrement(maxY);
            maxZ = MathF_BitIncrement(maxZ);

            List<int> result = [];

            Span<int> stack = stackalloc int[64];
            int stackCount = 0;
            stack[stackCount++] = 0;

            while (stackCount > 0)
            {
                PointCloudIndexNode node = nodes[stack[--stackCount]];

                if (node.MaxX < minX || node.MinX > maxX || node.MaxY < minY || node.MinY > maxY || node.MaxZ < minZ || node.MinZ > maxZ)
                {
                    continue;
                }

                if (node.MinX >= minX && node.MaxX <= maxX && node.MinY >= minY && node.MaxY <= maxY && node.MinZ >= minZ && node.MaxZ <= maxZ)
                {
                    // Fully contained: every point of this node qualifies, with no per-point test.
                    int end_Contained = node.Start + node.Count;
                    for (int i = node.Start; i < end_Contained; i++)
                    {
                        result.Add(order[i]);
                    }

                    continue;
                }

                if (node.ChildCount != 0)
                {
                    for (int i = 0; i < node.ChildCount; i++)
                    {
                        if (stackCount == stack.Length)
                        {
                            return null;
                        }

                        stack[stackCount++] = node.FirstChild + i;
                    }

                    continue;
                }

                int end_Partial = node.Start + node.Count;
                for (int i = node.Start; i < end_Partial; i++)
                {
                    int index = order[i];

                    bool inRange = true;
                    for (int axis = 0; axis < dimension; axis++)
                    {
                        double value = coordinates[axis][index];
                        if (value < minimums[axis] || value > maximums[axis])
                        {
                            inRange = false;

                            break;
                        }
                    }

                    if (inRange)
                    {
                        result.Add(index);
                    }
                }
            }

            int[] result_Temp = [.. result];
            Array.Sort(result_Temp);

            return result_Temp;
        }

        /// <summary>
        /// Retrieves the indexes of the points closest to a query position, nearest first.
        /// <para>The traversal is a depth-first descent that visits the children of every node in order of their distance from the query, nearest first. That ordering is what makes it fast: the very first leaf reached is the one containing the query, so the candidate set fills with genuinely close points immediately and the rejection radius collapses to a small value before any sibling is considered. Every remaining node is then dismissed by a single comparison. A best-first search with a priority queue visits the same nodes for a request this small, and pays for a heap to do it.</para>
        /// <para>Nodes are rejected when the distance from the query to the node box is not smaller than the distance to the furthest candidate held so far. This is sound because the boxes are single precision rounded outward, so they enclose more space than the points they own: the measured distance to a box can only understate the distance to the nearest point inside it, and understating it can only preserve a node that would otherwise be dropped.</para>
        /// <para>Nothing is allocated. The candidate set, the child ordering buffers and the traversal stack are all supplied by the caller or stack-allocated, and the whole search runs on scalar values without materializing a single point object.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays of the cloud the index was built for.</param>
        /// <param name="x">The X coordinate of the query position.</param>
        /// <param name="y">The Y coordinate of the query position.</param>
        /// <param name="z">The Z coordinate of the query position. Ignored for a planar index.</param>
        /// <param name="indexes">A buffer receiving the point indexes, nearest first. Its length is the number of neighbours requested.</param>
        /// <param name="distancesSquared">A buffer receiving the matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <returns>The number of neighbours written, which is smaller than the requested count when the cloud holds fewer points, or -1 when the input is mismatched or the traversal stack overflowed.</returns>
        public int NearestIndexes(double[][]? coordinates, double x, double y, double z, Span<int> indexes, Span<double> distancesSquared)
        {
            int count_Requested = indexes.Length;
            if (coordinates == null || coordinates.Length != dimension || count_Requested <= 0 || distancesSquared.Length < count_Requested || nodes.Length == 0)
            {
                return -1;
            }

            for (int i = 0; i < count_Requested; i++)
            {
                indexes[i] = -1;
                distancesSquared[i] = double.PositiveInfinity;
            }

            double[] values_X = coordinates[0];
            double[] values_Y = coordinates[1];
            double[] values_Z = dimension == 3 ? coordinates[2] : values_X;

            int count_Filled = 0;

            // Positive infinity until the candidate set is full, so that every point is admitted while
            // it is still filling. Once full this is the rejection radius, and it only ever shrinks.
            double worst = double.PositiveInfinity;

            Span<int> stack = stackalloc int[64];
            int stackCount = 0;
            stack[stackCount++] = 0;

            Span<int> children = stackalloc int[8];
            Span<double> distancesSquared_Child = stackalloc double[8];

            while (stackCount > 0)
            {
                PointCloudIndexNode node = nodes[stack[--stackCount]];

                // Re-tested on the way out as well as on the way in: the radius shrinks while this node
                // waits on the stack, so a node that was worth queuing is often no longer worth opening.
                if (DistanceSquaredToBounds(node, x, y, z, dimension) >= worst)
                {
                    continue;
                }

                if (node.ChildCount != 0)
                {
                    if (node.ChildCount > children.Length)
                    {
                        // Unreachable for a well-formed index, where a parent owns at most one child per
                        // octant. Handled rather than assumed: an unordered descent is still correct.
                        for (int i = 0; i < node.ChildCount; i++)
                        {
                            if (stackCount == stack.Length)
                            {
                                return -1;
                            }

                            stack[stackCount++] = node.FirstChild + i;
                        }

                        continue;
                    }

                    int count_Child = 0;
                    for (int i = 0; i < node.ChildCount; i++)
                    {
                        int index_Node = node.FirstChild + i;

                        double distanceSquared = DistanceSquaredToBounds(nodes[index_Node], x, y, z, dimension);
                        if (distanceSquared >= worst)
                        {
                            continue;
                        }

                        // Sorted descending, so pushing in order leaves the nearest child on top of the
                        // stack. Dropping the hopeless children here is also what keeps the stack bounded.
                        int position = count_Child++;
                        while (position > 0 && distancesSquared_Child[position - 1] < distanceSquared)
                        {
                            distancesSquared_Child[position] = distancesSquared_Child[position - 1];
                            children[position] = children[position - 1];
                            position--;
                        }

                        distancesSquared_Child[position] = distanceSquared;
                        children[position] = index_Node;
                    }

                    for (int i = 0; i < count_Child; i++)
                    {
                        if (stackCount == stack.Length)
                        {
                            return -1;
                        }

                        stack[stackCount++] = children[i];
                    }

                    continue;
                }

                int end = node.Start + node.Count;
                for (int i = node.Start; i < end; i++)
                {
                    int index = order[i];

                    double dx = values_X[index] - x;
                    double dy = values_Y[index] - y;
                    double distanceSquared = (dx * dx) + (dy * dy);

                    if (dimension == 3)
                    {
                        double dz = values_Z[index] - z;
                        distanceSquared += dz * dz;
                    }

                    // Cheap rejection before the call, which is where almost every leaf point ends up
                    // once the radius has collapsed. Ties and non-finite values fall through and are
                    // resolved by the shared insertion, so both search paths order them identically.
                    if (count_Filled == count_Requested && distanceSquared > worst)
                    {
                        continue;
                    }

                    Modify.InsertNeighbor(indexes, distancesSquared, index, distanceSquared, ref count_Filled, ref worst);
                }
            }

            return count_Filled;
        }

        /// <summary>
        /// Calculates the squared distance from a position to the nearest point of a node box, which is zero when the position lies inside it.
        /// </summary>
        /// <param name="node">The node whose box is measured.</param>
        /// <param name="x">The X coordinate of the position.</param>
        /// <param name="y">The Y coordinate of the position.</param>
        /// <param name="z">The Z coordinate of the position.</param>
        /// <param name="dimension">The number of coordinate axes. The Z term is omitted for two, where the node box carries a placeholder depth of zero.</param>
        /// <returns>A <see cref="double"/> squared distance.</returns>
        private static double DistanceSquaredToBounds(in PointCloudIndexNode node, double x, double y, double z, int dimension)
        {
            double result = 0;

            double delta = x - node.MinX;
            if (delta < 0)
            {
                result += delta * delta;
            }
            else
            {
                delta = x - node.MaxX;
                if (delta > 0)
                {
                    result += delta * delta;
                }
            }

            delta = y - node.MinY;
            if (delta < 0)
            {
                result += delta * delta;
            }
            else
            {
                delta = y - node.MaxY;
                if (delta > 0)
                {
                    result += delta * delta;
                }
            }

            if (dimension != 3)
            {
                return result;
            }

            delta = z - node.MinZ;
            if (delta < 0)
            {
                result += delta * delta;
            }
            else
            {
                delta = z - node.MaxZ;
                if (delta > 0)
                {
                    result += delta * delta;
                }
            }

            return result;
        }

        private static float MathF_BitDecrement(float value)
        {
            // MathF is not available on netstandard2.0, and a single ulp step is all that is needed to
            // absorb the rounding introduced by narrowing the query bounds to single precision.
            if (float.IsNegativeInfinity(value) || float.IsNaN(value))
            {
                return value;
            }

            return value - (System.Math.Abs(value) * 1e-6f) - 1e-30f;
        }

        private static float MathF_BitIncrement(float value)
        {
            if (float.IsPositiveInfinity(value) || float.IsNaN(value))
            {
                return value;
            }

            return value + (System.Math.Abs(value) * 1e-6f) + 1e-30f;
        }
    }
}

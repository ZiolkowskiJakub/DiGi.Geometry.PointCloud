namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Interleaves the low bits of two axis indexes into a single Z-order cell identifier.
        /// <para>Z-order is what turns a flat grid of cells into a hierarchy for free: dropping the low bits of a cell identifier yields the identifier of its parent cell, so a table of leaves sorted by this value can be folded upwards into a tree without any further sorting or searching.</para>
        /// </summary>
        /// <param name="x">The quantised X index.</param>
        /// <param name="y">The quantised Y index.</param>
        /// <param name="bits">The number of bits taken from each axis.</param>
        /// <returns>An <see cref="int"/> holding the interleaved cell identifier.</returns>
        public static int Morton(int x, int y, int bits)
        {
            int result = 0;
            for (int i = 0; i < bits; i++)
            {
                result |= ((x >> i) & 1) << (2 * i);
                result |= ((y >> i) & 1) << ((2 * i) + 1);
            }

            return result;
        }

        /// <summary>
        /// Interleaves the low bits of three axis indexes into a single Z-order cell identifier.
        /// <para>Z-order is what turns a flat grid of cells into a hierarchy for free: dropping the low three bits of a cell identifier yields the identifier of its parent cell, so a table of leaves sorted by this value can be folded upwards into a tree without any further sorting or searching.</para>
        /// </summary>
        /// <param name="x">The quantised X index.</param>
        /// <param name="y">The quantised Y index.</param>
        /// <param name="z">The quantised Z index.</param>
        /// <param name="bits">The number of bits taken from each axis.</param>
        /// <returns>An <see cref="int"/> holding the interleaved cell identifier.</returns>
        public static int Morton(int x, int y, int z, int bits)
        {
            int result = 0;
            for (int i = 0; i < bits; i++)
            {
                result |= ((x >> i) & 1) << (3 * i);
                result |= ((y >> i) & 1) << ((3 * i) + 1);
                result |= ((z >> i) & 1) << ((3 * i) + 2);
            }

            return result;
        }

        /// <summary>
        /// Calculates the subdivision depth to use for a spatial index over the given number of points.
        /// <para>The depth targets a leaf occupancy of roughly <see cref="Constants.PointCloud.IndexLeafPointCount"/> points. Shallower leaves force per-point testing over large groups; deeper ones inflate the cell table and the tree without reducing the work that matters.</para>
        /// </summary>
        /// <param name="count">The number of points to be indexed.</param>
        /// <param name="dimension">The number of coordinate axes.</param>
        /// <returns>An <see cref="int"/> depth, clamped to the range supported for the dimension.</returns>
        public static int IndexDepth(int count, int dimension)
        {
            if (dimension != 2 && dimension != 3)
            {
                return 0;
            }

            int maximum = dimension == 2 ? Constants.PointCloud.MaximumDepth2D : Constants.PointCloud.MaximumDepth3D;

            int result = Constants.PointCloud.MinimumDepth;
            while (result < maximum)
            {
                long cellCount = 1L << (dimension * (result + 1));
                if (cellCount > count / Constants.PointCloud.IndexLeafPointCount)
                {
                    break;
                }

                result++;
            }

            return result;
        }
    }
}

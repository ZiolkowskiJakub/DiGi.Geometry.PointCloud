namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a compacted copy of a coordinate-major payload holding only the points that fall inside an axis-aligned box.
        /// <para>Two passes rather than a growing buffer: the vectorised counting pass sizes the result exactly, then a single compaction pass fills it. That means one allocation per axis and no copying on growth, which matters because any array beyond about eighty-five kilobytes lands on the large object heap and repeated growth would fragment it.</para>
        /// <para>The compaction pass is deliberately scalar. It is branchy and memory-bound, and there is no portable compress instruction on this target, so vectorising it would add complexity for no gain.</para>
        /// <para>The bounds are expected to already include any tolerance.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="minimums">The inclusive lower bound of each axis.</param>
        /// <param name="maximums">The inclusive upper bound of each axis.</param>
        /// <returns>A new jagged <see cref="double"/> array holding the points inside the box, or <see langword="null"/> when the input is invalid or no point qualifies.</returns>
        public static double[][]? CoordinatesInRange(double[][]? coordinates, double[]? minimums, double[]? maximums)
        {
            int count_InRange = Query.InRangeCount(coordinates, minimums, maximums);
            if (count_InRange <= 0)
            {
                return null;
            }

            int dimension = coordinates!.Length;
            int count = coordinates[0]!.Length;

            double[][] result = new double[dimension][];
            for (int axis = 0; axis < dimension; axis++)
            {
                result[axis] = new double[count_InRange];
            }

            int index_Result = 0;
            for (int i = 0; i < count; i++)
            {
                bool inRange = true;
                for (int axis = 0; axis < dimension; axis++)
                {
                    double value = coordinates[axis]![i];
                    if (value < minimums![axis] || value > maximums![axis])
                    {
                        inRange = false;

                        break;
                    }
                }

                if (!inRange)
                {
                    continue;
                }

                for (int axis = 0; axis < dimension; axis++)
                {
                    result[axis][index_Result] = coordinates[axis]![i];
                }

                index_Result++;
            }

            return result;
        }
    }
}

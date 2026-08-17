namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the number of coordinate axes of a payload, together with its point count, when the payload is rectangular.
        /// <para>Reports zero for a ragged payload rather than the length of the outer array, so that a single check answers both "how many axes" and "is this usable at all".</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="count">When this method returns, contains the number of points, or zero when the payload is not rectangular.</param>
        /// <returns>The number of axes, or zero when the payload is null, empty or ragged.</returns>
        public static int Dimension(double[][]? coordinates, out int count)
        {
            count = 0;

            if (coordinates == null || coordinates.Length == 0)
            {
                return 0;
            }

            int dimension = coordinates.Length;

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return 0;
            }

            int count_Temp = values_First.Length;
            for (int axis = 1; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != count_Temp)
                {
                    return 0;
                }
            }

            count = count_Temp;

            return dimension;
        }
    }
}

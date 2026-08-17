namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Retrieves the indexes of the points whose every coordinate is finite.
        /// <para>This is the permutation carrying form of <see cref="FiniteCoordinates(double[][])"/>. Filtering non-finite points changes the point count, so anything stored alongside the coordinates has to be compacted by the same permutation; returning the indexes rather than the coordinates is what lets one filter drive both.</para>
        /// <para>The predicate matches <see cref="FiniteCoordinates(double[][])"/> exactly, so the two produce the same points in the same order.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <returns>An ascending <see cref="int"/> array of zero-based point indexes, or <see langword="null"/> when the input is null, ragged, or contains no finite point.</returns>
        public static int[]? FiniteIndexes(double[][]? coordinates)
        {
            if (coordinates == null || coordinates.Length == 0)
            {
                return null;
            }

            int dimension = coordinates.Length;

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return null;
            }

            int count = values_First.Length;
            for (int axis = 1; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != count)
                {
                    return null;
                }
            }

            bool isFinite(int index)
            {
                for (int axis = 0; axis < dimension; axis++)
                {
                    double value = coordinates[axis]![index];

                    // double.IsFinite is not available on netstandard2.0.
                    if (double.IsNaN(value) || double.IsInfinity(value))
                    {
                        return false;
                    }
                }

                return true;
            }

            int count_Finite = 0;
            for (int i = 0; i < count; i++)
            {
                if (isFinite(i))
                {
                    count_Finite++;
                }
            }

            if (count_Finite == 0)
            {
                return null;
            }

            int[] result = new int[count_Finite];

            int index_Result = 0;
            for (int i = 0; i < count; i++)
            {
                if (!isFinite(i))
                {
                    continue;
                }

                result[index_Result] = i;
                index_Result++;
            }

            return result;
        }
    }
}

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the points carrying a given model object identifier.
        /// <para>Returned in ascending order, so the result can drive <see cref="Create.GatheredCoordinates(double[][], int[])"/> directly and the extracted sub-cloud keeps the point order of its source.</para>
        /// </summary>
        /// <param name="referenceIndexes">The per-point identifiers, one per point.</param>
        /// <param name="referenceIndex">The identifier to select, where a negative value selects the points that link to nothing.</param>
        /// <returns>An ascending <see cref="int"/> array of zero-based point indexes, or <see langword="null"/> when the input is null or no point carries the identifier.</returns>
        public static int[]? PointIndexes(int[]? referenceIndexes, int referenceIndex)
        {
            if (referenceIndexes == null)
            {
                return null;
            }

            bool matches(int value)
            {
                return referenceIndex < 0 ? value < 0 : value == referenceIndex;
            }

            int count = 0;
            for (int i = 0; i < referenceIndexes.Length; i++)
            {
                if (matches(referenceIndexes[i]))
                {
                    count++;
                }
            }

            if (count == 0)
            {
                return null;
            }

            int[] result = new int[count];

            int index_Result = 0;
            for (int i = 0; i < referenceIndexes.Length; i++)
            {
                if (!matches(referenceIndexes[i]))
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

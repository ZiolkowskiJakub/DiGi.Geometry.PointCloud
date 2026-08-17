using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a new per-point identifier array holding the identifiers of the points named by a permutation, in the order the permutation names them.
        /// <para>This is the counterpart of <see cref="GatheredCoordinates(double[][], int[])"/> and must be driven by the same permutation. Gathering the coordinates without gathering the identifiers is what turns a filter into silent data corruption: the result keeps its point count and its table, so nothing looks wrong, while every point after the first discarded one is attributed to the wrong model object.</para>
        /// <para>The reference table itself is NOT rebuilt. Identifiers stay stable under filtering, so a table entry that no longer has any point simply goes unused, which costs one unread entry and keeps every surviving identifier valid.</para>
        /// </summary>
        /// <param name="referenceIndexes">The per-point identifiers, one per point of the source cloud.</param>
        /// <param name="indexes">The zero-based point indexes to gather.</param>
        /// <returns>A new <see cref="int"/> array holding the gathered identifiers, or <see langword="null"/> when either input is null, empty, or names a point that does not exist.</returns>
        public static int[]? GatheredReferenceIndexes(int[]? referenceIndexes, int[]? indexes)
        {
            if (referenceIndexes == null || indexes == null || indexes.Length == 0)
            {
                return null;
            }

            int[] result = new int[indexes.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                int index = indexes[i];
                if (index < 0 || index >= referenceIndexes.Length)
                {
                    return null;
                }

                result[i] = referenceIndexes[index];
            }

            return result;
        }

        /// <summary>
        /// Builds a new per-point identifier array holding the identifiers of the points named by a permutation, in the order the permutation names them.
        /// </summary>
        /// <param name="referenceIndexes">The per-point identifiers, one per point of the source cloud.</param>
        /// <param name="indexes">The zero-based point indexes to gather.</param>
        /// <returns>A new <see cref="int"/> array holding the gathered identifiers, or <see langword="null"/> when either input is null, empty, or names a point that does not exist.</returns>
        public static int[]? GatheredReferenceIndexes(int[]? referenceIndexes, IReadOnlyList<int>? indexes)
        {
            if (referenceIndexes == null || indexes == null || indexes.Count == 0)
            {
                return null;
            }

            int[] result = new int[indexes.Count];
            for (int i = 0; i < indexes.Count; i++)
            {
                int index = indexes[i];
                if (index < 0 || index >= referenceIndexes.Length)
                {
                    return null;
                }

                result[i] = referenceIndexes[index];
            }

            return result;
        }
    }
}

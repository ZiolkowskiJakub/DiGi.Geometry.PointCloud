using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a new coordinate payload holding the points named by a permutation, in the order the permutation names them.
        /// <para>This is the single gather used by every filter that changes the point count, so that the coordinates and anything stored alongside them are compacted by one shared routine and cannot drift apart.</para>
        /// <para>An out of range index yields <see langword="null"/> rather than a partly filled result, because a filter that quietly returned fewer or wrong points would be discovered as corrupted geometry much later.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="indexes">The zero-based point indexes to gather.</param>
        /// <returns>A new jagged <see cref="double"/> array holding the gathered points, or <see langword="null"/> when the input is null, ragged, empty, or names a point that does not exist.</returns>
        public static double[][]? GatheredCoordinates(double[][]? coordinates, int[]? indexes)
        {
            if (indexes == null)
            {
                return null;
            }

            int dimension = Query.Dimension(coordinates, out int count);
            if (dimension == 0 || indexes.Length == 0)
            {
                return null;
            }

            double[][] result = new double[dimension][];
            for (int axis = 0; axis < dimension; axis++)
            {
                double[] values_Source = coordinates![axis]!;

                double[] values = new double[indexes.Length];
                for (int i = 0; i < indexes.Length; i++)
                {
                    int index = indexes[i];
                    if (index < 0 || index >= count)
                    {
                        return null;
                    }

                    values[i] = values_Source[index];
                }

                result[axis] = values;
            }

            return result;
        }

        /// <summary>
        /// Builds a new coordinate payload holding the points named by a permutation, in the order the permutation names them.
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="indexes">The zero-based point indexes to gather.</param>
        /// <returns>A new jagged <see cref="double"/> array holding the gathered points, or <see langword="null"/> when the input is null, ragged, empty, or names a point that does not exist.</returns>
        public static double[][]? GatheredCoordinates(double[][]? coordinates, IReadOnlyList<int>? indexes)
        {
            if (indexes == null)
            {
                return null;
            }

            int dimension = Query.Dimension(coordinates, out int count);
            if (dimension == 0 || indexes.Count == 0)
            {
                return null;
            }

            double[][] result = new double[dimension][];
            for (int axis = 0; axis < dimension; axis++)
            {
                double[] values_Source = coordinates![axis]!;

                double[] values = new double[indexes.Count];
                for (int i = 0; i < indexes.Count; i++)
                {
                    int index = indexes[i];
                    if (index < 0 || index >= count)
                    {
                        return null;
                    }

                    values[i] = values_Source[index];
                }

                result[axis] = values;
            }

            return result;
        }
    }
}

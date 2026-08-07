namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Calculates the number of points held by a coordinate-major payload, verifying that it is rectangular.
        /// <para>A payload whose axis arrays differ in length has no meaningful point count, so the ragged case is reported rather than silently answered from the first axis.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis.</param>
        /// <returns>The number of points, or -1 when the payload is null, ragged or holds an unsupported number of axes.</returns>
        public static int PointCount(this double[][]? coordinates)
        {
            if (coordinates == null)
            {
                return -1;
            }

            int dimension = coordinates.Length;
            if (dimension < 2 || dimension > 3)
            {
                return -1;
            }

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return -1;
            }

            int result = values_First.Length;
            for (int axis = 1; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != result)
                {
                    return -1;
                }
            }

            return result;
        }
    }
}

using System;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Builds a compacted copy of the supplied coordinate arrays holding only those points whose every coordinate is finite.
        /// <para>This filtering is not cosmetic. The vectorised minimum and maximum reduction lowers to hardware instructions that return their second operand when either operand is not a number, whereas the scalar equivalent propagates it. A single such value therefore makes the vectorised and scalar bounding boxes disagree in a way that depends on how the data happens to align to vector lanes. Scan data routinely contains these values, so they are removed once, at construction, rather than guarded against on every read.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <returns>A new jagged <see cref="double"/> array holding only finite points, or <see langword="null"/> when the input is null, ragged, or contains no finite point.</returns>
        public static double[][]? FiniteCoordinates(double[][]? coordinates)
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

            double[][] result = new double[dimension][];
            for (int axis = 0; axis < dimension; axis++)
            {
                result[axis] = new double[count_Finite];
            }

            if (count_Finite == count)
            {
                for (int axis = 0; axis < dimension; axis++)
                {
                    Array.Copy(coordinates[axis]!, result[axis], count);
                }

                return result;
            }

            int index_Result = 0;
            for (int i = 0; i < count; i++)
            {
                if (!isFinite(i))
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

using System.Numerics;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Finds the smallest and largest value in a contiguous range of a coordinate array using a vectorised reduction.
        /// <para>The vectorised path processes <see cref="Vector{T}.Count"/> values per iteration and finishes with a scalar tail, so it is correct for any range length. The lane width is read at runtime and never assumed.</para>
        /// <para>IMPORTANT: the values are assumed to be finite. <see cref="Vector.Min{T}(Vector{T}, Vector{T})"/> and <see cref="Vector.Max{T}(Vector{T}, Vector{T})"/> lower to hardware instructions that return their second operand when either operand is not a number, whereas <see cref="System.Math.Min(double, double)"/> propagates it. A single such value therefore makes the vectorised and scalar results disagree in a way that depends on lane alignment. Point cloud factories filter non-finite coordinates before construction for exactly this reason.</para>
        /// </summary>
        /// <param name="values">The coordinate array to scan.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of values in the range.</param>
        /// <param name="min">When this method returns, contains the smallest value in the range, or <see cref="double.NaN"/> when the range is invalid.</param>
        /// <param name="max">When this method returns, contains the largest value in the range, or <see cref="double.NaN"/> when the range is invalid.</param>
        /// <returns><see langword="true"/> when the range was scanned; otherwise <see langword="false"/>.</returns>
        public static bool MinMax(this double[]? values, int startIndex, int count, out double min, out double max)
        {
            min = double.NaN;
            max = double.NaN;

            if (values == null || count <= 0 || startIndex < 0 || startIndex > values.Length - count)
            {
                return false;
            }

            int index = startIndex;
            int end = startIndex + count;

            double min_Temp = double.PositiveInfinity;
            double max_Temp = double.NegativeInfinity;

            int width = Vector<double>.Count;
            if (Vector.IsHardwareAccelerated && width > 1 && count >= width + width)
            {
                Vector<double> vector_Min = new(double.PositiveInfinity);
                Vector<double> vector_Max = new(double.NegativeInfinity);

                int end_Vector = end - width;
                for (; index <= end_Vector; index += width)
                {
                    Vector<double> vector = new(values, index);
                    vector_Min = Vector.Min(vector_Min, vector);
                    vector_Max = Vector.Max(vector_Max, vector);
                }

                for (int i = 0; i < width; i++)
                {
                    double value_Min = vector_Min[i];
                    if (value_Min < min_Temp)
                    {
                        min_Temp = value_Min;
                    }

                    double value_Max = vector_Max[i];
                    if (value_Max > max_Temp)
                    {
                        max_Temp = value_Max;
                    }
                }
            }

            for (; index < end; index++)
            {
                double value = values[index];
                if (value < min_Temp)
                {
                    min_Temp = value;
                }

                if (value > max_Temp)
                {
                    max_Temp = value;
                }
            }

            min = min_Temp;
            max = max_Temp;

            return true;
        }

        /// <summary>
        /// Finds the smallest and largest value in a coordinate array using a vectorised reduction.
        /// <para>See the ranged overload for the non-finite value caveat.</para>
        /// </summary>
        /// <param name="values">The coordinate array to scan.</param>
        /// <param name="min">When this method returns, contains the smallest value, or <see cref="double.NaN"/> when the array is null or empty.</param>
        /// <param name="max">When this method returns, contains the largest value, or <see cref="double.NaN"/> when the array is null or empty.</param>
        /// <returns><see langword="true"/> when the array was scanned; otherwise <see langword="false"/>.</returns>
        public static bool MinMax(this double[]? values, out double min, out double max)
        {
            if (values == null)
            {
                min = double.NaN;
                max = double.NaN;

                return false;
            }

            return MinMax(values, 0, values.Length, out min, out max);
        }
    }
}

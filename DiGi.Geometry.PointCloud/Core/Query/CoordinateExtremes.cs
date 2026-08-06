using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Calculates the smallest and largest value on every axis of a coordinate-major point payload.
        /// <para>Each partition accumulates into method-local variables and writes its result slot exactly once at the end, so there is no lock, no concurrent collection and no measurable false sharing. Padding the result slots would be ceremony: a single store per partition for the whole pass cannot contend.</para>
        /// <para>The parallel and serial paths produce bit-identical results, because minimum and maximum are exact and associative. Do not assume the same of a sum or a mean.</para>
        /// <para>Streaming passes use only a fraction of the available processors: memory bandwidth saturates well before every core is busy, and the surplus threads add scheduling cost and no throughput.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <returns>A <see cref="double"/> array holding the minimum and maximum of each axis in turn, or <see langword="null"/> when the input is null, ragged or empty.</returns>
        public static double[]? CoordinateExtremes(this double[][]? coordinates)
        {
            if (coordinates == null || coordinates.Length == 0)
            {
                return null;
            }

            int dimension = coordinates.Length;

            double[]? values_First = coordinates[0];
            if (values_First == null || values_First.Length == 0)
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

            double[] result = new double[dimension * 2];

            int partitionCount = PartitionCount(count, Constants.PointCloud.ParallelThreshold, Constants.PointCloud.StreamingProcessorFraction);

            for (int axis = 0; axis < dimension; axis++)
            {
                double[] values = coordinates[axis]!;

                if (partitionCount <= 1)
                {
                    if (!MinMax(values, out double min, out double max))
                    {
                        return null;
                    }

                    result[axis * 2] = min;
                    result[(axis * 2) + 1] = max;

                    continue;
                }

                double[] minimums = new double[partitionCount];
                double[] maximums = new double[partitionCount];

                int size = ((count - 1) / partitionCount) + 1;

                Parallel.For(0, partitionCount, i =>
                {
                    int startIndex = i * size;
                    int length = count - startIndex;
                    if (length > size)
                    {
                        length = size;
                    }

                    if (length <= 0 || !MinMax(values, startIndex, length, out double min_Partition, out double max_Partition))
                    {
                        min_Partition = double.PositiveInfinity;
                        max_Partition = double.NegativeInfinity;
                    }

                    minimums[i] = min_Partition;
                    maximums[i] = max_Partition;
                });

                double min_Total = double.PositiveInfinity;
                double max_Total = double.NegativeInfinity;

                for (int i = 0; i < partitionCount; i++)
                {
                    if (minimums[i] < min_Total)
                    {
                        min_Total = minimums[i];
                    }

                    if (maximums[i] > max_Total)
                    {
                        max_Total = maximums[i];
                    }
                }

                result[axis * 2] = min_Total;
                result[(axis * 2) + 1] = max_Total;
            }

            return result;
        }
    }
}

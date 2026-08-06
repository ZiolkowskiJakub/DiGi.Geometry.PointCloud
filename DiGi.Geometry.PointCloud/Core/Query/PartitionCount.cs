namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Calculates how many partitions a workload of the given size should be split into.
        /// <para>The result never exceeds the processor budget implied by <paramref name="processorFraction"/>, and never creates a partition smaller than <paramref name="minimumPartitionSize"/>, so small workloads collapse to a single serial partition instead of paying dispatch cost.</para>
        /// </summary>
        /// <param name="count">The total number of elements to process.</param>
        /// <param name="minimumPartitionSize">The smallest worthwhile partition size. Values of zero or less are treated as one.</param>
        /// <param name="processorFraction">The fraction of available processors to use. Use a value below one for memory-bound streaming passes, which saturate well before every core is busy.</param>
        /// <returns>An <see cref="int"/> partition count of at least one.</returns>
        public static int PartitionCount(int count, int minimumPartitionSize, double processorFraction = 1.0)
        {
            if (count <= 0)
            {
                return 1;
            }

            if (minimumPartitionSize <= 0)
            {
                minimumPartitionSize = 1;
            }

            int result = count / minimumPartitionSize;
            if (result <= 1)
            {
                return 1;
            }

            int processorCount = DiGi.Core.Query.ProcessorCount(processorFraction);
            if (result > processorCount)
            {
                result = processorCount;
            }

            return result;
        }
    }
}

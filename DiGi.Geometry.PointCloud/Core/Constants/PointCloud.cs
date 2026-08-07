namespace DiGi.Geometry.PointCloud.Core.Constants
{
    /// <summary>
    /// Provides tuning constants for <see cref="Classes.PointCloud"/> processing.
    /// <para>The thresholds are calibrated for a desktop-class CPU with dual-channel DDR5 memory. They trade a small amount of peak throughput for predictable behaviour on small inputs, where fan-out and index construction cost more than the work they save.</para>
    /// </summary>
    public static class PointCloud
    {
        /// <summary>
        /// The minimum point count at which bulk coordinate passes (bounding box, move, transform, filter) are worth parallelising.
        /// <para>A 32-way <c>Parallel.For</c> dispatch costs roughly 5-20 microseconds. Serial SIMD streaming runs at roughly 10-12 GB/s, so keeping dispatch below ten percent of the work requires about 200 microseconds of serial work, which is approximately this many three-dimensional points.</para>
        /// </summary>
        public const int ParallelThreshold = 100000;

        /// <summary>
        /// The minimum point count at which spatial index construction is worth parallelising.
        /// <para>Lower than <see cref="ParallelThreshold"/> because index construction performs more work per point than a streaming pass.</para>
        /// </summary>
        public const int ParallelThresholdIndex = 50000;

        /// <summary>
        /// The minimum number of query points at which a batch nearest neighbour search is worth parallelising.
        /// <para>An indexed descent for a single query costs roughly one to three microseconds, so a partition of this size carries well over a millisecond of work against a dispatch cost of 5-20 microseconds. Unlike the streaming passes this workload is latency bound rather than bandwidth bound, so it is partitioned across every processor rather than the streaming fraction.</para>
        /// </summary>
        public const int ParallelThresholdNeighbor = 1024;

        /// <summary>
        /// The number of nearest neighbours collected when a non-degenerate triple is required.
        /// <para>Three points are enough only when they are not collinear, which fails whenever the query sits on a scan line or a grid line of the source data. Collecting this many candidates lets the triangle factory step past a degenerate pair without a second traversal: the extra neighbours come from leaves that are already resident, so the additional cost is a handful of comparisons.</para>
        /// </summary>
        public const int MaximumNeighborCandidateCount = 8;

        /// <summary>
        /// The minimum point count at which building a spatial index is worthwhile.
        /// <para>Below this size a vectorised brute-force scan completes in tens of microseconds, which is cheaper than any index build. Callers should skip index construction entirely below this threshold.</para>
        /// </summary>
        public const int IndexThreshold = 65536;

        /// <summary>
        /// The target number of points per spatial index leaf, used to derive the index depth from the point count.
        /// </summary>
        public const int IndexLeafPointCount = 64;

        /// <summary>
        /// The maximum spatial index depth in two dimensions, bounding the cell table at four raised to this power.
        /// </summary>
        public const int MaximumDepth2D = 11;

        /// <summary>
        /// The maximum spatial index depth in three dimensions, bounding the cell table at eight raised to this power.
        /// </summary>
        public const int MaximumDepth3D = 7;

        /// <summary>
        /// The minimum spatial index depth, below which a single flat cell list performs as well as a hierarchy.
        /// </summary>
        public const int MinimumDepth = 2;

        /// <summary>
        /// The number of coarse buckets used by the most-significant-digit pass of the index counting sort.
        /// <para>Chosen so the number of concurrently open write streams per thread stays inside the write-combining and translation-lookaside-buffer budget. A single-level scatter over the full cell table would open hundreds of thousands of streams and dominate runtime.</para>
        /// </summary>
        public const int IndexBucketCount = 256;

        /// <summary>
        /// The fraction of available processors to use for memory-bound streaming passes.
        /// <para>Dual-channel DDR5 saturates at roughly eight to twelve threads. Additional threads contribute scheduling cost and no bandwidth.</para>
        /// </summary>
        public const double StreamingProcessorFraction = 0.5;

        /// <summary>
        /// The length in bytes of the fixed header of the binary point cloud format.
        /// </summary>
        public const int BinaryHeaderLength = 32;

        /// <summary>
        /// The version number written into the header of the binary point cloud format.
        /// </summary>
        public const int BinaryVersion = 1;

        /// <summary>
        /// The first byte of the four byte magic identifier of the binary point cloud format, spelling "DGPC".
        /// </summary>
        public const byte BinaryMagic_0 = (byte)'D';

        /// <summary>
        /// The second byte of the four byte magic identifier of the binary point cloud format.
        /// </summary>
        public const byte BinaryMagic_1 = (byte)'G';

        /// <summary>
        /// The third byte of the four byte magic identifier of the binary point cloud format.
        /// </summary>
        public const byte BinaryMagic_2 = (byte)'P';

        /// <summary>
        /// The fourth byte of the four byte magic identifier of the binary point cloud format.
        /// </summary>
        public const byte BinaryMagic_3 = (byte)'C';
    }
}

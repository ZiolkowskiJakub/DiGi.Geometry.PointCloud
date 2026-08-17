using DiGi.Core.Classes;
using DiGi.Geometry.PointCloud.Core.Interfaces;
using System;
using System.Threading;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Geometry.PointCloud.Core.Classes
{
    /// <summary>
    /// Represents an abstract, dimension-agnostic cloud of points stored one array per coordinate axis.
    /// <para>The layout is deliberately coordinate-major rather than a list of point objects. A point object derived from <see cref="Geometry.Core.Classes.Coordinate"/> is a heap object wrapping its own array, which costs roughly eighty bytes and two allocations per point; ten million points would occupy about eight hundred megabytes across twenty million objects that the garbage collector must trace. Three plain <see cref="double"/> arrays hold the same data in about two hundred and forty megabytes across three allocations, and because a <see cref="double"/> array contains no references the collector marks the header and never walks the payload.</para>
    /// <para>The layout is also what makes vectorisation possible: four consecutive values in an axis array are four different points' values for that axis, which is exactly the shape a lane-wise minimum or comparison needs. Interleaved storage would mix axes within a lane, and point objects are not contiguous at all.</para>
    /// <para>This diverges from <see cref="Geometry.Core.Classes.Mesh{TPoint}"/> on purpose. A mesh is random-access and topology-driven at thousands to a million vertices, where the object-per-vertex cost is irrelevant and per-vertex behaviour is required. A cloud is bulk and streaming at millions to billions of points with no topology at all.</para>
    /// <para>Instances are safe for concurrent reads. Mutation through a move or transform requires external synchronization, the same contract as a standard list.</para>
    /// </summary>
    public abstract class PointCloud : SerializableObject, IPointCloud
    {
        /// <summary>
        /// The coordinate arrays, one per axis and all of equal length. Index zero is X, index one is Y and index two, when present, is Z.
        /// <para>Not marked for serialization: the reflection serializer emits an array member as one JSON number per element, which for a large cloud would produce tens of millions of JSON value objects. The payload travels through <see cref="CoordinateData"/> instead.</para>
        /// <para>Cannot be readonly, because <see cref="CoordinateData"/> assigns it from its property setter during deserialization.</para>
        /// </summary>
        protected double[][]? coordinates;

        /// <summary>
        /// The number of coordinate axes, fixed by the concrete type at construction.
        /// <para>Held as a field rather than an abstract property because the JSON constructor must know the dimension before it deserializes, and calling an overridable member from a constructor is a defect the analyzers flag.</para>
        /// </summary>
        protected readonly int dimension;

        /// <summary>
        /// The cached spatial index, derived data that is rebuilt on demand and never serialized.
        /// <para>A plain field with no serialization attribute, which the reflection serializer skips: fields are opt-in, unlike properties, which are opt-out.</para>
        /// </summary>
        private PointCloudIndex? pointCloudIndex;

        /// <summary>
        /// Guards construction of the cached spatial index.
        /// <para>A plain object rather than the dedicated lock type introduced in recent framework versions, which does not exist on this target.</para>
        /// </summary>
        private readonly object object_PointCloudIndexLock = new();

        /// <summary>
        /// Initializes a new empty instance of the <see cref="PointCloud"/> class.
        /// </summary>
        /// <param name="dimension">The number of coordinate axes.</param>
        protected PointCloud(int dimension)
            : base()
        {
            this.dimension = dimension;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud"/> class by copying the coordinate arrays of an existing cloud.
        /// </summary>
        /// <param name="pointCloud">The cloud to copy from. This value can be null.</param>
        /// <param name="dimension">The number of coordinate axes.</param>
        protected PointCloud(PointCloud? pointCloud, int dimension)
            : base(pointCloud)
        {
            this.dimension = dimension;
            coordinates = CloneCoordinates(pointCloud?.coordinates, dimension);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud"/> class from a <see cref="JsonObject"/>.
        /// <para>The dimension is assigned before deserialization runs, mirroring <see cref="Geometry.Core.Classes.Coordinate(JsonObject, int)"/>, because the coordinate payload cannot be validated without it.</para>
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> holding the serialized cloud. This value can be null.</param>
        /// <param name="dimension">The number of coordinate axes.</param>
        protected PointCloud(JsonObject? jsonObject, int dimension)
            : base()
        {
            this.dimension = dimension;
            FromJsonObject(jsonObject);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud"/> class from prebuilt coordinate arrays.
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="dimension">The number of coordinate axes.</param>
        /// <param name="clone">When <see langword="true"/>, the arrays are defensively copied; when <see langword="false"/>, they are adopted directly. Use <see langword="false"/> only when the caller owns freshly created arrays that are not shared, which is the whole point of the filtering paths.</param>
        protected PointCloud(double[][]? coordinates, int dimension, bool clone)
            : base()
        {
            this.dimension = dimension;

            if (clone)
            {
                this.coordinates = CloneCoordinates(coordinates, dimension);
            }
            else if (IsRectangular(coordinates, dimension))
            {
                this.coordinates = coordinates;
            }
        }

        /// <summary>
        /// Gets or sets the serialized coordinate payload as a Base64 encoding of the binary point cloud format.
        /// <para>This member exists in this exact shape because of how the reflection serializer works. An array member would be written as one JSON number per element; a get-only property would be written but silently discarded on read, yielding an empty object with no error. A settable property carrying a single string is the only shape that round-trips.</para>
        /// <para>A Base64 payload is roughly three times smaller than a JSON number array and needs no number parsing, but it still materializes the whole cloud as a string. Use the binary conversion helpers for anything beyond a few million points.</para>
        /// </summary>
        [JsonPropertyName(nameof(CoordinateData))]
        private string? CoordinateData
        {
            get
            {
                byte[]? bytes = Convert.ToSystem_Bytes(coordinates);
                if (bytes == null)
                {
                    return null;
                }

                return System.Convert.ToBase64String(bytes);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    coordinates = null;

                    return;
                }

                byte[] bytes;
                try
                {
                    bytes = System.Convert.FromBase64String(value!);
                }
                catch (FormatException)
                {
                    coordinates = null;

                    return;
                }

                coordinates = Create.Coordinates(bytes, dimension);
            }
        }

        /// <summary>
        /// Gets the number of points in the cloud.
        /// <para>Returns zero rather than a negative sentinel when the cloud holds no data, so that a counted loop becomes a no-op and an allocation sized from this value succeeds. This deliberately differs from <see cref="Geometry.Core.Classes.Mesh{TPoint}.PointsCount"/>.</para>
        /// </summary>
        /// <value>An <see cref="int"/> point count of zero or more.</value>
        [JsonIgnore]
        public int Count
        {
            get
            {
                if (coordinates == null || coordinates.Length == 0)
                {
                    return 0;
                }

                double[]? values = coordinates[0];
                if (values == null)
                {
                    return 0;
                }

                return values.Length;
            }
        }

        /// <summary>
        /// Gets the number of coordinate axes.
        /// </summary>
        /// <value>An <see cref="int"/> equal to two for a planar cloud and three for a spatial one.</value>
        [JsonIgnore]
        public int Dimension
        {
            get
            {
                return dimension;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a spatial index is currently cached for this cloud.
        /// </summary>
        /// <value><see langword="true"/> when an index has been built and not since invalidated.</value>
        [JsonIgnore]
        public bool IsIndexed
        {
            get
            {
                return Volatile.Read(ref pointCloudIndex) != null;
            }
        }

        /// <summary>
        /// Returns the cached spatial index, building it on first use.
        /// <para>Built lazily rather than in a constructor, because constructing one is an order-of-count sweep and a caller who never runs a spatial query should not pay for it. Clouds below <see cref="Constants.PointCloud.IndexThreshold"/> points never get an index at all: an exhaustive vectorised scan over that many points finishes in tens of microseconds, which is less than any index build could cost.</para>
        /// <para>Concurrent readers are safe. The double-checked read means the common case takes no lock, and losing the race merely means one redundant build is discarded.</para>
        /// </summary>
        /// <returns>The cached <see cref="PointCloudIndex"/>, or <see langword="null"/> when the cloud is too small or cannot be indexed.</returns>
        internal PointCloudIndex? EnsureIndex()
        {
            if (coordinates == null || Count < Constants.PointCloud.IndexThreshold)
            {
                return null;
            }

            PointCloudIndex? result = Volatile.Read(ref pointCloudIndex);
            if (result != null)
            {
                return result;
            }

            lock (object_PointCloudIndexLock)
            {
                result = pointCloudIndex;
                if (result == null)
                {
                    result = Create.PointCloudIndex(coordinates);
                    Volatile.Write(ref pointCloudIndex, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Discards the cached spatial index.
        /// <para>Every mutation of the coordinate arrays MUST call this. An index describes where the points were, so a move or a transform that left it in place would silently answer later queries against stale geometry.</para>
        /// </summary>
        protected void InvalidateIndex()
        {
            lock (object_PointCloudIndexLock)
            {
                Volatile.Write(ref pointCloudIndex, null);
            }
        }

        /// <summary>
        /// Returns a read-only view over the coordinate array for a single axis, without copying.
        /// <para>This is the allocation-free way to hand an axis to a vectorised or streaming kernel. A span cannot be stored in a field, so the cloud itself continues to hold plain arrays and produces spans on demand.</para>
        /// </summary>
        /// <param name="axis">The zero-based axis index, where zero is X, one is Y and two is Z.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> over the axis, or an empty span when the cloud is empty or the axis is out of range.</returns>
        public ReadOnlySpan<double> AsSpan(int axis)
        {
            if (coordinates == null || axis < 0 || axis >= coordinates.Length)
            {
                return default;
            }

            return coordinates[axis];
        }

        /// <summary>
        /// Returns a read-only view over a contiguous range of the coordinate array for a single axis, without copying.
        /// </summary>
        /// <param name="axis">The zero-based axis index, where zero is X, one is Y and two is Z.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of values in the range.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> over the range, or an empty span when the cloud is empty or the range is out of bounds.</returns>
        public ReadOnlySpan<double> AsSpan(int axis, int startIndex, int count)
        {
            if (coordinates == null || axis < 0 || axis >= coordinates.Length)
            {
                return default;
            }

            double[]? values = coordinates[axis];
            if (values == null || startIndex < 0 || count < 0 || startIndex > values.Length - count)
            {
                return default;
            }

            return new ReadOnlySpan<double>(values, startIndex, count);
        }

        /// <summary>
        /// Retrieves the coordinate array for a single axis.
        /// </summary>
        /// <param name="axis">The zero-based axis index, where zero is X, one is Y and two is Z.</param>
        /// <returns>A copy of the axis array, or <see langword="null"/> when the cloud is empty or the axis is out of range.</returns>
        public double[]? GetCoordinates(int axis)
        {
            return GetCoordinates(axis, true);
        }

        /// <summary>
        /// Retrieves the coordinate array for a single axis, optionally without copying.
        /// </summary>
        /// <param name="axis">The zero-based axis index, where zero is X, one is Y and two is Z.</param>
        /// <param name="clone">When <see langword="true"/>, a copy is returned; when <see langword="false"/>, the internal array is returned directly and must not be modified by the caller.</param>
        /// <returns>The axis array, or <see langword="null"/> when the cloud is empty or the axis is out of range.</returns>
        public double[]? GetCoordinates(int axis, bool clone)
        {
            if (coordinates == null || axis < 0 || axis >= coordinates.Length)
            {
                return null;
            }

            double[]? values = coordinates[axis];
            if (values == null || !clone)
            {
                return values;
            }

            double[] result = new double[values.Length];
            Array.Copy(values, result, values.Length);

            return result;
        }

        /// <summary>
        /// Retrieves every coordinate array, optionally without copying.
        /// </summary>
        /// <param name="clone">When <see langword="true"/>, a deep copy is returned; when <see langword="false"/>, the internal arrays are returned directly and must not be modified by the caller.</param>
        /// <returns>A jagged <see cref="double"/> array holding one array per axis, or <see langword="null"/> when the cloud is empty.</returns>
        public double[][]? GetCoordinates(bool clone)
        {
            if (!clone)
            {
                return coordinates;
            }

            return CloneCoordinates(coordinates, dimension);
        }

        /// <summary>
        /// Retrieves a single coordinate value without allocating.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <param name="axis">The zero-based axis index, where zero is X, one is Y and two is Z.</param>
        /// <param name="value">When this method returns, contains the coordinate value, or <see cref="double.NaN"/> when the request is out of range.</param>
        /// <returns><see langword="true"/> when the value was retrieved; otherwise <see langword="false"/>.</returns>
        public bool TryGetCoordinate(int index, int axis, out double value)
        {
            value = double.NaN;

            if (coordinates == null || axis < 0 || axis >= coordinates.Length)
            {
                return false;
            }

            double[]? values = coordinates[axis];
            if (values == null || index < 0 || index >= values.Length)
            {
                return false;
            }

            value = values[index];

            return true;
        }

        private static bool IsRectangular(double[][]? coordinates, int dimension)
        {
            if (coordinates == null || coordinates.Length != dimension || dimension <= 0)
            {
                return false;
            }

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return false;
            }

            for (int i = 1; i < dimension; i++)
            {
                double[]? values = coordinates[i];
                if (values == null || values.Length != values_First.Length)
                {
                    return false;
                }
            }

            return true;
        }

        private static double[][]? CloneCoordinates(double[][]? coordinates, int dimension)
        {
            if (!IsRectangular(coordinates, dimension))
            {
                return null;
            }

            double[][] result = new double[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                double[] values = coordinates![i];

                double[] values_Temp = new double[values.Length];
                Array.Copy(values, values_Temp, values.Length);
                result[i] = values_Temp;
            }

            return result;
        }
    }
}

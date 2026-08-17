using DiGi.Core.Interfaces;
using DiGi.Geometry.PointCloud.Core.Classes;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Geometry.PointCloud.Spatial.Classes
{
    /// <summary>
    /// Represents a cloud of three-dimensional points in which each point carries the identifier of the DiGi model object it belongs to.
    /// <para>The link is stored as one <see cref="int"/> per point indexing into a <see cref="Core.Classes.PointCloudReferenceCollection"/>, which costs four bytes per point. Storing a reference object per point instead would cost well over a hundred bytes and one garbage collected object per point, reproducing exactly the overhead that <see cref="Core.Classes.PointCloud"/> exists to avoid.</para>
    /// <para>An identifier of -1 marks a point that links to nothing, so an unsegmented point needs no table entry and no sentinel object.</para>
    /// <para>Every inherited member is safe to use. <see cref="PointCloud3D.Move(Geometry.Spatial.Classes.Vector3D)"/> and <see cref="PointCloud3D.Transform(Geometry.Spatial.Interfaces.ITransform3D)"/> preserve both the count and the order of the points, so the identifiers continue to line up; the nearest and counting queries return indexes rather than clouds, and an index into this cloud is an index into its identifiers.</para>
    /// <para>WARNING: extension methods bind statically. Assigning this cloud to a variable typed <see cref="PointCloud3D"/> and calling a filter on it selects the overload declared for the base type, which builds a plain <see cref="PointCloud3D"/> and silently drops the links. Keep the variable typed as this class wherever the links matter.</para>
    /// </summary>
    public class ReferencedPointCloud3D : PointCloud3D
    {
        /// <summary>
        /// The per-point identifiers, one per point and in point order, where -1 marks a point that links to nothing.
        /// <para>Not marked for serialization: an array member would be written as one JSON number per element. The payload travels through <see cref="ReferenceIndexData"/> instead, exactly as the coordinates travel through their own encoded property.</para>
        /// <para>Cannot be readonly, because <see cref="ReferenceIndexData"/> assigns it from its property setter during deserialization.</para>
        /// </summary>
        private int[]? referenceIndexes;

        /// <summary>
        /// The distinct model objects this cloud links to, indexed by the identifiers.
        /// <para>Serialized as an ordinary member rather than folded into the encoded payload, so that the references keep their concrete types through the polymorphic type discriminator and stay readable in the document.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PointCloudReferenceCollection))]
        private readonly PointCloudReferenceCollection? pointCloudReferenceCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedPointCloud3D"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> holding the serialized cloud.</param>
        public ReferencedPointCloud3D(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedPointCloud3D"/> class by copying an existing cloud.
        /// <para>The identifiers and the reference table are both copied, not shared. This is what lets a filter return the source cloud unchanged when its query covers everything, without the copy and the source aliasing one table.</para>
        /// </summary>
        /// <param name="referencedPointCloud3D">The cloud to copy from.</param>
        public ReferencedPointCloud3D(ReferencedPointCloud3D? referencedPointCloud3D)
            : base(referencedPointCloud3D)
        {
            if (referencedPointCloud3D == null)
            {
                return;
            }

            int[]? referenceIndexes_Source = referencedPointCloud3D.referenceIndexes;
            if (referenceIndexes_Source != null)
            {
                referenceIndexes = new int[referenceIndexes_Source.Length];
                Array.Copy(referenceIndexes_Source, referenceIndexes, referenceIndexes_Source.Length);
            }

            pointCloudReferenceCollection = DiGi.Core.Query.Clone(referencedPointCloud3D.pointCloudReferenceCollection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedPointCloud3D"/> class by copying three coordinate arrays and their per-point identifiers.
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="z">The Z coordinates.</param>
        /// <param name="referenceIndexes">The per-point identifiers, which must hold one value per point.</param>
        /// <param name="pointCloudReferenceCollection">The reference table the identifiers index into.</param>
        public ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, PointCloudReferenceCollection? pointCloudReferenceCollection)
            : this(x, y, z, referenceIndexes, pointCloudReferenceCollection, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedPointCloud3D"/> class from three prebuilt coordinate arrays and their per-point identifiers.
        /// <para>The identifiers are attached only when they hold exactly one value per point. A mismatched array is dropped rather than adopted, mirroring the way a ragged coordinate array is dropped, because a cloud with no links is recoverable while a cloud whose links are offset by one silently attributes every point to the wrong model object.</para>
        /// </summary>
        /// <param name="x">The X coordinates.</param>
        /// <param name="y">The Y coordinates.</param>
        /// <param name="z">The Z coordinates.</param>
        /// <param name="referenceIndexes">The per-point identifiers, which must hold one value per point.</param>
        /// <param name="pointCloudReferenceCollection">The reference table the identifiers index into.</param>
        /// <param name="clone">When <see langword="true"/>, the arrays are defensively copied; when <see langword="false"/>, they are adopted directly. Use <see langword="false"/> only when the caller owns freshly created arrays that are not shared.</param>
        internal ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, PointCloudReferenceCollection? pointCloudReferenceCollection, bool clone)
            : base(x, y, z, clone)
        {
            if (referenceIndexes == null || referenceIndexes.Length != Count)
            {
                return;
            }

            if (clone)
            {
                this.referenceIndexes = new int[referenceIndexes.Length];
                Array.Copy(referenceIndexes, this.referenceIndexes, referenceIndexes.Length);
            }
            else
            {
                this.referenceIndexes = referenceIndexes;
            }

            this.pointCloudReferenceCollection = clone ? DiGi.Core.Query.Clone(pointCloudReferenceCollection) : pointCloudReferenceCollection;
        }

        /// <summary>
        /// Gets or sets the serialized identifier payload as a Base64 encoding of the binary point cloud reference format.
        /// <para>This member exists in this exact shape for the same reason as the coordinate payload: the reflection serializer writes an array member as one JSON number per element, and a get-only property is written but silently discarded on read.</para>
        /// <para>The reference table is NOT embedded here, because it is already serialized as its own member and writing it twice would let the two copies disagree.</para>
        /// </summary>
        [JsonPropertyName(nameof(ReferenceIndexData))]
        private string? ReferenceIndexData
        {
            get
            {
                byte[]? bytes = Core.Convert.ToSystem_Bytes(referenceIndexes, null);
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
                    referenceIndexes = null;

                    return;
                }

                byte[] bytes;
                try
                {
                    bytes = System.Convert.FromBase64String(value!);
                }
                catch (FormatException)
                {
                    referenceIndexes = null;

                    return;
                }

                referenceIndexes = Core.Create.ReferenceIndexes(bytes);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the identifiers are present and line up with the points.
        /// <para>The two halves of the cloud are decoded independently, and the serializer applies members in the order they appear in the document, so this is the check that a document did in fact carry both halves and that they agree.</para>
        /// </summary>
        /// <value><see langword="true"/> when one identifier is stored per point.</value>
        [JsonIgnore]
        public bool IsReferenced
        {
            get
            {
                return referenceIndexes != null && referenceIndexes.Length == Count;
            }
        }

        /// <summary>
        /// Gets the distinct model objects this cloud links to, indexed by the identifiers.
        /// <para>Returns a copy. Use <see cref="GetPointCloudReferenceCollection(bool)"/> when the copy is not wanted.</para>
        /// </summary>
        /// <value>The reference table, or <see langword="null"/> when the cloud carries none.</value>
        [JsonIgnore]
        public PointCloudReferenceCollection? PointCloudReferenceCollection
        {
            get
            {
                return GetPointCloudReferenceCollection(true);
            }
        }

        /// <summary>
        /// Gets the number of distinct model objects this cloud links to.
        /// </summary>
        /// <value>An <see cref="int"/> entry count of zero or more.</value>
        [JsonIgnore]
        public int ReferenceCount
        {
            get
            {
                return pointCloudReferenceCollection == null ? 0 : pointCloudReferenceCollection.Count;
            }
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A new <see cref="ISerializableObject"/> instance that is a clone of the current object.</returns>
        public override ISerializableObject? Clone()
        {
            return new ReferencedPointCloud3D(this);
        }

        /// <summary>
        /// Retrieves the reference table, optionally without copying.
        /// </summary>
        /// <param name="clone">When <see langword="true"/>, a copy is returned; when <see langword="false"/>, the internal table is returned directly and must not be modified by the caller.</param>
        /// <returns>The reference table, or <see langword="null"/> when the cloud carries none.</returns>
        public PointCloudReferenceCollection? GetPointCloudReferenceCollection(bool clone)
        {
            if (pointCloudReferenceCollection == null || !clone)
            {
                return pointCloudReferenceCollection;
            }

            return DiGi.Core.Query.Clone(pointCloudReferenceCollection);
        }

        /// <summary>
        /// Retrieves the model object a point links to.
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <returns>A copy of the reference, or <see langword="null"/> when the index is out of range or the point links to nothing.</returns>
        public ISerializableReference? GetReference(int index)
        {
            if (!TryGetReferenceIndex(index, out int referenceIndex) || pointCloudReferenceCollection == null)
            {
                return null;
            }

            return pointCloudReferenceCollection.GetReference(referenceIndex);
        }

        /// <summary>
        /// Retrieves the per-point identifiers.
        /// </summary>
        /// <returns>A copy of the identifiers, or <see langword="null"/> when the cloud carries none.</returns>
        public int[]? GetReferenceIndexes()
        {
            return GetReferenceIndexes(true);
        }

        /// <summary>
        /// Retrieves the per-point identifiers, optionally without copying.
        /// </summary>
        /// <param name="clone">When <see langword="true"/>, a copy is returned; when <see langword="false"/>, the internal array is returned directly and must not be modified by the caller.</param>
        /// <returns>The identifiers, or <see langword="null"/> when the cloud carries none.</returns>
        public int[]? GetReferenceIndexes(bool clone)
        {
            if (referenceIndexes == null || !clone)
            {
                return referenceIndexes;
            }

            int[] result = new int[referenceIndexes.Length];
            Array.Copy(referenceIndexes, result, referenceIndexes.Length);

            return result;
        }

        /// <summary>
        /// Retrieves the identifier of the model object a point links to, without allocating.
        /// <para>The index is checked against the identifier array rather than against the point count, so that a document carrying inconsistent halves reports a miss instead of reading past the end.</para>
        /// </summary>
        /// <param name="index">The zero-based point index.</param>
        /// <param name="referenceIndex">When this method returns, contains the identifier, or -1 when the index is out of range or the point links to nothing.</param>
        /// <returns><see langword="true"/> when the point links to a model object; otherwise <see langword="false"/>.</returns>
        public bool TryGetReferenceIndex(int index, out int referenceIndex)
        {
            referenceIndex = -1;

            if (referenceIndexes == null || index < 0 || index >= referenceIndexes.Length)
            {
                return false;
            }

            int referenceIndex_Temp = referenceIndexes[index];
            if (referenceIndex_Temp < 0)
            {
                return false;
            }

            referenceIndex = referenceIndex_Temp;

            return true;
        }
    }
}

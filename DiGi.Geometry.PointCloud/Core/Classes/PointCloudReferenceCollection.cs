using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.Geometry.PointCloud.Core.Classes
{
    /// <summary>
    /// Represents the distinct model objects a point cloud links to, held as an ordered table in which the position of an entry is its identifier.
    /// <para>The table exists so that a link costs four bytes per point instead of a reference object per point. A reference is a heap object carrying a type reference and a cached string, so one per point would reproduce exactly the cost that <see cref="PointCloud"/> is built to avoid; a table holds one entry per distinct model object, which is thousands rather than millions.</para>
    /// <para>The table is add-free by design. Nothing can remove or reorder an entry, because a point cloud stores only the position of an entry and any shift would silently repoint every linked point at the wrong model object. Build the table once through <see cref="Create.PointCloudReferenceCollection(IEnumerable{ISerializableReference})"/>, or let a cloud factory build it while it assigns identifiers.</para>
    /// <para>Entries are typed <see cref="ISerializableReference"/> rather than <see cref="IUniqueReference"/> on purpose. <see cref="ComplexReference"/> implements <see cref="IComplexReference"/>, which does not derive from <see cref="IUniqueReference"/>, so the narrower interface would exclude the composite references that identify a component within a building within a county.</para>
    /// </summary>
    public class PointCloudReferenceCollection : SerializableObject
    {
        /// <summary>
        /// The reference table, where the index of an entry is its identifier.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(References))]
        private readonly List<ISerializableReference> references = [];

        /// <summary>
        /// The cached reverse lookup from reference to identifier, derived data that is rebuilt on demand and never serialized.
        /// <para>Built lazily rather than in a constructor for the same reason as <see cref="PointCloud.EnsureIndex"/>: constructing it is an order-of-count sweep and a caller who only ever resolves identifiers forwards should not pay for it.</para>
        /// <para>A plain field with no serialization attribute, which the reflection serializer skips: fields are opt-in, unlike properties, which are opt-out.</para>
        /// </summary>
        private Dictionary<ISerializableReference, int>? dictionary;

        /// <summary>
        /// Guards construction of the cached reverse lookup.
        /// </summary>
        private readonly object object_DictionaryLock = new();

        /// <summary>
        /// Initializes a new empty instance of the <see cref="PointCloudReferenceCollection"/> class.
        /// </summary>
        public PointCloudReferenceCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudReferenceCollection"/> class from a sequence of references.
        /// <para>The sequence is copied in order and null entries are skipped, but it is NOT checked for duplicates; a duplicate would occupy two identifiers and quietly split one model object in two. Use <see cref="Create.PointCloudReferenceCollection(IEnumerable{ISerializableReference})"/> to remove duplicates first.</para>
        /// </summary>
        /// <param name="references">The references to store, in identifier order. This value can be null.</param>
        public PointCloudReferenceCollection(IEnumerable<ISerializableReference>? references)
            : base()
        {
            if (references == null)
            {
                return;
            }

            foreach (ISerializableReference reference in references)
            {
                if (reference == null)
                {
                    continue;
                }

                ISerializableReference? reference_Temp = DiGi.Core.Query.Clone(reference);
                if (reference_Temp == null)
                {
                    continue;
                }

                this.references.Add(reference_Temp);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudReferenceCollection"/> class by copying an existing table.
        /// <para>Entries are cloned one by one so that the copy shares no reference object with the source, and identifiers are preserved because the order is preserved.</para>
        /// </summary>
        /// <param name="pointCloudReferenceCollection">The table to copy from. This value can be null.</param>
        public PointCloudReferenceCollection(PointCloudReferenceCollection? pointCloudReferenceCollection)
            : base(pointCloudReferenceCollection)
        {
            if (pointCloudReferenceCollection?.references == null)
            {
                return;
            }

            foreach (ISerializableReference reference in pointCloudReferenceCollection.references)
            {
                if (reference == null)
                {
                    continue;
                }

                ISerializableReference? reference_Temp = DiGi.Core.Query.Clone(reference);
                if (reference_Temp == null)
                {
                    continue;
                }

                references.Add(reference_Temp);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloudReferenceCollection"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> holding the serialized table. This value can be null.</param>
        public PointCloudReferenceCollection(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of entries in the table.
        /// </summary>
        /// <value>An <see cref="int"/> entry count of zero or more.</value>
        [JsonIgnore]
        public int Count
        {
            get
            {
                return references.Count;
            }
        }

        /// <summary>
        /// Gets every entry in the table, in identifier order.
        /// <para>Returns a copy, because handing out the internal list would let a caller reorder or remove an entry and silently repoint every linked point at the wrong model object. Use <see cref="GetReferences(bool)"/> when the copy is not wanted.</para>
        /// </summary>
        /// <value>A <see cref="List{T}"/> of references, or <see langword="null"/> when the table is empty.</value>
        [JsonIgnore]
        public List<ISerializableReference>? References
        {
            get
            {
                return GetReferences(true);
            }
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// <para>Overridden to copy directly rather than through a JSON round trip, mirroring <see cref="PointCloud"/>.</para>
        /// </summary>
        /// <returns>A new <see cref="ISerializableObject"/> instance that is a clone of the current object.</returns>
        public override ISerializableObject? Clone()
        {
            return new PointCloudReferenceCollection(this);
        }

        /// <summary>
        /// Determines whether the table holds the specified reference.
        /// </summary>
        /// <param name="reference">The reference to look for.</param>
        /// <returns><see langword="true"/> when the reference is present; otherwise <see langword="false"/>.</returns>
        public bool Contains(ISerializableReference? reference)
        {
            return TryGetId(reference, out _);
        }

        /// <summary>
        /// Retrieves the reference stored against an identifier.
        /// </summary>
        /// <param name="id">The zero-based identifier, which is the position of the entry in the table.</param>
        /// <returns>A copy of the reference, or <see langword="null"/> when the identifier is out of range.</returns>
        public ISerializableReference? GetReference(int id)
        {
            return GetReference(id, true);
        }

        /// <summary>
        /// Retrieves the reference stored against an identifier, optionally without copying.
        /// </summary>
        /// <param name="id">The zero-based identifier, which is the position of the entry in the table.</param>
        /// <param name="clone">When <see langword="true"/>, a copy is returned; when <see langword="false"/>, the stored reference is returned directly and must not be modified by the caller.</param>
        /// <returns>The reference, or <see langword="null"/> when the identifier is out of range.</returns>
        public ISerializableReference? GetReference(int id, bool clone)
        {
            if (id < 0 || id >= references.Count)
            {
                return null;
            }

            ISerializableReference reference = references[id];

            return clone ? DiGi.Core.Query.Clone(reference) : reference;
        }

        /// <summary>
        /// Retrieves every entry in the table, in identifier order.
        /// </summary>
        /// <param name="clone">When <see langword="true"/>, a deep copy is returned; when <see langword="false"/>, the internal list is returned directly and must not be modified by the caller.</param>
        /// <returns>A <see cref="List{T}"/> of references, or <see langword="null"/> when the table is empty.</returns>
        public List<ISerializableReference>? GetReferences(bool clone)
        {
            if (references.Count == 0)
            {
                return null;
            }

            if (!clone)
            {
                return references;
            }

            List<ISerializableReference> result = new(references.Count);
            foreach (ISerializableReference reference in references)
            {
                ISerializableReference? reference_Temp = DiGi.Core.Query.Clone(reference);
                if (reference_Temp == null)
                {
                    continue;
                }

                result.Add(reference_Temp);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the identifier stored against a reference.
        /// <para>Keying a dictionary on a reference is safe because <see cref="SerializableReference"/> overrides both <see cref="object.GetHashCode"/> and <see cref="object.Equals(object)"/>. Comparing two interface typed references with <c>==</c> is NOT safe, because the equality operators live on the class and interface typed operands fall through to reference equality.</para>
        /// </summary>
        /// <param name="reference">The reference to look for.</param>
        /// <param name="id">When this method returns, contains the identifier, or -1 when the reference is not in the table.</param>
        /// <returns><see langword="true"/> when the reference was found; otherwise <see langword="false"/>.</returns>
        public bool TryGetId(ISerializableReference? reference, out int id)
        {
            id = -1;

            if (reference == null || references.Count == 0)
            {
                return false;
            }

            Dictionary<ISerializableReference, int>? dictionary_Temp = Volatile.Read(ref dictionary);
            if (dictionary_Temp == null)
            {
                lock (object_DictionaryLock)
                {
                    dictionary_Temp = dictionary;
                    if (dictionary_Temp == null)
                    {
                        dictionary_Temp = new Dictionary<ISerializableReference, int>(references.Count);
                        for (int i = 0; i < references.Count; i++)
                        {
                            dictionary_Temp[references[i]] = i;
                        }

                        Volatile.Write(ref dictionary, dictionary_Temp);
                    }
                }
            }

            if (!dictionary_Temp.TryGetValue(reference, out int id_Temp))
            {
                return false;
            }

            id = id_Temp;

            return true;
        }
    }
}

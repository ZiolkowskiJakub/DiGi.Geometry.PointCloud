using DiGi.Core.Interfaces;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the indexes of the points that link to a given model object.
        /// <para>The reference is resolved to its identifier once and the points are then matched on an integer, rather than comparing a reference per point. Comparing references per point would also be a correctness trap: between two interface typed operands the equality operators are plain reference equality, so the comparison would silently answer false for equal references that are not the same object.</para>
        /// </summary>
        /// <param name="referencedPointCloud3D">The cloud to search.</param>
        /// <param name="reference">The model object to select the points of.</param>
        /// <returns>An ascending <see cref="int"/> array of zero-based point indexes, or <see langword="null"/> when the cloud carries no link to the model object.</returns>
        public static int[]? PointIndexes(this Classes.ReferencedPointCloud3D? referencedPointCloud3D, ISerializableReference? reference)
        {
            if (referencedPointCloud3D == null || reference == null)
            {
                return null;
            }

            Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection = referencedPointCloud3D.GetPointCloudReferenceCollection(false);
            if (pointCloudReferenceCollection == null || !pointCloudReferenceCollection.TryGetId(reference, out int referenceIndex))
            {
                return null;
            }

            return Core.Query.PointIndexes(referencedPointCloud3D.GetReferenceIndexes(false), referenceIndex);
        }
    }
}

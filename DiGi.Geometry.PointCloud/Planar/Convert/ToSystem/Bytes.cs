using DiGi.Geometry.PointCloud.Core.Enums;
using DiGi.Geometry.PointCloud.Planar.Classes;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Convert
    {
        /// <summary>
        /// Encodes a <see cref="PointCloud2D"/> into bytes in the requested representation.
        /// <para>The format is a required argument rather than an optional one on purpose. <see cref="DiGi.Core.Convert.ToSystem_Bytes(DiGi.Core.Interfaces.ISerializableObject)"/> already accepts any serializable object and returns UTF-8 JSON. A same-arity overload here would be selected by whichever using directives happened to be in scope at the call site, so a caller expecting a compact binary payload could silently receive JSON several times larger, with no compiler diagnostic. Differing arity removes the ambiguity and makes the intent visible.</para>
        /// </summary>
        /// <param name="pointCloud2D">The cloud to encode.</param>
        /// <param name="pointCloudFormat">The representation to produce.</param>
        /// <returns>A <see cref="byte"/> array holding the encoded cloud, or <see langword="null"/> when the cloud is null or empty.</returns>
        public static byte[]? ToSystem_Bytes(this PointCloud2D? pointCloud2D, PointCloudFormat pointCloudFormat)
        {
            if (pointCloud2D == null)
            {
                return null;
            }

            if (pointCloudFormat == PointCloudFormat.Json)
            {
                return DiGi.Core.Convert.ToSystem_Bytes(pointCloud2D);
            }

            return Core.Convert.ToSystem_Bytes(pointCloud2D.GetCoordinates(false));
        }
    }
}

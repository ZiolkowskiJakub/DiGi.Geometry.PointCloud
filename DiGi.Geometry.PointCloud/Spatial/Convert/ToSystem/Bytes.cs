using DiGi.Geometry.PointCloud.Core.Enums;
using DiGi.Geometry.PointCloud.Spatial.Classes;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Convert
    {
        /// <summary>
        /// Encodes a <see cref="PointCloud3D"/> into bytes in the requested representation.
        /// <para>The format is a required argument rather than an optional one on purpose. <see cref="DiGi.Core.Convert.ToSystem_Bytes(DiGi.Core.Interfaces.ISerializableObject)"/> already accepts any serializable object and returns UTF-8 JSON. A same-arity overload here would be selected by whichever using directives happened to be in scope at the call site, so a caller expecting a compact binary payload could silently receive JSON several times larger, with no compiler diagnostic. Differing arity removes the ambiguity and makes the intent visible.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to encode.</param>
        /// <param name="pointCloudFormat">The representation to produce.</param>
        /// <returns>A <see cref="byte"/> array holding the encoded cloud, or <see langword="null"/> when the cloud is null or empty.</returns>
        public static byte[]? ToSystem_Bytes(this PointCloud3D? pointCloud3D, PointCloudFormat pointCloudFormat)
        {
            if (pointCloud3D == null)
            {
                return null;
            }

            if (pointCloudFormat == PointCloudFormat.Json)
            {
                return DiGi.Core.Convert.ToSystem_Bytes(pointCloud3D);
            }

            return Core.Convert.ToSystem_Bytes(pointCloud3D.GetCoordinates(false));
        }

        /// <summary>
        /// Encodes a <see cref="ReferencedPointCloud3D"/> into bytes in the requested representation, keeping the per-point model object links.
        /// <para>The binary representation is the coordinate block followed by the identifier block, with the reference table embedded in the second one so that the file stands alone. The length of the first block follows entirely from its own header, which is how the reader finds where the second begins, so neither block needs a pointer to the other.</para>
        /// <para>A cloud carrying no links encodes to the coordinate block alone, which is byte-identical to what the base overload would produce.</para>
        /// <para>Note that this overload is selected only when the argument is typed as the referenced cloud at the call site, because extension methods bind statically. Through a variable typed <see cref="PointCloud3D"/> the base overload runs and the links are not written.</para>
        /// </summary>
        /// <param name="referencedPointCloud3D">The cloud to encode.</param>
        /// <param name="pointCloudFormat">The representation to produce.</param>
        /// <returns>A <see cref="byte"/> array holding the encoded cloud, or <see langword="null"/> when the cloud is null or empty.</returns>
        public static byte[]? ToSystem_Bytes(this ReferencedPointCloud3D? referencedPointCloud3D, PointCloudFormat pointCloudFormat)
        {
            if (referencedPointCloud3D == null)
            {
                return null;
            }

            if (pointCloudFormat == PointCloudFormat.Json)
            {
                return DiGi.Core.Convert.ToSystem_Bytes(referencedPointCloud3D);
            }

            byte[]? bytes_Coordinates = Core.Convert.ToSystem_Bytes(referencedPointCloud3D.GetCoordinates(false));
            if (bytes_Coordinates == null)
            {
                return null;
            }

            byte[]? bytes_ReferenceIndexes = Core.Convert.ToSystem_Bytes(referencedPointCloud3D.GetReferenceIndexes(false), referencedPointCloud3D.GetPointCloudReferenceCollection(false));
            if (bytes_ReferenceIndexes == null)
            {
                return bytes_Coordinates;
            }

            byte[] result = new byte[bytes_Coordinates.Length + bytes_ReferenceIndexes.Length];

            System.Buffer.BlockCopy(bytes_Coordinates, 0, result, 0, bytes_Coordinates.Length);
            System.Buffer.BlockCopy(bytes_ReferenceIndexes, 0, result, bytes_Coordinates.Length, bytes_ReferenceIndexes.Length);

            return result;
        }
    }
}

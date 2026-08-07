using DiGi.Geometry.PointCloud.Core.Classes;
using DiGi.Geometry.Spatial.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the points of a cloud closest to a query point, nearest first, together with their distances.
        /// <para>The convenience form. It allocates the result list, so prefer <see cref="NearestIndexes(Classes.PointCloud3D, double, double, double, Span{int}, Span{double})"/> inside a loop over many query positions.</para>
        /// </summary>
        /// <param name="pointCloud3D">The cloud to search.</param>
        /// <param name="point3D">The query point.</param>
        /// <param name="count">The number of neighbours to retrieve.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="PointCloudNeighbor"/> ordered nearest first, or <see langword="null"/> when the cloud is empty or the count is not positive.</returns>
        public static List<PointCloudNeighbor>? NearestNeighbors(this Classes.PointCloud3D? pointCloud3D, Point3D? point3D, int count)
        {
            if (pointCloud3D == null || point3D == null || count <= 0)
            {
                return null;
            }

            int[] indexes = new int[count];
            double[] distancesSquared = new double[count];

            int count_Filled = NearestIndexes(pointCloud3D, point3D.X, point3D.Y, point3D.Z, indexes, distancesSquared);
            if (count_Filled <= 0)
            {
                return null;
            }

            List<PointCloudNeighbor> result = new(count_Filled);
            for (int i = 0; i < count_Filled; i++)
            {
                result.Add(new PointCloudNeighbor(indexes[i], distancesSquared[i]));
            }

            return result;
        }
    }
}

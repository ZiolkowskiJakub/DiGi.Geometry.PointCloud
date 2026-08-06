using NetTopologySuite.Geometries;
using NetTopologySuite.Triangulate;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Triangulates a set of planar sites and returns the triangles as index triples into the supplied arrays.
        /// <para>The triangulation itself is delegated to NetTopologySuite. What matters here is the mapping back: the vertices are looked up by their exact coordinate values rather than trusting any attribute to survive the internal quad-edge representation, so the result always indexes the caller's own points and any third axis is recovered from them rather than from the triangulator.</para>
        /// <para>A Delaunay triangulation covers the convex hull of its sites, so a cloud with a concave outline or interior holes comes back with a skirt of long thin triangles bridging the gaps. Supplying a maximum edge length removes them. For real scan data that filtering is a requirement, not a refinement.</para>
        /// </summary>
        /// <param name="x">The X coordinates of the sites.</param>
        /// <param name="y">The Y coordinates of the sites.</param>
        /// <param name="maximumEdgeLength">The longest edge a triangle may have, in model units. Values of zero or less keep every triangle.</param>
        /// <returns>A <see cref="List{T}"/> of three element index arrays, or <see langword="null"/> when the input is invalid or nothing could be triangulated.</returns>
        public static List<int[]>? DelaunayIndexes(double[]? x, double[]? y, double maximumEdgeLength = 0)
        {
            if (x == null || y == null || x.Length != y.Length || x.Length < 3)
            {
                return null;
            }

            int count = x.Length;

            Dictionary<(double, double), int> indexes = new(count);
            List<Coordinate> coordinates = new(count);

            for (int i = 0; i < count; i++)
            {
                (double, double) key = (x[i], y[i]);
                if (indexes.ContainsKey(key))
                {
                    continue;
                }

                indexes[key] = i;
                coordinates.Add(new Coordinate(x[i], y[i]));
            }

            if (coordinates.Count < 3)
            {
                return null;
            }

            DelaunayTriangulationBuilder delaunayTriangulationBuilder = new();
            delaunayTriangulationBuilder.SetSites(coordinates);

            // Fully qualified: the bare name Geometry binds to the DiGi.Geometry namespace here, which
            // sits nearer in the lookup chain than any using directive can reach.
            NetTopologySuite.Geometries.Geometry geometry = delaunayTriangulationBuilder.GetTriangles(new GeometryFactory());
            if (geometry == null)
            {
                return null;
            }

            double maximumEdgeLengthSquared = maximumEdgeLength * maximumEdgeLength;

            List<int[]> result = [];

            for (int i = 0; i < geometry.NumGeometries; i++)
            {
                if (geometry.GetGeometryN(i) is not Polygon polygon)
                {
                    continue;
                }

                Coordinate[] coordinates_Triangle = polygon.ExteriorRing.Coordinates;
                if (coordinates_Triangle.Length < 4)
                {
                    continue;
                }

                if (!indexes.TryGetValue((coordinates_Triangle[0].X, coordinates_Triangle[0].Y), out int index_1)
                    || !indexes.TryGetValue((coordinates_Triangle[1].X, coordinates_Triangle[1].Y), out int index_2)
                    || !indexes.TryGetValue((coordinates_Triangle[2].X, coordinates_Triangle[2].Y), out int index_3))
                {
                    continue;
                }

                if (index_1 == index_2 || index_2 == index_3 || index_3 == index_1)
                {
                    continue;
                }

                if (maximumEdgeLength > 0)
                {
                    static double lengthSquared(double[] x_Values, double[] y_Values, int index_Start, int index_End)
                    {
                        double dx = x_Values[index_End] - x_Values[index_Start];
                        double dy = y_Values[index_End] - y_Values[index_Start];

                        return (dx * dx) + (dy * dy);
                    }

                    if (lengthSquared(x, y, index_1, index_2) > maximumEdgeLengthSquared
                        || lengthSquared(x, y, index_2, index_3) > maximumEdgeLengthSquared
                        || lengthSquared(x, y, index_3, index_1) > maximumEdgeLengthSquared)
                    {
                        continue;
                    }
                }

                result.Add([index_1, index_2, index_3]);
            }

            return result.Count == 0 ? null : result;
        }
    }
}

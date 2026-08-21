using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Query
    {
        /// <summary>
        /// Removes the triangles that bridge no data from a triangulation, working inwards from its boundary, so that the result can never enclose a hole.
        /// <para>A Delaunay triangulation covers the convex hull of its sites, which means it also spans concave outlines and empty areas with triangles that look like surface and are not. Removing every triangle that fails a size test at once does clear them, but it also opens a hole around any single site the data happens to be missing, because the triangles that would close over it are exactly the oversized ones.</para>
        /// <para>Here a triangle is only ever removed while it lies on the boundary. Whatever is removed is therefore joined to the outside at the moment it goes, so the removed area always reaches the outer edge and no enclosed hole can form. A site missing from the interior keeps the triangles around it however long their edges are, and reads as a flat spot rather than a gap.</para>
        /// <para>The guarantee has a price, and it is the whole of the trade: an empty area entirely enclosed by data - a lake, a quarry, a pocket the survey never reached - is spanned rather than opened, because no triangle over it ever reaches the boundary to be removed. Only emptiness joined to the outside is cleared. This cannot tell a lake from a dropped measurement and deliberately does not try, so where the difference matters the surface has to be cut against a known outline afterwards. Use the absolute limit instead where an interior void has to stay open and a hole around a missing site is acceptable.</para>
        /// <para>The size test is relative rather than absolute: a triangle is judged against the spacing of its own vertices, taken as the shortest edge each of them carries. A triangulation whose density varies - fine where the surface moves, coarse where it does not - is therefore measured correctly everywhere at once, with no threshold to retune when the sampling changes. The spacing of the three vertices is combined by taking the largest, so the band where a dense area meets a sparse one is judged by the sparse side and survives.</para>
        /// <para>Eroding through a narrow neck can leave the result in disconnected pieces. That is a separation rather than a hole and is left alone.</para>
        /// </summary>
        /// <param name="x">The X coordinates of the sites.</param>
        /// <param name="y">The Y coordinates of the sites.</param>
        /// <param name="indexes">The triangles, as three element index arrays into the coordinate arrays.</param>
        /// <param name="edgeLengthFactor">How many times its own vertex spacing a triangle's longest edge may reach before it is treated as bridging no data. Values of zero or less keep every triangle.</param>
        /// <returns>A <see cref="List{T}"/> of the three element index arrays that survived, or <see langword="null"/> when the input is invalid or nothing survived.</returns>
        public static List<int[]>? ErodedIndexes(double[]? x, double[]? y, List<int[]>? indexes, double edgeLengthFactor)
        {
            if (x == null || y == null || indexes == null || x.Length != y.Length)
            {
                return null;
            }

            if (indexes.Count == 0)
            {
                return null;
            }

            if (edgeLengthFactor <= 0)
            {
                return indexes;
            }

            int count = indexes.Count;

            static double lengthSquared(double[] x_Values, double[] y_Values, int index_Start, int index_End)
            {
                double dx = x_Values[index_End] - x_Values[index_Start];
                double dy = y_Values[index_End] - y_Values[index_Start];

                return (dx * dx) + (dy * dy);
            }

            static (int, int) edgeKey(int index_1, int index_2)
            {
                return index_1 < index_2 ? (index_1, index_2) : (index_2, index_1);
            }

            // The shortest edge a site carries is its spacing. A Delaunay triangulation contains the nearest
            // neighbour graph, so this is the true nearest neighbour distance and costs nothing beyond the walk.
            // Held squared throughout: the comparison it feeds is squared too, so no root is ever taken.
            double[] spacingSquared = new double[x.Length];
            for (int i = 0; i < spacingSquared.Length; i++)
            {
                spacingSquared[i] = double.PositiveInfinity;
            }

            // Keyed on a tuple rather than on two indexes packed into one integer - a single hashed value built
            // that way collides far more readily than the tuple's own combined hash.
            Dictionary<(int, int), List<int>> indexes_Triangle_ByEdge = new(count * 3);

            for (int i = 0; i < count; i++)
            {
                int[] indexes_Triangle = indexes[i];
                if (indexes_Triangle == null || indexes_Triangle.Length < 3)
                {
                    continue;
                }

                for (int j = 0; j < 3; j++)
                {
                    int index_Start = indexes_Triangle[j];
                    int index_End = indexes_Triangle[(j + 1) % 3];

                    double lengthSquared_Edge = lengthSquared(x, y, index_Start, index_End);

                    if (lengthSquared_Edge < spacingSquared[index_Start])
                    {
                        spacingSquared[index_Start] = lengthSquared_Edge;
                    }

                    if (lengthSquared_Edge < spacingSquared[index_End])
                    {
                        spacingSquared[index_End] = lengthSquared_Edge;
                    }

                    (int, int) key = edgeKey(index_Start, index_End);
                    if (!indexes_Triangle_ByEdge.TryGetValue(key, out List<int>? indexes_Triangle_Edge))
                    {
                        indexes_Triangle_Edge = [];
                        indexes_Triangle_ByEdge[key] = indexes_Triangle_Edge;
                    }

                    indexes_Triangle_Edge.Add(i);
                }
            }

            double factorSquared = edgeLengthFactor * edgeLengthFactor;

            bool[] removed = new bool[count];

            // A triangle fails when its longest edge outreaches the spacing of its own vertices. Both sides are
            // squared, so squaring the factor keeps the comparison exact and free of roots.
            bool failing(int index_Triangle)
            {
                int[] indexes_Triangle = indexes[index_Triangle];
                if (indexes_Triangle == null || indexes_Triangle.Length < 3)
                {
                    return false;
                }

                double lengthSquared_Longest = 0;
                double spacingSquared_Largest = 0;

                for (int j = 0; j < 3; j++)
                {
                    int index_Start = indexes_Triangle[j];
                    int index_End = indexes_Triangle[(j + 1) % 3];

                    double lengthSquared_Edge = lengthSquared(x, y, index_Start, index_End);
                    if (lengthSquared_Edge > lengthSquared_Longest)
                    {
                        lengthSquared_Longest = lengthSquared_Edge;
                    }

                    double spacingSquared_Vertex = spacingSquared[index_Start];
                    if (double.IsInfinity(spacingSquared_Vertex))
                    {
                        // A vertex no edge reached carries no spacing to judge against, so nothing is judged.
                        return false;
                    }

                    if (spacingSquared_Vertex > spacingSquared_Largest)
                    {
                        spacingSquared_Largest = spacingSquared_Vertex;
                    }
                }

                return lengthSquared_Longest > factorSquared * spacingSquared_Largest;
            }

            // A triangle sits on the boundary when one of its edges is carried by no other live triangle.
            bool bounding(int index_Triangle)
            {
                int[] indexes_Triangle = indexes[index_Triangle];
                if (indexes_Triangle == null || indexes_Triangle.Length < 3)
                {
                    return false;
                }

                for (int j = 0; j < 3; j++)
                {
                    if (!indexes_Triangle_ByEdge.TryGetValue(edgeKey(indexes_Triangle[j], indexes_Triangle[(j + 1) % 3]), out List<int>? indexes_Triangle_Edge))
                    {
                        continue;
                    }

                    int count_Live = 0;
                    foreach (int index_Triangle_Edge in indexes_Triangle_Edge)
                    {
                        if (!removed[index_Triangle_Edge])
                        {
                            count_Live++;
                        }
                    }

                    if (count_Live < 2)
                    {
                        return true;
                    }
                }

                return false;
            }

            Queue<int> indexes_Queue = new();
            bool[] queued = new bool[count];

            for (int i = 0; i < count; i++)
            {
                if (bounding(i) && failing(i))
                {
                    indexes_Queue.Enqueue(i);
                    queued[i] = true;
                }
            }

            while (indexes_Queue.Count != 0)
            {
                int index_Triangle = indexes_Queue.Dequeue();
                queued[index_Triangle] = false;

                if (removed[index_Triangle])
                {
                    continue;
                }

                // Both conditions only ever become more true - failing is fixed by the geometry, and an edge
                // that lost a neighbour never regains one - so this cannot reject. It guards a removal that
                // cannot be undone, and costs a walk of three edges.
                if (!bounding(index_Triangle) || !failing(index_Triangle))
                {
                    continue;
                }

                removed[index_Triangle] = true;

                int[] indexes_Triangle_Removed = indexes[index_Triangle];
                for (int j = 0; j < 3; j++)
                {
                    if (!indexes_Triangle_ByEdge.TryGetValue(edgeKey(indexes_Triangle_Removed[j], indexes_Triangle_Removed[(j + 1) % 3]), out List<int>? indexes_Triangle_Edge))
                    {
                        continue;
                    }

                    foreach (int index_Triangle_Edge in indexes_Triangle_Edge)
                    {
                        if (removed[index_Triangle_Edge] || queued[index_Triangle_Edge])
                        {
                            continue;
                        }

                        if (bounding(index_Triangle_Edge) && failing(index_Triangle_Edge))
                        {
                            indexes_Queue.Enqueue(index_Triangle_Edge);
                            queued[index_Triangle_Edge] = true;
                        }
                    }
                }
            }

            List<int[]> result = [];
            for (int i = 0; i < count; i++)
            {
                int[] indexes_Triangle = indexes[i];
                if (!removed[i] && indexes_Triangle != null && indexes_Triangle.Length >= 3)
                {
                    result.Add(indexes_Triangle);
                }
            }

            return result.Count == 0 ? null : result;
        }
    }
}

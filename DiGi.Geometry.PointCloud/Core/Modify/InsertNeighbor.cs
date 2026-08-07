using System;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Modify
    {
        /// <summary>
        /// Offers a candidate to an insertion-sorted nearest neighbour set, evicting the furthest entry when the set is already full.
        /// <para>An insertion sort rather than a heap. A nearest neighbour set holds a handful of entries, so the whole structure stays in registers, the shifting loop is predictable, and a heap would add indirection and a larger constant factor for no asymptotic gain at this size.</para>
        /// <para>Ordering is by squared distance and then by point index, so equal distances resolve towards the lower index. This is what lets the indexed descent and the exhaustive scan return the same answer: one visits points in Z-order and the other in input order, and without an explicit tie-break a cloud containing duplicated points would answer differently depending on whether it was large enough to be indexed.</para>
        /// <para>Both paths route their candidates through here rather than repeating the comparison, because the two orderings only agree as long as they are literally the same code.</para>
        /// </summary>
        /// <param name="indexes">The candidate point indexes, ordered nearest first. Its length is the number of neighbours being collected.</param>
        /// <param name="distancesSquared">The matching squared distances, which must be at least as long as <paramref name="indexes"/>.</param>
        /// <param name="index">The zero-based index of the point being offered.</param>
        /// <param name="distanceSquared">The squared distance from the query position to the point being offered. A value of <see cref="double.NaN"/> is always rejected.</param>
        /// <param name="filled">The number of entries currently held, updated when the set grows.</param>
        /// <param name="worst">The current rejection radius, updated whenever the set changes. Holds <see cref="double.PositiveInfinity"/> until the set is full.</param>
        /// <returns><see langword="true"/> when the candidate was taken into the set; otherwise <see langword="false"/>.</returns>
        public static bool InsertNeighbor(
            Span<int> indexes,
            Span<double> distancesSquared,
            int index,
            double distanceSquared,
            ref int filled,
            ref double worst)
        {
            int count = indexes.Length;
            if (count <= 0 || distancesSquared.Length < count || filled < 0 || filled > count)
            {
                return false;
            }

            if (filled == count)
            {
                // Written as a positive test so that a non-finite distance falls out on its own: every
                // comparison against NaN is false, so a NaN candidate is never closer and never ties.
                bool closer = distanceSquared < worst || (distanceSquared == worst && index < indexes[count - 1]);
                if (!closer)
                {
                    return false;
                }
            }
            else if (double.IsNaN(distanceSquared))
            {
                return false;
            }

            // A full set overwrites its last slot, which is exactly the entry being evicted.
            int position = filled < count ? filled : count - 1;

            while (position > 0)
            {
                double distanceSquared_Previous = distancesSquared[position - 1];
                if (distanceSquared_Previous < distanceSquared || (distanceSquared_Previous == distanceSquared && indexes[position - 1] < index))
                {
                    break;
                }

                distancesSquared[position] = distanceSquared_Previous;
                indexes[position] = indexes[position - 1];
                position--;
            }

            distancesSquared[position] = distanceSquared;
            indexes[position] = index;

            if (filled < count)
            {
                filled++;
            }

            worst = filled == count ? distancesSquared[count - 1] : double.PositiveInfinity;

            return true;
        }
    }
}

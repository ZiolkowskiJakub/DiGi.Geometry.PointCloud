using DiGi.Geometry.PointCloud.Core.Enums;
using System.Collections.Generic;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Create
    {
        /// <summary>
        /// Reduces a coordinate-major payload to one representative point per cell of a regular grid laid over the first axes.
        /// <para>Decimation is a precondition of reconstruction, not an optimisation of it. Incremental Delaunay triangulation allocates several objects per site and costs roughly a microsecond each, so ten million sites would need tens of seconds and several gigabytes, while a few hundred thousand finish in a fraction of a second. The grid is the mechanism that gets from one to the other.</para>
        /// <para>Cells are keyed on a tuple rather than on a packed integer. The default hash of a packed key combines its halves by exclusive-or, which collapses catastrophically for the highly regular index pairs a grid produces; a tuple hash mixes them properly.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="cellSize">The edge length of a grid cell, in model units. Must be greater than zero.</param>
        /// <param name="pointCloudHeightSelection">Which point of a cell to keep. Ignored when there is no axis beyond the grid axes.</param>
        /// <returns>A new jagged <see cref="double"/> array holding the representatives, or <see langword="null"/> when the input is invalid.</returns>
        public static double[][]? DecimatedCoordinates(double[][]? coordinates, double cellSize, PointCloudHeightSelection pointCloudHeightSelection = PointCloudHeightSelection.Lowest)
        {
            if (coordinates == null || cellSize <= 0)
            {
                return null;
            }

            int dimension = coordinates.Length;
            if (dimension != 2 && dimension != 3)
            {
                return null;
            }

            double[]? coordinateExtremes = Query.CoordinateExtremes(coordinates);
            if (coordinateExtremes == null)
            {
                return null;
            }

            double x_Origin = coordinateExtremes[0];
            double y_Origin = coordinateExtremes[2];

            double[] x = coordinates[0];
            double[] y = coordinates[1];
            double[]? z = dimension == 3 ? coordinates[2] : null;

            int count = x.Length;

            Dictionary<(int, int), int> slots = new(count / 4 == 0 ? 4 : count / 4);

            List<int> indexes = [];
            List<double[]> sums = [];
            List<int> counts = [];

            bool mean = dimension == 3 && pointCloudHeightSelection == PointCloudHeightSelection.Mean;

            for (int i = 0; i < count; i++)
            {
                (int, int) cell = ((int)System.Math.Floor((x[i] - x_Origin) / cellSize), (int)System.Math.Floor((y[i] - y_Origin) / cellSize));

                if (!slots.TryGetValue(cell, out int slot))
                {
                    slot = indexes.Count;
                    slots[cell] = slot;
                    indexes.Add(i);

                    if (mean)
                    {
                        sums.Add([x[i], y[i], z![i]]);
                        counts.Add(1);
                    }

                    continue;
                }

                if (mean)
                {
                    double[] sum = sums[slot];
                    sum[0] += x[i];
                    sum[1] += y[i];
                    sum[2] += z![i];
                    counts[slot]++;

                    continue;
                }

                if (z == null)
                {
                    continue;
                }

                int index_Existing = indexes[slot];
                if (pointCloudHeightSelection == PointCloudHeightSelection.Highest ? z[i] > z[index_Existing] : z[i] < z[index_Existing])
                {
                    indexes[slot] = i;
                }
            }

            int count_Result = indexes.Count;
            if (count_Result == 0)
            {
                return null;
            }

            double[][] result = new double[dimension][];
            for (int axis = 0; axis < dimension; axis++)
            {
                result[axis] = new double[count_Result];
            }

            for (int slot = 0; slot < count_Result; slot++)
            {
                if (mean)
                {
                    double[] sum = sums[slot];
                    double divisor = counts[slot];

                    result[0][slot] = sum[0] / divisor;
                    result[1][slot] = sum[1] / divisor;
                    result[2][slot] = sum[2] / divisor;

                    continue;
                }

                int index = indexes[slot];
                for (int axis = 0; axis < dimension; axis++)
                {
                    result[axis][slot] = coordinates[axis][index];
                }
            }

            return result;
        }
    }
}

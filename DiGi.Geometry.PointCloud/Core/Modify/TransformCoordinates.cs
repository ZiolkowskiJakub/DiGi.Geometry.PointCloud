using System.Numerics;
using System.Threading.Tasks;

namespace DiGi.Geometry.PointCloud.Core
{
    public static partial class Modify
    {
        /// <summary>
        /// Applies a flattened affine transform to every coordinate in the supplied arrays, in place.
        /// <para>Large inputs are split across partitions. Because each partition writes a disjoint range of the same arrays, no synchronization is needed at all.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="affine">The row-major affine matrix. Six values forming two rows of three for a planar cloud, or twelve values forming three rows of four for a spatial one.</param>
        /// <returns><see langword="true"/> when the transform was applied; otherwise <see langword="false"/>.</returns>
        public static bool TransformCoordinates(this double[][]? coordinates, double[]? affine)
        {
            if (coordinates == null || affine == null || coordinates.Length == 0)
            {
                return false;
            }

            int dimension = coordinates.Length;
            if (dimension != 2 && dimension != 3)
            {
                return false;
            }

            if (affine.Length != dimension * (dimension + 1))
            {
                return false;
            }

            double[]? values_First = coordinates[0];
            if (values_First == null)
            {
                return false;
            }

            int count = values_First.Length;
            for (int axis = 1; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || values.Length != count)
                {
                    return false;
                }
            }

            int partitionCount = Query.PartitionCount(count, Constants.PointCloud.ParallelThreshold, Constants.PointCloud.StreamingProcessorFraction);
            if (partitionCount <= 1)
            {
                return TransformCoordinates(coordinates, affine, 0, count);
            }

            int size = ((count - 1) / partitionCount) + 1;

            Parallel.For(0, partitionCount, i =>
            {
                int startIndex = i * size;
                int length = count - startIndex;
                if (length > size)
                {
                    length = size;
                }

                if (length > 0)
                {
                    TransformCoordinates(coordinates, affine, startIndex, length);
                }
            });

            return true;
        }

        /// <summary>
        /// Applies a flattened affine transform to a contiguous range of coordinates, in place, using a vectorised loop with a scalar tail.
        /// <para>Taking the transform pre-flattened matters: reading the matrix through a transform object costs an indexer call per element per point, and a transform group would otherwise be walked once per point. Flattening once and streaming the result turns the whole pass into arithmetic.</para>
        /// <para>All axes of a lane are read before any is written, so the update is free of aliasing even though it is performed in place.</para>
        /// </summary>
        /// <param name="coordinates">The coordinate arrays, one per axis, all of equal length.</param>
        /// <param name="affine">The row-major affine matrix. Six values forming two rows of three for a planar cloud, or twelve values forming three rows of four for a spatial one.</param>
        /// <param name="startIndex">The inclusive index at which the range starts.</param>
        /// <param name="count">The number of points in the range.</param>
        /// <returns><see langword="true"/> when the transform was applied; otherwise <see langword="false"/>.</returns>
        public static bool TransformCoordinates(this double[][]? coordinates, double[]? affine, int startIndex, int count)
        {
            if (coordinates == null || affine == null || startIndex < 0 || count < 0)
            {
                return false;
            }

            int dimension = coordinates.Length;
            if (dimension != 2 && dimension != 3)
            {
                return false;
            }

            if (affine.Length != dimension * (dimension + 1))
            {
                return false;
            }

            for (int axis = 0; axis < dimension; axis++)
            {
                double[]? values = coordinates[axis];
                if (values == null || startIndex > values.Length - count)
                {
                    return false;
                }
            }

            if (count == 0)
            {
                return true;
            }

            int width = Vector<double>.Count;
            bool vectorise = Vector.IsHardwareAccelerated && width > 1 && count >= width + width;

            int index = startIndex;
            int end = startIndex + count;

            if (dimension == 2)
            {
                double[] x = coordinates[0]!;
                double[] y = coordinates[1]!;

                if (vectorise)
                {
                    Vector<double> vector_M00 = new(affine[0]);
                    Vector<double> vector_M01 = new(affine[1]);
                    Vector<double> vector_M02 = new(affine[2]);
                    Vector<double> vector_M10 = new(affine[3]);
                    Vector<double> vector_M11 = new(affine[4]);
                    Vector<double> vector_M12 = new(affine[5]);

                    int end_Vector = end - width;
                    for (; index <= end_Vector; index += width)
                    {
                        Vector<double> vector_X = new(x, index);
                        Vector<double> vector_Y = new(y, index);

                        Vector<double> vector_X_Temp = (vector_M00 * vector_X) + (vector_M01 * vector_Y) + vector_M02;
                        Vector<double> vector_Y_Temp = (vector_M10 * vector_X) + (vector_M11 * vector_Y) + vector_M12;

                        vector_X_Temp.CopyTo(x, index);
                        vector_Y_Temp.CopyTo(y, index);
                    }
                }

                for (; index < end; index++)
                {
                    double value_X = x[index];
                    double value_Y = y[index];

                    x[index] = (affine[0] * value_X) + (affine[1] * value_Y) + affine[2];
                    y[index] = (affine[3] * value_X) + (affine[4] * value_Y) + affine[5];
                }

                return true;
            }

            double[] x_Temp = coordinates[0]!;
            double[] y_Temp = coordinates[1]!;
            double[] z_Temp = coordinates[2]!;

            if (vectorise)
            {
                Vector<double> vector_M00 = new(affine[0]);
                Vector<double> vector_M01 = new(affine[1]);
                Vector<double> vector_M02 = new(affine[2]);
                Vector<double> vector_M03 = new(affine[3]);
                Vector<double> vector_M10 = new(affine[4]);
                Vector<double> vector_M11 = new(affine[5]);
                Vector<double> vector_M12 = new(affine[6]);
                Vector<double> vector_M13 = new(affine[7]);
                Vector<double> vector_M20 = new(affine[8]);
                Vector<double> vector_M21 = new(affine[9]);
                Vector<double> vector_M22 = new(affine[10]);
                Vector<double> vector_M23 = new(affine[11]);

                int end_Vector = end - width;
                for (; index <= end_Vector; index += width)
                {
                    Vector<double> vector_X = new(x_Temp, index);
                    Vector<double> vector_Y = new(y_Temp, index);
                    Vector<double> vector_Z = new(z_Temp, index);

                    Vector<double> vector_X_Result = (vector_M00 * vector_X) + (vector_M01 * vector_Y) + (vector_M02 * vector_Z) + vector_M03;
                    Vector<double> vector_Y_Result = (vector_M10 * vector_X) + (vector_M11 * vector_Y) + (vector_M12 * vector_Z) + vector_M13;
                    Vector<double> vector_Z_Result = (vector_M20 * vector_X) + (vector_M21 * vector_Y) + (vector_M22 * vector_Z) + vector_M23;

                    vector_X_Result.CopyTo(x_Temp, index);
                    vector_Y_Result.CopyTo(y_Temp, index);
                    vector_Z_Result.CopyTo(z_Temp, index);
                }
            }

            for (; index < end; index++)
            {
                double value_X = x_Temp[index];
                double value_Y = y_Temp[index];
                double value_Z = z_Temp[index];

                x_Temp[index] = (affine[0] * value_X) + (affine[1] * value_Y) + (affine[2] * value_Z) + affine[3];
                y_Temp[index] = (affine[4] * value_X) + (affine[5] * value_Y) + (affine[6] * value_Z) + affine[7];
                z_Temp[index] = (affine[8] * value_X) + (affine[9] * value_Y) + (affine[10] * value_Z) + affine[11];
            }

            return true;
        }
    }
}

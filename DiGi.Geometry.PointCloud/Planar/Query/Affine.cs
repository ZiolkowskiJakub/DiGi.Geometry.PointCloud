using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using System;

namespace DiGi.Geometry.PointCloud.Planar
{
    public static partial class Query
    {
        /// <summary>
        /// Flattens a two-dimensional transform into a row-major affine matrix of two rows of three values.
        /// <para>A transform group is composed into a single matrix rather than being replayed per point, so applying it to a cloud costs one multiply-add chain per coordinate regardless of how many transforms the group holds. Composition order matches the per-point behaviour of <see cref="Coordinate2D.Transform(ITransform2D)"/>, where each member of a group is applied in sequence.</para>
        /// </summary>
        /// <param name="transform2D">The transform to flatten.</param>
        /// <returns>A six element <see cref="double"/> array holding the rows of the affine matrix, or <see langword="null"/> when the transform is null or not a recognised kind.</returns>
        public static double[]? Affine(this ITransform2D? transform2D)
        {
            if (transform2D == null)
            {
                return null;
            }

            double[] result = [1, 0, 0, 0, 1, 0];

            bool compose(ITransform2D? transform)
            {
                if (transform is Transform2D transform_Temp)
                {
                    double m00 = transform_Temp[0, 0];
                    if (double.IsNaN(m00))
                    {
                        return false;
                    }

                    double[] values = new double[6];
                    for (int row = 0; row < 2; row++)
                    {
                        double t0 = transform_Temp[row, 0];
                        double t1 = transform_Temp[row, 1];
                        double t2 = transform_Temp[row, 2];

                        int offset = row * 3;
                        values[offset] = (t0 * result[0]) + (t1 * result[3]);
                        values[offset + 1] = (t0 * result[1]) + (t1 * result[4]);
                        values[offset + 2] = (t0 * result[2]) + (t1 * result[5]) + t2;
                    }

                    Array.Copy(values, result, 6);

                    return true;
                }

                if (transform is TransformGroup2D transformGroup2D)
                {
                    foreach (ITransform2D transform_Group in transformGroup2D)
                    {
                        if (transform_Group == null)
                        {
                            continue;
                        }

                        if (!compose(transform_Group))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            if (!compose(transform2D))
            {
                return null;
            }

            return result;
        }
    }
}

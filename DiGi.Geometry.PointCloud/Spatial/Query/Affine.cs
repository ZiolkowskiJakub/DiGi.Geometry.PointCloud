using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using System;

namespace DiGi.Geometry.PointCloud.Spatial
{
    public static partial class Query
    {
        /// <summary>
        /// Flattens a three-dimensional transform into a row-major affine matrix of three rows of four values.
        /// <para>A transform group is composed into a single matrix rather than being replayed per point, so applying it to a cloud costs one multiply-add chain per coordinate regardless of how many transforms the group holds. Composition order matches the per-point behaviour of <see cref="Coordinate3D.Transform(ITransform3D)"/>, where each member of a group is applied in sequence.</para>
        /// </summary>
        /// <param name="transform3D">The transform to flatten.</param>
        /// <returns>A twelve element <see cref="double"/> array holding the rows of the affine matrix, or <see langword="null"/> when the transform is null or not a recognised kind.</returns>
        public static double[]? Affine(this ITransform3D? transform3D)
        {
            if (transform3D == null)
            {
                return null;
            }

            double[] result = [1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0];

            bool compose(ITransform3D? transform)
            {
                if (transform is Transform3D transform_Temp)
                {
                    double m00 = transform_Temp[0, 0];
                    if (double.IsNaN(m00))
                    {
                        return false;
                    }

                    double[] values = new double[12];
                    for (int row = 0; row < 3; row++)
                    {
                        double t0 = transform_Temp[row, 0];
                        double t1 = transform_Temp[row, 1];
                        double t2 = transform_Temp[row, 2];
                        double t3 = transform_Temp[row, 3];

                        int offset = row * 4;
                        values[offset] = (t0 * result[0]) + (t1 * result[4]) + (t2 * result[8]);
                        values[offset + 1] = (t0 * result[1]) + (t1 * result[5]) + (t2 * result[9]);
                        values[offset + 2] = (t0 * result[2]) + (t1 * result[6]) + (t2 * result[10]);
                        values[offset + 3] = (t0 * result[3]) + (t1 * result[7]) + (t2 * result[11]) + t3;
                    }

                    Array.Copy(values, result, 12);

                    return true;
                }

                if (transform is TransformGroup3D transformGroup3D)
                {
                    foreach (ITransform3D transform_Group in transformGroup3D)
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

            if (!compose(transform3D))
            {
                return null;
            }

            return result;
        }
    }
}

namespace DiGi.Geometry.PointCloud.Core.Enums
{
    /// <summary>
    /// Specifies which point represents a grid cell when a cloud is decimated onto a height field.
    /// </summary>
    public enum PointCloudHeightSelection
    {
        /// <summary>
        /// Keeps the point with the smallest value on the height axis.
        /// <para>The usual choice for extracting ground from an aerial scan, where vegetation and structures sit above the surface of interest.</para>
        /// </summary>
        Lowest = 0,

        /// <summary>
        /// Keeps the point with the largest value on the height axis.
        /// <para>The usual choice for a surface model, where canopy and roofs are the surface of interest.</para>
        /// </summary>
        Highest = 1,

        /// <summary>
        /// Keeps the average of every point in the cell, on all axes.
        /// <para>Smoothest, and the most forgiving of scanner noise, but it fabricates a position that no measurement occupied and blurs genuine steps.</para>
        /// </summary>
        Mean = 2
    }
}

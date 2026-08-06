#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Core\.Enums Namespace
### Enums

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat'></a>

## PointCloudFormat Enum

Specifies the byte representation produced for a point cloud\.

```csharp
public enum PointCloudFormat
```
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat.Binary'></a>

`Binary` 0

The compact binary point cloud format, holding a fixed header followed by a coordinate\-major payload of raw doubles\.

This is the only representation that scales to tens of millions of points.

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat.Json'></a>

`Json` 1

The UTF\-8 encoded JSON representation, in which the coordinate payload appears as a single Base64 string\.

Convenient and self-describing, but a round trip holds several copies of the payload in memory at once, so it is impractical much beyond a few million points.

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection'></a>

## PointCloudHeightSelection Enum

Specifies which point represents a grid cell when a cloud is decimated onto a height field\.

```csharp
public enum PointCloudHeightSelection
```
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection.Lowest'></a>

`Lowest` 0

Keeps the point with the smallest value on the height axis\.

The usual choice for extracting ground from an aerial scan, where vegetation and structures sit above the surface of interest.

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection.Highest'></a>

`Highest` 1

Keeps the point with the largest value on the height axis\.

The usual choice for a surface model, where canopy and roofs are the surface of interest.

<a name='DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection.Mean'></a>

`Mean` 2

Keeps the average of every point in the cell, on all axes\.

Smoothest, and the most forgiving of scanner noise, but it fabricates a position that no measurement occupied and blurs genuine steps.
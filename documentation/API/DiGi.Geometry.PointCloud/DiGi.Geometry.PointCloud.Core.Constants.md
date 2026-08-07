#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Core\.Constants Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud'></a>

## PointCloud Class

Provides tuning constants for [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') processing\.

The thresholds are calibrated for a desktop-class CPU with dual-channel DDR5 memory. They trade a small amount of peak throughput for predictable behaviour on small inputs, where fan-out and index construction cost more than the work they save.

```csharp
public static class PointCloud
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → PointCloud
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryHeaderLength'></a>

## PointCloud\.BinaryHeaderLength Field

The length in bytes of the fixed header of the binary point cloud format\.

```csharp
public const int BinaryHeaderLength = 32;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryMagic_0'></a>

## PointCloud\.BinaryMagic\_0 Field

The first byte of the four byte magic identifier of the binary point cloud format, spelling "DGPC"\.

```csharp
public const byte BinaryMagic_0 = 68;
```

#### Field Value
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryMagic_1'></a>

## PointCloud\.BinaryMagic\_1 Field

The second byte of the four byte magic identifier of the binary point cloud format\.

```csharp
public const byte BinaryMagic_1 = 71;
```

#### Field Value
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryMagic_2'></a>

## PointCloud\.BinaryMagic\_2 Field

The third byte of the four byte magic identifier of the binary point cloud format\.

```csharp
public const byte BinaryMagic_2 = 80;
```

#### Field Value
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryMagic_3'></a>

## PointCloud\.BinaryMagic\_3 Field

The fourth byte of the four byte magic identifier of the binary point cloud format\.

```csharp
public const byte BinaryMagic_3 = 67;
```

#### Field Value
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryVersion'></a>

## PointCloud\.BinaryVersion Field

The version number written into the header of the binary point cloud format\.

```csharp
public const int BinaryVersion = 1;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexBucketCount'></a>

## PointCloud\.IndexBucketCount Field

The number of coarse buckets used by the most\-significant\-digit pass of the index counting sort\.

Chosen so the number of concurrently open write streams per thread stays inside the write-combining and translation-lookaside-buffer budget. A single-level scatter over the full cell table would open hundreds of thousands of streams and dominate runtime.

```csharp
public const int IndexBucketCount = 256;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexLeafPointCount'></a>

## PointCloud\.IndexLeafPointCount Field

The target number of points per spatial index leaf, used to derive the index depth from the point count\.

```csharp
public const int IndexLeafPointCount = 64;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexThreshold'></a>

## PointCloud\.IndexThreshold Field

The minimum point count at which building a spatial index is worthwhile\.

Below this size a vectorised brute-force scan completes in tens of microseconds, which is cheaper than any index build. Callers should skip index construction entirely below this threshold.

```csharp
public const int IndexThreshold = 65536;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MaximumDepth2D'></a>

## PointCloud\.MaximumDepth2D Field

The maximum spatial index depth in two dimensions, bounding the cell table at four raised to this power\.

```csharp
public const int MaximumDepth2D = 11;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MaximumDepth3D'></a>

## PointCloud\.MaximumDepth3D Field

The maximum spatial index depth in three dimensions, bounding the cell table at eight raised to this power\.

```csharp
public const int MaximumDepth3D = 7;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MaximumNeighborCandidateCount'></a>

## PointCloud\.MaximumNeighborCandidateCount Field

The number of nearest neighbours collected when a non\-degenerate triple is required\.

Three points are enough only when they are not collinear, which fails whenever the query sits on a scan line or a grid line of the source data. Collecting this many candidates lets the triangle factory step past a degenerate pair without a second traversal: the extra neighbours come from leaves that are already resident, so the additional cost is a handful of comparisons.

```csharp
public const int MaximumNeighborCandidateCount = 8;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MinimumDepth'></a>

## PointCloud\.MinimumDepth Field

The minimum spatial index depth, below which a single flat cell list performs as well as a hierarchy\.

```csharp
public const int MinimumDepth = 2;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.ParallelThreshold'></a>

## PointCloud\.ParallelThreshold Field

The minimum point count at which bulk coordinate passes \(bounding box, move, transform, filter\) are worth parallelising\.

A 32-way `Parallel.For` dispatch costs roughly 5-20 microseconds. Serial SIMD streaming runs at roughly 10-12 GB/s, so keeping dispatch below ten percent of the work requires about 200 microseconds of serial work, which is approximately this many three-dimensional points.

```csharp
public const int ParallelThreshold = 100000;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.ParallelThresholdIndex'></a>

## PointCloud\.ParallelThresholdIndex Field

The minimum point count at which spatial index construction is worth parallelising\.

Lower than [ParallelThreshold](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.ParallelThreshold 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.ParallelThreshold') because index construction performs more work per point than a streaming pass.

```csharp
public const int ParallelThresholdIndex = 50000;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.ParallelThresholdNeighbor'></a>

## PointCloud\.ParallelThresholdNeighbor Field

The minimum number of query points at which a batch nearest neighbour search is worth parallelising\.

An indexed descent for a single query costs roughly one to three microseconds, so a partition of this size carries well over a millisecond of work against a dispatch cost of 5-20 microseconds. Unlike the streaming passes this workload is latency bound rather than bandwidth bound, so it is partitioned across every processor rather than the streaming fraction.

```csharp
public const int ParallelThresholdNeighbor = 1024;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Constants.PointCloud.StreamingProcessorFraction'></a>

## PointCloud\.StreamingProcessorFraction Field

The fraction of available processors to use for memory\-bound streaming passes\.

Dual-channel DDR5 saturates at roughly eight to twelve threads. Additional threads contribute scheduling cost and no bandwidth.

```csharp
public const double StreamingProcessorFraction = 0.5;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')
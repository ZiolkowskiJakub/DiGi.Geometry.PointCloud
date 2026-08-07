#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Planar Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Planar.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat)'></a>

## Convert\.ToSystem\_Bytes\(this PointCloud2D, PointCloudFormat\) Method

Encodes a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') into bytes in the requested representation\.

The format is a required argument rather than an optional one on purpose. [DiGi\.Core\.Convert\.ToSystem\_Bytes\(DiGi\.Core\.Interfaces\.ISerializableObject\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.convert.tosystem_bytes#digi-core-convert-tosystem_bytes(digi-core-interfaces-iserializableobject) 'DiGi\.Core\.Convert\.ToSystem\_Bytes\(DiGi\.Core\.Interfaces\.ISerializableObject\)') already accepts any serializable object and returns UTF-8 JSON. A same-arity overload here would be selected by whichever using directives happened to be in scope at the call site, so a caller expecting a compact binary payload could silently receive JSON several times larger, with no compiler diagnostic. Differing arity removes the ambiguity and makes the intent visible.

```csharp
public static byte[]? ToSystem_Bytes(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat pointCloudFormat);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to encode\.

<a name='DiGi.Geometry.PointCloud.Planar.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).pointCloudFormat'></a>

`pointCloudFormat` [PointCloudFormat](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudFormat')

The representation to produce\.

#### Returns
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte') array holding the encoded cloud, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is null or empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Create.Mesh2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D_)'></a>

## Create\.Mesh2D\(this PointCloud2D, IPointCloudMeshSolver\<PointCloud2D,Mesh2D\>\) Method

Reconstructs a [Mesh2D\(this PointCloud2D, IPointCloudMeshSolver&lt;PointCloud2D,Mesh2D&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Create.Mesh2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D_) 'DiGi\.Geometry\.PointCloud\.Planar\.Create\.Mesh2D\(this DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D, DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D,DiGi\.Geometry\.Planar\.Classes\.Mesh2D\>\)') from a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') using the supplied strategy\.

```csharp
public static DiGi.Geometry.Planar.Classes.Mesh2D? Mesh2D(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver<DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D>? pointCloudMeshSolver);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.Mesh2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D_).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to reconstruct\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Mesh2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D_).pointCloudMeshSolver'></a>

`pointCloudMeshSolver` [DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')[,](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[DiGi\.Geometry\.Planar\.Classes\.Mesh2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.mesh2d 'DiGi\.Geometry\.Planar\.Classes\.Mesh2D')[&gt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')

The reconstruction strategy to apply\.

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.Mesh2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.mesh2d 'DiGi\.Geometry\.Planar\.Classes\.Mesh2D')  
The reconstructed [Mesh2D\(this PointCloud2D, IPointCloudMeshSolver&lt;PointCloud2D,Mesh2D&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Create.Mesh2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Mesh2D_) 'DiGi\.Geometry\.PointCloud\.Planar\.Create\.Mesh2D\(this DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D, DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D,DiGi\.Geometry\.Planar\.Classes\.Mesh2D\>\)'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either argument is null or the reconstruction produced nothing\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(byte[])'></a>

## Create\.PointCloud2D\(byte\[\]\) Method

Creates a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') from a buffer in the binary point cloud format\.

Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null').

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? PointCloud2D(byte[]? bytes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(byte[]).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(double[],double[])'></a>

## Create\.PointCloud2D\(double\[\], double\[\]\) Method

Creates a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') from two coordinate arrays, dropping points with a non\-finite coordinate\.

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? PointCloud2D(double[]? x, double[]? y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(double[],double[]).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(double[],double[]).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the arrays are null, of unequal length, or hold no finite point\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(double[][])'></a>

## Create\.PointCloud2D\(double\[\]\[\]\) Method

Creates a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') from coordinate arrays, dropping points with a non\-finite coordinate\.

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? PointCloud2D(double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(double[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\. Exactly two axes are required\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, not two\-dimensional, ragged, or holds no finite point\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_)'></a>

## Create\.PointCloud2D\(this IEnumerable\<Point2D\>\) Method

Creates a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') from a sequence of points, dropping null entries and points with a non\-finite coordinate\.

The filtering lives here rather than in the constructor because it is an order-of-count sweep over the input, and a caller holding data that is already clean should not pay for it. See [FiniteCoordinates\(double\[\]\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.FiniteCoordinates\(double\[\]\[\]\)') for why non-finite coordinates must not reach the vectorised paths.

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? PointCloud2D(this System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Classes.Point2D?>? point2Ds);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_).point2Ds'></a>

`point2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The points to store\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null or holds no usable point\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.IO.FileInfo)'></a>

## Create\.PointCloud2D\(this FileInfo\) Method

Creates a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') by reading a file written in the binary point cloud format\.

Never throws. A missing or unreadable file yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null').

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? PointCloud2D(this System.IO.FileInfo? fileInfo);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.IO.FileInfo).fileInfo'></a>

`fileInfo` [System\.IO\.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System\.IO\.FileInfo')

The file to read\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the file could not be read or decoded\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,double)'></a>

## Create\.Triangle2D\(this PointCloud2D, Point2D, double\) Method

Builds a [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D') from the points of a cloud nearest to a query point\.

```csharp
public static DiGi.Geometry.Planar.Classes.Triangle2D? Triangle2D(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.Planar.Classes.Point2D? point2D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,double).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The query point\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D')  
A new [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud holds too few points or offers no non\-degenerate triple\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,double)'></a>

## Create\.Triangle2D\(this PointCloud2D, double, double, double\) Method

Builds a [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D') from the points of a cloud nearest to a query position\.

The three nearest points are taken first, and are used as they are whenever they form a usable triangle. They frequently do not: a query sitting on a scan line or a grid line of the source data has three nearest points that are exactly collinear, and three collinear points enclose no area. Rather than fail there, the search collects [MaximumNeighborCandidateCount](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MaximumNeighborCandidateCount 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.MaximumNeighborCandidateCount') neighbours in one traversal and steps through the pairs beyond the first until a triple stands clear of a line. Those extra neighbours come from leaves the traversal already visited, so the widening costs a handful of comparisons and no second search.

The nearest point always stays as a corner. Any candidate that would displace it is a duplicate of it, which is interchangeable, so anchoring there costs nothing and keeps the triangle attached to the point the caller actually asked about.

Everything up to the result allocates nothing: the candidate set is stack-allocated and the corners are selected from raw coordinates. The three [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects are created only once a triangle is known to exist.

```csharp
public static DiGi.Geometry.Planar.Classes.Triangle2D? Triangle2D(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, double x, double y, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2D(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D')  
A new [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud holds too few points or offers no non\-degenerate triple\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2Ds(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double)'></a>

## Create\.Triangle2Ds\(this PointCloud2D, PointCloud2D, double\) Method

Builds one [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D') per point of a query cloud from the points of a source cloud nearest to it\.

This is where a many-core machine earns its keep. A single query is answered by a descent over a few dozen nodes and finishes in microseconds, so parallelising one would cost more in dispatch than the whole search; a batch of queries is a different problem entirely. Each query is independent, reads a shared index that is never written, and writes to its own slot of the result, so there is no shared mutable state, no lock and no contention.

The index is built once before the fan-out. Its lazy construction is thread safe, but arriving at it with every worker at once would leave all but one of them waiting on the lock for the build.

The partitioning uses every processor rather than the fraction the bulk coordinate passes use. Those are limited by memory bandwidth and saturate well before every core is busy; a descent walks a small, cache-resident node table and is bound by latency and arithmetic instead, so it keeps scaling.

The result is aligned with the query cloud, one entry per query point in the same order, holding [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') wherever no non-degenerate triple exists. Compacting the nulls away would break the correspondence that makes the result usable.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.Triangle2D?>? Triangle2Ds(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D_Query, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2Ds(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2Ds(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double).pointCloud2D_Query'></a>

`pointCloud2D_Query` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud of query positions\.

<a name='DiGi.Geometry.PointCloud.Planar.Create.Triangle2Ds(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') holding one [DiGi\.Geometry\.Planar\.Classes\.Triangle2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.triangle2d 'DiGi\.Geometry\.Planar\.Classes\.Triangle2D') per query point, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Query.Affine(thisDiGi.Geometry.Planar.Interfaces.ITransform2D)'></a>

## Query\.Affine\(this ITransform2D\) Method

Flattens a two\-dimensional transform into a row\-major affine matrix of two rows of three values\.

A transform group is composed into a single matrix rather than being replayed per point, so applying it to a cloud costs one multiply-add chain per coordinate regardless of how many transforms the group holds. Composition order matches the per-point behaviour of [DiGi\.Geometry\.Planar\.Classes\.Coordinate2D\.Transform\(DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.coordinate2d.transform#digi-geometry-planar-classes-coordinate2d-transform(digi-geometry-planar-interfaces-itransform2d) 'DiGi\.Geometry\.Planar\.Classes\.Coordinate2D\.Transform\(DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D\)'), where each member of a group is applied in sequence.

```csharp
public static double[]? Affine(this DiGi.Geometry.Planar.Interfaces.ITransform2D? transform2D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.Affine(thisDiGi.Geometry.Planar.Interfaces.ITransform2D).transform2D'></a>

`transform2D` [DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.itransform2d 'DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D')

The transform to flatten\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A six element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the rows of the affine matrix, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the transform is null or not a recognised kind\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRange(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## Query\.InRange\(this PointCloud2D, BoundingBox2D, double\) Method

Filters a cloud down to the points that fall inside an axis\-aligned box\.

No [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') object is created anywhere on this path. The result is built directly as coordinate arrays and handed to the adopting constructor.

The tolerance is folded into the bounds once, before the scan, so the result agrees exactly with [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D\.InRange\(DiGi\.Geometry\.Planar\.Classes\.Point2D,System\.Double\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d.inrange#digi-geometry-planar-classes-boundingbox2d-inrange(digi-geometry-planar-classes-point2d-system-double) 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D\.InRange\(DiGi\.Geometry\.Planar\.Classes\.Point2D,System\.Double\)') applied point by point.

```csharp
public static DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? InRange(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRange(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to filter\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRange(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The box to filter against\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRange(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
A new [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') holding the points inside the box, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when nothing qualifies\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## Query\.InRangeCount\(this PointCloud2D, BoundingBox2D, double\) Method

Counts the points of a cloud that fall inside an axis\-aligned box, without materializing them\.

```csharp
public static int InRangeCount(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to test\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The box to test against\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of points inside the box, or \-1 when the cloud or box is null\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## Query\.InRangeIndexes\(this PointCloud2D, BoundingBox2D, double\) Method

Retrieves the indexes of the points of a cloud that fall inside an axis\-aligned box\.

```csharp
public static System.Collections.Generic.List<int>? InRangeIndexes(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to test\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The box to test against\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of zero\-based point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud or box is null\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.Maximums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## Query\.Maximums\(this BoundingBox2D, double\) Method

Produces the per\-axis upper bounds of a box widened by a tolerance\.

```csharp
public static double[] Maximums(this DiGi.Geometry.Planar.Classes.BoundingBox2D boundingBox2D, double tolerance);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.Maximums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The box to read\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.Maximums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A two element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the widened upper bounds\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.Minimums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double)'></a>

## Query\.Minimums\(this BoundingBox2D, double\) Method

Produces the per\-axis lower bounds of a box widened by a tolerance\.

```csharp
public static double[] Minimums(this DiGi.Geometry.Planar.Classes.BoundingBox2D boundingBox2D, double tolerance);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.Minimums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The box to read\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.Minimums(thisDiGi.Geometry.Planar.Classes.BoundingBox2D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A two element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the widened lower bounds\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_)'></a>

## Query\.NearestIndexes\(this PointCloud2D, double, double, Span\<int\>, Span\<double\>\) Method

Retrieves the indexes of the points of a cloud closest to a query position, nearest first\.

Allocation free. The caller owns the result buffers, which are typically stack-allocated.

```csharp
public static int NearestIndexes(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, double x, double y, System.Span<int> indexes, System.Span<double> distancesSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_).indexes 'DiGi\.Geometry\.PointCloud\.Planar\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D, double, double, System\.Span\<int\>, System\.Span\<double\>\)\.indexes')\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, or \-1 when the cloud is empty or the request is mismatched\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,int)'></a>

## Query\.NearestNeighbors\(this PointCloud2D, Point2D, int\) Method

Retrieves the points of a cloud closest to a query point, nearest first, together with their distances\.

The convenience form. It allocates the result list, so prefer [NearestIndexes\(this PointCloud2D, double, double, Span&lt;int&gt;, Span&lt;double&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_) 'DiGi\.Geometry\.PointCloud\.Planar\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D, double, double, System\.Span\<int\>, System\.Span\<double\>\)') inside a loop over many query positions.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor>? NearestNeighbors(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, DiGi.Geometry.Planar.Classes.Point2D? point2D, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,int).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,int).point2D'></a>

`point2D` [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')

The query point\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,DiGi.Geometry.Planar.Classes.Point2D,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of neighbours to retrieve\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[PointCloudNeighbor](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [PointCloudNeighbor](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor') ordered nearest first, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the count is not positive\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int)'></a>

## Query\.TryGetNearestIndexes\(this PointCloud2D, double, double, int, int, int\) Method

Retrieves the indexes of the three points of a cloud closest to a query position\.

The whole search allocates nothing. The query is taken as two loose coordinates rather than a [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') so that a caller sweeping many positions never constructs one, and the three results are returned as separate values rather than a collection so that nothing is constructed on the way out either.

Three neighbours are what a triangle needs, which is why this exact arity is worth a dedicated member. Use [NearestIndexes\(this PointCloud2D, double, double, Span&lt;int&gt;, Span&lt;double&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,System.Span_int_,System.Span_double_) 'DiGi\.Geometry\.PointCloud\.Planar\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D, double, double, System\.Span\<int\>, System\.Span\<double\>\)') for any other count.

```csharp
public static bool TryGetNearestIndexes(this DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D, double x, double y, out int index_1, out int index_2, out int index_3);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).index_1'></a>

`index_1` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the closest point\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).index_2'></a>

`index_2` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the second closest point\.

<a name='DiGi.Geometry.PointCloud.Planar.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D,double,double,int,int,int).index_3'></a>

`index_3` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the third closest point\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when three distinct points were found; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
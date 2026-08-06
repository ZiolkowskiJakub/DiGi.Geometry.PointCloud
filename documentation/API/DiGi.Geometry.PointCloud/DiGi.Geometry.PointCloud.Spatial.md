#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Spatial Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Spatial.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat)'></a>

## Convert\.ToSystem\_Bytes\(this PointCloud3D, PointCloudFormat\) Method

Encodes a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') into bytes in the requested representation\.

The format is a required argument rather than an optional one on purpose. [DiGi\.Core\.Convert\.ToSystem\_Bytes\(DiGi\.Core\.Interfaces\.ISerializableObject\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.convert.tosystem_bytes#digi-core-convert-tosystem_bytes(digi-core-interfaces-iserializableobject) 'DiGi\.Core\.Convert\.ToSystem\_Bytes\(DiGi\.Core\.Interfaces\.ISerializableObject\)') already accepts any serializable object and returns UTF-8 JSON. A same-arity overload here would be selected by whichever using directives happened to be in scope at the call site, so a caller expecting a compact binary payload could silently receive JSON several times larger, with no compiler diagnostic. Differing arity removes the ambiguity and makes the intent visible.

```csharp
public static byte[]? ToSystem_Bytes(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat pointCloudFormat);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to encode\.

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).pointCloudFormat'></a>

`pointCloudFormat` [PointCloudFormat](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudFormat')

The representation to produce\.

#### Returns
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte') array holding the encoded cloud, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is null or empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Mesh3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D_)'></a>

## Create\.Mesh3D\(this PointCloud3D, IPointCloudMeshSolver\<PointCloud3D,Mesh3D\>\) Method

Reconstructs a [Mesh3D\(this PointCloud3D, IPointCloudMeshSolver&lt;PointCloud3D,Mesh3D&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Create.Mesh3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Create\.Mesh3D\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D,DiGi\.Geometry\.Spatial\.Classes\.Mesh3D\>\)') from a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') using the supplied strategy\.

The strategy is required rather than defaulted. Reconstruction from an unstructured cloud is a modelling decision, not a calculation, and every available strategy is wrong for some input: a height field cannot express a vertical face, and an isosurface without surface normals cannot distinguish inside from outside. Making the caller choose keeps that decision visible.

```csharp
public static DiGi.Geometry.Spatial.Classes.Mesh3D? Mesh3D(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver<DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D>? pointCloudMeshSolver);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Mesh3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D_).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to reconstruct\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Mesh3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D_).pointCloudMeshSolver'></a>

`pointCloudMeshSolver` [DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')[,](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D')[&gt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')

The reconstruction strategy to apply\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D')  
The reconstructed [Mesh3D\(this PointCloud3D, IPointCloudMeshSolver&lt;PointCloud3D,Mesh3D&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Create.Mesh3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Mesh3D_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Create\.Mesh3D\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D,DiGi\.Geometry\.Spatial\.Classes\.Mesh3D\>\)'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either argument is null or the reconstruction produced nothing\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(byte[])'></a>

## Create\.PointCloud3D\(byte\[\]\) Method

Creates a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') from a buffer in the binary point cloud format\.

Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null').

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(byte[]? bytes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(byte[]).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[],double[],double[])'></a>

## Create\.PointCloud3D\(double\[\], double\[\], double\[\]\) Method

Creates a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') from three coordinate arrays, dropping points with a non\-finite coordinate\.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(double[]? x, double[]? y, double[]? z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[],double[],double[]).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[],double[],double[]).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[],double[],double[]).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the arrays are null, of unequal length, or hold no finite point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[][])'></a>

## Create\.PointCloud3D\(double\[\]\[\]\) Method

Creates a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') from coordinate arrays, dropping points with a non\-finite coordinate\.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(double[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\. Exactly three axes are required\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, not three\-dimensional, ragged, or holds no finite point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_)'></a>

## Create\.PointCloud3D\(this IEnumerable\<Point3D\>\) Method

Creates a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') from a sequence of points, dropping null entries and points with a non\-finite coordinate\.

The filtering lives here rather than in the constructor because it is an order-of-count sweep over the input, and a caller holding data that is already clean should not pay for it. See [FiniteCoordinates\(double\[\]\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.FiniteCoordinates\(double\[\]\[\]\)') for why non-finite coordinates must not reach the vectorised paths.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(this System.Collections.Generic.IEnumerable<DiGi.Geometry.Spatial.Classes.Point3D?>? point3Ds);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_).point3Ds'></a>

`point3Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The points to store\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null or holds no usable point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.IO.FileInfo)'></a>

## Create\.PointCloud3D\(this FileInfo\) Method

Creates a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') by reading a file written in the binary point cloud format\.

Never throws. A missing or unreadable file yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null').

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(this System.IO.FileInfo? fileInfo);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.IO.FileInfo).fileInfo'></a>

`fileInfo` [System\.IO\.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System\.IO\.FileInfo')

The file to read\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the file could not be read or decoded\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Affine(thisDiGi.Geometry.Spatial.Interfaces.ITransform3D)'></a>

## Query\.Affine\(this ITransform3D\) Method

Flattens a three\-dimensional transform into a row\-major affine matrix of three rows of four values\.

A transform group is composed into a single matrix rather than being replayed per point, so applying it to a cloud costs one multiply-add chain per coordinate regardless of how many transforms the group holds. Composition order matches the per-point behaviour of [DiGi\.Geometry\.Spatial\.Classes\.Coordinate3D\.Transform\(DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.coordinate3d.transform#digi-geometry-spatial-classes-coordinate3d-transform(digi-geometry-spatial-interfaces-itransform3d) 'DiGi\.Geometry\.Spatial\.Classes\.Coordinate3D\.Transform\(DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D\)'), where each member of a group is applied in sequence.

```csharp
public static double[]? Affine(this DiGi.Geometry.Spatial.Interfaces.ITransform3D? transform3D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Affine(thisDiGi.Geometry.Spatial.Interfaces.ITransform3D).transform3D'></a>

`transform3D` [DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.itransform3d 'DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D')

The transform to flatten\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A twelve element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the rows of the affine matrix, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the transform is null or not a recognised kind\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.InRange\(this PointCloud3D, BoundingBox3D, double\) Method

Filters a cloud down to the points that fall inside an axis\-aligned box\.

No [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') object is created anywhere on this path. The result is built directly as coordinate arrays and handed to the adopting constructor, so filtering a cloud of ten million points allocates three arrays and nothing else.

The tolerance is folded into the bounds once, before the scan, so the result agrees exactly with [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D\.InRange\(DiGi\.Geometry\.Spatial\.Classes\.Point3D,System\.Double\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d.inrange#digi-geometry-spatial-classes-boundingbox3d-inrange(digi-geometry-spatial-classes-point3d-system-double) 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D\.InRange\(DiGi\.Geometry\.Spatial\.Classes\.Point3D,System\.Double\)') applied point by point.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? InRange(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.Spatial.Classes.BoundingBox3D? boundingBox3D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to filter\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to filter against\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') holding the points inside the box, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when nothing qualifies\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.InRangeCount\(this PointCloud3D, BoundingBox3D, double\) Method

Counts the points of a cloud that fall inside an axis\-aligned box, without materializing them\.

Useful for sizing a buffer once when a caller intends to filter repeatedly, which avoids repeatedly allocating and discarding large object heap arrays.

```csharp
public static int InRangeCount(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.Spatial.Classes.BoundingBox3D? boundingBox3D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to test\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to test against\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeCount(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of points inside the box, or \-1 when the cloud or box is null\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.InRangeIndexes\(this PointCloud3D, BoundingBox3D, double\) Method

Retrieves the indexes of the points of a cloud that fall inside an axis\-aligned box\.

```csharp
public static System.Collections.Generic.List<int>? InRangeIndexes(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.Spatial.Classes.BoundingBox3D? boundingBox3D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to test\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to test against\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of zero\-based point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud or box is null\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Maximums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.Maximums\(this BoundingBox3D, double\) Method

Produces the per\-axis upper bounds of a box widened by a tolerance\.

```csharp
public static double[] Maximums(this DiGi.Geometry.Spatial.Classes.BoundingBox3D boundingBox3D, double tolerance);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Maximums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to read\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Maximums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A three element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the widened upper bounds\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Minimums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.Minimums\(this BoundingBox3D, double\) Method

Produces the per\-axis lower bounds of a box widened by a tolerance\.

```csharp
public static double[] Minimums(this DiGi.Geometry.Spatial.Classes.BoundingBox3D boundingBox3D, double tolerance);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Minimums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to read\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.Minimums(thisDiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A three element [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the widened lower bounds\.
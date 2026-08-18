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

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat)'></a>

## Convert\.ToSystem\_Bytes\(this ReferencedPointCloud3D, PointCloudFormat\) Method

Encodes a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') into bytes in the requested representation, keeping the per\-point model object links\.

The binary representation is the coordinate block followed by the identifier block, with the reference table embedded in the second one so that the file stands alone. The length of the first block follows entirely from its own header, which is how the reader finds where the second begins, so neither block needs a pointer to the other.

A cloud carrying no links encodes to the coordinate block alone, which is byte-identical to what the base overload would produce.

Note that this overload is selected only when the argument is typed as the referenced cloud at the call site, because extension methods bind statically. Through a variable typed [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') the base overload runs and the links are not written.

```csharp
public static byte[]? ToSystem_Bytes(this DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? referencedPointCloud3D, DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat pointCloudFormat);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).referencedPointCloud3D'></a>

`referencedPointCloud3D` [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

The cloud to encode\.

<a name='DiGi.Geometry.PointCloud.Spatial.Convert.ToSystem_Bytes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.PointCloud.Core.Enums.PointCloudFormat).pointCloudFormat'></a>

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

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D_)'></a>

## Create\.PointCloud3D\(this IEnumerable\<PointCloud3D\>\) Method

Creates a single [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') holding every point of the supplied clouds, in the order the clouds are given\.

Null and empty clouds in the sequence are skipped. Points with a non-finite coordinate are dropped, as they are by every other overload here.

No [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') object is created anywhere on this path: the sources are read through [GetCoordinates\(bool\)](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(bool) 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud\.GetCoordinates\(bool\)') without cloning and block copied into arrays allocated once at the combined size.

IMPORTANT: the result is a plain cloud. A [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') passed in here comes back with its per-point model object links gone, and because extension methods bind statically nothing at the call site warns about it. Merging referenced clouds means merging their reference tables and renumbering their identifiers; there is deliberately no overload for it.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? PointCloud3D(this System.Collections.Generic.IEnumerable<DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D?>? pointCloud3Ds);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D_).pointCloud3Ds'></a>

`pointCloud3Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The clouds to concatenate\.

#### Returns
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
A new [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null or holds no usable point\.

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

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(byte[])'></a>

## Create\.ReferencedPointCloud3D\(byte\[\]\) Method

Creates a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') from a buffer holding a coordinate block optionally followed by an identifier block\.

Never throws. A truncated, misaligned, foreign, wrong-dimension or future-versioned buffer yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null'), and a buffer holding coordinates alone yields a cloud that carries no links.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(byte[]? bytes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(byte[]).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection)'></a>

## Create\.ReferencedPointCloud3D\(double\[\], double\[\], double\[\], int\[\], PointCloudReferenceCollection\) Method

Creates a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') from three coordinate arrays and their per\-point identifiers, dropping points with a non\-finite coordinate\.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, one per point, where \-1 marks a point that links to nothing\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).pointCloudReferenceCollection'></a>

`pointCloudReferenceCollection` [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

The reference table the identifiers index into\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the arrays are null, of unequal length, or hold no finite point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[][],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection)'></a>

## Create\.ReferencedPointCloud3D\(double\[\]\[\], int\[\], PointCloudReferenceCollection\) Method

Creates a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') from coordinate arrays and their per\-point identifiers, dropping points with a non\-finite coordinate\.

The filter is expressed as a permutation and applied to the coordinates and the identifiers by the same gather, which is what keeps a point and its model object together across a change of point count.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(double[][]? coordinates, int[]? referenceIndexes, DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[][],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\. Exactly three axes are required\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[][],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, one per point, where \-1 marks a point that links to nothing\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(double[][],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).pointCloudReferenceCollection'></a>

`pointCloudReferenceCollection` [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

The reference table the identifiers index into\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, not three\-dimensional, ragged, or holds no finite point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference)'></a>

## Create\.ReferencedPointCloud3D\(this ReferencedPointCloud3D, ISerializableReference\) Method

Extracts the points of a cloud that link to one model object, as a cloud in its own right\.

The extracted cloud keeps the identifiers and the whole reference table rather than being renumbered, so an identifier means the same thing in the extract as in the source and the two can be compared without a translation step. The entries that keep no points simply go unread.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(this DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? referencedPointCloud3D, DiGi.Core.Interfaces.ISerializableReference? reference);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference).referencedPointCloud3D'></a>

`referencedPointCloud3D` [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

The cloud to extract from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference).reference'></a>

`reference` [DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')

The model object to extract the points of\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') holding the points of the model object, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries no link to it\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_,System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_)'></a>

## Create\.ReferencedPointCloud3D\(this IEnumerable\<Point3D\>, IEnumerable\<ISerializableReference\>\) Method

Creates a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') from a sequence of points and the model object each one belongs to, dropping null entries and points with a non\-finite coordinate\.

The two sequences are consumed in lockstep, so a null point discards its reference with it. This is why the cloud cannot simply be built from the point constructor and then handed a separate array: that constructor drops null points silently, which would shift every later reference onto the wrong point.

The reference table and the identifiers are built together, and the non-finite filter is applied to both by one permutation, so the two halves cannot come out of step.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(this System.Collections.Generic.IEnumerable<DiGi.Geometry.Spatial.Classes.Point3D?>? point3Ds, System.Collections.Generic.IEnumerable<DiGi.Core.Interfaces.ISerializableReference>? references);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_,System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_).point3Ds'></a>

`point3Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The points to store\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_,System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The model object each point belongs to, in the same order\. A null entry marks a point that links to nothing\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either input is null or no usable point remains\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisSystem.IO.FileInfo)'></a>

## Create\.ReferencedPointCloud3D\(this FileInfo\) Method

Creates a [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') by reading a file holding a coordinate block optionally followed by an identifier block\.

Never throws. A missing or unreadable file yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null').

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? ReferencedPointCloud3D(this System.IO.FileInfo? fileInfo);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.ReferencedPointCloud3D(thisSystem.IO.FileInfo).fileInfo'></a>

`fileInfo` [System\.IO\.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System\.IO\.FileInfo')

The file to read\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the file could not be read or decoded\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,double)'></a>

## Create\.Triangle3D\(this PointCloud3D, Point3D, double\) Method

Builds a [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D') from the points of a cloud nearest to a query point\.

```csharp
public static DiGi.Geometry.Spatial.Classes.Triangle3D? Triangle3D(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.Spatial.Classes.Point3D? point3D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,double).point3D'></a>

`point3D` [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')

The query point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D')  
A new [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud holds too few points or offers no non\-degenerate triple\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double)'></a>

## Create\.Triangle3D\(this PointCloud3D, double, double, double, double\) Method

Builds a [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D') from the points of a cloud nearest to a query position\.

The three nearest points are taken first, and are used as they are whenever they form a usable triangle. They frequently do not: a query sitting on a scan line or a grid line of the source data has three nearest points that are exactly collinear, and three collinear points describe no plane. Rather than fail there, the search collects [MaximumNeighborCandidateCount](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.MaximumNeighborCandidateCount 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.MaximumNeighborCandidateCount') neighbours in one traversal and steps through the pairs beyond the first until a triple stands clear of a line. Those extra neighbours come from leaves the traversal already visited, so the widening costs a handful of comparisons and no second search.

The nearest point always stays as a corner. Any candidate that would displace it is a duplicate of it, which is interchangeable, so anchoring there costs nothing and keeps the triangle attached to the point the caller actually asked about.

Everything up to the result allocates nothing: the candidate set is stack-allocated and the corners are selected from raw coordinates. The three [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') objects are created only once a triangle is known to exist.

```csharp
public static DiGi.Geometry.Spatial.Classes.Triangle3D? Triangle3D(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, double x, double y, double z, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3D(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D')  
A new [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud holds too few points or offers no non\-degenerate triple\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3Ds(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double)'></a>

## Create\.Triangle3Ds\(this PointCloud3D, PointCloud3D, double\) Method

Builds one [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D') per point of a query cloud from the points of a source cloud nearest to it\.

This is where a many-core machine earns its keep. A single query is answered by a descent over a few dozen nodes and finishes in microseconds, so parallelising one would cost more in dispatch than the whole search; a batch of queries is a different problem entirely. Each query is independent, reads a shared index that is never written, and writes to its own slot of the result, so there is no shared mutable state, no lock and no contention.

The index is built once before the fan-out. Its lazy construction is thread safe, but arriving at it with every worker at once would leave all but one of them waiting on the lock for the build.

The partitioning uses every processor rather than the fraction the bulk coordinate passes use. Those are limited by memory bandwidth and saturate well before every core is busy; a descent walks a small, cache-resident node table and is bound by latency and arithmetic instead, so it keeps scaling.

The result is aligned with the query cloud, one entry per query point in the same order, holding [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') wherever no non-degenerate triple exists. Compacting the nulls away would break the correspondence that makes the result usable.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.Spatial.Classes.Triangle3D?>? Triangle3Ds(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D_Query, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3Ds(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to take the corners from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3Ds(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double).pointCloud3D_Query'></a>

`pointCloud3D_Query` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud of query positions\.

<a name='DiGi.Geometry.PointCloud.Spatial.Create.Triangle3Ds(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance below which the third corner counts as lying on the line through the other two\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') holding one [DiGi\.Geometry\.Spatial\.Classes\.Triangle3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.triangle3d 'DiGi\.Geometry\.Spatial\.Classes\.Triangle3D') per query point, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either cloud is empty\.

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

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double)'></a>

## Query\.InRange\(this ReferencedPointCloud3D, BoundingBox3D, double\) Method

Filters a cloud that carries per\-point model object links down to the points that fall inside an axis\-aligned box, carrying the links with them\.

This overload exists because extension methods bind statically. Without it a filtered cloud would come back as a plain [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') and the links would be gone; with it, the links survive as long as the variable is typed as the referenced cloud at the call site.

The points and their identifiers are compacted by ONE permutation, obtained from [InRangeIndexes\(this PointCloud3D, BoundingBox3D, double\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Query.InRangeIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double) 'DiGi\.Geometry\.PointCloud\.Spatial\.Query\.InRangeIndexes\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D, double\)'). Gathering them separately is what would let them drift apart, and a cloud whose identifiers are offset by one looks entirely healthy while attributing every point to the wrong model object.

The reference table is shared with the source rather than copied, which is safe because the table has no mutating members and identifiers stay valid under filtering. An entry that keeps no points simply goes unused.

```csharp
public static DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? InRange(this DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? referencedPointCloud3D, DiGi.Geometry.Spatial.Classes.BoundingBox3D? boundingBox3D, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).referencedPointCloud3D'></a>

`referencedPointCloud3D` [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

The cloud to filter\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).boundingBox3D'></a>

`boundingBox3D` [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')

The box to filter against\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.InRange(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Geometry.Spatial.Classes.BoundingBox3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance by which the box is widened on every axis before testing\.

#### Returns
[ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')  
A new [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') holding the points inside the box, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when nothing qualifies\.

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

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_)'></a>

## Query\.NearestIndexes\(this PointCloud3D, double, double, double, Span\<int\>, Span\<double\>\) Method

Retrieves the indexes of the points of a cloud closest to a query position, nearest first\.

Allocation free. The caller owns the result buffers, which are typically stack-allocated.

```csharp
public static int NearestIndexes(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, double x, double y, double z, System.Span<int> indexes, System.Span<double> distancesSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_).indexes 'DiGi\.Geometry\.PointCloud\.Spatial\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, double, double, double, System\.Span\<int\>, System\.Span\<double\>\)\.indexes')\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, or \-1 when the cloud is empty or the request is mismatched\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,int)'></a>

## Query\.NearestNeighbors\(this PointCloud3D, Point3D, int\) Method

Retrieves the points of a cloud closest to a query point, nearest first, together with their distances\.

The convenience form. It allocates the result list, so prefer [NearestIndexes\(this PointCloud3D, double, double, double, Span&lt;int&gt;, Span&lt;double&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, double, double, double, System\.Span\<int\>, System\.Span\<double\>\)') inside a loop over many query positions.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor>? NearestNeighbors(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, DiGi.Geometry.Spatial.Classes.Point3D? point3D, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,int).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,int).point3D'></a>

`point3D` [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')

The query point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.NearestNeighbors(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,DiGi.Geometry.Spatial.Classes.Point3D,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of neighbours to retrieve\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[PointCloudNeighbor](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [PointCloudNeighbor](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor') ordered nearest first, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the count is not positive\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.PointIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference)'></a>

## Query\.PointIndexes\(this ReferencedPointCloud3D, ISerializableReference\) Method

Retrieves the indexes of the points that link to a given model object\.

The reference is resolved to its identifier once and the points are then matched on an integer, rather than comparing a reference per point. Comparing references per point would also be a correctness trap: between two interface typed operands the equality operators are plain reference equality, so the comparison would silently answer false for equal references that are not the same object.

```csharp
public static int[]? PointIndexes(this DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? referencedPointCloud3D, DiGi.Core.Interfaces.ISerializableReference? reference);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.PointIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference).referencedPointCloud3D'></a>

`referencedPointCloud3D` [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.PointIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D,DiGi.Core.Interfaces.ISerializableReference).reference'></a>

`reference` [DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')

The model object to select the points of\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
An ascending [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array of zero\-based point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries no link to the model object\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int)'></a>

## Query\.TryGetNearestIndexes\(this PointCloud3D, double, double, double, int, int, int\) Method

Retrieves the indexes of the three points of a cloud closest to a query position\.

The whole search allocates nothing. The query is taken as three loose coordinates rather than a [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') so that a caller sweeping many positions never constructs one, and the three results are returned as separate values rather than a collection so that nothing is constructed on the way out either.

Three neighbours are what a triangle needs, which is why this exact arity is worth a dedicated member. Use [NearestIndexes\(this PointCloud3D, double, double, double, Span&lt;int&gt;, Span&lt;double&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,System.Span_int_,System.Span_double_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D, double, double, double, System\.Span\<int\>, System\.Span\<double\>\)') for any other count.

```csharp
public static bool TryGetNearestIndexes(this DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D, double x, double y, double z, out int index_1, out int index_2, out int index_3);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).index_1'></a>

`index_1` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the closest point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).index_2'></a>

`index_2` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the second closest point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Query.TryGetNearestIndexes(thisDiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D,double,double,double,int,int,int).index_3'></a>

`index_3` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the index of the third closest point\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when three distinct points were found; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
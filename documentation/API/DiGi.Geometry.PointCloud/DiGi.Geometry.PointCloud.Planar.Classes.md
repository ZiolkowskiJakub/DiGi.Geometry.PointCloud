#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Planar\.Classes Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver'></a>

## DelaunayPointCloud2DMeshSolver Class

Reconstructs a [DiGi\.Geometry\.Planar\.Classes\.Mesh2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.mesh2d 'DiGi\.Geometry\.Planar\.Classes\.Mesh2D') from a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') by Delaunay triangulation\.

In two dimensions reconstruction is unambiguous, so unlike the spatial case there is no modelling assumption to get wrong: the Delaunay triangulation is the triangulation of the point set.

Set [CellSize](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.CellSize 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.CellSize') above zero to decimate onto a grid first. Triangulation cost grows far faster than the point count, so a cloud of any real size must be thinned before it is triangulated.

Set [MaximumEdgeLength](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.MaximumEdgeLength 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.DelaunayPointCloud2DMeshSolver\.MaximumEdgeLength') to discard the long thin triangles that a Delaunay triangulation necessarily produces where it spans a concave outline or an interior hole.

```csharp
public class DelaunayPointCloud2DMeshSolver : DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver<DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D, DiGi.Geometry.Planar.Classes.Mesh2D>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')[,](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[DiGi\.Geometry\.Planar\.Classes\.Mesh2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.mesh2d 'DiGi\.Geometry\.Planar\.Classes\.Mesh2D')[&gt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>') → DelaunayPointCloud2DMeshSolver
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.DelaunayPointCloud2DMeshSolver(double,double,double)'></a>

## DelaunayPointCloud2DMeshSolver\(double, double, double\) Constructor

Initializes a new instance of the [DelaunayPointCloud2DMeshSolver](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.DelaunayPointCloud2DMeshSolver') class\.

```csharp
public DelaunayPointCloud2DMeshSolver(double cellSize=0.0, double maximumEdgeLength=0.0, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.DelaunayPointCloud2DMeshSolver(double,double,double).cellSize'></a>

`cellSize` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The edge length of the decimation grid, in model units\. Values of zero or less triangulate every point\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.DelaunayPointCloud2DMeshSolver(double,double,double).maximumEdgeLength'></a>

`maximumEdgeLength` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The longest edge a triangle may have, in model units\. Values of zero or less keep every triangle\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.DelaunayPointCloud2DMeshSolver(double,double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used when comparing coordinates\.
### Properties

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.Input'></a>

## DelaunayPointCloud2DMeshSolver\.Input Property

Sets the cloud to reconstruct\.

```csharp
public override DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? Input { set; }
```

Implements [Input](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2.input 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2\.Input')

#### Property Value
[PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
The [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') to consume\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.MaximumEdgeLength'></a>

## DelaunayPointCloud2DMeshSolver\.MaximumEdgeLength Property

Gets or sets the longest edge a triangle may have, in model units\.

```csharp
public double MaximumEdgeLength { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the maximum edge length\. Values of zero or less keep every triangle\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver.Solve()'></a>

## DelaunayPointCloud2DMeshSolver\.Solve\(\) Method

Runs the triangulation\.

```csharp
public override bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a mesh was produced; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D'></a>

## PointCloud2D Class

Represents a cloud of two\-dimensional points stored as two parallel coordinate arrays\.

See [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') for why the storage is coordinate-major rather than a list of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects, and for the concurrency contract.

Construct through [PointCloud2D\(this IEnumerable&lt;Point2D&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_) 'DiGi\.Geometry\.PointCloud\.Planar\.Create\.PointCloud2D\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.Point2D\>\)') when the input may contain non-finite coordinates. The constructors here only assign and copy; the factory performs the filtering.

```csharp
public class PointCloud2D : DiGi.Geometry.PointCloud.Core.Classes.PointCloud, DiGi.Geometry.Planar.Interfaces.IGeometry2D, DiGi.Geometry.Core.Interfaces.IGeometry, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject, DiGi.Geometry.Planar.Interfaces.IMovable2D, DiGi.Geometry.Core.Interfaces.IMovable<DiGi.Geometry.Planar.Classes.Vector2D>, DiGi.Geometry.Core.Interfaces.IMovable, DiGi.Geometry.Planar.Interfaces.ITransformable2D, DiGi.Geometry.Core.Interfaces.ITransformable<DiGi.Geometry.Planar.Interfaces.ITransform2D>, DiGi.Geometry.Core.Interfaces.ITransformable, DiGi.Geometry.Planar.Interfaces.IBoundable2D, DiGi.Geometry.Core.Interfaces.IBoundable, DiGi.Geometry.Planar.Interfaces.ICollectable2D, DiGi.Geometry.Core.Interfaces.ICollectable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') → PointCloud2D

Implements [DiGi\.Geometry\.Planar\.Interfaces\.IGeometry2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.igeometry2d 'DiGi\.Geometry\.Planar\.Interfaces\.IGeometry2D'), [DiGi\.Geometry\.Core\.Interfaces\.IGeometry](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometry 'DiGi\.Geometry\.Core\.Interfaces\.IGeometry'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Geometry\.Planar\.Interfaces\.IMovable2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.imovable2d 'DiGi\.Geometry\.Planar\.Interfaces\.IMovable2D'), [DiGi\.Geometry\.Core\.Interfaces\.IMovable&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.imovable-1 'DiGi\.Geometry\.Core\.Interfaces\.IMovable\`1')[DiGi\.Geometry\.Planar\.Classes\.Vector2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.vector2d 'DiGi\.Geometry\.Planar\.Classes\.Vector2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.imovable-1 'DiGi\.Geometry\.Core\.Interfaces\.IMovable\`1'), [DiGi\.Geometry\.Core\.Interfaces\.IMovable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.imovable 'DiGi\.Geometry\.Core\.Interfaces\.IMovable'), [DiGi\.Geometry\.Planar\.Interfaces\.ITransformable2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.itransformable2d 'DiGi\.Geometry\.Planar\.Interfaces\.ITransformable2D'), [DiGi\.Geometry\.Core\.Interfaces\.ITransformable&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable-1 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable\`1')[DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.itransform2d 'DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable-1 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable\`1'), [DiGi\.Geometry\.Core\.Interfaces\.ITransformable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable'), [DiGi\.Geometry\.Planar\.Interfaces\.IBoundable2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.iboundable2d 'DiGi\.Geometry\.Planar\.Interfaces\.IBoundable2D'), [DiGi\.Geometry\.Core\.Interfaces\.IBoundable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.iboundable 'DiGi\.Geometry\.Core\.Interfaces\.IBoundable'), [DiGi\.Geometry\.Planar\.Interfaces\.ICollectable2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.icollectable2d 'DiGi\.Geometry\.Planar\.Interfaces\.ICollectable2D'), [DiGi\.Geometry\.Core\.Interfaces\.ICollectable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.icollectable 'DiGi\.Geometry\.Core\.Interfaces\.ICollectable')
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D)'></a>

## PointCloud2D\(PointCloud2D\) Constructor

Initializes a new instance of the [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') class by copying an existing cloud\.

```csharp
public PointCloud2D(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to copy from\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[])'></a>

## PointCloud2D\(double\[\], double\[\]\) Constructor

Initializes a new instance of the [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') class by copying two coordinate arrays\.

```csharp
public PointCloud2D(double[]? x, double[]? y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[]).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[]).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[],bool)'></a>

## PointCloud2D\(double\[\], double\[\], bool\) Constructor

Initializes a new instance of the [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') class from two prebuilt coordinate arrays\.

```csharp
internal PointCloud2D(double[]? x, double[]? y, bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[],bool).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[],bool).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(double[],double[],bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the arrays are defensively copied; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), they are adopted directly\. Use [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') only when the caller owns freshly created arrays that are not shared\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_)'></a>

## PointCloud2D\(IEnumerable\<Point2D\>\) Constructor

Initializes a new instance of the [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') class from a sequence of points\.

Null entries are skipped, since a null has no representation in a coordinate array. Non-finite coordinates are NOT filtered here; use [PointCloud2D\(this IEnumerable&lt;Point2D&gt;\)](DiGi.Geometry.PointCloud.Planar.md#DiGi.Geometry.PointCloud.Planar.Create.PointCloud2D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_) 'DiGi\.Geometry\.PointCloud\.Planar\.Create\.PointCloud2D\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.Point2D\>\)') for that.

```csharp
public PointCloud2D(System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Classes.Point2D?>? point2Ds);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.Point2D_).point2Ds'></a>

`point2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The points to store\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(System.Text.Json.Nodes.JsonObject)'></a>

## PointCloud2D\(JsonObject\) Constructor

Initializes a new instance of the [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public PointCloud2D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.PointCloud2D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') holding the serialized cloud\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.AsView()'></a>

## PointCloud2D\.AsView\(\) Method

Returns a zero\-copy view over the whole cloud\.

The view is a ref struct holding two spans, so it cannot escape to the heap and cannot outlive the arrays it points at. Reading through it allocates nothing.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView AsView();
```

#### Returns
[PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView')  
A [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView') over the cloud\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.AsView(int,int)'></a>

## PointCloud2D\.AsView\(int, int\) Method

Returns a zero\-copy view over a contiguous range of the cloud\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView AsView(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.AsView(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.AsView(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView')  
A [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView') over the range, empty when the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Clone()'></a>

## PointCloud2D\.Clone\(\) Method

Creates a copy of the current object\.

```csharp
public override DiGi.Core.Interfaces.ISerializableObject? Clone();
```

Implements [Clone\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1.clone 'DiGi\.Core\.Interfaces\.ICloneableObject\`1\.Clone')

#### Returns
[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')  
A new [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject') instance that is a clone of the current object\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetBoundingBox()'></a>

## PointCloud2D\.GetBoundingBox\(\) Method

Calculates the axis\-aligned bounding box enclosing every point in the cloud\.

```csharp
public DiGi.Geometry.Planar.Classes.BoundingBox2D? GetBoundingBox();
```

Implements [GetBoundingBox\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.iboundable2d.getboundingbox 'DiGi\.Geometry\.Planar\.Interfaces\.IBoundable2D\.GetBoundingBox')

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')  
A [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D') enclosing the cloud, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetEnumerator()'></a>

## PointCloud2D\.GetEnumerator\(\) Method

Returns an enumerator that walks the cloud without allocating\.

This is duck-typed rather than an [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') implementation, and deliberately so. Implementing [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') would make this type match both `ToSystem_Bytes` overloads in DiGi.Core, and it would invite query operators that materialize one object per point and undo the entire storage design.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator GetEnumerator();
```

#### Returns
[Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Enumerator')  
An [Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Enumerator') positioned before the first point\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoint(int)'></a>

## PointCloud2D\.GetPoint\(int\) Method

Retrieves the point at the specified index\.

```csharp
public DiGi.Geometry.Planar.Classes.Point2D? GetPoint(int index);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoint(int).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')  
A new [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the index is out of range\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoints()'></a>

## PointCloud2D\.GetPoints\(\) Method

Materializes every point in the cloud as a list of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects\.

This allocates one object per point and is intended for interoperability, not for bulk processing. Prefer the coordinate array accessors when working at scale.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.Point2D>? GetPoints();
```

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoints(int,int)'></a>

## PointCloud2D\.GetPoints\(int, int\) Method

Materializes a contiguous range of the cloud as a list of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects\.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.Point2D>? GetPoints(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoints(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetPoints(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetX()'></a>

## PointCloud2D\.GetX\(\) Method

Retrieves the X coordinate array\.

```csharp
public double[]? GetX();
```

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the X coordinates, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.GetY()'></a>

## PointCloud2D\.GetY\(\) Method

Retrieves the Y coordinate array\.

```csharp
public double[]? GetY();
```

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the Y coordinates, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Move(DiGi.Geometry.Planar.Classes.Vector2D)'></a>

## PointCloud2D\.Move\(Vector2D\) Method

Translates every point in the cloud by the specified vector\.

```csharp
public bool Move(DiGi.Geometry.Planar.Classes.Vector2D? vector2D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Move(DiGi.Geometry.Planar.Classes.Vector2D).vector2D'></a>

`vector2D` [DiGi\.Geometry\.Planar\.Classes\.Vector2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.vector2d 'DiGi\.Geometry\.Planar\.Classes\.Vector2D')

The translation vector\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the cloud was moved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point2Ds()'></a>

## PointCloud2D\.Point2Ds\(\) Method

Returns the cloud as a lazy sequence of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') objects for interoperability with APIs that require them\.

Named explicitly rather than exposed through [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') so that the per-point allocation is a visible choice at the call site.

```csharp
public System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Classes.Point2D> Point2Ds();
```

#### Returns
[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')  
An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Transform(DiGi.Geometry.Planar.Interfaces.ITransform2D)'></a>

## PointCloud2D\.Transform\(ITransform2D\) Method

Applies a transform to every point in the cloud\.

The transform is flattened into an affine matrix once and then streamed over the coordinate arrays, rather than being replayed per point.

```csharp
public bool Transform(DiGi.Geometry.Planar.Interfaces.ITransform2D? transform);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Transform(DiGi.Geometry.Planar.Interfaces.ITransform2D).transform'></a>

`transform` [DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.itransform2d 'DiGi\.Geometry\.Planar\.Interfaces\.ITransform2D')

The transform to apply\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the cloud was transformed; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.TryGetPoint(int,double,double)'></a>

## PointCloud2D\.TryGetPoint\(int, double, double\) Method

Retrieves a single point without allocating\.

```csharp
public bool TryGetPoint(int index, out double x, out double y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.TryGetPoint(int,double,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.TryGetPoint(int,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.TryGetPoint(int,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Y coordinate\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the point was retrieved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
### Structs

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator'></a>

## PointCloud2D\.Enumerator Struct

Walks a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') one point at a time without allocating\.

A plain struct rather than a ref struct, so it remains usable inside iterators, lambdas and asynchronous methods. The span-based counterpart lives on [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView').

```csharp
public struct PointCloud2D.Enumerator
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D)'></a>

## Enumerator\(PointCloud2D\) Constructor

Initializes a new instance of the [Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Enumerator') struct\.

```csharp
public Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D? pointCloud2D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D).pointCloud2D'></a>

`pointCloud2D` [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')

The cloud to walk\.
### Properties

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator.Current'></a>

## PointCloud2D\.Enumerator\.Current Property

Gets the point at the current position\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point Current { get; }
```

#### Property Value
[Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point')  
A [Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point') holding the current coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Enumerator.MoveNext()'></a>

## PointCloud2D\.Enumerator\.MoveNext\(\) Method

Advances to the next point\.

```csharp
public bool MoveNext();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a further point is available; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point'></a>

## PointCloud2D\.Point Struct

Represents a single point of a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D') as a value\.

A plain readonly struct rather than a ref struct: a point holds two doubles and no reference, so the ref struct restrictions would buy nothing while preventing use in generics, lambdas, arrays and lists.

```csharp
public readonly struct PointCloud2D.Point
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.Point(double,double)'></a>

## Point\(double, double\) Constructor

Initializes a new instance of the [Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point') struct\.

```csharp
public Point(double x, double y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.Point(double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.Point(double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate\.
### Properties

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.X'></a>

## PointCloud2D\.Point\.X Property

Gets the X coordinate\.

```csharp
public double X { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.Y'></a>

## PointCloud2D\.Point\.Y Property

Gets the Y coordinate\.

```csharp
public double Y { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the Y coordinate\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point.ToPoint2D()'></a>

## PointCloud2D\.Point\.ToPoint2D\(\) Method

Materializes this value as a [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D') object\.

```csharp
public DiGi.Geometry.Planar.Classes.Point2D ToPoint2D();
```

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')  
A new [DiGi\.Geometry\.Planar\.Classes\.Point2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.point2d 'DiGi\.Geometry\.Planar\.Classes\.Point2D')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView'></a>

## PointCloud2DView Struct

Represents a zero\-copy, read\-only window onto the coordinate arrays of a [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')\.

Declared as a ref struct because it holds spans. That restriction is the point: the view cannot be boxed, stored in a field, captured by a lambda or held across an await, so it cannot outlive the arrays it points at.

Slicing produces another view rather than copying, which makes it the natural way to hand a partition of a large cloud to a worker without allocating anything.

```csharp
public readonly ref struct PointCloud2DView
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.PointCloud2DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_)'></a>

## PointCloud2DView\(ReadOnlySpan\<double\>, ReadOnlySpan\<double\>\) Constructor

Initializes a new instance of the [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView') struct\.

When the two spans are not of equal length the view is empty, so a mismatched construction cannot produce out-of-range reads.

```csharp
public PointCloud2DView(System.ReadOnlySpan<double> x, System.ReadOnlySpan<double> y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.PointCloud2DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_).x'></a>

`x` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.PointCloud2DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_).y'></a>

`y` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The Y coordinates\.
### Properties

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Count'></a>

## PointCloud2DView\.Count Property

Gets the number of points in the view\.

```csharp
public int Count { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') point count of zero or more\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.this[int]'></a>

## PointCloud2DView\.this\[int\] Property

Gets the point at the specified index\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point this[int index] { get; }
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.this[int].index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

#### Property Value
[Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point')

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.X'></a>

## PointCloud2DView\.X Property

Gets the X coordinates\.

```csharp
public System.ReadOnlySpan<double> X { get; }
```

#### Property Value
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the X coordinates\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Y'></a>

## PointCloud2DView\.Y Property

Gets the Y coordinates\.

```csharp
public System.ReadOnlySpan<double> Y { get; }
```

#### Property Value
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the Y coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.GetEnumerator()'></a>

## PointCloud2DView\.GetEnumerator\(\) Method

Returns an enumerator that walks the view without allocating\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator GetEnumerator();
```

#### Returns
[Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView\.Enumerator')  
An [Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView\.Enumerator') positioned before the first point\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Slice(int,int)'></a>

## PointCloud2DView\.Slice\(int, int\) Method

Returns a view over a contiguous range of this view, without copying\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView Slice(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Slice(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Slice(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView')  
A [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView') over the range, empty when the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.TryGetPoint(int,double,double)'></a>

## PointCloud2DView\.TryGetPoint\(int, double, double\) Method

Retrieves a single point without allocating\.

```csharp
public bool TryGetPoint(int index, out double x, out double y);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.TryGetPoint(int,double,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.TryGetPoint(int,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.TryGetPoint(int,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Y coordinate\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the point was retrieved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator'></a>

## PointCloud2DView\.Enumerator Struct

Walks a [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView') one point at a time without allocating\.

This one must be a ref struct, because it holds the spans of the view it walks.

```csharp
public ref struct PointCloud2DView.Enumerator
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView)'></a>

## Enumerator\(PointCloud2DView\) Constructor

Initializes a new instance of the [Enumerator](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView\.Enumerator') struct\.

```csharp
public Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView pointCloud2DView);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView).pointCloud2DView'></a>

`pointCloud2DView` [PointCloud2DView](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2DView')

The view to walk\.
### Properties

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator.Current'></a>

## PointCloud2DView\.Enumerator\.Current Property

Gets the point at the current position\.

```csharp
public DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point Current { get; }
```

#### Property Value
[Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point')  
A [Point](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D.Point 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D\.Point') holding the current coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2DView.Enumerator.MoveNext()'></a>

## PointCloud2DView\.Enumerator\.MoveNext\(\) Method

Advances to the next point\.

```csharp
public bool MoveNext();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a further point is available; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
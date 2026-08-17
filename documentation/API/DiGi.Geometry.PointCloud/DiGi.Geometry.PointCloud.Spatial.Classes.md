#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Spatial\.Classes Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver'></a>

## HeightFieldPointCloud3DMeshSolver Class

Reconstructs a [DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D') from a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') by triangulating the cloud on the XY plane and carrying each vertex's Z through unchanged\.

This is a two-and-a-half dimensional reconstruction: exactly one height per XY position. That makes it fast, robust and free of tuning for terrain, floors, roofs and any other surface that is a function of plan position.

IMPORTANT: it cannot represent a vertical face, an overhang, or canopy above ground. Given a facade scan or a full building interior it will produce confident nonsense, because the model itself cannot express what the data contains. Use the isosurface solver for arbitrary geometry.

Decimation through [CellSize](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.CellSize 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.CellSize') is effectively mandatory at scale, and [PointCloudHeightSelection](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.PointCloudHeightSelection 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.HeightFieldPointCloud3DMeshSolver\.PointCloudHeightSelection') decides which measurement in a cell survives it: the lowest for bare ground, the highest for a surface model.

```csharp
public class HeightFieldPointCloud3DMeshSolver : DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver<DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D, DiGi.Geometry.Spatial.Classes.Mesh3D>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')[,](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D')[&gt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>') → HeightFieldPointCloud3DMeshSolver
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.HeightFieldPointCloud3DMeshSolver(double,double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection,double)'></a>

## HeightFieldPointCloud3DMeshSolver\(double, double, PointCloudHeightSelection, double\) Constructor

Initializes a new instance of the [HeightFieldPointCloud3DMeshSolver](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.HeightFieldPointCloud3DMeshSolver') class\.

```csharp
public HeightFieldPointCloud3DMeshSolver(double cellSize=0.0, double maximumEdgeLength=0.0, DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection pointCloudHeightSelection=DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection.Lowest, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.HeightFieldPointCloud3DMeshSolver(double,double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection,double).cellSize'></a>

`cellSize` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The edge length of the decimation grid, in model units\. Values of zero or less triangulate every point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.HeightFieldPointCloud3DMeshSolver(double,double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection,double).maximumEdgeLength'></a>

`maximumEdgeLength` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The longest edge a triangle may have, in model units\. Values of zero or less keep every triangle\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.HeightFieldPointCloud3DMeshSolver(double,double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection,double).pointCloudHeightSelection'></a>

`pointCloudHeightSelection` [PointCloudHeightSelection](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudHeightSelection')

Which measurement in a cell survives decimation\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.HeightFieldPointCloud3DMeshSolver(double,double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used when comparing coordinates\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.Input'></a>

## HeightFieldPointCloud3DMeshSolver\.Input Property

Sets the cloud to reconstruct\.

```csharp
public override DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? Input { set; }
```

Implements [Input](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2.input 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2\.Input')

#### Property Value
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
The [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') to consume\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.MaximumEdgeLength'></a>

## HeightFieldPointCloud3DMeshSolver\.MaximumEdgeLength Property

Gets or sets the longest edge a triangle may have, in model units\.

Without this, the triangulation spans the convex hull of the data and bridges every concave boundary and hole with long thin triangles.

```csharp
public double MaximumEdgeLength { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the maximum edge length\. Values of zero or less keep every triangle\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.PointCloudHeightSelection'></a>

## HeightFieldPointCloud3DMeshSolver\.PointCloudHeightSelection Property

Gets or sets which measurement in a grid cell survives decimation\.

```csharp
public DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection PointCloudHeightSelection { get; set; }
```

#### Property Value
[PointCloudHeightSelection](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudHeightSelection')  
A [PointCloudHeightSelection](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudHeightSelection') value\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver.Solve()'></a>

## HeightFieldPointCloud3DMeshSolver\.Solve\(\) Method

Runs the reconstruction\.

```csharp
public override bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a mesh was produced; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver'></a>

## IsosurfacePointCloud3DMeshSolver Class

Reconstructs a [DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D') from a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') by accumulating the points into a voxel density field and extracting an isosurface from it\.

Unlike a height field this places no constraint on the shape of the data, so vertical faces, overhangs and enclosed volumes all survive. The cost is a dense field: memory grows with the cube of the resolution, which is what [MaximumVoxelCount](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.MaximumVoxelCount 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.IsosurfacePointCloud3DMeshSolver\.MaximumVoxelCount') exists to bound.

The cell is subdivided into six tetrahedra rather than being resolved through the classic two hundred and fifty six case cube table. Both extract the same surface, but the tetrahedral decomposition has no ambiguous configurations, so it cannot produce the holes that the naive cube table yields on saddle cells, and it is driven by a handful of cases that can be read and checked rather than by four thousand table entries that cannot.

IMPORTANT LIMITATION: a cloud carries positions but no surface normals, so the field can only express how much data is nearby, not which side of a surface a voxel is on. The extracted isosurface therefore wraps the points on both sides and comes out as a thin closed shell roughly two voxels thick, not as a single surface sheet. That is inherent to reconstructing from positions alone; separating inside from outside needs oriented normals and a fitted implicit function. Treat the result as an envelope of the measured material, and expect its area to be about twice that of the surface it was scanned from.

The field accumulation and extraction run serially. They are bounded by the voxel count rather than the point count, and the vertex welding depends on a single shared edge table, so partitioning them would need a redesign rather than a parallel loop.

```csharp
public class IsosurfacePointCloud3DMeshSolver : DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver<DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D, DiGi.Geometry.Spatial.Classes.Mesh3D>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')[,](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')[DiGi\.Geometry\.Spatial\.Classes\.Mesh3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.mesh3d 'DiGi\.Geometry\.Spatial\.Classes\.Mesh3D')[&gt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>') → IsosurfacePointCloud3DMeshSolver
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double)'></a>

## IsosurfacePointCloud3DMeshSolver\(double, double, int, int, double\) Constructor

Initializes a new instance of the [IsosurfacePointCloud3DMeshSolver](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.IsosurfacePointCloud3DMeshSolver') class\.

```csharp
public IsosurfacePointCloud3DMeshSolver(double cellSize, double isoValue=0.5, int smoothingIterations=1, int maximumVoxelCount=8000000, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double).cellSize'></a>

`cellSize` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The edge length of a voxel, in model units\. Must be greater than zero\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double).isoValue'></a>

`isoValue` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The density level at which the surface is drawn\. Larger values pull the surface tighter onto the denser parts of the cloud\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double).smoothingIterations'></a>

`smoothingIterations` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of separable box filter passes applied to the field before extraction\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double).maximumVoxelCount'></a>

`maximumVoxelCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The largest number of voxels permitted\. The resolution is reduced until the field fits\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsosurfacePointCloud3DMeshSolver(double,double,int,int,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used when comparing coordinates\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.Input'></a>

## IsosurfacePointCloud3DMeshSolver\.Input Property

Sets the cloud to reconstruct\.

```csharp
public override DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? Input { set; }
```

Implements [Input](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2.input 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2\.Input')

#### Property Value
[PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')  
The [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') to consume\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.IsoValue'></a>

## IsosurfacePointCloud3DMeshSolver\.IsoValue Property

Gets or sets the density level at which the surface is drawn\.

```csharp
public double IsoValue { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the iso level\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.MaximumVoxelCount'></a>

## IsosurfacePointCloud3DMeshSolver\.MaximumVoxelCount Property

Gets or sets the largest number of voxels permitted\.

The field is dense, so this is a hard memory bound: the resolution is reduced until the field fits. It also bounds the output, because triangle count scales with the surface area measured in voxels.

```csharp
public int MaximumVoxelCount { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') holding the voxel budget\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.SmoothingIterations'></a>

## IsosurfacePointCloud3DMeshSolver\.SmoothingIterations Property

Gets or sets the number of separable box filter passes applied to the field before extraction\.

Three one-dimensional passes approximate a three-dimensional blur at a fraction of its cost. Smoothing suppresses the speckle that sparse sampling leaves in the field, at the price of rounding genuine detail.

```csharp
public int SmoothingIterations { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') holding the number of passes\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver.Solve()'></a>

## IsosurfacePointCloud3DMeshSolver\.Solve\(\) Method

Runs the reconstruction\.

```csharp
public override bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a mesh was produced; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D'></a>

## PointCloud3D Class

Represents a cloud of three\-dimensional points stored as three parallel coordinate arrays\.

See [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') for why the storage is coordinate-major rather than a list of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') objects, and for the concurrency contract.

Construct through [PointCloud3D\(this IEnumerable&lt;Point3D&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Create\.PointCloud3D\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Spatial\.Classes\.Point3D\>\)') when the input may contain non-finite coordinates. The constructors here only assign and copy; the factory performs the filtering.

```csharp
public class PointCloud3D : DiGi.Geometry.PointCloud.Core.Classes.PointCloud, DiGi.Geometry.Spatial.Interfaces.IGeometry3D, DiGi.Geometry.Core.Interfaces.IGeometry, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject, DiGi.Geometry.Spatial.Interfaces.IMovable3D, DiGi.Geometry.Spatial.Interfaces.IBoundable3D, DiGi.Geometry.Core.Interfaces.IBoundable, DiGi.Geometry.Spatial.Interfaces.ICollectable3D, DiGi.Geometry.Core.Interfaces.ICollectable, DiGi.Geometry.Spatial.Interfaces.ITransformable3D, DiGi.Geometry.Core.Interfaces.ITransformable<DiGi.Geometry.Spatial.Interfaces.ITransform3D>, DiGi.Geometry.Core.Interfaces.ITransformable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') → PointCloud3D

Derived  
↳ [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

Implements [DiGi\.Geometry\.Spatial\.Interfaces\.IGeometry3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.igeometry3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IGeometry3D'), [DiGi\.Geometry\.Core\.Interfaces\.IGeometry](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometry 'DiGi\.Geometry\.Core\.Interfaces\.IGeometry'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Geometry\.Spatial\.Interfaces\.IMovable3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.imovable3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IMovable3D'), [DiGi\.Geometry\.Spatial\.Interfaces\.IBoundable3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.iboundable3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IBoundable3D'), [DiGi\.Geometry\.Core\.Interfaces\.IBoundable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.iboundable 'DiGi\.Geometry\.Core\.Interfaces\.IBoundable'), [DiGi\.Geometry\.Spatial\.Interfaces\.ICollectable3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.icollectable3d 'DiGi\.Geometry\.Spatial\.Interfaces\.ICollectable3D'), [DiGi\.Geometry\.Core\.Interfaces\.ICollectable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.icollectable 'DiGi\.Geometry\.Core\.Interfaces\.ICollectable'), [DiGi\.Geometry\.Spatial\.Interfaces\.ITransformable3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.itransformable3d 'DiGi\.Geometry\.Spatial\.Interfaces\.ITransformable3D'), [DiGi\.Geometry\.Core\.Interfaces\.ITransformable&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable-1 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable\`1')[DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.itransform3d 'DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable-1 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable\`1'), [DiGi\.Geometry\.Core\.Interfaces\.ITransformable](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.itransformable 'DiGi\.Geometry\.Core\.Interfaces\.ITransformable')
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D)'></a>

## PointCloud3D\(PointCloud3D\) Constructor

Initializes a new instance of the [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') class by copying an existing cloud\.

```csharp
public PointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to copy from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[])'></a>

## PointCloud3D\(double\[\], double\[\], double\[\]\) Constructor

Initializes a new instance of the [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') class by copying three coordinate arrays\.

```csharp
public PointCloud3D(double[]? x, double[]? y, double[]? z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[]).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[]).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[]).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[],bool)'></a>

## PointCloud3D\(double\[\], double\[\], double\[\], bool\) Constructor

Initializes a new instance of the [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') class from three prebuilt coordinate arrays\.

```csharp
internal PointCloud3D(double[]? x, double[]? y, double[]? z, bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[],bool).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[],bool).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[],bool).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(double[],double[],double[],bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the arrays are defensively copied; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), they are adopted directly\. Use [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') only when the caller owns freshly created arrays that are not shared\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(System.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_)'></a>

## PointCloud3D\(IEnumerable\<Point3D\>\) Constructor

Initializes a new instance of the [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') class from a sequence of points\.

Null entries are skipped, since a null has no representation in a coordinate array. Non-finite coordinates are NOT filtered here; use [PointCloud3D\(this IEnumerable&lt;Point3D&gt;\)](DiGi.Geometry.PointCloud.Spatial.md#DiGi.Geometry.PointCloud.Spatial.Create.PointCloud3D(thisSystem.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_) 'DiGi\.Geometry\.PointCloud\.Spatial\.Create\.PointCloud3D\(this System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Spatial\.Classes\.Point3D\>\)') for that.

```csharp
public PointCloud3D(System.Collections.Generic.IEnumerable<DiGi.Geometry.Spatial.Classes.Point3D?>? point3Ds);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(System.Collections.Generic.IEnumerable_DiGi.Geometry.Spatial.Classes.Point3D_).point3Ds'></a>

`point3Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The points to store\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(System.Text.Json.Nodes.JsonObject)'></a>

## PointCloud3D\(JsonObject\) Constructor

Initializes a new instance of the [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public PointCloud3D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.PointCloud3D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') holding the serialized cloud\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.AsView()'></a>

## PointCloud3D\.AsView\(\) Method

Returns a zero\-copy view over the whole cloud\.

The view is a ref struct holding three spans, so it cannot escape to the heap and cannot outlive the arrays it points at. Reading through it allocates nothing.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView AsView();
```

#### Returns
[PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView')  
A [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView') over the cloud\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.AsView(int,int)'></a>

## PointCloud3D\.AsView\(int, int\) Method

Returns a zero\-copy view over a contiguous range of the cloud\.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView AsView(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.AsView(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.AsView(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView')  
A [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView') over the range, empty when the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Clone()'></a>

## PointCloud3D\.Clone\(\) Method

Creates a copy of the current object\.

```csharp
public override DiGi.Core.Interfaces.ISerializableObject? Clone();
```

Implements [Clone\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1.clone 'DiGi\.Core\.Interfaces\.ICloneableObject\`1\.Clone')

#### Returns
[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')  
A new [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject') instance that is a clone of the current object\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetBoundingBox()'></a>

## PointCloud3D\.GetBoundingBox\(\) Method

Calculates the axis\-aligned bounding box enclosing every point in the cloud\.

```csharp
public DiGi.Geometry.Spatial.Classes.BoundingBox3D? GetBoundingBox();
```

Implements [GetBoundingBox\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.iboundable3d.getboundingbox 'DiGi\.Geometry\.Spatial\.Interfaces\.IBoundable3D\.GetBoundingBox')

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D')  
A [DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.boundingbox3d 'DiGi\.Geometry\.Spatial\.Classes\.BoundingBox3D') enclosing the cloud, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetEnumerator()'></a>

## PointCloud3D\.GetEnumerator\(\) Method

Returns an enumerator that walks the cloud without allocating\.

This is duck-typed rather than an [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') implementation, and deliberately so. Implementing [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') would make this type match both `ToSystem_Bytes` overloads in DiGi.Core, and it would invite query operators that materialize one object per point and undo the entire storage design.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator GetEnumerator();
```

#### Returns
[Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Enumerator')  
An [Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Enumerator') positioned before the first point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoint(int)'></a>

## PointCloud3D\.GetPoint\(int\) Method

Retrieves the point at the specified index\.

```csharp
public DiGi.Geometry.Spatial.Classes.Point3D? GetPoint(int index);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoint(int).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')  
A new [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the index is out of range\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoints()'></a>

## PointCloud3D\.GetPoints\(\) Method

Materializes every point in the cloud as a list of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') objects\.

This allocates one object per point and is intended for interoperability, not for bulk processing. Prefer the coordinate array accessors when working at scale.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Spatial.Classes.Point3D>? GetPoints();
```

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoints(int,int)'></a>

## PointCloud3D\.GetPoints\(int, int\) Method

Materializes a contiguous range of the cloud as a list of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') objects\.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Spatial.Classes.Point3D>? GetPoints(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoints(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetPoints(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetX()'></a>

## PointCloud3D\.GetX\(\) Method

Retrieves the X coordinate array\.

```csharp
public double[]? GetX();
```

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the X coordinates, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetY()'></a>

## PointCloud3D\.GetY\(\) Method

Retrieves the Y coordinate array\.

```csharp
public double[]? GetY();
```

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the Y coordinates, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.GetZ()'></a>

## PointCloud3D\.GetZ\(\) Method

Retrieves the Z coordinate array\.

```csharp
public double[]? GetZ();
```

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the Z coordinates, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Move(DiGi.Geometry.Spatial.Classes.Vector3D)'></a>

## PointCloud3D\.Move\(Vector3D\) Method

Translates every point in the cloud by the specified vector\.

```csharp
public bool Move(DiGi.Geometry.Spatial.Classes.Vector3D? vector3D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Move(DiGi.Geometry.Spatial.Classes.Vector3D).vector3D'></a>

`vector3D` [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')

The translation vector\.

Implements [Move\(Vector3D\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.imovable3d.move#digi-geometry-spatial-interfaces-imovable3d-move(digi-geometry-spatial-classes-vector3d) 'DiGi\.Geometry\.Spatial\.Interfaces\.IMovable3D\.Move\(DiGi\.Geometry\.Spatial\.Classes\.Vector3D\)')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the cloud was moved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point3Ds()'></a>

## PointCloud3D\.Point3Ds\(\) Method

Returns the cloud as a lazy sequence of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') objects for interoperability with APIs that require them\.

Named explicitly rather than exposed through [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') so that the per-point allocation is a visible choice at the call site.

```csharp
public System.Collections.Generic.IEnumerable<DiGi.Geometry.Spatial.Classes.Point3D> Point3Ds();
```

#### Returns
[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')  
An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') of [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Transform(DiGi.Geometry.Spatial.Interfaces.ITransform3D)'></a>

## PointCloud3D\.Transform\(ITransform3D\) Method

Applies a transform to every point in the cloud\.

The transform is flattened into an affine matrix once and then streamed over the coordinate arrays, rather than being replayed per point.

```csharp
public bool Transform(DiGi.Geometry.Spatial.Interfaces.ITransform3D? transform);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Transform(DiGi.Geometry.Spatial.Interfaces.ITransform3D).transform'></a>

`transform` [DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.itransform3d 'DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D')

The transform to apply\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the cloud was transformed; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.TryGetPoint(int,double,double,double)'></a>

## PointCloud3D\.TryGetPoint\(int, double, double, double\) Method

Retrieves a single point without allocating\.

```csharp
public bool TryGetPoint(int index, out double x, out double y, out double z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.TryGetPoint(int,double,double,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.TryGetPoint(int,double,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.TryGetPoint(int,double,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Y coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.TryGetPoint(int,double,double,double).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Z coordinate\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the point was retrieved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D'></a>

## ReferencedPointCloud3D Class

Represents a cloud of three\-dimensional points in which each point carries the identifier of the DiGi model object it belongs to\.

The link is stored as one [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') per point indexing into a [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection'), which costs four bytes per point. Storing a reference object per point instead would cost well over a hundred bytes and one garbage collected object per point, reproducing exactly the overhead that [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') exists to avoid.

An identifier of -1 marks a point that links to nothing, so an unsegmented point needs no table entry and no sentinel object.

Every inherited member is safe to use. [Move\(Vector3D\)](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Move(DiGi.Geometry.Spatial.Classes.Vector3D) 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Move\(DiGi\.Geometry\.Spatial\.Classes\.Vector3D\)') and [Transform\(ITransform3D\)](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Transform(DiGi.Geometry.Spatial.Interfaces.ITransform3D) 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Transform\(DiGi\.Geometry\.Spatial\.Interfaces\.ITransform3D\)') preserve both the count and the order of the points, so the identifiers continue to line up; the nearest and counting queries return indexes rather than clouds, and an index into this cloud is an index into its identifiers.

WARNING: extension methods bind statically. Assigning this cloud to a variable typed [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') and calling a filter on it selects the overload declared for the base type, which builds a plain [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') and silently drops the links. Keep the variable typed as this class wherever the links matter.

```csharp
public class ReferencedPointCloud3D : DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') → [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') → ReferencedPointCloud3D
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D)'></a>

## ReferencedPointCloud3D\(ReferencedPointCloud3D\) Constructor

Initializes a new instance of the [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') class by copying an existing cloud\.

The identifiers and the reference table are both copied, not shared. This is what lets a filter return the source cloud unchanged when its query covers everything, without the copy and the source aliasing one table.

```csharp
public ReferencedPointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D? referencedPointCloud3D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D).referencedPointCloud3D'></a>

`referencedPointCloud3D` [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D')

The cloud to copy from\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection)'></a>

## ReferencedPointCloud3D\(double\[\], double\[\], double\[\], int\[\], PointCloudReferenceCollection\) Constructor

Initializes a new instance of the [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') class by copying three coordinate arrays and their per\-point identifiers\.

```csharp
public ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, which must hold one value per point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).pointCloudReferenceCollection'></a>

`pointCloudReferenceCollection` [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

The reference table the identifiers index into\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool)'></a>

## ReferencedPointCloud3D\(double\[\], double\[\], double\[\], int\[\], PointCloudReferenceCollection, bool\) Constructor

Initializes a new instance of the [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') class from three prebuilt coordinate arrays and their per\-point identifiers\.

The identifiers are attached only when they hold exactly one value per point. A mismatched array is dropped rather than adopted, mirroring the way a ragged coordinate array is dropped, because a cloud with no links is recoverable while a cloud whose links are offset by one silently attributes every point to the wrong model object.

```csharp
internal ReferencedPointCloud3D(double[]? x, double[]? y, double[]? z, int[]? referenceIndexes, DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection, bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, which must hold one value per point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).pointCloudReferenceCollection'></a>

`pointCloudReferenceCollection` [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

The reference table the identifiers index into\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(double[],double[],double[],int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection,bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the arrays are defensively copied; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), they are adopted directly\. Use [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') only when the caller owns freshly created arrays that are not shared\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(System.Text.Json.Nodes.JsonObject)'></a>

## ReferencedPointCloud3D\(JsonObject\) Constructor

Initializes a new instance of the [ReferencedPointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public ReferencedPointCloud3D(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferencedPointCloud3D(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') holding the serialized cloud\.
### Fields

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.pointCloudReferenceCollection'></a>

## ReferencedPointCloud3D\.pointCloudReferenceCollection Field

The distinct model objects this cloud links to, indexed by the identifiers\.

Serialized as an ordinary member rather than folded into the encoded payload, so that the references keep their concrete types through the polymorphic type discriminator and stay readable in the document.

```csharp
private readonly PointCloudReferenceCollection? pointCloudReferenceCollection;
```

#### Field Value
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.referenceIndexes'></a>

## ReferencedPointCloud3D\.referenceIndexes Field

The per\-point identifiers, one per point and in point order, where \-1 marks a point that links to nothing\.

Not marked for serialization: an array member would be written as one JSON number per element. The payload travels through [ReferenceIndexData](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferenceIndexData 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D\.ReferenceIndexData') instead, exactly as the coordinates travel through their own encoded property.

Cannot be readonly, because [ReferenceIndexData](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferenceIndexData 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D\.ReferenceIndexData') assigns it from its property setter during deserialization.

```csharp
private int[]? referenceIndexes;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.IsReferenced'></a>

## ReferencedPointCloud3D\.IsReferenced Property

Gets a value indicating whether the identifiers are present and line up with the points\.

The two halves of the cloud are decoded independently, and the serializer applies members in the order they appear in the document, so this is the check that a document did in fact carry both halves and that they agree.

```csharp
public bool IsReferenced { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when one identifier is stored per point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.PointCloudReferenceCollection'></a>

## ReferencedPointCloud3D\.PointCloudReferenceCollection Property

Gets the distinct model objects this cloud links to, indexed by the identifiers\.

Returns a copy. Use [GetPointCloudReferenceCollection\(bool\)](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetPointCloudReferenceCollection(bool) 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.ReferencedPointCloud3D\.GetPointCloudReferenceCollection\(bool\)') when the copy is not wanted.

```csharp
public DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? PointCloudReferenceCollection { get; }
```

#### Property Value
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')  
The reference table, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries none\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferenceCount'></a>

## ReferencedPointCloud3D\.ReferenceCount Property

Gets the number of distinct model objects this cloud links to\.

```csharp
public int ReferenceCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') entry count of zero or more\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.ReferenceIndexData'></a>

## ReferencedPointCloud3D\.ReferenceIndexData Property

Gets or sets the serialized identifier payload as a Base64 encoding of the binary point cloud reference format\.

This member exists in this exact shape for the same reason as the coordinate payload: the reflection serializer writes an array member as one JSON number per element, and a get-only property is written but silently discarded on read.

The reference table is NOT embedded here, because it is already serialized as its own member and writing it twice would let the two copies disagree.

```csharp
private string? ReferenceIndexData { private get; private set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.Clone()'></a>

## ReferencedPointCloud3D\.Clone\(\) Method

Creates a copy of the current object\.

```csharp
public override DiGi.Core.Interfaces.ISerializableObject? Clone();
```

Implements [Clone\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1.clone 'DiGi\.Core\.Interfaces\.ICloneableObject\`1\.Clone')

#### Returns
[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')  
A new [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject') instance that is a clone of the current object\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetPointCloudReferenceCollection(bool)'></a>

## ReferencedPointCloud3D\.GetPointCloudReferenceCollection\(bool\) Method

Retrieves the reference table, optionally without copying\.

```csharp
public DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? GetPointCloudReferenceCollection(bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetPointCloudReferenceCollection(bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), a copy is returned; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the internal table is returned directly and must not be modified by the caller\.

#### Returns
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')  
The reference table, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries none\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetReference(int)'></a>

## ReferencedPointCloud3D\.GetReference\(int\) Method

Retrieves the model object a point links to\.

```csharp
public DiGi.Core.Interfaces.ISerializableReference? GetReference(int index);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetReference(int).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

#### Returns
[DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')  
A copy of the reference, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the index is out of range or the point links to nothing\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetReferenceIndexes()'></a>

## ReferencedPointCloud3D\.GetReferenceIndexes\(\) Method

Retrieves the per\-point identifiers\.

```csharp
public int[]? GetReferenceIndexes();
```

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the identifiers, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries none\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetReferenceIndexes(bool)'></a>

## ReferencedPointCloud3D\.GetReferenceIndexes\(bool\) Method

Retrieves the per\-point identifiers, optionally without copying\.

```csharp
public int[]? GetReferenceIndexes(bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.GetReferenceIndexes(bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), a copy is returned; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the internal array is returned directly and must not be modified by the caller\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
The identifiers, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud carries none\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.TryGetReferenceIndex(int,int)'></a>

## ReferencedPointCloud3D\.TryGetReferenceIndex\(int, int\) Method

Retrieves the identifier of the model object a point links to, without allocating\.

The index is checked against the identifier array rather than against the point count, so that a document carrying inconsistent halves reports a miss instead of reading past the end.

```csharp
public bool TryGetReferenceIndex(int index, out int referenceIndex);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.TryGetReferenceIndex(int,int).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.ReferencedPointCloud3D.TryGetReferenceIndex(int,int).referenceIndex'></a>

`referenceIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the identifier, or \-1 when the index is out of range or the point links to nothing\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the point links to a model object; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
### Structs

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator'></a>

## PointCloud3D\.Enumerator Struct

Walks a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') one point at a time without allocating\.

A plain struct rather than a ref struct, so it remains usable inside iterators, lambdas and asynchronous methods. The span-based counterpart lives on [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView').

```csharp
public struct PointCloud3D.Enumerator
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D)'></a>

## Enumerator\(PointCloud3D\) Constructor

Initializes a new instance of the [Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Enumerator') struct\.

```csharp
public Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D? pointCloud3D);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D).pointCloud3D'></a>

`pointCloud3D` [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

The cloud to walk\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator.Current'></a>

## PointCloud3D\.Enumerator\.Current Property

Gets the point at the current position\.

```csharp
public readonly DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point Current { get; }
```

#### Property Value
[Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point')  
A [Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point') holding the current coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Enumerator.MoveNext()'></a>

## PointCloud3D\.Enumerator\.MoveNext\(\) Method

Advances to the next point\.

```csharp
public bool MoveNext();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a further point is available; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point'></a>

## PointCloud3D\.Point Struct

Represents a single point of a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D') as a value\.

A plain readonly struct rather than a ref struct: a point holds three doubles and no reference, so the ref struct restrictions would buy nothing while preventing use in generics, lambdas, arrays and lists.

```csharp
public readonly struct PointCloud3D.Point
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Point(double,double,double)'></a>

## Point\(double, double, double\) Constructor

Initializes a new instance of the [Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point') struct\.

```csharp
public Point(double x, double y, double z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Point(double,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Point(double,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Point(double,double,double).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.X'></a>

## PointCloud3D\.Point\.X Property

Gets the X coordinate\.

```csharp
public double X { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Y'></a>

## PointCloud3D\.Point\.Y Property

Gets the Y coordinate\.

```csharp
public double Y { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the Y coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.Z'></a>

## PointCloud3D\.Point\.Z Property

Gets the Z coordinate\.

```csharp
public double Z { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the Z coordinate\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point.ToPoint3D()'></a>

## PointCloud3D\.Point\.ToPoint3D\(\) Method

Materializes this value as a [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D') object\.

```csharp
public DiGi.Geometry.Spatial.Classes.Point3D ToPoint3D();
```

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')  
A new [DiGi\.Geometry\.Spatial\.Classes\.Point3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.point3d 'DiGi\.Geometry\.Spatial\.Classes\.Point3D')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView'></a>

## PointCloud3DView Struct

Represents a zero\-copy, read\-only window onto the coordinate arrays of a [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')\.

Declared as a ref struct because it holds spans. That restriction is the point: the view cannot be boxed, stored in a field, captured by a lambda or held across an await, so it cannot outlive the arrays it points at.

Slicing produces another view rather than copying, which makes it the natural way to hand a partition of a large cloud to a worker without allocating anything.

```csharp
public readonly ref struct PointCloud3DView
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.PointCloud3DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_,System.ReadOnlySpan_double_)'></a>

## PointCloud3DView\(ReadOnlySpan\<double\>, ReadOnlySpan\<double\>, ReadOnlySpan\<double\>\) Constructor

Initializes a new instance of the [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView') struct\.

When the three spans are not of equal length the view is empty, so a mismatched construction cannot produce out-of-range reads.

```csharp
public PointCloud3DView(System.ReadOnlySpan<double> x, System.ReadOnlySpan<double> y, System.ReadOnlySpan<double> z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.PointCloud3DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_,System.ReadOnlySpan_double_).x'></a>

`x` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.PointCloud3DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_,System.ReadOnlySpan_double_).y'></a>

`y` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.PointCloud3DView(System.ReadOnlySpan_double_,System.ReadOnlySpan_double_,System.ReadOnlySpan_double_).z'></a>

`z` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The Z coordinates\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Count'></a>

## PointCloud3DView\.Count Property

Gets the number of points in the view\.

```csharp
public int Count { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') point count of zero or more\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.this[int]'></a>

## PointCloud3DView\.this\[int\] Property

Gets the point at the specified index\.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point this[int index] { get; }
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.this[int].index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

#### Property Value
[Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point')

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.X'></a>

## PointCloud3DView\.X Property

Gets the X coordinates\.

```csharp
public System.ReadOnlySpan<double> X { get; }
```

#### Property Value
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the X coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Y'></a>

## PointCloud3DView\.Y Property

Gets the Y coordinates\.

```csharp
public System.ReadOnlySpan<double> Y { get; }
```

#### Property Value
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the Y coordinates\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Z'></a>

## PointCloud3DView\.Z Property

Gets the Z coordinates\.

```csharp
public System.ReadOnlySpan<double> Z { get; }
```

#### Property Value
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the Z coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.GetEnumerator()'></a>

## PointCloud3DView\.GetEnumerator\(\) Method

Returns an enumerator that walks the view without allocating\.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator GetEnumerator();
```

#### Returns
[Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView\.Enumerator')  
An [Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView\.Enumerator') positioned before the first point\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Slice(int,int)'></a>

## PointCloud3DView\.Slice\(int, int\) Method

Returns a view over a contiguous range of this view, without copying\.

```csharp
public DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView Slice(int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Slice(int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Slice(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView')  
A [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView') over the range, empty when the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.TryGetPoint(int,double,double,double)'></a>

## PointCloud3DView\.TryGetPoint\(int, double, double, double\) Method

Retrieves a single point without allocating\.

```csharp
public bool TryGetPoint(int index, out double x, out double y, out double z);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.TryGetPoint(int,double,double,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.TryGetPoint(int,double,double,double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the X coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.TryGetPoint(int,double,double,double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Y coordinate\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.TryGetPoint(int,double,double,double).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the Z coordinate\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the point was retrieved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator'></a>

## PointCloud3DView\.Enumerator Struct

Walks a [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView') one point at a time without allocating\.

This one must be a ref struct, because it holds the spans of the view it walks.

```csharp
public ref struct PointCloud3DView.Enumerator
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView)'></a>

## Enumerator\(PointCloud3DView\) Constructor

Initializes a new instance of the [Enumerator](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView\.Enumerator') struct\.

```csharp
public Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView pointCloud3DView);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator.Enumerator(DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView).pointCloud3DView'></a>

`pointCloud3DView` [PointCloud3DView](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3DView')

The view to walk\.
### Properties

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator.Current'></a>

## PointCloud3DView\.Enumerator\.Current Property

Gets the point at the current position\.

```csharp
public readonly DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point Current { get; }
```

#### Property Value
[Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point')  
A [Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point') holding the current coordinates\.
### Methods

<a name='DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3DView.Enumerator.MoveNext()'></a>

## PointCloud3DView\.Enumerator\.MoveNext\(\) Method

Advances to the next point\.

```csharp
public bool MoveNext();
```

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a further point is available; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
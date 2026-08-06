#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Core\.Interfaces Namespace
### Interfaces

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud'></a>

## IPointCloud Interface

Represents an unordered collection of points held in a coordinate\-major layout, sized for bulk streaming rather than random access\.

```csharp
public interface IPointCloud : DiGi.Geometry.Core.Interfaces.IGeometry, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject
```

Derived  
↳ [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud')

Implements [DiGi\.Geometry\.Core\.Interfaces\.IGeometry](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometry 'DiGi\.Geometry\.Core\.Interfaces\.IGeometry'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud.Count'></a>

## IPointCloud\.Count Property

Gets the number of points in the cloud, or zero when the cloud holds no coordinate data\.

```csharp
int Count { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud.Dimension'></a>

## IPointCloud\.Dimension Property

Gets the number of coordinate axes, which is two for a planar cloud and three for a spatial one\.

```csharp
int Dimension { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_'></a>

## IPointCloudMeshSolver\<TPointCloud,TMesh\> Interface

Represents a pluggable strategy for reconstructing a mesh from a point cloud\.

Surface reconstruction has no single right answer, so the strategy is a parameter rather than a hard-coded step. A height field triangulation is the right tool for terrain, floors and roofs; an isosurface extraction is the right tool for an arbitrary scan. Each carries limitations that make it wrong for the other's data, so the choice belongs to the caller.

```csharp
public interface IPointCloudMeshSolver<TPointCloud,TMesh> : DiGi.Geometry.Core.Interfaces.IOneToOneGeometrySolver<TPointCloud, TMesh>, DiGi.Geometry.Core.Interfaces.IGeometrySolver, DiGi.Core.Interfaces.ISolver, DiGi.Core.Interfaces.IEvaluator, DiGi.Core.Interfaces.IOneToOneSolver<TPointCloud, TMesh>
    where TPointCloud : DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud
    where TMesh : DiGi.Geometry.Core.Interfaces.IMesh
```
#### Type parameters

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud'></a>

`TPointCloud`

The point cloud type consumed\.

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TMesh'></a>

`TMesh`

The mesh type produced\.

Derived  
↳ [PointCloudMeshSolver&lt;TPointCloud,TMesh&gt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>')

Implements [DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2')[TPointCloud](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2')[TMesh](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2'), [DiGi\.Geometry\.Core\.Interfaces\.IGeometrySolver](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometrysolver 'DiGi\.Geometry\.Core\.Interfaces\.IGeometrySolver'), [DiGi\.Core\.Interfaces\.ISolver](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver 'DiGi\.Core\.Interfaces\.ISolver'), [DiGi\.Core\.Interfaces\.IEvaluator](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ievaluator 'DiGi\.Core\.Interfaces\.IEvaluator'), [DiGi\.Core\.Interfaces\.IOneToOneSolver&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')[TPointCloud](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')[TMesh](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.CellSize'></a>

## IPointCloudMeshSolver\<TPointCloud,TMesh\>\.CellSize Property

Gets or sets the edge length of the working grid, in model units\.

This is the single knob that trades detail against cost, and it is not optional: reconstruction cost grows far faster than linearly with the number of sites, so it is what keeps a cloud of millions tractable.

```csharp
double CellSize { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.Tolerance'></a>

## IPointCloudMeshSolver\<TPointCloud,TMesh\>\.Tolerance Property

Gets or sets the distance tolerance used when comparing coordinates\.

```csharp
double Tolerance { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')
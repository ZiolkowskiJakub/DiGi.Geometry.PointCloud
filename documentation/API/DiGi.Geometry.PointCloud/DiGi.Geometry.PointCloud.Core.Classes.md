#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Core\.Classes Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud'></a>

## PointCloud Class

Represents an abstract, dimension\-agnostic cloud of points stored one array per coordinate axis\.

The layout is deliberately coordinate-major rather than a list of point objects. A point object derived from [DiGi\.Geometry\.Core\.Classes\.Coordinate](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.classes.coordinate 'DiGi\.Geometry\.Core\.Classes\.Coordinate') is a heap object wrapping its own array, which costs roughly eighty bytes and two allocations per point; ten million points would occupy about eight hundred megabytes across twenty million objects that the garbage collector must trace. Three plain [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') arrays hold the same data in about two hundred and forty megabytes across three allocations, and because a [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array contains no references the collector marks the header and never walks the payload.

The layout is also what makes vectorisation possible: four consecutive values in an axis array are four different points' values for that axis, which is exactly the shape a lane-wise minimum or comparison needs. Interleaved storage would mix axes within a lane, and point objects are not contiguous at all.

This diverges from [DiGi\.Geometry\.Core\.Classes\.Mesh&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.classes.mesh-1 'DiGi\.Geometry\.Core\.Classes\.Mesh\`1') on purpose. A mesh is random-access and topology-driven at thousands to a million vertices, where the object-per-vertex cost is irrelevant and per-vertex behaviour is required. A cloud is bulk and streaming at millions to billions of points with no topology at all.

Instances are safe for concurrent reads. Mutation through a move or transform requires external synchronization, the same contract as a standard list.

```csharp
public abstract class PointCloud : DiGi.Core.Classes.SerializableObject, DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud, DiGi.Geometry.Core.Interfaces.IGeometry, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → PointCloud

Derived  
↳ [PointCloud2D](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.PointCloud2D 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.PointCloud2D')  
↳ [PointCloud3D](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D')

Implements [IPointCloud](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloud'), [DiGi\.Geometry\.Core\.Interfaces\.IGeometry](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometry 'DiGi\.Geometry\.Core\.Interfaces\.IGeometry'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(DiGi.Geometry.PointCloud.Core.Classes.PointCloud,int)'></a>

## PointCloud\(PointCloud, int\) Constructor

Initializes a new instance of the [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') class by copying the coordinate arrays of an existing cloud\.

```csharp
protected PointCloud(DiGi.Geometry.PointCloud.Core.Classes.PointCloud? pointCloud, int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(DiGi.Geometry.PointCloud.Core.Classes.PointCloud,int).pointCloud'></a>

`pointCloud` [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud')

The cloud to copy from\. This value can be null\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(DiGi.Geometry.PointCloud.Core.Classes.PointCloud,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(double[][],int,bool)'></a>

## PointCloud\(double\[\]\[\], int, bool\) Constructor

Initializes a new instance of the [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') class from prebuilt coordinate arrays\.

```csharp
protected PointCloud(double[][]? coordinates, int dimension, bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(double[][],int,bool).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(double[][],int,bool).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(double[][],int,bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the arrays are defensively copied; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), they are adopted directly\. Use [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') only when the caller owns freshly created arrays that are not shared, which is the whole point of the filtering paths\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(int)'></a>

## PointCloud\(int\) Constructor

Initializes a new empty instance of the [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') class\.

```csharp
protected PointCloud(int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(System.Text.Json.Nodes.JsonObject,int)'></a>

## PointCloud\(JsonObject, int\) Constructor

Initializes a new instance of the [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

The dimension is assigned before deserialization runs, mirroring [DiGi\.Geometry\.Core\.Classes\.Coordinate\.\#ctor\(System\.Text\.Json\.Nodes\.JsonObject,System\.Int32\)](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.classes.coordinate.-ctor#digi-geometry-core-classes-coordinate-ctor(system-text-json-nodes-jsonobject-system-int32) 'DiGi\.Geometry\.Core\.Classes\.Coordinate\.\#ctor\(System\.Text\.Json\.Nodes\.JsonObject,System\.Int32\)'), because the coordinate payload cannot be validated without it.

```csharp
protected PointCloud(System.Text.Json.Nodes.JsonObject? jsonObject, int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(System.Text.Json.Nodes.JsonObject,int).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') holding the serialized cloud\. This value can be null\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.PointCloud(System.Text.Json.Nodes.JsonObject,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.coordinates'></a>

## PointCloud\.coordinates Field

The coordinate arrays, one per axis and all of equal length\. Index zero is X, index one is Y and index two, when present, is Z\.

Not marked for serialization: the reflection serializer emits an array member as one JSON number per element, which for a large cloud would produce tens of millions of JSON value objects. The payload travels through [CoordinateData](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud.CoordinateData 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud\.CoordinateData') instead.

Cannot be readonly, because [CoordinateData](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud.CoordinateData 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud\.CoordinateData') assigns it from its property setter during deserialization.

```csharp
protected double[][]? coordinates;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.dimension'></a>

## PointCloud\.dimension Field

The number of coordinate axes, fixed by the concrete type at construction\.

Held as a field rather than an abstract property because the JSON constructor must know the dimension before it deserializes, and calling an overridable member from a constructor is a defect the analyzers flag.

```csharp
protected readonly int dimension;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.object_PointCloudIndexLock'></a>

## PointCloud\.object\_PointCloudIndexLock Field

Guards construction of the cached spatial index\.

A plain object rather than the dedicated lock type introduced in recent framework versions, which does not exist on this target.

```csharp
private readonly object object_PointCloudIndexLock;
```

#### Field Value
[System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.pointCloudIndex'></a>

## PointCloud\.pointCloudIndex Field

The cached spatial index, derived data that is rebuilt on demand and never serialized\.

A plain field with no serialization attribute, which the reflection serializer skips: fields are opt-in, unlike properties, which are opt-out.

```csharp
private PointCloudIndex? pointCloudIndex;
```

#### Field Value
[PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex')
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.CoordinateData'></a>

## PointCloud\.CoordinateData Property

Gets or sets the serialized coordinate payload as a Base64 encoding of the binary point cloud format\.

This member exists in this exact shape because of how the reflection serializer works. An array member would be written as one JSON number per element; a get-only property would be written but silently discarded on read, yielding an empty object with no error. A settable property carrying a single string is the only shape that round-trips.

A Base64 payload is roughly three times smaller than a JSON number array and needs no number parsing, but it still materializes the whole cloud as a string. Use the binary conversion helpers for anything beyond a few million points.

```csharp
private string? CoordinateData { private get; private set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.Count'></a>

## PointCloud\.Count Property

Gets the number of points in the cloud\.

Returns zero rather than a negative sentinel when the cloud holds no data, so that a counted loop becomes a no-op and an allocation sized from this value succeeds. This deliberately differs from [DiGi\.Geometry\.Core\.Classes\.Mesh&lt;&gt;\.PointsCount](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.classes.mesh-1.pointscount 'DiGi\.Geometry\.Core\.Classes\.Mesh\`1\.PointsCount').

```csharp
public int Count { get; }
```

Implements [Count](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud.Count 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloud\.Count')

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') point count of zero or more\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.Dimension'></a>

## PointCloud\.Dimension Property

Gets the number of coordinate axes\.

```csharp
public int Dimension { get; }
```

Implements [Dimension](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud.Dimension 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloud\.Dimension')

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') equal to two for a planar cloud and three for a spatial one\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.IsIndexed'></a>

## PointCloud\.IsIndexed Property

Gets a value indicating whether a spatial index is currently cached for this cloud\.

```csharp
public bool IsIndexed { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when an index has been built and not since invalidated\.
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int)'></a>

## PointCloud\.AsSpan\(int\) Method

Returns a read\-only view over the coordinate array for a single axis, without copying\.

This is the allocation-free way to hand an axis to a vectorised or streaming kernel. A span cannot be stored in a field, so the cloud itself continues to hold plain arrays and produces spans on demand.

```csharp
public System.ReadOnlySpan<double> AsSpan(int axis);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int).axis'></a>

`axis` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based axis index, where zero is X, one is Y and two is Z\.

#### Returns
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the axis, or an empty span when the cloud is empty or the axis is out of range\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int,int,int)'></a>

## PointCloud\.AsSpan\(int, int, int\) Method

Returns a read\-only view over a contiguous range of the coordinate array for a single axis, without copying\.

```csharp
public System.ReadOnlySpan<double> AsSpan(int axis, int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int,int,int).axis'></a>

`axis` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based axis index, where zero is X, one is Y and two is Z\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int,int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.AsSpan(int,int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of values in the range\.

#### Returns
[System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')  
A [System\.ReadOnlySpan&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1') over the range, or an empty span when the cloud is empty or the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.EnsureIndex()'></a>

## PointCloud\.EnsureIndex\(\) Method

Returns the cached spatial index, building it on first use\.

Built lazily rather than in a constructor, because constructing one is an order-of-count sweep and a caller who never runs a spatial query should not pay for it. Clouds below [IndexThreshold](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexThreshold 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.IndexThreshold') points never get an index at all: an exhaustive vectorised scan over that many points finishes in tens of microseconds, which is less than any index build could cost.

Concurrent readers are safe. The double-checked read means the common case takes no lock, and losing the race merely means one redundant build is discarded.

```csharp
internal DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex? EnsureIndex();
```

#### Returns
[PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex')  
The cached [PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is too small or cannot be indexed\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(bool)'></a>

## PointCloud\.GetCoordinates\(bool\) Method

Retrieves every coordinate array, optionally without copying\.

```csharp
public double[][]? GetCoordinates(bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), a deep copy is returned; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the internal arrays are returned directly and must not be modified by the caller\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding one array per axis, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(int)'></a>

## PointCloud\.GetCoordinates\(int\) Method

Retrieves the coordinate array for a single axis\.

```csharp
public double[]? GetCoordinates(int axis);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(int).axis'></a>

`axis` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based axis index, where zero is X, one is Y and two is Z\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A copy of the axis array, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the axis is out of range\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(int,bool)'></a>

## PointCloud\.GetCoordinates\(int, bool\) Method

Retrieves the coordinate array for a single axis, optionally without copying\.

```csharp
public double[]? GetCoordinates(int axis, bool clone);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(int,bool).axis'></a>

`axis` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based axis index, where zero is X, one is Y and two is Z\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.GetCoordinates(int,bool).clone'></a>

`clone` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), a copy is returned; when [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), the internal array is returned directly and must not be modified by the caller\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
The axis array, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the cloud is empty or the axis is out of range\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.InvalidateIndex()'></a>

## PointCloud\.InvalidateIndex\(\) Method

Discards the cached spatial index\.

Every mutation of the coordinate arrays MUST call this. An index describes where the points were, so a move or a transform that left it in place would silently answer later queries against stale geometry.

```csharp
protected void InvalidateIndex();
```

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.TryGetCoordinate(int,int,double)'></a>

## PointCloud\.TryGetCoordinate\(int, int, double\) Method

Retrieves a single coordinate value without allocating\.

```csharp
public bool TryGetCoordinate(int index, int axis, out double value);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.TryGetCoordinate(int,int,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based point index\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.TryGetCoordinate(int,int,double).axis'></a>

`axis` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based axis index, where zero is X, one is Y and two is Z\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloud.TryGetCoordinate(int,int,double).value'></a>

`value` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the coordinate value, or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when the request is out of range\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the value was retrieved; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex'></a>

## PointCloudIndex Class

Represents a pointerless spatial index over a point cloud: a Z\-order sorted permutation of the points, plus a table of nodes describing the hierarchy above it\.

The structure is a linear octree in three dimensions and a linear quadtree in two. Because the points are sorted by Z-order cell identifier, every node owns a contiguous range of the permutation, and the hierarchy is derived by repeatedly dropping the low bits of the cell identifiers. There are no child pointers to chase and no per-node allocations.

The index never touches the cloud's coordinate arrays. It owns a permutation instead. Reordering the cloud during a read would change the observable order of its points, which is a surprising and racy side effect on a type that is otherwise safe to read concurrently.

A query classifies each node against the search box: disjoint nodes are pruned outright, fully contained nodes contribute their whole range with no per-point test at all, and only partially overlapping leaves are examined point by point. That is where the speed comes from — for a small box the work is proportional to the answer, not to the cloud.

```csharp
internal sealed class PointCloudIndex
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → PointCloudIndex
### Constructors

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.PointCloudIndex(int,int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode[])'></a>

## PointCloudIndex\(int, int\[\], PointCloudIndexNode\[\]\) Constructor

Initializes a new instance of the [PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex') class\.

```csharp
internal PointCloudIndex(int dimension, int[] order, DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode[] nodes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.PointCloudIndex(int,int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode[]).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.PointCloudIndex(int,int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode[]).order'></a>

`order` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Z\-order sorted permutation of point indexes\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.PointCloudIndex(int,int[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode[]).nodes'></a>

`nodes` [PointCloudIndexNode](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndexNode')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The node table, with the root at index zero\.
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.Count'></a>

## PointCloudIndex\.Count Property

Gets the number of points covered by the index\.

```csharp
public int Count { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') point count\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NodeCount'></a>

## PointCloudIndex\.NodeCount Property

Gets the number of nodes in the hierarchy\.

```csharp
public int NodeCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') node count\.
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int)'></a>

## PointCloudIndex\.DistanceSquaredToBounds\(PointCloudIndexNode, double, double, double, int\) Method

Calculates the squared distance from a position to the nearest point of a node box, which is zero when the position lies inside it\.

```csharp
private static double DistanceSquaredToBounds(in DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode node, double x, double y, double z, int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int).node'></a>

`node` [PointCloudIndexNode](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndexNode')

The node whose box is measured\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the position\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the position\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate of the position\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.DistanceSquaredToBounds(DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode,double,double,double,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\. The Z term is omitted for two, where the node box carries a placeholder depth of zero\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') squared distance\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.InRangeIndexes(double[][],double[],double[])'></a>

## PointCloudIndex\.InRangeIndexes\(double\[\]\[\], double\[\], double\[\]\) Method

Retrieves the indexes of the points that fall inside an axis\-aligned box\.

The result is sorted ascending so that it matches an exhaustive scan exactly, both in content and in order. Without that, a filtered cloud would come back in spatial order below the index threshold and in input order above it, which would make the result depend on the size of the input.

The traversal stack is stack-allocated. The depth is bounded by the index depth, so the bound is known at compile time and there is no allocation and no pooled buffer to return.

```csharp
public int[]? InRangeIndexes(double[][]? coordinates, double[]? minimums, double[]? maximums);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.InRangeIndexes(double[][],double[],double[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays of the cloud the index was built for\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.InRangeIndexes(double[][],double[],double[]).minimums'></a>

`minimums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive lower bound of each axis, tolerance already folded in\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.InRangeIndexes(double[][],double[],double[]).maximums'></a>

`maximums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive upper bound of each axis, tolerance already folded in\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array of ascending point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is mismatched\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_)'></a>

## PointCloudIndex\.NearestIndexes\(double\[\]\[\], double, double, double, Span\<int\>, Span\<double\>\) Method

Retrieves the indexes of the points closest to a query position, nearest first\.

The traversal is a depth-first descent that visits the children of every node in order of their distance from the query, nearest first. That ordering is what makes it fast: the very first leaf reached is the one containing the query, so the candidate set fills with genuinely close points immediately and the rejection radius collapses to a small value before any sibling is considered. Every remaining node is then dismissed by a single comparison. A best-first search with a priority queue visits the same nodes for a request this small, and pays for a heap to do it.

Nodes are rejected when the distance from the query to the node box is not smaller than the distance to the furthest candidate held so far. This is sound because the boxes are single precision rounded outward, so they enclose more space than the points they own: the measured distance to a box can only understate the distance to the nearest point inside it, and understating it can only preserve a node that would otherwise be dropped.

Nothing is allocated. The candidate set, the child ordering buffers and the traversal stack are all supplied by the caller or stack-allocated, and the whole search runs on scalar values without materializing a single point object.

```csharp
public int NearestIndexes(double[][]? coordinates, double x, double y, double z, System.Span<int> indexes, System.Span<double> distancesSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays of the cloud the index was built for\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The X coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Y coordinate of the query position\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).z'></a>

`z` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The Z coordinate of the query position\. Ignored for a planar index\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex.NearestIndexes(double[][],double,double,double,System.Span_int_,System.Span_double_).indexes 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex\.NearestIndexes\(double\[\]\[\], double, double, double, System\.Span\<int\>, System\.Span\<double\>\)\.indexes')\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, which is smaller than the requested count when the cloud holds fewer points, or \-1 when the input is mismatched or the traversal stack overflowed\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\> Class

Represents an abstract base for point cloud mesh reconstruction strategies, holding the shared settings and the produced mesh\.

Not a serializable object, matching the other solvers in the geometry library: a solver is a transient piece of machinery, not part of a model.

```csharp
public abstract class PointCloudMeshSolver<TPointCloud,TMesh> : DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver<TPointCloud, TMesh>, DiGi.Geometry.Core.Interfaces.IOneToOneGeometrySolver<TPointCloud, TMesh>, DiGi.Geometry.Core.Interfaces.IGeometrySolver, DiGi.Core.Interfaces.ISolver, DiGi.Core.Interfaces.IEvaluator, DiGi.Core.Interfaces.IOneToOneSolver<TPointCloud, TMesh>
    where TPointCloud : DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloud
    where TMesh : DiGi.Geometry.Core.Interfaces.IMesh
```
#### Type parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud'></a>

`TPointCloud`

The point cloud type consumed\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh'></a>

`TMesh`

The mesh type produced\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → PointCloudMeshSolver\<TPointCloud,TMesh\>

Derived  
↳ [DelaunayPointCloud2DMeshSolver](DiGi.Geometry.PointCloud.Planar.Classes.md#DiGi.Geometry.PointCloud.Planar.Classes.DelaunayPointCloud2DMeshSolver 'DiGi\.Geometry\.PointCloud\.Planar\.Classes\.DelaunayPointCloud2DMeshSolver')  
↳ [HeightFieldPointCloud3DMeshSolver](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.HeightFieldPointCloud3DMeshSolver 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.HeightFieldPointCloud3DMeshSolver')  
↳ [IsosurfacePointCloud3DMeshSolver](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.IsosurfacePointCloud3DMeshSolver 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.IsosurfacePointCloud3DMeshSolver')

Implements [DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver&lt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[TPointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')[,](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>')[TMesh](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')[&gt;](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>'), [DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2')[TPointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2')[TMesh](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.ionetoonegeometrysolver-2 'DiGi\.Geometry\.Core\.Interfaces\.IOneToOneGeometrySolver\`2'), [DiGi\.Geometry\.Core\.Interfaces\.IGeometrySolver](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.core.interfaces.igeometrysolver 'DiGi\.Geometry\.Core\.Interfaces\.IGeometrySolver'), [DiGi\.Core\.Interfaces\.ISolver](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver 'DiGi\.Core\.Interfaces\.ISolver'), [DiGi\.Core\.Interfaces\.IEvaluator](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ievaluator 'DiGi\.Core\.Interfaces\.IEvaluator'), [DiGi\.Core\.Interfaces\.IOneToOneSolver&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')[TPointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')[TMesh](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2')
### Constructors

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.PointCloudMeshSolver(double,double)'></a>

## PointCloudMeshSolver\(double, double\) Constructor

Initializes a new instance of the [PointCloudMeshSolver&lt;TPointCloud,TMesh&gt;](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_ 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>') class\.

```csharp
protected PointCloudMeshSolver(double cellSize, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.PointCloudMeshSolver(double,double).cellSize'></a>

`cellSize` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The edge length of the working grid, in model units\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.PointCloudMeshSolver(double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used when comparing coordinates\.
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.cellSize'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.cellSize Field

The edge length of the working grid, in model units\.

```csharp
protected double cellSize;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.output'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.output Field

The mesh produced by the most recent successful solve\.

```csharp
protected TMesh? output;
```

#### Field Value
[TMesh](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.tolerance'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.tolerance Field

The distance tolerance used when comparing coordinates\.

```csharp
protected double tolerance;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.CellSize'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.CellSize Property

Gets or sets the edge length of the working grid, in model units\.

```csharp
public double CellSize { get; set; }
```

Implements [CellSize](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.CellSize 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.CellSize')

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the grid edge length\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.Input'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.Input Property

Sets the cloud to reconstruct\.

```csharp
public abstract TPointCloud? Input { set; }
```

Implements [Input](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2.input 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2\.Input')

#### Property Value
[TPointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TPointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TPointCloud')  
The point cloud to consume\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.Output'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.Output Property

Gets the mesh produced by the most recent successful solve\.

```csharp
public TMesh? Output { get; }
```

Implements [Output](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ionetoonesolver-2.output 'DiGi\.Core\.Interfaces\.IOneToOneSolver\`2\.Output')

#### Property Value
[TMesh](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.TMesh 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudMeshSolver\<TPointCloud,TMesh\>\.TMesh')  
The reconstructed mesh, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when no solve has succeeded\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.Tolerance'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.Tolerance Property

Gets or sets the distance tolerance used when comparing coordinates\.

```csharp
public double Tolerance { get; set; }
```

Implements [Tolerance](DiGi.Geometry.PointCloud.Core.Interfaces.md#DiGi.Geometry.PointCloud.Core.Interfaces.IPointCloudMeshSolver_TPointCloud,TMesh_.Tolerance 'DiGi\.Geometry\.PointCloud\.Core\.Interfaces\.IPointCloudMeshSolver\<TPointCloud,TMesh\>\.Tolerance')

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') holding the distance tolerance\.
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudMeshSolver_TPointCloud,TMesh_.Solve()'></a>

## PointCloudMeshSolver\<TPointCloud,TMesh\>\.Solve\(\) Method

Runs the reconstruction\.

```csharp
public abstract bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when a mesh was produced; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.
### Structs

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode'></a>

## PointCloudIndexNode Struct

Represents one node of a [PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex'): an axis\-aligned box together with the contiguous range of the index permutation it owns\.

The box is stored as single-precision values rounded outward. Half the memory of double precision, and rounding outward guarantees the box never excludes a point it contains, so a rejection is always sound. Boxes are tight to the points rather than derived from the cell grid, which matters on scan data that is nearly empty along one axis: a grid-derived box would span the whole empty extent and prune almost nothing.

The box and the range live in the same struct rather than in parallel arrays because traversal reads both together.

```csharp
internal readonly struct PointCloudIndexNode
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int)'></a>

## PointCloudIndexNode\(float\[\], int, int, int, int\) Constructor

Initializes a new instance of the [PointCloudIndexNode](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndexNode') struct\.

```csharp
public PointCloudIndexNode(float[] bounds, int start, int count, int firstChild, int childCount);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int).bounds'></a>

`bounds` [System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The six bounds in the order minimum X, minimum Y, minimum Z, maximum X, maximum Y, maximum Z\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int).start'></a>

`start` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive start of the range within the index permutation\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of permutation entries owned\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int).firstChild'></a>

`firstChild` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The index of the first child node, or \-1 for a leaf\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.PointCloudIndexNode(float[],int,int,int,int).childCount'></a>

`childCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of child nodes, zero for a leaf\.
### Fields

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.ChildCount'></a>

## PointCloudIndexNode\.ChildCount Field

The number of child nodes, zero when this node is a leaf\.

```csharp
public readonly int ChildCount;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.Count'></a>

## PointCloudIndexNode\.Count Field

The number of permutation entries this node owns\.

```csharp
public readonly int Count;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.FirstChild'></a>

## PointCloudIndexNode\.FirstChild Field

The index of the first child node, or \-1 when this node is a leaf\.

```csharp
public readonly int FirstChild;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MaxX'></a>

## PointCloudIndexNode\.MaxX Field

The upper X bound, rounded outward\.

```csharp
public readonly float MaxX;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MaxY'></a>

## PointCloudIndexNode\.MaxY Field

The upper Y bound, rounded outward\.

```csharp
public readonly float MaxY;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MaxZ'></a>

## PointCloudIndexNode\.MaxZ Field

The upper Z bound, rounded outward\. Unused for a planar index\.

```csharp
public readonly float MaxZ;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MinX'></a>

## PointCloudIndexNode\.MinX Field

The lower X bound, rounded outward\.

```csharp
public readonly float MinX;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MinY'></a>

## PointCloudIndexNode\.MinY Field

The lower Y bound, rounded outward\.

```csharp
public readonly float MinY;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.MinZ'></a>

## PointCloudIndexNode\.MinZ Field

The lower Z bound, rounded outward\. Unused for a planar index\.

```csharp
public readonly float MinZ;
```

#### Field Value
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode.Start'></a>

## PointCloudIndexNode\.Start Field

The inclusive start of this node's range within the index permutation\.

```csharp
public readonly int Start;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor'></a>

## PointCloudNeighbor Struct

Represents one result of a nearest neighbour query: the index of a point within its cloud, together with its squared distance from the query position\.

The distance is held squared because that is what the search actually computes. A nearest neighbour search compares distances, never uses them, and squaring is monotonic, so every comparison along the way is exact and the square root is deferred until a caller asks for one. On a query that examines a few hundred candidates this removes a few hundred square roots from the hot path.

A plain readonly struct rather than a record struct: the target framework has no `IsExternalInit`, so init-only accessors would need a shim, and the type has no use for value equality that the two fields do not already provide by inspection. This matches [PointCloudIndexNode](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndexNode 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndexNode') and [Point](DiGi.Geometry.PointCloud.Spatial.Classes.md#DiGi.Geometry.PointCloud.Spatial.Classes.PointCloud3D.Point 'DiGi\.Geometry\.PointCloud\.Spatial\.Classes\.PointCloud3D\.Point').

```csharp
public readonly struct PointCloudNeighbor
```
### Constructors

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.PointCloudNeighbor(int,double)'></a>

## PointCloudNeighbor\(int, double\) Constructor

Initializes a new instance of the [PointCloudNeighbor](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor') struct\.

```csharp
public PointCloudNeighbor(int index, double distanceSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.PointCloudNeighbor(int,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based index of the point within its cloud\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.PointCloudNeighbor(int,double).distanceSquared'></a>

`distanceSquared` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The squared distance from the query position to the point\.
### Properties

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.Distance'></a>

## PointCloudNeighbor\.Distance Property

Gets the distance from the query position to the point\.

Computed on demand. Prefer [DistanceSquared](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.DistanceSquared 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudNeighbor\.DistanceSquared') when the value is only being compared against another distance from the same query.

```csharp
public double Distance { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') distance\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.DistanceSquared'></a>

## PointCloudNeighbor\.DistanceSquared Property

Gets the squared distance from the query position to the point\.

```csharp
public double DistanceSquared { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') squared distance, or [System\.Double\.PositiveInfinity](https://learn.microsoft.com/en-us/dotnet/api/system.double.positiveinfinity 'System\.Double\.PositiveInfinity') when the neighbour is unset\.

<a name='DiGi.Geometry.PointCloud.Core.Classes.PointCloudNeighbor.Index'></a>

## PointCloudNeighbor\.Index Property

Gets the zero\-based index of the point within its cloud\.

```csharp
public int Index { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') point index, or \-1 when the neighbour is unset\.
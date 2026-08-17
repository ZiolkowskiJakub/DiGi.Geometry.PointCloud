#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Core Namespace
### Classes

<a name='DiGi.Geometry.PointCloud.Core.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisdouble[][])'></a>

## Convert\.ToSystem\_Bytes\(this double\[\]\[\]\) Method

Encodes a coordinate\-major point payload into the binary point cloud format\.

The layout is a fixed [BinaryHeaderLength](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryHeaderLength 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.BinaryHeaderLength') byte little-endian header holding the magic identifier, version, dimension, point count and flags, followed by the coordinate arrays one after another. The payload is planar rather than interleaved so it matches the in-memory layout exactly and each axis copies with a single block move.

```csharp
public static byte[]? ToSystem_Bytes(this double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisdouble[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\. Index zero is X, index one is Y and index two, when present, is Z\.

#### Returns
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte') array holding the encoded cloud, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, empty, ragged or too large to address\.

<a name='DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisint[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection)'></a>

## Convert\.ToSystem\_Bytes\(this int\[\], PointCloudReferenceCollection\) Method

Encodes per\-point model object identifiers, and optionally the reference table they index into, as a self\-contained binary payload\.

The layout is a fixed [BinaryReferenceHeaderLength](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.BinaryReferenceHeaderLength 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.BinaryReferenceHeaderLength') byte little-endian header holding the magic identifier, version, identifier size, point count and flags, followed by the identifiers and then, when the table is embedded, its length prefixed UTF-8 JSON.

The payload is self-describing, carrying its own point count, because the reflection serializer applies members in the order they appear in the JSON document rather than in the order the type declares them. A payload that relied on the coordinates having been read first would decode correctly or not depending on how a document happened to be written.

The table is embedded only when the payload has to stand alone, as it does in a file. Inside a serialized object the table is already a member, and embedding it there would write the same fact twice and invite the two copies to disagree.

```csharp
public static byte[]? ToSystem_Bytes(this int[]? referenceIndexes, DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? pointCloudReferenceCollection);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisint[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, where \-1 marks a point that links to nothing\.

<a name='DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisint[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection).pointCloudReferenceCollection'></a>

`pointCloudReferenceCollection` [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')

The reference table to embed, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') to write the identifiers alone\.

#### Returns
[System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte') array holding the encoded payload, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null or too large to address\.

<a name='DiGi.Geometry.PointCloud.Core.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int)'></a>

## Create\.Bounds\(double\[\]\[\], int\[\], int, int, int\) Method

Calculates the tight bounds of a run of the index permutation, rounded outward to single precision\.

Rounding outward is what makes a single-precision box safe: the stored box always encloses the double-precision points, so a node can never be rejected while still holding a qualifying point.

```csharp
public static float[] Bounds(double[][] coordinates, int[] order, int startIndex, int count, int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int).order'></a>

`order` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The index permutation\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive start of the run within the permutation\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The length of the run\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Bounds(double[][],int[],int,int,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

#### Returns
[System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A six element [System\.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single 'System\.Single') array holding the minimum and then the maximum of each axis\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Cell(double,double,double,int)'></a>

## Create\.Cell\(double, double, double, int\) Method

Quantises a coordinate onto the cell grid of a spatial index\.

```csharp
public static int Cell(double value, double origin, double scale, int resolution);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.Cell(double,double,double,int).value'></a>

`value` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The coordinate value\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Cell(double,double,double,int).origin'></a>

`origin` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The lower bound of the axis\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Cell(double,double,double,int).scale'></a>

`scale` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The number of cells per unit along the axis\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Cell(double,double,double,int).resolution'></a>

`resolution` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of cells along the axis\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') cell index clamped to the grid\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int)'></a>

## Create\.Coordinates\(byte\[\], int\) Method

Decodes a coordinate\-major point payload from the binary point cloud format produced by [ToSystem\_Bytes\(this double\[\]\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisdouble[][]) 'DiGi\.Geometry\.PointCloud\.Core\.Convert\.ToSystem\_Bytes\(this double\[\]\[\]\)')\.

Every failure mode returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') rather than throwing, so a truncated, misaligned, foreign or future-versioned buffer degrades to an empty result instead of propagating an exception out of a deserialization path.

```csharp
public static double[][]? Coordinates(byte[]? bytes, int dimension=0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The expected number of coordinate axes, or a value of zero or less to accept whatever the header declares\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding one array per axis, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int,int)'></a>

## Create\.Coordinates\(byte\[\], int, int\) Method

Decodes a coordinate\-major point payload from a binary point cloud block starting at an offset inside a longer buffer\.

Unlike [Coordinates\(byte\[\], int\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.Coordinates\(byte\[\], int\)') this accepts trailing bytes, because a block located by offset is expected to be followed by something: a file holding a cloud together with its per-point model object links stores the two blocks one after the other.

Every failure mode returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') rather than throwing, matching the exact length form.

```csharp
public static double[][]? Coordinates(byte[]? bytes, int dimension, int startIndex);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int,int).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The expected number of coordinate axes, or a value of zero or less to accept whatever the header declares\.

<a name='DiGi.Geometry.PointCloud.Core.Create.Coordinates(byte[],int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The offset at which the block starts\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding one array per axis, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the block could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Core.Create.CoordinatesInRange(double[][],double[],double[])'></a>

## Create\.CoordinatesInRange\(double\[\]\[\], double\[\], double\[\]\) Method

Builds a compacted copy of a coordinate\-major payload holding only the points that fall inside an axis\-aligned box\.

Two passes rather than a growing buffer: the vectorised counting pass sizes the result exactly, then a single compaction pass fills it. That means one allocation per axis and no copying on growth, which matters because any array beyond about eighty-five kilobytes lands on the large object heap and repeated growth would fragment it.

The compaction pass is deliberately scalar. It is branchy and memory-bound, and there is no portable compress instruction on this target, so vectorising it would add complexity for no gain.

The bounds are expected to already include any tolerance.

```csharp
public static double[][]? CoordinatesInRange(double[][]? coordinates, double[]? minimums, double[]? maximums);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.CoordinatesInRange(double[][],double[],double[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Create.CoordinatesInRange(double[][],double[],double[]).minimums'></a>

`minimums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive lower bound of each axis\.

<a name='DiGi.Geometry.PointCloud.Core.Create.CoordinatesInRange(double[][],double[],double[]).maximums'></a>

`maximums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive upper bound of each axis\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the points inside the box, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is invalid or no point qualifies\.

<a name='DiGi.Geometry.PointCloud.Core.Create.DecimatedCoordinates(double[][],double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection)'></a>

## Create\.DecimatedCoordinates\(double\[\]\[\], double, PointCloudHeightSelection\) Method

Reduces a coordinate\-major payload to one representative point per cell of a regular grid laid over the first axes\.

Decimation is a precondition of reconstruction, not an optimisation of it. Incremental Delaunay triangulation allocates several objects per site and costs roughly a microsecond each, so ten million sites would need tens of seconds and several gigabytes, while a few hundred thousand finish in a fraction of a second. The grid is the mechanism that gets from one to the other.

Cells are keyed on a tuple rather than on a packed integer. The default hash of a packed key combines its halves by exclusive-or, which collapses catastrophically for the highly regular index pairs a grid produces; a tuple hash mixes them properly.

```csharp
public static double[][]? DecimatedCoordinates(double[][]? coordinates, double cellSize, DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection pointCloudHeightSelection=DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection.Lowest);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.DecimatedCoordinates(double[][],double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Create.DecimatedCoordinates(double[][],double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection).cellSize'></a>

`cellSize` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The edge length of a grid cell, in model units\. Must be greater than zero\.

<a name='DiGi.Geometry.PointCloud.Core.Create.DecimatedCoordinates(double[][],double,DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection).pointCloudHeightSelection'></a>

`pointCloudHeightSelection` [PointCloudHeightSelection](DiGi.Geometry.PointCloud.Core.Enums.md#DiGi.Geometry.PointCloud.Core.Enums.PointCloudHeightSelection 'DiGi\.Geometry\.PointCloud\.Core\.Enums\.PointCloudHeightSelection')

Which point of a cell to keep\. Ignored when there is no axis beyond the grid axes\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the representatives, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is invalid\.

<a name='DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][])'></a>

## Create\.FiniteCoordinates\(double\[\]\[\]\) Method

Builds a compacted copy of the supplied coordinate arrays holding only those points whose every coordinate is finite\.

This filtering is not cosmetic. The vectorised minimum and maximum reduction lowers to hardware instructions that return their second operand when either operand is not a number, whereas the scalar equivalent propagates it. A single such value therefore makes the vectorised and scalar bounding boxes disagree in a way that depends on how the data happens to align to vector lanes. Scan data routinely contains these values, so they are removed once, at construction, rather than guarded against on every read.

```csharp
public static double[][]? FiniteCoordinates(double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding only finite points, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged, or contains no finite point\.

<a name='DiGi.Geometry.PointCloud.Core.Create.FiniteIndexes(double[][])'></a>

## Create\.FiniteIndexes\(double\[\]\[\]\) Method

Retrieves the indexes of the points whose every coordinate is finite\.

This is the permutation carrying form of [FiniteCoordinates\(double\[\]\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.FiniteCoordinates\(double\[\]\[\]\)'). Filtering non-finite points changes the point count, so anything stored alongside the coordinates has to be compacted by the same permutation; returning the indexes rather than the coordinates is what lets one filter drive both.

The predicate matches [FiniteCoordinates\(double\[\]\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.FiniteCoordinates(double[][]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.FiniteCoordinates\(double\[\]\[\]\)') exactly, so the two produce the same points in the same order.

```csharp
public static int[]? FiniteIndexes(double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.FiniteIndexes(double[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
An ascending [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array of zero\-based point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged, or contains no finite point\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],int[])'></a>

## Create\.GatheredCoordinates\(double\[\]\[\], int\[\]\) Method

Builds a new coordinate payload holding the points named by a permutation, in the order the permutation names them\.

This is the single gather used by every filter that changes the point count, so that the coordinates and anything stored alongside them are compacted by one shared routine and cannot drift apart.

An out of range index yields [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') rather than a partly filled result, because a filter that quietly returned fewer or wrong points would be discovered as corrupted geometry much later.

```csharp
public static double[][]? GatheredCoordinates(double[][]? coordinates, int[]? indexes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],int[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],int[]).indexes'></a>

`indexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The zero\-based point indexes to gather\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the gathered points, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged, empty, or names a point that does not exist\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],System.Collections.Generic.IReadOnlyList_int_)'></a>

## Create\.GatheredCoordinates\(double\[\]\[\], IReadOnlyList\<int\>\) Method

Builds a new coordinate payload holding the points named by a permutation, in the order the permutation names them\.

```csharp
public static double[][]? GatheredCoordinates(double[][]? coordinates, System.Collections.Generic.IReadOnlyList<int>? indexes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],System.Collections.Generic.IReadOnlyList_int_).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],System.Collections.Generic.IReadOnlyList_int_).indexes'></a>

`indexes` [System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')

The zero\-based point indexes to gather\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new jagged [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the gathered points, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged, empty, or names a point that does not exist\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],int[])'></a>

## Create\.GatheredReferenceIndexes\(int\[\], int\[\]\) Method

Builds a new per\-point identifier array holding the identifiers of the points named by a permutation, in the order the permutation names them\.

This is the counterpart of [GatheredCoordinates\(double\[\]\[\], int\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],int[]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.GatheredCoordinates\(double\[\]\[\], int\[\]\)') and must be driven by the same permutation. Gathering the coordinates without gathering the identifiers is what turns a filter into silent data corruption: the result keeps its point count and its table, so nothing looks wrong, while every point after the first discarded one is attributed to the wrong model object.

The reference table itself is NOT rebuilt. Identifiers stay stable under filtering, so a table entry that no longer has any point simply goes unused, which costs one unread entry and keeps every surviving identifier valid.

```csharp
public static int[]? GatheredReferenceIndexes(int[]? referenceIndexes, int[]? indexes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],int[]).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, one per point of the source cloud\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],int[]).indexes'></a>

`indexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The zero\-based point indexes to gather\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array holding the gathered identifiers, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either input is null, empty, or names a point that does not exist\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],System.Collections.Generic.IReadOnlyList_int_)'></a>

## Create\.GatheredReferenceIndexes\(int\[\], IReadOnlyList\<int\>\) Method

Builds a new per\-point identifier array holding the identifiers of the points named by a permutation, in the order the permutation names them\.

```csharp
public static int[]? GatheredReferenceIndexes(int[]? referenceIndexes, System.Collections.Generic.IReadOnlyList<int>? indexes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],System.Collections.Generic.IReadOnlyList_int_).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, one per point of the source cloud\.

<a name='DiGi.Geometry.PointCloud.Core.Create.GatheredReferenceIndexes(int[],System.Collections.Generic.IReadOnlyList_int_).indexes'></a>

`indexes` [System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')

The zero\-based point indexes to gather\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A new [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array holding the gathered identifiers, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when either input is null, empty, or names a point that does not exist\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudIndex(double[][])'></a>

## Create\.PointCloudIndex\(double\[\]\[\]\) Method

Builds a pointerless spatial index over a coordinate\-major point payload\.

The build is a counting sort on the Z-order cell identifier, which is linear in the number of points rather than the linearithmic cost of a comparison sort. A full ordering of the points is never needed: the atomic unit of a box query is a leaf cell, and the order of points within a leaf is irrelevant, so sorting by cell identifier is sufficient and roughly halves the work.

The same sort produces the hierarchy for free. Because the leaves come out ordered by Z-order code, a parent's children are always contiguous, so the tree is folded upwards by a single scan per level rather than by any further sorting or searching.

The scatter opens one write stream per occupied cell. At the depths chosen here the cell table stays small enough to remain cache-resident; if profiling ever shows the scatter dominating on much larger clouds, the next step is to split it into a coarse pass over the high bits followed by independent per-bucket passes.

```csharp
internal static DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex? PointCloudIndex(double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudIndex(double[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

#### Returns
[PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex')  
A new [PointCloudIndex](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudIndex 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudIndex'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged, empty or not two\- or three\-dimensional\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(byte[],int)'></a>

## Create\.PointCloudReferenceCollection\(byte\[\], int\) Method

Decodes the embedded reference table from a binary payload produced by [ToSystem\_Bytes\(this int\[\], PointCloudReferenceCollection\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisint[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection) 'DiGi\.Geometry\.PointCloud\.Core\.Convert\.ToSystem\_Bytes\(this int\[\], DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection\)')\.

Returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the payload carries identifiers alone, which is the normal case for a payload travelling inside a serialized object that already holds the table as a member. Every failure mode returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') rather than throwing, matching [ReferenceIndexes\(byte\[\], int\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.ReferenceIndexes(byte[],int) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.ReferenceIndexes\(byte\[\], int\)').

```csharp
public static DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? PointCloudReferenceCollection(byte[]? bytes, int startIndex=0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(byte[],int).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(byte[],int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The offset at which the block starts, which is non\-zero when the block follows a coordinate block in the same buffer\.

#### Returns
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')  
The embedded [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when none is present or the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_)'></a>

## Create\.PointCloudReferenceCollection\(IEnumerable\<ISerializableReference\>\) Method

Builds a reference table from a sequence of references, discarding nulls and duplicates\.

The first occurrence of a reference fixes its identifier, and later duplicates are dropped. Deduplication happens here rather than in the constructor because a duplicate is a defect to clean up, not a shape to validate, and the constructor sits on the hot path of every clone and copy.

```csharp
public static DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? PointCloudReferenceCollection(System.Collections.Generic.IEnumerable<DiGi.Core.Interfaces.ISerializableReference>? references);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The references to store\. This value can be null\.

#### Returns
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')  
A new [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when no valid reference was supplied\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_,int[])'></a>

## Create\.PointCloudReferenceCollection\(IEnumerable\<ISerializableReference\>, int\[\]\) Method

Builds a reference table and the matching per\-point identifier array from a sequence holding one reference per point\.

This is the shape a segmentation pass produces: it walks the points once, assigning a new identifier the first time it meets a model object and reusing it afterwards. Building both halves here is what keeps them consistent, because the table and the identifiers are the same fact recorded twice and nothing else in the library can check that they agree.

A null entry in the sequence marks a point that links to nothing and is recorded as -1.

```csharp
public static DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection? PointCloudReferenceCollection(System.Collections.Generic.IEnumerable<DiGi.Core.Interfaces.ISerializableReference>? references, out int[]? referenceIndexes);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_,int[]).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Core\.Interfaces\.ISerializableReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablereference 'DiGi\.Core\.Interfaces\.ISerializableReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The references, one per point and in point order\. This value can be null\.

<a name='DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(System.Collections.Generic.IEnumerable_DiGi.Core.Interfaces.ISerializableReference_,int[]).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

When this method returns, contains the per\-point identifiers, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the sequence was null\.

#### Returns
[PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection')  
A new [PointCloudReferenceCollection](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when every point links to nothing\.

<a name='DiGi.Geometry.PointCloud.Core.Create.ReferenceIndexes(byte[],int)'></a>

## Create\.ReferenceIndexes\(byte\[\], int\) Method

Decodes the per\-point model object identifiers from a binary payload produced by [ToSystem\_Bytes\(this int\[\], PointCloudReferenceCollection\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Convert.ToSystem_Bytes(thisint[],DiGi.Geometry.PointCloud.Core.Classes.PointCloudReferenceCollection) 'DiGi\.Geometry\.PointCloud\.Core\.Convert\.ToSystem\_Bytes\(this int\[\], DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloudReferenceCollection\)')\.

Every failure mode returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') rather than throwing, so a truncated, misaligned, foreign or future-versioned buffer degrades to an empty result instead of propagating an exception out of a deserialization path.

The embedded reference table, when present, is ignored here. Read it with [PointCloudReferenceCollection\(byte\[\], int\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.PointCloudReferenceCollection(byte[],int) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.PointCloudReferenceCollection\(byte\[\], int\)').

```csharp
public static int[]? ReferenceIndexes(byte[]? bytes, int startIndex=0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Create.ReferenceIndexes(byte[],int).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

<a name='DiGi.Geometry.PointCloud.Core.Create.ReferenceIndexes(byte[],int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The offset at which the block starts, which is non\-zero when the block follows a coordinate block in the same buffer\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array holding one identifier per point, where \-1 marks a point that links to nothing, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the buffer could not be decoded\.

<a name='DiGi.Geometry.PointCloud.Core.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double)'></a>

## Modify\.InsertNeighbor\(Span\<int\>, Span\<double\>, int, double, int, double\) Method

Offers a candidate to an insertion\-sorted nearest neighbour set, evicting the furthest entry when the set is already full\.

An insertion sort rather than a heap. A nearest neighbour set holds a handful of entries, so the whole structure stays in registers, the shifting loop is predictable, and a heap would add indirection and a larger constant factor for no asymptotic gain at this size.

Ordering is by squared distance and then by point index, so equal distances resolve towards the lower index. This is what lets the indexed descent and the exhaustive scan return the same answer: one visits points in Z-order and the other in input order, and without an explicit tie-break a cloud containing duplicated points would answer differently depending on whether it was large enough to be indexed.

Both paths route their candidates through here rather than repeating the comparison, because the two orderings only agree as long as they are literally the same code.

```csharp
public static bool InsertNeighbor(System.Span<int> indexes, System.Span<double> distancesSquared, int index, double distanceSquared, ref int filled, ref double worst);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

The candidate point indexes, ordered nearest first\. Its length is the number of neighbours being collected\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

The matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).indexes 'DiGi\.Geometry\.PointCloud\.Core\.Modify\.InsertNeighbor\(System\.Span\<int\>, System\.Span\<double\>, int, double, int, double\)\.indexes')\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The zero\-based index of the point being offered\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).distanceSquared'></a>

`distanceSquared` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The squared distance from the query position to the point being offered\. A value of [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') is always rejected\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).filled'></a>

`filled` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of entries currently held, updated when the set grows\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double).worst'></a>

`worst` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The current rejection radius, updated whenever the set changes\. Holds [System\.Double\.PositiveInfinity](https://learn.microsoft.com/en-us/dotnet/api/system.double.positiveinfinity 'System\.Double\.PositiveInfinity') until the set is full\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the candidate was taken into the set; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[])'></a>

## Modify\.OffsetCoordinates\(this double\[\]\[\], double\[\]\) Method

Adds a per\-axis offset to every coordinate in the supplied arrays, in place\.

Large inputs are split across partitions. Because each partition writes a disjoint range of the same arrays, no synchronization is needed at all. The ranged overload validates before it writes, and the partitions are checked up front, so a failure cannot leave the arrays half-modified.

```csharp
public static bool OffsetCoordinates(this double[][]? coordinates, double[]? offsets);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[]).offsets'></a>

`offsets` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The offset to add to each axis\. Must hold one value per axis\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the offset was applied; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[],int,int)'></a>

## Modify\.OffsetCoordinates\(this double\[\]\[\], double\[\], int, int\) Method

Adds a per\-axis offset to a contiguous range of coordinates, in place, using a vectorised loop with a scalar tail\.

Every axis is validated before any value is written, so a ragged input leaves the arrays untouched rather than partially modified.

```csharp
public static bool OffsetCoordinates(this double[][]? coordinates, double[]? offsets, int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[],int,int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[],int,int).offsets'></a>

`offsets` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The offset to add to each axis\. Must hold one value per axis\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[],int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.OffsetCoordinates(thisdouble[][],double[],int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the offset was applied; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[])'></a>

## Modify\.TransformCoordinates\(this double\[\]\[\], double\[\]\) Method

Applies a flattened affine transform to every coordinate in the supplied arrays, in place\.

Large inputs are split across partitions. Because each partition writes a disjoint range of the same arrays, no synchronization is needed at all.

```csharp
public static bool TransformCoordinates(this double[][]? coordinates, double[]? affine);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[]).affine'></a>

`affine` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The row\-major affine matrix\. Six values forming two rows of three for a planar cloud, or twelve values forming three rows of four for a spatial one\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the transform was applied; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[],int,int)'></a>

## Modify\.TransformCoordinates\(this double\[\]\[\], double\[\], int, int\) Method

Applies a flattened affine transform to a contiguous range of coordinates, in place, using a vectorised loop with a scalar tail\.

Taking the transform pre-flattened matters: reading the matrix through a transform object costs an indexer call per element per point, and a transform group would otherwise be walked once per point. Flattening once and streaming the result turns the whole pass into arithmetic.

All axes of a lane are read before any is written, so the update is free of aliasing even though it is performed in place.

```csharp
public static bool TransformCoordinates(this double[][]? coordinates, double[]? affine, int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[],int,int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[],int,int).affine'></a>

`affine` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The row\-major affine matrix\. Six values forming two rows of three for a planar cloud, or twelve values forming three rows of four for a spatial one\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[],int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Modify.TransformCoordinates(thisdouble[][],double[],int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the transform was applied; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Geometry.PointCloud.Core.Query.BinaryLength(byte[],int)'></a>

## Query\.BinaryLength\(byte\[\], int\) Method

Retrieves the total length in bytes of the binary point cloud block starting at an offset, header included\.

The length follows entirely from the header, which is what allows a block to be located inside a longer buffer: a file holding a cloud and its per-point model object links stores the two blocks one after the other, and this is how the reader finds where the first one ends.

Returns -1 rather than throwing for every malformed input, matching the decoders.

```csharp
public static int BinaryLength(byte[]? bytes, int startIndex=0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.BinaryLength(byte[],int).bytes'></a>

`bytes` [System\.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System\.Byte')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The encoded buffer\.

<a name='DiGi.Geometry.PointCloud.Core.Query.BinaryLength(byte[],int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The offset at which the block starts\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The total length of the block in bytes, or \-1 when no valid block starts at the offset\.

<a name='DiGi.Geometry.PointCloud.Core.Query.CoordinateExtremes(thisdouble[][])'></a>

## Query\.CoordinateExtremes\(this double\[\]\[\]\) Method

Calculates the smallest and largest value on every axis of a coordinate\-major point payload\.

Each partition accumulates into method-local variables and writes its result slot exactly once at the end, so there is no lock, no concurrent collection and no measurable false sharing. Padding the result slots would be ceremony: a single store per partition for the whole pass cannot contend.

The parallel and serial paths produce bit-identical results, because minimum and maximum are exact and associative. Do not assume the same of a sum or a mean.

Streaming passes use only a fraction of the available processors: memory bandwidth saturates well before every core is busy, and the surplus threads add scheduling cost and no throughput.

```csharp
public static double[]? CoordinateExtremes(this double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.CoordinateExtremes(thisdouble[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
A [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double') array holding the minimum and maximum of each axis in turn, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null, ragged or empty\.

<a name='DiGi.Geometry.PointCloud.Core.Query.DelaunayIndexes(double[],double[],double)'></a>

## Query\.DelaunayIndexes\(double\[\], double\[\], double\) Method

Triangulates a set of planar sites and returns the triangles as index triples into the supplied arrays\.

The triangulation itself is delegated to NetTopologySuite. What matters here is the mapping back: the vertices are looked up by their exact coordinate values rather than trusting any attribute to survive the internal quad-edge representation, so the result always indexes the caller's own points and any third axis is recovered from them rather than from the triangulator.

A Delaunay triangulation covers the convex hull of its sites, so a cloud with a concave outline or interior holes comes back with a skirt of long thin triangles bridging the gaps. Supplying a maximum edge length removes them. For real scan data that filtering is a requirement, not a refinement.

```csharp
public static System.Collections.Generic.List<int[]>? DelaunayIndexes(double[]? x, double[]? y, double maximumEdgeLength=0.0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.DelaunayIndexes(double[],double[],double).x'></a>

`x` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The X coordinates of the sites\.

<a name='DiGi.Geometry.PointCloud.Core.Query.DelaunayIndexes(double[],double[],double).y'></a>

`y` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The Y coordinates of the sites\.

<a name='DiGi.Geometry.PointCloud.Core.Query.DelaunayIndexes(double[],double[],double).maximumEdgeLength'></a>

`maximumEdgeLength` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The longest edge a triangle may have, in model units\. Values of zero or less keep every triangle\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A [System\.Collections\.Generic\.List&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1') of three element index arrays, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is invalid or nothing could be triangulated\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Dimension(double[][],int)'></a>

## Query\.Dimension\(double\[\]\[\], int\) Method

Retrieves the number of coordinate axes of a payload, together with its point count, when the payload is rectangular\.

Reports zero for a ragged payload rather than the length of the outer array, so that a single check answers both "how many axes" and "is this usable at all".

```csharp
public static int Dimension(double[][]? coordinates, out int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.Dimension(double[][],int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Dimension(double[][],int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns, contains the number of points, or zero when the payload is not rectangular\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of axes, or zero when the payload is null, empty or ragged\.

<a name='DiGi.Geometry.PointCloud.Core.Query.IndexDepth(int,int)'></a>

## Query\.IndexDepth\(int, int\) Method

Calculates the subdivision depth to use for a spatial index over the given number of points\.

The depth targets a leaf occupancy of roughly [IndexLeafPointCount](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexLeafPointCount 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.IndexLeafPointCount') points. Shallower leaves force per-point testing over large groups; deeper ones inflate the cell table and the tree without reducing the work that matters.

```csharp
public static int IndexDepth(int count, int dimension);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.IndexDepth(int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points to be indexed\.

<a name='DiGi.Geometry.PointCloud.Core.Query.IndexDepth(int,int).dimension'></a>

`dimension` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of coordinate axes\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') depth, clamped to the range supported for the dimension\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[])'></a>

## Query\.InRangeCount\(this double\[\]\[\], double\[\], double\[\]\) Method

Counts the points of a coordinate\-major payload that fall inside an axis\-aligned box\.

The bounds are expected to already include any tolerance. Folding the tolerance in once, before the scan, keeps it out of the inner loop and makes the result agree exactly with the per-point bounding box test, which compares against bounds widened by the same amount.

```csharp
public static int InRangeCount(this double[][]? coordinates, double[]? minimums, double[]? maximums);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[]).minimums'></a>

`minimums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive lower bound of each axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[]).maximums'></a>

`maximums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive upper bound of each axis\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of points inside the box, or \-1 when the input is null, ragged or mismatched\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int)'></a>

## Query\.InRangeCount\(this double\[\]\[\], double\[\], double\[\], int, int\) Method

Counts the points of a contiguous range of a coordinate\-major payload that fall inside an axis\-aligned box\.

The counting pass is fully vectorised and needs no per-lane extraction. A lane-wise comparison yields a mask whose true lanes hold all bits set, which as a signed integer is minus one, so subtracting the mask from a running vector increments exactly the lanes that passed. Only one horizontal reduction is needed, at the very end.

This matters because there is no portable way to extract a comparison mask on this target: the move-mask instruction lives behind the hardware intrinsics surface, which is not available here.

```csharp
public static int InRangeCount(this double[][]? coordinates, double[]? minimums, double[]? maximums, int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int).minimums'></a>

`minimums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive lower bound of each axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int).maximums'></a>

`maximums` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The inclusive upper bound of each axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Query.InRangeCount(thisdouble[][],double[],double[],int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of points inside the box, or \-1 when the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],double,double)'></a>

## Query\.MinMax\(this double\[\], double, double\) Method

Finds the smallest and largest value in a coordinate array using a vectorised reduction\.

See the ranged overload for the non-finite value caveat.

```csharp
public static bool MinMax(this double[]? values, out double min, out double max);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],double,double).values'></a>

`values` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate array to scan\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],double,double).min'></a>

`min` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the smallest value, or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when the array is null or empty\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],double,double).max'></a>

`max` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the largest value, or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when the array is null or empty\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the array was scanned; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double)'></a>

## Query\.MinMax\(this double\[\], int, int, double, double\) Method

Finds the smallest and largest value in a contiguous range of a coordinate array using a vectorised reduction\.

The vectorised path processes [System\.Numerics\.Vector&lt;&gt;\.Count](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.vector-1.count 'System\.Numerics\.Vector\`1\.Count') values per iteration and finishes with a scalar tail, so it is correct for any range length. The lane width is read at runtime and never assumed.

IMPORTANT: the values are assumed to be finite. [System\.Numerics\.Vector\.Min&lt;&gt;\.Numerics\.Vector\{&lt;&gt;\.Numerics\.Vector\{&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.vector.min--1#system-numerics-vector-min--1(system-numerics-vector{--0}-system-numerics-vector{--0}) 'System\.Numerics\.Vector\.Min\`\`1\(System\.Numerics\.Vector\{\`\`0\},System\.Numerics\.Vector\{\`\`0\}\)') and [System\.Numerics\.Vector\.Max&lt;&gt;\.Numerics\.Vector\{&lt;&gt;\.Numerics\.Vector\{&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.vector.max--1#system-numerics-vector-max--1(system-numerics-vector{--0}-system-numerics-vector{--0}) 'System\.Numerics\.Vector\.Max\`\`1\(System\.Numerics\.Vector\{\`\`0\},System\.Numerics\.Vector\{\`\`0\}\)') lower to hardware instructions that return their second operand when either operand is not a number, whereas [System\.Math\.Min\(System\.Double,System\.Double\)](https://learn.microsoft.com/en-us/dotnet/api/system.math.min#system-math-min(system-double-system-double) 'System\.Math\.Min\(System\.Double,System\.Double\)') propagates it. A single such value therefore makes the vectorised and scalar results disagree in a way that depends on lane alignment. Point cloud factories filter non-finite coordinates before construction for exactly this reason.

```csharp
public static bool MinMax(this double[]? values, int startIndex, int count, out double min, out double max);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double).values'></a>

`values` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate array to scan\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of values in the range\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double).min'></a>

`min` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the smallest value in the range, or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when the range is invalid\.

<a name='DiGi.Geometry.PointCloud.Core.Query.MinMax(thisdouble[],int,int,double,double).max'></a>

`max` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the largest value in the range, or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when the range is invalid\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when the range was scanned; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int)'></a>

## Query\.Morton\(int, int, int\) Method

Interleaves the low bits of two axis indexes into a single Z\-order cell identifier\.

Z-order is what turns a flat grid of cells into a hierarchy for free: dropping the low bits of a cell identifier yields the identifier of its parent cell, so a table of leaves sorted by this value can be folded upwards into a tree without any further sorting or searching.

```csharp
public static int Morton(int x, int y, int bits);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int).x'></a>

`x` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The quantised X index\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int).y'></a>

`y` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The quantised Y index\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int).bits'></a>

`bits` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of bits taken from each axis\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') holding the interleaved cell identifier\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int,int)'></a>

## Query\.Morton\(int, int, int, int\) Method

Interleaves the low bits of three axis indexes into a single Z\-order cell identifier\.

Z-order is what turns a flat grid of cells into a hierarchy for free: dropping the low three bits of a cell identifier yields the identifier of its parent cell, so a table of leaves sorted by this value can be folded upwards into a tree without any further sorting or searching.

```csharp
public static int Morton(int x, int y, int z, int bits);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int,int).x'></a>

`x` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The quantised X index\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int,int).y'></a>

`y` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The quantised Y index\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int,int).z'></a>

`z` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The quantised Z index\.

<a name='DiGi.Geometry.PointCloud.Core.Query.Morton(int,int,int,int).bits'></a>

`bits` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of bits taken from each axis\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') holding the interleaved cell identifier\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_)'></a>

## Query\.NearestIndexes\(this PointCloud, ReadOnlySpan\<double\>, Span\<int\>, Span\<double\>\) Method

Retrieves the indexes of the points of a cloud closest to a query position, nearest first\.

Chooses between the two searches the same way the box queries do. Above [IndexThreshold](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexThreshold 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.IndexThreshold') the cloud carries a spatial index and the descent visits a few dozen nodes regardless of how large the cloud is; below it there is no index and an exhaustive vectorised sweep is cheaper than building one.

Both searches return the same answer for the same input, including on duplicated points, because both order their candidates through [InsertNeighbor\(Span&lt;int&gt;, Span&lt;double&gt;, int, double, int, double\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Modify.InsertNeighbor(System.Span_int_,System.Span_double_,int,double,int,double) 'DiGi\.Geometry\.PointCloud\.Core\.Modify\.InsertNeighbor\(System\.Span\<int\>, System\.Span\<double\>, int, double, int, double\)').

Nothing is allocated on either path. The caller owns the result buffers, and the search runs on scalar coordinates without materializing a point object.

```csharp
public static int NearestIndexes(this DiGi.Geometry.PointCloud.Core.Classes.PointCloud? pointCloud, System.ReadOnlySpan<double> query, System.Span<int> indexes, System.Span<double> distancesSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).pointCloud'></a>

`pointCloud` [PointCloud](DiGi.Geometry.PointCloud.Core.Classes.md#DiGi.Geometry.PointCloud.Core.Classes.PointCloud 'DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud')

The cloud to search\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).query'></a>

`query` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The query position, holding one value per axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisDiGi.Geometry.PointCloud.Core.Classes.PointCloud,System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).indexes 'DiGi\.Geometry\.PointCloud\.Core\.Query\.NearestIndexes\(this DiGi\.Geometry\.PointCloud\.Core\.Classes\.PointCloud, System\.ReadOnlySpan\<double\>, System\.Span\<int\>, System\.Span\<double\>\)\.indexes')\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, which is smaller than the requested count when the cloud holds fewer points, or \-1 when the cloud is empty or the request is mismatched\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_)'></a>

## Query\.NearestIndexes\(this double\[\]\[\], ReadOnlySpan\<double\>, Span\<int\>, Span\<double\>\) Method

Retrieves the indexes of the points of a coordinate\-major payload closest to a query position, nearest first\.

This is the path taken when no spatial index exists, which is every cloud below [IndexThreshold](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.IndexThreshold 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.IndexThreshold'). An exhaustive vectorised sweep over that many points finishes in tens of microseconds, which is less than building an index would cost, so there is nothing to gain from a hierarchy at that size.

Parallelised only above [ParallelThreshold](DiGi.Geometry.PointCloud.Core.Constants.md#DiGi.Geometry.PointCloud.Core.Constants.PointCloud.ParallelThreshold 'DiGi\.Geometry\.PointCloud\.Core\.Constants\.PointCloud\.ParallelThreshold'), which in practice means only when a cloud large enough to be indexed failed to produce an index. Each partition collects its own candidate set and they are merged afterwards, which is exact rather than approximate: a global winner is a winner in whichever partition holds it.

```csharp
public static int NearestIndexes(this double[][]? coordinates, System.ReadOnlySpan<double> query, System.Span<int> indexes, System.Span<double> distancesSquared);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).query'></a>

`query` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The query position, holding one value per axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_).indexes 'DiGi\.Geometry\.PointCloud\.Core\.Query\.NearestIndexes\(this double\[\]\[\], System\.ReadOnlySpan\<double\>, System\.Span\<int\>, System\.Span\<double\>\)\.indexes')\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, which is smaller than the requested count when the payload holds fewer points, or \-1 when the input is null, ragged or mismatched\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int)'></a>

## Query\.NearestIndexes\(this double\[\]\[\], ReadOnlySpan\<double\>, Span\<int\>, Span\<double\>, int, int\) Method

Retrieves the indexes of the points of a contiguous range of a coordinate\-major payload closest to a query position, nearest first\.

The sweep is vectorised, and the shape of it is what makes it cheap. A lane-wise squared distance is compared against a broadcast of the current rejection radius, and when no lane beats it the whole block is skipped with a single test. Only a block that actually contains a candidate is unpacked lane by lane, and only then is the broadcast rebuilt.

That asymmetry is the point. The radius collapses within the first few hundred points and almost never moves again, so the steady state is a handful of arithmetic operations per block with a perfectly predicted branch, and no per-lane extraction at all. Extraction would otherwise dominate, because the move-mask instruction that makes it cheap lives behind the hardware intrinsics surface, which is not available on this target.

Squared distances are compared throughout. Squaring is monotonic, so every comparison is exact, and the square root is never needed.

```csharp
public static int NearestIndexes(this double[][]? coordinates, System.ReadOnlySpan<double> query, System.Span<int> indexes, System.Span<double> distancesSquared, int startIndex, int count);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis, all of equal length\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).query'></a>

`query` [System\.ReadOnlySpan&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1 'System\.ReadOnlySpan\`1')

The query position, holding one value per axis\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).indexes'></a>

`indexes` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the point indexes, nearest first\. Its length is the number of neighbours requested\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).distancesSquared'></a>

`distancesSquared` [System\.Span&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.span-1 'System\.Span\`1')

A buffer receiving the matching squared distances, which must be at least as long as [indexes](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).indexes 'DiGi\.Geometry\.PointCloud\.Core\.Query\.NearestIndexes\(this double\[\]\[\], System\.ReadOnlySpan\<double\>, System\.Span\<int\>, System\.Span\<double\>, int, int\)\.indexes')\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).startIndex'></a>

`startIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The inclusive index at which the range starts\.

<a name='DiGi.Geometry.PointCloud.Core.Query.NearestIndexes(thisdouble[][],System.ReadOnlySpan_double_,System.Span_int_,System.Span_double_,int,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of points in the range\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of neighbours written, or \-1 when the input is mismatched or the range is out of bounds\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double)'></a>

## Query\.PartitionCount\(int, int, double\) Method

Calculates how many partitions a workload of the given size should be split into\.

The result never exceeds the processor budget implied by [processorFraction](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double).processorFraction 'DiGi\.Geometry\.PointCloud\.Core\.Query\.PartitionCount\(int, int, double\)\.processorFraction'), and never creates a partition smaller than [minimumPartitionSize](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double).minimumPartitionSize 'DiGi\.Geometry\.PointCloud\.Core\.Query\.PartitionCount\(int, int, double\)\.minimumPartitionSize'), so small workloads collapse to a single serial partition instead of paying dispatch cost.

```csharp
public static int PartitionCount(int count, int minimumPartitionSize, double processorFraction=1.0);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The total number of elements to process\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double).minimumPartitionSize'></a>

`minimumPartitionSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The smallest worthwhile partition size\. Values of zero or less are treated as one\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PartitionCount(int,int,double).processorFraction'></a>

`processorFraction` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The fraction of available processors to use\. Use a value below one for memory\-bound streaming passes, which saturate well before every core is busy\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
An [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') partition count of at least one\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PointCount(thisdouble[][])'></a>

## Query\.PointCount\(this double\[\]\[\]\) Method

Calculates the number of points held by a coordinate\-major payload, verifying that it is rectangular\.

A payload whose axis arrays differ in length has no meaningful point count, so the ragged case is reported rather than silently answered from the first axis.

```csharp
public static int PointCount(this double[][]? coordinates);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.PointCount(thisdouble[][]).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The coordinate arrays, one per axis\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of points, or \-1 when the payload is null, ragged or holds an unsupported number of axes\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PointIndexes(int[],int)'></a>

## Query\.PointIndexes\(int\[\], int\) Method

Retrieves the indexes of the points carrying a given model object identifier\.

Returned in ascending order, so the result can drive [GatheredCoordinates\(double\[\]\[\], int\[\]\)](DiGi.Geometry.PointCloud.Core.md#DiGi.Geometry.PointCloud.Core.Create.GatheredCoordinates(double[][],int[]) 'DiGi\.Geometry\.PointCloud\.Core\.Create\.GatheredCoordinates\(double\[\]\[\], int\[\]\)') directly and the extracted sub-cloud keeps the point order of its source.

```csharp
public static int[]? PointIndexes(int[]? referenceIndexes, int referenceIndex);
```
#### Parameters

<a name='DiGi.Geometry.PointCloud.Core.Query.PointIndexes(int[],int).referenceIndexes'></a>

`referenceIndexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The per\-point identifiers, one per point\.

<a name='DiGi.Geometry.PointCloud.Core.Query.PointIndexes(int[],int).referenceIndex'></a>

`referenceIndex` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier to select, where a negative value selects the points that link to nothing\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')  
An ascending [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32') array of zero\-based point indexes, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when the input is null or no point carries the identifier\.
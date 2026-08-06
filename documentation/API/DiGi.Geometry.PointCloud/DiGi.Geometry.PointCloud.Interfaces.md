#### [DiGi\.Geometry\.PointCloud](DiGi.Geometry.PointCloud.Overview.md 'DiGi\.Geometry\.PointCloud\.Overview')

## DiGi\.Geometry\.PointCloud\.Interfaces Namespace
### Interfaces

<a name='DiGi.Geometry.PointCloud.Interfaces.IGeometryPointCloudObject'></a>

## IGeometryPointCloudObject Interface

Defines the basic properties and behaviors for a Geometry PointCloud object\.

```csharp
public interface IGeometryPointCloudObject : DiGi.Core.Interfaces.IObject
```

Derived  
↳ [IGeometryPointCloudSerializableObject](DiGi.Geometry.PointCloud.Interfaces.md#DiGi.Geometry.PointCloud.Interfaces.IGeometryPointCloudSerializableObject 'DiGi\.Geometry\.PointCloud\.Interfaces\.IGeometryPointCloudSerializableObject')

Implements [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.Geometry.PointCloud.Interfaces.IGeometryPointCloudSerializableObject'></a>

## IGeometryPointCloudSerializableObject Interface

Defines a contract for objects that are both PointCloud\-compatible and serializable\.

```csharp
public interface IGeometryPointCloudSerializableObject : DiGi.Geometry.PointCloud.Interfaces.IGeometryPointCloudObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Implements [IGeometryPointCloudObject](DiGi.Geometry.PointCloud.Interfaces.md#DiGi.Geometry.PointCloud.Interfaces.IGeometryPointCloudObject 'DiGi\.Geometry\.PointCloud\.Interfaces\.IGeometryPointCloudObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
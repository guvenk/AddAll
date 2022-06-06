
### What is AddAll?
Automates registration of all services in your solution.

### How does it work internally?

1. It scans all the assemblies in your solution using the string **prefix**. By default its your entry assembly name until the first dot(.) comma(,) or hyphen(-).
2. Finds all defined interfaces and non-abstract classes which implements those interfaces.
3. Registers them all to the built-in dependency injection container.

> **_NOTE:_**  It only registers the interfaces with their implementations if they are both in the same assembly.



### Example Usage:
```csharp

services.AddAllAsTransient();
services.AddAllAsTransient(options =>
{
    options.PrefixAssemblyName = "Prefix";
    options.IncludedTypes = new List<Type> { typeof(IMyService) };
    options.ExcludedTypes = new List<Type> { typeof(IMyOtherService) };
    options.IncludedAssemblies = new List<Assembly> { someAssembly };
    options.ExcludedAssemblies = new List<Assembly> { otherAssembly };
});

```

---
**Contact**

guven89@hotmail.com - Guven Sezgin Kurt

---
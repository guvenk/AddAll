
### What is AddAll?
Automates registration of all services in your solution.

### How does it work internally?

1. It scans all the assemblies in your solution using the string **prefix**. By default its your entry assembly name until the first dot(.) comma(,) or hyphen(-).
2. Finds all defined interfaces and non-abstract classes which implements those interfaces.
3. Registers them all to the built-in dependency injection container.

> **_NOTE:_**  It only registers the interfaces with their implementations if they are both in the same assembly.



### Sample Usage:
```csharp

services.AddAllAsTransient();
services.AddAllAsTransient(options =>
{
    options.PrefixAssemblyName = "Prefix";
    options.ExcludedTypes = new List<Type> { typeof(IMyService) };
});

services.TryAddAllAsTransient();
services.TryAddAllAsTransient(options =>
{
    options.PrefixAssemblyName = "Prefix";
    options.ExcludedTypes = new List<Type> { typeof(IMyService) };
});

```

### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [AddAll](https://www.nuget.org/packages/AddAll/) from the package manager console

```
PM> Install-Package AddAll
```


---
**Contact**

guven89@hotmail.com - Guven Sezgin Kurt

---
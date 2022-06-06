
### What is AddAll?
Automates registration of all services in your solution.

### How does it work internally?
It scans all the assemblies in your solution and registers all the services found.
By default it will take the entry assembly and take the name until the first dot(.) comma(,) or hyphen(-) 
and use it as a prefix to find all assemblies in your solution.

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

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [AddAll](https://www.nuget.org/packages/AddAll/) from the package manager console:

```
PM> Install-Package AddAll
```

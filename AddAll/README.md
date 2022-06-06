
### What is AddAll?
Automates registration of all services in your solution.


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

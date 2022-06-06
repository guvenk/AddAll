using System;
using System.Collections.Generic;
using System.Reflection;

namespace AddAll
{
    public class AddAllOptions
    {
        public string PrefixAssemblyName { get; set; } = string.Empty;
        public IList<Type> IncludedTypes { get; set; } = new List<Type>();
        public IList<Type> ExcludedTypes { get; set; } = new List<Type>();
        public IList<Assembly> ExcludedAssemblies { get; set; } = new List<Assembly>();
        public IList<Assembly> IncludedAssemblies { get; set; } = new List<Assembly>();
    }
}

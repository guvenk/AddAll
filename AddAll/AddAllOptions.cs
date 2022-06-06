using System;
using System.Collections.Generic;

namespace AddAll
{
    public class AddAllOptions
    {
        public string PrefixAssemblyName { get; set; } = string.Empty;
        public IList<Type> ExcludedTypes { get; set; } = new List<Type>();
    }
}

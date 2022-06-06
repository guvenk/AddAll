using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AddAll
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAllAsTransient(
            this IServiceCollection services,
            Action<AddAllOptions> setupAction = null)
        {
            var config = new AddAllOptions();
            setupAction?.Invoke(config);

            var assemblies = FindAssemblies(config.PrefixAssemblyName);

            var typesToRegister = GetAllTypes(assemblies);

            typesToRegister = typesToRegister.Where(x => !config.ExcludedTypes.Contains(x.Key));

            foreach (var typePair in typesToRegister)
            {
                services.AddTransient(typePair.Key, typePair.Value);
            }

            return services;
        }

        public static IServiceCollection TryAddAllAsTransient(
           this IServiceCollection services,
           Action<AddAllOptions> setupAction = null)
        {
            var config = new AddAllOptions();
            setupAction?.Invoke(config);

            var assemblies = FindAssemblies(config.PrefixAssemblyName);

            var typesToRegister = GetAllTypes(assemblies);

            typesToRegister = typesToRegister.Where(x => !config.ExcludedTypes.Contains(x.Key));



            foreach (var typePair in typesToRegister)
            {
                services.TryAddTransient(typePair.Key, typePair.Value);
            }

            return services;
        }

        private static IEnumerable<Assembly> FindAssemblies(string prefix = "")
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (string.IsNullOrEmpty(prefix))
            {
                var name = entryAssembly.FullName.Split(new[] { '.', '-', ' ' }).First();
                prefix = name;
            }

            var assemblies = entryAssembly
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith(prefix))
                .Select(x => Assembly.Load(x))
                .ToList();

            assemblies.Add(entryAssembly);
            return assemblies;
        }

        private static IEnumerable<KeyValuePair<Type, Type>> GetAllTypes(
            IEnumerable<Assembly> assemblies)
        {
            var types = new List<KeyValuePair<Type, Type>>();
            foreach (var assembly in assemblies)
            {
                var interfaceTypes = assembly
                    .GetTypes()
                    .Where(x => x.IsInterface)
                    .ToList();

                foreach (var serviceType in interfaceTypes)
                {
                    var implementationTypes = GetImplementationsOfInterface(assembly, serviceType)
                        .ToList();

                    foreach (var implementationType in implementationTypes)
                    {
                        yield return new KeyValuePair<Type, Type>(serviceType, implementationType);
                    }
                }
            }
        }

        private static IEnumerable<Type> GetImplementationsOfInterface(Assembly assembly, Type serviceType)
        {
            return assembly
                .GetTypes()
                .Where(type => serviceType.IsAssignableFrom(type)
                && !type.IsInterface
                && !type.IsAbstract);
        }
    }

}

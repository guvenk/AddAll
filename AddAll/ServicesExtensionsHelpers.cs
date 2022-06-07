﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AddAll
{
    internal static class ServicesExtensionsHelpers
    {
        private static readonly char[] _separators = new[] { '.', ',', '-', ' ' };

        private static IList<KeyValuePair<Type, Type>> GetAllServices(
            AddAllOptions options,
            IList<Assembly> assemblies)
        {
            var services = new List<KeyValuePair<Type, Type>>();
            foreach (var assembly in assemblies)
            {
                var interfaceTypes = assembly
                    .GetTypes()
                    .Where(x => x.IsInterface)
                    .Where(x => !options.ExcludedTypes.Contains(x))
                    .ToList();

                if (options.IncludedTypes.Count > 0)
                {
                    interfaceTypes = interfaceTypes
                        .Where(x => options.IncludedTypes.Contains(x))
                        .ToList();
                }

                foreach (var serviceType in interfaceTypes)
                {
                    var implementationTypes = GetImplementationsOfInterface(assembly, serviceType);

                    foreach (var implementationType in implementationTypes)
                    {
                        services.Add(new KeyValuePair<Type, Type>(serviceType, implementationType));
                    }
                }
            }

            return services;
        }

        private static IList<Assembly> GetAssemblies(AddAllOptions options)
        {
            var prefix = options.PrefixAssemblyName;
            var entryAssembly = Assembly.GetEntryAssembly();
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = entryAssembly.FullName.Split(_separators).First();
            }

            var assemblies = entryAssembly
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith(prefix))
                .Select(x => Assembly.Load(x))
                .Where(x => !options.ExcludedAssemblies.Contains(x))
                .ToList();

            if (!options.ExcludedAssemblies.Contains(entryAssembly)
                && entryAssembly.GetName().Name.StartsWith(prefix))
            {
                assemblies.Add(entryAssembly);
            }

            if (options.IncludedAssemblies.Count > 0)
            {
                assemblies = assemblies
                    .Where(x => options.IncludedAssemblies.Contains(x))
                    .ToList();
            }

            return assemblies;
        }

        private static IList<KeyValuePair<Type, Type>> GetFilteredTypePairs(
            IServiceCollection services,
            Action<AddAllOptions> setupAction)
        {
            var options = new AddAllOptions();
            setupAction?.Invoke(options);

            var typesToRegister = GetTypesToRegister(options);

            var httpClientServices = services.Where(x =>
                x.ImplementationType is null &&
                x.ImplementationFactory != null)
                .Select(x => x.ServiceType)
                .ToList();

            typesToRegister = typesToRegister.Where(x => !httpClientServices.Contains(x.Key)).ToList();

            return typesToRegister;
        }

        private static IEnumerable<Type> GetImplementationsOfInterface(Assembly assembly, Type serviceType)
        {
            return assembly
                .GetTypes()
                .Where(type => serviceType.IsAssignableFrom(type)
                && !type.IsInterface
                && !type.IsAbstract);
        }


        private static IList<KeyValuePair<Type, Type>> GetTypesToRegister(AddAllOptions options)
        {
            var assemblies = GetAssemblies(options);
            var typesToRegister = GetAllServices(options, assemblies);

            return typesToRegister;
        }
    }
}
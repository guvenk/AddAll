using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace AddAll
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Adds all services automatically as transient with a configurable options.
        /// </summary>
        /// <param name="services">IServiceCollection services.</param>
        /// <param name="setupAction">Customizable configuration options.</param>
        /// <returns>A Reference to IServiceCollection after the operation has completed.</returns>
        public static IServiceCollection AddAllAsTransient(
            this IServiceCollection services,
            Action<AddAllOptions> setupAction = null)
        {
            var typePairs = ServicesExtensionsHelper.GetFilteredTypePairs(services, setupAction);

            foreach (var typePair in typePairs)
            {
                services.AddTransient(typePair.Key, typePair.Value);
            }

            return services;
        }

        public static IServiceCollection TryAddAllAsTransient(
           this IServiceCollection services,
           Action<AddAllOptions> setupAction = null)
        {
            var typePairs = ServicesExtensionsHelper.GetFilteredTypePairs(services, setupAction);

            foreach (var typePair in typePairs)
            {
                services.TryAddTransient(typePair.Key, typePair.Value);
            }

            return services;
        }
    }
}
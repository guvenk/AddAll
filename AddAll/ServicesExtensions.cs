using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace AddAll
{
    public static class ServicesExtensions
    {
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
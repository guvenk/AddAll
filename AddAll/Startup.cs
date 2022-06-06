using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace AddAll
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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

        }

        interface IMyService { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

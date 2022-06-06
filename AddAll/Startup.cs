using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AddAll
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var otherAssembly = Assembly.GetEntryAssembly();
            var someAssembly = Assembly.GetAssembly(typeof(ICustomAttributeProvider));

            services.AddAllAsTransient();
            services.AddAllAsTransient(options =>
            {
                options.PrefixAssemblyName = "Prefix";
                options.IncludedTypes = new List<Type> { typeof(ISomeService) };
                options.ExcludedTypes = new List<Type> { typeof(IOtherService) };
                options.IncludedAssemblies = new List<Assembly> { someAssembly };
                options.ExcludedAssemblies = new List<Assembly> { otherAssembly };
            });
        }

        interface ISomeService { }
        interface IOtherService { }

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

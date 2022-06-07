using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AddAll.TestLibrary;

namespace AddAll
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IMyService, MyService>();

            var entryAssembly = Assembly.GetEntryAssembly();
            var testAssembly = Assembly.GetAssembly(typeof(ITestService));

            //services.AddAllAsTransient(options =>
            //{
            //    options.PrefixAssemblyName = "AddAll";
            //    options.IncludedTypes = new List<Type> { typeof(IMyService2) };
            //    options.ExcludedTypes = new List<Type> { typeof(IMyService2) };
            //    options.IncludedAssemblies = new List<Assembly> { testAssembly };
            //    options.ExcludedAssemblies = new List<Assembly> { entryAssembly };
            //});


            services.AddAllAsTransient();


            var test1 = services
                .Where(x => x.ServiceType == typeof(ITestService))
                .Count();
            var test2 = services
                .Where(x => x.ServiceType == typeof(IOtherTestService))
                .Count();
            var test3 = services
                .Where(x => x.ServiceType == typeof(IMyService))
                .Count();
            var test4 = services
               .Where(x => x.ServiceType == typeof(IMyService2))
               .Count();

            var abc = 123;
        }

        interface IMyService { }
        class MyService : IMyService { }
        interface IMyService2 { }
        class MyService2 : IMyService2 { }

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

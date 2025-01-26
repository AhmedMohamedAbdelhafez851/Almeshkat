using BL.Data;
using BL.Healper;
using BL.Interfaces;
using BL.Services;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Almeshkat_Online_Schools.Utilities
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            // Register Unit of Work and Generic Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register custom logging service
            services.AddScoped(typeof(ILoggingService<>), typeof(LoggingService<>));

            // Register IHttpContextAccessor and MemoryCache
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            // Register interceptors
            services.AddSingleton<CachingInterceptor>();
            services.AddSingleton<LoggingInterceptor>();

            // Set up Castle Proxy Generator
            var proxyGenerator = new ProxyGenerator();
            services.AddSingleton(proxyGenerator);

            // Automatically register and proxy all services ending with "Service"
            var assembly = typeof(ApplicationDbContext).Assembly; // Adjust this to the assembly containing your services
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            foreach (var implementationType in serviceTypes)
            {
                // Find the interface matching the class (e.g., IClassService for ClassService)
                var interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, provider =>
                    {
                        var implementation = ActivatorUtilities.CreateInstance(provider, implementationType);
                        var loggingInterceptor = provider.GetRequiredService<LoggingInterceptor>();
                        var cachingInterceptor = provider.GetRequiredService<CachingInterceptor>();
                        return proxyGenerator.CreateInterfaceProxyWithTarget(
                            interfaceType,
                            implementation,
                            loggingInterceptor,
                            cachingInterceptor
                        );
                    });
                }
                else
                {
                    services.AddScoped(implementationType);
                }
            }
        }
    }
}

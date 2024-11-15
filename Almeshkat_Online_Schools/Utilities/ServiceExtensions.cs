using BL.Data;
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

            services.AddHttpContextAccessor();


            // Set up Castle Proxy Generator and Logging Interceptor
            var proxyGenerator = new ProxyGenerator();
            services.AddSingleton(proxyGenerator);
            services.AddSingleton<LoggingInterceptor>();

            // Automatically register and proxy all services ending with "Service"
            var assembly = typeof(ApplicationDbContext).Assembly; // Adjust this to the assembly containing your services

            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            foreach (var implementationType in serviceTypes)
            {
                // Find an interface with the same name as the class prefixed with "I"
                var interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

                if (interfaceType != null)
                {
                    // Register the service with proxy interception
                    services.AddScoped(interfaceType, provider =>
                    {
                        var implementation = ActivatorUtilities.CreateInstance(provider, implementationType);
                        var interceptor = provider.GetRequiredService<LoggingInterceptor>();
                        return proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, implementation, interceptor);
                    });
                }
                else
                {
                    // Register without proxy if no interface is found
                    services.AddScoped(implementationType);
                }
            }
        }
    }
}

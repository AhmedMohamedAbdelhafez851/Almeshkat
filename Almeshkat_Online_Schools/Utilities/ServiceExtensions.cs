using BL.Data;
using BL.Services;
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
            // Load the assembly where your services are located
            var assembly = typeof(ApplicationDbContext).Assembly; // Adjust to the actual assembly containing services

            // Find all class types that end with "Service" and are not abstract
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            // Register each service type found
            foreach (var implementationType in serviceTypes)
            {
                // Find an interface with the same name as the class prefixed with "I"
                var interfaceType = implementationType.GetInterface($"I{implementationType.Name}");

                if (interfaceType != null)
                {
                    // Register the service with its interface
                    services.AddScoped(interfaceType, implementationType);
                }
                else
                {
                    // Register the service without an interface if none is found
                    services.AddScoped(implementationType);
                }
            }
        }
    }
}

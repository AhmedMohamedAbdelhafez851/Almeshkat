using Castle.Core;
using Castle.DynamicProxy;

namespace Almeshkat_Online_Schools.Utilities
{
    public static class ProxyGeneratorExtensions
    {
        public static void AddInterceptedScoped<TInterface, TImplementation>(
            this IServiceCollection services,
            ProxyGenerator proxyGenerator)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            services.AddScoped<TImplementation>();
            services.AddScoped<TInterface>(provider =>
            {
                var implementation = provider.GetRequiredService<TImplementation>();
                var interceptor = provider.GetRequiredService<LoggingInterceptor>();
                return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, interceptor);
            });
        }
    }
}

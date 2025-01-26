using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace BL.Healper
{
    public class CachingInterceptor : IInterceptor
    {
        private readonly IMemoryCache _memoryCache;

        public CachingInterceptor(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Intercept(IInvocation invocation)
        {
            var cacheableAttr = invocation.Method.GetCustomAttributes(true)
                .OfType<CacheableAttribute>()
                .FirstOrDefault();

            if (cacheableAttr == null)
            {
                // Proceed with method execution if no CacheableAttribute is present
                invocation.Proceed();
                return;
            }

            var cacheKey = cacheableAttr.CacheKey;
            if (_memoryCache.TryGetValue(cacheKey, out var cachedResult))
            {
                invocation.ReturnValue = cachedResult;
                return;
            }

            invocation.Proceed();

            if (invocation.ReturnValue != null)
            {
                _memoryCache.Set(cacheKey, invocation.ReturnValue, TimeSpan.FromSeconds(cacheableAttr.DurationInSeconds));
            }
        }
    }
}

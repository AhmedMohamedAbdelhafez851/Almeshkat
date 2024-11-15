using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;

namespace Almeshkat_Online_Schools.Utilities
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Intercept(IInvocation invocation)
        {
            var className = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var arguments = string.Join(", ", invocation.Arguments.Select(arg => $"{arg?.GetType().Name}: {arg}"));

            var requestUrl = _httpContextAccessor.HttpContext?.Request?.Path +
                             _httpContextAccessor.HttpContext?.Request?.QueryString;

            Log.Information("Starting operation: {ClassName}.{MethodName} with arguments: {Arguments}, URL: {RequestUrl}",
                className, methodName, arguments, requestUrl);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                stopwatch.Stop();

                Log.Information("Operation {ClassName}.{MethodName} completed successfully in {ElapsedMilliseconds}ms. URL: {RequestUrl}",
                    className, methodName, stopwatch.Elapsed.TotalMilliseconds, requestUrl);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                Log.Error(ex, "Operation {ClassName}.{MethodName} failed after {ElapsedMilliseconds}ms. URL: {RequestUrl}",
                    className, methodName, stopwatch.Elapsed.TotalMilliseconds, requestUrl);

                throw;
            }
        }
    }
}

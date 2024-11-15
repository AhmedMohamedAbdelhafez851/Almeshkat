using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System;
namespace Almeshkat_Online_Schools.Utilities
{
    public static class SerilogConfig
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() // Set default logging level
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Override logging level for Microsoft namespaces
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}") // Console output template
                .WriteTo.Async(a => a.File(
                    path: "Logs/log-.txt", // Rolling text log files (one per day)
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB file size limit
                    rollOnFileSizeLimit: true, // Roll the file if it exceeds the limit
                    retainedFileCountLimit: 30)) // Keep 30 days of log files
                .CreateLogger();
        }
    }



}









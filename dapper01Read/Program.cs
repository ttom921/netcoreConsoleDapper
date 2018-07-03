using Autofac;
using GomoApp;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.RollingFileAlternate;
using System;

namespace dapper01Read
{
    class Program
    {
        static int Main(string[] args)
        {
            #region 設定Serilog
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.RollingFileAlternate("Log_Data", LogEventLevel.Warning)
               .CreateLogger();
            #endregion
            try
            {
                Log.Information("Starting console app");
                // Startup.cs finally :)
                Startup startup = new Startup();
                var container = startup.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IApplication>();
                    app.Run();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

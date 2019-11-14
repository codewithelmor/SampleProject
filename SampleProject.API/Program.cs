using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace SampleProject.API
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Map(
                    keyPropertyName: "Name",
                    defaultKey: "application",
                    configure: (name, configuration) =>
                        configuration.File(
                            formatter: new CompactJsonFormatter(),
                            path: $"./logs/log-{name}.json",
                            rollingInterval: RollingInterval.Day))
                .CreateLogger();

            //Log.Information("Hello ,{Name}!", "sample");

            try
            {
                Log.Information("Starting web host");

                var host = CreateHostBuilder(args).Build();

                host.Run();

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return Task.FromException(ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}

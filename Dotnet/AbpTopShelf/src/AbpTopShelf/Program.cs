using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using Topshelf;

namespace AbpTopShelf
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            try
            {
                Log.Information("Starting console host.");

                HostFactory.Run(x =>
                {
                    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
                    var name = configuration["ServiceName"];
                    x.SetServiceName(name);
                    x.SetDisplayName(name);
                    x.SetDescription(name);
                    x.Service<IHost>(s =>
                    {
                        s.ConstructUsing(() =>
                            CreateHostBuilder(args).UseConsoleLifetime().Build()
                        );

                        s.WhenStarted(service => service.StartAsync(default));
                        s.WhenStopped(service => service.StopAsync(default));
                    });
                });
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseAutofac()
                .UseSerilog()
                .ConfigureAppConfiguration((context, config) =>
                {
                    //setup your additional configuration sources
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<AbpTopShelfModule>();
                })
                .UseWindowsService();
    }
}
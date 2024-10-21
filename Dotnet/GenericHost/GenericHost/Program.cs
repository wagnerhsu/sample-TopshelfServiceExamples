using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Topshelf;
using Host = Microsoft.Extensions.Hosting.Host;

namespace GenericHost
{
    public class Program
    {
        public static IConfiguration Configuration =>
            new ConfigurationBuilder().AddJsonFile("appsettings.json",false,true).Build();
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                var serviceName = Configuration["ServiceName"];
                x.SetServiceName(serviceName);
                x.SetDisplayName(serviceName);
                x.SetDescription(serviceName);
                x.StartAutomatically();
                x.RunAsLocalSystem();
                x.Service<IHost>(s =>
                {
                    s.ConstructUsing(() => CreateHostBuilder(args).Build());
                    s.WhenStarted(async service =>await service.StartAsync(default));
                    s.WhenStopped(async service =>await service.StopAsync(default));
                });
            });
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                        { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    NLog.LogManager.LoadConfiguration("nlog.config");
                })
                .UseConsoleLifetime()
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}

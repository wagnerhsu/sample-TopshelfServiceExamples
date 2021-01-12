using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Threading.Tasks;
using Topshelf;

namespace AbpTopShelfNLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
                var name = configuration["ServiceName"];
                x.SetServiceName(name);
                x.SetDisplayName(name);
                x.SetDescription(name);

                x.Service<IHost>(s =>
                {
                    s.ConstructUsing(() => CreateHostBuilder(args).Build());
                    s.WhenStarted(service =>
                    {
                        Task.Run(() => service.StartAsync());
                    });
                    s.WhenStopped(service =>
                    {
                        Task.Run(() => service.StopAsync());
                    });
                });
            });
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseAutofac()
                .ConfigureLogging(logBuilder =>
                {
                    logBuilder.ClearProviders();
                    logBuilder.SetMinimumLevel(LogLevel.Trace);
                    logBuilder.AddNLog(new NLogProviderOptions
                    { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    NLog.LogManager.LoadConfiguration("nlog.config");
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    //setup your additional configuration sources
                    
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<AbpTopShelfNLogModule>();
                });
    }
}
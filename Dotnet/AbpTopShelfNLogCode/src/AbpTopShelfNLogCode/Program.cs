using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using NLogWrapper;


namespace AbpTopShelfNLogCode
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            NLogWrapper.NLogServiceNoConfigFile.BuildLogingConfiguration(new NLogOptions());

            NLog.ILogger Log = LogManager.GetCurrentClassLogger();
            try
            {
                Log.Info("Starting console host.");
                await CreateHostBuilder(args).RunConsoleAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                LogManager.Shutdown();
            }

        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseAutofac()
                .ConfigureLogging(NLogWrapper.NLogServiceNoConfigFile.ConfigureLogging)
                .ConfigureAppConfiguration((context, config) =>
                {
                    //setup your additional configuration sources
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<AbpTopShelfNLogCodeModule>();
                });
    }
}

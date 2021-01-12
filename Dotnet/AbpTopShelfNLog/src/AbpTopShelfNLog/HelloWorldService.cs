using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace AbpTopShelfNLog
{
    public class HelloWorldService : ITransientDependency
    {
        private readonly ILogger<HelloWorldService> _logger;

        public HelloWorldService(ILogger<HelloWorldService> logger)
        {
            this._logger = logger;
        }

        public void SayHello()
        {
            _logger.LogInformation("Hello World!");
        }
    }
}
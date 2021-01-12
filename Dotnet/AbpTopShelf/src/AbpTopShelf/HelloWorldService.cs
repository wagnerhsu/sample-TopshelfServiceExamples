using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.DependencyInjection;

namespace AbpTopShelf
{
    public class HelloWorldService : ITransientDependency
    {
        private readonly ILogger<HelloWorldService> _logger;

        public HelloWorldService(ILogger<HelloWorldService> logger)
        {
            _logger = logger;
        }

        public void SayHello()
        {
            _logger.LogInformation("Hello World!");
        }
    }
}

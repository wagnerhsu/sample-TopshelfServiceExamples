using System;
using Volo.Abp.DependencyInjection;

namespace AbpTopShelfNLogCode
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}

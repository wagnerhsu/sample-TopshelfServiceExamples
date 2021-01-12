using System;
using Volo.Abp.DependencyInjection;

namespace AbpTopShelf
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}

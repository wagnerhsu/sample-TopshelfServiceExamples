 ## Add TopShelf support for Abp Console Application
- Create Abp console application
- Add package `Microsoft.Extensions.Hosting.WindowsServices`
- Add `UseWindowsService` for `IHostBuilder`
- Construct IHost
```cs
s.ConstructUsing(service => CreateHostBuilder(args).UseConsoleLifetime().Build());
```

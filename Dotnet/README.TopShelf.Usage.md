## Create Windows Service for .NET 5.0
- New a project
```c#
dotnet new worker
```
- Add TopShelf package
```c#
dotnet add package TopShelf
```
- Modify Program.cs to enable TopShelf support
```cs
public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
```
## Create Windows Service for Abp console template
- Create Abp console application
- Add package `Microsoft.Extensions.Hosting.WindowsServices`
- Add `UseWindowsService` and `UseConsoleLifetime` for `IHostBuilder`
- Construct IHost

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

## Create Windows Service for Abp console template
- Create Abp console application
- Add package `Microsoft.Extensions.Hosting.WindowsServices`
- Add `UseWindowsService` and `UseConsoleLifetime` for `IHostBuilder`
- Construct IHost

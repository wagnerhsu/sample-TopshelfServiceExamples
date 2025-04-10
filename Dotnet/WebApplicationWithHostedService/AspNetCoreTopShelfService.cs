namespace WebAplicationDemo;

public class AspNetCoreTopShelfService
{
    private readonly IHost _host;

    public AspNetCoreTopShelfService(IHost host)
    {
        _host = host;
    }

    public bool Start()
    {
        // Start the ASP.NET Core application
        _ = _host.StartAsync();
        return true;
    }

    public bool Stop()
    {
        // Stop the ASP.NET Core application
        _ = _host.StopAsync();
        return true;
    }
}
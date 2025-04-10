namespace WebAplicationDemo;

public class MyHostedService : IHostedService, IDisposable
{
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hosted Service Starting...");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        Console.WriteLine($"Hosted Service is working: {DateTime.Now}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hosted Service Stopping...");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
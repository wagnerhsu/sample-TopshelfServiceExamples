using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Topshelf;
using WebAplicationDemo;

var builder = WebApplication.CreateBuilder(args);

// Register the Hosted Service
builder.Services.AddHostedService<MyHostedService>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the WebApplication
var app = builder.Build();
app.UseSwagger();
    app.UseSwaggerUI();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
// Configure TopShelf
HostFactory.Run(x =>
{
    x.Service<AspNetCoreTopShelfService>(s =>
    {
        s.ConstructUsing(name => new AspNetCoreTopShelfService(app));
        s.WhenStarted(tc => tc.Start());
        s.WhenStopped(tc => tc.Stop());
    });

    x.RunAsLocalSystem();
    x.SetDescription("ASP.NET Core Application with Hosted Service");
    x.SetDisplayName("AspNetCoreHostedService");
    x.SetServiceName("AspNetCoreHostedService");
});

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

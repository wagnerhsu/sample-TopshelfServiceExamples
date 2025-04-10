using Microsoft.AspNetCore.Mvc;

namespace WebAplicationDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public string Index()
    {
        return "Hello";
    }
}
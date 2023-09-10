using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers;

[ApiController]
[Route("[controller]")]
public class TranslationController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public TranslationController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTranslation")]
    public string Get()
    {
        return "Hello World!";
    }
}

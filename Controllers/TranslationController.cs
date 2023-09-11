using Microsoft.AspNetCore;
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

    private readonly ILogger<TranslationController> _logger;

    public TranslationController(ILogger<TranslationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTranslation")]
    public string Get()
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        return "Hello World!";
    }

    [HttpGet("{text}")]
    public string GetJPTranslation(string text) {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        return text;
    }
}

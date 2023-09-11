using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Routing.Constraints;
using System.IO;

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

        StreamReader sr = new StreamReader("./dict.txt");
        int len = text.Length;
        string? line = sr.ReadLine();
        while (line != null) {
            if (line.Length > len+2 && line.Substring(0, len+1).Equals(text.ToLower() + ':')) {
                return line.Substring(len+2);
            }
            line = sr.ReadLine();
        }
        return "Not in dictionary :(";
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

    [HttpGet("en/jp/{text}")]
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

    [HttpGet("jp/en/{text}")]
    public string GetENTranslation(string text) {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        StreamReader sr = new StreamReader("./dict.txt");
        int len = text.Length;
        string? line = sr.ReadLine();
        while (line != null) {
            if (line.Length > len+2 && line.Substring(line.Length - len - 2).Equals(": " + text.ToLower())) {
                return line.Substring(0, line.Length - len - 2);
            }
            line = sr.ReadLine();
        }
        return "Not in dictionary :(";
    }

    [HttpGet("rand/en/{num}")]
    public string[] GetRandomENWords(int num) {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        if (num <= 0) {
            return new string[0];
        }

        // Get size num array of random numbers between 0 and 999 with no repeats.
        Random rand = new Random();
        List<int> choiceList = new List<int>();
        for (int i = 0; i < num; i++) {
            int nextChoice = rand.Next(1000);
            while (choiceList.Contains(nextChoice)) {
                nextChoice = rand.Next(1000);
            }
            choiceList.Add(nextChoice);
        }
        int[] choices = choiceList.ToArray();
        Array.Sort(choices);

        // For each number in the array, get that entry in the dictionary.
        StreamReader sr = new StreamReader("./dict.txt");
        string[] result = new string[num];
        int currIndex = 0;
        int curr = choices[currIndex];
        for (int i = 0; i < 1000; i++) {
            if (i == curr) {
                string? line = sr.ReadLine();
                int demarcIndex = line.IndexOf(":");
                result[currIndex] = line.Substring(0, demarcIndex);
                currIndex++;
                if (currIndex >= num) break;
                curr = choices[currIndex];
            } else {
                sr.ReadLine();
            }
        }

        Debug.Assert(result.Length == num);

        return result;
    }

    [HttpGet("rand/jp/{num}")]
    public string[] GetRandomJPWords(int num) {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        if (num <= 0) {
            return new string[0];
        }

        // Get size num array of random numbers between 0 and 999 with no repeats.
        Random rand = new Random();
        List<int> choiceList = new List<int>();
        for (int i = 0; i < num; i++) {
            int nextChoice = rand.Next(1000);
            while (choiceList.Contains(nextChoice)) {
                nextChoice = rand.Next(1000);
            }
            choiceList.Add(nextChoice);
        }
        int[] choices = choiceList.ToArray();
        Array.Sort(choices);

        // For each number in the array, get that entry in the dictionary.
        StreamReader sr = new StreamReader("./dict.txt");
        string[] result = new string[num];
        int currIndex = 0;
        int curr = choices[currIndex];
        for (int i = 0; i < 1000; i++) {
            if (i == curr) {
                string? line = sr.ReadLine();
                int demarcIndex = line.IndexOf(":");
                result[currIndex] = line.Substring(demarcIndex+1);
                currIndex++;
                if (currIndex >= num) break;
                curr = choices[currIndex];
            } else {
                sr.ReadLine();
            }
        }

        Debug.Assert(result.Length == num);

        return result;
    }
}

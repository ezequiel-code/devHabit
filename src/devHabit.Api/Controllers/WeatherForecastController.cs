using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace devHabit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    private readonly ILogger<HelloController> _logger;
    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;   
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("GetWeatherForecast");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC =  RandomNumberGenerator.GetInt32(-20, 55),
                Summary = Summaries[RandomNumberGenerator.GetInt32(Summaries.Length)]
            })
            .ToArray();
    }
}
